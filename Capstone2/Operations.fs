namespace Capstone2
    
    open System

    module Operations = 

        let deposit (amount:decimal)(account:Account) : Account = 
            { account with Balance = account.Balance + amount }

        let withdraw (amount:decimal)(account:Account):Account = 
            if amount > account.Balance then account
            else { account with Balance = account.Balance - amount}

        let auditAs operationName audit operation amount account =
            audit account (sprintf "%O: Performing a %s operation for $%M" DateTime.UtcNow operationName amount)
            
            let updatedAccount = operation amount account

            let accountIsUnchanged = (updatedAccount = account)

            if accountIsUnchanged then audit account (sprintf "%O: Transaction rejected!" DateTime.UtcNow)
            else audit account (sprintf "%O: Transaction accepted! Balance is now $%M" DateTime.UtcNow updatedAccount.Balance)

            updatedAccount

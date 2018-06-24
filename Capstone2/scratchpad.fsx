// Domain
type Customer = {
    Name: string
}

type Account = {
    Owner: Customer
    Balance: decimal
    AccountId: System.Guid
}

let firstAccount = {
    Owner={Name="Luis"}
    Balance=100M
    AccountId = System.Guid.Empty
}

//Operations
let deposit (amount:decimal)(account:Account) : Account = 
    { account with Balance = account.Balance + amount }

let withdraw (amount:decimal)(account:Account):Account = 
    if amount > account.Balance then account
    else { account with Balance = account.Balance - amount}

//Logging and Auditing
 
open System.IO

let createFilePath account = sprintf "%s/%A.txt" account.Owner.Name account.AccountId

let fileSystemAudit account message =

    let baseDir = "Capstone2"

    let dirPath = System.IO.Path.Combine(baseDir,account.Owner.Name)

    if Directory.Exists(dirPath) <> true then Directory.CreateDirectory(dirPath) |> ignore

    let savePath = createFilePath account

    File.AppendAllText(savePath,message + "\n")

let consoleAudit account message = 
    printfn "Acount %A: %s" account.AccountId message

fileSystemAudit firstAccount "Hello World"
fileSystemAudit firstAccount "Testing Again"

let customer = {Name="Isaac"}
let account = {AccountId = System.Guid.Empty;Owner=customer;Balance=90M}

let newAccount = account |> withdraw 10M

newAccount.Balance = 80M //Should be true

consoleAudit account "Testing console auditor"

// Higher Order Functions
let auditAs operationName audit operation amount account = 
    let newAccount = account |> operation amount
    newAccount

let withdrawWithConsoleAudit = auditAs "withdraw" consoleAudit withdraw
let depositWithConsoleAudit = auditAs "deposit" consoleAudit deposit

let account = {AccountId=System.Guid.NewGuid();Owner = {Name="Isaac"};Balance=100M}

account |> deposit 100M |> withdraw 50M

account |> depositWithConsoleAudit 100M |> withdrawWithConsoleAudit 50M    






    




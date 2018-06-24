namespace Capstone2

    type Customer = {
        Name: string
    }

    type Account = {
        Owner: Customer
        Balance: decimal
        AccountId: System.Guid
    }

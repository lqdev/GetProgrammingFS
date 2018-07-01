#load "Domain.fs"
#load "Operations.fs"

open System
open Capstone3.Operations
open Capstone3.Domain

let openingAccount = {
    Owner = {Name = "Isaac"}
    Balance = 0M
    AccountId = Guid.Empty
}

let commands = ['d';'w';'z';'f';'d';'x';'w']

let isValidCommand command = 
    command = 'x' || command = 'd' || command = 'w'

let isStopCommand command = 
    command = 'x'

let getAmount command = 
    if command = 'd' then ('d', 50M)
    elif command = 'w' then ('w', 25M)
    else ('x', 0M)

let processCommand account command =
    let operation,amount = command
    if operation = 'd' then deposit amount account
    elif operation = 'w' then withdraw amount account
    else account

commands
|> Seq.filter isValidCommand
|> Seq.takeWhile (not << isStopCommand)
|> Seq.map getAmount
|> Seq.fold processCommand openingAccount

namespace Capstone2
    
    open System.IO;

    module Auditing = 
        let fileSystem account message =

            Directory.CreateDirectory(sprintf @"Capstone2/%s" account.Owner.Name) |> ignore

            let savePath = sprintf "Capstone2/%s/%O.txt" account.Owner.Name account.AccountId

            File.AppendAllLines(savePath,[message])

        let console account message = 
            printfn "Account %O: %s" account.AccountId message           
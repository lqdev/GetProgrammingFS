// Your first higher order function in F# Pg 154

open System

type Customer = { Age:int }

let where filter customers = 
    seq {
        for customer in customers do
            if filter customer then 
                yield customer
    }

let customers = [
    {Age = 21};
    {Age = 25};
    {Age = 36};
]

let isOver35 customer = customer.Age > 35

customers |> where isOver35 //Supplying the Functions
customers |> where (fun customer -> customer.Age > 35) //Inline Syntax

// Exercise Pg 157
let printCustomerAge writer customer = 
    if customer.Age < 13 then writer "Child"
    elif customer.Age < 20 then writer "Teenager"
    else writer "Adult"


printCustomerAge Console.WriteLine {Age = 35}
printCustomerAge Console.WriteLine {Age = 11}
printCustomerAge Console.WriteLine {Age = 13}

// Partially Applying A Function with Dependencies Pg 158

let printToConsole = printCustomerAge Console.WriteLine

printToConsole {Age = 21}
printToConsole {Age = 12}
printToConsole {Age = 18}

// Creating A Dependency To Write To File Pg 158
open System.IO

let writeToFile text = File.WriteAllText(@"output.txt",text)

let printToFile = printCustomerAge writeToFile
printToFile {Age = 21}



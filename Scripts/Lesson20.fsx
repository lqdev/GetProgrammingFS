// 20.1 for .. in loops in F#
for number in 1..10 do
    printfn "%d Hello!" number

for number in 10..-1..1 do
    printfn "%d Hello!" number   

let customerIds = [45 .. 99]
for customerId in customerIds do
    printfn "%d bought something!" customerId

for even in 2 .. 2 .. 10 do
    printfn "%d is an even number!" even    

// 20.2 while loops in F#
// open System.IO
// let reader = new StreamReader(File.OpenRead @"File.txt")
// while (not reader.EndOfStream) do
//     printfn "%s" (reader.ReadLine())    

//20.3 Comprehensions in F#
open System
let arrayOfChars = [|for c in 'a' .. 'z' -> Char.ToUpper c|]
let listOfSquares = [ for i in 1..10 -> i * i]
let seqOfStrings = seq {for i in 2..4..20 -> sprintf "Number %d" i}

//20.4 If/then expressions for complex logic
// let limit = 
//     if score = "medium" && years = 1 then 500
//     elif score = "good" && (years = 0 || years = 1) then 750
//     elif score = "good" && years = 2 then 1000
//     elif score = "good" then 2000
//     else 250

//20.5 OUr first pattern-matching example
// let limit = 
//     match customer with
//     | "medium",1 -> 500
//     | "good",0 | "good",1 -> 750
//     | "good",2 -> 1000
//     | "good" -> 2000
//     | _ -> 2000

// Now you try
let getCreditLimit customer =  
    match customer with
    | "medium",1 -> 500
    | "good",0 | "good",1 -> 750
    | "good",2 -> 1000
    | "good",_ -> 2000
    | _ -> 250
    
    
getCreditLimit ("medium",1)    

//20.6 Uisng the when guard clause
let getCreditLimit customer = 
    match customer with
    | "medium",1 -> 500
    | "good", years when years < 2 -> 750
    | "good",2 -> 1000
    | "good",_ -> 2000
    | _ -> 250
    
//20.7 Nesting matches inside one another
let getCreditCustomer customer = 
    match customer with
    | "medium",1 -> 500
    | "good", years ->
        match years with
        | 0 | 1 -> 750
        | 2 -> 1000
        | _ -> 2000
    | _ -> 250

// Now you try
type Customer = {
    Balance: int
    Name: string
}    

let handleCustomer (custlist:Customer list) = 
    match custlist.Length with 
    | 0 -> failwith "No customers supplied!"
    | 1 -> printfn "%s" custlist.Head.Name
    | 2 -> printfn "%d" ([for i in 0..1 -> custlist.[i]] |> List.sumBy(fun x -> x.Balance))
    | _ -> printfn "%d" custlist.Length

//20.8 Matching against lists
let handleCustomer customers = 
    match customers with 
    | [] -> failwith "No customer supplied!"
    | [customer] -> printfn "Single customer, name is %s" customer.Name
    | [first;second] -> printfn "Two customers balance = %d" (first.Balance + second.Balance)
    | customers -> printfn "Customers supplied %d" customers.Length    

handleCustomer []
handleCustomer [ { Balance = 10; Name = "Joe" } ]

//20.9 Pattern matching with records
let getStatus customer = 
    match customer with
    | {Balance = 0} -> "Customer has empty balance!"
    | {Name = "Isaac" } -> "This is a great customer!"
    | {Name = name;Balance = 50} -> sprintf "%s has a large balance!" name
    | {Name = name} -> sprintf "%s is a normal customer" name

{Balance = 50; Name = "Joe"} |> getStatus    

//20.10 Combining multiple patterns
let getStatus customer = 
    match customer with
    | [ { Name = "Tanya" }; { Balance = 25 }; _ ] -> "It's a match!"
    | _ -> "No match!"

//20.11 When to use if/then over match
// if customer.Name = "Isaac" then printfn "Hello!"
// match customer.Name with
// | "Isaac" -> printfn "Hello!"
// | _ -> ()    


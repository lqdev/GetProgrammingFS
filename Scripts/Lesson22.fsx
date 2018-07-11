// 22.4 Trying to set an F# type value to null
let myMainDisk = 
{
    Manufacturer = "HardDisks Inc."
    SizeGb = 500
    DiskData = null
}

// 22.5 Sample code to calculate a premium

let aNumber: int = 10
let maybeANUmber: int option = Some 10

let calculateAnnualPremiumUsd score = 
    match score with
    | Some 0 -> 250
    | Some score when score < 0 -> 400
    | Some score when score > 0 -> 150
    | None -> printfn "No score supplied! Using temporary premium. "; 300

calculateAnnualPremiumUsd(Some 250)
calculateAnnualPremiumUsd None

// Now you try
type Driver = {
    Name: string
    SafetyScore: int option
    YearPassed: int
}

let drivers = [
    {Name="Fred Smith";SafetyScore=Some 500;YearPassed=1980}
    {Name="Jane Dunn";SafetyScore=None;YearPassed=1980}
]

let calculateAnnualPremiumUsd customer = 
    match customer.SafetyScore with
    | Some 0 -> 250
    | Some score when score < 0 -> 400
    | Some score when score > 0 -> 150
    | None -> printfn "No score supplied! Using temporary premium."; 300
    
customers |> List.map calculateAnnualPremiumUsd

// 22.6 Matching and Mapping
let customer = customers.[0]
let describe safetyScore = if safetyScore > 200 then "Safe" else "High Risk"

let description = 
    match customer.SafetyScore with
    | Some score -> Some(describe score)
    | None -> None

let descriptionTwo = 
    customer.SafetyScore
    |> Option.map(fun score -> describe score)

let shorthand = customer.SafetyScore |> Option.map describe
let optionalDescribe = Option.map describe

// 22.7 Chaining functions that return an option with Option.bind
// Unwraps List of List
let tryFindCustomer cId = 
    if cId = 10 then 
        Some drivers.[0] 
    else None
let getSafetyScore customer = customer.SafetyScore
let score = tryFindCustomer 10 |> Option.bind getSafetyScore

// 22.8 Filtering on options
let test1 = Some 5 |> Option.filter(fun x -> x > 5)
let test1 = Some 5 |> Option.filter(fun x -> x = 5)

// Now you try
let tryLoadCustomer id = 
    match id with
    | id when (id >=2 && id <= 7) -> Some (sprintf "Customer %d" id)
    | _ -> None

let customerIds = [0..10]

customerIds |> List.choose tryLoadCustomer

//Try This
open System.IO

let printFileInfo path = 
    let info = new FileInfo(path)
    sprintf "%A" info

let tryPrintFileInfo path = 
    let exists = File.Exists path
    match exists with
    | true -> Some (path |> printFileInfo)
    | false -> Some "No file here"
    | _ -> None


tryPrintFileInfo "C:\\Users\\lqdev\\Development\\nodejs\\vuetifyrouterdemo\\demo.png"
tryPrintFileInfo "C:\\Users\\lqdev\\Development\\nodejs\\vuetifyrouterdemo\\fakefile.tsv"
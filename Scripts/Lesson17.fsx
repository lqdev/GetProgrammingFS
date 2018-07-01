// 17.1 Standard dictionary functionality

open System.Collections.Generic

let inventory = Dictionary<string,float>()
inventory.Add("Apples",0.33)
inventory.Add("Oranges",0.23)
inventory.Add("Bananas",0.45)
inventory.Remove "Oranges"
let banaanas = inventory.["Bananas"]
let oranges = inventory.["Oranges"]

// 17.2 Generic type inference with Dictionary
let inventory = Dictionary<_,_>() //Explicit placeholders for generic type args
inventory.Add("Apples",0.33)
let inventory = Dictionary() // Imitting generic type args completely
inventory.Add("Apples",0.33)

//17.3 Creating an immutable IDictionary
let inventory : IDictionary<string,float> = 
    ["Apples",0.33;"Oranges",0.23;"Bananas",0.45]
    |> dict
let bananas = inventory.["Bananas"]    
inventory.Add("Pineapples",0.85)
inventory.Remove("Bananas")

// Quickly creating full dictionaries
["Apples",10;"Bananas",20;"Grapes",15]
|> dict
|> Dictionary

// 17.4 Using the F# Map lookup
let inventory = 
    ["Apples",0.33;"Oranges",0.23;"Bananas",0.45]
    |> Map.ofList

let apples = inventory.["Apples"]
let pineapples = inventory.["Pineapples"]

let newInventory = 
    inventory
    |> Map.add "Pineapples" 0.87
    |> Map.remove "Apples"

// 17.5 Uinsg the F# Map module functions
let cheapFruit, expensiveFruit = 
    inventory
    |> Map.partition(fun fruit cost -> cost < 0.3)


// Now you try
open System
open System.IO

Directory.EnumerateDirectories "."
|> Seq.map(fun x -> new DirectoryInfo(x))
|> Seq.map(fun x -> (x.Name,x.CreationTimeUtc))
|> Map.ofSeq
|> Map.map(fun x y -> DateTime.Now.Subtract(y).Days)

// 17.6 Creating a set from a sequence
let myBasket = ["Apples";"Apples";"Apples";"Bananas";"Pineapples"]
let fruitsILike = myBasket |> Set.ofList

//17.7 Comparing List - and Set-based operations
let yourBasket = ["Kiwi";"Bananas";"Grapes"]
let allFruitsList = (myBasket @ yourBasket) |> List.distinct

let fruitsYouLike = yourBasket |> Set.ofList
let allFruits = fruitsILike + fruitsYouLike

//17.8 Sample Set-based operations
let fruitsJustForMe = allFruits - fruitsYouLike //A not B
let fruitsWeCanShare = fruitsILike |> Set.intersect fruitsYouLike //A and B
let doILikeAllYouFruits = fruitsILike |> Set.isSubset fruitsYouLike //All A in B

//Try this

// Copy functions from Lesson 16
let files = getSubDirectoryInfo "."

let folders = 
    files 
    |> List.map (fun directory -> (directory.Name,Set.ofList directory.DistinctExt))
    |> Map.ofList

let getSharedFiles dir1 dir2 = dir1 |> Set.intersect dir2

getSharedFiles 
    folders.["C:\\Users\\lqdev\\Development\\dotnet\\GetProgrammingFS\\Capstone1"]
    folders.["C:\\Users\\lqdev\\Development\\dotnet\\GetProgrammingFS\\Lesson12"]
// 15.1 Sample dataset of football results Pg 174 

type FootbalResult = 
    {
        HomeTeam: string
        AwayTeam: string
        HomeGoals: int
        AwayGoals: int
    }

let create (ht,hg)(at,ag) = {HomeTeam=ht;AwayTeam=at;HomeGoals=hg;AwayGoals=ag}

let results = 
    [
        create("Messiville",1)("Ronaldo City",2)
        create("Messiville",1)("Bale Town",3)
        create("Bale Town",1)("Ronaldo City",1)
        create("Bale Town",1)("Messiville",1)
        create("Ronaldo City",1)("Messiville",2)
        create("Ronaldo City",1)("Bale Town",2)
    ]

//15.2 An Imperative Solution to a Calculation Over Data Pg 175
open System.Collections.Generic
type TeamSummary = {Name: string;mutable AwayWins:int}
let summary = ResizeArray<TeamSummary>()

for result in results do
    if result.AwayGoals > result.HomeGoals then
        let mutable found = false
        for entry in summary do
            if entry.Name = result.AwayTeam then
                found <- true
                entry.AwayWins <- entry.AwayWins + 1
            if not found then
                summary.Add {Name=result.AwayTeam;AwayWins=1}

let comparer = 
    {new IComparer<TeamSummary> with 
        member this.Compare(x,y) = 
            if x.AwayWins > y.AwayWins then -1
            elif x.AwayWins < y.AwayWins then 1
            else 0
    }                

summary.Sort(comparer)

//15.4 A declarative solution to a calculation over data Pg 178
let isAwayWin result = result.AwayGoals > result.HomeGoals

results
|> List.filter isAwayWin
|> List.countBy(fun result -> result.AwayTeam)
|> List.sortByDescending(fun(_,awayWins) -> awayWins)

//15.5 Working with .NET arrays in F# Pg 182
let numbersArray = [|1;2;3;4;6|]
let firstNumber = numbersArray.[0]
let firstThreeNumbers = numbersArray.[0..2]
numbersArray.[0] <- 99

//15.6 Workign with F# lists Pg 183
let numbers = [1;2;3;4;5;6] //Creating a list of 6 numbers
let numbersQuick = [1..6] //Shorthand for of list creation (also valid on arrays and sequences)
let head :: tail = numbers //Decomposing a list into head (1) and a tail 2..6

let moreNumbers = 0 :: numbers // Creating a new list by placing 0 at the front of numbers
let evenMoreNumbers = moreNumbers @ [7..9] //Appending more numbers and 7..9 together to create a new list

//Special Operations
// Create [a;b;c]
// Deconstruct into single item (head) and remainder tail ::
// Place single item at front of list ::
// Merge two lists with @

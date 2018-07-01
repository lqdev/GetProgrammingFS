// 18.1 
type Sum = int seq -> int
type Average = float seq -> float
type Count<'T> = 'T seq -> int

//18.2 Imperative implementation of sum
let sum inputs = 
    let mutable accumulator = 0
    for input in inputs do
        accumulator <- accumulator + input
    accumulator

//Now You Try
let average inputs = 
    let mutable accumulator = 0.0
    let mutable accumulator2 = 0.0
    for input in inputs do
        accumulator <- accumulator + input
        accumulator2 <- accumulator2 + 1.0
    accumulator / accumulator2

let length inputs = 
    let mutable accumulator = 0
    for input in inputs do
        accumulator <- accumulator + 1
    accumulator   
let maxVal inputs = 
    let mutable accumulator = 0;
    for input in inputs do
        if input > accumulator then accumulator <- input 
        else accumulator |> ignore
    accumulator

// 18.3 Implementing sum through fold
let sum inputs = 
    Seq.fold(fun state input -> state + input) 0 inputs

// 18.4 Looking at fold with logging
let sum inputs =
    Seq.fold(fun state input -> 
        let newState = state + input
        printfn "Current state is %d, input is %d, new state value is %d" state input newState 
        newState) 0 inputs

//Now You Try
let length inputs = 
    Seq.fold(fun state input -> state + 1) 0 inputs            

let maxVal inputs = 
    Seq.fold(fun state input -> 
        if input > state then input else state) 0 inputs

//18.5 Making fold read in a more logical way
// Seq.fold(fun state input -> state + input) 0 inputs

// inputs |> Seq.fold(fun state input -> state + input) 0

// (0,inputs) ||> Seq.fold(fun state input -> state + input)

(0,[1..5]) ||> Seq.fold(fun state input -> state + input)
        
// 18.6 Accumulating through a while loop
// open System.IO

// let mutable totalChars = 0

// let sr = new StreamReader(File.OpenRead("book.txt"))

// while (not sr.EndOfStream)
// do
//     let line = sr.ReadLine()
//     totalChars <- totalChars + line.ToCharArray().Length
    
// 18.7 Simulating a collection through sequence expressions    
open System.IO
let lines : string seq = seq {
        use sr = new StreamReader(File.OpenRead(@"book.txt"))
        while (not sr.EndOfStream)
            do yield sr.ReadLine()
        (0,lines) ||> Seq.fold(fun total line -> total + line.Length)
    }

//18.8  Creating a list of rules

type Rule = string -> bool * string

open System

let rules : Rule list = 
    [
        fun text -> (text.Split ' ').Length = 3,"Must be three words"
        fun text -> text.Length <= 30, "Max length is 30 characters"
        fun text -> 
            (text 
            |> Seq.filter Char.IsLetter
            |> Seq.forall Char.IsUpper),
            "All letter must be caps"
    ]

// 18.9 Manually binding a super rule

let validateManual (rules:Rule list) word = 
    let passed,error = rules.[0] word
    if not passed then false, error
    else
        let passed, error = rules.[1] word
        if not passed then false, error
        else
            let passed, error = rules.[2] word
            if not passed then false, error
            else true, ""

//18.10 Composing a list of rules by using reduce
let buildValidator (rules:Rule list) = 
    rules
    |> List.reduce(fun firstRule secondRule -> 
        fun word -> 
            let passed,error = firstRule word
            if passed then
                let passed,error = secondRule word
                if passed then true, "" else false, error
            else false,error
        )


let validate = buildValidator rules
let word = "HELLO FrOM F#"

validate word

//Now you try
let rules : Rule list = 
    [
        fun text -> 
            printfn "Running 3 word function"
            (text.Split ' ').Length = 3,"Must be three words"
        fun text ->
            printfn "Running 30 character function" 
            text.Length <= 30, "Max length is 30 characters"
        fun text -> 
            printfn "Running all caps function"
            (text 
            |> Seq.filter Char.IsLetter
            |> Seq.forall Char.IsUpper),
            "All letter must be caps"
    ]

//Try This
// Get functions from lesson 16

open System.IO

type FSRule = FileInfo -> (bool * string)

let fsrules a b c: FSRule list = 
    [
        fun file -> file.Length > a,"File is too large"
        fun file -> file.Extension = b, "File extension not correct"
        fun file -> DateTime.Compare(file.CreationTimeUtc.Date,c) = -1,"File created before specified date"
    ]

let fsbuildValidator (rules:FSRule list) = 
    rules
    |> List.reduce(fun firstRule secondRule -> 
        fun word -> 
            let passed,error = firstRule word
            if passed then
                let passed,error = secondRule word
                if passed then true, "" else false, error
            else false,error
        )

let rules = fsrules 10L ".fs" (DateTime.Today.AddDays(-1.0))

let fsvalidate = fsbuildValidator rules

let fileInfo = 
    getFilesInDirectory "."  
    |> List.collect (fun x -> 
            let name,files = x
            files
        )

fileInfo |> List.map(fsvalidate)

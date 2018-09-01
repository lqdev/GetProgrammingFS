(*
    Asynchronous Workflows
*)

// Listing 36.1 Scheduling work with async block F#

open System

printfn "Loading Data!"
System.Threading.Thread.Sleep(5000);
printfn "Loaded data!"
printfn "My name is Simon"

async {
    printfn "Loading Data!"
    System.Threading.Thread.Sleep(5000)
    printfn "Loaded data!"
} |> Async.Start
printfn "My name is Simon"

// Listing 36.2 Returning the result from an Async Block!

let asyncHello : Async<string> = async {return "Hello"}
let length = asyncHello.Length //Can't access value because it hasn't been unwrapped

let text = asyncHello |> Async.RunSynchronously
let lengthTwo = text.Length

// Listing 36.3 Larger async blocks in F#

open System.Threading

let printThread text = 
    printfn "THREAD %d: %s" Thread.CurrentThread.ManagedThreadId text

let doWork() = 
    printThread "Starting long running work!"
    Thread.Sleep 5000
    "HELLO"

let asyncLength : Async<int> = 
    printThread "Creating async block"
    let asyncBlock = 
        async {
            printThread "In block!"
            let text = doWork()
            return (text + "WORLD").Length
        }
    printThread "Created async block"
    asyncBlock

let length = asyncLength |> Async.RunSynchronously

// Listing 36.4 Creating a continuation by using let!
let getTextAsync = async {return "HELLO"}

let printHelloWorld = 
    async {
        let! (text:string) = getTextAsync
        return printf "%s WORLD" text
    }

printHelloWorld |> Async.Start

// Listing 36.5 Looking at fork/join with Async.Parallel
open System
let random = System.Random()
let pickANumberAsync = async {return random.Next(10)}

let createFiftyNumbers = 
    let workflows = [for i in 1..50 -> pickANumberAsync]
    async {
        let! numbers = workflows |> Async.Parallel
        printfn "Total is %d" (numbers |> Array.sum)
    }

createFiftyNumbers |> Async.Start

// Listin 36.6 Asynchronously downloading data over HTTP in parallel
open System.Threading
open System.Net

let downloadData url = async {
    use wc = new WebClient()
    printfn "Downloading data on thread %d" Thread.CurrentThread.ManagedThreadId
    let! data = wc.AsyncDownloadData(System.Uri url)
    return data.Length 
}

let downloadedBytes = 
    [|"http://fsharp.org";"http://microsoft.com";"http://fsharpforfunandprofit.com"|]
    |> Array.map downloadData
    |> Async.Parallel
    |> Async.RunSynchronously
    
printfn "You downloaded %d characters" (Array.sum downloadedBytes)
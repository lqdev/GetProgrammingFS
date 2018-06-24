// Learn more about F# at http://fsharp.org

open System
open Car

let getDestination() = 
    Console.WriteLine("Enter a destination")
    Console.ReadLine()

let getDistance(destination) = 
    if destination = "Gas" then 10
    elif destination = "Home" then 25
    elif destination = "Stadium" then 25
    elif destination = "Office" then 50
    else failwith "Unknown Destination"

let calculateRemaining(currentPetrol:int, distance: int) : int = 
    if currentPetrol >= distance then currentPetrol - distance
    else failwith "Oops! You've run out of petrol!"
let mutable petrol = 100

[<EntryPoint>]
let main argv =
    while true do
        try
            let destination = getDestination()
            printfn "Trying to drive to %s" destination
            petrol <- driveTo(petrol,destination)
            printfn "Made it to %s! You have %d petrol left" destination petrol
        with ex -> printfn "ERROR: %s" ex.Message
    0 // return an integer exit code

module Car

open System

//TODO: Create helper function to provide the building blocks to implement DriveTo
let getDistance(destination) = 
    if destination = "Gas" then 10
    elif destination = "Home" then 25
    elif destination = "Stadium" then 25
    elif destination = "Office" then 50
    else failwith "Unknown Destination"

let calculateRemaining(currentPetrol:int, distance: int) : int = 
    if currentPetrol >= distance then currentPetrol - distance
    else failwith "Oops! You've run out of petrol!"


//Drives to a given destination given a starting amount of pertrol
let driveTo(petrol, destination) = 
    let distance = getDistance(destination)
    let remainingPetrol = calculateRemaining(petrol,distance)
    if destination = "Gas" then remainingPetrol + 50
    else remainingPetrol
    
    
    

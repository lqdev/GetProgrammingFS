let getDistance(destination) = 
    if destination = "Gas" then 10
    elif destination = "Home" then 25
    elif destination = "Stadium" then 25
    elif destination = "Office" then 50
    else failwith "Unknown Destination"

getDistance("Apple")
getDistance("Home")
getDistance("Office")
getDistance("Stadium")
getDistance("Gas")

let calculateRemaining(currentPetrol:int, distance: int) : int = 
    if currentPetrol >= distance then currentPetrol - distance
    else failwith "Oops! You've run out of petrol!"

calculateRemaining(4,3)
calculateRemaining(3,4)    

let distanceToGas = getDistance("Gas")
calculateRemaining(25,distanceToGas)
calculateRemaining(5,distanceToGas)

let driveTo(petrol, destination) = 
    let distance = getDistance(destination)
    let remainingPetrol = calculateRemaining(petrol,distance)
    if destination = "Gas" then remainingPetrol + 50
    else remainingPetrol

let a = driveTo(100,"Office")
let b = driveTo(a, "Stadium")
let c = driveTo(b, "Gas")
let d = driveTo(c, "Home")

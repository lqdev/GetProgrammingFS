// Ceating immutable values in F# Pg 72

let name = "isaac"
name = "kate"

// Creating a mutable variable in F# Pg 73
// let mutable name = "isaac"
// name <- "kate"

// Managing state with mutable variables Pg 76
let mutable petrol = 100.0
let drive(distance) = 
    if distance = "far" then petrol <- petrol / 2.0
    elif distance = "medium" then petrol <- petrol - 10.0
    else petrol <- petrol - 1.0

drive("far")
drive("medium")
drive("short")

petrol

// Managing state with immutable values Pg 77
let drive(petrol,distance) = 
    if distance = "far" then petrol / 2.0
    elif distance = "medium" then petrol - 10.0
    else petrol - 1.0

let petrol = 100.0
let firstState = drive(petrol,"far")
let secondState = drive(firstState,"medium")
let thirdState = drive(secondState,"short")

//Exercise Pg 79
let drive(petrol,distance) = 
    if distance > 50.0 then petrol / 2.0
    elif distance > 25.0 then petrol - 10.0
    elif distance > 0.0 then petrol - 1.0
    else petrol

let petrol = 100.0
let firstState = drive(petrol,51.0)
let secondState = drive(firstState,26.0)

let thirdState = drive(secondState,1.0)

let fourthState = drive(thirdState,0.0)
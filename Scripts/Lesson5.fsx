// Explicit Type Annotations Pg 62

let add(a:int,b:int):int = 
    let answer:int = a + b
    answer

// Type Inference

// let add (a, b) = 
//     let answer = a + b
//     answer

// Inferred Type Arguments Pg 65
open System.Collections.Generic

//both lists use generics and type is only inferred after first use.
let numbers = List<_>()
numbers.Add(10)
numbers.Add(20)

let otherNumbers = List()
otherNumbers.Add(10)
otherNumbers.Add(20)

// Automatic Generalization of a Function Pg 66
let createList(first,second) =
    let output = List()
    output.Add(first)
    output.Add(second)
    output

//Complex Type Inference Example Pg 67
let sayHello(someValue) = 
    let innerFunction(number) = 
        if number > 10 then "Isaac"
        elif number > 20 then "Fred"
        else "Sara"
    
    let resultofInner = 
        if someValue < 10.0 then innerFunction(5)
        else innerFunction(15)
    
    "Hello " + resultofInner

// Breaking changes
// let sayHello(someValue) = 
//     let innerFunction(number) = 
//         if number > "Hello" then "Isaac"
//         elif number > 20 then "Fred"
//         else "Sara"
    
//     let resultofInner = 
//         if someValue < 10.0 then innerFunction(5)
//         else innerFunction(15)
    
//     "Hello " + resultofInner
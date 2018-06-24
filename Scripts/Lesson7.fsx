// Working with Expression in F# Pg 86

open System

let describeAge age = 
    let ageDescription = 
        if age < 18 then "Child!"
        elif age < 65 then "Adult!"
        else "OAP!"
    
    let greeting = "Hello"
    Console.WriteLine("{0}! You are a '{1}'",greeting,ageDescription)

// Exercise Page 87
let x = ()
let y = describeAge 5

x = y

// Discaring the result of an expression Pg 88
let writeTextToDisk text = 
    let path = System.IO.Path.GetTempFileName()
    System.IO.File.WriteAllText(path,text)
    path

// Implicit ignore
let createManyFiles() = 
    writeTextToDisk "The quick brown fox jumped over the lazy dog"
    writeTextToDisk "The quick brown fox jumped over the lazy dog"
    writeTextToDisk "The quick brown fox jumped over the lazy dog"

//Explicit Ignore
// let createManyFiles() = 
//     ignore (writeTextToDisk "The quick brown fox jumped over the lazy dog")
//     ignore (writeTextToDisk "The quick brown fox jumped over the lazy dog")
//     writeTextToDisk "The quick brown fox jumped over the lazy dog"


createManyFiles()


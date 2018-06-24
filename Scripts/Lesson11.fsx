// Calling curreid function in steps Page 127
let add first second = first + second
let addFive = add 5
let fifteen = addFive 10 //15

// Explicitly Creating wrapper functions F# 128

open System

let buildDt year month day = DateTime(year,month,day)
let buildDtThisYear month day = buildDt DateTime.UtcNow.Year month day
let buildDtThisMonth day = buildDtThisYear DateTime.UtcNow.Month day

// Creating wrapper functions by currying 128
let builtDtThisYear = buildDt DateTime.UtcNow.Year
let buildDtThisMonth = buildDtThisYear DateTime.UtcNow.Month

// Exercise 129
let writeToFile (date:DateTime) filename text =
    System.IO.File.WriteAllText(sprintf "%s-%s.txt" (System.DateTime.Today.Date.ToShortDateString()) filename,text)

//Creating your first curried function Pg 130
open System
open System.IO

let writeToFile (date:DateTime) filename text = 
    let path = sprintf "%s-%s.txt" (date.ToString "yyMMdd") filename
    File.WriteAllText(path,text)

// Creating constrianed functions Pg 130
let writeToToday = writeToFile DateTime.UtcNow.Date
let writeToTomorrow = writeToFile (DateTime.UtcNow.Date.AddDays 1.)
let writeToTodayHelloWorld = writeToToday "hello-world"

writeToToday "first-file" "The quick brown fox jumped over the lazy dog"
writeToTomorrow "second-file" "The quick brown fox jumped over the lazy the lazy dog"
writeToTodayHelloWorld "The quick brown fox jumped over the lazy dog"


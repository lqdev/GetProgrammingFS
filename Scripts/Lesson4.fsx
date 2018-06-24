// Pg 50
let first = "Hello"
let second = System.Random()
let third () = second.Next()

// Pg 52

open System

let doStuffWithTwoNumbers (first,second) = 
    let added = first + second
    Console.WriteLine("{0} + {1} = {2}",first,second,added);
    let doubled = added * 2
    doubled

//Pg 54
// Tightly Bound Scope
let estimatedAge = 
    let age = 
        let year = DateTime.Now.Year
        year - 1979
    sprintf "You are about %d years old" age
 
// Nested Funfctions
let estimatedAges (familyName, year1, year2, year3) = 
    let calculateAge yearOfBirth = 
        let year = System.DateTime.Now.Year
        year - yearOfBirth
    
    let estimatedAge1 = calculateAge year1
    let estimatedAge2 = calculateAge year2
    let estimatedAge3 = calculateAge year3

    let averageAge = (estimatedAge1 + estimatedAge2 + estimatedAge3) / 3
    
    sprintf "Average age for family %s is %d" familyName averageAge

estimatedAges("Quintanilla", 1997, 1961, 1964)

// Pg 56 will only work on windows
open System
open System.Net
open System.Windows.Forms

let webClient = new WebClient()
let fsharpOrg = webClient.DownloadString(Uri "http://fsharp.org")

let browser = new WebBrowser(ScriptErrorsSuppressed = true,
                            Dock = DockStyle.Fill,
                            DocumentText=fsharpOrg)

let form = new Form(Text="Hello from F#!")
form.Controls.Add browser
form.Show()

//Rewrite for scoping
let displayPage uri = 
    let form = new Form(Text="Hello from F#!")
    let fsharpOrg = 
        let webClient = new WebClient()
        webClient.DownloadString(Uri uri)
    let browser = new WebBrowser(ScriptErrorsSuppressed=true, Dock=DockStyle.Fill,DocumentText=fsharpOrg)
    form.Controls.Add browser
    form.Show()
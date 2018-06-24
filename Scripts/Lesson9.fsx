// Returning arbitrary data pairs in F# Pg 103

let parseName(name:string) = 
    let parts = name.Split(' ')
    let forename = parts.[0]
    let surname = parts.[1]
    forename,surname

let name = parseName("Isaac Abraham")
let forename,surname = name // Deconstructing a tuple ito meaningful values
let fname,sname = parseName("Isaac Abraham") // Deconstrcting tuple directly from function call

//Exercise Pg 104
let parse(person:string) = 
    let parts = person.Split(' ')
    let name = parts.[0]
    let game = parts.[1]
    let score = System.Convert.ToInt32(parts.[2])
    name,game,score

let name,game,score = parse("Mary Asteroids 2500")

// Returning More Complex Arbitrary Data Parirs in F# Pg 106
let nameAndAge = ("Joe","Bloggs"),28
let name,age = nameAndAge
let (forename,surname),theAge = nameAndAge

//Using Wildcards with Tuples PG 107
let nameAndAge = "Jane","Smith",25
let forename,surname,_ = nameAndAge // _ discards unwanted values in deconstruction

// Type Inference with Tuples Pg 107
let explicit : int * int = 10,5
let implicit = 10,5

let addNumbers arguments = 
    let a,b = arguments
    a + b

//Genericized Functions with Tuples Pg 107
let addNumbers arguments = 
    let a,b,c,_ = arguments
    a + b

// Implicity Mapping of out Parameters to Tuples Pg 109
// var number = "123";
// var result = 0;
// var parsed = Int32.TryParse(number,out result);
let number = "123"
let result,parsed = System.Int32.TryParse(number)
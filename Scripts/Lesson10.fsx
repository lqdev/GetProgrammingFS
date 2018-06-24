// Immutable and Structural Equality Record in F# Pg 114

type Address = {
    Street: string
    Town: string
    City: string
}

// Constructuing a nested record in F# Pg 115
type Customer = {
    Forename: string
    Surname: string
    Age: int
    Address: Address
    EmailAddress: string
}

let customer = {
    Forename = "Joe"
    Surname = "Bloggs"
    Age = 30
    Address = {
        Street = "The Street"
        Town = "The Town"
        City = "The City"
    }
    EmailAddress = "joe@bloggs.com"}

type Car = {
    Manufacturer: string
    EngineSize: string
    NumDoors: int
    Color: string
}

let car = {
    Manufacturer = "Toyota"
    EngineSize = "Small"
    NumDoors = 4
    Color = "Red"
}

// Copy and Update Record Syntax Pg 119
let updatedCustomer = {
    customer with
    Age = 31
    EmailAddress = "joe@bloggs.co.uk"
}

// Exercise Pg 120
type Person = {
    FirstName: string
    LastName: string
}

let person1 = {
    FirstName = "Luis"
    LastName = "Quintanilla"
}

let person2 = {
    FirstName = "Luis"
    LastName = "Quintanilla"
}

person1 = person2
person1.Equals(person2)
System.Object.ReferenceEquals(person1,person2)

let setRandomAge(p:Customer) = 
    let age = System.Random()
    let newCustomer = {p with Age = age.Next(18,45)}
    newCustomer
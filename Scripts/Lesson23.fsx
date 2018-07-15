// 23.1 Sample F# record representiong a sample customer
type Customer = {
    CustomerId: string
    Email: string
    Telephone: string
    Address: string
}

// 23.2 Creating a customer through a helper function
let createCustomer customerId email telephone address = 
    {
        CustomerId = telephone
        Email = customerId
        Telephone = address
        Address = email
    } 

let customer = createCustomer "C-123" "nicki@myemail.com" "029-239-23" "1 The Street"

// 23.3 Creating a wrapper type via a sincgle-case discriminated union
type Address = Address of string //Create single-case DU to store a streing Address

let myAddress = Address "1 The Street" //Create an instance of a wrapped address

let isTheSameAddress = (myAddress = "1 The Street") //Compare wrapped Addres and raw string 

let (Address addressData) = myAddress // Unwrap address into raw string as addressData





//Now you try
type CustomerId = CustomerId of string

type ContactDetails = 
| Address of string
| Telephone of string
| Email of string

type Customer = {
    CustomerId: CustomerId
    PrimaryContactDetails: ContactDetails
    SecondaryContactDetails: ContactDetails option
}

let createCustomer customerId contactDetails secondaryContactDetails = 
    {
        CustomerId=customerId
        PrimaryContactDetails=contactDetails
        SecondaryContactDetails=secondaryContactDetails
    }

let customer = createCustomer (CustomerId "Nicki") (Email "nicki@myemail.com") None    

//23.7 Creating a function to rate a customer
type GenuineCustomer = GenuineCustomer of Customer

let validateCustomer customer = 
    match customer.PrimaryContactDetails with
    | Email e when e.EndsWith "SuperCorp.com" -> Some (GenuineCustomer customer)
    | Address _ | Telephone _ -> Some (GenuineCustomer customer)
    | Email _ -> None

let sendWelcomeEmail (GenuineCustomer customer) = 
    printfn "Hello, %A, and welcome to our site!" customer.CustomerId

let customer2 = createCustomer (CustomerId "James") (Email "james@SuperCorp.com") None 

customer |> validateCustomer |> Option.map sendWelcomeEmail

// 23.8 Creating a result type to encode success or failure
type Result<'a> = 
| Success of 'a
| Failure of string 
let insertContact contactDetails =
  if contactDetails = (Email "nicki@myemail.com") then
    Success { CustomerId = CustomerId "ABC"
              PrimaryContactDetails = contactDetails
              SecondaryContactDetails = None }
  else Failure "Unable to insert  - customer already exists."

match insertContact (Email "nicki@email.com") with
| Success customerId -> printfn "Saved with %A" customerId
| Failure error -> printfn "Unable to save %s" error
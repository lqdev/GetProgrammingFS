#r "../packages/FSharp.Data/lib/net45/FSharp.Data.dll"
open FSharp.Data
#r "../packages/FSharp.Data.SqlClient/lib/net40/FSharp.Data.SqlClient.dll"

// Listing 32.1 Quering a database with the SqlCommandProvider type provider
open FSharp.Data
let [<Literal>] Conn = "Server=(localdb)\MSSQLLocalDB;Database=adventureworks-lt"

type GetCustomers = 
    SqlCommandProvider<"SELECT * FROM SalesLT.Customer",Conn>

let customers = GetCustomers.Create(Conn).Execute() |> Seq.toArray
    
let customer = customers.[0]

let printCompanyInfo fname lname companyName =
    match companyName with
    | Some cname -> printfn "%s %s works for %s" fname lname cname
    | _ -> printfn "%s %s does not work anywhere" fname lname

printCompanyInfo customer.FirstName customer.LastName customer.CompanyName

// Now you try
type AdventureWorks = SqlProgrammabilityProvider<Conn>

let ProductCategory = new AdventureWorks.SalesLT.Tables.ProductCategory() //Create instance of ProductCategory Table

ProductCategory.AddRow("Mittens",Some 3) //Insert Row
ProductCategory.AddRow("Long Shorts",Some 3) //Insert Row
ProductCategory.AddRow("Wooly Hats",Some 4) //Insert Row

ProductCategory.Update() // Updates. Could also use BulkInsert()

// 32.2 Generating client-side reference data from a SQL Table
type Categories = SqlEnumProvider<"SELECT Name,ProductCategoryId FROM SalesLT.ProductCategory",Conn> //Generate categories type for all product categories

let woolyHats = Categories.``Wooly Hats`` //Accesing Wooly Hats integer ID


// 32.3 Querying data by using SQLProvider library 
#r "../packages/SQLProvider/lib/net451/FSharp.Data.SqlProvider.dll"

open FSharp.Data.Sql

type AdventureWorks1 = SqlDataProvider<ConnectionString=Conn,UseOptionTypes=true> //Create Connection

let context = AdventureWorks1.GetDataContext() // Get a handle to a sessionized data context

let customers1 = 
    query {
        for customer in context.SalesLt.Customer do
        take 10
    } |> Seq.toArray // Query against Customer table

let customer1 = customers1.[0]

// 32.4 Projecting data within a more complex query

query {
    for customer in context.SalesLt.Customer do
    where (customer.CompanyName = Some "Sharp Bikes")
    select (customer.FirstName,customer.LastName)
    distinct
} |> Seq.toArray

// 32.5 Inserting new data

let category = context.SalesLt.ProductCategory.Create() // Create a new entity attached to the ProductCategory table

category.ParentProductCategoryId <- Some 3 // Mutating properties on the entity
category.Name <- "Scarf" // Mutating properties on the entity
context.SubmitUpdates() //Save new data

(* Navigating through reference data by using Individuals by Name
let mittens = 
    context.SalesLt.ProductCategory.Individuals.``As Name``.``1, Bikes``
*)


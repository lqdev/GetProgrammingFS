// Listing 31.1 Opening a remote JSON data source 

#r "../packages/FSharp.Data/lib/net45/FSharp.Data.dll"

open FSharp.Data

type TvListing = JsonProvider<"http://www.bbc.co.uk/programmes/genres/comedy/schedules/upcoming.json">

let tvListing = TvListing.GetSample()

let title = tvListing.Broadcasts.[0].Programme.DisplayTitles.Title

// Now You Try

type Films = HtmlProvider<"https://en.wikipedia.org/wiki/Robert_De_Niro_filmography">

let deNiro = Films.GetSample()

deNiro.Tables.Film.Rows
|> Array.countBy(fun row -> string row.Director)

// Now You Try 2
type Package = HtmlProvider<"https://www.nuget.org/packages/nunit">

let nunit = Package.Load "https://www.nuget.org/packages/nunit"
let entityFramework = Package.Load "https://www.nuget.org/packages/entityframework"

let newtonsoftJson = Package.Load "https://www.nuget.org/packages/Newtonsoft.Json/"

[nunit;entityFramework;newtonsoftJson]
|> Seq.collect(fun package -> package.Tables.``Version History``.Rows) 
|> Seq.sortByDescending(fun element -> element.Item3)
|> Seq.take 10
|> Seq.map(fun element -> (element.Item2,element.Item3))
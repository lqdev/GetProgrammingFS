// 16.1 map
let numbers = [1..10]
let timesTwo n = n * 2

let outputImperative = ResizeArray<int>()
for number in numbers do
    outputImperative.Add (number |> timesTwo)


let outputFunctional = numbers |> List.map timesTwo

// 16.2 collect

type Order = {OrderId: int}
type Customer = {CustomerId: int; Orders: Order list; Town:string}
let customers : Customer list = []
let orders: Order list = customers |> List.collect(fun c -> c.Orders)

//16.3 Using pairwise within the context of a larger pipeline
open System

[DateTime(2010,5,1);
DateTime(2010,6,1);
DateTime(2010,6,12);
DateTime(2010,7,3)]
|> List.pairwise
|> List.map(fun(a,b) -> b-a)
|> List.map(fun time -> time.TotalDays)

//16.4 Splittng a collection in two based on a predicate

let londonCustomers,otherCustomers = customers |> List.partition(fun c -> c.Town = "London")

//16.5  Simple aggregation functions in F#
let numbers = [1.0 .. 10.0]
let total = numbers |> List.sum
let average = numbers |> List.average
let max = numbers |> List.max
let min = numbers |> List.min

//16.6 Converting between lists,arrays, and sequences
let numberOne = 
[1..5] // Construct an int list
|> List.toArray // COnvert from anint list to an int array
|> Seq.ofArray // Convert from an int array to an int sequence
|> Seq.head

//Try this
type Folder = { 
    Name:string
    Size:int64
    NumFiles:int64
    AvgSize:float
    DistinctExt:string list
}

open System.IO

// Helper Functions
let getDirectories = Directory.GetDirectories >> Array.toList //Get list of Directories in specified directory
let getFiles = Directory.GetFiles >> Array.toList // Get list of files in specified directory

//Get list of FileInfo grouped by directory name
let getFilesInDirectory dir =
    getFiles dir
    |> List.map(fun x -> new FileInfo(x))
    |> List.groupBy(fun x -> x.DirectoryName)

//Extract name and directory size from 
let getInfo (x:(string * FileInfo list)) = 
    let name,filelist = x
    let dirSize = filelist |> List.sumBy(fun x -> x.Length)
    (name,dirSize)

//Get list of subdirectories and file sizes for specified directory
let files = 
    getDirectories "."
    |> List.collect getFilesInDirectory
    |> List.map getInfo
    |> List.sortByDescending(fun (_,x) -> x)

//Improvement (Returns Folder Record Type)
let getFolderInfo (x:(string * FileInfo list)) = 
    let name,filelist = x
    let dirSize = filelist |> List.sumBy(fun x -> x.Length)
    let fileCount = int64(filelist.Length)
    let distinctext = filelist |> List.map(fun x-> x.Extension) |> List.distinct
    let avgSize = float(dirSize / fileCount)
    {Name=name;Size=dirSize;NumFiles=fileCount;AvgSize=avgSize;DistinctExt=distinctext}


//Get subdirectory folder info 
let getSubDirectoryInfo root =
    getDirectories root
    |> List.collect getFilesInDirectory
    |> List.map getFolderInfo

//Sort subdirectory info in descending order
getSubDirectoryInfo "." |> List.sortByDescending(fun folder -> folder.Size)






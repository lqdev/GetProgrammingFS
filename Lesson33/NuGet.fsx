#r "../packages/FSharp.Data/lib/net45/FSharp.Data.dll"

open FSharp.Data

// 33.1 Creating your first type provider-backed API function
type Package = HtmlProvider<"https://www.nuget.org/packages/entityframework">
(*
let getDownloadsForPackage packageName = 
    let package = Package.Load(sprintf "https://www.nuget.org/packages/%s" packageName)
    package.Tables.``Version History``.Rows
    |> Seq.sumBy(fun p -> p.Downloads)


let getDetailsForVersion versionText packageName = 
    let package = Package.Load(sprintf "https://www.nuget.org/packages/%s" packageName)
    package.Tables.``Version History``.Rows
    |> Seq.tryFind(fun p -> p.Version.Contains versionText)
    
// 33.2 Try to gain code reuse across multiple functions
let getPackage packageName = 
    packageName |> sprintf "https://www.nuget.org/packages/%s" |> Package.Load

let getDetailsForVersion versionText packageName = 
    let package = getPackage packageName
    package.Tables.``Version History``.Rows |> Seq.tryFind(fun p -> p.Version.Contains versionText)    
*)
// 33.3 Further refactoring an API Implementation
let getPackage = 
    sprintf "https://www.nuget.org/packages/%s" >> Package.Load

let getVersionsForPackage (package:Package) = 
    package.Tables.``Version History``.Rows

(* 
let loadPackageVersions = getPackage >> getVersionsForPackage

let getDownloadsForPackage = 
    loadPackageVersions >> Seq.sumBy(fun p -> p.Downloads)
let getDetailsForVersion versionText = 
    loadPackageVersions >> Seq.tryFind(fun p -> p.Version.Contains versionText)
*)

//33.4 Creating a custom domain for NuGet package statistics

open System

// Classifier of package version
type PackageVersion =
    | CurrentVersion
    | Prerelease
    | Old

// Representation of a single package version
type VersionDetails =
    { Version : Version
      Downloads : decimal
      PackageVersion : PackageVersion
      LastUpdated : DateTime }
// Representation of an entire package
type NuGetPackage =
    { PackageName : string
      Versions : VersionDetails list }

// Now you try
//33.5 Creating a custom domain for NuGet package statistics
let parse (versionText:string) =
    let getVersionPart (version:string) isCurrent =
        match version.Split '-', isCurrent with
        | [| version; _ |], true
        | [| version |], true -> Version.Parse version, CurrentVersion
        | [| version; _ |], false -> Version.Parse version, Prerelease
        | [| version |], false -> Version.Parse version, Old
        | _ -> failwith "unknown version format"

    let parts = versionText.Split ' ' |> Seq.toList |> List.rev
    match parts with
    | [] -> failwith "Must be at least two elements to a version"
    | ")" :: "(current" :: version :: _ -> getVersionPart version true
    | version :: _ -> getVersionPart version false

let enrich (versionHistory:Package.VersionHistory.Row seq) = 
    { PackageName =
        match versionHistory |> Seq.map(fun row -> row.Version.Split ' ' |> Array.toList |> List.rev) |> Seq.head with
        | ")" :: "(current" :: _ :: name | _ :: name -> List.rev name |> String.concat " "
        | _ -> failwith "Unable to parse version name"
      Versions =
        versionHistory 
        |> Seq.map(fun versionHistory ->
            let version, packageVersion = parse versionHistory.Version
            { Version = version
              Downloads = versionHistory.Downloads
              LastUpdated = versionHistory.``Last updated``
              PackageVersion = packageVersion })
        |> Seq.toList }


//33.6 Updating your API with your latest domain model
let loadPackageVersions = getPackage >> getVersionsForPackage >> enrich >> (fun p -> p.Versions)
let getDetailsForVersion version = loadPackageVersions >> Seq.find(fun p -> p.Version = version)
let getDetailsForCurrentVersion = loadPackageVersions >> Seq.find(fun p -> p.PackageVersion = CurrentVersion)

getDetailsForCurrentVersion "entityframework" |> printfn "%A"

//let details = "Newtonsoft.Json" |> getDetailsForVersion (Version.Parse "9.0.1")





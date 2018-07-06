//Discriminated Unions (Way of modeling is-a relationships)

// CLosed type hierarchy - All different subtypes are deifned up front and
// new subtypes can't be declared later
// Like enums but with the ability to add metadata to each enum case
// DU represent a fixed type hierarchy. You can't create new subtypes except 
// where the DU is defined.

// 21.1 Composition with recorids in F#
type Disk = {SizeGb : int}
type Computer = {Manufacturer:string;Disks: Disk list}

//TODO: Look into units of measure.
let myPc = { 
    Manufacturer = "Computers Inc."
    Disks = [ 
        {SizeGb = 100}
        {SizeGb = 250}
        {SizeGb = 500}
    ]
}

// 21.2 Discriminated Unions in F#
type Disk = 
| HardDisk of RPM:int * Platters:int
| SolidState
| MMC of NumberOfPins:int

let myHardDisk = HardDisk(RPM = 250,Platters = 7)
let myHardDiskShort = HardDisk(250,7)

let args = 250,7
let myHardDiskTupled = HardDisk args
let myMMC = MMC 5

let mySsd = SolidState

// 21.4 Writing functions for a discriminated union
let seek disk = 
    match disk with
    | HardDisk _ -> "Seeking loudly at a reasonable speed!"
    | HardDisk(5400,5) -> "Seeking very slowly!"
    | HardDisk(rpm,7) -> "I have 7 splindles and RMP %d" rpm
    | MMC _ -> "Seeking quietly by slowly"
    | MMC 3 -> "Seeking. I have 3 pins!"
    | SolidState -> "Already found it!"

let describe disk = 
    match disk with
    | SolidState -> "I'm a newly fangled SSD"
    | MMC pins -> 
        match pins with
        | 1 -> "I'm an MMC with 1 pin"
        | pins when pins < 5 -> "I'm an MMC with a few pins"
        | _ -> sprintf "I'm an MMC with %d pins" pins 
    | HardDisk (5400,_) -> "I'm a slow Hard Disk"
    | HardDisk (_,7) -> "I have 7 splindles!"
    | HardDisk _ -> "I'm a Hard Disk"


describe myHardDisk

describe (MMC 1)

// 21.6 Nested Discriminated Unions
type MMCDisk = 
| RsMmc
| MmcPlus
| SecureMMC

type Disk = 
| MMC of MMCDisk * NumberOfPins:int

let disk = MMC(MmcPlus,3)

match disk with
| MMC(MmcPlus,3) -> "Seeking quietly but slowly"
| MMC(SecureMMC,6) -> "Seeking quietly with 6 pins"


// 21.7 Shared fields using a combination of records and discriminated unions.
type DiskInfo = {
    Manufacturer: string
    SizeGb: int
    DiskData: Disk //Discriminated Union
}

type Computer = {
    Manufacturer: string
    Disks: DiskInfo list
}

let myPc = {
    Manufacturer = "Computers Inc."
    Disks = [
        {
            Manufacturer = "HardDisks Inc."
            SizeGb = 100
            DiskData = HardDisk(5400,7)
        }
        {
            Manufacturer = "SuperDisks Corp."
            SizeGb = 250
            DiskData = SolidState
        }
    ]
}

//TODO: Check out Active Patterns 

// 21.8 Creating an enum in F#
type Printer =
| Inkjet = 0
| LaserJet = 1
| DotMatrix = 2
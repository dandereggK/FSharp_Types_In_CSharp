namespace Example

//1. Records

type Cat = 
  { Name : string 
    Age : string
    Lives : int }

(*1.1 A basic record type definition with names followed by type annotations.
Note that members are immutable by default and only contain getters. 
Note that constructer that takes all arguments is also included. 
Note that structural equality is also included. *)

[<Struct>]
type Date =
  { Year : int
    Month : uint8
    Day : uint8 }

(*1.2 Prepending the struct attribute converts the record type to a stack allocated type.
Note that all members must be a stack allocated type too.
Note that attributes in fsharp must have the <> brackets. *)

type Dog =
  { Name : string 
    mutable LastThingEaten : string }

(*1.3 Prepending mutable to a member adds a setter to that members property. *)

[<CLIMutable>]
type Bird = 
  { Name : string 
    Age : string }

(*1.4 Prepending the CLIMutable attribute alters the class to make all members mutable and includes a empty constructer. *)

// 2. Discriminated Unions

type SparrowSpecies =
  | AmericanTree
  | Chipping
  | ClayColored
  | Fox
  | Harris's
  | Nelson's
  | Savannah
  | Song
  | Swamp
  | Vesper
  | WhiteCrowned
  | WhiteThroated
  | House

(*2.1 A basic discrimated union defintion. 
A value of this type can be one and only one of the type members at a time.*)


namespace Example.Models

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

(*2.1 A basic discrimated union definition. 
The names seperated by the | are called cases. 
A value of this type can be one and only one of the cases at a time.*)

type Shape =
  | Point
  | Circle of float
  | Rentengle of length : float * width : float

(* 2.2 A complex discrimated union definition. 
The circle case has an unamed value representing radius
The point case has no values
The Rentengle case has a tuple ( * operator) of named members.
Note: that we would gernerally avoid defintions like the Retangle case 
and instead define a rectangle record type and put that inplace of the tuple.
Note: that float in fsharp is 64bit making it equvilant to a C# Double
*)
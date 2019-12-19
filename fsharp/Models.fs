namespace Example.Models
//place types into a namespace, don't use module yet.

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


//3. Tuples

type Rectangle = float * float

(*3.1 This is type abrivation useful for shortening lengthly type abrivations or putting a business name to a more generic type.
However, they only work with F# and don't show up in C#.
In this case Rectangle is equvilent to Tuple<float,float>.*)

type DrawBox<'t> =
  {
    Position : float * float * float
    Shape : 't
  }

(*3.2 First use of generics. All generic identifiers are prefixed with ' in F#.
The Position member of the record is a tuple with three members. *)

type DrawRectangle = DrawBox<Rectangle>

(*3.3 Another use of type abrivation.*)

//4. Option Type

(* The option type is an FSharp.Core type defined like so.
type Option<'t> =
| Some of 't
| None
This is used whenever the existantance of a value is in question depricating the entire concept of nulls.
*)

type PollResult =
    { Yes: int option
      No: option<int> }

(*4.1 Note types with only one generic can be rewritten postfix style. Both Yes and No types are equvilent.*)


//5. Collection Types: Array, Map, List, Set

type MegaType =
  { Arrays : int []
    Maps : Map<string,int>
    Sets : Set<int>
    Lists : int option list //list<option<int>>
    ArrayofArrays : int [] []
    Array2d : int [,]
  }
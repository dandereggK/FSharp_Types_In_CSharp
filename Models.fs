namespace Example

type Cat = 
  { Name : string 
    Age : string
    Lives : int }


[<Struct>]
type Date =
  { Year : int
    Month : uint8
    Day : uint8 }

type Dog =
  { Name : string 
    mutable LastThingEaten : string }


namespace WithFs
open System.Collections.Generic
type Customer(id:int, name:string)=
    member val id=id
    member val name= name
type CustomerWithMissingCtorArgument(id:int)=
    member val id=id
    member val name= "named"

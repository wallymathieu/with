// Learn more about F# at http://fsharp.net. See the 'F# Tutorial' project
// for more guidance on F# programming.
#I "../Tests/bin/Debug/"
#r "With.dll"

#load "With.fs"
open With

let r = [0;1;2;3]

stitch_and_end r -1

let r' = List.map Some r

stitch r'
stitch_and_end r' None


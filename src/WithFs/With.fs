module With

let stitch s= 
    let head = s |> Seq.head
    let tail = s |> Seq.skip 1
    tail |> Seq.scan (fun state elem-> (snd state, elem)) (head, head) 
         |> Seq.skip 1 
         |> Seq.toList

open System.Linq

let stitch_and_end (s: 'a seq) (last: 'a) = 
    let stitched = stitch s
    let e = Enumerable.Last s
    stitched @ [(e, last)]

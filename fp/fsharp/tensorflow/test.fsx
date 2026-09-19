#r "nuget:TensorFlow.NET, 0.100.4"
#r "nuget:SciSharp.TensorFlow.Redist, 2.10.0"

open Tensorflow

open Tensorflow.NumPy

let hello = Binding.tf.constant "Hello, TensorFlow.NET!"
printfn "%A" hello

let t1 = new Tensor 3
let t2 = new Tensor(new NDArray [| 1; 2; 3 |])

let t3 = array2D [ [ 4; 5; 6 ]; [ 7; 8; 9 ] ] |> Binding.tf.constant


printfn "%A" t1
printfn "%A" t2
printfn "%A" t3

#r "nuget:TensorFlow.NET, 0.100.4"
#r "nuget:SciSharp.TensorFlow.Redist, 2.10.0"

open Tensorflow
open Tensorflow.NumPy

let hello = Binding.tf.constant "Hello, TensorFlow.NET!"
printfn "%A" hello

let t1 = new Tensor 3
let t2 = new Tensor(new NDArray [| 1; 2; 3 |])

printfn "%A" t1
printfn "%A" t2

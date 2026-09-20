#r "nuget:TensorFlow.NET, 0.100.4"
#r "nuget:SciSharp.TensorFlow.Redist, 2.10.0"

open Tensorflow

open Tensorflow.NumPy

let hello = Binding.tf.constant "Hello, TensorFlow.NET!"
//printfn "%A" hello

let t1 = new Tensor 3
let t2 = new Tensor(new NDArray [| 1; 2; 3 |])

let tt3 = Binding.tf.constant [| 1; 2; 3 |]
let tt4 = Binding.tf.constant 3

let tt5 =
    Binding.tf.constant (array2D [ [ 1; 2; 3; 3 ]; [ 3; 4; 5; 6 ]; [ 9; 7; 8; 9 ] ])


let a2 = Binding.tf.multiply (tt5, 9)
Tensorflow.Binding.print a2

let tt6 = Binding.tf.constant (array2D [ [ 1; 2 ]; [ 3; 4 ] ])
let tt7 = Binding.tf.constant (array2D [ [ 5; 6 ]; [ 7; 8 ] ])

let a3 = Binding.tf.matmul (tt6, tt7)
Tensorflow.Binding.print ("a3:", a3)


let t3 = array2D [ [ 4; 5; 6 ]; [ 7; 8; 9 ] ] |> Binding.tf.constant

//let t4 = new Tensor(new NDArray [| [| 10; 11; 12 |]; [| 13; 14; 15 |] |])

//Tensorflow.Binding.print hello

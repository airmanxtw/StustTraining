#r "nuget:TensorFlow.NET, 0.100.4"
#r "nuget:SciSharp.TensorFlow.Redist, 2.10.0"

open Tensorflow

open Tensorflow.NumPy

let x = Binding.tf.Variable 5.0
let tape = Binding.tf.GradientTape()
let y = Binding.tf.square x + Binding.tf.constant 3.0

let y_grad = tape.gradient (y, x)
Binding.tf.print y
Binding.tf.print y_grad

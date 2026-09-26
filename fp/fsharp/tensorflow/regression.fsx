#r "nuget:TensorFlow.NET, 0.100.4"
#r "nuget:SciSharp.TensorFlow.Redist, 2.10.0"
#r "nuget:TensorFlow.Keras, 0.10.4"

open Tensorflow

open Tensorflow.NumPy

let train_X =
    np.array
        [| 3.3f
           4.4f
           5.5f
           6.71f
           6.93f
           4.168f
           9.779f
           6.182f
           7.59f
           2.167f
           7.042f
           10.791f
           5.313f
           7.997f
           5.654f
           9.27f
           3.1f |]

let train_Y =
    np.array
        [| 1.7f
           2.76f
           2.09f
           3.19f
           1.694f
           1.573f
           3.366f
           2.596f
           2.53f
           1.221f
           2.827f
           3.465f
           1.65f
           2.904f
           2.42f
           2.94f
           1.3f |]

let n_samples = train_X.shape.[0]

let W = Binding.tf.Variable(0.0f, name = "weight")
let b = Binding.tf.Variable(0.0f, name = "bias")

let learning_rate = 0.01f
let optimizer = KerasApi.keras.optimizers.SGD learning_rate

let training_steps = 4000
let display_step = 50

for step in 1..training_steps do

    use g = Binding.tf.GradientTape()
    // Linear regression model
    let pred = W * train_X + b

    // Mean squared error
    let loss = Binding.tf.reduce_mean (Binding.tf.square (pred - train_Y))

    let gradients = g.gradient (loss, Seq.cast<IVariableV1> [ W; b ])

    // Update weights
    optimizer.apply_gradients (
        Seq.zip gradients [| W; b |]
        |> Seq.map (fun (gradient, variable) -> struct (Binding.tf.cast (gradient, TF_DataType.TF_FLOAT), variable))
    )

    if step % display_step = 0 then
        Binding.print ("Step: ", step, " Loss: ", loss, " W: ", W, " b: ", b)
//printfn "Step: %d, Loss: %O, W: %O, b: %O" step loss W b

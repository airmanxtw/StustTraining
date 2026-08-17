#r "nuget:LanguageExt.Core,4.4.8"
#r "nuget:LanguageExt.Transformers,4.4.8"

using LanguageExt;

System.Console.WriteLine("Hello, LanguageExt!");

var add = (int x, int y) => Right(Some(x + y));

var result = add(3,4);

result.Match(
    Right: sum => System.Console.WriteLine($"The sum is: {sum}"),
    Left: error => System.Console.WriteLine($"Error: {error}")
);


#r "nuget: Deedle,8.1.0"
open Deedle
let data = Frame.ReadCsv "business-operations-survey-2022-business-finance.csv"
data.Print()

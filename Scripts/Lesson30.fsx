
// Listing 30.1 Working with CSV files using FSharp.Data
#r "../packages/FSharp.Data/lib/net45/FSharp.Data.dll"
open FSharp.Data;

type Football = CsvProvider<"../data/FootballResults.csv">
let data = Football.GetSample().Rows |> Seq.toArray

data.[0].``Away Cards``

//Listing 30.2 Charting the top ten teams for home wins
#r "../packages/XPlot.GoogleCharts/lib/net45/XPlot.GoogleCharts.dll"
#r "../packages/Google.DataTable.Net.Wrapper/lib/Google.DataTable.Net.Wrapper.dll"

open XPlot.GoogleCharts

data
|> Seq.filter(fun row -> row.``Full Time Home Goals`` > row.``Full Time Away Goals``)
|> Seq.countBy(fun row -> row.``Home Team``)
|> Seq.sortByDescending snd
|> Seq.take 10
|> Chart.Column
|> Chart.Show
//
// F# library to analyze Chicago crime data
//
// Prof. Joe Hummel
// U. of Illinois, Chicago
// CS341, Spring 2016
// Homework 6: Solution
//

module FSAnalysis

#light

open System
open FSharp.Charting
open FSharp.Charting.ChartTypes
open System.Drawing
open System.Windows.Forms

//
// Parse one line of CSV data:
//
//   Date,IUCR,Arrest,Domestic,Beat,District,Ward,Community,Year
//   09/03/2015 11:57:00 PM,0820,true,false,0835,008,18,66,2015
//   ...
//
// Returns back a tuple with most of the information:
//
//   (date, iucr, arrested, domestic, community, year)
//
// as string*string*bool*bool*int*int.
//
let private ParseOneCrime (line : string) = 
  let elements = line.Split(',')
  let date = elements.[0]
  let iucr = elements.[1]
  let arrested = Convert.ToBoolean(elements.[2])
  let domestic = Convert.ToBoolean(elements.[3])
  let community = Convert.ToInt32(elements.[elements.Length - 2])
  let year = Convert.ToInt32(elements.[elements.Length - 1])
  (date, iucr, arrested, domestic, community, year)


// 
// Parse file of crime data, where the format of each line 
// is discussed above; returns back a list of tuples of the
// form shown above.
//
// NOTE: the "|>" means pipe the data from one function to
// the next.  The code below is equivalent to letting a var
// hold the value and then using that var in the next line:
//
//  let LINES  = System.IO.File.ReadLines(filename)
//  let DATA   = Seq.skip 1 LINES
//  let CRIMES = Seq.map ParseOneCrime DATA
//  Seq.toList CRIMES
//
let private ParseCrimeData filename = 
  System.IO.File.ReadLines(filename)
  |> Seq.skip 1  // skip header row:
  |> Seq.map ParseOneCrime
  |> Seq.toList


//
// Given a list of crime tuples, returns a count of how many 
// crimes were reported for the given year:
//
let private CrimesThisYear crimes crimeyear = 
  let crimes2 = List.filter (fun (date, iucr, arrested, domestic, community, year) -> year = crimeyear) crimes
  let numCrimes = List.length crimes2
  numCrimes


//
// Given a list of crime tuples, returns a count of how many 
// arrests were made for the given year:
//
let private ArrestsThisYear crimes crimeyear = 
  let arrests = List.filter (fun (date, iucr, arrested, domestic, community, year) -> year = crimeyear && arrested) crimes
  let numArrests = List.length arrests
  numArrests


//
// Given a list of crime tuples, returns a count of how many of THIS
// particular crime were reported for the given year:
//
let private ParticularCrimeThisYear crimes crimeyear crimecode = 
  let crimes2 = List.filter (fun (date, iucr, arrested, domestic, community, year) -> year = crimeyear && iucr = crimecode) crimes
  let numCrimes = List.length crimes2
  numCrimes


//
// Given a list of crime tuples, returns a count of how many occurred in
// THIS AREA for the given year:
//
let private AreaThisYear crimes crimeyear crimearea = 
  let crimes2 = List.filter (fun (date, iucr, arrested, domestic, community, year) -> year = crimeyear && community = crimearea) crimes
  let numCrimes = List.length crimes2
  numCrimes


//
// Given a list of crime tuples, returns a count of how many 
// crimes were reported in THIS AREA across ALL the years:
//
let private CrimesThisArea crimes area = 
  let crimes2 = List.filter (fun (date, iucr, arrested, domestic, community, year) -> community = area) crimes
  let numCrimes = List.length crimes2
  numCrimes


//
//
//
let private ParseOneCrimeCode (line : string) = 
  let elements = line.Split(',')
  let iucr = elements.[0]
  let primary = elements.[1]
  let secondary = elements.[2]
  (iucr, primary, secondary)

let private LookupCrime codefile crimeCode = 
  let lines = [for line in System.IO.File.ReadLines(codefile) -> line]
  //
  // skip header row via lines.Tail:
  //
  let codes = List.map ParseOneCrimeCode lines.Tail
  let R = List.filter (fun (iucr, primary, secondary) -> iucr = crimeCode) codes
  if R = [] then
    "unknown crime code"
  else
    let (iucr, primary, secondary) = R.Head
    primary + ": " + secondary


//
//
//
let private ParseOneArea (line : string) = 
  let elements = line.Split(',')
  let area = Convert.ToInt32(elements.[0])
  let name = elements.[1]
  (area, name)


let private ParseAreaData areaFile = 
  let lines = [for line in System.IO.File.ReadLines(areaFile) -> line]
  //
  // skip header row via lines.Tail:
  //
  let areas = List.map ParseOneArea lines.Tail
  areas

let private LookupArea areaFile areaName = 
  let areas = ParseAreaData areaFile
  let R = List.filter (fun (area, name) -> name = areaName) areas
  if R = [] then
    0
  else
    let (area, areaName) = R.Head
    area


// ##################################################################
//
// Public functions called by C# GUI:
//
// ##################################################################

//
// (1) CrimesByYear:
//
// Given a CSV file of crime data, analyzes # of crimes by year, 
// returning a chart that can be displayed in a Windows desktop
// app:
//
let CrimesByYear(filename) = 
  //
  // debugging:  print filename, which appears in Visual Studio's Output window
  //
  printfn "Calling CrimesByYear: %A" filename
  //
  let crimes = ParseCrimeData filename
  //
  let (_, _, _, _, _, minYear) = List.minBy (fun (date, iucr, arrested, domestic, community, year) -> year) crimes
  let (_, _, _, _, _, maxYear) = List.maxBy (fun (date, iucr, arrested, domestic, community, year) -> year) crimes
  //
  let range  = [minYear .. maxYear]
  let counts = List.map (fun year -> CrimesThisYear crimes year) range
  let countsByYear = List.map2 (fun year count -> (year, count)) range counts
  //
  // debugging: see Visual Studio's Output window (may need to scroll up?)
  //
  printfn "Counts: %A" counts
  printfn "Counts by Year: %A" countsByYear
  //
  // plot:
  //
  let myChart = 
    Chart.Line(countsByYear, Name="Total # of Crimes")
  let myChart2 = 
    myChart.WithTitle(filename).WithLegend();
  let myChartControl = 
    new ChartControl(myChart2, Dock=DockStyle.Fill)
  //
  // return back the chart for display:
  //
  myChartControl


// 
// (2) CrimesVsArrests:
//
// Given a CSV file of crime data, analyzes # of crimes by year vs.
// arrests each year, and plots both.  Returns a chart for display.
//
let CrimesVsArrests(filename) = 
  //
  // debugging:  print filename, which appears in Visual Studio's Output window
  //
  printfn "Calling CrimesVsArrests: %A" filename
  //
  let crimes = ParseCrimeData filename
  //
  let (_, _, _, _, _, minYear) = List.minBy (fun (date, iucr, arrested, domestic, community, year) -> year) crimes
  let (_, _, _, _, _, maxYear) = List.maxBy (fun (date, iucr, arrested, domestic, community, year) -> year) crimes
  //
  // Total crimes each year:
  //
  let range  = [minYear .. maxYear]
  let counts = List.map (fun year -> CrimesThisYear crimes year) range
  let countsByYear = List.map2 (fun year count -> (year, count)) range counts
  //
  printfn "%A" counts
  printfn "%A" countsByYear
  // 
  // Now repeat with arrests each year:
  //
  let range = [minYear .. maxYear]
  let arrests = List.map (fun year -> ArrestsThisYear crimes year) range
  let arrestsByYear = List.map2 (fun year count -> (year, count)) range arrests
  //
  printfn "%A" arrests
  printfn "%A" arrestsByYear
  //
  // plot:
  //
  let myChart = 
    Chart.Combine([
                    Chart.Line(countsByYear, Name="Total # of Crimes")
                    Chart.Line(arrestsByYear, Name="# of Arrests")
                  ])
  let myChart2 = 
    myChart.WithTitle(filename).WithLegend();
  let myChartControl = 
    new ChartControl(myChart2, Dock=DockStyle.Fill)
  //
  // return back the chart for display:
  //
  myChartControl


//
// (3) GivenCrimeByYear
//
// Given a CSV file of crime data and an IUCR crime code, analyzes # of 
// crimes by year vs. the # of this particular crime by year, and plots
// both.  Returns a chart for display.
//
let GivenCrimeByYear(filename, crimeCode) = 
  //
  // debugging:  print filename, which appears in Visual Studio's Output window
  //
  printfn "Calling GivenCrimeByYear: %A" filename
  //
  let crimes = ParseCrimeData filename
  //
  let (_, _, _, _, _, minYear) = List.minBy (fun (date, iucr, arrested, domestic, community, year) -> year) crimes
  let (_, _, _, _, _, maxYear) = List.maxBy (fun (date, iucr, arrested, domestic, community, year) -> year) crimes
  //
  // Total crimes each year:
  //
  let range  = [minYear .. maxYear]
  let counts = List.map (fun year -> CrimesThisYear crimes year) range
  let countsByYear = List.map2 (fun year count -> (year, count)) range counts
  //
  printfn "%A" counts
  printfn "%A" countsByYear
  //
  // For given crime, total # each year:
  //
  let range  = [minYear .. maxYear]
  let countsThisCrime = List.map (fun year -> ParticularCrimeThisYear crimes year crimeCode) range
  let countsThisCrimeByYear = List.map2 (fun year count -> (year, count)) range countsThisCrime
  //
  printfn "%A" countsThisCrime
  printfn "%A" countsThisCrimeByYear
  //
  // Figure out the crime associated with this crime code:
  //
  let codefile = "IUCR-codes.csv"
  let description = LookupCrime codefile crimeCode
  //
  // plot:
  //
  let myChart = 
    Chart.Combine([
                    Chart.Line(countsByYear, Name="Total # of Crimes")
                    Chart.Line(countsThisCrimeByYear, Name=description)
                  ])
  let myChart2 = 
    myChart.WithTitle(filename).WithLegend();
  let myChartControl = 
    new ChartControl(myChart2, Dock=DockStyle.Fill)
  //
  // return back the chart for display:
  //
  myChartControl


//
// (4) CrimesByArea
//
// Given a CSV file of crime data and the name of a particular area of the
// city (e.g. "Loop"), analyzes # of crimes by year vs. the # of crimes in
// this particular area of the city by year, and plots both.  Returns a 
// chart for display.
//
let CrimesByArea(filename, areaName) = 
  //
  // debugging:  print filename, which appears in Visual Studio's Output window
  //
  printfn "Calling CrimesByArea: %A" filename
  //
  let crimes = ParseCrimeData filename
  //
  let (_, _, _, _, _, minYear) = List.minBy (fun (date, iucr, arrested, domestic, community, year) -> year) crimes
  let (_, _, _, _, _, maxYear) = List.maxBy (fun (date, iucr, arrested, domestic, community, year) -> year) crimes
  //
  // Total crimes each year:
  //
  let range  = [minYear .. maxYear]
  let counts = List.map (fun year -> CrimesThisYear crimes year) range
  let countsByYear = List.map2 (fun year count -> (year, count)) range counts
  //
  printfn "%A" counts
  printfn "%A" countsByYear
  //
  // we have city area by its name, we have to lookup name and get
  // it's numeric value (1..72 or so):
  //
  let areaFile = "Areas.csv"
  let areaCode = LookupArea areaFile areaName
  //
  printfn "area code: %A" areaCode
  //
  // For given crime, total # each year:
  //
  let range  = [minYear .. maxYear]
  let countsThisArea = List.map (fun year -> AreaThisYear crimes year areaCode) range
  let countsThisAreaByYear = List.map2 (fun year count -> (year, count)) range countsThisArea
  //
  printfn "%A" countsThisArea
  printfn "%A" countsThisAreaByYear
  //
  // plot:
  //
  let myChart = 
    Chart.Combine([
                    Chart.Line(countsByYear, Name="Total # of Crimes")
                    Chart.Line(countsThisAreaByYear, Name=areaName)
                  ])
  let myChart2 = 
    myChart.WithTitle(filename).WithLegend();
  let myChartControl = 
    new ChartControl(myChart2, Dock=DockStyle.Fill)
  //
  // return back the chart for display:
  //
  myChartControl


//
// (5) CrimesAcrossChicago
//
// Given a CSV file of crime data, computes the total # of crimes in each
// area of the city (there are 70+ areas).  Returns a chart for display.
//
let CrimesAcrossChicago(filename) = 
  //
  // debugging:  print filename, which appears in Visual Studio's Output window
  //
  printfn "Calling CrimesAcrossChicago: %A" filename
  //
  let crimes = ParseCrimeData filename
  let areas = ParseAreaData "Areas.csv"
  //
  let (minArea, _) = List.minBy (fun (area, name) -> area) areas
  let (maxArea, _) = List.maxBy (fun (area, name) -> area) areas
  //
  // Total crimes for each area
  //
  let range  = [minArea .. maxArea]
  let counts = List.map (fun area -> CrimesThisArea crimes area) range
  let countsByArea = List.map2 (fun area count -> (area, count)) range counts
  //
  printfn "%A" counts
  printfn "%A" countsByArea
  //
  // plot:
  //
  let myChart = 
    Chart.Line(countsByArea, Name="Total Crimes by Chicago Area")
  let myChart2 = 
    myChart.WithTitle(filename).WithLegend();
  let myChartControl = 
    new ChartControl(myChart2, Dock=DockStyle.Fill)
  //
  // return back the chart for display:
  //
  myChartControl


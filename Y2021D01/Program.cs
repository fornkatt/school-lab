// See https://aka.ms/new-console-template for more information

using AdventOfCode.Common;
using Y2021D01;

Console.WriteLine("Hello, World!");
var path = Path.Combine(AppContext.BaseDirectory, "input.txt");
var text = File.ReadAllText(path);

var depths = Input.Numbers(text);
var sonar = new Sonar();
var increases = sonar.CountIncreases(depths);

Console.WriteLine($"The file contained {depths.Length} entries, and they increased {increases} times.");
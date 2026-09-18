namespace Y2015D10;

class Program
{
    static void Main(string[] args)
    {
        var elvesNumbers = new ElvesNumbers();

        var result = elvesNumbers.ReocurringNumbers("1");

        Console.WriteLine(result);
    }
}
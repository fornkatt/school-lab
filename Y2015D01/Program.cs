namespace Y2015D01;

class Program
{
    static void Main(string[] args)
    {
        var path = Path.Combine(AppContext.BaseDirectory, "input.txt");
        var text = File.ReadAllText(path);
        
        var santaFloorGuide = new SantaFloorGuide();
        
        var santaFloorGuideResult = santaFloorGuide.CalculateFloor(text);
        var santaBasementIndex = santaFloorGuide.CalculateEndFloor(text);

        Console.WriteLine($"Floor: {santaFloorGuideResult}");
        Console.WriteLine($"Basement: {santaBasementIndex}");
    }
}
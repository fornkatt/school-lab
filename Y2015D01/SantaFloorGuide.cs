namespace Y2015D01;

public class SantaFloorGuide
{
    public int CalculateFloor(string input)
    {
        var rightParanthesisLength = input.Replace("(", "").Length;

        var leftParanthesisLength = input.Replace(")", "").Length;
        
        return leftParanthesisLength - rightParanthesisLength;

        // int floor = 0;
        //
        // for (int i = 0; i < input.Length; i++)
        // {
        //     if (input[i] == '(') floor++;
        //     else floor--;
        // }
        //
        // return floor;
    }

    public int CalculateEndFloor(string input)
    {
        var floor = 0;
        int i;
        
        for (i = 0; i < input.Length; i++)
        {
            if (input[i] == '(')
                floor++;
            else if (input[i] == ')') 
                floor--;

            if (floor == -1)
                break;
        }

        return i + 1;
    }
}
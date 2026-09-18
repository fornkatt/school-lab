namespace Y2015D10;

public class ElvesNumbers
{
    public string ReocurringNumbers(string input)
    {
        var sameNumberCount = 0;
        var output = "";
        var actual = "";

        for (int i = 1; i < input.Length; i++)
        {
            if (input[i] == input[i - 1])
            {
                sameNumberCount++;
                actual = input[i].ToString();
                output += sameNumberCount + actual;
            }
            else
            {
                sameNumberCount = 0;
                output += input[i - 1].ToString();
            }
        }
        
        return output;

        // var reocuuring = 0;
        // string actual;
        // string output = "";
        // for (int i = 0; i < input.Length; i++)
        // {
        //     if (i -1 == i)
        //     {
        //         reocuuring++;
        //         actual = input[i].ToString();
        //         output += reocuuring.ToString();
        //     }
        //     else
        //         reocuuring = 0;
        // }
        //
        // return output;
    }
}
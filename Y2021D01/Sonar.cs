namespace Y2021D01;

public class Sonar
{
    public int CountIncreases(int[] depths)
    {
        

        for (int i = 1; i < depths.Length; i++)
        {
            if (depths[i] > depths[i-1] )
            {
                increases++;
            }
        }

        return increases;
    }
}
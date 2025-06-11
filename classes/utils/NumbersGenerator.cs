namespace argumentParser.Classes.Utils;

class NumbersGenerator
{
    public static List<int> GenerateRandomNumbers(int count, int min = 0, int max = 99)
    {
        if (count <= 0)
        {
            throw new ArgumentException("Count must be greater than zero.", nameof(count));
        }

        if (min >= max)
        {
            throw new ArgumentException("Min must be less than Max.", nameof(min));
        }

        Random random = new Random();
        List<int> numbers = new List<int>();

        for (int i = 0; i < count; i++)
        {
            numbers.Add(random.Next(min, max));
        }

        return numbers;
    }
}
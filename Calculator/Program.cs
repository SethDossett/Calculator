namespace Calculator;

internal class Program
{
    public static Device device = new Device();
    public static bool EndApp = false;

    public static void Main(string[] args)
    {
        EndApp = false;

        while (!EndApp)
        {
            device.RunCalculator();
        }


    }

    public static double ValidateAnswer(string result)
    {
        result = result.Trim();
        while (string.IsNullOrEmpty(result) || !double.TryParse(result, out _))
        {
            Console.WriteLine("Please enter an integer, Try Again");

            result = Console.ReadLine();
        }

        return double.Parse(result.Trim());
    }

}

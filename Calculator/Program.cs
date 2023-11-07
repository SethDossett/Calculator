using CalculatorLibrary;
namespace CalculatorProgram;

internal class Program
{
    public static Device device = new Device();
    public static bool EndApp = false;

    public static void Main(string[] args)
    {
        EndApp = false;
        //Start App
        while (!EndApp)
        {
            device.RunCalculator();
            device.Finish();
            EndApp = true;
        }
    }

}

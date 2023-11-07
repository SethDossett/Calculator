using Newtonsoft.Json;
namespace CalculatorLibrary;

public class Device
{
    JsonWriter writer;

    //public Device()
    //{
    //    StreamWriter logFile = File.CreateText("calculator.log");
    //    Trace.Listeners.Add(new TextWriterTraceListener(logFile));
    //    Trace.AutoFlush = true;
    //    Trace.WriteLine("Starting Calculator Log");
    //    Trace.WriteLine(String.Format("Started {0}", System.DateTime.Now.ToString()));
    //}
    public Device()
    {
        StreamWriter logFile = File.CreateText("calculator.log");
        logFile.AutoFlush = true;
        writer = new JsonTextWriter(logFile);
        writer.Formatting = Formatting.Indented;
        writer.WriteStartObject();
        writer.WritePropertyName("Operations");
        writer.WriteStartArray();
    }

    public void Finish()
    {
        writer.WriteEndArray();
        writer.WriteEndObject();
        writer.Close();
    }

    public void RunCalculator()
    {
        //Welcoming
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");
        //Get Info
        double result = 0;
        var num1 = GetFirstNumber();
        var num2 = GetSecondNumber();
        string op = GetOperator();
        //Calculate Info
        try
        {
            result = Calculate(num1, num2, op);
            if (double.IsNaN(result))
            {
                Console.WriteLine("This operation will result in a mathematical error.\n");
            }
            else Console.WriteLine("Your result: {0:0.##}\n", result);

        }
        catch (Exception e)
        {
            Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
        }

        //End Application
        Console.WriteLine("------------------------\n");
        Console.WriteLine("Press any key to close the app");
        Console.ReadKey(false);
    }
    private double GetFirstNumber()
    {
        Console.WriteLine("Type a number, and then press Enter");

        var num = ValidateAnswer(Console.ReadLine());

        return num;
    }
    private double GetSecondNumber()
    {
        Console.WriteLine("Type another number, and then press Enter");

        var num = ValidateAnswer(Console.ReadLine());

        return num;
    }
    private string GetOperator()
    {
        Console.Write("Choose an option from the following list: \n" +
            "\ta - add \n" +
            "\ts - subtract \n" +
            "\tm - multiply \n" +
            "\td - divide \n");
        string op = "";
        bool loopAgain = true;
        op = Console.ReadLine().ToLower().Trim();
        do
        {
            if (op == "a" || op == "s" || op == "m" || op == "d")
            {
                loopAgain = false;
                break;
            }
            else
            {
                loopAgain = true;
            }
            Console.WriteLine("Invalid Entry");
            op = Console.ReadLine().ToLower().Trim();

        } while (string.IsNullOrEmpty(op) || loopAgain);

        Console.WriteLine("Your option: " + op);

        return op;
    }
    private double Calculate(double num1, double num2, string op)
    {
        string sentence = "";
        double answer = double.NaN;
        string symbol = "";

        writer.WriteStartObject();
        writer.WritePropertyName("Operand1");
        writer.WriteValue(num1);
        writer.WritePropertyName("Operand2");
        writer.WriteValue(num2);
        writer.WritePropertyName("Operation");
        switch (op)
        {
            case "a":
                answer = num1 + num2;
                symbol = "+";
                //Trace.WriteLine(String.Format("{0} + {1} = {2}", num1, num2, answer));
                writer.WriteValue("Add");
                break;

            case "s":
                answer = num1 - num2;
                symbol = "-";
                //Trace.WriteLine(String.Format("{0} - {1} = {2}", num1, num2, answer));
                writer.WriteValue("Subtract");
                break;

            case "m":
                answer = num1 * num2;
                symbol = "*";
                //Trace.WriteLine(String.Format("{0} * {1} = {2}", num1, num2, answer));
                writer.WriteValue("Multiply");
                break;

            case "d":
                answer = num1 / num2;
                symbol = "/";
                while (num2 == 0)
                {
                    Console.WriteLine("Enter a non-zero divisor: ");
                    num2 = Convert.ToDouble(ValidateAnswer(Console.ReadLine()));
                    answer = num1 / num2;
                }
                //Trace.WriteLine(String.Format("{0} / {1} = {2}", num1, num2, answer));
                writer.WriteValue("Divide");
                break;
            default:
                answer = num1 + num2;
                symbol = "+";
                //Trace.WriteLine(String.Format("{0} + {1} = {2}", num1, num2, answer));
                writer.WriteValue("Add");
                break;
        }
        writer.WritePropertyName("Result");
        writer.WriteValue(answer);
        writer.WriteEndObject();

        //sentence = sentence = $"Your Result: {num1} {symbol} {num2} = {answer}";
        //Console.WriteLine(sentence);
        return answer;
    }
    public double ValidateAnswer(string result)
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

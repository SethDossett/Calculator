namespace Calculator;

public class Device
{
    public Device() { }

    public void RunCalculator()
    {
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        double result = 0;
        var num1 = GetFirstNumber();
        var num2 = GetSecondNumber();
        string op = GetOperator();

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


        Console.WriteLine("------------------------\n");
        Console.WriteLine("Press any key to close the app");
        Console.ReadKey(false);
        Program.EndApp = true;
    }
    private double GetFirstNumber()
    {
        Console.WriteLine("Type a number, and then press Enter");

        var num = Program.ValidateAnswer(Console.ReadLine());

        return num;
    }
    private double GetSecondNumber()
    {
        Console.WriteLine("Type another number, and then press Enter");

        var num = Program.ValidateAnswer(Console.ReadLine());

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
        switch (op)
        {
            case "a":
                answer = num1 + num2;
                symbol = "+";
                break;

            case "s":
                answer = num1 - num2;
                symbol = "-";
                break;

            case "m":
                answer = num1 * num2;
                symbol = "*";
                break;

            case "d":
                answer = num1 / num2;
                symbol = "/";
                while (num2 == 0)
                {
                    Console.WriteLine("Enter a non-zero divisor: ");
                    num2 = Convert.ToDouble(Program.ValidateAnswer(Console.ReadLine()));
                    answer = num1 / num2;
                }
                break;
            default:
                answer = num1 + num2;
                symbol = "+";
                break;
        }

        //sentence = sentence = $"Your Result: {num1} {symbol} {num2} = {answer}";
        //Console.WriteLine(sentence);
        return answer;
    }
}


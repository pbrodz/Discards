using System;

/// <summary>
/// C# 7.0 feature
/// </summary>
namespace Discards
{
    class Program
    {
        // Lets create some method body methods
        // Remember these can only have a single expression - useful for ctors and properties
        #region MethodBody methods
        private static int DoSomething() => 24;
        private static void DoSomethingElse(string data) => Console.WriteLine($"printing: {data}");
        private static void DoSomethingAndGiveValue(out int value) => value = 24;
        #endregion

        static void Main(string[] args)
        {
            // The simplest discard
            _ = 1 + 2;

            // Here is another simple example - create an 8-tuple
            var tuple = (1, 2, 3, 4, 5, 6, 7, 8);
            // Then initialize another tuple by instantiating just the positions we care about - 2 and 4
            (_, var first, _, var second, _, _, _, _) = tuple;
            // And output them
            Console.WriteLine($"first: {first} second {second}");

            // The real power is using discards with out parameters as follows:
            Prs("02/29/2019");

            // Let's use explicit parameters just as a baseline
            Console.Write("Please enter the totalHours: ");
            double totalHours = Convert.ToDouble(Console.ReadLine());
            Console.Write("Please enter the availableHours: ");
            double availableHours = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine(SLA(totalHours, availableHours));

            // Let's use out parameters as they don't have to be declared *or* initialized
            Console.Write("Please enter the totalHours: ");
            totalHours = Convert.ToDouble(Console.ReadLine());
            Console.Write("Please enter the availableHours: ");
            availableHours = Convert.ToDouble(Console.ReadLine());
            // This is an inline declaration
            // *WARNING* Async and Iterator methods don't have an out parameter
            Console.WriteLine(SLA(totalHours, availableHours, out double result, out string sla));
        }
        public static void Prs(string dtstr)
        {
            // Here we can use a discard because we don't care about the result - we just want to know if it's valid or not
            if (DateTime.TryParse(dtstr, out _))
            {
                Console.WriteLine("Date is valid");
            }
            else
            {
                Console.WriteLine("Date is not valid");
            }
        }
        public static string SLA (double a, double b)
        {
            double result = Math.Round(((b / a) * 100), 2);
            string sla = $"Total hours: {a}, Available hours: {b} => SLA:{result} %";
            return sla;
        }
        public static string SLA(double a, double b, out double result, out string sla)
        {
            result = Math.Round(((b / a) * 100), 2);
            sla = $"Total hours: {a}, Available hours: {b} => SLA:{result} %";
            return sla;
        }
    }
}
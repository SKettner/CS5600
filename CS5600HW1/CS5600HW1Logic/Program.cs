
public class CS5600HW1Logic
{
    public static void Main(string[] args)
    {
        Func<double, double> polynomialFunction1 = x => -12 - 21 * x + 18 * Math.Pow(x, 2) - 2.75 * Math.Pow(x, 3);

        Console.WriteLine(Bisection(-2, 1, polynomialFunction1));
    }

    public static double Bisection(double xl, double xu, Func<double, double> polynomialFunction)
    {
        double xr = (xl - xu) / 2;
        double xrOld = 0;

        var test = Math.Abs((xr - xrOld) / xr);

        for (int i = 0; i < 20 && test > 0.1; i++)
        {
            Console.WriteLine("Number of iterations: " + i);
            double fl = polynomialFunction(xl);
            double fu = polynomialFunction(xu);
            double fr = polynomialFunction(xr);

            if (fr == 0)
            {
                return xr;
            }
            else if (fl * fr < 0)
            {
                xu = xr;
            }
            else
            {
                xl = xr;
            }

            xrOld = xr;
            xr = (xl - xu) / 2;

            test = Math.Abs((xr - xrOld) / xr);
        }

        return xr;
    }

}


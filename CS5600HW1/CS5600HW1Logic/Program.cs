
public class CS5600HW1Logic
{
    public static void Main(string[] args)
    {
        Func<double, double> polynomialFunction1 = x => -12 - 21 * x + 18 * Math.Pow(x, 2) - 2.75 * Math.Pow(x, 3);

        Console.WriteLine(FalsePostion(-2, 1, polynomialFunction1));
    }

    public static double Bisection(double xl, double xu, Func<double, double> polynomialFunction)
    {
        double xr = xl + Math.Abs(((xl - xu) / 2));
        double xrOld = 0;

        for (int i = 0; i < 20 && Math.Abs((xr - xrOld) / xr) > 0.1; i++)
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
            xr = xl + Math.Abs(((xl - xu) / 2));
        }

        return xr;
    }

    public static double FalsePostion(double xl, double xu, Func<double, double> polynomialFunction)
    {
        double xr = 0;
        double xrOld = 0;
        int i = 0;

        do
        {
            Console.WriteLine("Number of iterations: " + i);
            Console.WriteLine("Current xr: " + xr);

            double fl = polynomialFunction(xl);
            double fu = polynomialFunction(xu);
            

            xrOld = xr;
            xr = (fl* xu- fu* xl)/(fl- fu);

            double fr = polynomialFunction(xr);

            if (fr == 0)
            {
                return xr;
            }
            else if (fr < 0)
            {
                xl = xr;
            }
            else
            {
                xu = xr;
            }

            i++;

        } while (i < 20 && Math.Abs((xr - xrOld) / xr) > 0.1);

        return xr;
    }

    public static double NewtonRaphson(double x, Func<double, double> polynomialFunction)
    {
        double xr = 0;
        double xrOld = 0;
        int i = 0;

        do
        {
            Console.WriteLine("Number of iterations: " + i);
            Console.WriteLine("Current xr: " + xr);

            double fl = polynomialFunction(xl);
            double fu = polynomialFunction(xu);


            xrOld = xr;
            xr = (fl * xu - fu * xl) / (fl - fu);

            double fr = polynomialFunction(xr);

            if (fr == 0)
            {
                return xr;
            }
            else if (fr < 0)
            {
                xl = xr;
            }
            else
            {
                xu = xr;
            }

            i++;

        } while (i < 20 && Math.Abs((xr - xrOld) / xr) > 0.1);

        return xr;
    }

}


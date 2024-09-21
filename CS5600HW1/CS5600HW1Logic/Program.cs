
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

//Much of this code was generated with the help of ChatGPT full conversation here: https://docs.google.com/document/d/1UrLh0OxeJ3FkEKg26lzU8MEIWFEr2leX91e96LXiORs/edit?usp=sharing

public class CS5600HW1Logic
{
    public static void Main(string[] args)
    {
        //Func<double, double> polynomialFunction1 = x => -12 - 21 * x + 18 * Math.Pow(x, 2) - 2.75 * Math.Pow(x, 3);

        //Console.WriteLine(Bisection(-1, 0, polynomialFunction1));

        //Console.WriteLine(FalsePostion(-1, 0, polynomialFunction1));

        /*
        Func<double, double> polynomialFunction2 = x => Math.Pow(x, 3) - 6 * Math.Pow(x, 2) + 11 * x - 6.1;

        List<Tuple<double, double>> rootBrackets = FindAllRootBrackets(-1000, 1000, .1, polynomialFunction2);

        rootBrackets = RefineRootBrackets(rootBrackets, .01, polynomialFunction2);

        for(int i = 0; i < rootBrackets.Count; i++)
        {
            Console.WriteLine("Root " + i + " : " + Bisection(rootBrackets[i].Item1, rootBrackets[i].Item2, polynomialFunction2));
        }

        Func<double, double> polynomialFunction3 = x => .0074 * Math.Pow(x, 4) - .284 * Math.Pow(x, 3) + 3.355 * Math.Pow(x, 2) - 12.183 * x + 5;

        Func<double, double> polynomialFunction3Prime = x => 4 * .0074 * Math.Pow(x, 3) - 3 * .284 * Math.Pow(x, 2) + 2 * 3.355 * x - 12.183;

        Console.WriteLine(NewtonRaphson(17, polynomialFunction3, polynomialFunction3Prime));
        */
        /*
        Func<double, double> polynomialFunction4 = x => 4 * x - 1.8 * Math.Pow(x, 2) + 1.2 * Math.Pow(x, 3) - 0.3 * Math.Pow(x, 4);

        Func<double, double> polynomialFunction4Prime = x => 4 - 2 * 1.8 * x + 3 * 1.2 * Math.Pow(x, 2) - 4 * 0.3 * Math.Pow(x, 3);

        Console.WriteLine(Bisection(1, 3, polynomialFunction4Prime));

        Console.WriteLine(GoldenSectionSearchMax(-2, 4, polynomialFunction4));

        Console.WriteLine(parabolicInterpolation(1.75, 2, 2.5, polynomialFunction4));*/

        Tuple<double, double> output = CS5600HW1Prob6(.6, 10, .4);

        Console.WriteLine("x1: " + output.Item1 + "         Degrees: " + output.Item2);


    }

    public static List<Tuple<double, double>> RefineRootBrackets(List<Tuple<double, double>> initialBrackets, double refinedChangeBy, Func<double, double> polynomialFunction)
    {
        List<Tuple<double, double>> refinedBrackets = new List<Tuple<double, double>>();

        foreach (var bracket in initialBrackets)
        {
            double start = bracket.Item1;
            double end = bracket.Item2;

            // Run FindAllRootBrackets again with the refined step size
            var refined = FindAllRootBrackets(start, end, refinedChangeBy, polynomialFunction);

            // Add the refined results to the final list
            refinedBrackets.AddRange(refined);
        }

        return refinedBrackets;
    }

    public static List<Tuple<double, double>> FindAllRootBrackets(double start, double end, double changeby, Func<double, double> polynomialFunction)
    {
        double lastEntry = polynomialFunction(start);

        List<Tuple<double, double>> returnList = new List<Tuple<double, double>>();

        for (double i = start + changeby; i <= end; i = i + changeby)
        {
            double currnetEntry = polynomialFunction(i);

            if (HasSignChange(currnetEntry, lastEntry))
            {
                returnList.Add(Tuple.Create(i - changeby, i));
            }

            lastEntry = currnetEntry;
        }

        return returnList;
    }

    public static bool HasSignChange(double a, double b)
    {
        return a * b < 0;
    }

    public static double Bisection(double xl, double xu, Func<double, double> polynomialFunction)
    {
        double xr = xl + Math.Abs(((xl - xu) / 2));
        double xrOld = 0;

        for (int i = 0; i < 20 && Math.Abs((xr - xrOld) / xr) > 0.0001; i++)
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

    public static double NewtonRaphson(double x, Func<double, double> polynomialFunction, Func<double, double> polynomialFunctionPrime)
    {
        double xOld = 0;
        int i = 0;

        do
        {
            Console.WriteLine("Number of iterations: " + i);
            Console.WriteLine("Current x: " + x);

            double fx = polynomialFunction(x);
            double fxPrime = polynomialFunctionPrime(x);


            xOld = x;

            x = x - fx / fxPrime;

            i++;

        } while (i < 20 && Math.Abs((x - xOld) / x) > 0.0000001);

        return x;
    }


    public static double GoldenSectionSearchMax(double xl, double xu, Func<double, double> polynomialFunction)
    {
        double x = 0;
        double xOld = 0;
        int i = 0;

        double d = .6180 * (xu - xl);

        double x1 = xl + d;
        double x2 = xu - d;

        double f1 = polynomialFunction(x1);
        double f2 = polynomialFunction(x2);

        do
        {
            Console.WriteLine("Number of iterations: " + i);
            Console.WriteLine("Current x: " + x);

            xOld = x;
            x = (xl + xu) / 2; // Midpoint for convergence check

            if (f1 < f2)
            {
                xu = x1;
                x1 = x2;
                f1 = f2;

                d = .6180 * (xu - xl);

                x2 = xu - d;
                f2 = polynomialFunction(x2);
            }
            else
            {
                xl = x2;
                x2 = x1;
                f2 = f1;

                d = .6180 * (xu - xl);

                x1 = xl + d;
                f1 = polynomialFunction(x1);
            }

            i++;

        } while (i < 20 && Math.Abs((x - xOld) / x) > 0.01);

        return f1 > f2 ? x1 : x2;
    }

    public static double parabolicInterpolation(double x1, double x2, double x3, Func<double, double> polynomialFunction)
    {
        double xOld = 0;
        double x = 0;
        int i = 0;

        do
        {
            Console.WriteLine("Number of iterations: " + i);
            Console.WriteLine("Current x: " + x);

            // Calculate function values
            double f1 = polynomialFunction(x1);
            double f2 = polynomialFunction(x2);
            double f3 = polynomialFunction(x3);

            // Calculate the numerator
            double numerator = (Math.Pow(x2 - x1, 2) * (f2 - f3)) - (Math.Pow(x2 - x3, 2) * (f2 - f1));

            // Calculate the denominator
            double denominator = 2 * ((x2 - x1) * (f2 - f3) - (x2 - x3) * (f2 - f1));

            // Calculate x4
            double x4 = x2 - (numerator / denominator);
            double f4 = polynomialFunction(x4);

            xOld = x;

            if (x2 < x4 && x4 < x3)
            {
                if (f4 > f2)
                {
                    x1 = x2;
                    x2 = x4;
                    x = x2;
                }
                else
                {
                    x3 = x4;
                    x = x4;
                }
            }
            else
            {
                if (f4 > f2)
                {
                    x3 = x2;
                    x2 = x4;
                    x = x2;
                }
                else
                {
                    x1 = x4;
                    x = x4;
                }
            }

            i++;

        } while (i < 5); //&& Math.Abs((x - xOld) / x) > 0.001);

        return x;
    }

    public static Tuple<double, double> CS5600HW1Prob6(double h1, double h2, double L)
    {

        double initialVelocity = 15.0;  // Initial velocity in m/s
        double g = 9.8;   // Gravitational acceleration in m/s²

        double minT = double.MaxValue;
        double minValue = double.MaxValue;
        double bestTheta = 0;


        for (double thetaDegrees = 0; thetaDegrees <= 90; thetaDegrees++)
        {
            // Convert degrees to radians
            double theta = Math.PI * thetaDegrees / 180.0;

            // Quadratic coefficients for a*t^2 + b*t + c = 0
            double a = -0.5 * g;  // Coefficient of t^2
            double b = initialVelocity * Math.Sin(theta);  // Coefficient of t
            double c = -(h2 - h1 + L * Math.Sin(theta));  // Constant term

            // Use the quadratic formula to find the two values for t
            var result = QuadraticFormula(a, b, c);

            if (result != null)
            {
                double t1 = result.Item1;
                double t2 = result.Item2;

                double currentT = 0;

                if (t1 > 0 && t2 > 0)
                {
                    currentT = Math.Min(t1, t2);
                }
                else if (t1 > 0)
                {
                    currentT = t1;
                }
                else if (t2 > 0)
                {
                    currentT = t2;
                }
                else
                {
                    // No positive time, skip this iteration
                    continue;
                }


                Func<double, double> polynomialFunctionT = t =>
            (t * (initialVelocity * Math.Sin(theta))) - (0.5 * g * Math.Pow(t, 2)) - (h2 - h1 + L * Math.Sin(theta));

                double currentValue = polynomialFunctionT(currentT);

                if (currentValue < minValue && currentT > 0 && currentValue > 0)
                {
                    minT = currentT;
                    bestTheta = thetaDegrees;
                    minValue = currentValue;
                }
            }
        }

        double lastTheta = bestTheta;

        for (double thetaDegrees = lastTheta - 1; thetaDegrees <= lastTheta + 1; thetaDegrees = thetaDegrees + .001)
        {
            // Convert degrees to radians
            double theta = Math.PI * thetaDegrees / 180.0;

            // Quadratic coefficients for a*t^2 + b*t + c = 0
            double a = -0.5 * g;  // Coefficient of t^2
            double b = initialVelocity * Math.Sin(theta);  // Coefficient of t
            double c = -(h2 - h1 + L * Math.Sin(theta));  // Constant term

            // Use the quadratic formula to find the two values for t
            var result = QuadraticFormula(a, b, c);

            if (result != null)
            {
                double t1 = result.Item1;
                double t2 = result.Item2;

                double currentT = 0;

                if (t1 > 0 && t2 > 0)
                {
                    currentT = Math.Min(t1, t2);
                }
                else if (t1 > 0)
                {
                    currentT = t1;
                }
                else if (t2 > 0)
                {
                    currentT = t2;
                }
                else
                {
                    // No positive time, skip this iteration
                    continue;
                }

                Func<double, double> polynomialFunctionT = t =>
            (t * (initialVelocity * Math.Sin(theta))) - (0.5 * g * Math.Pow(t, 2)) - (h2 - h1 + L * Math.Sin(theta));

                double currentValue = polynomialFunctionT(currentT);

                if (currentValue < minValue && currentT > 0 && currentValue > 0)
                {
                    minT = currentT;
                    bestTheta = thetaDegrees;
                    minValue = currentValue;
                }
            }
        }

        double bestThetaRadians = Math.PI * bestTheta / 180.0;
        double x1 = minT * (initialVelocity * Math.Cos(bestThetaRadians)) + L * Math.Cos(bestThetaRadians);

        return Tuple.Create(x1, bestTheta);

    } 


    public static Tuple<double, double>? QuadraticFormula(double a, double b, double c)
    {
        // Calculate the discriminant (b^2 - 4ac)
        double discriminant = Math.Pow(b, 2) - 4 * a * c;

        if (discriminant < 0)
        {
            return null;
        }
        else
        {
            // Two possible solutions for t
            double t1 = (-b + Math.Sqrt(discriminant)) / (2 * a);
            double t2 = (-b - Math.Sqrt(discriminant)) / (2 * a);

            return Tuple.Create(t1, t2);
        }
    }
}


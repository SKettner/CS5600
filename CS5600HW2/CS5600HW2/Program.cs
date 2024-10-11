
using System;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;


//Much of this code was generated with the help of ChatGPT full conversation here: https://docs.google.com/document/d/1vj1MNyvnpIJs6DyeoXAeA-R1DEpe1scPs09khxQoBk8/edit?usp=sharing

class Program
{
    static void Main()
    {
        //var result = SolveEquations(
        //    x1: 0.0, x2: 0.0, x3: 0.0,  // Initial guesses
        //    a11: 10, a12: 2, a13: -1,
        //    a21: -3, a22: -6, a23: 2,
        //    a31: 1, a32: 1, a33: 5,
        //    b1: 27, b2: -61.5, b3: -21.5
        //);

        //Console.WriteLine($"x1: {result.x1}, x2: {result.x2}, x3: {result.x3}");


        // Define matrix A
        //var matrixA = DenseMatrix.OfArray(new double[,]
        //{
        //    { -192, 22, 0 },
        //    { 5, -27, 7 },
        //    { 177, 0, 88 }
        //});

        //// Compute the Eigenvalue Decomposition
        //var evd = matrixA.Evd();  // This returns an Eigenvalue Decomposition object

        //// Get the Eigenvalues
        //var eigenvalues = evd.EigenValues;

        //// Print the Eigenvalues
        //Console.WriteLine("Eigenvalues:");
        //foreach (var eigenvalue in eigenvalues)
        //{
        //    Console.WriteLine(eigenvalue);
        //}

        //// Parameters for the power method
        //double tolerance = 1e-7;
        //int maxIterations = 1000;

        //// Compute the inverse power method for the smallest eigenvalue
        //var(eigenvalue2, iterations) = InversePowerMethod(matrixA, tolerance, maxIterations);

        //Console.WriteLine($"Smallest Eigenvalue: {eigenvalue2}");
        //    Console.WriteLine($"Iterations: {iterations}");














        var A = DenseMatrix.OfArray(new double[,]
        {
            { -192, 22, 0 },
            { 5, -27, 7 },
            { 177, 0, 88 }
        });


        //// Perform LU decomposition
        //var lu = A.LU();

        //// Extract L and U matrices
        //var L = lu.L;
        //var U = lu.U;

        //// Print L and U matrices
        //Console.WriteLine("Matrix L:");
        //Console.WriteLine(L.ToString());

        //Console.WriteLine("\nMatrix U:");
        //Console.WriteLine(U.ToString());


        //Compute the inverse using LU factorization
        var inverseA = ComputeInverseUsingLU(A);

        // Print the original matrix A
        Console.WriteLine("Matrix A:");
        Console.WriteLine(A.ToString());

        // Print the inverse of A
        Console.WriteLine("\nInverse of A for LU factorization:");
        Console.WriteLine(inverseA.ToString());

        //// Approximate the inverse of the matrix
        //var inverseAByComputer = A.Inverse();

        //Console.WriteLine("\nInverse of A By Computer:");
        //Console.WriteLine(inverseA.ToString());
















        ////Define the matrix A
        //var A = DenseMatrix.OfArray(new double[,]
        //{
        //    { -0.00501509, -0.00408637, 0.000325052 },
        //    { 0.00168647, -0.0356629, 0.00283682 },
        //    { 0.0100872, 0.00821918, 0.0107098 }
        //});

        //double determinant = A.Determinant();
        //Console.WriteLine($"Determinant of A: {determinant}");

        //// Define the vector b
        ////var b = Vector<double>.Build.Dense(new double[] { 1000, 2000, 0 });
        //var b = Vector<double>.Build.Dense(new double[] { 1000, 0, 0 });

        //// Normalize each row of A and b by the largest absolute value in each row.
        //for (int i = 0; i < A.RowCount; i++)
        //{
        //    double rowNorm = A.Row(i).L2Norm();
        //    A.SetRow(i, A.Row(i) / rowNorm);
        //    b[i] /= rowNorm;
        //}

        //// Solve the system again after normalizing
        //var c = A.Solve(b);

        //// Print the solution
        //Console.WriteLine("Normalized solution vector c:");
        //Console.WriteLine(c);

        //var result = A * c;
        //Console.WriteLine("Verification (A * c):");
        //Console.WriteLine(result);

        //Console.WriteLine("Original b:");
        //Console.WriteLine(b);
    }

    public static (double x1, double x2, double x3) SolveEquations(
    double x1, double x2, double x3,
    double a11, double a12, double a13,
    double a21, double a22, double a23,
    double a31, double a32, double a33,
    double b1, double b2, double b3)
    {
        // Temporary variables to hold updated values
        double newX1, newX2, newX3;

        // Iteratively solve the system
        newX1 = (b1 - a12 * x2 - a13 * x3) / a11;

        newX2 = (b2 - a21 * newX1 - a23 * x3) / a22;

        newX3 = (b3 - a31 * newX1 - a32 * newX2) / a33;

        // Calculate errors
        double errorX1 = Math.Abs(newX1 - x1) / Math.Abs(newX1);
        double errorX2 = Math.Abs(newX2 - x2) / Math.Abs(newX2);
        double errorX3 = Math.Abs(newX3 - x3) / Math.Abs(newX3);

        if (errorX1 > .5 || errorX2 > .5 || errorX3 > .5)
        {
            SolveEquations(newX1, newX2, newX3, a11, a12, a13, a21, a22, a23, a31, a32, a33, b1, b2, b3);
        }

        // Return the updated x1, x2, and x3 values
        return (newX1, newX2, newX3);
    }

    public static (double eigenvalue, int iterations) InversePowerMethod(Matrix<double> matrix, double tolerance, int maxIterations)
    {
        int n = matrix.RowCount;
        var identityMatrix = DenseMatrix.CreateIdentity(n);

        // Initial guess for the eigenvector (can be a vector of ones)
        var x = Vector<double>.Build.Dense(n, 1);

        // Approximate the inverse of the matrix
        var inverseA = matrix.Inverse();

        double eigenvalue = 0.0;
        double previousEigenvalue;
        int iterations = 0;

        do
        {
            previousEigenvalue = eigenvalue;

            // Multiply x by the inverse of A
            x = inverseA * x;

            // Normalize x to prevent overflow/underflow
            x = x / x.L2Norm();

            // Approximate the eigenvalue (Rayleigh quotient)
            var Ax = matrix * x;
            eigenvalue = x.DotProduct(Ax) / x.DotProduct(x);

            iterations++;
        }
        while (Math.Abs(eigenvalue - previousEigenvalue) > tolerance && iterations < maxIterations);

        return (eigenvalue, iterations);
    }
    public static Matrix<double> ComputeInverseUsingLU(Matrix<double> matrix)
    {
        int n = matrix.RowCount;

        // Perform LU decomposition
        var lu = matrix.LU();

        // Create an identity matrix of the same size as 'matrix'
        var identity = DenseMatrix.CreateIdentity(n);

        // Create a matrix to store the inverse
        var inverse = DenseMatrix.Create(n, n, 0);

        // Solve for each column of the inverse
        for (int i = 0; i < n; i++)
        {
            // Extract the i-th column of the identity matrix
            var e = identity.Column(i);

            // Use the LU decomposition to solve for the i-th column of the inverse
            var x = lu.Solve(e);

            // Place the solution in the corresponding column of the inverse matrix
            inverse.SetColumn(i, x);
        }

        return inverse;
    }
}

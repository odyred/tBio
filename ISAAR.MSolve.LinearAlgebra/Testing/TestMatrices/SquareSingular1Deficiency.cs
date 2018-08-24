﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISAAR.MSolve.LinearAlgebra.Exceptions;
using ISAAR.MSolve.LinearAlgebra.Matrices;
using ISAAR.MSolve.LinearAlgebra.Output;
using ISAAR.MSolve.LinearAlgebra.Testing.Utilities;
using ISAAR.MSolve.LinearAlgebra.Vectors;

namespace ISAAR.MSolve.LinearAlgebra.Testing.TestMatrices
{
    /// <summary>
    /// Square non-symmetric singular matrix. There is only 1 rank deficiency (aka the dimension of its nullspace is 1). 
    /// LAPACK LU factorization does not flag it as singular, since there is no need to divide via the last pivot which is 0. 
    /// </summary>
    class SquareSingular1Deficiency
    {
        public const int order = 10;

        public static readonly double[,] matrix = new double[,] {
            { 5.0389,    0.2156,    9.4373,    8.2953,    4.0673,    3.8888,    4.5039,    2.7529,    5.7474,    1.1704 },
            { 6.4681,    5.5984,    5.4916,    8.4909,    6.6693,    4.5474,    2.0567,    7.1667,    3.2604,    8.1468 },
            { 3.0775,    3.0082,    7.2839,    3.7253,    9.3373,    2.4669,    8.9965,    2.8338,    4.5642,    3.2486 },
            { 1.3872,    9.3941,    5.7676,    5.9318,    8.1095,    7.8442,    7.6259,    8.9620,    7.1380,    2.4623 },
            { 6.6851,    5.9753,    3.7231,    6.5385,    9.8797,    1.4888,    1.2281,    8.3437,    4.3851,    3.9582 },
            { 3.6246,    2.8662,    4.4653,    9.3350,    7.5675,    9.1371,    2.8495,    3.9003,    7.2086,    3.7569 },
            { 7.8811,    8.0082,    6.4630,    6.6846,    4.1705,    5.5828,    6.7323,    4.9790,    0.1861,    5.4655 },
            { 7.8030,    8.9611,    5.2120,    2.0678,    9.7179,    5.9887,    6.6428,    6.9481,    6.7478,    5.6192 },
            { 6.6851,    5.9753,    3.7231,    6.5385,    9.8797,    1.4888,    1.2281,    8.3437,    4.3851,    3.9582 },
            { 1.3350,    8.8402,    9.3713,    0.7205,    8.6415,    8.9971,    4.0732,    6.0963,    4.3782,    3.9813 }};

        public static readonly double[] lhs = { 3.4484, 1.9563, 2.7385, 4.2828, 5.3064, 4.3251, 0.1117, 4.0487, 2.6311, 2.6269 };
        public static readonly double[] rhs = Utilities.MatrixOperations.MatrixTimesVector(matrix, lhs);

        public static readonly double[,] lower = new double[,] {
            { 1.000000000000000,  0.000000000000000,  0.000000000000000,  0.000000000000000, 0.000000000000000,  0.000000000000000,  0.000000000000000,  0.000000000000000, 0.000000000000000, 0.000000000000000 },
            { 0.176016038370278,  1.000000000000000,  0.000000000000000,  0.000000000000000, 0.000000000000000,  0.000000000000000,  0.000000000000000,  0.000000000000000, 0.000000000000000, 0.000000000000000 },
            { 0.639365063252592, -0.614258360352771,  1.000000000000000,  0.000000000000000, 0.000000000000000,  0.000000000000000,  0.000000000000000,  0.000000000000000, 0.000000000000000, 0.000000000000000 },
            { 0.169392597480047,  0.937271415669778,  0.483113323627930,  1.000000000000000, 0.000000000000000,  0.000000000000000,  0.000000000000000,  0.000000000000000, 0.000000000000000, 0.000000000000000 },
            { 0.848244534392407, -0.102399521086466, -0.157685267248492, -0.297951665007305, 1.000000000000000,  0.000000000000000,  0.000000000000000,  0.000000000000000, 0.000000000000000, 0.000000000000000 },
            { 0.459910418596389, -0.102304679402225,  0.241323495128026, -0.616809201690111, 0.512564638063054,  1.000000000000000,  0.000000000000000,  0.000000000000000, 0.000000000000000, 0.000000000000000 },
            { 0.390491175089772, -0.014895235256103,  0.592595216332522,  0.356105296144049, 0.662819266563724, -0.112366063390058,  1.000000000000000,  0.000000000000000, 0.000000000000000, 0.000000000000000 },
            { 0.990090215832815,  0.129282468147027, -0.219107740852256,  0.443190110914549, 0.902218083252459,  0.383040953999945,  0.804780729738722,  1.000000000000000, 0.000000000000000, 0.000000000000000 },
            { 0.820710306936849, -0.121987453223332,  0.092298751705360, -0.358045299789388, 0.392713916638845,  0.140868030195130, -0.326422176831072, -0.057833721997052, 1.000000000000000, 0.000000000000000 },
            { 0.848244534392407, -0.102399521086466, -0.157685267248492, -0.297951665007305, 1.000000000000000, -0.000000000000000, -0.000000000000000,  0.000000000000000, 0.000000000000000, 1.000000000000000 }};

        public static readonly double[,] upper = new double[,] {
            { 7.881100000000000, 8.008200000000000, 6.463000000000000,  6.684600000000000,  4.170500000000000,  5.582800000000000,  6.732300000000000,  4.979000000000000,  0.186100000000000,  5.465500000000000 },
            { 0.000000000000000, 7.984528361523138, 4.630008344012891,  4.755203189910038,  7.375425111976755,  6.861537660986411,  6.440907224879775,  8.085616144954384,  7.105243415259291,  1.500284342287244 },
            { 0.000000000000000, 0.000000000000000, 8.149104930011509,  6.942323612760134,  5.931244539892565,  4.534109598009734,  4.155883695803547,  4.536158665706923,  9.992869331893191, -1.402487553050741 },
            { 0.000000000000000, 0.000000000000000, 0.000000000000000, -8.222666816980754, -1.843190226576928, -0.570196867983617, -5.111842801766466, -4.517001323090270, -7.140553832400010,  2.326871552171181 },
            { 0.000000000000000, 0.000000000000000, 0.000000000000000,  0.000000000000000,  7.483424452047309, -1.999090238705725, -4.690751306515450,  4.317691010621143,  4.403003582815848, -0.052108475965917 },
            { 0.000000000000000, 0.000000000000000, 0.000000000000000,  0.000000000000000,  0.000000000000000,  6.850252639898760, -1.339450773287580, -3.656302980631695, -1.222787079562291,  3.197143660640692 },
            { 0.000000000000000, 0.000000000000000, 0.000000000000000,  0.000000000000000,  0.000000000000000,  0.000000000000000,  8.779744117436120, -3.342289399487064, -1.837369037792679,  1.533002651565117 },
            { 0.000000000000000, 0.000000000000000, 0.000000000000000,  0.000000000000000,  0.000000000000000,  0.000000000000000,  0.000000000000000,  4.163735174483451,  8.973685945725602, -3.735995619097978 },
            { 0.000000000000000, 0.000000000000000, 0.000000000000000,  0.000000000000000,  0.000000000000000,  0.000000000000000,  0.000000000000000,  0.000000000000000, -1.142200259654890,  4.661224880916461 },
            { 0.000000000000000, 0.000000000000000, 0.000000000000000,  0.000000000000000,  0.000000000000000,  0.000000000000000,  0.000000000000000,  0.000000000000000,  0.000000000000000, -0.000000000000000 } };

        public static void CheckFactorization()
        {
            var comparer = new Comparer(Comparer.PrintMode.Always);
            var A = Matrix.CreateFromArray(matrix);
            var factor = A.FactorLU();
            var L = factor.GetFactorL();
            var U = factor.GetFactorU();
            comparer.CheckFactorizationLU(matrix, lower, upper, L.CopyToArray2D(), U.CopyToArray2D(), factor.IsSingular);
        }

        public static void CheckSystemSolution()
        {
            var comparer = new Comparer(Comparer.PrintMode.Always);
            var b = Vector.CreateFromArray(rhs);
            var A = Matrix.CreateFromArray(matrix);
            try
            {
                var factor = A.FactorLU();
                var x = factor.SolveLinearSystem(b);
                comparer.CheckSystemSolution(matrix, rhs, lhs, x.InternalData);
            }
            catch (SingularMatrixException)
            {
                var printer = new Printer();
                printer.PrintSingularMatrix(matrix);
            }
        }

        public static void Print()
        {
            var A = Matrix.CreateFromArray(matrix);
            Console.WriteLine("Square singular matrix = ");
            var writer = new FullMatrixWriter();
            writer.WriteToConsole(A);
        }
    }
}

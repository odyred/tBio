﻿using System;
using System.Collections.Generic;
using System.Text;
using ISAAR.MSolve.XFEM.Geometry.CoordinateSystems;
using ISAAR.MSolve.XFEM.Solvers;
using ISAAR.MSolve.XFEM.Solvers.MenkBordas;

namespace ISAAR.MSolve.XFEM.Tests.GRACM
{
    class BenchmarkSolvers
    {
        private delegate ISolver CreateSolver(IBenchmark benchmark);

        public static void Run()
        {
            SingleTest();
            //BenchmarkSolver();
        }


        private static void SingleTest()
        {
            IBenchmarkBuilder builder = Fillet.SetupBenchmark();
            IBenchmark benchmark = builder.BuildBenchmark();
            benchmark.InitializeModel();

            // Solvers
            var solver = CreateCholeskySuiteSparseSolver(benchmark);
            //var solver = CreateCholeskyAMDSolver(benchmark);
            //var solver = CreateMenkBordasSolver(benchmark);
            IReadOnlyList<ICartesianPoint2D> crackPath = benchmark.Analyze(solver);

            //Reanalysis solvers
            //IReadOnlyList<ICartesianPoint2D> crackPath;
            //using (var solver = CreateReanalysisRebuildingSolver(benchmark))
            //using (var solver = CreateReanalysisSolver(benchmark))
            //{
            //    crackPath = benchmark.Analyze(solver);
            //}

            Console.WriteLine("Crack path:");
            foreach (var point in crackPath)
            {
                Console.WriteLine("{0} {1}", point.X, point.Y);
            }
            Console.WriteLine();

            Console.WriteLine("Crack growth angles:");
            foreach (var angle in benchmark.GrowthAngles)
            {
                Console.WriteLine(angle);
            }
        }

        private static void BenchmarkSolver()
        {
            int numRepetitions = 10;

            /// Define the benchmark problem once
            IBenchmarkBuilder builder = Fillet.SetupBenchmark();
            builder.LsmOutputDirectory = null; // Don't waste time with all that I/O when benchmarking

            /// Choose solver
            //CreateSolver solverFunc = CreateCholeskyAMDSolver;
            //string solverName = "CholeskyAMDSolver";
            CreateSolver solverFunc = CreateReanalysisSolver;
            string solverName = "ReanalysisSolver";
            //CreateSolver solverFunc = CreateMenkBordasSolver;
            //string solverName = "MenkBordasSolver";

            /// Call once to load all necessary DLLs
            IBenchmark firstTry = builder.BuildBenchmark();
            firstTry.InitializeModel();
            firstTry.Analyze(solverFunc(firstTry));

            /// Actually time the solver
            for (int t = 0; t < numRepetitions; ++t)
            {
                /// Create a new benchmark at each iteration
                IBenchmark benchmark = builder.BuildBenchmark();
                benchmark.InitializeModel();

                /// Run the analysis
                ISolver solver = solverFunc(benchmark);
                benchmark.Analyze(solver);

                /// Write the timing results
                solver.Logger.WriteToFile(builder.TimingPath, solverName, true);

                /// Dispose any unmanaged memory
                if (solver is IDisposable handle) handle.Dispose();
            }
        }

        private static ISolver CreateCholeskySuiteSparseSolver(IBenchmark benchmark)
        {
            return new CholeskySuiteSparseSolver(benchmark.Model);
        }

        private static ISolver CreateCholeskyAMDSolver(IBenchmark benchmark)
        {
            return new CholeskyAMDSolver(benchmark.Model);
        }

        private static ISolver CreateMenkBordasSolver(IBenchmark benchmark)
        {
            return new MenkBordasSolver(benchmark.Model, benchmark.Decomposer, 1000000, double.Epsilon);
        }

        private static ReanalysisRebuildingSolver CreateReanalysisRebuildingSolver(IBenchmark benchmark)
        {
            return new ReanalysisRebuildingSolver(benchmark.Model, benchmark.EnrichedArea, benchmark.Crack);
        }

        private static ReanalysisSolver CreateReanalysisSolver(IBenchmark benchmark)
        {
            return new ReanalysisSolver(benchmark.Model, benchmark.EnrichedArea, benchmark.Crack);
        }
    }
}

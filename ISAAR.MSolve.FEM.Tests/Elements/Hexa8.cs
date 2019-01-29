﻿using System.Collections.Generic;
using ISAAR.MSolve.FEM.Elements;
using ISAAR.MSolve.FEM.Entities;
using ISAAR.MSolve.FEM.Materials;
using ISAAR.MSolve.Geometry.Shapes;
using ISAAR.MSolve.LinearAlgebra.Matrices;
using ISAAR.MSolve.Materials;
using Xunit;

namespace ISAAR.MSolve.FEM.Tests.Elements
{
    /// <summary>
    /// Tests 8-noded hexahedral instances of <see cref="Hexa8"/>
    /// </summary>
    public class Hexa8
    {
        private static  readonly ElasticMaterial3D_v2 Material0 =  new ElasticMaterial3D_v2
        {
            YoungModulus = 1000,
            PoissonRatio = 0.3
        };

        private static readonly DynamicMaterial DynamicMaterial= new DynamicMaterial(1,0,0);

        internal static IReadOnlyList<Node_v2> AbaqusToMSolveHexa8Nodes(IReadOnlyList<Node_v2> abaqusOrder)
        {
            var msolveFromAbaqusMap = new int[] { 2, 6, 7, 3, 1, 5, 4, 0 };
            var msolveOrder = new Node_v2[8];
            for (int i = 0; i < 8; ++i) msolveOrder[i] = abaqusOrder[msolveFromAbaqusMap[i]]; 
            return msolveOrder;
        }

        internal static Matrix AbaqusToMSolveHexa8StructuralMatrix(IMatrixView abaqusMatrix)
        {
            var msolveFromAbaqusNodeMap = new int[] { 2, 6, 7, 3, 1, 5, 4, 0 };
            var msolveFromAbaqusDofMap = new int[24];
            for (int node = 0; node < 8; ++node)
            {
                msolveFromAbaqusDofMap[3 * node + 0] = 3 * msolveFromAbaqusNodeMap[node] + 0;
                msolveFromAbaqusDofMap[3 * node + 1] = 3 * msolveFromAbaqusNodeMap[node] + 1;
                msolveFromAbaqusDofMap[3 * node + 2] = 3 * msolveFromAbaqusNodeMap[node] + 2;
            }

            var msolveMatrix = Matrix.CreateFromMatrix(abaqusMatrix);
            return msolveMatrix.Reorder(msolveFromAbaqusDofMap, false);
        }

        // The nodes in the order of the Abaqus Hexa8 element
        private static readonly IReadOnlyList<Node_v2> NodeSet0= new Node_v2[]
        {
            new Node_v2 { ID = 0, X = 0, Y = 5, Z = 5 },
            new Node_v2 { ID = 1, X = 0, Y = 0, Z = 5 },
            new Node_v2 { ID = 2, X = 0, Y = 0, Z = 0 },
            new Node_v2 { ID = 3, X = 0, Y = 5, Z = 0 },

            new Node_v2 { ID = 4, X = 5, Y = 5, Z = 5 },
            new Node_v2 { ID = 5, X = 5, Y = 0, Z = 5 },
            new Node_v2 { ID = 6, X = 5, Y = 0, Z = 0 },
            new Node_v2 { ID = 7, X = 5, Y = 5, Z = 0 },
        };

        [Fact]
        private static void TestStiffnessMatrix0()
        {
            var factory = new ContinuumElement3DFactory(Material0, DynamicMaterial);
            var hexa8 = factory.CreateElement(CellType3D.Hexa8, AbaqusToMSolveHexa8Nodes(NodeSet0));
            var computedK = hexa8.BuildStiffnessMatrix();

            //string output = @"C:\Users\Serafeim\Desktop\hexa8.txt";
            //var writer = new LinearAlgebra.Output.FullMatrixWriter();
            //writer.ArrayFormat = LinearAlgebra.Output.Formatting.Array2DFormat.CSharpArray;
            //writer.WriteToFile(computedK, output);

            //TODO: I think this is from a reduced integration Hexa8 Abaqus element.
            var expectedK = AbaqusToMSolveHexa8StructuralMatrix(Matrix.CreateFromArray(new double[24, 24]
            {
                { 972.667378917380  ,  -313.835470085470 ,  -313.835470085470 ,  296.029202279200  ,  -6.67735042735140  , -287.126068376070 ,  296.029202279200  ,  -287.126068376070 ,  -6.67735042735140 ,  117.966524216520  ,  126.869658119660  ,  126.869658119660  ,  -331.641737891740 ,  6.67735042735140 ,   6.67735042735140  ,  -456.285612535610 ,  313.835470085470  ,  -126.869658119660 ,  -456.285612535610 ,  -126.869658119660 ,  313.835470085470  ,  -438.479344729340 ,  287.126068376070   , 287.126068376070},
                { -313.835470085470 ,  972.667378917380 ,   313.835470085470  ,  6.67735042735140  ,  -331.641737891740  , -6.67735042735140 ,  -287.126068376070 ,  296.029202279200  ,  6.67735042735140 ,   -126.869658119660 ,  -456.285612535610 ,  -313.835470085470 ,  -6.67735042735140 ,  296.029202279200 ,   287.126068376070  ,  313.835470085470  ,  -456.285612535610 ,  126.869658119660  ,  126.869658119660  ,  117.966524216520  ,  -126.869658119660 ,  287.126068376070  ,  -438.479344729340  , -287.126068376070},
                { -313.835470085470 ,  313.835470085470 ,   972.667378917380  ,  -287.126068376070 ,  6.67735042735140   , 296.029202279200  ,  6.67735042735140  ,  -6.67735042735140 ,  -331.641737891740,   -126.869658119660 ,  -313.835470085470 ,  -456.285612535610 ,  -6.67735042735140 ,  287.126068376070 ,   296.029202279200  ,  126.869658119660  ,  -126.869658119660 ,  117.966524216520  ,  313.835470085470  ,  126.869658119660  ,  -456.285612535610 ,  287.126068376070  ,  -287.126068376070  , -438.479344729340},
                { 296.029202279200  ,  6.67735042735140  ,  -287.126068376070 ,  972.667378917380  ,  313.835470085470  ,  -313.835470085470 ,  117.966524216520  ,  -126.869658119660 ,  126.869658119660 ,   296.029202279200  ,  287.126068376070  ,  -6.67735042735140 ,  -456.285612535610 ,  -313.835470085470,   -126.869658119660 ,  -331.641737891740 ,  -6.67735042735140 ,  6.67735042735140  ,  -438.479344729340 ,  -287.126068376070 ,  287.126068376070  ,  -456.285612535610 ,  126.869658119660   , 313.835470085470},
                { -6.67735042735140 ,  -331.641737891740 ,  6.67735042735140  ,  313.835470085470  ,  972.667378917380  ,  -313.835470085470 ,  126.869658119660  ,  -456.285612535610 ,  313.835470085470 ,   287.126068376070  ,  296.029202279200  ,  -6.67735042735140,   -313.835470085470 ,  -456.285612535610,   -126.869658119660 ,  6.67735042735130  ,  296.029202279200 ,   -287.126068376070 ,  -287.126068376070 ,  -438.479344729340 ,  287.126068376070  ,  -126.869658119660 ,  117.966524216520   , 126.869658119660},
                { -287.126068376070 ,  -6.67735042735140 ,  296.029202279200  ,  -313.835470085470 ,  -313.835470085470 ,  972.667378917380  ,  -126.869658119660 ,  313.835470085470  ,  -456.285612535610,   6.67735042735140  ,  6.67735042735140  ,  -331.641737891740,   126.869658119660  ,  126.869658119660 ,   117.966524216520  ,  -6.67735042735140 ,  -287.126068376070,   296.029202279200  ,  287.126068376070  ,  287.126068376070  ,  -438.479344729340 ,  313.835470085470  ,  -126.869658119660  , -456.285612535610},
                { 296.029202279200  ,  -287.126068376070 ,  6.67735042735140  ,  117.966524216520  ,  126.869658119660  ,  -126.869658119660 ,  972.667378917380  ,  -313.835470085470 ,  313.835470085470,    296.029202279200  ,  -6.67735042735140 ,  287.126068376070 ,   -456.285612535610 ,  -126.869658119660,   -313.835470085470 ,  -438.479344729340 ,  287.126068376070 ,   -287.126068376070 ,  -331.641737891740 ,  6.67735042735140  ,  -6.67735042735140 ,  -456.285612535610 ,  313.835470085470   , 126.869658119660},
                { -287.126068376070 ,  296.029202279200   , -6.67735042735140 ,  -126.869658119660 ,  -456.285612535610 ,  313.835470085470  ,  -313.835470085470 ,  972.667378917380  ,  -313.835470085470 ,  6.67735042735140  ,  -331.641737891740 ,  6.67735042735130  ,  126.869658119660  ,  117.966524216520 ,   126.869658119660  ,  287.126068376070  ,  -438.479344729340 ,  287.126068376070  ,  -6.67735042735140 ,  296.029202279200  ,  -287.126068376070 ,  313.835470085470  ,  -456.285612535610  , -126.869658119660},
                { -6.67735042735140 ,  6.67735042735140  ,  -331.641737891740 ,  126.869658119660  ,  313.835470085470  ,  -456.285612535610 ,  313.835470085470  ,  -313.835470085470 ,  972.667378917380  ,  287.126068376070  ,  -6.67735042735140 ,  296.029202279200  ,  -313.835470085470 ,  -126.869658119660,   -456.285612535610 ,  -287.126068376070 ,  287.126068376070  ,  -438.479344729340 ,  6.67735042735140  ,  -287.126068376070 ,  296.029202279200  ,  -126.869658119660 ,  126.869658119660   , 117.966524216520},
                { 117.966524216520  ,  -126.869658119660 ,  -126.869658119660 ,  296.029202279200  ,  287.126068376070  ,  6.67735042735140  ,  296.029202279200  ,  6.67735042735140  ,  287.126068376070  ,  972.667378917380  ,  313.835470085470  ,  313.835470085470  ,  -438.479344729340 ,  -287.126068376070,   -287.126068376070 ,  -456.285612535610 ,  126.869658119660  ,  -313.835470085470 ,  -456.285612535610 ,  -313.835470085470 ,  126.869658119660  ,  -331.641737891740 ,  -6.67735042735140  , -6.67735042735140},
                { 126.869658119660   , -456.285612535610  , -313.835470085470 ,  287.126068376070  ,  296.029202279200  ,  6.67735042735140  ,  -6.67735042735140 ,  -331.641737891740 ,  -6.67735042735140 ,  313.835470085470  ,  972.667378917380  ,  313.835470085470  ,  -287.126068376070 ,  -438.479344729340,   -287.126068376070 ,  -126.869658119660 ,  117.966524216520  ,  -126.869658119660 ,  -313.835470085470 ,  -456.285612535610 ,  126.869658119660  ,  6.67735042735140  ,  296.029202279200   , 287.126068376070},
                { 126.869658119660  ,  -313.835470085470 ,  -456.285612535610 ,  -6.67735042735140 ,  -6.67735042735140 ,  -331.641737891740 ,  287.126068376070  ,  6.67735042735130  ,  296.029202279200  ,  313.835470085470  ,  313.835470085470  ,  972.667378917380  ,  -287.126068376070 ,  -287.126068376070,   -438.479344729340 ,  -313.835470085470 ,  126.869658119660  ,  -456.285612535610 ,  -126.869658119660  , -126.869658119660 ,  117.966524216520  ,  6.67735042735140  ,  287.126068376070   , 296.029202279200},
                { -331.641737891740 ,  -6.67735042735140  , -6.67735042735140 ,  -456.285612535610 ,  -313.835470085470 ,  126.869658119660  ,  -456.285612535610 ,  126.869658119660  ,  -313.835470085470 ,  -438.479344729340 ,  -287.126068376070 ,  -287.126068376070 ,  972.667378917380  ,  313.835470085470 ,   313.835470085470  ,  296.029202279200  ,  6.67735042735130  ,  287.126068376070  ,  296.029202279200  ,  287.126068376070  ,  6.67735042735140  ,  117.966524216520  ,  -126.869658119660  , -126.869658119660},
                { 6.67735042735140  ,  296.029202279200  ,  287.126068376070  ,  -313.835470085470 ,  -456.285612535610 ,  126.869658119660  ,  -126.869658119660 ,  117.966524216520  ,  -126.869658119660 ,  -287.126068376070 ,  -438.479344729340 ,  -287.126068376070 ,  313.835470085470  ,  972.667378917380 ,   313.835470085470  ,  -6.67735042735140 ,  -331.641737891740 ,  -6.67735042735140 ,  287.126068376070  ,  296.029202279200  ,  6.67735042735140  ,  126.869658119660  ,  -456.285612535610  , -313.835470085470},
                { 6.67735042735140  ,  287.126068376070  ,  296.029202279200  ,  -126.869658119660 ,  -126.869658119660 ,  117.966524216520  ,  -313.835470085470 ,  126.869658119660  ,  -456.285612535610 ,  -287.126068376070 ,  -287.126068376070 ,  -438.479344729340 ,  313.835470085470  ,  313.835470085470 ,   972.667378917380  ,  287.126068376070  ,  6.67735042735140  ,  296.029202279200  ,  -6.67735042735140 ,  -6.67735042735140 ,  -331.641737891740 ,  126.869658119660  ,  -313.835470085470  , -456.285612535610},
                { -456.285612535610 ,  313.835470085470  ,  126.869658119660  ,  -331.641737891740 ,  6.67735042735130  ,  -6.67735042735140 ,  -438.479344729340 ,  287.126068376070  ,  -287.126068376070 ,  -456.285612535610 ,  -126.869658119660 ,  -313.835470085470 ,  296.029202279200  ,  -6.67735042735140,   287.126068376070  ,  972.667378917380  ,  -313.835470085470 ,  313.835470085470  ,  117.966524216520  ,  126.869658119660  ,  -126.869658119660 ,  296.029202279200  ,  -287.126068376070  , 6.67735042735140},
                { 313.835470085470  ,  -456.285612535610 ,  -126.869658119660 ,  -6.67735042735140 ,  296.029202279200  ,  -287.126068376070 ,  287.126068376070  ,  -438.479344729340 ,  287.126068376070  ,  126.869658119660  ,  117.966524216520  ,  126.869658119660  ,  6.67735042735130  ,  -331.641737891740,   6.67735042735140  ,  -313.835470085470 ,  972.667378917380  ,  -313.835470085470 ,  -126.869658119660 ,  -456.285612535610 ,  313.835470085470  ,  -287.126068376070 ,  296.029202279200   , -6.67735042735140},
                { -126.869658119660 ,  126.869658119660  ,  117.966524216520  ,  6.67735042735140  ,  -287.126068376070 ,  296.029202279200  ,  -287.126068376070 ,  287.126068376070  ,  -438.479344729340 ,  -313.835470085470 ,  -126.869658119660 ,  -456.285612535610 ,  287.126068376070  ,  -6.67735042735140,   296.029202279200  ,  313.835470085470  ,  -313.835470085470 ,  972.667378917380  ,  126.869658119660  ,  313.835470085470  ,  -456.285612535610 ,  -6.67735042735130 ,  6.67735042735140   , -331.641737891740},
                { -456.285612535610  , 126.869658119660  ,  313.835470085470  ,  -438.479344729340 ,  -287.126068376070 ,  287.126068376070  ,  -331.641737891740 ,  -6.67735042735140 ,  6.67735042735140  ,  -456.285612535610 ,  -313.835470085470 ,  -126.869658119660 ,  296.029202279200  ,  287.126068376070 ,   -6.67735042735140 ,  117.966524216520  ,  -126.869658119660 ,  126.869658119660  ,  972.667378917380  ,  313.835470085470  ,  -313.835470085470 ,  296.029202279200  ,  6.67735042735140   , -287.126068376070},
                { -126.869658119660 ,  117.966524216520  ,  126.869658119660  ,  -287.126068376070 ,  -438.479344729340 ,  287.126068376070  ,  6.67735042735140  ,  296.029202279200  ,  -287.126068376070 ,  -313.835470085470 ,  -456.285612535610 ,  -126.869658119660 ,  287.126068376070  ,  296.029202279200 ,   -6.67735042735140 ,  126.869658119660  ,  -456.285612535610 ,  313.835470085470  ,  313.835470085470  ,  972.667378917380  ,  -313.835470085470 ,  -6.67735042735140 ,  -331.641737891740  , 6.67735042735140},
                { 313.835470085470  ,  -126.869658119660 ,  -456.285612535610 ,  287.126068376070  ,  287.126068376070  ,  -438.479344729340,   -6.67735042735140 ,  -287.126068376070 ,  296.029202279200  ,  126.869658119660  ,  126.869658119660  ,  117.966524216520  ,  6.67735042735140  ,  6.67735042735140 ,   -331.641737891740 ,  -126.869658119660 ,  313.835470085470  ,  -456.285612535610 ,  -313.835470085470 ,  -313.835470085470 ,  972.667378917380  ,  -287.126068376070 ,  -6.67735042735140  , 296.029202279200},
                { -438.479344729340 ,  287.126068376070  ,  287.126068376070  ,  -456.285612535610 ,  -126.869658119660 ,  313.835470085470  ,  -456.285612535610 ,  313.835470085470  ,  -126.869658119660 ,  -331.641737891740 ,  6.67735042735140  ,  6.67735042735140  ,  117.966524216520  ,  126.869658119660 ,   126.869658119660  ,  296.029202279200  ,  -287.126068376070 ,  -6.67735042735130 ,  296.029202279200  ,  -6.67735042735140 ,  -287.126068376070 ,  972.667378917380  ,  -313.835470085470  , -313.835470085470},
                { 287.126068376070  ,  -438.479344729340 ,  -287.126068376070 ,  126.869658119660  ,  117.966524216520  ,  -126.869658119660 ,  313.835470085470  ,  -456.285612535610 ,  126.869658119660  ,  -6.67735042735140 ,  296.029202279200  ,  287.126068376070  ,  -126.869658119660 ,  -456.285612535610,   -313.835470085470 ,  -287.126068376070 ,  296.029202279200  ,  6.67735042735140  ,  6.67735042735140  ,  -331.641737891740 ,  -6.67735042735140 ,  -313.835470085470 ,  972.667378917380   , 313.835470085470},
                { 287.126068376070   , -287.126068376070  , -438.479344729340 ,  313.835470085470  ,  126.869658119660  ,  -456.285612535610 ,  126.869658119660  ,  -126.869658119660 ,  117.966524216520  ,  -6.67735042735140 ,  287.126068376070  ,  296.029202279200  ,  -126.869658119660 ,  -313.835470085470,   -456.285612535610 ,  6.67735042735140  ,  -6.67735042735140 ,  -331.641737891740 ,  -287.126068376070 ,  6.67735042735140  ,  296.029202279200  ,  -313.835470085470 ,  313.835470085470   , 972.667378917380},
            }));

            //Assert.True(computedK.Equals(expectedK, 1e-8));
        }
    }
}

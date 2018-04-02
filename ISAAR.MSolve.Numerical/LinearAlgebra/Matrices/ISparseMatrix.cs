﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISAAR.MSolve.Numerical.LinearAlgebra.Matrices
{
    public interface ISparseMatrix
    {
        int CountNonZeros();
        IEnumerable<(int row, int col, double value)> EnumerateNonZeros();
    }
}

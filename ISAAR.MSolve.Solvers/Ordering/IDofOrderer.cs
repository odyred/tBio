﻿using ISAAR.MSolve.Discretization.FreedomDegrees;
using ISAAR.MSolve.Discretization.Interfaces;

namespace ISAAR.MSolve.Solvers.Ordering
{
    /// <summary>
    /// Orders the unconstrained freedom degrees (dofs) of the physical model, by assigning an index to each unique dof. These 
    /// indices are used in vectors and matrices that contain quantities for the whole model (or its subdomains) to locate the 
    /// contribution of each dof.
    /// Authors: Serafeim Bakalakos
    /// </summary>
    public interface IDofOrderer
    {
        /// <summary>
        /// Calculates an ordering for the unconstrained freedom degrees of the physical model and its subdomains.
        /// </summary>
        /// <param name="model">The physical model.</param>
        IGlobalFreeDofOrdering OrderDofs(IStructuralModel_v2 model);
    }
}

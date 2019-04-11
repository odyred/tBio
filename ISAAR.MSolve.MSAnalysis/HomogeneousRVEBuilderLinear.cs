﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ISAAR.MSolve.Materials.Interfaces; //using ISAAR.MSolve.PreProcessor.Interfaces;
using ISAAR.MSolve.Numerical.LinearAlgebra.Interfaces; //using ISAAR.MSolve.Matrices.Interfaces;
using ISAAR.MSolve.Numerical.LinearAlgebra; //using ISAAR.MSolve.Matrices;
using ISAAR.MSolve.Analyzers;
using ISAAR.MSolve.Logging;
using ISAAR.MSolve.PreProcessor;
using ISAAR.MSolve.Problems;
using ISAAR.MSolve.FEM;
using ISAAR.MSolve.FEM.Elements;
using ISAAR.MSolve.FEM.Entities;
using ISAAR.MSolve.FEM.Materials;
using ISAAR.MSolve.Materials;
using ISAAR.MSolve.Solvers.Interfaces;
using ISAAR.MSolve.MultiscaleAnalysis.Interfaces;
using ISAAR.MSolve.PreProcessor.Embedding;
using ISAAR.MSolve.MultiscaleAnalysis.SupportiveClasses;
using ISAAR.MSolve.Discretization.Interfaces;

namespace ISAAR.MSolve.MultiscaleAnalysis
{
    /// <summary>
    /// Creates a elastic matrix rve
    /// Authors Gerasimos Sotiropoulos
    /// </summary>
    public class HomogeneousRVEBuilderLinear : IRVEbuilder_v2
    {        
        private Tuple<rveMatrixParameters, grapheneSheetParameters> mpgp ;
        private rveMatrixParameters mp;
        private grapheneSheetParameters gp;
        private string renumbering_vector_path;

        public HomogeneousRVEBuilderLinear()
        {
            //TODOGerasimos
            // this.renumbering_vector_path=renumbering_vector_path,
            // this.subdiscr1=subdiscr1
        }
        public IRVEbuilder_v2 Clone(int a) => new HomogeneousRVEBuilderLinear();

        public Tuple<Model_v2, Dictionary<int, Node_v2>,double> GetModelAndBoundaryNodes()
        {
           return Reference2RVEExample10_000withRenumbering_mono_hexa();
        }

        

        public Tuple<Model_v2, Dictionary<int, Node_v2>,double> Reference2RVEExample10_000withRenumbering_mono_hexa()
        {
            Model_v2 model = new Model_v2();
            model.SubdomainsDictionary.Add(1, new Subdomain_v2( 1 ));

            Dictionary<int, Node_v2> boundaryNodes= new Dictionary<int, Node_v2>();
            // COPY APO: Reference2RVEExample100_000withRenumbering_mono_hexa
            double[,] Dq = new double[1, 1];
            //Tuple<rveMatrixParameters, grapheneSheetParameters> mpgp;
            //rveMatrixParameters mp;
            //grapheneSheetParameters gp;
            renumbering_vector_path = "..\\..\\..\\RveTemplates\\Input\\RveHomogeneous\\REF_new_total_numbering27.txt";
            
            string Fxk_p_komvoi_rve_path = @"C:\Users\turbo-x\Desktop\notes_elegxoi\REFERENCE_fe2_diafora_check\fe2_tax_me1_arxiko_chol_dixws_me1_OriginalRVEExampleChol_me_a1_REF2_10_000_renu_new_multiple_algorithms_check_stress_27hexa\Fxk_p_komvoi_rve.txt";
            int subdiscr1 = 1;
            int discr1 = 3;
            // int discr2 dn xrhsimopoieitai
            int discr3 = 3;
            int subdiscr1_shell = 7;
            int discr1_shell = 1;

            mpgp = FEMMeshBuilder_v2.GetReferenceRveExampleParameters(subdiscr1, discr1, discr3, subdiscr1_shell, discr1_shell);
            mp = mpgp.Item1;
            gp = mpgp.Item2;
            double[][] ekk_xyz = new double[2][] { new double[] { 0, 0, 0 }, new double[] { 0.25 * 105, 0, 0.25 * 40 } };

            int graphene_sheets_number = 0; // 0 gra sheets afou exoume mono hexa
            o_x_parameters[] model_o_x_parameteroi = new o_x_parameters[graphene_sheets_number];


            FEMMeshBuilder_v2.LinearHexaElementsOnlyRVEwithRenumbering_forMS(model, mp, Dq, renumbering_vector_path, boundaryNodes);
            double volume = mp.L01 * mp.L02 * mp.L03;

            

            return new Tuple<Model_v2, Dictionary<int, Node_v2>,double>(model, boundaryNodes,volume);
        }

        


    }
    //HomogeneousRVEBuilderCheck27HexaLinear
    //TODOGerasimos gia na ta krataei mesa kai na kanei build model oses fores tou zhththei
    // omoiws na ginei kai to RVE me graphene sheets 
    // string renumbering_vector_path; 
    // int subdiscr1;
}

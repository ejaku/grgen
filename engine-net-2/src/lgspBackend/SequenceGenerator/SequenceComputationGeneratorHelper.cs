/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// The sequence computation generator helper contains code for (sequence/sequence computation) result variable handling.
    /// </summary>
    public class SequenceComputationGeneratorHelper
    {
        /// <summary>
        /// Returns a string containing a C# expression to get the value of the result variable of the sequence or sequence computation construct
        /// (every sequence part writes a success-value which is read by other parts determining execution flow)
        /// </summary>
        public static string GetResultVar(SequenceBase seq)
        {
            return "res_" + seq.Id;
        }

        /// <summary>
        /// Returns a string containing a C# assignment to set the result variable of the sequence or sequence computation construct
        /// to the value as computed by the C# expression in the string given
        /// (every sequence part writes a success-value which is read by other parts determining execution flow)
        /// </summary>
        public static string SetResultVar(SequenceBase seq, String valueToWrite)
        {
            if(seq is Sequence)
                return "res_" + seq.Id + " = (bool)(" + valueToWrite + ");\n";
            else
                return "res_" + seq.Id + " = " + valueToWrite + ";\n";
        }

        /// <summary>
        /// Returns a string containing a C# declaration of the result variable of the sequence (or sequence computation) construct
        /// </summary>
        public static string DeclareResultVar(SequenceBase seq)
        {
            if(seq is Sequence)
                return "bool res_" + seq.Id + ";\n";
            else
                return "object res_" + seq.Id + ";\n";
        }
    }
}

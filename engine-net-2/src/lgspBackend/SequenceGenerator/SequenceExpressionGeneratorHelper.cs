/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2019 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Diagnostics;
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// The sequence expression generator helper generates the code for the sequence expression operators,
    /// in case the (result) type is known statically (same semantics as calling the SequenceExpressionExecutionHelper, but faster due to statically known types).
    /// </summary>
    public static class SequenceExpressionGeneratorHelper
    {
        public static string EqualStatic(string leftValue, string rightValue,
            string balancedType, string leftType, string rightType, IGraphModel model)
        {
            // byte and short are only used for storing, no computations are done with them
            // enums are handled via int, node and edges are handled via reference equality
            if(balancedType == "int")
            {
                return "((int)"+leftValue+" == "+"(int)"+rightValue+")";
            }
            else if(balancedType == "long")
            {
                return "((long)"+leftValue+" == "+"(long)"+rightValue+")";
            }
            else if(balancedType == "float")
            {
                return "((float)"+leftValue+" == "+"(float)"+rightValue+")";
            }
            else if(balancedType == "double")
            {
                return "((double)"+leftValue+" == "+"(double)"+rightValue+")";
            }
            else if(balancedType == "boolean")
            {
                return "((bool)"+leftValue+" == "+"(bool)"+rightValue+")";
            }
            else if(balancedType == "string")
            {
                return "((string)"+leftValue+" == "+"(string)"+rightValue+")";
            }
            else if(balancedType == "graph")
            {
                return "GRGEN_LIBGR.GraphHelper.Equal((GRGEN_LIBGR.IGraph)" + leftValue + ", (GRGEN_LIBGR.IGraph)" + rightValue + ")";
            }
            else if(balancedType.StartsWith("set<"))
            {
                return "GRGEN_LIBGR.ContainerHelper.EqualIDictionary((IDictionary)"+leftValue+", (IDictionary)"+rightValue+")";
            }
            else if(balancedType.StartsWith("map<"))
            {
                return "GRGEN_LIBGR.ContainerHelper.EqualIDictionary((IDictionary)"+leftValue+", (IDictionary)"+rightValue+")";
            }
            else if(balancedType.StartsWith("array<"))
            {
                return "GRGEN_LIBGR.ContainerHelper.EqualIList((IList)"+leftValue+", (IList)"+rightValue+")";
            }
            else if(balancedType.StartsWith("deque<"))
            {
                return "GRGEN_LIBGR.ContainerHelper.EqualDeque((Deque)" + leftValue + ", (Deque)" + rightValue + ")";
            }
            else if(TypesHelper.IsExternalTypeIncludingObjectType(balancedType, model))
            {
                return "graph.Model.IsEqual((object)"+leftValue+", (object)"+rightValue+")";
            }
            else
            {
                return "Object.Equals("+leftValue+", "+rightValue+")";
            }
        }

        public static string NotEqualStatic(string leftValue, string rightValue,
            string balancedType, string leftType, string rightType, IGraphModel model)
        {
            // byte and short are only used for storing, no computations are done with them
            // enums are handled via int, node and edges are handled via reference equality
            if(balancedType == "int")
            {
                return "((int)"+leftValue+" != "+"(int)"+rightValue+")";
            }
            else if(balancedType == "long")
            {
                return "((long)"+leftValue+" != "+"(long)"+rightValue+")";
            }
            else if(balancedType == "float")
            {
                return "((float)"+leftValue+" != "+"(float)"+rightValue+")";
            }
            else if(balancedType == "double")
            {
                return "((double)"+leftValue+" != "+"(double)"+rightValue+")";
            }
            else if(balancedType == "boolean")
            {
                return "((bool)"+leftValue+" != "+"(bool)"+rightValue+")";
            }
            else if(balancedType == "string")
            {
                return "((string)"+leftValue+" != "+"(string)"+rightValue+")";
            }
            else if(balancedType == "graph")
            {
                return "!GRGEN_LIBGR.GraphHelper.Equal((GRGEN_LIBGR.IGraph)" + leftValue + ", (GRGEN_LIBGR.IGraph)" + rightValue + ")";
            }
            else if(balancedType.StartsWith("set<"))
            {
                return "GRGEN_LIBGR.ContainerHelper.NotEqualIDictionary((IDictionary)"+leftValue+", (IDictionary)"+rightValue+")";
            }
            else if(balancedType.StartsWith("map<"))
            {
                return "GRGEN_LIBGR.ContainerHelper.NotEqualIDictionary((IDictionary)"+leftValue+", (IDictionary)"+rightValue+")";
            }
            else if(balancedType.StartsWith("array<"))
            {
                return "GRGEN_LIBGR.ContainerHelper.NotEqualIList((IList)"+leftValue+", (IList)"+rightValue+")";
            }
            else if(balancedType.StartsWith("deque<"))
            {
                return "GRGEN_LIBGR.ContainerHelper.NotEqualDeque((Deque)" + leftValue + ", (Deque)" + rightValue + ")";
            }
            else if(TypesHelper.IsExternalTypeIncludingObjectType(balancedType, model))
            {
                return "!graph.Model.IsEqual((object)" + leftValue + ", (object)" + rightValue + ")";
            }
            else
            {
                return "!Object.Equals("+leftValue+", "+rightValue+")";
            }
        }

        public static string LowerStatic(string leftValue, string rightValue,
            string balancedType, string leftType, string rightType, IGraphModel model)
        {
            // byte and short are only used for storing, no computations are done with them
            // enums are handled via int
            if(balancedType == "int")
            {
                return "((int)"+leftValue+" < "+"(int)"+rightValue+")";
            }
            else if(balancedType == "long")
            {
                return "((long)"+leftValue+" < "+"(long)"+rightValue+")";
            }
            else if(balancedType == "float")
            {
                return "((float)"+leftValue+" < "+"(float)"+rightValue+")";
            }
            else if(balancedType == "double")
            {
                return "((double)"+leftValue+" < "+"(double)"+rightValue+")";
            }
            else if(balancedType.StartsWith("set<"))
            {
                return "GRGEN_LIBGR.ContainerHelper.LessThanIDictionary((IDictionary)"+leftValue+", (IDictionary)"+rightValue+")";
            }
            else if(balancedType.StartsWith("map<"))
            {
                return "GRGEN_LIBGR.ContainerHelper.LessThanIDictionary((IDictionary)"+leftValue+", (IDictionary)"+rightValue+")";
            }
            else if(balancedType.StartsWith("array<"))
            {
                return "GRGEN_LIBGR.ContainerHelper.LessThanIList((IList)"+leftValue+", (IList)"+rightValue+")";
            }
            else if(balancedType.StartsWith("deque<"))
            {
                return "GRGEN_LIBGR.ContainerHelper.LessThanDeque((Deque)" + leftValue + ", (Deque)" + rightValue + ")";
            }
            else if(TypesHelper.IsExternalTypeIncludingObjectType(balancedType, model))
            {
                return "graph.Model.IsLower((object)" + leftValue + ", (object)" + rightValue + ")";
            }

            return null;
        }

        public static string GreaterStatic(string leftValue, string rightValue,
            string balancedType, string leftType, string rightType, IGraphModel model)
        {
            // byte and short are only used for storing, no computations are done with them
            // enums are handled via int
            if(balancedType == "int")
            {
                return "((int)"+leftValue+" > "+"(int)"+rightValue+")";
            }
            else if(balancedType == "long")
            {
                return "((long)"+leftValue+" > "+"(long)"+rightValue+")";
            }
            else if(balancedType == "float")
            {
                return "((float)"+leftValue+" > "+"(float)"+rightValue+")";
            }
            else if(balancedType == "double")
            {
                return "((double)"+leftValue+" > "+"(double)"+rightValue+")";
            }
            else if(balancedType.StartsWith("set<"))
            {
                return "GRGEN_LIBGR.ContainerHelper.GreaterThanIDictionary((IDictionary)"+leftValue+", (IDictionary)"+rightValue+")";
            }
            else if(balancedType.StartsWith("map<"))
            {
                return "GRGEN_LIBGR.ContainerHelper.GreaterThanIDictionary((IDictionary)"+leftValue+", (IDictionary)"+rightValue+")";
            }
            else if(balancedType.StartsWith("array<"))
            {
                return "GRGEN_LIBGR.ContainerHelper.GreaterThanIList((IList)"+leftValue+", (IList)"+rightValue+")";
            }
            else if(balancedType.StartsWith("deque<"))
            {
                return "GRGEN_LIBGR.ContainerHelper.GreaterThanDeque((Deque)" + leftValue + ", (Deque)" + rightValue + ")";
            }
            else if(TypesHelper.IsExternalTypeIncludingObjectType(balancedType, model))
            {
                return "(!graph.Model.IsLower((object)" + leftValue + ", (object)" + rightValue + ")"
                    + " && !graph.Model.IsEqual((object)" + leftValue + ", (object)" + rightValue + "))";
            }

            return null;
        }

        public static string LowerEqualStatic(string leftValue, string rightValue,
            string balancedType, string leftType, string rightType, IGraphModel model)
        {
            // byte and short are only used for storing, no computations are done with them
            // enums are handled via int
            if(balancedType == "int")
            {
                return "((int)"+leftValue+" <= "+"(int)"+rightValue+")";
            }
            else if(balancedType == "long")
            {
                return "((long)"+leftValue+" <= "+"(long)"+rightValue+")";
            }
            else if(balancedType == "float")
            {
                return "((float)"+leftValue+" <= "+"(float)"+rightValue+")";
            }
            else if(balancedType == "double")
            {
                return "((double)"+leftValue+" <= "+"(double)"+rightValue+")";
            }
            else if(balancedType.StartsWith("set<"))
            {
                return "GRGEN_LIBGR.ContainerHelper.LessOrEqualIDictionary((IDictionary)"+leftValue+", (IDictionary)"+rightValue+")";
            }
            else if(balancedType.StartsWith("map<"))
            {
                return "GRGEN_LIBGR.ContainerHelper.LessOrEqualIDictionary((IDictionary)"+leftValue+", (IDictionary)"+rightValue+")";
            }
            else if(balancedType.StartsWith("array<"))
            {
                return "GRGEN_LIBGR.ContainerHelper.LessOrEqualIList((IList)"+leftValue+", (IList)"+rightValue+")";
            }
            else if(balancedType.StartsWith("deque<"))
            {
                return "GRGEN_LIBGR.ContainerHelper.LessOrEqualDeque((Deque)" + leftValue + ", (Deque)" + rightValue + ")";
            }
            else if(TypesHelper.IsExternalTypeIncludingObjectType(balancedType, model))
            {
                return "(graph.Model.IsLower((object)" + leftValue + ", (object)" + rightValue + ")"
                    + " || graph.Model.IsEqual((object)" + leftValue + ", (object)" + rightValue + "))";
            }

            return null;
        }

        public static string GreaterEqualStatic(string leftValue, string rightValue,
            string balancedType, string leftType, string rightType, IGraphModel model)
        {
            // byte and short are only used for storing, no computations are done with them
            // enums are handled via int
            if(balancedType == "int")
            {
                return "((int)"+leftValue+" >= "+"(int)"+rightValue+")";
            }
            else if(balancedType == "long")
            {
                return "((long)"+leftValue+" >= "+"(long)"+rightValue+")";
            }
            else if(balancedType == "float")
            {
                return "((float)"+leftValue+" >= "+"(float)"+rightValue+")";
            }
            else if(balancedType == "double")
            {
                return "((double)"+leftValue+" >= "+"(double)"+rightValue+")";
            }
            else if(balancedType.StartsWith("set<"))
            {
                return "GRGEN_LIBGR.ContainerHelper.GreaterOrEqualIDictionary((IDictionary)"+leftValue+", (IDictionary)"+rightValue+")";
            }
            else if(balancedType.StartsWith("map<"))
            {
                return "GRGEN_LIBGR.ContainerHelper.GreaterOrEqualIDictionary((IDictionary)"+leftValue+", (IDictionary)"+rightValue+")";
            }
            else if(balancedType.StartsWith("array<"))
            {
                return "GRGEN_LIBGR.ContainerHelper.GreaterOrEqualIList((IList)"+leftValue+", (IList)"+rightValue+")";
            }
            else if(balancedType.StartsWith("deque<"))
            {
                return "GRGEN_LIBGR.ContainerHelper.GreaterOrEqualDeque((Deque)" + leftValue + ", (Deque)" + rightValue + ")";
            }
            else if(TypesHelper.IsExternalTypeIncludingObjectType(balancedType, model))
            {
                return "!graph.Model.IsLower((object)" + leftValue + ", (object)" + rightValue + ")";
            }

            return null;
        }

        public static string StructuralEqualStatic(object leftValue, object rightValue)
        {
            return "((GRGEN_LIBGR.IGraph)" + leftValue + ").HasSameStructure((GRGEN_LIBGR.IGraph)" + rightValue + ")";
        }

        public static string PlusStatic(string leftValue, string rightValue,
            string balancedType, string leftType, string rightType, IGraphModel model)
        {
            // byte and short are only used for storing, no computations are done with them
            // enums are handled via int
            if(balancedType == "int")
            {
                return "((int)"+leftValue+" + "+"(int)"+rightValue+")";
            }
            else if(balancedType == "long")
            {
                return "((long)"+leftValue+" + "+"(long)"+rightValue+")";
            }
            else if(balancedType == "float")
            {
                return "((float)"+leftValue+" + "+"(float)"+rightValue+")";
            }
            else if(balancedType == "double")
            {
                return "((double)"+leftValue+" + "+"(double)"+rightValue+")";
            }
            else if(balancedType == "string")
            {
                if(leftType == "string")
                {
                    if(rightType.StartsWith("set<"))
                        return "("+leftValue+" + GRGEN_LIBGR.EmitHelper.ToString("+rightValue+", graph))";
                    else if(rightType.StartsWith("map<"))
                        return "("+leftValue+" + GRGEN_LIBGR.EmitHelper.ToString("+rightValue+", graph))";
                    else if(rightType.StartsWith("array<"))
                        return "("+leftValue+" + GRGEN_LIBGR.EmitHelper.ToString("+rightValue+", graph))";
                    else if(rightType.StartsWith("deque<"))
                        return "(" + leftValue + " + GRGEN_LIBGR.EmitHelper.ToString(" + rightValue + ", graph))";
                    else if(rightType == "string")
                        return "("+leftValue+" + "+rightValue+")";
                    else
                        return "("+leftValue+" + GRGEN_LIBGR.EmitHelper.ToString("+rightValue+", graph))";
                }
                else //rightType == "string"
                {
                    if(leftType.StartsWith("set<"))
                        return "(GRGEN_LIBGR.EmitHelper.ToString("+leftValue+", graph) + "+rightValue+")";
                    else if(leftType.StartsWith("map<"))
                        return "(GRGEN_LIBGR.EmitHelper.ToString("+leftValue+", graph) + "+rightValue+")";
                    else if(leftType.StartsWith("array<"))
                        return "(GRGEN_LIBGR.EmitHelper.ToString("+leftValue+", graph) + "+rightValue+")";
                    else if(leftType.StartsWith("deque<"))
                        return "(GRGEN_LIBGR.EmitHelper.ToString(" + leftValue + ", graph) + " + rightValue + ")";
                    else if(leftType == "string")
                        return "("+leftValue+" + "+rightValue+")";
                    else
                        return "(GRGEN_LIBGR.EmitHelper.ToString("+leftValue+", graph) + "+rightValue+")";
                }
            }
            else if(balancedType.StartsWith("array<"))
            {
                return "GRGEN_LIBGR.ContainerHelper.ConcatenateIList((IList)"+leftValue+", (IList)"+rightValue+")";
            }
            else if(balancedType.StartsWith("deque<"))
            {
                return "GRGEN_LIBGR.ContainerHelper.ConcatenateDeque((Deque)" + leftValue + ", (Deque)" + rightValue + ")";
            }

            return null;
        }

        public static string MinusStatic(string leftValue, string rightValue,
            string balancedType, string leftType, string rightType, IGraphModel model)
        {
            // byte and short are only used for storing, no computations are done with them
            // enums are handled via int
            if(balancedType == "int")
            {
                return "((int)" + leftValue + " - " + "(int)" + rightValue + ")";
            }
            else if(balancedType == "long")
            {
                return "((long)" + leftValue + " - " + "(long)" + rightValue + ")";
            }
            else if(balancedType == "float")
            {
                return "((float)" + leftValue + " - " + "(float)" + rightValue + ")";
            }
            else if(balancedType == "double")
            {
                return "((double)" + leftValue + " - " + "(double)" + rightValue + ")";
            }

            return null;
        }

        public static string MulStatic(string leftValue, string rightValue,
            string balancedType, string leftType, string rightType, IGraphModel model)
        {
            // byte and short are only used for storing, no computations are done with them
            // enums are handled via int
            if(balancedType == "int")
            {
                return "((int)" + leftValue + " * " + "(int)" + rightValue + ")";
            }
            else if(balancedType == "long")
            {
                return "((long)" + leftValue + " * " + "(long)" + rightValue + ")";
            }
            else if(balancedType == "float")
            {
                return "((float)" + leftValue + " * " + "(float)" + rightValue + ")";
            }
            else if(balancedType == "double")
            {
                return "((double)" + leftValue + " * " + "(double)" + rightValue + ")";
            }

            return null;
        }

        public static string DivStatic(string leftValue, string rightValue,
            string balancedType, string leftType, string rightType, IGraphModel model)
        {
            // byte and short are only used for storing, no computations are done with them
            // enums are handled via int
            if(balancedType == "int")
            {
                return "((int)" + leftValue + " / " + "(int)" + rightValue + ")";
            }
            else if(balancedType == "long")
            {
                return "((long)" + leftValue + " / " + "(long)" + rightValue + ")";
            }
            else if(balancedType == "float")
            {
                return "((float)" + leftValue + " / " + "(float)" + rightValue + ")";
            }
            else if(balancedType == "double")
            {
                return "((double)" + leftValue + " / " + "(double)" + rightValue + ")";
            }

            return null;
        }

        public static string ModStatic(string leftValue, string rightValue,
            string balancedType, string leftType, string rightType, IGraphModel model)
        {
            // byte and short are only used for storing, no computations are done with them
            // enums are handled via int
            if(balancedType == "int")
            {
                return "((int)" + leftValue + " % " + "(int)" + rightValue + ")";
            }
            else if(balancedType == "long")
            {
                return "((long)" + leftValue + " % " + "(long)" + rightValue + ")";
            }
            else if(balancedType == "float")
            {
                return "((float)" + leftValue + " % " + "(float)" + rightValue + ")";
            }
            else if(balancedType == "double")
            {
                return "((double)" + leftValue + " % " + "(double)" + rightValue + ")";
            }

            return null;
        }
    }
}

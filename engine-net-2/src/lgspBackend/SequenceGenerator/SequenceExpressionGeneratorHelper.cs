/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

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
                return "((int)"+leftValue+" == "+"(int)"+rightValue+")";
            else if(balancedType == "long")
                return "((long)"+leftValue+" == "+"(long)"+rightValue+")";
            else if(balancedType == "float")
                return "((float)"+leftValue+" == "+"(float)"+rightValue+")";
            else if(balancedType == "double")
                return "((double)"+leftValue+" == "+"(double)"+rightValue+")";
            else if(balancedType == "boolean")
                return "((bool)"+leftValue+" == "+"(bool)"+rightValue+")";
            else if(balancedType == "string")
                return "((string)"+leftValue+" == "+"(string)"+rightValue+")";
            else if(balancedType == "graph")
                return "GRGEN_LIBGR.GraphHelper.Equal((GRGEN_LIBGR.IGraph)" + leftValue + ", (GRGEN_LIBGR.IGraph)" + rightValue + ")";
            else if(balancedType.StartsWith("set<"))
                return "GRGEN_LIBGR.ContainerHelper.EqualIDictionary((IDictionary)"+leftValue+", (IDictionary)"+rightValue+")";
            else if(balancedType.StartsWith("map<"))
                return "GRGEN_LIBGR.ContainerHelper.EqualIDictionary((IDictionary)"+leftValue+", (IDictionary)"+rightValue+")";
            else if(balancedType.StartsWith("array<"))
                return "GRGEN_LIBGR.ContainerHelper.EqualIList((IList)"+leftValue+", (IList)"+rightValue+")";
            else if(balancedType.StartsWith("deque<"))
                return "GRGEN_LIBGR.ContainerHelper.EqualIDeque((GRGEN_LIBGR.IDeque)" + leftValue + ", (GRGEN_LIBGR.IDeque)" + rightValue + ")";
            else if(TypesHelper.IsExternalObjectTypeIncludingObjectType(balancedType, model))
                return "graph.Model.IsEqual((object)"+leftValue+", (object)"+rightValue+")";
            else if(model.ObjectModel.GetType(balancedType) != null)
                return "GRGEN_LIBGR.ContainerHelper.IsEqual((GRGEN_LIBGR.IObject)" + leftValue + ", (GRGEN_LIBGR.IObject)" + rightValue + ")";
            else if(model.TransientObjectModel.GetType(balancedType) != null)
                return "GRGEN_LIBGR.ContainerHelper.IsEqual((GRGEN_LIBGR.ITransientObject)" + leftValue + ", (GRGEN_LIBGR.ITransientObject)" + rightValue + ")";
            else
                return "object.Equals("+leftValue+", "+rightValue+")";
        }

        public static string NotEqualStatic(string leftValue, string rightValue,
            string balancedType, string leftType, string rightType, IGraphModel model)
        {
            // byte and short are only used for storing, no computations are done with them
            // enums are handled via int, node and edges are handled via reference equality
            if(balancedType == "int")
                return "((int)"+leftValue+" != "+"(int)"+rightValue+")";
            else if(balancedType == "long")
                return "((long)"+leftValue+" != "+"(long)"+rightValue+")";
            else if(balancedType == "float")
                return "((float)"+leftValue+" != "+"(float)"+rightValue+")";
            else if(balancedType == "double")
                return "((double)"+leftValue+" != "+"(double)"+rightValue+")";
            else if(balancedType == "boolean")
                return "((bool)"+leftValue+" != "+"(bool)"+rightValue+")";
            else if(balancedType == "string")
                return "((string)"+leftValue+" != "+"(string)"+rightValue+")";
            else if(balancedType == "graph")
                return "!GRGEN_LIBGR.GraphHelper.Equal((GRGEN_LIBGR.IGraph)" + leftValue + ", (GRGEN_LIBGR.IGraph)" + rightValue + ")";
            else if(balancedType.StartsWith("set<"))
                return "GRGEN_LIBGR.ContainerHelper.NotEqualIDictionary((IDictionary)"+leftValue+", (IDictionary)"+rightValue+")";
            else if(balancedType.StartsWith("map<"))
                return "GRGEN_LIBGR.ContainerHelper.NotEqualIDictionary((IDictionary)"+leftValue+", (IDictionary)"+rightValue+")";
            else if(balancedType.StartsWith("array<"))
                return "GRGEN_LIBGR.ContainerHelper.NotEqualIList((IList)"+leftValue+", (IList)"+rightValue+")";
            else if(balancedType.StartsWith("deque<"))
                return "GRGEN_LIBGR.ContainerHelper.NotEqualIDeque((GRGEN_LIBGR.IDeque)" + leftValue + ", (GRGEN_LIBGR.IDeque)" + rightValue + ")";
            else if(TypesHelper.IsExternalObjectTypeIncludingObjectType(balancedType, model))
                return "!graph.Model.IsEqual((object)" + leftValue + ", (object)" + rightValue + ")";
            else if(model.ObjectModel.GetType(balancedType) != null)
                return "!GRGEN_LIBGR.ContainerHelper.IsEqual((GRGEN_LIBGR.IObject)" + leftValue + ", (GRGEN_LIBGR.IObject)" + rightValue + ")";
            else if(model.TransientObjectModel.GetType(balancedType) != null)
                return "!GRGEN_LIBGR.ContainerHelper.IsEqual((GRGEN_LIBGR.ITransientObject)" + leftValue + ", (GRGEN_LIBGR.ITransientObject)" + rightValue + ")";
            else
                return "!object.Equals("+leftValue+", "+rightValue+")";
        }

        public static string StructuralEqualStatic(object leftValue, object rightValue,
            string balancedType, string leftType, string rightType, IGraphModel model)
        {
            // byte and short are only used for storing, no computations are done with them
            // enums are handled via int, node and edges are handled via reference equality
            if(balancedType == "graph")
                return "GRGEN_LIBGR.GraphHelper.HasSameStructure((GRGEN_LIBGR.IGraph)" + leftValue + ", (GRGEN_LIBGR.IGraph)" + rightValue + ")";
            else if(balancedType.StartsWith("set<"))
                return "GRGEN_LIBGR.ContainerHelper.DeeplyEqual((IDictionary)" + leftValue + ", (IDictionary)" + rightValue + ", new Dictionary<object, object>())";
            else if(balancedType.StartsWith("map<"))
                return "GRGEN_LIBGR.ContainerHelper.DeeplyEqual((IDictionary)" + leftValue + ", (IDictionary)" + rightValue + ", new Dictionary<object, object>())";
            else if(balancedType.StartsWith("array<"))
                return "GRGEN_LIBGR.ContainerHelper.DeeplyEqual((IList)" + leftValue + ", (IList)" + rightValue + ", new Dictionary<object, object>())";
            else if(balancedType.StartsWith("deque<"))
                return "GRGEN_LIBGR.ContainerHelper.DeeplyEqual((GRGEN_LIBGR.IDeque)" + leftValue + ", (GRGEN_LIBGR.IDeque)" + rightValue + ", new Dictionary<object, object>())";
            else if(TypesHelper.IsExternalObjectTypeIncludingObjectType(balancedType, model))
                return "graph.Model.IsEqual((object)" + leftValue + ", (object)" + rightValue + ")";
            else
                return "GRGEN_LIBGR.ContainerHelper.DeeplyEqual((GRGEN_LIBGR.IAttributeBearer)" + leftValue + ", (GRGEN_LIBGR.IAttributeBearer)" + rightValue + ", new Dictionary<object, object>())";
        }

        public static string LowerStatic(string leftValue, string rightValue,
            string balancedType, string leftType, string rightType, IGraphModel model)
        {
            // byte and short are only used for storing, no computations are done with them
            // enums are handled via int
            if(balancedType == "int")
                return "((int)"+leftValue+" < "+"(int)"+rightValue+")";
            else if(balancedType == "long")
                return "((long)"+leftValue+" < "+"(long)"+rightValue+")";
            else if(balancedType == "float")
                return "((float)"+leftValue+" < "+"(float)"+rightValue+")";
            else if(balancedType == "double")
                return "((double)"+leftValue+" < "+"(double)"+rightValue+")";
            else if(balancedType.StartsWith("set<"))
                return "GRGEN_LIBGR.ContainerHelper.LessThanIDictionary((IDictionary)"+leftValue+", (IDictionary)"+rightValue+")";
            else if(balancedType.StartsWith("map<"))
                return "GRGEN_LIBGR.ContainerHelper.LessThanIDictionary((IDictionary)"+leftValue+", (IDictionary)"+rightValue+")";
            else if(balancedType.StartsWith("array<"))
                return "GRGEN_LIBGR.ContainerHelper.LessThanIList((IList)"+leftValue+", (IList)"+rightValue+")";
            else if(balancedType.StartsWith("deque<"))
                return "GRGEN_LIBGR.ContainerHelper.LessThanIDeque((GRGEN_LIBGR.IDeque)" + leftValue + ", (GRGEN_LIBGR.IDeque)" + rightValue + ")";
            else if(TypesHelper.IsExternalObjectTypeIncludingObjectType(balancedType, model))
                return "graph.Model.IsLower((object)" + leftValue + ", (object)" + rightValue + ")";

            return null;
        }

        public static string GreaterStatic(string leftValue, string rightValue,
            string balancedType, string leftType, string rightType, IGraphModel model)
        {
            // byte and short are only used for storing, no computations are done with them
            // enums are handled via int
            if(balancedType == "int")
                return "((int)"+leftValue+" > "+"(int)"+rightValue+")";
            else if(balancedType == "long")
                return "((long)"+leftValue+" > "+"(long)"+rightValue+")";
            else if(balancedType == "float")
                return "((float)"+leftValue+" > "+"(float)"+rightValue+")";
            else if(balancedType == "double")
                return "((double)"+leftValue+" > "+"(double)"+rightValue+")";
            else if(balancedType.StartsWith("set<"))
                return "GRGEN_LIBGR.ContainerHelper.GreaterThanIDictionary((IDictionary)"+leftValue+", (IDictionary)"+rightValue+")";
            else if(balancedType.StartsWith("map<"))
                return "GRGEN_LIBGR.ContainerHelper.GreaterThanIDictionary((IDictionary)"+leftValue+", (IDictionary)"+rightValue+")";
            else if(balancedType.StartsWith("array<"))
                return "GRGEN_LIBGR.ContainerHelper.GreaterThanIList((IList)"+leftValue+", (IList)"+rightValue+")";
            else if(balancedType.StartsWith("deque<"))
                return "GRGEN_LIBGR.ContainerHelper.GreaterThanIDeque((GRGEN_LIBGR.IDeque)" + leftValue + ", (GRGEN_LIBGR.IDeque)" + rightValue + ")";
            else if(TypesHelper.IsExternalObjectTypeIncludingObjectType(balancedType, model))
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
                return "((int)"+leftValue+" <= "+"(int)"+rightValue+")";
            else if(balancedType == "long")
                return "((long)"+leftValue+" <= "+"(long)"+rightValue+")";
            else if(balancedType == "float")
                return "((float)"+leftValue+" <= "+"(float)"+rightValue+")";
            else if(balancedType == "double")
                return "((double)"+leftValue+" <= "+"(double)"+rightValue+")";
            else if(balancedType.StartsWith("set<"))
                return "GRGEN_LIBGR.ContainerHelper.LessOrEqualIDictionary((IDictionary)"+leftValue+", (IDictionary)"+rightValue+")";
            else if(balancedType.StartsWith("map<"))
                return "GRGEN_LIBGR.ContainerHelper.LessOrEqualIDictionary((IDictionary)"+leftValue+", (IDictionary)"+rightValue+")";
            else if(balancedType.StartsWith("array<"))
                return "GRGEN_LIBGR.ContainerHelper.LessOrEqualIList((IList)"+leftValue+", (IList)"+rightValue+")";
            else if(balancedType.StartsWith("deque<"))
                return "GRGEN_LIBGR.ContainerHelper.LessOrEqualIDeque((GRGEN_LIBGR.IDeque)" + leftValue + ", (GRGEN_LIBGR.IDeque)" + rightValue + ")";
            else if(TypesHelper.IsExternalObjectTypeIncludingObjectType(balancedType, model))
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
                return "((int)"+leftValue+" >= "+"(int)"+rightValue+")";
            else if(balancedType == "long")
                return "((long)"+leftValue+" >= "+"(long)"+rightValue+")";
            else if(balancedType == "float")
                return "((float)"+leftValue+" >= "+"(float)"+rightValue+")";
            else if(balancedType == "double")
                return "((double)"+leftValue+" >= "+"(double)"+rightValue+")";
            else if(balancedType.StartsWith("set<"))
                return "GRGEN_LIBGR.ContainerHelper.GreaterOrEqualIDictionary((IDictionary)"+leftValue+", (IDictionary)"+rightValue+")";
            else if(balancedType.StartsWith("map<"))
                return "GRGEN_LIBGR.ContainerHelper.GreaterOrEqualIDictionary((IDictionary)"+leftValue+", (IDictionary)"+rightValue+")";
            else if(balancedType.StartsWith("array<"))
                return "GRGEN_LIBGR.ContainerHelper.GreaterOrEqualIList((IList)"+leftValue+", (IList)"+rightValue+")";
            else if(balancedType.StartsWith("deque<"))
                return "GRGEN_LIBGR.ContainerHelper.GreaterOrEqualIDeque((GRGEN_LIBGR.IDeque)" + leftValue + ", (GRGEN_LIBGR.IDeque)" + rightValue + ")";
            else if(TypesHelper.IsExternalObjectTypeIncludingObjectType(balancedType, model))
                return "!graph.Model.IsLower((object)" + leftValue + ", (object)" + rightValue + ")";

            return null;
        }

        public static string ShiftLeftStatic(string leftValue, string rightValue,
            string balancedType, string leftType, string rightType, IGraphModel model)
        {
            // byte and short are only used for storing, no computations are done with them
            // enums are handled via int
            if(balancedType == "int")
                return "((int)" + leftValue + " << " + "(int)" + rightValue + ")";
            else if(balancedType == "long")
                return "((long)" + leftValue + " << " + "(int)" + rightValue + ")";

            return null;
        }

        public static string ShiftRightStatic(string leftValue, string rightValue,
            string balancedType, string leftType, string rightType, IGraphModel model)
        {
            // byte and short are only used for storing, no computations are done with them
            // enums are handled via int
            if(balancedType == "int")
                return "((int)" + leftValue + " >> " + "(int)" + rightValue + ")";
            else if(balancedType == "long")
                return "((long)" + leftValue + " >> " + "(int)" + rightValue + ")";

            return null;
        }

        public static string ShiftRightUnsignedStatic(string leftValue, string rightValue,
            string balancedType, string leftType, string rightType, IGraphModel model)
        {
            // byte and short are only used for storing, no computations are done with them
            // enums are handled via int
            if(balancedType == "int")
                return "(int)((uint)" + leftValue + " >> " + "(int)" + rightValue + ")";
            else if(balancedType == "long")
                return "(long)((ulong)" + leftValue + " >> " + "(int)" + rightValue + ")";

            return null;
        }

        public static string PlusStatic(string leftValue, string rightValue,
            string balancedType, string leftType, string rightType, IGraphModel model)
        {
            // byte and short are only used for storing, no computations are done with them
            // enums are handled via int
            if(balancedType == "int")
                return "((int)"+leftValue+" + "+"(int)"+rightValue+")";
            else if(balancedType == "long")
                return "((long)"+leftValue+" + "+"(long)"+rightValue+")";
            else if(balancedType == "float")
                return "((float)"+leftValue+" + "+"(float)"+rightValue+")";
            else if(balancedType == "double")
                return "((double)"+leftValue+" + "+"(double)"+rightValue+")";
            else if(balancedType == "string")
            {
                if(leftType == "string")
                {
                    if(rightType.StartsWith("set<"))
                        return "("+leftValue+" + GRGEN_LIBGR.EmitHelper.ToString("+rightValue+", graph, false, null, null))";
                    else if(rightType.StartsWith("map<"))
                        return "("+leftValue+" + GRGEN_LIBGR.EmitHelper.ToString("+rightValue+", graph, false, null, null))";
                    else if(rightType.StartsWith("array<"))
                        return "("+leftValue+" + GRGEN_LIBGR.EmitHelper.ToString("+rightValue+", graph, false, null, null))";
                    else if(rightType.StartsWith("deque<"))
                        return "(" + leftValue + " + GRGEN_LIBGR.EmitHelper.ToString(" + rightValue + ", graph, false, null, null))";
                    else if(rightType == "string")
                        return "("+leftValue+" + "+rightValue+")";
                    else
                        return "("+leftValue+" + GRGEN_LIBGR.EmitHelper.ToString("+rightValue+", graph, false, null, null))";
                }
                else //rightType == "string"
                {
                    if(leftType.StartsWith("set<"))
                        return "(GRGEN_LIBGR.EmitHelper.ToString("+leftValue+", graph, false, null, null) + "+rightValue+")";
                    else if(leftType.StartsWith("map<"))
                        return "(GRGEN_LIBGR.EmitHelper.ToString("+leftValue+", graph, false, null, null) + "+rightValue+")";
                    else if(leftType.StartsWith("array<"))
                        return "(GRGEN_LIBGR.EmitHelper.ToString("+leftValue+", graph, false, null, null) + "+rightValue+")";
                    else if(leftType.StartsWith("deque<"))
                        return "(GRGEN_LIBGR.EmitHelper.ToString(" + leftValue + ", graph, false, null, null) + " + rightValue + ")";
                    else if(leftType == "string")
                        return "("+leftValue+" + "+rightValue+")";
                    else
                        return "(GRGEN_LIBGR.EmitHelper.ToString("+leftValue+", graph, false, null, null) + "+rightValue+")";
                }
            }
            else if(balancedType.StartsWith("array<"))
                return "GRGEN_LIBGR.ContainerHelper.ConcatenateIList((IList)"+leftValue+", (IList)"+rightValue+")";
            else if(balancedType.StartsWith("deque<"))
                return "GRGEN_LIBGR.ContainerHelper.ConcatenateIDeque((GRGEN_LIBGR.IDeque)" + leftValue + ", (GRGEN_LIBGR.IDeque)" + rightValue + ")";

            return null;
        }

        public static string UnaryPlusStatic(string operandValue, string balancedType, string operandType, IGraphModel model)
        {
            // byte and short are only used for storing, no computations are done with them
            // enums are handled via int
            if(balancedType == "int")
                return "( + " + "(int)" + operandValue + ")";
            else if(balancedType == "long")
                return "( + " + "(long)" + operandValue + ")";
            else if(balancedType == "float")
                return "( + " + "(float)" + operandValue + ")";
            else if(balancedType == "double")
                return "( + " + "(double)" + operandValue + ")";

            return null;
        }

        public static string UnaryMinusStatic(string operandValue, string balancedType, string operandType, IGraphModel model)
        {
            // byte and short are only used for storing, no computations are done with them
            // enums are handled via int
            if(balancedType == "int")
                return "( - " + "(int)" + operandValue + ")";
            else if(balancedType == "long")
                return "( - " + "(long)" + operandValue + ")";
            else if(balancedType == "float")
                return "( - " + "(float)" + operandValue + ")";
            else if(balancedType == "double")
                return "( - " + "(double)" + operandValue + ")";

            return null;
        }

        public static string BitwiseComplementStatic(string operandValue, string balancedType, string operandType, IGraphModel model)
        {
            // byte and short are only used for storing, no computations are done with them
            // enums are handled via int
            if(balancedType == "int")
                return "( ~ " + "(int)" + operandValue + ")";
            else if(balancedType == "long")
                return "( ~ " + "(long)" + operandValue + ")";

            return null;
        }

        public static string MinusStatic(string leftValue, string rightValue,
            string balancedType, string leftType, string rightType, IGraphModel model)
        {
            // byte and short are only used for storing, no computations are done with them
            // enums are handled via int
            if(balancedType == "int")
                return "((int)" + leftValue + " - " + "(int)" + rightValue + ")";
            else if(balancedType == "long")
                return "((long)" + leftValue + " - " + "(long)" + rightValue + ")";
            else if(balancedType == "float")
                return "((float)" + leftValue + " - " + "(float)" + rightValue + ")";
            else if(balancedType == "double")
                return "((double)" + leftValue + " - " + "(double)" + rightValue + ")";

            return null;
        }

        public static string MulStatic(string leftValue, string rightValue,
            string balancedType, string leftType, string rightType, IGraphModel model)
        {
            // byte and short are only used for storing, no computations are done with them
            // enums are handled via int
            if(balancedType == "int")
                return "((int)" + leftValue + " * " + "(int)" + rightValue + ")";
            else if(balancedType == "long")
                return "((long)" + leftValue + " * " + "(long)" + rightValue + ")";
            else if(balancedType == "float")
                return "((float)" + leftValue + " * " + "(float)" + rightValue + ")";
            else if(balancedType == "double")
                return "((double)" + leftValue + " * " + "(double)" + rightValue + ")";

            return null;
        }

        public static string DivStatic(string leftValue, string rightValue,
            string balancedType, string leftType, string rightType, IGraphModel model)
        {
            // byte and short are only used for storing, no computations are done with them
            // enums are handled via int
            if(balancedType == "int")
                return "((int)" + leftValue + " / " + "(int)" + rightValue + ")";
            else if(balancedType == "long")
                return "((long)" + leftValue + " / " + "(long)" + rightValue + ")";
            else if(balancedType == "float")
                return "((float)" + leftValue + " / " + "(float)" + rightValue + ")";
            else if(balancedType == "double")
                return "((double)" + leftValue + " / " + "(double)" + rightValue + ")";

            return null;
        }

        public static string ModStatic(string leftValue, string rightValue,
            string balancedType, string leftType, string rightType, IGraphModel model)
        {
            // byte and short are only used for storing, no computations are done with them
            // enums are handled via int
            if(balancedType == "int")
                return "((int)" + leftValue + " % " + "(int)" + rightValue + ")";
            else if(balancedType == "long")
                return "((long)" + leftValue + " % " + "(long)" + rightValue + ")";
            else if(balancedType == "float")
                return "((float)" + leftValue + " % " + "(float)" + rightValue + ")";
            else if(balancedType == "double")
                return "((double)" + leftValue + " % " + "(double)" + rightValue + ")";

            return null;
        }

        public static string ExceptStatic(string leftValue, string rightValue,
            string balancedType, string leftType, string rightType, IGraphModel model)
        {
            if(balancedType.StartsWith("set<") || balancedType.StartsWith("map<"))
                return "GRGEN_LIBGR.ContainerHelper.Except((IDictionary)" + leftValue + ", (IDictionary)" + rightValue + ")";

            return null;
        }

        public static string OrStatic(string leftValue, string rightValue,
            string balancedType, string leftType, string rightType, IGraphModel model)
        {
            if(balancedType == "boolean")
                return "((bool)" + leftValue + " | " + "(bool)" + rightValue + ")";
            else if(balancedType == "int")
                return "((int)" + leftValue + " | " + "(int)" + rightValue + ")";
            else if(balancedType == "long")
                return "((long)" + leftValue + " | " + "(long)" + rightValue + ")";
            else if(balancedType.StartsWith("set<") || balancedType.StartsWith("map<"))
                return "GRGEN_LIBGR.ContainerHelper.Union((IDictionary)" + leftValue + ", (IDictionary)" + rightValue + ")";

            return null;
        }

        public static string XorStatic(string leftValue, string rightValue,
            string balancedType, string leftType, string rightType, IGraphModel model)
        {
            if(balancedType == "boolean")
                return "((bool)" + leftValue + " ^ " + "(bool)" + rightValue + ")";
            else if(balancedType == "int")
                return "((int)" + leftValue + " ^ " + "(int)" + rightValue + ")";
            else if(balancedType == "long")
                return "((long)" + leftValue + " ^ " + "(long)" + rightValue + ")";

            return null;
        }

        public static string AndStatic(string leftValue, string rightValue,
            string balancedType, string leftType, string rightType, IGraphModel model)
        {
            if(balancedType == "boolean")
                return "((bool)" + leftValue + " & " + "(bool)" + rightValue + ")";
            else if(balancedType == "int")
                return "((int)" + leftValue + " & " + "(int)" + rightValue + ")";
            else if(balancedType == "long")
                return "((long)" + leftValue + " & " + "(long)" + rightValue + ")";
            else if(balancedType.StartsWith("set<") || balancedType.StartsWith("map<"))
                return "GRGEN_LIBGR.ContainerHelper.Intersect((IDictionary)" + leftValue + ", (IDictionary)" + rightValue + ")";

            return null;
        }
    }
}

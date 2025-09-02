/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// The sequence expression type helper contains code for operator type balancing,
    /// i.e. determining the correct version(/implementation) of an operator.
    /// It is used in type checking, and implementation selection (esp. meaning apropriate casting of the arguments),
    /// at runtime (SequenceExpressionExecutionHelper) as well as compile time (SequenceExpressionGeneratorHelper).
    /// </summary>
    public static class SequenceExpressionTypeHelper
    {
        // implicit casts supported by GrGen:
        // enum -> (integer) number
        // integer number -> larger integer number (note that there are no operators working on byte and short)
        // floating point number -> larger floating point number
        // integer number -> floating point number
        // everything -> string (for convenient emitting)

        // explicit casts supported by GrGen:
        // integer number -> smaller integer number
        // floating point number -> smaller floating point number
        // floating point number -> integer number
        // everything -> object
        // object -> everything (at least in a true OO language this should work, in GrGen.NET the exact meaning of object is not fully defined yet (neither fully covered by tests) (with some influence from C++ not supporting boxing))

        /// <summary>
        /// Returns whether the source operand type can be casted to the specified target type (true if the type is unknown at compile time, in case of incompatible types the cast will then fail at runtime).
        /// </summary>
        public static bool IsCastable(string source, string target, IGraphModel model)
        {
            if(source == "")
                return true;
            if(source == "object") // "static" type resulting from cast to this type, no real value of this type existing at runtime
                return true;
            if(source == target)
                return true;

            if(TypesHelper.IsNumericType(source) && TypesHelper.IsNumericType(target))
                return true;
            if(TypesHelper.IsEnumType(source, model) && TypesHelper.IsNumericType(target))
                return true;

            if(target == "string")
                return true;
            if(target == "object")
                return true;

            if(TypesHelper.GetNodeType(target, model) != null)
            {
                NodeType targetNodeType = TypesHelper.GetNodeType(target, model);
                NodeType sourceNodeType = TypesHelper.GetNodeType(source, model);
                if(sourceNodeType != null)
                {
                    if(targetNodeType.IsA(sourceNodeType) || sourceNodeType.IsA(targetNodeType))
                        return true;
                }
            }
            else if(TypesHelper.GetEdgeType(target, model) != null)
            {
                EdgeType targetEdgeType = TypesHelper.GetEdgeType(target, model);
                EdgeType sourceEdgeType = TypesHelper.GetEdgeType(source, model);
                if(sourceEdgeType != null)
                {
                    if(targetEdgeType.IsA(sourceEdgeType) || sourceEdgeType.IsA(targetEdgeType))
                        return true;
                }
            }
            else if(TypesHelper.GetObjectType(target, model) != null)
            {
                ObjectType targetObjectType = TypesHelper.GetObjectType(target, model);
                ObjectType sourceObjectType = TypesHelper.GetObjectType(source, model);
                if(sourceObjectType != null)
                {
                    if(targetObjectType.IsA(sourceObjectType) || sourceObjectType.IsA(targetObjectType))
                        return true;
                }
            }
            else if(TypesHelper.GetTransientObjectType(target, model) != null)
            {
                TransientObjectType targetTransientObjectType = TypesHelper.GetTransientObjectType(target, model);
                TransientObjectType sourceTransientObjectType = TypesHelper.GetTransientObjectType(source, model);
                if(sourceTransientObjectType != null)
                {
                    if(targetTransientObjectType.IsA(sourceTransientObjectType) || sourceTransientObjectType.IsA(targetTransientObjectType))
                        return true;
                }
            }
            else if(TypesHelper.GetExternalObjectType(target, model) != null) // includes "object", but that one was already handled before
            {
                ExternalObjectType targetExternalObjectType = TypesHelper.GetExternalObjectType(target, model);
                ExternalObjectType sourceExternalObjectType = TypesHelper.GetExternalObjectType(source, model);
                if(sourceExternalObjectType != null)
                {
                    if(targetExternalObjectType.IsA(sourceExternalObjectType) || sourceExternalObjectType.IsA(targetExternalObjectType))
                        return true;
                }
            }
            // TODO: match class, match types of actions -- require access to actions...

            // container handled by source == target, or source == "", i.e. only type equality/invariance or unboxing casts are supported

            return false;
        }

        /// <summary>
        /// Returns the types to which the operands must be casted to, 
        /// for the types of the left and right operands and the operator given.
        /// Used for type checking and casting at compile time.
        /// Returns "" if the type can only be determined at runtime.
        /// Returns "-" in case of a type error.
        /// </summary>
        public static string Balance(SequenceExpressionType op, string left, string right, IGraphModel model)
        {
            string result;

            switch(op)
            {
                case SequenceExpressionType.Equal:
                case SequenceExpressionType.NotEqual:
                    result = BalanceArithmetic(left, right, model);
                    if(result == "") return "";
                    if(result == "-")
                    {
                        result = BalanceString(left, right, model);
                        if(result == "") return "";
                        if(result == "-")
                        {
                            result = BalanceGraphElement(left, right, model);
                            if(result == "") return "";
                            if(result == "-")
                            {
                                result = BalanceExternalObjectType(left, right, model);
                                if(result == "") return "";
                                if(result == "-")
                                {
                                    if(left == right) return left;
                                    else return "-";
                                }
                            }
                        }
                    }
                    return result;

                case SequenceExpressionType.StructuralEqual:
                    result = BalanceStructuralEqual(left, right, model);
                    return result;

                case SequenceExpressionType.Lower:
                case SequenceExpressionType.LowerEqual:
                case SequenceExpressionType.Greater:
                case SequenceExpressionType.GreaterEqual:
                    result = BalanceArithmetic(left, right, model);
                    if(result == "-")
                    {
                        if(left == right && (left.StartsWith("set<") || left.StartsWith("map<")
                            || left.StartsWith("array<") || left.StartsWith("deque<")))
                        {
                            return left;
                        }

                        result = BalanceExternalObjectType(left, right, model);
                        return result;
                    }
                    return result;

                case SequenceExpressionType.ShiftLeft:
                case SequenceExpressionType.ShiftRight:
                case SequenceExpressionType.ShiftRightUnsigned:
                    result = BalanceShift(left, right, model);
                    return result;

                case SequenceExpressionType.Plus:
                    result = BalanceArithmetic(left, right, model);
                    if(result == "") return "";
                    if(result == "-")
                    {
                        result = BalanceString(left, right, model);
                        if(result == "") return "";
                        if(result == "-")
                        {
                            if(left == right && (left.StartsWith("array<") || left.StartsWith("deque<")))
                                return left;
                            else return "-";
                        }
                    }
                    return result;

                case SequenceExpressionType.Minus:
                case SequenceExpressionType.Mul:
                case SequenceExpressionType.Div:
                case SequenceExpressionType.Mod:
                    result = BalanceArithmetic(left, right, model);
                    return result;

                case SequenceExpressionType.Except:
                    if(left == "" || right == "")
                        return "";
                    if(left == right && left.StartsWith("set<"))
                        return left;
                    if(left == right && left.StartsWith("map<"))
                        return left;
                    if(left.StartsWith("map<") && right.StartsWith("set<") && TypesHelper.ExtractSrc(left)==TypesHelper.ExtractSrc(right))
                        return left;
                    return "-";

                case SequenceExpressionType.StrictAnd:
                case SequenceExpressionType.StrictOr:
                    if(left == "" || right == "")
                        return "";
                    if(left == right && left == "boolean")
                        return left;
                    if(left == right && left.StartsWith("set<"))
                        return left;
                    if(left == right && left.StartsWith("map<"))
                        return left;
                    if(left == "byte" || left == "short" || left == "int" || left == "long"
                        || right == "byte" || right == "short" || right == "int" || right == "long")
                        return BalanceBitwise(left, right, model);
                    return "-";

                case SequenceExpressionType.StrictXor:
                    if(left == "" || right == "")
                        return "";
                    if(left == right && left == "boolean")
                        return left;
                    if(left == "byte" || left == "short" || left == "int" || left == "long"
                        || right == "byte" || right == "short" || right == "int" || right == "long")
                        return BalanceBitwise(left, right, model);
                    return "-";

                default:
                    return "";
            }
        }

        public static string Balance(SequenceExpressionType op, string operand, IGraphModel model)
        {
            string result;

            switch(op)
            {
            case SequenceExpressionType.UnaryPlus:
                result = BalanceArithmetic(operand, model);
                return result;

            case SequenceExpressionType.UnaryMinus:
                result = BalanceArithmetic(operand, model);
                return result;

            case SequenceExpressionType.BitwiseComplement:
                result = BalanceBitwise(operand, model);
                return result;

            default:
                return "";
            }
        }

        /// <summary>
        /// Returns the types to which the operands must be casted to, 
        /// assuming an arithmetic operator.
        /// Returns "" if the type can only be determined at runtime.
        /// Returns "-" in case of a type error and/or if no operator working on numbers can be applied.
        /// </summary>
        private static string BalanceShift(string left, string right, IGraphModel model)
        {
            switch(left)
            {
            case "byte":
            case "short":
            case "int":
                switch(right)
                {
                case "byte":
                case "short":
                case "int":
                case "long":
                    return "int";
                case "": return "";
                default:
                    if(TypesHelper.IsEnumType(right, model)) return "int";
                    else return "-";
                }
            case "long":
                switch(right)
                {
                case "byte":
                case "short":
                case "int":
                case "long":
                    return "long";
                case "": return "";
                default:
                    if(TypesHelper.IsEnumType(right, model)) return "long";
                    else return "-";
                }
            case "":
                switch(right)
                {
                case "byte":
                case "short":
                case "int":
                case "long":
                    return "";
                case "": return "";
                default:
                    if(TypesHelper.IsEnumType(right, model)) return "";
                    else return "-";
                }
            default:
                if(TypesHelper.IsEnumType(left, model) && TypesHelper.IsEnumType(right, model)) return "int";
                else return "-";
            }
        }

        /// <summary>
        /// Returns the types to which the operands must be casted to, 
        /// assuming an arithmetic operator.
        /// Returns "" if the type can only be determined at runtime.
        /// Returns "-" in case of a type error and/or if no operator working on numbers can be applied.
        /// </summary>
        private static string BalanceArithmetic(string left, string right, IGraphModel model)
        {
            switch(left)
            {
                case "byte":
                case "short":
                case "int":
                    switch(right)
                    {
                        case "byte": return "int";
                        case "short": return "int";
                        case "int": return "int";
                        case "long": return "long";
                        case "float": return "float";
                        case "double": return "double";
                        case "": return "";
                        default:
                            if(TypesHelper.IsEnumType(right, model)) return "int";
                            else return "-";
                    }
                case "long":
                    switch(right)
                    {
                        case "byte": return "long";
                        case "short": return "long";
                        case "int": return "long";
                        case "long": return "long";
                        case "float": return "float";
                        case "double": return "double";
                        case "": return "";
                        default:
                            if(TypesHelper.IsEnumType(right, model)) return "long";
                            else return "-";
                    }
                case "float":
                    switch(right)
                    {
                        case "byte": return "float";
                        case "short": return "float";
                        case "int": return "float";
                        case "long": return "float";
                        case "float": return "float";
                        case "double": return "double";
                        case "": return "";
                        default:
                            if(TypesHelper.IsEnumType(right, model)) return "float";
                            else return "-";
                    }
                case "double":
                    switch(right)
                    {
                        case "byte": return "double";
                        case "short": return "double";
                        case "int": return "double";
                        case "long": return "double";
                        case "float": return "double";
                        case "double": return "double";
                        case "": return "";
                        default:
                            if(TypesHelper.IsEnumType(right, model)) return "double";
                            else return "-";
                    }
                case "":
                    switch(right)
                    {
                        case "byte": return "";
                        case "short": return "";
                        case "int": return "";
                        case "long": return "";
                        case "float": return "";
                        case "double": return "";
                        case "": return "";
                        default:
                            if(TypesHelper.IsEnumType(right, model)) return "";
                            else return "-";
                    }
                default:
                    if(TypesHelper.IsEnumType(left, model) && TypesHelper.IsEnumType(right, model)) return "int";
                    else return "-";
            }
        }

        /// <summary>
        /// Returns the types to which the operands must be casted to, 
        /// assuming a bitwise operator.
        /// Returns "" if the type can only be determined at runtime.
        /// Returns "-" in case of a type error and/or if no operator working on integers bitwisely can be applied.
        /// </summary>
        private static string BalanceBitwise(string left, string right, IGraphModel model)
        {
            switch(left)
            {
            case "byte":
            case "short":
            case "int":
                switch(right)
                {
                    case "byte": return "int";
                    case "short": return "int";
                    case "int": return "int";
                    case "long": return "long";
                    case "": return "";
                    default:
                        if(TypesHelper.IsEnumType(right, model)) return "int";
                        else return "-";
                }
            case "long":
                switch(right)
                {
                    case "byte": return "long";
                    case "short": return "long";
                    case "int": return "long";
                    case "long": return "long";
                    case "": return "";
                    default:
                        if(TypesHelper.IsEnumType(right, model)) return "long";
                        else return "-";
                }
            case "":
                switch(right)
                {
                    case "byte": return "";
                    case "short": return "";
                    case "int": return "";
                    case "long": return "";
                    case "": return "";
                    default:
                        if(TypesHelper.IsEnumType(right, model)) return "";
                        else return "-";
                }
            default:
                if(TypesHelper.IsEnumType(left, model) && TypesHelper.IsEnumType(right, model)) return "int";
                else return "-";
            }
        }

        private static string BalanceArithmetic(string operand, IGraphModel model)
        {
            switch(operand)
            {
            case "byte":
            case "short":
            case "int":
                return "int";
            case "long":
                return "long";
            case "float":
                return "float";
            case "double":
                return "double";
            case "":
                return "";
            default:
                if(TypesHelper.IsEnumType(operand, model)) return "int";
                else return "-";
            }
        }

        private static string BalanceBitwise(string operand, IGraphModel model)
        {
            switch(operand)
            {
            case "byte":
            case "short":
            case "int":
                return "int";
            case "long":
                return "long";
            case "":
                return "";
            default:
                if(TypesHelper.IsEnumType(operand, model)) return "int";
                else return "-";
            }
        }

        /// <summary>
        /// Returns the types to which the operands must be casted to, 
        /// assuming a string operator.
        /// Returns "" if the type can only be determined at runtime.
        /// Returns "-" in case of a type error and/or if no operator working on strings can be applied.
        /// </summary>
        private static string BalanceString(string left, string right, IGraphModel model)
        {
            if(left == "string" || right == "string")
                return "string";

            if(left == "" || right == "")
                return "";

            return "-";
        }

        /// <summary>
        /// Returns the types to which the operands must be casted to, 
        /// assuming the graph element (in)equality operator.
        /// Returns "" if the type can only be determined at runtime.
        /// Returns "-" in case of a type error and/or if no operator working on strings can be applied.
        /// </summary>
        private static string BalanceGraphElement(string left, string right, IGraphModel model)
        {
            if(left == right)
                return left;

            if(left == "" || right == "")
                return "";

            if(TypesHelper.IsSameOrSubtype(left, right, model) && TypesHelper.GetExternalObjectType(right, model) == null)
                return right;

            if(TypesHelper.IsSameOrSubtype(right, left, model) && TypesHelper.GetExternalObjectType(left, model) == null)
                return left;

            return "-";
        }

        /// <summary>
        /// Returns the types to which the operands must be casted to, 
        /// assuming an external type equality or ordering operator.
        /// Returns "" if the type can only be determined at runtime.
        /// Returns "-" in case of a type error and/or if no operator working on external types can be applied.
        /// </summary>
        private static string BalanceExternalObjectType(string left, string right, IGraphModel model)
        {
            if(left == right)
                return left;

            if(left == "" || right == "")
                return "";

            if(TypesHelper.IsSameOrSubtype(left, right, model) && TypesHelper.GetExternalObjectType(right, model) != null)
                return right;

            if(TypesHelper.IsSameOrSubtype(right, left, model) && TypesHelper.GetExternalObjectType(left, model) != null)
                return left;

            return "-";
        }

        /// <summary>
        /// Returns the types to which the operands must be casted to, 
        /// assuming a structural equality comparison operator.
        /// Returns "" if the type can only be determined at runtime.
        /// Returns "-" in case of a type error.
        /// </summary>
        private static string BalanceStructuralEqual(string left, string right, IGraphModel model)
        {
            if(left == "" || right == "")
                return "";

            if(model.NodeModel.GetType(left) != null && model.NodeModel.GetType(right) != null)
            {
                if(TypesHelper.IsSameOrSubtype(left, right, model))
                    return right;
                if(TypesHelper.IsSameOrSubtype(right, left, model))
                    return left;
            }
            else if(model.EdgeModel.GetType(left) != null && model.EdgeModel.GetType(right) != null)
            {
                if(TypesHelper.IsSameOrSubtype(left, right, model))
                    return right;
                if(TypesHelper.IsSameOrSubtype(right, left, model))
                    return left;
            }
            else if(model.ObjectModel.GetType(left) != null && model.ObjectModel.GetType(right) != null)
            {
                if(TypesHelper.IsSameOrSubtype(left, right, model))
                    return right;
                if(TypesHelper.IsSameOrSubtype(right, left, model))
                    return left;
            }
            else if(model.TransientObjectModel.GetType(left) != null && model.TransientObjectModel.GetType(right) != null)
            {
                if(TypesHelper.IsSameOrSubtype(left, right, model))
                    return right;
                if(TypesHelper.IsSameOrSubtype(right, left, model))
                    return left;
            }

            if(TypesHelper.IsContainerType(left) && TypesHelper.IsContainerType(right))
            {
                if(left == right)
                    return left;
            }

            if(left == "graph" && right == "graph")
                return "graph";

            return BalanceExternalObjectType(left, right, model);
        }
    }
}

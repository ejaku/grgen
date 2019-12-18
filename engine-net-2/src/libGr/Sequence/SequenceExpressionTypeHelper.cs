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

namespace de.unika.ipd.grGen.libGr
{
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


        /// <summary>
        /// Returns the type to which the operand must be casted to,
        /// to satisfy the expected type (i.e. the expected type itself).
        /// Returns "" if the type can only be determined at runtime.
        /// Returns "-" in case of a type error (not allowed cast).
        /// </summary>
        public static string Adjust(string expected, string given, IGraphModel model)
        {
            // todo: enums erst in int, dann in zieltyp casten, an stellen wo nötig

            if(TypesHelper.IsSameOrSubtype(given, expected, model))
                return expected;
            if(expected == "int" && TypesHelper.IsEnumType(given, model)) return "int";
            if(expected == "short" && given == "byte") return "short";
            if(expected == "int" && (given == "byte" || given == "short")) return "int";
            if(expected == "long" && (given == "byte" || given == "short" || given == "int")) return "long";
            if(expected == "float" && (given == "byte" || given == "short" || given == "int" || given == "long")) return "float";
            if(expected == "double" && (given == "byte" || given == "short" || given == "int" || given == "long" || given == "float")) return "double";
            if(expected == "string") return "string";
            return "-";
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
                                result = BalanceExternalType(left, right, model);
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

                        result = BalanceExternalType(left, right, model);
                        return result;
                    }
                    return result;

                case SequenceExpressionType.StructuralEqual:
                    return "graph";

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
        public static string BalanceArithmetic(string left, string right, IGraphModel model)
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
        /// assuming a string operator.
        /// Returns "" if the type can only be determined at runtime.
        /// Returns "-" in case of a type error and/or if no operator working on strings can be applied.
        /// </summary>
        public static string BalanceString(string left, string right, IGraphModel model)
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
        public static string BalanceGraphElement(string left, string right, IGraphModel model)
        {
            if(left == right)
                return left;

            if(left == "" || right == "")
                return "";

            if(TypesHelper.IsSameOrSubtype(left, right, model) && !TypesHelper.IsExternalTypeIncludingObjectType(right, model))
                return right;

            if(TypesHelper.IsSameOrSubtype(right, left, model) && !TypesHelper.IsExternalTypeIncludingObjectType(left, model))
                return left;

            return "-";
        }

        /// <summary>
        /// Returns the types to which the operands must be casted to, 
        /// assuming an external type equality or ordering operator.
        /// Returns "" if the type can only be determined at runtime.
        /// Returns "-" in case of a type error and/or if no operator working on external types can be applied.
        /// </summary>
        public static string BalanceExternalType(string left, string right, IGraphModel model)
        {
            if(left == right)
                return left;

            if(left == "" || right == "")
                return "";

            if(TypesHelper.IsSameOrSubtype(left, right, model) && TypesHelper.IsExternalTypeIncludingObjectType(right, model))
                return right;

            if(TypesHelper.IsSameOrSubtype(right, left, model) && TypesHelper.IsExternalTypeIncludingObjectType(left, model))
                return left;

            return "-";
        }
    }
}

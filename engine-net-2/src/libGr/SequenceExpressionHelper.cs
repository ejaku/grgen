/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Diagnostics;

namespace de.unika.ipd.grGen.libGr
{
    public static class SequenceExpressionHelper
    {
        // the double casting in the following code is needed because a boxed value must be unboxed first
        // with the correct type before it can be casted to the real target type

        public static bool EqualObjects(object leftValue, object rightValue, 
            string balancedType, string leftType, string rightType)
        {
            // byte and short are only used for storing, no computations are done with them
            // enums are handled via int, node and edges are handled via reference equality
            if(balancedType == "int")
            {
                if(leftType == "byte")
                {
                    if(rightType == "byte")
                        return (int)(sbyte)leftValue == (int)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (int)(sbyte)leftValue == (int)(short)rightValue;
                    else if(rightType == "int")
                        return (int)(sbyte)leftValue == (int)(int)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (int)(sbyte)leftValue == (int)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "short")
                {
                    if(rightType == "byte")
                        return (int)(short)leftValue == (int)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (int)(short)leftValue == (int)(short)rightValue;
                    else if(rightType == "int")
                        return (int)(short)leftValue == (int)(int)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (int)(short)leftValue == (int)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "int")
                {
                    if(rightType == "byte")
                        return (int)(int)leftValue == (int)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (int)(int)leftValue == (int)(short)rightValue;
                    else if(rightType == "int")
                        return (int)(int)leftValue == (int)(int)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (int)(int)leftValue == (int)Convert.ToInt32((Enum)rightValue);
                }
                else // TypesHelper.IsEnumType(leftType, model)
                {
                    if(rightType == "byte")
                        return (int)Convert.ToInt32((Enum)leftValue) == (int)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (int)Convert.ToInt32((Enum)leftValue) == (int)(short)rightValue;
                    else if(rightType == "int")
                        return (int)Convert.ToInt32((Enum)leftValue) == (int)(int)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (int)Convert.ToInt32((Enum)leftValue) == (int)Convert.ToInt32((Enum)rightValue);
                }
            }
            else if(balancedType == "long")
            {
                if(leftType == "byte")
                {
                    if(rightType == "byte")
                        return (long)(sbyte)leftValue == (long)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (long)(sbyte)leftValue == (long)(short)rightValue;
                    else if(rightType == "int")
                        return (long)(sbyte)leftValue == (long)(int)rightValue;
                    else if(rightType == "long")
                        return (long)(sbyte)leftValue == (long)(long)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (long)(sbyte)leftValue == (long)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "short")
                {
                    if(rightType == "byte")
                        return (long)(short)leftValue == (long)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (long)(short)leftValue == (long)(short)rightValue;
                    else if(rightType == "int")
                        return (long)(short)leftValue == (long)(int)rightValue;
                    else if(rightType == "long")
                        return (long)(short)leftValue == (long)(long)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (long)(short)leftValue == (long)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "int")
                {
                    if(rightType == "byte")
                        return (long)(int)leftValue == (long)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (long)(int)leftValue == (long)(short)rightValue;
                    else if(rightType == "int")
                        return (long)(int)leftValue == (long)(int)rightValue;
                    else if(rightType == "long")
                        return (long)(int)leftValue == (long)(long)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (long)(int)leftValue == (long)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "long")
                {
                    if(rightType == "byte")
                        return (long)(long)leftValue == (long)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (long)(long)leftValue == (long)(short)rightValue;
                    else if(rightType == "int")
                        return (long)(long)leftValue == (long)(int)rightValue;
                    else if(rightType == "long")
                        return (long)(long)leftValue == (long)(long)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (long)(long)leftValue == (long)Convert.ToInt32((Enum)rightValue);
                }
                else // TypesHelper.IsEnumType(leftType, model)
                {
                    if(rightType == "byte")
                        return (long)Convert.ToInt32((Enum)leftValue) == (long)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (long)Convert.ToInt32((Enum)leftValue) == (long)(short)rightValue;
                    else if(rightType == "int")
                        return (long)Convert.ToInt32((Enum)leftValue) == (long)(int)rightValue;
                    else if(rightType == "long")
                        return (long)Convert.ToInt32((Enum)leftValue) == (long)(long)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (long)Convert.ToInt32((Enum)leftValue) == (long)Convert.ToInt32((Enum)rightValue);
                }
            }
            else if(balancedType == "float")
            {
                if(leftType == "byte")
                {
                    if(rightType == "byte")
                        return (float)(sbyte)leftValue == (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)(sbyte)leftValue == (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)(sbyte)leftValue == (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)(sbyte)leftValue == (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)(sbyte)leftValue == (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)(sbyte)leftValue == (float)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "short")
                {
                    if(rightType == "byte")
                        return (float)(short)leftValue == (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)(short)leftValue == (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)(short)leftValue == (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)(short)leftValue == (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)(short)leftValue == (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)(short)leftValue == (float)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "int")
                {
                    if(rightType == "byte")
                        return (float)(int)leftValue == (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)(int)leftValue == (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)(int)leftValue == (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)(int)leftValue == (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)(int)leftValue == (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)(int)leftValue == (float)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "long")
                {
                    if(rightType == "byte")
                        return (float)(long)leftValue == (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)(long)leftValue == (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)(long)leftValue == (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)(long)leftValue == (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)(long)leftValue == (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)(long)leftValue == (float)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "float")
                {
                    if(rightType == "byte")
                        return (float)(float)leftValue == (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)(float)leftValue == (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)(float)leftValue == (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)(float)leftValue == (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)(float)leftValue == (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)(float)leftValue == (float)Convert.ToInt32((Enum)rightValue);
                }
                else // TypesHelper.IsEnumType(leftType, model)
                {
                    if(rightType == "byte")
                        return (float)Convert.ToInt32((Enum)leftValue) == (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)Convert.ToInt32((Enum)leftValue) == (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)Convert.ToInt32((Enum)leftValue) == (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)Convert.ToInt32((Enum)leftValue) == (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)Convert.ToInt32((Enum)leftValue) == (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)Convert.ToInt32((Enum)leftValue) == (float)Convert.ToInt32((Enum)rightValue);
                }
            }
            else if(balancedType == "double")
            {
                if(leftType == "byte")
                {
                    if(rightType == "byte")
                        return (double)(sbyte)leftValue == (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(sbyte)leftValue == (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(sbyte)leftValue == (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(sbyte)leftValue == (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(sbyte)leftValue == (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(sbyte)leftValue == (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(sbyte)leftValue == (double)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "short")
                {
                    if(rightType == "byte")
                        return (double)(short)leftValue == (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(short)leftValue == (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(short)leftValue == (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(short)leftValue == (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(short)leftValue == (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(short)leftValue == (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(short)leftValue == (double)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "int")
                {
                    if(rightType == "byte")
                        return (double)(int)leftValue == (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(int)leftValue == (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(int)leftValue == (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(int)leftValue == (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(int)leftValue == (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(int)leftValue == (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(int)leftValue == (double)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "long")
                {
                    if(rightType == "byte")
                        return (double)(long)leftValue == (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(long)leftValue == (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(long)leftValue == (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(long)leftValue == (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(long)leftValue == (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(long)leftValue == (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(long)leftValue == (double)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "float")
                {
                    if(rightType == "byte")
                        return (double)(float)leftValue == (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(float)leftValue == (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(float)leftValue == (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(float)leftValue == (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(float)leftValue == (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(float)leftValue == (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(float)leftValue == (double)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "double")
                {
                    if(rightType == "byte")
                        return (double)(double)leftValue == (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(double)leftValue == (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(double)leftValue == (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(double)leftValue == (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(double)leftValue == (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(double)leftValue == (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(double)leftValue == (double)Convert.ToInt32((Enum)rightValue);
                }
                else // TypesHelper.IsEnumType(leftType, model)
                {
                    if(rightType == "byte")
                        return (double)Convert.ToInt32((Enum)leftValue) == (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)Convert.ToInt32((Enum)leftValue) == (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)Convert.ToInt32((Enum)leftValue) == (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)Convert.ToInt32((Enum)leftValue) == (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)Convert.ToInt32((Enum)leftValue) == (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)Convert.ToInt32((Enum)leftValue) == (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)Convert.ToInt32((Enum)leftValue) == (double)Convert.ToInt32((Enum)rightValue);
                }
            }
            else if(balancedType == "boolean")
            {
                return (bool)leftValue == (bool)rightValue;
            }
            else if(balancedType == "string")
            {
                return (string)leftValue == (string)rightValue;
            }
            else if(balancedType == "object")
            {
                return (object)leftValue == (object)rightValue;
            }
            else if(balancedType == "graph")
            {
                return ((IGraph)leftValue).IsIsomorph((IGraph)rightValue);
            }
            else if(balancedType.StartsWith("set<"))
            {
                return DictionaryListHelper.EqualIDictionary((IDictionary)leftValue, (IDictionary)rightValue);
            }
            else if(balancedType.StartsWith("map<"))
            {
                return DictionaryListHelper.EqualIDictionary((IDictionary)leftValue, (IDictionary)rightValue);
            }
            else if(balancedType.StartsWith("array<"))
            {
                return DictionaryListHelper.EqualIList((IList)leftValue, (IList)rightValue);
            }
            else
            {
                return Object.Equals(leftValue, rightValue);
            }
        }

        public static string EqualStatic(string leftValue, string rightValue,
            string balancedType, string leftType, string rightType)
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
            else if(balancedType == "object")
            {
                return "((object)"+leftValue+" == "+"(object)"+rightValue+")";
            }
            else if(balancedType == "graph")
            {
                return "((GRGEN_LIBGR.IGraph)"+leftValue+").IsIsomorph((GRGEN_LIBGR.IGraph)"+rightValue+");";
            }
            else if(balancedType.StartsWith("set<"))
            {
                return "GRGEN_LIBGR.DictionaryListHelper.EqualIDictionary((IDictionary)"+leftValue+", (IDictionary)"+rightValue+")";
            }
            else if(balancedType.StartsWith("map<"))
            {
                return "GRGEN_LIBGR.DictionaryListHelper.EqualIDictionary((IDictionary)"+leftValue+", (IDictionary)"+rightValue+")";
            }
            else if(balancedType.StartsWith("array<"))
            {
                return "GRGEN_LIBGR.DictionaryListHelper.EqualIList((IList)"+leftValue+", (IList)"+rightValue+")";
            }
            else
            {
                return "Object.Equals("+leftValue+", "+rightValue+")";
            }
        }

        public static bool NotEqualObjects(object leftValue, object rightValue,
            string balancedType, string leftType, string rightType)
        {
            // byte and short are only used for storing, no computations are done with them
            // enums are handled via int, node and edges are handled via reference equality
            if(balancedType == "int")
            {
                if(leftType == "byte")
                {
                    if(rightType == "byte")
                        return (int)(sbyte)leftValue != (int)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (int)(sbyte)leftValue != (int)(short)rightValue;
                    else if(rightType == "int")
                        return (int)(sbyte)leftValue != (int)(int)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (int)(sbyte)leftValue != (int)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "short")
                {
                    if(rightType == "byte")
                        return (int)(short)leftValue != (int)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (int)(short)leftValue != (int)(short)rightValue;
                    else if(rightType == "int")
                        return (int)(short)leftValue != (int)(int)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (int)(short)leftValue != (int)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "int")
                {
                    if(rightType == "byte")
                        return (int)(int)leftValue != (int)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (int)(int)leftValue != (int)(short)rightValue;
                    else if(rightType == "int")
                        return (int)(int)leftValue != (int)(int)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (int)(int)leftValue != (int)Convert.ToInt32((Enum)rightValue);
                }
                else // TypesHelper.IsEnumType(leftType, model)
                {
                    if(rightType == "byte")
                        return (int)Convert.ToInt32((Enum)leftValue) != (int)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (int)Convert.ToInt32((Enum)leftValue) != (int)(short)rightValue;
                    else if(rightType == "int")
                        return (int)Convert.ToInt32((Enum)leftValue) != (int)(int)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (int)Convert.ToInt32((Enum)leftValue) != (int)Convert.ToInt32((Enum)rightValue);
                }
            }
            else if(balancedType == "long")
            {
                if(leftType == "byte")
                {
                    if(rightType == "byte")
                        return (long)(sbyte)leftValue != (long)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (long)(sbyte)leftValue != (long)(short)rightValue;
                    else if(rightType == "int")
                        return (long)(sbyte)leftValue != (long)(int)rightValue;
                    else if(rightType == "long")
                        return (long)(sbyte)leftValue != (long)(long)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (long)(sbyte)leftValue != (long)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "short")
                {
                    if(rightType == "byte")
                        return (long)(short)leftValue != (long)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (long)(short)leftValue != (long)(short)rightValue;
                    else if(rightType == "int")
                        return (long)(short)leftValue != (long)(int)rightValue;
                    else if(rightType == "long")
                        return (long)(short)leftValue != (long)(long)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (long)(short)leftValue != (long)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "int")
                {
                    if(rightType == "byte")
                        return (long)(int)leftValue != (long)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (long)(int)leftValue != (long)(short)rightValue;
                    else if(rightType == "int")
                        return (long)(int)leftValue != (long)(int)rightValue;
                    else if(rightType == "long")
                        return (long)(int)leftValue != (long)(long)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (long)(int)leftValue != (long)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "long")
                {
                    if(rightType == "byte")
                        return (long)(long)leftValue != (long)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (long)(long)leftValue != (long)(short)rightValue;
                    else if(rightType == "int")
                        return (long)(long)leftValue != (long)(int)rightValue;
                    else if(rightType == "long")
                        return (long)(long)leftValue != (long)(long)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (long)(long)leftValue != (long)Convert.ToInt32((Enum)rightValue);
                }
                else // TypesHelper.IsEnumType(leftType, model)
                {
                    if(rightType == "byte")
                        return (long)Convert.ToInt32((Enum)leftValue) != (long)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (long)Convert.ToInt32((Enum)leftValue) != (long)(short)rightValue;
                    else if(rightType == "int")
                        return (long)Convert.ToInt32((Enum)leftValue) != (long)(int)rightValue;
                    else if(rightType == "long")
                        return (long)Convert.ToInt32((Enum)leftValue) != (long)(long)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (long)Convert.ToInt32((Enum)leftValue) != (long)Convert.ToInt32((Enum)rightValue);
                }
            }
            else if(balancedType == "float")
            {
                if(leftType == "byte")
                {
                    if(rightType == "byte")
                        return (float)(sbyte)leftValue != (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)(sbyte)leftValue != (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)(sbyte)leftValue != (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)(sbyte)leftValue != (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)(sbyte)leftValue != (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)(sbyte)leftValue != (float)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "short")
                {
                    if(rightType == "byte")
                        return (float)(short)leftValue != (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)(short)leftValue != (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)(short)leftValue != (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)(short)leftValue != (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)(short)leftValue != (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)(short)leftValue != (float)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "int")
                {
                    if(rightType == "byte")
                        return (float)(int)leftValue != (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)(int)leftValue != (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)(int)leftValue != (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)(int)leftValue != (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)(int)leftValue != (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)(int)leftValue != (float)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "long")
                {
                    if(rightType == "byte")
                        return (float)(long)leftValue != (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)(long)leftValue != (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)(long)leftValue != (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)(long)leftValue != (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)(long)leftValue != (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)(long)leftValue != (float)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "float")
                {
                    if(rightType == "byte")
                        return (float)(float)leftValue != (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)(float)leftValue != (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)(float)leftValue != (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)(float)leftValue != (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)(float)leftValue != (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)(float)leftValue != (float)Convert.ToInt32((Enum)rightValue);
                }
                else // TypesHelper.IsEnumType(leftType, model)
                {
                    if(rightType == "byte")
                        return (float)Convert.ToInt32((Enum)leftValue) != (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)Convert.ToInt32((Enum)leftValue) != (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)Convert.ToInt32((Enum)leftValue) != (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)Convert.ToInt32((Enum)leftValue) != (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)Convert.ToInt32((Enum)leftValue) != (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)Convert.ToInt32((Enum)leftValue) != (float)Convert.ToInt32((Enum)rightValue);
                }
            }
            else if(balancedType == "double")
            {
                if(leftType == "byte")
                {
                    if(rightType == "byte")
                        return (double)(sbyte)leftValue != (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(sbyte)leftValue != (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(sbyte)leftValue != (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(sbyte)leftValue != (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(sbyte)leftValue != (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(sbyte)leftValue != (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(sbyte)leftValue != (double)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "short")
                {
                    if(rightType == "byte")
                        return (double)(short)leftValue != (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(short)leftValue != (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(short)leftValue != (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(short)leftValue != (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(short)leftValue != (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(short)leftValue != (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(short)leftValue != (double)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "int")
                {
                    if(rightType == "byte")
                        return (double)(int)leftValue != (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(int)leftValue != (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(int)leftValue != (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(int)leftValue != (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(int)leftValue != (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(int)leftValue != (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(int)leftValue != (double)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "long")
                {
                    if(rightType == "byte")
                        return (double)(long)leftValue != (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(long)leftValue != (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(long)leftValue != (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(long)leftValue != (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(long)leftValue != (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(long)leftValue != (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(long)leftValue != (double)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "float")
                {
                    if(rightType == "byte")
                        return (double)(float)leftValue != (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(float)leftValue != (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(float)leftValue != (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(float)leftValue != (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(float)leftValue != (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(float)leftValue != (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(float)leftValue != (double)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "double")
                {
                    if(rightType == "byte")
                        return (double)(double)leftValue != (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(double)leftValue != (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(double)leftValue != (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(double)leftValue != (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(double)leftValue != (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(double)leftValue != (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(double)leftValue != (double)Convert.ToInt32((Enum)rightValue);
                }
                else // TypesHelper.IsEnumType(leftType, model)
                {
                    if(rightType == "byte")
                        return (double)Convert.ToInt32((Enum)leftValue) != (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)Convert.ToInt32((Enum)leftValue) != (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)Convert.ToInt32((Enum)leftValue) != (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)Convert.ToInt32((Enum)leftValue) != (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)Convert.ToInt32((Enum)leftValue) != (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)Convert.ToInt32((Enum)leftValue) != (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)Convert.ToInt32((Enum)leftValue) != (double)Convert.ToInt32((Enum)rightValue);
                }
            }
            else if(balancedType == "boolean")
            {
                return (bool)leftValue != (bool)rightValue;
            }
            else if(balancedType == "string")
            {
                return (string)leftValue != (string)rightValue;
            }
            else if(balancedType == "object")
            {
                return (object)leftValue != (object)rightValue;
            }
            else if(balancedType == "graph")
            {
                return !((IGraph)leftValue).IsIsomorph((IGraph)rightValue);
            }
            else if(balancedType.StartsWith("set<"))
            {
                return DictionaryListHelper.NotEqualIDictionary((IDictionary)leftValue, (IDictionary)rightValue);
            }
            else if(balancedType.StartsWith("map<"))
            {
                return DictionaryListHelper.NotEqualIDictionary((IDictionary)leftValue, (IDictionary)rightValue);
            }
            else if(balancedType.StartsWith("array<"))
            {
                return DictionaryListHelper.NotEqualIList((IList)leftValue, (IList)rightValue);
            }
            else
            {
                return !Object.Equals(leftValue, rightValue);
            }
        }

        public static string NotEqualStatic(string leftValue, string rightValue,
            string balancedType, string leftType, string rightType)
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
            else if(balancedType == "object")
            {
                return "((object)"+leftValue+" != "+"(object)"+rightValue+")";
            }
            else if(balancedType == "graph")
            {
                return "!((GRGEN_LIBGR.IGraph)"+leftValue+").IsIsomorph((GRGEN_LIBGR.IGraph)"+rightValue+");";
            }
            else if(balancedType.StartsWith("set<"))
            {
                return "GRGEN_LIBGR.DictionaryListHelper.NotEqualIDictionary((IDictionary)"+leftValue+", (IDictionary)"+rightValue+")";
            }
            else if(balancedType.StartsWith("map<"))
            {
                return "GRGEN_LIBGR.DictionaryListHelper.NotEqualIDictionary((IDictionary)"+leftValue+", (IDictionary)"+rightValue+")";
            }
            else if(balancedType.StartsWith("array<"))
            {
                return "GRGEN_LIBGR.DictionaryListHelper.NotEqualIList((IList)"+leftValue+", (IList)"+rightValue+")";
            }
            else
            {
                return "!Object.Equals("+leftValue+", "+rightValue+")";
            }
        }

        public static bool LowerObjects(object leftValue, object rightValue,
            string balancedType, string leftType, string rightType)
        {
            // byte and short are only used for storing, no computations are done with them
            // enums are handled via int
            if(balancedType == "int")
            {
                if(leftType == "byte")
                {
                    if(rightType == "byte")
                        return (int)(sbyte)leftValue < (int)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (int)(sbyte)leftValue < (int)(short)rightValue;
                    else if(rightType == "int")
                        return (int)(sbyte)leftValue < (int)(int)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (int)(sbyte)leftValue < (int)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "short")
                {
                    if(rightType == "byte")
                        return (int)(short)leftValue < (int)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (int)(short)leftValue < (int)(short)rightValue;
                    else if(rightType == "int")
                        return (int)(short)leftValue < (int)(int)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (int)(short)leftValue < (int)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "int")
                {
                    if(rightType == "byte")
                        return (int)(int)leftValue < (int)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (int)(int)leftValue < (int)(short)rightValue;
                    else if(rightType == "int")
                        return (int)(int)leftValue < (int)(int)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (int)(int)leftValue < (int)Convert.ToInt32((Enum)rightValue);
                }
                else // TypesHelper.IsEnumType(leftType, model)
                {
                    if(rightType == "byte")
                        return (int)Convert.ToInt32((Enum)leftValue) < (int)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (int)Convert.ToInt32((Enum)leftValue) < (int)(short)rightValue;
                    else if(rightType == "int")
                        return (int)Convert.ToInt32((Enum)leftValue) < (int)(int)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (int)Convert.ToInt32((Enum)leftValue) < (int)Convert.ToInt32((Enum)rightValue);
                }
            }
            else if(balancedType == "long")
            {
                if(leftType == "byte")
                {
                    if(rightType == "byte")
                        return (long)(sbyte)leftValue < (long)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (long)(sbyte)leftValue < (long)(short)rightValue;
                    else if(rightType == "int")
                        return (long)(sbyte)leftValue < (long)(int)rightValue;
                    else if(rightType == "long")
                        return (long)(sbyte)leftValue < (long)(long)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (long)(sbyte)leftValue < (long)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "short")
                {
                    if(rightType == "byte")
                        return (long)(short)leftValue < (long)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (long)(short)leftValue < (long)(short)rightValue;
                    else if(rightType == "int")
                        return (long)(short)leftValue < (long)(int)rightValue;
                    else if(rightType == "long")
                        return (long)(short)leftValue < (long)(long)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (long)(short)leftValue < (long)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "int")
                {
                    if(rightType == "byte")
                        return (long)(int)leftValue < (long)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (long)(int)leftValue < (long)(short)rightValue;
                    else if(rightType == "int")
                        return (long)(int)leftValue < (long)(int)rightValue;
                    else if(rightType == "long")
                        return (long)(int)leftValue < (long)(long)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (long)(int)leftValue < (long)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "long")
                {
                    if(rightType == "byte")
                        return (long)(long)leftValue < (long)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (long)(long)leftValue < (long)(short)rightValue;
                    else if(rightType == "int")
                        return (long)(long)leftValue < (long)(int)rightValue;
                    else if(rightType == "long")
                        return (long)(long)leftValue < (long)(long)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (long)(long)leftValue < (long)Convert.ToInt32((Enum)rightValue);
                }
                else // TypesHelper.IsEnumType(leftType, model)
                {
                    if(rightType == "byte")
                        return (long)Convert.ToInt32((Enum)leftValue) < (long)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (long)Convert.ToInt32((Enum)leftValue) < (long)(short)rightValue;
                    else if(rightType == "int")
                        return (long)Convert.ToInt32((Enum)leftValue) < (long)(int)rightValue;
                    else if(rightType == "long")
                        return (long)Convert.ToInt32((Enum)leftValue) < (long)(long)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (long)Convert.ToInt32((Enum)leftValue) < (long)Convert.ToInt32((Enum)rightValue);
                }
            }
            else if(balancedType == "float")
            {
                if(leftType == "byte")
                {
                    if(rightType == "byte")
                        return (float)(sbyte)leftValue < (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)(sbyte)leftValue < (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)(sbyte)leftValue < (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)(sbyte)leftValue < (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)(sbyte)leftValue < (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)(sbyte)leftValue < (float)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "short")
                {
                    if(rightType == "byte")
                        return (float)(short)leftValue < (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)(short)leftValue < (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)(short)leftValue < (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)(short)leftValue < (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)(short)leftValue < (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)(short)leftValue < (float)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "int")
                {
                    if(rightType == "byte")
                        return (float)(int)leftValue < (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)(int)leftValue < (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)(int)leftValue < (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)(int)leftValue < (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)(int)leftValue < (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)(int)leftValue < (float)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "long")
                {
                    if(rightType == "byte")
                        return (float)(long)leftValue < (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)(long)leftValue < (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)(long)leftValue < (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)(long)leftValue < (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)(long)leftValue < (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)(long)leftValue < (float)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "float")
                {
                    if(rightType == "byte")
                        return (float)(float)leftValue < (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)(float)leftValue < (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)(float)leftValue < (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)(float)leftValue < (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)(float)leftValue < (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)(float)leftValue < (float)Convert.ToInt32((Enum)rightValue);
                }
                else // TypesHelper.IsEnumType(leftType, model)
                {
                    if(rightType == "byte")
                        return (float)Convert.ToInt32((Enum)leftValue) < (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)Convert.ToInt32((Enum)leftValue) < (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)Convert.ToInt32((Enum)leftValue) < (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)Convert.ToInt32((Enum)leftValue) < (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)Convert.ToInt32((Enum)leftValue) < (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)Convert.ToInt32((Enum)leftValue) < (float)Convert.ToInt32((Enum)rightValue);
                }
            }
            else if(balancedType == "double")
            {
                if(leftType == "byte")
                {
                    if(rightType == "byte")
                        return (double)(sbyte)leftValue < (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(sbyte)leftValue < (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(sbyte)leftValue < (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(sbyte)leftValue < (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(sbyte)leftValue < (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(sbyte)leftValue < (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(sbyte)leftValue < (double)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "short")
                {
                    if(rightType == "byte")
                        return (double)(short)leftValue < (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(short)leftValue < (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(short)leftValue < (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(short)leftValue < (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(short)leftValue < (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(short)leftValue < (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(short)leftValue < (double)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "int")
                {
                    if(rightType == "byte")
                        return (double)(int)leftValue < (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(int)leftValue < (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(int)leftValue < (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(int)leftValue < (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(int)leftValue < (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(int)leftValue < (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(int)leftValue < (double)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "long")
                {
                    if(rightType == "byte")
                        return (double)(long)leftValue < (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(long)leftValue < (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(long)leftValue < (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(long)leftValue < (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(long)leftValue < (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(long)leftValue < (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(long)leftValue < (double)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "float")
                {
                    if(rightType == "byte")
                        return (double)(float)leftValue < (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(float)leftValue < (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(float)leftValue < (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(float)leftValue < (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(float)leftValue < (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(float)leftValue < (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(float)leftValue < (double)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "double")
                {
                    if(rightType == "byte")
                        return (double)(double)leftValue < (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(double)leftValue < (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(double)leftValue < (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(double)leftValue < (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(double)leftValue < (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(double)leftValue < (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(double)leftValue < (double)Convert.ToInt32((Enum)rightValue);
                }
                else // TypesHelper.IsEnumType(leftType, model)
                {
                    if(rightType == "byte")
                        return (double)Convert.ToInt32((Enum)leftValue) < (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)Convert.ToInt32((Enum)leftValue) < (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)Convert.ToInt32((Enum)leftValue) < (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)Convert.ToInt32((Enum)leftValue) < (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)Convert.ToInt32((Enum)leftValue) < (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)Convert.ToInt32((Enum)leftValue) < (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)Convert.ToInt32((Enum)leftValue) < (double)Convert.ToInt32((Enum)rightValue);
                }
            }
            else if(balancedType.StartsWith("set<"))
            {
                return DictionaryListHelper.LessThanIDictionary((IDictionary)leftValue, (IDictionary)rightValue);
            }
            else if(balancedType.StartsWith("map<"))
            {
                return DictionaryListHelper.LessThanIDictionary((IDictionary)leftValue, (IDictionary)rightValue);
            }
            else if(balancedType.StartsWith("array<"))
            {
                return DictionaryListHelper.LessThanIList((IList)leftValue, (IList)rightValue);
            }

            throw new Exception("Invalid types for <");
        }

        public static string LowerStatic(string leftValue, string rightValue,
            string balancedType, string leftType, string rightType)
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
                return "GRGEN_LIBGR.DictionaryListHelper.LessThanIDictionary((IDictionary)"+leftValue+", (IDictionary)"+rightValue+")";
            }
            else if(balancedType.StartsWith("map<"))
            {
                return "GRGEN_LIBGR.DictionaryListHelper.LessThanIDictionary((IDictionary)"+leftValue+", (IDictionary)"+rightValue+")";
            }
            else if(balancedType.StartsWith("array<"))
            {
                return "GRGEN_LIBGR.DictionaryListHelper.LessThanIList((IList)"+leftValue+", (IList)"+rightValue+")";
            }

            return null;
        }

        public static bool GreaterObjects(object leftValue, object rightValue,
            string balancedType, string leftType, string rightType)
        {
            // byte and short are only used for storing, no computations are done with them
            // enums are handled via int
            if(balancedType == "int")
            {
                if(leftType == "byte")
                {
                    if(rightType == "byte")
                        return (int)(sbyte)leftValue > (int)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (int)(sbyte)leftValue > (int)(short)rightValue;
                    else if(rightType == "int")
                        return (int)(sbyte)leftValue > (int)(int)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (int)(sbyte)leftValue > (int)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "short")
                {
                    if(rightType == "byte")
                        return (int)(short)leftValue > (int)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (int)(short)leftValue > (int)(short)rightValue;
                    else if(rightType == "int")
                        return (int)(short)leftValue > (int)(int)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (int)(short)leftValue > (int)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "int")
                {
                    if(rightType == "byte")
                        return (int)(int)leftValue > (int)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (int)(int)leftValue > (int)(short)rightValue;
                    else if(rightType == "int")
                        return (int)(int)leftValue > (int)(int)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (int)(int)leftValue > (int)Convert.ToInt32((Enum)rightValue);
                }
                else // TypesHelper.IsEnumType(leftType, model)
                {
                    if(rightType == "byte")
                        return (int)Convert.ToInt32((Enum)leftValue) > (int)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (int)Convert.ToInt32((Enum)leftValue) > (int)(short)rightValue;
                    else if(rightType == "int")
                        return (int)Convert.ToInt32((Enum)leftValue) > (int)(int)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (int)Convert.ToInt32((Enum)leftValue) > (int)Convert.ToInt32((Enum)rightValue);
                }
            }
            else if(balancedType == "long")
            {
                if(leftType == "byte")
                {
                    if(rightType == "byte")
                        return (long)(sbyte)leftValue > (long)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (long)(sbyte)leftValue > (long)(short)rightValue;
                    else if(rightType == "int")
                        return (long)(sbyte)leftValue > (long)(int)rightValue;
                    else if(rightType == "long")
                        return (long)(sbyte)leftValue > (long)(long)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (long)(sbyte)leftValue > (long)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "short")
                {
                    if(rightType == "byte")
                        return (long)(short)leftValue > (long)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (long)(short)leftValue > (long)(short)rightValue;
                    else if(rightType == "int")
                        return (long)(short)leftValue > (long)(int)rightValue;
                    else if(rightType == "long")
                        return (long)(short)leftValue > (long)(long)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (long)(short)leftValue > (long)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "int")
                {
                    if(rightType == "byte")
                        return (long)(int)leftValue > (long)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (long)(int)leftValue > (long)(short)rightValue;
                    else if(rightType == "int")
                        return (long)(int)leftValue > (long)(int)rightValue;
                    else if(rightType == "long")
                        return (long)(int)leftValue > (long)(long)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (long)(int)leftValue > (long)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "long")
                {
                    if(rightType == "byte")
                        return (long)(long)leftValue > (long)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (long)(long)leftValue > (long)(short)rightValue;
                    else if(rightType == "int")
                        return (long)(long)leftValue > (long)(int)rightValue;
                    else if(rightType == "long")
                        return (long)(long)leftValue > (long)(long)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (long)(long)leftValue > (long)Convert.ToInt32((Enum)rightValue);
                }
                else // TypesHelper.IsEnumType(leftType, model)
                {
                    if(rightType == "byte")
                        return (long)Convert.ToInt32((Enum)leftValue) > (long)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (long)Convert.ToInt32((Enum)leftValue) > (long)(short)rightValue;
                    else if(rightType == "int")
                        return (long)Convert.ToInt32((Enum)leftValue) > (long)(int)rightValue;
                    else if(rightType == "long")
                        return (long)Convert.ToInt32((Enum)leftValue) > (long)(long)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (long)Convert.ToInt32((Enum)leftValue) > (long)Convert.ToInt32((Enum)rightValue);
                }
            }
            else if(balancedType == "float")
            {
                if(leftType == "byte")
                {
                    if(rightType == "byte")
                        return (float)(sbyte)leftValue > (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)(sbyte)leftValue > (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)(sbyte)leftValue > (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)(sbyte)leftValue > (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)(sbyte)leftValue > (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)(sbyte)leftValue > (float)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "short")
                {
                    if(rightType == "byte")
                        return (float)(short)leftValue > (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)(short)leftValue > (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)(short)leftValue > (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)(short)leftValue > (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)(short)leftValue > (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)(short)leftValue > (float)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "int")
                {
                    if(rightType == "byte")
                        return (float)(int)leftValue > (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)(int)leftValue > (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)(int)leftValue > (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)(int)leftValue > (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)(int)leftValue > (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)(int)leftValue > (float)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "long")
                {
                    if(rightType == "byte")
                        return (float)(long)leftValue > (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)(long)leftValue > (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)(long)leftValue > (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)(long)leftValue > (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)(long)leftValue > (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)(long)leftValue > (float)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "float")
                {
                    if(rightType == "byte")
                        return (float)(float)leftValue > (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)(float)leftValue > (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)(float)leftValue > (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)(float)leftValue > (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)(float)leftValue > (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)(float)leftValue > (float)Convert.ToInt32((Enum)rightValue);
                }
                else // TypesHelper.IsEnumType(leftType, model)
                {
                    if(rightType == "byte")
                        return (float)Convert.ToInt32((Enum)leftValue) > (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)Convert.ToInt32((Enum)leftValue) > (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)Convert.ToInt32((Enum)leftValue) > (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)Convert.ToInt32((Enum)leftValue) > (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)Convert.ToInt32((Enum)leftValue) > (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)Convert.ToInt32((Enum)leftValue) > (float)Convert.ToInt32((Enum)rightValue);
                }
            }
            else if(balancedType == "double")
            {
                if(leftType == "byte")
                {
                    if(rightType == "byte")
                        return (double)(sbyte)leftValue > (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(sbyte)leftValue > (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(sbyte)leftValue > (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(sbyte)leftValue > (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(sbyte)leftValue > (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(sbyte)leftValue > (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(sbyte)leftValue > (double)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "short")
                {
                    if(rightType == "byte")
                        return (double)(short)leftValue > (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(short)leftValue > (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(short)leftValue > (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(short)leftValue > (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(short)leftValue > (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(short)leftValue > (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(short)leftValue > (double)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "int")
                {
                    if(rightType == "byte")
                        return (double)(int)leftValue > (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(int)leftValue > (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(int)leftValue > (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(int)leftValue > (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(int)leftValue > (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(int)leftValue > (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(int)leftValue > (double)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "long")
                {
                    if(rightType == "byte")
                        return (double)(long)leftValue > (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(long)leftValue > (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(long)leftValue > (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(long)leftValue > (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(long)leftValue > (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(long)leftValue > (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(long)leftValue > (double)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "float")
                {
                    if(rightType == "byte")
                        return (double)(float)leftValue > (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(float)leftValue > (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(float)leftValue > (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(float)leftValue > (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(float)leftValue > (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(float)leftValue > (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(float)leftValue > (double)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "double")
                {
                    if(rightType == "byte")
                        return (double)(double)leftValue > (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(double)leftValue > (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(double)leftValue > (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(double)leftValue > (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(double)leftValue > (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(double)leftValue > (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(double)leftValue > (double)Convert.ToInt32((Enum)rightValue);
                }
                else // TypesHelper.IsEnumType(leftType, model)
                {
                    if(rightType == "byte")
                        return (double)Convert.ToInt32((Enum)leftValue) > (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)Convert.ToInt32((Enum)leftValue) > (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)Convert.ToInt32((Enum)leftValue) > (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)Convert.ToInt32((Enum)leftValue) > (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)Convert.ToInt32((Enum)leftValue) > (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)Convert.ToInt32((Enum)leftValue) > (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)Convert.ToInt32((Enum)leftValue) > (double)Convert.ToInt32((Enum)rightValue);
                }
            }
            else if(balancedType.StartsWith("set<"))
            {
                return DictionaryListHelper.GreaterThanIDictionary((IDictionary)leftValue, (IDictionary)rightValue);
            }
            else if(balancedType.StartsWith("map<"))
            {
                return DictionaryListHelper.GreaterThanIDictionary((IDictionary)leftValue, (IDictionary)rightValue);
            }
            else if(balancedType.StartsWith("array<"))
            {
                return DictionaryListHelper.GreaterThanIList((IList)leftValue, (IList)rightValue);
            }

            throw new Exception("Invalid types for >");
        }

        public static string GreaterStatic(string leftValue, string rightValue,
            string balancedType, string leftType, string rightType)
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
                return "GRGEN_LIBGR.DictionaryListHelper.GreaterThanIDictionary((IDictionary)"+leftValue+", (IDictionary)"+rightValue+")";
            }
            else if(balancedType.StartsWith("map<"))
            {
                return "GRGEN_LIBGR.DictionaryListHelper.GreaterThanIDictionary((IDictionary)"+leftValue+", (IDictionary)"+rightValue+")";
            }
            else if(balancedType.StartsWith("array<"))
            {
                return "GRGEN_LIBGR.DictionaryListHelper.GreaterThanIList((IList)"+leftValue+", (IList)"+rightValue+")";
            }

            return null;
        }

        public static bool LowerEqualObjects(object leftValue, object rightValue,
            string balancedType, string leftType, string rightType)
        {
            // byte and short are only used for storing, no computations are done with them
            // enums are handled via int
            if(balancedType == "int")
            {
                if(leftType == "byte")
                {
                    if(rightType == "byte")
                        return (int)(sbyte)leftValue <= (int)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (int)(sbyte)leftValue <= (int)(short)rightValue;
                    else if(rightType == "int")
                        return (int)(sbyte)leftValue <= (int)(int)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (int)(sbyte)leftValue <= (int)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "short")
                {
                    if(rightType == "byte")
                        return (int)(short)leftValue <= (int)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (int)(short)leftValue <= (int)(short)rightValue;
                    else if(rightType == "int")
                        return (int)(short)leftValue <= (int)(int)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (int)(short)leftValue <= (int)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "int")
                {
                    if(rightType == "byte")
                        return (int)(int)leftValue <= (int)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (int)(int)leftValue <= (int)(short)rightValue;
                    else if(rightType == "int")
                        return (int)(int)leftValue <= (int)(int)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (int)(int)leftValue <= (int)Convert.ToInt32((Enum)rightValue);
                }
                else // TypesHelper.IsEnumType(leftType, model)
                {
                    if(rightType == "byte")
                        return (int)Convert.ToInt32((Enum)leftValue) <= (int)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (int)Convert.ToInt32((Enum)leftValue) <= (int)(short)rightValue;
                    else if(rightType == "int")
                        return (int)Convert.ToInt32((Enum)leftValue) <= (int)(int)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (int)Convert.ToInt32((Enum)leftValue) <= (int)Convert.ToInt32((Enum)rightValue);
                }
            }
            else if(balancedType == "long")
            {
                if(leftType == "byte")
                {
                    if(rightType == "byte")
                        return (long)(sbyte)leftValue <= (long)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (long)(sbyte)leftValue <= (long)(short)rightValue;
                    else if(rightType == "int")
                        return (long)(sbyte)leftValue <= (long)(int)rightValue;
                    else if(rightType == "long")
                        return (long)(sbyte)leftValue <= (long)(long)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (long)(sbyte)leftValue <= (long)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "short")
                {
                    if(rightType == "byte")
                        return (long)(short)leftValue <= (long)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (long)(short)leftValue <= (long)(short)rightValue;
                    else if(rightType == "int")
                        return (long)(short)leftValue <= (long)(int)rightValue;
                    else if(rightType == "long")
                        return (long)(short)leftValue <= (long)(long)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (long)(short)leftValue <= (long)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "int")
                {
                    if(rightType == "byte")
                        return (long)(int)leftValue <= (long)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (long)(int)leftValue <= (long)(short)rightValue;
                    else if(rightType == "int")
                        return (long)(int)leftValue <= (long)(int)rightValue;
                    else if(rightType == "long")
                        return (long)(int)leftValue <= (long)(long)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (long)(int)leftValue <= (long)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "long")
                {
                    if(rightType == "byte")
                        return (long)(long)leftValue <= (long)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (long)(long)leftValue <= (long)(short)rightValue;
                    else if(rightType == "int")
                        return (long)(long)leftValue <= (long)(int)rightValue;
                    else if(rightType == "long")
                        return (long)(long)leftValue <= (long)(long)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (long)(long)leftValue <= (long)Convert.ToInt32((Enum)rightValue);
                }
                else // TypesHelper.IsEnumType(leftType, model)
                {
                    if(rightType == "byte")
                        return (long)Convert.ToInt32((Enum)leftValue) <= (long)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (long)Convert.ToInt32((Enum)leftValue) <= (long)(short)rightValue;
                    else if(rightType == "int")
                        return (long)Convert.ToInt32((Enum)leftValue) <= (long)(int)rightValue;
                    else if(rightType == "long")
                        return (long)Convert.ToInt32((Enum)leftValue) <= (long)(long)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (long)Convert.ToInt32((Enum)leftValue) <= (long)Convert.ToInt32((Enum)rightValue);
                }
            }
            else if(balancedType == "float")
            {
                if(leftType == "byte")
                {
                    if(rightType == "byte")
                        return (float)(sbyte)leftValue <= (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)(sbyte)leftValue <= (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)(sbyte)leftValue <= (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)(sbyte)leftValue <= (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)(sbyte)leftValue <= (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)(sbyte)leftValue <= (float)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "short")
                {
                    if(rightType == "byte")
                        return (float)(short)leftValue <= (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)(short)leftValue <= (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)(short)leftValue <= (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)(short)leftValue <= (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)(short)leftValue <= (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)(short)leftValue <= (float)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "int")
                {
                    if(rightType == "byte")
                        return (float)(int)leftValue <= (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)(int)leftValue <= (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)(int)leftValue <= (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)(int)leftValue <= (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)(int)leftValue <= (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)(int)leftValue <= (float)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "long")
                {
                    if(rightType == "byte")
                        return (float)(long)leftValue <= (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)(long)leftValue <= (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)(long)leftValue <= (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)(long)leftValue <= (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)(long)leftValue <= (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)(long)leftValue <= (float)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "float")
                {
                    if(rightType == "byte")
                        return (float)(float)leftValue <= (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)(float)leftValue <= (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)(float)leftValue <= (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)(float)leftValue <= (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)(float)leftValue <= (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)(float)leftValue <= (float)Convert.ToInt32((Enum)rightValue);
                }
                else // TypesHelper.IsEnumType(leftType, model)
                {
                    if(rightType == "byte")
                        return (float)Convert.ToInt32((Enum)leftValue) <= (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)Convert.ToInt32((Enum)leftValue) <= (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)Convert.ToInt32((Enum)leftValue) <= (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)Convert.ToInt32((Enum)leftValue) <= (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)Convert.ToInt32((Enum)leftValue) <= (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)Convert.ToInt32((Enum)leftValue) <= (float)Convert.ToInt32((Enum)rightValue);
                }
            }
            else if(balancedType == "double")
            {
                if(leftType == "byte")
                {
                    if(rightType == "byte")
                        return (double)(sbyte)leftValue <= (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(sbyte)leftValue <= (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(sbyte)leftValue <= (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(sbyte)leftValue <= (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(sbyte)leftValue <= (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(sbyte)leftValue <= (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(sbyte)leftValue <= (double)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "short")
                {
                    if(rightType == "byte")
                        return (double)(short)leftValue <= (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(short)leftValue <= (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(short)leftValue <= (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(short)leftValue <= (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(short)leftValue <= (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(short)leftValue <= (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(short)leftValue <= (double)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "int")
                {
                    if(rightType == "byte")
                        return (double)(int)leftValue <= (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(int)leftValue <= (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(int)leftValue <= (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(int)leftValue <= (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(int)leftValue <= (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(int)leftValue <= (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(int)leftValue <= (double)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "long")
                {
                    if(rightType == "byte")
                        return (double)(long)leftValue <= (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(long)leftValue <= (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(long)leftValue <= (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(long)leftValue <= (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(long)leftValue <= (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(long)leftValue <= (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(long)leftValue <= (double)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "float")
                {
                    if(rightType == "byte")
                        return (double)(float)leftValue <= (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(float)leftValue <= (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(float)leftValue <= (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(float)leftValue <= (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(float)leftValue <= (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(float)leftValue <= (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(float)leftValue <= (double)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "double")
                {
                    if(rightType == "byte")
                        return (double)(double)leftValue <= (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(double)leftValue <= (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(double)leftValue <= (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(double)leftValue <= (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(double)leftValue <= (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(double)leftValue <= (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(double)leftValue <= (double)Convert.ToInt32((Enum)rightValue);
                }
                else // TypesHelper.IsEnumType(leftType, model)
                {
                    if(rightType == "byte")
                        return (double)Convert.ToInt32((Enum)leftValue) <= (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)Convert.ToInt32((Enum)leftValue) <= (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)Convert.ToInt32((Enum)leftValue) <= (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)Convert.ToInt32((Enum)leftValue) <= (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)Convert.ToInt32((Enum)leftValue) <= (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)Convert.ToInt32((Enum)leftValue) <= (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)Convert.ToInt32((Enum)leftValue) <= (double)Convert.ToInt32((Enum)rightValue);
                }
            }
            else if(balancedType.StartsWith("set<"))
            {
                return DictionaryListHelper.LessOrEqualIDictionary((IDictionary)leftValue, (IDictionary)rightValue);
            }
            else if(balancedType.StartsWith("map<"))
            {
                return DictionaryListHelper.LessOrEqualIDictionary((IDictionary)leftValue, (IDictionary)rightValue);
            }
            else if(balancedType.StartsWith("array<"))
            {
                return DictionaryListHelper.LessOrEqualIList((IList)leftValue, (IList)rightValue);
            }

            throw new Exception("Invalid types for <=");
        }

        public static string LowerEqualStatic(string leftValue, string rightValue,
            string balancedType, string leftType, string rightType)
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
                return "GRGEN_LIBGR.DictionaryListHelper.LessOrEqualIDictionary((IDictionary)"+leftValue+", (IDictionary)"+rightValue+")";
            }
            else if(balancedType.StartsWith("map<"))
            {
                return "GRGEN_LIBGR.DictionaryListHelper.LessOrEqualIDictionary((IDictionary)"+leftValue+", (IDictionary)"+rightValue+")";
            }
            else if(balancedType.StartsWith("array<"))
            {
                return "GRGEN_LIBGR.DictionaryListHelper.LessOrEqualIList((IList)"+leftValue+", (IList)"+rightValue+")";
            }

            return null;
        }

        public static bool GreaterEqualObjects(object leftValue, object rightValue,
            string balancedType, string leftType, string rightType)
        {
            // byte and short are only used for storing, no computations are done with them
            // enums are handled via int
            if(balancedType == "int")
            {
                if(leftType == "byte")
                {
                    if(rightType == "byte")
                        return (int)(sbyte)leftValue >= (int)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (int)(sbyte)leftValue >= (int)(short)rightValue;
                    else if(rightType == "int")
                        return (int)(sbyte)leftValue >= (int)(int)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (int)(sbyte)leftValue >= (int)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "short")
                {
                    if(rightType == "byte")
                        return (int)(short)leftValue >= (int)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (int)(short)leftValue >= (int)(short)rightValue;
                    else if(rightType == "int")
                        return (int)(short)leftValue >= (int)(int)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (int)(short)leftValue >= (int)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "int")
                {
                    if(rightType == "byte")
                        return (int)(int)leftValue >= (int)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (int)(int)leftValue >= (int)(short)rightValue;
                    else if(rightType == "int")
                        return (int)(int)leftValue >= (int)(int)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (int)(int)leftValue >= (int)Convert.ToInt32((Enum)rightValue);
                }
                else // TypesHelper.IsEnumType(leftType, model)
                {
                    if(rightType == "byte")
                        return (int)Convert.ToInt32((Enum)leftValue) >= (int)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (int)Convert.ToInt32((Enum)leftValue) >= (int)(short)rightValue;
                    else if(rightType == "int")
                        return (int)Convert.ToInt32((Enum)leftValue) >= (int)(int)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (int)Convert.ToInt32((Enum)leftValue) >= (int)Convert.ToInt32((Enum)rightValue);
                }
            }
            else if(balancedType == "long")
            {
                if(leftType == "byte")
                {
                    if(rightType == "byte")
                        return (long)(sbyte)leftValue >= (long)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (long)(sbyte)leftValue >= (long)(short)rightValue;
                    else if(rightType == "int")
                        return (long)(sbyte)leftValue >= (long)(int)rightValue;
                    else if(rightType == "long")
                        return (long)(sbyte)leftValue >= (long)(long)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (long)(sbyte)leftValue >= (long)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "short")
                {
                    if(rightType == "byte")
                        return (long)(short)leftValue >= (long)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (long)(short)leftValue >= (long)(short)rightValue;
                    else if(rightType == "int")
                        return (long)(short)leftValue >= (long)(int)rightValue;
                    else if(rightType == "long")
                        return (long)(short)leftValue >= (long)(long)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (long)(short)leftValue >= (long)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "int")
                {
                    if(rightType == "byte")
                        return (long)(int)leftValue >= (long)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (long)(int)leftValue >= (long)(short)rightValue;
                    else if(rightType == "int")
                        return (long)(int)leftValue >= (long)(int)rightValue;
                    else if(rightType == "long")
                        return (long)(int)leftValue >= (long)(long)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (long)(int)leftValue >= (long)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "long")
                {
                    if(rightType == "byte")
                        return (long)(long)leftValue >= (long)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (long)(long)leftValue >= (long)(short)rightValue;
                    else if(rightType == "int")
                        return (long)(long)leftValue >= (long)(int)rightValue;
                    else if(rightType == "long")
                        return (long)(long)leftValue >= (long)(long)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (long)(long)leftValue >= (long)Convert.ToInt32((Enum)rightValue);
                }
                else // TypesHelper.IsEnumType(leftType, model)
                {
                    if(rightType == "byte")
                        return (long)Convert.ToInt32((Enum)leftValue) >= (long)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (long)Convert.ToInt32((Enum)leftValue) >= (long)(short)rightValue;
                    else if(rightType == "int")
                        return (long)Convert.ToInt32((Enum)leftValue) >= (long)(int)rightValue;
                    else if(rightType == "long")
                        return (long)Convert.ToInt32((Enum)leftValue) >= (long)(long)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (long)Convert.ToInt32((Enum)leftValue) >= (long)Convert.ToInt32((Enum)rightValue);
                }
            }
            else if(balancedType == "float")
            {
                if(leftType == "byte")
                {
                    if(rightType == "byte")
                        return (float)(sbyte)leftValue >= (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)(sbyte)leftValue >= (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)(sbyte)leftValue >= (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)(sbyte)leftValue >= (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)(sbyte)leftValue >= (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)(sbyte)leftValue >= (float)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "short")
                {
                    if(rightType == "byte")
                        return (float)(short)leftValue >= (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)(short)leftValue >= (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)(short)leftValue >= (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)(short)leftValue >= (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)(short)leftValue >= (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)(short)leftValue >= (float)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "int")
                {
                    if(rightType == "byte")
                        return (float)(int)leftValue >= (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)(int)leftValue >= (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)(int)leftValue >= (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)(int)leftValue >= (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)(int)leftValue >= (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)(int)leftValue >= (float)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "long")
                {
                    if(rightType == "byte")
                        return (float)(long)leftValue >= (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)(long)leftValue >= (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)(long)leftValue >= (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)(long)leftValue >= (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)(long)leftValue >= (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)(long)leftValue >= (float)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "float")
                {
                    if(rightType == "byte")
                        return (float)(float)leftValue >= (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)(float)leftValue >= (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)(float)leftValue >= (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)(float)leftValue >= (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)(float)leftValue >= (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)(float)leftValue >= (float)Convert.ToInt32((Enum)rightValue);
                }
                else // TypesHelper.IsEnumType(leftType, model)
                {
                    if(rightType == "byte")
                        return (float)Convert.ToInt32((Enum)leftValue) >= (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)Convert.ToInt32((Enum)leftValue) >= (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)Convert.ToInt32((Enum)leftValue) >= (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)Convert.ToInt32((Enum)leftValue) >= (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)Convert.ToInt32((Enum)leftValue) >= (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)Convert.ToInt32((Enum)leftValue) >= (float)Convert.ToInt32((Enum)rightValue);
                }
            }
            else if(balancedType == "double")
            {
                if(leftType == "byte")
                {
                    if(rightType == "byte")
                        return (double)(sbyte)leftValue >= (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(sbyte)leftValue >= (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(sbyte)leftValue >= (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(sbyte)leftValue >= (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(sbyte)leftValue >= (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(sbyte)leftValue >= (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(sbyte)leftValue >= (double)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "short")
                {
                    if(rightType == "byte")
                        return (double)(short)leftValue >= (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(short)leftValue >= (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(short)leftValue >= (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(short)leftValue >= (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(short)leftValue >= (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(short)leftValue >= (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(short)leftValue >= (double)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "int")
                {
                    if(rightType == "byte")
                        return (double)(int)leftValue >= (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(int)leftValue >= (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(int)leftValue >= (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(int)leftValue >= (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(int)leftValue >= (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(int)leftValue >= (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(int)leftValue >= (double)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "long")
                {
                    if(rightType == "byte")
                        return (double)(long)leftValue >= (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(long)leftValue >= (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(long)leftValue >= (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(long)leftValue >= (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(long)leftValue >= (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(long)leftValue >= (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(long)leftValue >= (double)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "float")
                {
                    if(rightType == "byte")
                        return (double)(float)leftValue >= (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(float)leftValue >= (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(float)leftValue >= (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(float)leftValue >= (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(float)leftValue >= (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(float)leftValue >= (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(float)leftValue >= (double)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "double")
                {
                    if(rightType == "byte")
                        return (double)(double)leftValue >= (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(double)leftValue >= (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(double)leftValue >= (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(double)leftValue >= (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(double)leftValue >= (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(double)leftValue >= (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(double)leftValue >= (double)Convert.ToInt32((Enum)rightValue);
                }
                else // TypesHelper.IsEnumType(leftType, model)
                {
                    if(rightType == "byte")
                        return (double)Convert.ToInt32((Enum)leftValue) >= (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)Convert.ToInt32((Enum)leftValue) >= (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)Convert.ToInt32((Enum)leftValue) >= (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)Convert.ToInt32((Enum)leftValue) >= (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)Convert.ToInt32((Enum)leftValue) >= (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)Convert.ToInt32((Enum)leftValue) >= (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)Convert.ToInt32((Enum)leftValue) >= (double)Convert.ToInt32((Enum)rightValue);
                }
            }
            else if(balancedType.StartsWith("set<"))
            {
                return DictionaryListHelper.GreaterOrEqualIDictionary((IDictionary)leftValue, (IDictionary)rightValue);
            }
            else if(balancedType.StartsWith("map<"))
            {
                return DictionaryListHelper.GreaterOrEqualIDictionary((IDictionary)leftValue, (IDictionary)rightValue);
            }
            else if(balancedType.StartsWith("array<"))
            {
                return DictionaryListHelper.GreaterOrEqualIList((IList)leftValue, (IList)rightValue);
            }

            throw new Exception("Invalid types for >=");
        }

        public static string GreaterEqualStatic(string leftValue, string rightValue,
            string balancedType, string leftType, string rightType)
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
                return "GRGEN_LIBGR.DictionaryListHelper.GreaterOrEqualIDictionary((IDictionary)"+leftValue+", (IDictionary)"+rightValue+")";
            }
            else if(balancedType.StartsWith("map<"))
            {
                return "GRGEN_LIBGR.DictionaryListHelper.GreaterOrEqualIDictionary((IDictionary)"+leftValue+", (IDictionary)"+rightValue+")";
            }
            else if(balancedType.StartsWith("array<"))
            {
                return "GRGEN_LIBGR.DictionaryListHelper.GreaterOrEqualIList((IList)"+leftValue+", (IList)"+rightValue+")";
            }

            return null;
        }

        public static bool StructuralEqualObjects(object leftValue, object rightValue)
        {
            return ((IGraph)leftValue).HasSameStructure((IGraph)rightValue);
        }

        public static object PlusObjects(object leftValue, object rightValue,
            string balancedType, string leftType, string rightType, IGraph graph)
        {
            // byte and short are only used for storing, no computations are done with them
            // enums are handled via int
            if(balancedType == "int")
            {
                if(leftType == "byte")
                {
                    if(rightType == "byte")
                        return (int)(sbyte)leftValue + (int)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (int)(sbyte)leftValue + (int)(short)rightValue;
                    else if(rightType == "int")
                        return (int)(sbyte)leftValue + (int)(int)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (int)(sbyte)leftValue + (int)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "short")
                {
                    if(rightType == "byte")
                        return (int)(short)leftValue + (int)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (int)(short)leftValue + (int)(short)rightValue;
                    else if(rightType == "int")
                        return (int)(short)leftValue + (int)(int)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (int)(short)leftValue + (int)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "int")
                {
                    if(rightType == "byte")
                        return (int)(int)leftValue + (int)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (int)(int)leftValue + (int)(short)rightValue;
                    else if(rightType == "int")
                        return (int)(int)leftValue + (int)(int)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (int)(int)leftValue + (int)Convert.ToInt32((Enum)rightValue);
                }
                else // TypesHelper.IsEnumType(leftType, model)
                {
                    if(rightType == "byte")
                        return (int)Convert.ToInt32((Enum)leftValue) + (int)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (int)Convert.ToInt32((Enum)leftValue) + (int)(short)rightValue;
                    else if(rightType == "int")
                        return (int)Convert.ToInt32((Enum)leftValue) + (int)(int)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (int)Convert.ToInt32((Enum)leftValue) + (int)Convert.ToInt32((Enum)rightValue);
                }
            }
            else if(balancedType == "long")
            {
                if(leftType == "byte")
                {
                    if(rightType == "byte")
                        return (long)(sbyte)leftValue + (long)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (long)(sbyte)leftValue + (long)(short)rightValue;
                    else if(rightType == "int")
                        return (long)(sbyte)leftValue + (long)(int)rightValue;
                    else if(rightType == "long")
                        return (long)(sbyte)leftValue + (long)(long)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (long)(sbyte)leftValue + (long)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "short")
                {
                    if(rightType == "byte")
                        return (long)(short)leftValue + (long)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (long)(short)leftValue + (long)(short)rightValue;
                    else if(rightType == "int")
                        return (long)(short)leftValue + (long)(int)rightValue;
                    else if(rightType == "long")
                        return (long)(short)leftValue + (long)(long)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (long)(short)leftValue + (long)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "int")
                {
                    if(rightType == "byte")
                        return (long)(int)leftValue + (long)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (long)(int)leftValue + (long)(short)rightValue;
                    else if(rightType == "int")
                        return (long)(int)leftValue + (long)(int)rightValue;
                    else if(rightType == "long")
                        return (long)(int)leftValue + (long)(long)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (long)(int)leftValue + (long)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "long")
                {
                    if(rightType == "byte")
                        return (long)(long)leftValue + (long)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (long)(long)leftValue + (long)(short)rightValue;
                    else if(rightType == "int")
                        return (long)(long)leftValue + (long)(int)rightValue;
                    else if(rightType == "long")
                        return (long)(long)leftValue + (long)(long)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (long)(long)leftValue + (long)Convert.ToInt32((Enum)rightValue);
                }
                else // TypesHelper.IsEnumType(leftType, model)
                {
                    if(rightType == "byte")
                        return (long)Convert.ToInt32((Enum)leftValue) + (long)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (long)Convert.ToInt32((Enum)leftValue) + (long)(short)rightValue;
                    else if(rightType == "int")
                        return (long)Convert.ToInt32((Enum)leftValue) + (long)(int)rightValue;
                    else if(rightType == "long")
                        return (long)Convert.ToInt32((Enum)leftValue) + (long)(long)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (long)Convert.ToInt32((Enum)leftValue) + (long)Convert.ToInt32((Enum)rightValue);
                }
            }
            else if(balancedType == "float")
            {
                if(leftType == "byte")
                {
                    if(rightType == "byte")
                        return (float)(sbyte)leftValue + (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)(sbyte)leftValue + (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)(sbyte)leftValue + (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)(sbyte)leftValue + (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)(sbyte)leftValue + (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)(sbyte)leftValue + (float)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "short")
                {
                    if(rightType == "byte")
                        return (float)(short)leftValue + (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)(short)leftValue + (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)(short)leftValue + (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)(short)leftValue + (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)(short)leftValue + (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)(short)leftValue + (float)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "int")
                {
                    if(rightType == "byte")
                        return (float)(int)leftValue + (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)(int)leftValue + (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)(int)leftValue + (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)(int)leftValue + (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)(int)leftValue + (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)(int)leftValue + (float)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "long")
                {
                    if(rightType == "byte")
                        return (float)(long)leftValue + (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)(long)leftValue + (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)(long)leftValue + (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)(long)leftValue + (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)(long)leftValue + (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)(long)leftValue + (float)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "float")
                {
                    if(rightType == "byte")
                        return (float)(float)leftValue + (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)(float)leftValue + (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)(float)leftValue + (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)(float)leftValue + (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)(float)leftValue + (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)(float)leftValue + (float)Convert.ToInt32((Enum)rightValue);
                }
                else // TypesHelper.IsEnumType(leftType, model)
                {
                    if(rightType == "byte")
                        return (float)Convert.ToInt32((Enum)leftValue) + (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)Convert.ToInt32((Enum)leftValue) + (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)Convert.ToInt32((Enum)leftValue) + (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)Convert.ToInt32((Enum)leftValue) + (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)Convert.ToInt32((Enum)leftValue) + (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)Convert.ToInt32((Enum)leftValue) + (float)Convert.ToInt32((Enum)rightValue);
                }
            }
            else if(balancedType == "double")
            {
                if(leftType == "byte")
                {
                    if(rightType == "byte")
                        return (double)(sbyte)leftValue + (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(sbyte)leftValue + (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(sbyte)leftValue + (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(sbyte)leftValue + (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(sbyte)leftValue + (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(sbyte)leftValue + (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(sbyte)leftValue + (double)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "short")
                {
                    if(rightType == "byte")
                        return (double)(short)leftValue + (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(short)leftValue + (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(short)leftValue + (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(short)leftValue + (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(short)leftValue + (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(short)leftValue + (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(short)leftValue + (double)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "int")
                {
                    if(rightType == "byte")
                        return (double)(int)leftValue + (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(int)leftValue + (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(int)leftValue + (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(int)leftValue + (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(int)leftValue + (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(int)leftValue + (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(int)leftValue + (double)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "long")
                {
                    if(rightType == "byte")
                        return (double)(long)leftValue + (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(long)leftValue + (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(long)leftValue + (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(long)leftValue + (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(long)leftValue + (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(long)leftValue + (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(long)leftValue + (double)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "float")
                {
                    if(rightType == "byte")
                        return (double)(float)leftValue + (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(float)leftValue + (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(float)leftValue + (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(float)leftValue + (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(float)leftValue + (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(float)leftValue + (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(float)leftValue + (double)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "double")
                {
                    if(rightType == "byte")
                        return (double)(double)leftValue + (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(double)leftValue + (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(double)leftValue + (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(double)leftValue + (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(double)leftValue + (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(double)leftValue + (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(double)leftValue + (double)Convert.ToInt32((Enum)rightValue);
                }
                else // TypesHelper.IsEnumType(leftType, model)
                {
                    if(rightType == "byte")
                        return (double)Convert.ToInt32((Enum)leftValue) + (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)Convert.ToInt32((Enum)leftValue) + (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)Convert.ToInt32((Enum)leftValue) + (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)Convert.ToInt32((Enum)leftValue) + (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)Convert.ToInt32((Enum)leftValue) + (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)Convert.ToInt32((Enum)leftValue) + (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)Convert.ToInt32((Enum)leftValue) + (double)Convert.ToInt32((Enum)rightValue);
                }
            }
            else if(balancedType == "string")
            {
                if(leftType == "string")
                {
                    if(rightType.StartsWith("set<"))
                        return (string)leftValue + DictionaryListHelper.ToString((IDictionary)rightValue, graph);
                    else if(rightType.StartsWith("map<"))
                        return (string)leftValue + DictionaryListHelper.ToString((IDictionary)rightValue, graph);
                    else if(rightType.StartsWith("array<"))
                        return (string)leftValue + DictionaryListHelper.ToString((IList)rightValue, graph);
                    else if(rightType == "string")
                        return (string)leftValue + (string)rightValue;
                    else
                        return (string)leftValue + DictionaryListHelper.ToString(rightValue, graph);
                }
                else //rightType == "string"
                {
                    if(leftType.StartsWith("set<"))
                        return DictionaryListHelper.ToString((IDictionary)leftValue, graph) + (string)rightValue;
                    else if(leftType.StartsWith("map<"))
                        return DictionaryListHelper.ToString((IDictionary)leftValue, graph) + (string)rightValue;
                    else if(leftType.StartsWith("array<"))
                        return DictionaryListHelper.ToString((IList)leftValue, graph) + (string)rightValue;
                    else if(leftType == "string")
                        return (string)leftValue + (string)rightValue;
                    else
                        return DictionaryListHelper.ToString(leftValue, graph) + (string)rightValue;
                }
            }
            else if(balancedType.StartsWith("array<"))
            {
                return DictionaryListHelper.ConcatenateIList((IList)leftValue, (IList)rightValue);
            }

            throw new Exception("Invalid types for +");
        }

        public static string PlusStatic(string leftValue, string rightValue,
            string balancedType, string leftType, string rightType)
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
                        return "("+leftValue+" + GRGEN_LIBGR.DictionaryListHelper.ToString("+rightValue+", graph))";
                    else if(rightType.StartsWith("map<"))
                        return "("+leftValue+" + GRGEN_LIBGR.DictionaryListHelper.ToString("+rightValue+", graph))";
                    else if(rightType.StartsWith("array<"))
                        return "("+leftValue+" + GRGEN_LIBGR.DictionaryListHelper.ToString("+rightValue+", graph))";
                    else if(rightType == "string")
                        return "("+leftValue+" + "+rightValue+")";
                    else
                        return "("+leftValue+" + GRGEN_LIBGR.DictionaryListHelper.ToString("+rightValue+", graph))";
                }
                else //rightType == "string"
                {
                    if(leftType.StartsWith("set<"))
                        return "(GRGEN_LIBGR.DictionaryListHelper.ToString("+leftValue+", graph) + "+rightValue+")";
                    else if(leftType.StartsWith("map<"))
                        return "(GRGEN_LIBGR.DictionaryListHelper.ToString("+leftValue+", graph) + "+rightValue+")";
                    else if(leftType.StartsWith("array<"))
                        return "(GRGEN_LIBGR.DictionaryListHelper.ToString("+leftValue+", graph) + "+rightValue+")";
                    else if(leftType == "string")
                        return "("+leftValue+" + "+rightValue+")";
                    else
                        return "(GRGEN_LIBGR.DictionaryListHelper.ToString("+leftValue+", graph) + "+rightValue+")";
                }
            }
            else if(balancedType.StartsWith("array<"))
            {
                return "GRGEN_LIBGR.DictionaryListHelper.ConcatenateIList((IList)"+leftValue+", (IList)"+rightValue+")";
            }

            return null;
        }


        ///////////////////////////////////////////////////////////////////////////

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
                                if(left == right) return left;
                                else return "-";
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
                        if(left == right && (left.StartsWith("set<") || left.StartsWith("map<") || left.StartsWith("array<")))
                            return left;
                        else return "-";
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
                            if(left == right && left.StartsWith("array<"))
                                return left;
                            else return "-";
                        }
                    }
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

            if(TypesHelper.IsSameOrSubtype(left, right, model))
                return right;

            if(TypesHelper.IsSameOrSubtype(right, left, model))
                return left;

            return "-";
        }
    }
}

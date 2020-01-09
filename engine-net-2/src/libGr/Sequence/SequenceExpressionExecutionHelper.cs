/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
    /// <summary>
    /// The sequence expression execution helper selects and executes the correct version of an operator based on the target and argument types.
    /// It is employed at runtime for sequence operator implementation, or at compile time in case the type is not known statically.
    /// </summary>
    public static class SequenceExpressionExecutionHelper
    {
        // the double casting in the following code is needed because a boxed value must be unboxed first
        // with the correct type before it can be casted to the real target type

        public static bool EqualObjects(object leftValue, object rightValue, 
            string balancedType, string leftType, string rightType, IGraph graph)
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
            else if(balancedType == "graph")
            {
                return GraphHelper.Equal((IGraph)leftValue, (IGraph)rightValue);
            }
            else if(balancedType.StartsWith("set<"))
            {
                return ContainerHelper.EqualIDictionary((IDictionary)leftValue, (IDictionary)rightValue);
            }
            else if(balancedType.StartsWith("map<"))
            {
                return ContainerHelper.EqualIDictionary((IDictionary)leftValue, (IDictionary)rightValue);
            }
            else if(balancedType.StartsWith("array<"))
            {
                return ContainerHelper.EqualIList((IList)leftValue, (IList)rightValue);
            }
            else if(balancedType.StartsWith("deque<"))
            {
                return ContainerHelper.EqualIDeque((IDeque)leftValue, (IDeque)rightValue);
            }
            else if(TypesHelper.IsExternalTypeIncludingObjectType(balancedType, graph.Model))
            {
                return graph.Model.IsEqual((object)leftValue, (object)rightValue);
            }
            else
            {
                return Object.Equals(leftValue, rightValue);
            }
        }

        public static bool NotEqualObjects(object leftValue, object rightValue,
            string balancedType, string leftType, string rightType, IGraph graph)
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
            else if(balancedType == "graph")
            {
                return !GraphHelper.Equal((IGraph)leftValue, (IGraph)rightValue);
            }
            else if(balancedType.StartsWith("set<"))
            {
                return ContainerHelper.NotEqualIDictionary((IDictionary)leftValue, (IDictionary)rightValue);
            }
            else if(balancedType.StartsWith("map<"))
            {
                return ContainerHelper.NotEqualIDictionary((IDictionary)leftValue, (IDictionary)rightValue);
            }
            else if(balancedType.StartsWith("array<"))
            {
                return ContainerHelper.NotEqualIList((IList)leftValue, (IList)rightValue);
            }
            else if(balancedType.StartsWith("deque<"))
            {
                return ContainerHelper.NotEqualIDeque((IDeque)leftValue, (IDeque)rightValue);
            }
            else if(TypesHelper.IsExternalTypeIncludingObjectType(balancedType, graph.Model))
            {
                return !graph.Model.IsEqual((object)leftValue, (object)rightValue);
            }
            else
            {
                return !Object.Equals(leftValue, rightValue);
            }
        }

        public static bool LowerObjects(object leftValue, object rightValue,
            string balancedType, string leftType, string rightType, IGraph graph)
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
                return ContainerHelper.LessThanIDictionary((IDictionary)leftValue, (IDictionary)rightValue);
            }
            else if(balancedType.StartsWith("map<"))
            {
                return ContainerHelper.LessThanIDictionary((IDictionary)leftValue, (IDictionary)rightValue);
            }
            else if(balancedType.StartsWith("array<"))
            {
                return ContainerHelper.LessThanIList((IList)leftValue, (IList)rightValue);
            }
            else if(balancedType.StartsWith("deque<"))
            {
                return ContainerHelper.LessThanIDeque((IDeque)leftValue, (IDeque)rightValue);
            }
            else if(TypesHelper.IsExternalTypeIncludingObjectType(balancedType, graph.Model))
            {
                return graph.Model.IsLower((object)leftValue, (object)rightValue);
            }

            throw new Exception("Invalid types for <");
        }

        public static bool GreaterObjects(object leftValue, object rightValue,
            string balancedType, string leftType, string rightType, IGraph graph)
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
                return ContainerHelper.GreaterThanIDictionary((IDictionary)leftValue, (IDictionary)rightValue);
            }
            else if(balancedType.StartsWith("map<"))
            {
                return ContainerHelper.GreaterThanIDictionary((IDictionary)leftValue, (IDictionary)rightValue);
            }
            else if(balancedType.StartsWith("array<"))
            {
                return ContainerHelper.GreaterThanIList((IList)leftValue, (IList)rightValue);
            }
            else if(balancedType.StartsWith("deque<"))
            {
                return ContainerHelper.GreaterThanIDeque((IDeque)leftValue, (IDeque)rightValue);
            }
            else if(TypesHelper.IsExternalTypeIncludingObjectType(balancedType, graph.Model))
            {
                return !graph.Model.IsLower((object)leftValue, (object)rightValue) && !graph.Model.IsEqual((object)leftValue, (object)rightValue);
            }

            throw new Exception("Invalid types for >");
        }

        public static bool LowerEqualObjects(object leftValue, object rightValue,
            string balancedType, string leftType, string rightType, IGraph graph)
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
                return ContainerHelper.LessOrEqualIDictionary((IDictionary)leftValue, (IDictionary)rightValue);
            }
            else if(balancedType.StartsWith("map<"))
            {
                return ContainerHelper.LessOrEqualIDictionary((IDictionary)leftValue, (IDictionary)rightValue);
            }
            else if(balancedType.StartsWith("array<"))
            {
                return ContainerHelper.LessOrEqualIList((IList)leftValue, (IList)rightValue);
            }
            else if(balancedType.StartsWith("deque<"))
            {
                return ContainerHelper.LessOrEqualIDeque((IDeque)leftValue, (IDeque)rightValue);
            }
            else if(TypesHelper.IsExternalTypeIncludingObjectType(balancedType, graph.Model))
            {
                return graph.Model.IsLower((object)leftValue, (object)rightValue) || graph.Model.IsEqual((object)leftValue, (object)rightValue);
            }

            throw new Exception("Invalid types for <=");
        }

        public static bool GreaterEqualObjects(object leftValue, object rightValue,
            string balancedType, string leftType, string rightType, IGraph graph)
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
                return ContainerHelper.GreaterOrEqualIDictionary((IDictionary)leftValue, (IDictionary)rightValue);
            }
            else if(balancedType.StartsWith("map<"))
            {
                return ContainerHelper.GreaterOrEqualIDictionary((IDictionary)leftValue, (IDictionary)rightValue);
            }
            else if(balancedType.StartsWith("array<"))
            {
                return ContainerHelper.GreaterOrEqualIList((IList)leftValue, (IList)rightValue);
            }
            else if(balancedType.StartsWith("deque<"))
            {
                return ContainerHelper.GreaterOrEqualIDeque((IDeque)leftValue, (IDeque)rightValue);
            }
            else if(TypesHelper.IsExternalTypeIncludingObjectType(balancedType, graph.Model))
            {
                return !graph.Model.IsLower((object)leftValue, (object)rightValue);
            }

            throw new Exception("Invalid types for >=");
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
                        return (string)leftValue + EmitHelper.ToString((IDictionary)rightValue, graph);
                    else if(rightType.StartsWith("map<"))
                        return (string)leftValue + EmitHelper.ToString((IDictionary)rightValue, graph);
                    else if(rightType.StartsWith("array<"))
                        return (string)leftValue + EmitHelper.ToString((IList)rightValue, graph);
                    else if(rightType.StartsWith("deque<"))
                        return (string)leftValue + EmitHelper.ToString((IDeque)rightValue, graph);
                    else if(rightType == "string")
                        return (string)leftValue + (string)rightValue;
                    else
                        return (string)leftValue + EmitHelper.ToString(rightValue, graph);
                }
                else //rightType == "string"
                {
                    if(leftType.StartsWith("set<"))
                        return EmitHelper.ToString((IDictionary)leftValue, graph) + (string)rightValue;
                    else if(leftType.StartsWith("map<"))
                        return EmitHelper.ToString((IDictionary)leftValue, graph) + (string)rightValue;
                    else if(leftType.StartsWith("array<"))
                        return EmitHelper.ToString((IList)leftValue, graph) + (string)rightValue;
                    else if(leftType.StartsWith("deque<"))
                        return EmitHelper.ToString((IDeque)leftValue, graph) + (string)rightValue;
                    else if(leftType == "string")
                        return (string)leftValue + (string)rightValue;
                    else
                        return EmitHelper.ToString(leftValue, graph) + (string)rightValue;
                }
            }
            else if(balancedType.StartsWith("array<"))
            {
                return ContainerHelper.ConcatenateIList((IList)leftValue, (IList)rightValue);
            }
            else if(balancedType.StartsWith("deque<"))
            {
                return ContainerHelper.ConcatenateIDeque((IDeque)leftValue, (IDeque)rightValue);
            }

            throw new Exception("Invalid types for +");
        }

        public static object MinusObjects(object leftValue, object rightValue,
            string balancedType, string leftType, string rightType, IGraph graph)
        {
            // byte and short are only used for storing, no computations are done with them
            // enums are handled via int
            if(balancedType == "int")
            {
                if(leftType == "byte")
                {
                    if(rightType == "byte")
                        return (int)(sbyte)leftValue - (int)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (int)(sbyte)leftValue - (int)(short)rightValue;
                    else if(rightType == "int")
                        return (int)(sbyte)leftValue - (int)(int)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (int)(sbyte)leftValue - (int)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "short")
                {
                    if(rightType == "byte")
                        return (int)(short)leftValue - (int)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (int)(short)leftValue - (int)(short)rightValue;
                    else if(rightType == "int")
                        return (int)(short)leftValue - (int)(int)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (int)(short)leftValue - (int)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "int")
                {
                    if(rightType == "byte")
                        return (int)(int)leftValue - (int)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (int)(int)leftValue - (int)(short)rightValue;
                    else if(rightType == "int")
                        return (int)(int)leftValue - (int)(int)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (int)(int)leftValue - (int)Convert.ToInt32((Enum)rightValue);
                }
                else // TypesHelper.IsEnumType(leftType, model)
                {
                    if(rightType == "byte")
                        return (int)Convert.ToInt32((Enum)leftValue) - (int)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (int)Convert.ToInt32((Enum)leftValue) - (int)(short)rightValue;
                    else if(rightType == "int")
                        return (int)Convert.ToInt32((Enum)leftValue) - (int)(int)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (int)Convert.ToInt32((Enum)leftValue) - (int)Convert.ToInt32((Enum)rightValue);
                }
            }
            else if(balancedType == "long")
            {
                if(leftType == "byte")
                {
                    if(rightType == "byte")
                        return (long)(sbyte)leftValue - (long)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (long)(sbyte)leftValue - (long)(short)rightValue;
                    else if(rightType == "int")
                        return (long)(sbyte)leftValue - (long)(int)rightValue;
                    else if(rightType == "long")
                        return (long)(sbyte)leftValue - (long)(long)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (long)(sbyte)leftValue - (long)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "short")
                {
                    if(rightType == "byte")
                        return (long)(short)leftValue - (long)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (long)(short)leftValue - (long)(short)rightValue;
                    else if(rightType == "int")
                        return (long)(short)leftValue - (long)(int)rightValue;
                    else if(rightType == "long")
                        return (long)(short)leftValue - (long)(long)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (long)(short)leftValue - (long)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "int")
                {
                    if(rightType == "byte")
                        return (long)(int)leftValue - (long)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (long)(int)leftValue - (long)(short)rightValue;
                    else if(rightType == "int")
                        return (long)(int)leftValue - (long)(int)rightValue;
                    else if(rightType == "long")
                        return (long)(int)leftValue - (long)(long)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (long)(int)leftValue - (long)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "long")
                {
                    if(rightType == "byte")
                        return (long)(long)leftValue - (long)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (long)(long)leftValue - (long)(short)rightValue;
                    else if(rightType == "int")
                        return (long)(long)leftValue - (long)(int)rightValue;
                    else if(rightType == "long")
                        return (long)(long)leftValue - (long)(long)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (long)(long)leftValue - (long)Convert.ToInt32((Enum)rightValue);
                }
                else // TypesHelper.IsEnumType(leftType, model)
                {
                    if(rightType == "byte")
                        return (long)Convert.ToInt32((Enum)leftValue) - (long)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (long)Convert.ToInt32((Enum)leftValue) - (long)(short)rightValue;
                    else if(rightType == "int")
                        return (long)Convert.ToInt32((Enum)leftValue) - (long)(int)rightValue;
                    else if(rightType == "long")
                        return (long)Convert.ToInt32((Enum)leftValue) - (long)(long)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (long)Convert.ToInt32((Enum)leftValue) - (long)Convert.ToInt32((Enum)rightValue);
                }
            }
            else if(balancedType == "float")
            {
                if(leftType == "byte")
                {
                    if(rightType == "byte")
                        return (float)(sbyte)leftValue - (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)(sbyte)leftValue - (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)(sbyte)leftValue - (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)(sbyte)leftValue - (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)(sbyte)leftValue - (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)(sbyte)leftValue - (float)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "short")
                {
                    if(rightType == "byte")
                        return (float)(short)leftValue - (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)(short)leftValue - (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)(short)leftValue - (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)(short)leftValue - (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)(short)leftValue - (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)(short)leftValue - (float)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "int")
                {
                    if(rightType == "byte")
                        return (float)(int)leftValue - (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)(int)leftValue - (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)(int)leftValue - (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)(int)leftValue - (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)(int)leftValue - (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)(int)leftValue - (float)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "long")
                {
                    if(rightType == "byte")
                        return (float)(long)leftValue - (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)(long)leftValue - (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)(long)leftValue - (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)(long)leftValue - (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)(long)leftValue - (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)(long)leftValue - (float)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "float")
                {
                    if(rightType == "byte")
                        return (float)(float)leftValue - (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)(float)leftValue - (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)(float)leftValue - (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)(float)leftValue - (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)(float)leftValue - (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)(float)leftValue - (float)Convert.ToInt32((Enum)rightValue);
                }
                else // TypesHelper.IsEnumType(leftType, model)
                {
                    if(rightType == "byte")
                        return (float)Convert.ToInt32((Enum)leftValue) - (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)Convert.ToInt32((Enum)leftValue) - (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)Convert.ToInt32((Enum)leftValue) - (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)Convert.ToInt32((Enum)leftValue) - (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)Convert.ToInt32((Enum)leftValue) - (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)Convert.ToInt32((Enum)leftValue) - (float)Convert.ToInt32((Enum)rightValue);
                }
            }
            else if(balancedType == "double")
            {
                if(leftType == "byte")
                {
                    if(rightType == "byte")
                        return (double)(sbyte)leftValue - (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(sbyte)leftValue - (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(sbyte)leftValue - (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(sbyte)leftValue - (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(sbyte)leftValue - (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(sbyte)leftValue - (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(sbyte)leftValue - (double)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "short")
                {
                    if(rightType == "byte")
                        return (double)(short)leftValue - (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(short)leftValue - (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(short)leftValue - (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(short)leftValue - (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(short)leftValue - (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(short)leftValue - (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(short)leftValue - (double)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "int")
                {
                    if(rightType == "byte")
                        return (double)(int)leftValue - (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(int)leftValue - (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(int)leftValue - (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(int)leftValue - (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(int)leftValue - (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(int)leftValue - (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(int)leftValue - (double)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "long")
                {
                    if(rightType == "byte")
                        return (double)(long)leftValue - (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(long)leftValue - (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(long)leftValue - (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(long)leftValue - (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(long)leftValue - (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(long)leftValue - (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(long)leftValue - (double)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "float")
                {
                    if(rightType == "byte")
                        return (double)(float)leftValue - (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(float)leftValue - (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(float)leftValue - (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(float)leftValue - (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(float)leftValue - (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(float)leftValue - (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(float)leftValue - (double)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "double")
                {
                    if(rightType == "byte")
                        return (double)(double)leftValue - (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(double)leftValue - (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(double)leftValue - (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(double)leftValue - (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(double)leftValue - (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(double)leftValue - (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(double)leftValue - (double)Convert.ToInt32((Enum)rightValue);
                }
                else // TypesHelper.IsEnumType(leftType, model)
                {
                    if(rightType == "byte")
                        return (double)Convert.ToInt32((Enum)leftValue) - (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)Convert.ToInt32((Enum)leftValue) - (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)Convert.ToInt32((Enum)leftValue) - (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)Convert.ToInt32((Enum)leftValue) - (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)Convert.ToInt32((Enum)leftValue) - (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)Convert.ToInt32((Enum)leftValue) - (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)Convert.ToInt32((Enum)leftValue) - (double)Convert.ToInt32((Enum)rightValue);
                }
            }

            throw new Exception("Invalid types for -");
        }

        public static object MulObjects(object leftValue, object rightValue,
            string balancedType, string leftType, string rightType, IGraph graph)
        {
            // byte and short are only used for storing, no computations are done with them
            // enums are handled via int
            if(balancedType == "int")
            {
                if(leftType == "byte")
                {
                    if(rightType == "byte")
                        return (int)(sbyte)leftValue * (int)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (int)(sbyte)leftValue * (int)(short)rightValue;
                    else if(rightType == "int")
                        return (int)(sbyte)leftValue * (int)(int)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (int)(sbyte)leftValue * (int)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "short")
                {
                    if(rightType == "byte")
                        return (int)(short)leftValue * (int)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (int)(short)leftValue * (int)(short)rightValue;
                    else if(rightType == "int")
                        return (int)(short)leftValue * (int)(int)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (int)(short)leftValue * (int)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "int")
                {
                    if(rightType == "byte")
                        return (int)(int)leftValue * (int)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (int)(int)leftValue * (int)(short)rightValue;
                    else if(rightType == "int")
                        return (int)(int)leftValue * (int)(int)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (int)(int)leftValue * (int)Convert.ToInt32((Enum)rightValue);
                }
                else // TypesHelper.IsEnumType(leftType, model)
                {
                    if(rightType == "byte")
                        return (int)Convert.ToInt32((Enum)leftValue) * (int)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (int)Convert.ToInt32((Enum)leftValue) * (int)(short)rightValue;
                    else if(rightType == "int")
                        return (int)Convert.ToInt32((Enum)leftValue) * (int)(int)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (int)Convert.ToInt32((Enum)leftValue) * (int)Convert.ToInt32((Enum)rightValue);
                }
            }
            else if(balancedType == "long")
            {
                if(leftType == "byte")
                {
                    if(rightType == "byte")
                        return (long)(sbyte)leftValue * (long)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (long)(sbyte)leftValue * (long)(short)rightValue;
                    else if(rightType == "int")
                        return (long)(sbyte)leftValue * (long)(int)rightValue;
                    else if(rightType == "long")
                        return (long)(sbyte)leftValue * (long)(long)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (long)(sbyte)leftValue * (long)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "short")
                {
                    if(rightType == "byte")
                        return (long)(short)leftValue * (long)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (long)(short)leftValue * (long)(short)rightValue;
                    else if(rightType == "int")
                        return (long)(short)leftValue * (long)(int)rightValue;
                    else if(rightType == "long")
                        return (long)(short)leftValue * (long)(long)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (long)(short)leftValue * (long)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "int")
                {
                    if(rightType == "byte")
                        return (long)(int)leftValue * (long)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (long)(int)leftValue * (long)(short)rightValue;
                    else if(rightType == "int")
                        return (long)(int)leftValue * (long)(int)rightValue;
                    else if(rightType == "long")
                        return (long)(int)leftValue * (long)(long)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (long)(int)leftValue * (long)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "long")
                {
                    if(rightType == "byte")
                        return (long)(long)leftValue * (long)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (long)(long)leftValue * (long)(short)rightValue;
                    else if(rightType == "int")
                        return (long)(long)leftValue * (long)(int)rightValue;
                    else if(rightType == "long")
                        return (long)(long)leftValue * (long)(long)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (long)(long)leftValue * (long)Convert.ToInt32((Enum)rightValue);
                }
                else // TypesHelper.IsEnumType(leftType, model)
                {
                    if(rightType == "byte")
                        return (long)Convert.ToInt32((Enum)leftValue) * (long)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (long)Convert.ToInt32((Enum)leftValue) * (long)(short)rightValue;
                    else if(rightType == "int")
                        return (long)Convert.ToInt32((Enum)leftValue) * (long)(int)rightValue;
                    else if(rightType == "long")
                        return (long)Convert.ToInt32((Enum)leftValue) * (long)(long)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (long)Convert.ToInt32((Enum)leftValue) * (long)Convert.ToInt32((Enum)rightValue);
                }
            }
            else if(balancedType == "float")
            {
                if(leftType == "byte")
                {
                    if(rightType == "byte")
                        return (float)(sbyte)leftValue * (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)(sbyte)leftValue * (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)(sbyte)leftValue * (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)(sbyte)leftValue * (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)(sbyte)leftValue * (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)(sbyte)leftValue * (float)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "short")
                {
                    if(rightType == "byte")
                        return (float)(short)leftValue * (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)(short)leftValue * (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)(short)leftValue * (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)(short)leftValue * (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)(short)leftValue * (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)(short)leftValue * (float)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "int")
                {
                    if(rightType == "byte")
                        return (float)(int)leftValue * (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)(int)leftValue * (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)(int)leftValue * (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)(int)leftValue * (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)(int)leftValue * (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)(int)leftValue * (float)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "long")
                {
                    if(rightType == "byte")
                        return (float)(long)leftValue * (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)(long)leftValue * (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)(long)leftValue * (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)(long)leftValue * (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)(long)leftValue * (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)(long)leftValue * (float)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "float")
                {
                    if(rightType == "byte")
                        return (float)(float)leftValue * (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)(float)leftValue * (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)(float)leftValue * (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)(float)leftValue * (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)(float)leftValue * (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)(float)leftValue * (float)Convert.ToInt32((Enum)rightValue);
                }
                else // TypesHelper.IsEnumType(leftType, model)
                {
                    if(rightType == "byte")
                        return (float)Convert.ToInt32((Enum)leftValue) * (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)Convert.ToInt32((Enum)leftValue) * (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)Convert.ToInt32((Enum)leftValue) * (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)Convert.ToInt32((Enum)leftValue) * (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)Convert.ToInt32((Enum)leftValue) * (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)Convert.ToInt32((Enum)leftValue) * (float)Convert.ToInt32((Enum)rightValue);
                }
            }
            else if(balancedType == "double")
            {
                if(leftType == "byte")
                {
                    if(rightType == "byte")
                        return (double)(sbyte)leftValue * (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(sbyte)leftValue * (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(sbyte)leftValue * (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(sbyte)leftValue * (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(sbyte)leftValue * (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(sbyte)leftValue * (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(sbyte)leftValue * (double)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "short")
                {
                    if(rightType == "byte")
                        return (double)(short)leftValue * (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(short)leftValue * (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(short)leftValue * (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(short)leftValue * (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(short)leftValue * (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(short)leftValue * (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(short)leftValue * (double)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "int")
                {
                    if(rightType == "byte")
                        return (double)(int)leftValue * (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(int)leftValue * (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(int)leftValue * (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(int)leftValue * (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(int)leftValue * (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(int)leftValue * (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(int)leftValue * (double)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "long")
                {
                    if(rightType == "byte")
                        return (double)(long)leftValue * (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(long)leftValue * (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(long)leftValue * (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(long)leftValue * (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(long)leftValue * (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(long)leftValue * (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(long)leftValue * (double)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "float")
                {
                    if(rightType == "byte")
                        return (double)(float)leftValue * (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(float)leftValue * (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(float)leftValue * (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(float)leftValue * (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(float)leftValue * (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(float)leftValue * (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(float)leftValue * (double)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "double")
                {
                    if(rightType == "byte")
                        return (double)(double)leftValue * (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(double)leftValue * (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(double)leftValue * (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(double)leftValue * (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(double)leftValue * (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(double)leftValue * (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(double)leftValue * (double)Convert.ToInt32((Enum)rightValue);
                }
                else // TypesHelper.IsEnumType(leftType, model)
                {
                    if(rightType == "byte")
                        return (double)Convert.ToInt32((Enum)leftValue) * (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)Convert.ToInt32((Enum)leftValue) * (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)Convert.ToInt32((Enum)leftValue) * (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)Convert.ToInt32((Enum)leftValue) * (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)Convert.ToInt32((Enum)leftValue) * (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)Convert.ToInt32((Enum)leftValue) * (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)Convert.ToInt32((Enum)leftValue) * (double)Convert.ToInt32((Enum)rightValue);
                }
            }

            throw new Exception("Invalid types for *");
        }

        public static object DivObjects(object leftValue, object rightValue,
            string balancedType, string leftType, string rightType, IGraph graph)
        {
            // byte and short are only used for storing, no computations are done with them
            // enums are handled via int
            if(balancedType == "int")
            {
                if(leftType == "byte")
                {
                    if(rightType == "byte")
                        return (int)(sbyte)leftValue / (int)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (int)(sbyte)leftValue / (int)(short)rightValue;
                    else if(rightType == "int")
                        return (int)(sbyte)leftValue / (int)(int)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (int)(sbyte)leftValue / (int)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "short")
                {
                    if(rightType == "byte")
                        return (int)(short)leftValue / (int)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (int)(short)leftValue / (int)(short)rightValue;
                    else if(rightType == "int")
                        return (int)(short)leftValue / (int)(int)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (int)(short)leftValue / (int)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "int")
                {
                    if(rightType == "byte")
                        return (int)(int)leftValue / (int)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (int)(int)leftValue / (int)(short)rightValue;
                    else if(rightType == "int")
                        return (int)(int)leftValue / (int)(int)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (int)(int)leftValue / (int)Convert.ToInt32((Enum)rightValue);
                }
                else // TypesHelper.IsEnumType(leftType, model)
                {
                    if(rightType == "byte")
                        return (int)Convert.ToInt32((Enum)leftValue) / (int)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (int)Convert.ToInt32((Enum)leftValue) / (int)(short)rightValue;
                    else if(rightType == "int")
                        return (int)Convert.ToInt32((Enum)leftValue) / (int)(int)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (int)Convert.ToInt32((Enum)leftValue) / (int)Convert.ToInt32((Enum)rightValue);
                }
            }
            else if(balancedType == "long")
            {
                if(leftType == "byte")
                {
                    if(rightType == "byte")
                        return (long)(sbyte)leftValue / (long)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (long)(sbyte)leftValue / (long)(short)rightValue;
                    else if(rightType == "int")
                        return (long)(sbyte)leftValue / (long)(int)rightValue;
                    else if(rightType == "long")
                        return (long)(sbyte)leftValue / (long)(long)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (long)(sbyte)leftValue / (long)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "short")
                {
                    if(rightType == "byte")
                        return (long)(short)leftValue / (long)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (long)(short)leftValue / (long)(short)rightValue;
                    else if(rightType == "int")
                        return (long)(short)leftValue / (long)(int)rightValue;
                    else if(rightType == "long")
                        return (long)(short)leftValue / (long)(long)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (long)(short)leftValue / (long)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "int")
                {
                    if(rightType == "byte")
                        return (long)(int)leftValue / (long)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (long)(int)leftValue / (long)(short)rightValue;
                    else if(rightType == "int")
                        return (long)(int)leftValue / (long)(int)rightValue;
                    else if(rightType == "long")
                        return (long)(int)leftValue / (long)(long)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (long)(int)leftValue / (long)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "long")
                {
                    if(rightType == "byte")
                        return (long)(long)leftValue / (long)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (long)(long)leftValue / (long)(short)rightValue;
                    else if(rightType == "int")
                        return (long)(long)leftValue / (long)(int)rightValue;
                    else if(rightType == "long")
                        return (long)(long)leftValue / (long)(long)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (long)(long)leftValue / (long)Convert.ToInt32((Enum)rightValue);
                }
                else // TypesHelper.IsEnumType(leftType, model)
                {
                    if(rightType == "byte")
                        return (long)Convert.ToInt32((Enum)leftValue) / (long)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (long)Convert.ToInt32((Enum)leftValue) / (long)(short)rightValue;
                    else if(rightType == "int")
                        return (long)Convert.ToInt32((Enum)leftValue) / (long)(int)rightValue;
                    else if(rightType == "long")
                        return (long)Convert.ToInt32((Enum)leftValue) / (long)(long)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (long)Convert.ToInt32((Enum)leftValue) / (long)Convert.ToInt32((Enum)rightValue);
                }
            }
            else if(balancedType == "float")
            {
                if(leftType == "byte")
                {
                    if(rightType == "byte")
                        return (float)(sbyte)leftValue / (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)(sbyte)leftValue / (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)(sbyte)leftValue / (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)(sbyte)leftValue / (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)(sbyte)leftValue / (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)(sbyte)leftValue / (float)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "short")
                {
                    if(rightType == "byte")
                        return (float)(short)leftValue / (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)(short)leftValue / (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)(short)leftValue / (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)(short)leftValue / (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)(short)leftValue / (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)(short)leftValue / (float)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "int")
                {
                    if(rightType == "byte")
                        return (float)(int)leftValue / (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)(int)leftValue / (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)(int)leftValue / (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)(int)leftValue / (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)(int)leftValue / (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)(int)leftValue / (float)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "long")
                {
                    if(rightType == "byte")
                        return (float)(long)leftValue / (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)(long)leftValue / (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)(long)leftValue / (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)(long)leftValue / (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)(long)leftValue / (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)(long)leftValue / (float)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "float")
                {
                    if(rightType == "byte")
                        return (float)(float)leftValue / (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)(float)leftValue / (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)(float)leftValue / (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)(float)leftValue / (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)(float)leftValue / (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)(float)leftValue / (float)Convert.ToInt32((Enum)rightValue);
                }
                else // TypesHelper.IsEnumType(leftType, model)
                {
                    if(rightType == "byte")
                        return (float)Convert.ToInt32((Enum)leftValue) / (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)Convert.ToInt32((Enum)leftValue) / (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)Convert.ToInt32((Enum)leftValue) / (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)Convert.ToInt32((Enum)leftValue) / (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)Convert.ToInt32((Enum)leftValue) / (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)Convert.ToInt32((Enum)leftValue) / (float)Convert.ToInt32((Enum)rightValue);
                }
            }
            else if(balancedType == "double")
            {
                if(leftType == "byte")
                {
                    if(rightType == "byte")
                        return (double)(sbyte)leftValue / (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(sbyte)leftValue / (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(sbyte)leftValue / (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(sbyte)leftValue / (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(sbyte)leftValue / (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(sbyte)leftValue / (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(sbyte)leftValue / (double)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "short")
                {
                    if(rightType == "byte")
                        return (double)(short)leftValue / (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(short)leftValue / (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(short)leftValue / (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(short)leftValue / (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(short)leftValue / (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(short)leftValue / (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(short)leftValue / (double)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "int")
                {
                    if(rightType == "byte")
                        return (double)(int)leftValue / (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(int)leftValue / (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(int)leftValue / (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(int)leftValue / (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(int)leftValue / (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(int)leftValue / (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(int)leftValue / (double)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "long")
                {
                    if(rightType == "byte")
                        return (double)(long)leftValue / (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(long)leftValue / (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(long)leftValue / (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(long)leftValue / (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(long)leftValue / (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(long)leftValue / (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(long)leftValue / (double)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "float")
                {
                    if(rightType == "byte")
                        return (double)(float)leftValue / (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(float)leftValue / (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(float)leftValue / (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(float)leftValue / (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(float)leftValue / (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(float)leftValue / (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(float)leftValue / (double)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "double")
                {
                    if(rightType == "byte")
                        return (double)(double)leftValue / (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(double)leftValue / (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(double)leftValue / (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(double)leftValue / (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(double)leftValue / (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(double)leftValue / (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(double)leftValue / (double)Convert.ToInt32((Enum)rightValue);
                }
                else // TypesHelper.IsEnumType(leftType, model)
                {
                    if(rightType == "byte")
                        return (double)Convert.ToInt32((Enum)leftValue) / (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)Convert.ToInt32((Enum)leftValue) / (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)Convert.ToInt32((Enum)leftValue) / (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)Convert.ToInt32((Enum)leftValue) / (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)Convert.ToInt32((Enum)leftValue) / (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)Convert.ToInt32((Enum)leftValue) / (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)Convert.ToInt32((Enum)leftValue) / (double)Convert.ToInt32((Enum)rightValue);
                }
            }

            throw new Exception("Invalid types for /");
        }

        public static object ModObjects(object leftValue, object rightValue,
            string balancedType, string leftType, string rightType, IGraph graph)
        {
            // byte and short are only used for storing, no computations are done with them
            // enums are handled via int
            if(balancedType == "int")
            {
                if(leftType == "byte")
                {
                    if(rightType == "byte")
                        return (int)(sbyte)leftValue % (int)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (int)(sbyte)leftValue % (int)(short)rightValue;
                    else if(rightType == "int")
                        return (int)(sbyte)leftValue % (int)(int)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (int)(sbyte)leftValue % (int)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "short")
                {
                    if(rightType == "byte")
                        return (int)(short)leftValue % (int)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (int)(short)leftValue % (int)(short)rightValue;
                    else if(rightType == "int")
                        return (int)(short)leftValue % (int)(int)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (int)(short)leftValue % (int)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "int")
                {
                    if(rightType == "byte")
                        return (int)(int)leftValue % (int)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (int)(int)leftValue % (int)(short)rightValue;
                    else if(rightType == "int")
                        return (int)(int)leftValue % (int)(int)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (int)(int)leftValue % (int)Convert.ToInt32((Enum)rightValue);
                }
                else // TypesHelper.IsEnumType(leftType, model)
                {
                    if(rightType == "byte")
                        return (int)Convert.ToInt32((Enum)leftValue) % (int)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (int)Convert.ToInt32((Enum)leftValue) % (int)(short)rightValue;
                    else if(rightType == "int")
                        return (int)Convert.ToInt32((Enum)leftValue) % (int)(int)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (int)Convert.ToInt32((Enum)leftValue) % (int)Convert.ToInt32((Enum)rightValue);
                }
            }
            else if(balancedType == "long")
            {
                if(leftType == "byte")
                {
                    if(rightType == "byte")
                        return (long)(sbyte)leftValue % (long)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (long)(sbyte)leftValue % (long)(short)rightValue;
                    else if(rightType == "int")
                        return (long)(sbyte)leftValue % (long)(int)rightValue;
                    else if(rightType == "long")
                        return (long)(sbyte)leftValue % (long)(long)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (long)(sbyte)leftValue % (long)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "short")
                {
                    if(rightType == "byte")
                        return (long)(short)leftValue % (long)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (long)(short)leftValue % (long)(short)rightValue;
                    else if(rightType == "int")
                        return (long)(short)leftValue % (long)(int)rightValue;
                    else if(rightType == "long")
                        return (long)(short)leftValue % (long)(long)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (long)(short)leftValue % (long)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "int")
                {
                    if(rightType == "byte")
                        return (long)(int)leftValue % (long)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (long)(int)leftValue % (long)(short)rightValue;
                    else if(rightType == "int")
                        return (long)(int)leftValue % (long)(int)rightValue;
                    else if(rightType == "long")
                        return (long)(int)leftValue % (long)(long)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (long)(int)leftValue % (long)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "long")
                {
                    if(rightType == "byte")
                        return (long)(long)leftValue % (long)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (long)(long)leftValue % (long)(short)rightValue;
                    else if(rightType == "int")
                        return (long)(long)leftValue % (long)(int)rightValue;
                    else if(rightType == "long")
                        return (long)(long)leftValue % (long)(long)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (long)(long)leftValue % (long)Convert.ToInt32((Enum)rightValue);
                }
                else // TypesHelper.IsEnumType(leftType, model)
                {
                    if(rightType == "byte")
                        return (long)Convert.ToInt32((Enum)leftValue) % (long)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (long)Convert.ToInt32((Enum)leftValue) % (long)(short)rightValue;
                    else if(rightType == "int")
                        return (long)Convert.ToInt32((Enum)leftValue) % (long)(int)rightValue;
                    else if(rightType == "long")
                        return (long)Convert.ToInt32((Enum)leftValue) % (long)(long)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (long)Convert.ToInt32((Enum)leftValue) % (long)Convert.ToInt32((Enum)rightValue);
                }
            }
            else if(balancedType == "float")
            {
                if(leftType == "byte")
                {
                    if(rightType == "byte")
                        return (float)(sbyte)leftValue % (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)(sbyte)leftValue % (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)(sbyte)leftValue % (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)(sbyte)leftValue % (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)(sbyte)leftValue % (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)(sbyte)leftValue % (float)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "short")
                {
                    if(rightType == "byte")
                        return (float)(short)leftValue % (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)(short)leftValue % (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)(short)leftValue % (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)(short)leftValue % (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)(short)leftValue % (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)(short)leftValue % (float)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "int")
                {
                    if(rightType == "byte")
                        return (float)(int)leftValue % (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)(int)leftValue % (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)(int)leftValue % (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)(int)leftValue % (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)(int)leftValue % (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)(int)leftValue % (float)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "long")
                {
                    if(rightType == "byte")
                        return (float)(long)leftValue % (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)(long)leftValue % (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)(long)leftValue % (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)(long)leftValue % (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)(long)leftValue % (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)(long)leftValue % (float)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "float")
                {
                    if(rightType == "byte")
                        return (float)(float)leftValue % (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)(float)leftValue % (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)(float)leftValue % (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)(float)leftValue % (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)(float)leftValue % (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)(float)leftValue % (float)Convert.ToInt32((Enum)rightValue);
                }
                else // TypesHelper.IsEnumType(leftType, model)
                {
                    if(rightType == "byte")
                        return (float)Convert.ToInt32((Enum)leftValue) % (float)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (float)Convert.ToInt32((Enum)leftValue) % (float)(short)rightValue;
                    else if(rightType == "int")
                        return (float)Convert.ToInt32((Enum)leftValue) % (float)(int)rightValue;
                    else if(rightType == "long")
                        return (float)Convert.ToInt32((Enum)leftValue) % (float)(long)rightValue;
                    else if(rightType == "float")
                        return (float)Convert.ToInt32((Enum)leftValue) % (float)(float)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (float)Convert.ToInt32((Enum)leftValue) % (float)Convert.ToInt32((Enum)rightValue);
                }
            }
            else if(balancedType == "double")
            {
                if(leftType == "byte")
                {
                    if(rightType == "byte")
                        return (double)(sbyte)leftValue % (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(sbyte)leftValue % (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(sbyte)leftValue % (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(sbyte)leftValue % (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(sbyte)leftValue % (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(sbyte)leftValue % (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(sbyte)leftValue % (double)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "short")
                {
                    if(rightType == "byte")
                        return (double)(short)leftValue % (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(short)leftValue % (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(short)leftValue % (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(short)leftValue % (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(short)leftValue % (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(short)leftValue % (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(short)leftValue % (double)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "int")
                {
                    if(rightType == "byte")
                        return (double)(int)leftValue % (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(int)leftValue % (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(int)leftValue % (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(int)leftValue % (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(int)leftValue % (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(int)leftValue % (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(int)leftValue % (double)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "long")
                {
                    if(rightType == "byte")
                        return (double)(long)leftValue % (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(long)leftValue % (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(long)leftValue % (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(long)leftValue % (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(long)leftValue % (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(long)leftValue % (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(long)leftValue % (double)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "float")
                {
                    if(rightType == "byte")
                        return (double)(float)leftValue % (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(float)leftValue % (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(float)leftValue % (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(float)leftValue % (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(float)leftValue % (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(float)leftValue % (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(float)leftValue % (double)Convert.ToInt32((Enum)rightValue);
                }
                else if(leftType == "double")
                {
                    if(rightType == "byte")
                        return (double)(double)leftValue % (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)(double)leftValue % (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)(double)leftValue % (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)(double)leftValue % (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)(double)leftValue % (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)(double)leftValue % (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)(double)leftValue % (double)Convert.ToInt32((Enum)rightValue);
                }
                else // TypesHelper.IsEnumType(leftType, model)
                {
                    if(rightType == "byte")
                        return (double)Convert.ToInt32((Enum)leftValue) % (double)(sbyte)rightValue;
                    else if(rightType == "short")
                        return (double)Convert.ToInt32((Enum)leftValue) % (double)(short)rightValue;
                    else if(rightType == "int")
                        return (double)Convert.ToInt32((Enum)leftValue) % (double)(int)rightValue;
                    else if(rightType == "long")
                        return (double)Convert.ToInt32((Enum)leftValue) % (double)(long)rightValue;
                    else if(rightType == "float")
                        return (double)Convert.ToInt32((Enum)leftValue) % (double)(float)rightValue;
                    else if(rightType == "double")
                        return (double)Convert.ToInt32((Enum)leftValue) % (double)(double)rightValue;
                    else // TypesHelper.IsEnumType(rightType, model)
                        return (double)Convert.ToInt32((Enum)leftValue) % (double)Convert.ToInt32((Enum)rightValue);
                }
            }

            throw new Exception("Invalid types for %");
        }
    }
}

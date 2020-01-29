/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System;
using System.Collections.Generic;
using System.Collections;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// The common base of sequence, sequence computation, and sequence expression objects,
    /// with some common infrastructure.
    /// </summary>
    public abstract class SequenceBase
    {
        /// <summary>
        /// Initializes a new SequenceBase object (sets the id).
        /// </summary>
        protected SequenceBase()
        {
            id = idSource;
            ++idSource;
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="that">The sequence base to be copied.</param>
        protected SequenceBase(SequenceBase that)
        {
            id = that.id;
        }

        /// <summary>
        /// Checks the sequence /expression for errors utilizing the given checking environment
        /// reports them by exception
        /// </summary>s
        public abstract void Check(SequenceCheckingEnvironment env);

        /// <summary>
        /// Returns the type of the sequence /expression (for sequences always "boolean")
        /// </summary>
        public abstract string Type(SequenceCheckingEnvironment env);
        
        /// <summary>
        /// A common random number generator for all sequence /expression objects.
        /// It uses a time-dependent seed.
        /// </summary>
        public static Random randomGenerator = new Random();

        /// <summary>
        /// The precedence of this operator. Zero is the highest priority, int.MaxValue the lowest.
        /// Used to add needed parentheses for printing sequences /expressions
        /// TODO: WTF? das ist im Parser genau umgekehrt implementiert!
        /// </summary>
        public abstract int Precedence { get; }

        /// <summary>
        /// A string symbol representing this sequence /expression kind.
        /// </summary>
        public abstract String Symbol { get; }

        /// <summary>
        /// returns the sequence /expresion id - every sequence /expression is assigned a unique id used in xgrs code generation
        /// for copies the old id is just taken over, does not cause problems as code is only generated once per defined sequence
        /// </summary>
        public int Id { get { return id; } }

        /// <summary>
        /// stores the sequence /expression unique id
        /// </summary>
        protected readonly int id;

        /// <summary>
        /// the static member used to assign the unique ids to the sequence /expression instances
        /// </summary>
        protected static int idSource = 0;

        /// <summary>
        /// Enumerates all child sequence computation objects
        /// </summary>
        public abstract IEnumerable<SequenceBase> ChildrenBase { get; }

        /// <summary>
        /// sets for the very node the profiling flag (does not recurse)
        /// </summary>
        public virtual void SetNeedForProfiling(bool profiling)
        {
            // NOP, sufficient for most sequences / sequence computations / sequence expressions,
            // only the node/edge/incident/adjacent/reachable/isX-constructs need to call a special version
            // counting up the search steps with each visited element/graph element accessed (but not the implicit operations)
        }

        /// <summary>
        /// sets for the node and all children, i.e. the entire tree the profiling flag
        /// </summary>
        public void SetNeedForProfilingRecursive(bool profiling)
        {
            SetNeedForProfiling(true);
            foreach(SequenceBase child in ChildrenBase)
            {
                child.SetNeedForProfilingRecursive(profiling);
            }
        }

        // resolves names that are given without package context but do not reference global names
        // because they are used from a sequence that is contained in a package (only possible for compiled sequences from rule language)
        // (i.e. calls of entities from packages, without package prefix are changed to package calls (may occur for entities from the same package))
        public static void ResolvePackage(String Name, String PrePackage, String PrePackageContext, bool unprefixedNameExists,
            out String Package, out String PackagePrefixedName)
        {
            if(PrePackage != null)
            {
                Package = PrePackage;
                PackagePrefixedName = PrePackage + "::" + Name;
                return;
            }

            if(unprefixedNameExists)
            {
                Package = null;
                PackagePrefixedName = Name;
                return;
            }

            if(PrePackageContext != null)
            {
                Package = PrePackageContext;
                PackagePrefixedName = PrePackageContext + "::" + Name;
                return;
            }

            // should not occur, (to be) handled in SequenceCheckingEnvironment
            Package = null;
            PackagePrefixedName = Name;
        }

        #region helper methods for call input argument and argument expression, as well as return variable handling

        public static void FillArgumentsFromArgumentExpressions(SequenceExpression[] ArgumentExpressions, object[] Arguments, IGraphProcessingEnvironment procEnv)
        {
            for(int i = 0; i < ArgumentExpressions.Length; ++i)
            {
                Arguments[i] = ArgumentExpressions[i].Evaluate(procEnv);
            }
        }

        public static void FillReturnVariablesFromValues(SequenceVariable[] ReturnVars, IAction Action, IGraphProcessingEnvironment procEnv, List<object[]> retElemsList, int which)
        {
            if(which == -1)
            {
                FillReturnVariablesFromValues(ReturnVars, Action, procEnv, retElemsList);
            }
            else
            {
                object[] retElems = retElemsList[0];
                FillReturnVariablesFromValues(ReturnVars, procEnv, retElems);
            }
        }

        public static void FillReturnVariablesFromValues(SequenceVariable[] ReturnVars, IAction Action, IGraphProcessingEnvironment procEnv, List<object[]> retElemsList)
        {
            IList[] returnVars = null;
            if(ReturnVars.Length > 0)
            {
                returnVars = new IList[ReturnVars.Length];
                for(int i = 0; i < ReturnVars.Length; ++i)
                {
                    returnVars[i] = ReturnVars[i].GetVariableValue(procEnv) as IList;
                    if(returnVars[i] == null)
                    {
                        string returnType = TypesHelper.DotNetTypeToXgrsType(Action.RulePattern.Outputs[i]);
                        Type valueType = ContainerHelper.GetTypeFromNameForContainer(returnType, procEnv.Graph);
                        returnVars[i] = ContainerHelper.NewList(valueType);
                        ReturnVars[i].SetVariableValue(returnVars[i], procEnv);
                    }
                    else
                        returnVars[i].Clear();
                }
            }
            for(int curRetElemNum = 0; curRetElemNum < retElemsList.Count; ++curRetElemNum)
            {
                object[] retElems = retElemsList[curRetElemNum];
                for(int i = 0; i < ReturnVars.Length; ++i)
                {
                    returnVars[i].Add(retElems[i]);
                }
            }
        }

        public static void FillReturnVariablesFromValues(SequenceVariable[] ReturnVars, IGraphProcessingEnvironment procEnv, object[] retElems)
        {
            for(int i = 0; i < ReturnVars.Length; ++i)
            {
                ReturnVars[i].SetVariableValue(retElems[i], procEnv);
            }
        }

        public static void InitializeArgumentExpressionsAndArguments(List<SequenceExpression> argExprs,
            out SequenceExpression[] ArgumentExpressions, out object[] Arguments)
        {
            foreach(SequenceExpression argExpr in argExprs)
            {
                if(argExpr == null)
                    throw new Exception("Null entry in argument expressions");
            }
            ArgumentExpressions = argExprs.ToArray();
            Arguments = new object[ArgumentExpressions.Length];
        }

        public static void InitializeReturnVariables(List<SequenceVariable> returnVars,
            out SequenceVariable[] ReturnVars)
        {
            foreach(SequenceVariable returnVar in returnVars)
            {
                if(returnVar == null)
                    throw new Exception("Null entry in return variables");
            }
            ReturnVars = returnVars.ToArray();
        }

        public static void CopyArgumentExpressionsAndArguments(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv,
            SequenceExpression[] sourceArgumentExpressions, 
            out SequenceExpression[] targetArgumentExpressions, out object[] targetArguments)
        {
            targetArgumentExpressions = new SequenceExpression[sourceArgumentExpressions.Length];
            for(int i = 0; i < sourceArgumentExpressions.Length; ++i)
            {
                targetArgumentExpressions[i] = sourceArgumentExpressions[i].CopyExpression(originalToCopy, procEnv);
            }
            targetArguments = new object[targetArgumentExpressions.Length];
        }

        public static void CopyReturnVars(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv,
            SequenceVariable[] sourceReturnVars,
            out SequenceVariable[] targetReturnVars)
        {
            targetReturnVars = new SequenceVariable[sourceReturnVars.Length];
            for(int i = 0; i < sourceReturnVars.Length; ++i)
            {
                targetReturnVars[i] = sourceReturnVars[i].Copy(originalToCopy, procEnv);
            }
        }

        public static void GetLocalVariables(SequenceExpression[] ArgumentExpressions, 
            Dictionary<SequenceVariable, SetValueType> variables, List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            foreach(SequenceExpression seqExpr in ArgumentExpressions)
            {
                seqExpr.GetLocalVariables(variables, containerConstructors);
            }
        }

        public static void GetLocalVariables(SequenceVariable[] ReturnVars, 
            Dictionary<SequenceVariable, SetValueType> variables, List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            foreach(SequenceVariable seqVar in ReturnVars)
            {
                seqVar.GetLocalVariables(variables);
            }
        }

        #endregion helper methods for call input argument and argument expression, as well as return variable handling
    }
}

/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Collections.Generic;

namespace de.unika.ipd.grGen.libGr.sequenceParser
{
    /// <summary>
    /// An environment class for the sequence parser, 
    /// gives it access to the entitites (and types) that can be referenced in the sequence, and works as a factory for call objects, 
    /// abstracting away the difference between interpreted and compiled sequences.
    /// Abstract base class, there are two concrete subclasses, one for interpreted, one for compiled sequences.
    /// </summary>
    public abstract class SequenceParserEnvironment
    {
        /// <summary>
        /// The model used in the specification
        /// </summary>
        private readonly IGraphModel model;
        public IGraphModel Model
        {
            get { return model; }
        }

        /// <summary>
        /// The name of the package the sequence is contained in (defining some context), null if it is not contained in a package. 
        /// Also null in case of an interpreted sequence, only compiled sequences may appear within a package.
        /// </summary>
        public virtual String PackageContext
        {
            get { return null; }
        }

        /// <summary>
        /// Gives the rule of the match this stands for in the if clause of the debug match event.
        /// Null if the context is not the one of a debug event condition.
        /// </summary>
        public virtual string RuleOfMatchThis
        {
            get { return null; }
        }

        /// <summary>
        /// Gives the graph element type of the graph element this stands for in the if clause of the debug new/delete/retype/set-attributes event.
        /// Null if the context is not the one of a debug event condition.
        /// </summary>
        public virtual string TypeOfGraphElementThis
        {
            get { return null; }
        }

        protected SequenceParserEnvironment(IGraphModel model)
        {
            this.model = model;
        }


        abstract public bool IsSequenceName(String ruleOrSequenceName, String package);

        abstract public SequenceSequenceCall CreateSequenceSequenceCall(String sequenceName, String packagePrefix,
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars, SequenceVariable subgraph,
            bool special);

        abstract public bool IsRuleName(String ruleOrSequenceName, String package);

        abstract public SequenceRuleCall CreateSequenceRuleCall(String ruleName, String packagePrefix,
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars, SequenceVariable subgraph,
            bool special, bool test, bool isRuleForMultiRuleAllCallReturningArrays);

        abstract public SequenceRuleAllCall CreateSequenceRuleAllCall(String ruleName, String packagePrefix,
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars, SequenceVariable subgraph,
            bool special, bool test,
            bool chooseRandom, SequenceVariable varChooseRandom,
            bool chooseRandom2, SequenceVariable varChooseRandom2, bool choice);

        abstract public SequenceRuleCountAllCall CreateSequenceRuleCountAllCall(String ruleName, String packagePrefix,
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars, SequenceVariable subgraph,
            bool special, bool test);

        // must be called with resolved rule package
        abstract public SequenceFilterCallBase CreateSequenceFilterCall(String ruleName, String rulePackage,
            String packagePrefix, String filterBase, List<String> entities, List<SequenceExpression> argExprs);

        abstract public SequenceFilterCallBase CreateSequenceFilterCall(String ruleName, String rulePackage,
            String packagePrefix, String filterBase, List<String> entities,
            SequenceVariable arrayAccess, SequenceVariable index, SequenceVariable element, SequenceExpression lambdaExpr);

        abstract public SequenceFilterCallBase CreateSequenceFilterCall(String ruleName, String rulePackage,
            String packagePrefix, String filterBase, List<String> entities,
            SequenceVariable initArrayAccess, SequenceExpression initExpr,
            SequenceVariable arrayAccess, SequenceVariable previousAccumulationAccess,
            SequenceVariable index, SequenceVariable element, SequenceExpression lambdaExpr);

        abstract public string GetPackagePrefixedMatchClassName(String matchClassName, String matchClassPackage);

        abstract public SequenceFilterCallBase CreateSequenceMatchClassFilterCall(String matchClassName, String matchClassPackage,
            String packagePrefix, String filterBase, List<String> entities, List<SequenceExpression> argExprs);

        abstract public SequenceFilterCallBase CreateSequenceMatchClassFilterCall(String matchClassName, String matchClassPackage,
            String packagePrefix, String filterBase, List<String> entities, 
            SequenceVariable arrayAccess, SequenceVariable index, SequenceVariable element, SequenceExpression lambdaExpr);

        abstract public SequenceFilterCallBase CreateSequenceMatchClassFilterCall(String matchClassName, String matchClassPackage,
            String packagePrefix, String filterBase, List<String> entities,
            SequenceVariable initArrayAccess, SequenceExpression initExpr,
            SequenceVariable arrayAccess, SequenceVariable previousAccumulationAccess,
            SequenceVariable index, SequenceVariable element, SequenceExpression lambdaExpr);

        protected String GetFilterName(String filterBase, List<String> entities)
        {
            if(entities == null || entities.Count == 0)
                return filterBase;
            if(filterBase == "auto")
                return filterBase;
            return filterBase + "<" + String.Join(",", entities.ToArray()) + ">";
        }

        public bool IsAutoSuppliedFilterName(String filterBase)
        {
            return filterBase == "keepFirst" || filterBase == "keepLast"
                    || filterBase == "removeFirst" || filterBase == "removeLast"
                    || filterBase == "keepFirstFraction" || filterBase == "keepLastFraction"
                    || filterBase == "removeFirstFraction" || filterBase == "removeLastFraction";
        }

        public bool IsAutoGeneratedBaseFilterName(String filterBase)
        {
            return filterBase == "orderAscendingBy" || filterBase == "orderDescendingBy" || filterBase == "groupBy"
                || filterBase == "keepSameAsFirst" || filterBase == "keepSameAsLast" 
                || filterBase == "keepOneForEach" || filterBase == "keepOneForEachAccumulateBy";
        }

        public bool IsPerElementBaseFilterName(String filterBase)
        {
            return filterBase == "assign" || filterBase == "removeIf" || filterBase == "assignStartWithAccumulateBy";
        }

        protected bool IsAutoGeneratedFilter(String filterBase, List<String> entities)
        {
            return filterBase == "auto" || (entities != null && entities.Count > 0);
        }


        public SequenceComputation CreateSequenceComputationProcedureCall(String procedureName, String package,
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars)
        {
            if(procedureName == "valloc" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count != 0)
                    throw new ParseException("\"" + procedureName + "\" expects no parameters)");
                return new SequenceComputationBuiltinProcedureCall(new SequenceComputationVAlloc(), returnVars);
            }
            else if(procedureName == "vfree" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count != 1)
                    throw new ParseException("\"" + procedureName + "\" expects 1 parameter)");
                return new SequenceComputationVFree(getArgument(argExprs, 0), true);
            }
            else if(procedureName == "vfreenonreset" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count != 1)
                    throw new ParseException("\"" + procedureName + "\" expects 1 parameter)");
                return new SequenceComputationVFree(getArgument(argExprs, 0), false);
            }
            else if(procedureName == "vreset" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count != 1)
                    throw new ParseException("\"" + procedureName + "\" expects 1 parameter)");
                return new SequenceComputationVReset(getArgument(argExprs, 0));
            }
            else if(procedureName == "emit" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count == 0)
                    throw new ParseException("\"" + procedureName + "\" expects at least 1 parameter)");
                return new SequenceComputationEmit(argExprs, false);
            }
            else if(procedureName == "emitdebug" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count == 0)
                    throw new ParseException("\"" + procedureName + "\" expects at least 1 parameter)");
                return new SequenceComputationEmit(argExprs, true);
            }
            else if(procedureName == "record" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count != 1)
                    throw new ParseException("\"" + procedureName + "\" expects 1 parameter)");
                return new SequenceComputationRecord(getArgument(argExprs, 0));
            }
            else if(procedureName == "add" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count != 1 && argExprs.Count != 3)
                    throw new ParseException("\"" + procedureName + "\" expects 1(for a node) or 3(for an edge) parameters)");
                return new SequenceComputationBuiltinProcedureCall(new SequenceComputationGraphAdd(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2)), returnVars);
            }
            else if(procedureName == "rem" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count != 1)
                    throw new ParseException("\"" + procedureName + "\" expects 1 parameter)");
                return new SequenceComputationGraphRem(getArgument(argExprs, 0));
            }
            else if(procedureName == "clear" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count != 0)
                    throw new ParseException("\"" + procedureName + "\" expects no parameters)");
                return new SequenceComputationGraphClear();
            }
            else if(procedureName == "retype" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count != 2)
                    throw new ParseException("\"" + procedureName + "\" expects 2 (graph entity, new type) parameters)");
                return new SequenceComputationBuiltinProcedureCall(new SequenceComputationGraphRetype(getArgument(argExprs, 0), getArgument(argExprs, 1)), returnVars);
            }
            else if(procedureName == "addCopy" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count != 1 && argExprs.Count != 3)
                    throw new ParseException("\"" + procedureName + "\" expects 1(for a node) or 3(for an edge) parameters)");
                return new SequenceComputationBuiltinProcedureCall(new SequenceComputationGraphAddCopy(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), true), returnVars);
            }
            else if(procedureName == "addClone" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count != 1 && argExprs.Count != 3)
                    throw new ParseException("\"" + procedureName + "\" expects 1(for a node) or 3(for an edge) parameters)");
                return new SequenceComputationBuiltinProcedureCall(new SequenceComputationGraphAddCopy(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), false), returnVars);
            }
            else if(procedureName == "merge" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count != 2)
                    throw new ParseException("\"" + procedureName + "\" expects 2 (the nodes to merge) parameters)");
                return new SequenceComputationGraphMerge(getArgument(argExprs, 0), getArgument(argExprs, 1));
            }
            else if(procedureName == "redirectSource" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count != 2)
                    throw new ParseException("\"" + procedureName + "\" expects 2 (edge to redirect, new source node) parameters)");
                return new SequenceComputationGraphRedirectSource(getArgument(argExprs, 0), getArgument(argExprs, 1));
            }
            else if(procedureName == "redirectTarget" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count != 2)
                    throw new ParseException("\"" + procedureName + "\" expects 2 (edge to redirect, new target node) parameters)");
                return new SequenceComputationGraphRedirectTarget(getArgument(argExprs, 0), getArgument(argExprs, 1));
            }
            else if(procedureName == "redirectSourceAndTarget" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count != 3)
                    throw new ParseException("\"" + procedureName + "\" expects 3 (edge to redirect, new source node, new target node) parameters)");
                return new SequenceComputationGraphRedirectSourceAndTarget(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2));
            }
            else if(procedureName == "insert" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count != 1)
                    throw new ParseException("\"" + procedureName + "\" expects 1 (graph to destroyingly insert) parameter)");
                return new SequenceComputationInsert(getArgument(argExprs, 0));
            }
            else if(procedureName == "insertCopy" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count != 2)
                    throw new ParseException("\"" + procedureName + "\" expects 2 (graph and one node to return the clone of) parameters)");
                return new SequenceComputationBuiltinProcedureCall(new SequenceComputationInsertCopy(getArgument(argExprs, 0), getArgument(argExprs, 1)), returnVars);
            }
            else if(procedureName == "insertInduced" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count != 2)
                    throw new ParseException("\"" + procedureName + "\" expects 2 parameters (the set of nodes to compute the induced subgraph from which will be cloned and inserted, and one node of the set of which the clone will be returned)");
                return new SequenceComputationBuiltinProcedureCall(new SequenceComputationInsertInduced(getArgument(argExprs, 0), getArgument(argExprs, 1)), returnVars);
            }
            else if(procedureName == "insertDefined" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count != 2)
                    throw new ParseException("\"" + procedureName + "\" expects 2 parameters (the set of edges which define the subgraph which will be cloned and inserted, and one edge of the set of which the clone will be returned)");
                return new SequenceComputationBuiltinProcedureCall(new SequenceComputationInsertDefined(getArgument(argExprs, 0), getArgument(argExprs, 1)), returnVars);
            }
            else if(procedureName == "export" && package != null && package == "File")
            {
                if(argExprs.Count != 1 && argExprs.Count != 2)
                    throw new ParseException("\"File::export\" expects 1 (name of file only) or 2 (graph to export, name of file) parameters)");
                return new SequenceComputationExport(getArgument(argExprs, 0), getArgument(argExprs, 1));
            }
            else if(procedureName == "delete" && package != null && package == "File")
            {
                if(argExprs.Count != 1)
                    throw new ParseException("\"File::delete\" expects 1 (the path of the file) parameter)");
                return new SequenceComputationDeleteFile(getArgument(argExprs, 0));
            }
            else if(procedureName == "add" && package != null && package == "Debug")
            {
                if(argExprs.Count < 1)
                    throw new ParseException("\"Debug::add\" expects at least 1 parameter (the message/entered entity)");
                return new SequenceComputationDebugAdd(argExprs);
            }
            else if(procedureName == "rem" && package != null && package == "Debug")
            {
                if(argExprs.Count < 1)
                    throw new ParseException("\"Debug::rem\" expects at least 1 parameter (the message/exited entity)");
                return new SequenceComputationDebugRem(argExprs);
            }
            else if(procedureName == "emit" && package != null && package == "Debug")
            {
                if(argExprs.Count < 1)
                    throw new ParseException("\"Debug::emit\" expects at least 1 parameter (the message)");
                return new SequenceComputationDebugEmit(argExprs);
            }
            else if(procedureName == "halt" && package != null && package == "Debug")
            {
                if(argExprs.Count < 1)
                    throw new ParseException("\"Debug::halt\" expects at least 1 parameter (the message)");
                return new SequenceComputationDebugHalt(argExprs);
            }
            else if(procedureName == "highlight" && package != null && package == "Debug")
            {
                if(argExprs.Count < 1)
                    throw new ParseException("\"Debug::highlight\" expects at least 1 parameter (the message)");
                return new SequenceComputationDebugHighlight(argExprs);
            }
            else if(procedureName == "assert" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 1)
                    throw new ParseException("\"" + procedureName + "\" expects at least 1 parameter (the condition to assert on)");
                return new SequenceComputationAssert(argExprs, false);
            }
            else if(procedureName == "assertAlways" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 1)
                    throw new ParseException("\"" + procedureName + "\" expects at least 1 parameter (the condition to assert on)");
                return new SequenceComputationAssert(argExprs, true);
            }
            else if(procedureName == "enter" && package != null && package == "Synchronization")
            {
                if(argExprs.Count != 1)
                    throw new ParseException("\"Synchronization::enter\" expects 1 parameter (the lock object)");
                return new SequenceComputationSynchronizationEnter(getArgument(argExprs, 0));
            }
            else if(procedureName == "tryenter" && package != null && package == "Synchronization")
            {
                if(argExprs.Count != 1)
                    throw new ParseException("\"Synchronization::tryenter\" expects 1 parameter (the lock object)");
                return new SequenceComputationBuiltinProcedureCall(new SequenceComputationSynchronizationTryEnter(getArgument(argExprs, 0)), returnVars);
            }
            else if(procedureName == "exit" && package != null && package == "Synchronization")
            {
                if(argExprs.Count != 1)
                    throw new ParseException("\"Synchronization::exit\" expects 1 parameter (the lock object)");
                return new SequenceComputationSynchronizationExit(getArgument(argExprs, 0));
            }
            else if(procedureName == "getEquivalentOrAdd" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count != 2)
                    throw new ParseException("\"getEquivalentOrAdd\" expects 2 parameters (the graph to check for, and the array of graphs to check against)");
                return new SequenceComputationBuiltinProcedureCall(new SequenceComputationGetEquivalentOrAdd(getArgument(argExprs, 0), getArgument(argExprs, 1), true), returnVars);
            }
            else if(procedureName == "getEquivalentStructurallyOrAdd" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count != 2)
                    throw new ParseException("\"getEquivalentStructurallyOrAdd\" expects 2 parameters (the graph to check for, and the array of graphs to check against)");
                return new SequenceComputationBuiltinProcedureCall(new SequenceComputationGetEquivalentOrAdd(getArgument(argExprs, 0), getArgument(argExprs, 1), false), returnVars);
            }
            else
            {
                if(IsProcedureName(procedureName, package))
                    return CreateSequenceComputationProcedureCallUserProcedure(procedureName, package, argExprs, returnVars);
                else
                    throw new ParseException("Unknown procedure name: \"" + procedureName + "\"!"
                        + " (available are valloc|vfree|vfreenonreset|vreset|emit|emitdebug|record|File::export|File::delete"
                        + "|add|addCopy|rem|clear|retype|merge|redirectSource|redirectTarget|redirectSourceAndTarget"
                        + "|insert|insertCopy|insertInduced|insertDefined"
                        + "|Debug::add|Debug::rem|Debug::emit|Debug::halt|Debug::highlight"
                        + "|assert|assertAlways"
                        + "|Synchronization::enter|Synchronization::tryenter|Synchronization::exit"
                        + "|getEquivalentOrAdd|getEquivalentStructurallyOrAdd"
                        + " or one of the procedureNames defined in the .grg: " + GetProcedureNames() + ")");
            }
        }

        public SequenceComputation CreateSequenceComputationProcedureMethodCall(SequenceExpressionAttributeAccess attrAcc,
            String procedureName, List<SequenceExpression> argExprs, List<SequenceVariable> returnVars)
        {
            if(procedureName == "add")
            {
                if(argExprs.Count != 1 && argExprs.Count != 2)
                    throw new ParseException("\"" + procedureName + "\" expects 1(for set,deque,array end) or 2(for map,array with index) parameters)");
                return new SequenceComputationContainerAdd(attrAcc, argExprs[0], argExprs.Count == 2 ? argExprs[1] : null);
            }
            else if(procedureName == "rem")
            {
                if(argExprs.Count > 1)
                    throw new ParseException("\"" + procedureName + "\" expects 1(for set,map,array with index) or 0(for deque,array end) parameters )");
                return new SequenceComputationContainerRem(attrAcc, argExprs.Count == 1 ? argExprs[0] : null);
            }
            else if(procedureName == "clear")
            {
                if(argExprs.Count > 0)
                    throw new ParseException("\"" + procedureName + "\" expects no parameters)");
                return new SequenceComputationContainerClear(attrAcc);
            }
            else
                return CreateSequenceComputationProcedureMethodCallUserProcedure(attrAcc, procedureName, argExprs, returnVars);
        }

        public SequenceComputation CreateSequenceComputationProcedureMethodCall(SequenceVariable targetVar,
            String procedureName, List<SequenceExpression> argExprs, List<SequenceVariable> returnVars)
        {
            if(procedureName == "add")
            {
                if(argExprs.Count != 1 && argExprs.Count != 2)
                    throw new ParseException("\"" + procedureName + "\" expects 1(for set,deque,array end) or 2(for map,array with index) parameters)");
                return new SequenceComputationContainerAdd(targetVar, argExprs[0], argExprs.Count == 2 ? argExprs[1] : null);
            }
            else if(procedureName == "rem")
            {
                if(argExprs.Count > 1)
                    throw new ParseException("\"" + procedureName + "\" expects 1(for set,map,array with index) or 0(for deque,array end) parameters )");
                return new SequenceComputationContainerRem(targetVar, argExprs.Count == 1 ? argExprs[0] : null);
            }
            else if(procedureName == "clear")
            {
                if(argExprs.Count > 0)
                    throw new ParseException("\"" + procedureName + "\" expects no parameters)");
                return new SequenceComputationContainerClear(targetVar);
            }
            if(procedureName == "addAll")
            {
                if(argExprs.Count != 1)
                    throw new ParseException("\"" + procedureName + "\" expects 1 parameter)");
                return new SequenceComputationContainerAddAll(targetVar, argExprs[0]);
            }
            else
                return CreateSequenceComputationProcedureMethodCallUserProcedure(targetVar, procedureName, argExprs, returnVars);
        }


        abstract public bool IsProcedureName(String procedureName, String package);

        abstract public string GetProcedureNames();

        abstract public SequenceComputationProcedureCall CreateSequenceComputationProcedureCallUserProcedure(String procedureName, String packagePrefix,
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars);

        public SequenceComputationProcedureMethodCall CreateSequenceComputationProcedureMethodCallUserProcedure(SequenceExpression targetExpr,
            String procedureName, List<SequenceExpression> argExprs, List<SequenceVariable> returnVars)
        {
            return new SequenceComputationProcedureMethodCall(targetExpr, procedureName, argExprs, returnVars);
        }

        public SequenceComputationProcedureMethodCall CreateSequenceComputationProcedureMethodCallUserProcedure(SequenceVariable targetVar,
            String procedureName, List<SequenceExpression> argExprs, List<SequenceVariable> returnVars)
        {
            return new SequenceComputationProcedureMethodCall(targetVar, procedureName, argExprs, returnVars);
        }


        public SequenceExpression CreateSequenceExpressionFunctionCall(String functionName, String package,
            List<SequenceExpression> argExprs)
        {
            if(functionName == "nodes" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count > 1)
                    throw new ParseException("\"" + functionName + "\" expects 1 parameter (node type) or none (to get all nodes)");
                return new SequenceExpressionNodes(getArgument(argExprs, 0));
            }
            else if(functionName == "edges" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count > 1)
                    throw new ParseException("\"" + functionName + "\" expects 1 parameter (edge type) or none (to get all edges)");
                return new SequenceExpressionEdges(getArgument(argExprs, 0));
            }
            else if(functionName == "countNodes" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count > 1)
                    throw new ParseException("\"" + functionName + "\" expects 1 parameter (node type) or none (to get count of all nodes)");
                return new SequenceExpressionCountNodes(getArgument(argExprs, 0));
            }
            else if(functionName == "countEdges" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count > 1)
                    throw new ParseException("\"" + functionName + "\" expects 1 parameter (edge type) or none (to get count of all edges)");
                return new SequenceExpressionCountEdges(getArgument(argExprs, 0));
            }
            else if(functionName == "empty" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count > 0)
                    throw new ParseException("\"" + functionName + "\" expects no parameters");
                return new SequenceExpressionEmpty();
            }
            else if(functionName == "size" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count > 0)
                    throw new ParseException("\"" + functionName + "\" expects no parameters");
                return new SequenceExpressionSize();
            }
            else if(functionName == "adjacent" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 1 || argExprs.Count > 3)
                    throw new ParseException("\"" + functionName + "\" expects 1 (start node only) or 2 (start node, incident edge type) or 3 (start node, incident edge type, adjacent node type) parameters)");
                return new SequenceExpressionAdjacentIncident(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), SequenceExpressionType.AdjacentNodes);
            }
            else if(functionName == "adjacentIncoming" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 1 || argExprs.Count > 3)
                    throw new ParseException("\"" + functionName + "\" expects 1 (start node only) or 2 (start node, incident edge type) or 3 (start node, incident edge type, adjacent node type) parameters)");
                return new SequenceExpressionAdjacentIncident(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), SequenceExpressionType.AdjacentNodesViaIncoming);
            }
            else if(functionName == "adjacentOutgoing" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 1 || argExprs.Count > 3)
                    throw new ParseException("\"" + functionName + "\" expects 1 (start node only) or 2 (start node, incident edge type) or 3 (start node, incident edge type, adjacent node type) parameters)");
                return new SequenceExpressionAdjacentIncident(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), SequenceExpressionType.AdjacentNodesViaOutgoing);
            }
            else if(functionName == "incident" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 1 || argExprs.Count > 3)
                    throw new ParseException("\"" + functionName + "\" expects 1 (start node only) or 2 (start node, incident edge type) or 3 (start node, incident edge type, adjacent node type) parameters)");
                return new SequenceExpressionAdjacentIncident(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), SequenceExpressionType.IncidentEdges);
            }
            else if(functionName == "incoming" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 1 || argExprs.Count > 3)
                    throw new ParseException("\"" + functionName + "\" expects 1 (start node only) or 2 (start node, incident edge type) or 3 (start node, incident edge type, adjacent node type) parameters)");
                return new SequenceExpressionAdjacentIncident(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), SequenceExpressionType.IncomingEdges);
            }
            else if(functionName == "outgoing" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 1 || argExprs.Count > 3)
                    throw new ParseException("\"" + functionName + "\" expects 1 (start node only) or 2 (start node, incident edge type) or 3 (start node, incident edge type, adjacent node type) parameters)");
                return new SequenceExpressionAdjacentIncident(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), SequenceExpressionType.OutgoingEdges);
            }
            else if(functionName == "reachable" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 1 || argExprs.Count > 3)
                    throw new ParseException("\"" + functionName + "\" expects 1 (start node only) or 2 (start node, incident edge type) or 3 (start node, incident edge type, adjacent node type) parameters)");
                return new SequenceExpressionReachable(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), SequenceExpressionType.ReachableNodes);
            }
            else if(functionName == "reachableIncoming" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 1 || argExprs.Count > 3)
                    throw new ParseException("\"" + functionName + "\" expects 1 (start node only) or 2 (start node, incident edge type) or 3 (start node, incident edge type, adjacent node type) parameters)");
                return new SequenceExpressionReachable(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), SequenceExpressionType.ReachableNodesViaIncoming);
            }
            else if(functionName == "reachableOutgoing" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 1 || argExprs.Count > 3)
                    throw new ParseException("\"" + functionName + "\" expects 1 (start node only) or 2 (start node, incident edge type) or 3 (start node, incident edge type, adjacent node type) parameters)");
                return new SequenceExpressionReachable(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), SequenceExpressionType.ReachableNodesViaOutgoing);
            }
            else if(functionName == "reachableEdges" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 1 || argExprs.Count > 3)
                    throw new ParseException("\"" + functionName + "\" expects 1 (start node only) or 2 (start node, incident edge type) or 3 (start node, incident edge type, adjacent node type) parameters)");
                return new SequenceExpressionReachable(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), SequenceExpressionType.ReachableEdges);
            }
            else if(functionName == "reachableEdgesIncoming" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 1 || argExprs.Count > 3)
                    throw new ParseException("\"" + functionName + "\" expects 1 (start node only) or 2 (start node, incident edge type) or 3 (start node, incident edge type, adjacent node type) parameters)");
                return new SequenceExpressionReachable(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), SequenceExpressionType.ReachableEdgesViaIncoming);
            }
            else if(functionName == "reachableEdgesOutgoing" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 1 || argExprs.Count > 3)
                    throw new ParseException("\"" + functionName + "\" expects 1 (start node only) or 2 (start node, incident edge type) or 3 (start node, incident edge type, adjacent node type) parameters)");
                return new SequenceExpressionReachable(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), SequenceExpressionType.ReachableEdgesViaOutgoing);
            }
            else if(functionName == "boundedReachable" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 2 || argExprs.Count > 4)
                    throw new ParseException("\"" + functionName + "\" expects 2 (start node, max depth) or 3 (start node, max depth, incident edge type) or 4 (start node, max depth, incident edge type, adjacent node type) parameters)");
                return new SequenceExpressionBoundedReachable(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), getArgument(argExprs, 3), SequenceExpressionType.BoundedReachableNodes);
            }
            else if(functionName == "boundedReachableIncoming" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 2 || argExprs.Count > 4)
                    throw new ParseException("\"" + functionName + "\" expects 2 (start node, max depth) or 3 (start node, max depth, incident edge type) or 4 (start node, max depth, incident edge type, adjacent node type) parameters)");
                return new SequenceExpressionBoundedReachable(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), getArgument(argExprs, 3), SequenceExpressionType.BoundedReachableNodesViaIncoming);
            }
            else if(functionName == "boundedReachableOutgoing" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 2 || argExprs.Count > 4)
                    throw new ParseException("\"" + functionName + "\" expects 2 (start node, max depth) or 3 (start node, max depth, incident edge type) or 4 (start node, max depth, incident edge type, adjacent node type) parameters)");
                return new SequenceExpressionBoundedReachable(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), getArgument(argExprs, 3), SequenceExpressionType.BoundedReachableNodesViaOutgoing);
            }
            else if(functionName == "boundedReachableEdges" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 2 || argExprs.Count > 4)
                    throw new ParseException("\"" + functionName + "\" expects 2 (start node, max depth) or 3 (start node, max depth, incident edge type) or 4 (start node, max depth, incident edge type, adjacent node type) parameters)");
                return new SequenceExpressionBoundedReachable(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), getArgument(argExprs, 3), SequenceExpressionType.BoundedReachableEdges);
            }
            else if(functionName == "boundedReachableEdgesIncoming" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 2 || argExprs.Count > 4)
                    throw new ParseException("\"" + functionName + "\" expects 2 (start node, max depth) or 3 (start node, max depth, incident edge type) or 4 (start node, max depth, incident edge type, adjacent node type) parameters)");
                return new SequenceExpressionBoundedReachable(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), getArgument(argExprs, 3), SequenceExpressionType.BoundedReachableEdgesViaIncoming);
            }
            else if(functionName == "boundedReachableEdgesOutgoing" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 2 || argExprs.Count > 4)
                    throw new ParseException("\"" + functionName + "\" expects 2 (start node, max depth) or 3 (start node, max depth, incident edge type) or 4 (start node, max depth, incident edge type, adjacent node type) parameters)");
                return new SequenceExpressionBoundedReachable(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), getArgument(argExprs, 3), SequenceExpressionType.BoundedReachableEdgesViaOutgoing);
            }
            else if(functionName == "boundedReachableWithRemainingDepth" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 2 || argExprs.Count > 4)
                    throw new ParseException("\"" + functionName + "\" expects 2 (start node, max depth) or 3 (start node, max depth, incident edge type) or 4 (start node, max depth, incident edge type, adjacent node type) parameters)");
                return new SequenceExpressionBoundedReachableWithRemainingDepth(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), getArgument(argExprs, 3), SequenceExpressionType.BoundedReachableNodesWithRemainingDepth);
            }
            else if(functionName == "boundedReachableWithRemainingDepthIncoming" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 2 || argExprs.Count > 4)
                    throw new ParseException("\"" + functionName + "\" expects 2 (start node, max depth) or 3 (start node, max depth, incident edge type) or 4 (start node, max depth, incident edge type, adjacent node type) parameters)");
                return new SequenceExpressionBoundedReachableWithRemainingDepth(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), getArgument(argExprs, 3), SequenceExpressionType.BoundedReachableNodesWithRemainingDepthViaIncoming);
            }
            else if(functionName == "boundedReachableWithRemainingDepthOutgoing" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 2 || argExprs.Count > 4)
                    throw new ParseException("\"" + functionName + "\" expects 2 (start node, max depth) or 3 (start node, max depth, incident edge type) or 4 (start node, max depth, incident edge type, adjacent node type) parameters)");
                return new SequenceExpressionBoundedReachableWithRemainingDepth(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), getArgument(argExprs, 3), SequenceExpressionType.BoundedReachableNodesWithRemainingDepthViaOutgoing);
            }
            else if(functionName == "countAdjacent" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 1 || argExprs.Count > 3)
                    throw new ParseException("\"" + functionName + "\" expects 1 (start node only) or 2 (start node, incident edge type) or 3 (start node, incident edge type, adjacent node type) parameters)");
                return new SequenceExpressionCountAdjacentIncident(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), SequenceExpressionType.CountAdjacentNodes);
            }
            else if(functionName == "countAdjacentIncoming" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 1 || argExprs.Count > 3)
                    throw new ParseException("\"" + functionName + "\" expects 1 (start node only) or 2 (start node, incident edge type) or 3 (start node, incident edge type, adjacent node type) parameters)");
                return new SequenceExpressionCountAdjacentIncident(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), SequenceExpressionType.CountAdjacentNodesViaIncoming);
            }
            else if(functionName == "countAdjacentOutgoing" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 1 || argExprs.Count > 3)
                    throw new ParseException("\"" + functionName + "\" expects 1 (start node only) or 2 (start node, incident edge type) or 3 (start node, incident edge type, adjacent node type) parameters)");
                return new SequenceExpressionCountAdjacentIncident(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), SequenceExpressionType.CountAdjacentNodesViaOutgoing);
            }
            else if(functionName == "countIncident" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 1 || argExprs.Count > 3)
                    throw new ParseException("\"" + functionName + "\" expects 1 (start node only) or 2 (start node, incident edge type) or 3 (start node, incident edge type, adjacent node type) parameters)");
                return new SequenceExpressionCountAdjacentIncident(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), SequenceExpressionType.CountIncidentEdges);
            }
            else if(functionName == "countIncoming" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 1 || argExprs.Count > 3)
                    throw new ParseException("\"" + functionName + "\" expects 1 (start node only) or 2 (start node, incident edge type) or 3 (start node, incident edge type, adjacent node type) parameters)");
                return new SequenceExpressionCountAdjacentIncident(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), SequenceExpressionType.CountIncomingEdges);
            }
            else if(functionName == "countOutgoing" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 1 || argExprs.Count > 3)
                    throw new ParseException("\"" + functionName + "\" expects 1 (start node only) or 2 (start node, incident edge type) or 3 (start node, incident edge type, adjacent node type) parameters)");
                return new SequenceExpressionCountAdjacentIncident(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), SequenceExpressionType.CountOutgoingEdges);
            }
            else if(functionName == "countReachable" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 1 || argExprs.Count > 3)
                    throw new ParseException("\"" + functionName + "\" expects 1 (start node only) or 2 (start node, incident edge type) or 3 (start node, incident edge type, adjacent node type) parameters)");
                return new SequenceExpressionCountReachable(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), SequenceExpressionType.CountReachableNodes);
            }
            else if(functionName == "countReachableIncoming" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 1 || argExprs.Count > 3)
                    throw new ParseException("\"" + functionName + "\" expects 1 (start node only) or 2 (start node, incident edge type) or 3 (start node, incident edge type, adjacent node type) parameters)");
                return new SequenceExpressionCountReachable(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), SequenceExpressionType.CountReachableNodesViaIncoming);
            }
            else if(functionName == "countReachableOutgoing" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 1 || argExprs.Count > 3)
                    throw new ParseException("\"" + functionName + "\" expects 1 (start node only) or 2 (start node, incident edge type) or 3 (start node, incident edge type, adjacent node type) parameters)");
                return new SequenceExpressionCountReachable(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), SequenceExpressionType.CountReachableNodesViaOutgoing);
            }
            else if(functionName == "countReachableEdges" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 1 || argExprs.Count > 3)
                    throw new ParseException("\"" + functionName + "\" expects 1 (start node only) or 2 (start node, incident edge type) or 3 (start node, incident edge type, adjacent node type) parameters)");
                return new SequenceExpressionCountReachable(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), SequenceExpressionType.CountReachableEdges);
            }
            else if(functionName == "countReachableEdgesIncoming" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 1 || argExprs.Count > 3)
                    throw new ParseException("\"" + functionName + "\" expects 1 (start node only) or 2 (start node, incident edge type) or 3 (start node, incident edge type, adjacent node type) parameters)");
                return new SequenceExpressionCountReachable(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), SequenceExpressionType.CountReachableEdgesViaIncoming);
            }
            else if(functionName == "countReachableEdgesOutgoing" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 1 || argExprs.Count > 3)
                    throw new ParseException("\"" + functionName + "\" expects 1 (start node only) or 2 (start node, incident edge type) or 3 (start node, incident edge type, adjacent node type) parameters)");
                return new SequenceExpressionCountReachable(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), SequenceExpressionType.CountReachableEdgesViaOutgoing);
            }
            else if(functionName == "countBoundedReachable" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 2 || argExprs.Count > 4)
                    throw new ParseException("\"" + functionName + "\" expects 2 (start node, max depth) or 3 (start node, max depth, incident edge type) or 4 (start node, max depth, incident edge type, adjacent node type) parameters)");
                return new SequenceExpressionCountBoundedReachable(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), getArgument(argExprs, 3), SequenceExpressionType.CountBoundedReachableNodes);
            }
            else if(functionName == "countBoundedReachableIncoming" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 2 || argExprs.Count > 4)
                    throw new ParseException("\"" + functionName + "\" expects 2 (start node, max depth) or 3 (start node, max depth, incident edge type) or 4 (start node, max depth, incident edge type, adjacent node type) parameters)");
                return new SequenceExpressionCountBoundedReachable(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), getArgument(argExprs, 3), SequenceExpressionType.CountBoundedReachableNodesViaIncoming);
            }
            else if(functionName == "countBoundedReachableOutgoing" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 2 || argExprs.Count > 4)
                    throw new ParseException("\"" + functionName + "\" expects 2 (start node, max depth) or 3 (start node, max depth, incident edge type) or 4 (start node, max depth, incident edge type, adjacent node type) parameters)");
                return new SequenceExpressionCountBoundedReachable(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), getArgument(argExprs, 3), SequenceExpressionType.CountBoundedReachableNodesViaOutgoing);
            }
            else if(functionName == "countBoundedReachableEdges" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 2 || argExprs.Count > 4)
                    throw new ParseException("\"" + functionName + "\" expects 2 (start node, max depth) or 3 (start node, max depth, incident edge type) or 4 (start node, max depth, incident edge type, adjacent node type) parameters)");
                return new SequenceExpressionCountBoundedReachable(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), getArgument(argExprs, 3), SequenceExpressionType.CountBoundedReachableEdges);
            }
            else if(functionName == "countBoundedReachableEdgesIncoming" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 2 || argExprs.Count > 4)
                    throw new ParseException("\"" + functionName + "\" expects 2 (start node, max depth) or 3 (start node, max depth, incident edge type) or 4 (start node, max depth, incident edge type, adjacent node type) parameters)");
                return new SequenceExpressionCountBoundedReachable(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), getArgument(argExprs, 3), SequenceExpressionType.CountBoundedReachableEdgesViaIncoming);
            }
            else if(functionName == "countBoundedReachableEdgesOutgoing" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 2 || argExprs.Count > 4)
                    throw new ParseException("\"" + functionName + "\" expects 2 (start node, max depth) or 3 (start node, max depth, incident edge type) or 4 (start node, max depth, incident edge type, adjacent node type) parameters)");
                return new SequenceExpressionCountBoundedReachable(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), getArgument(argExprs, 3), SequenceExpressionType.CountBoundedReachableEdgesViaOutgoing);
            }
            else if(functionName == "isAdjacent" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 2 || argExprs.Count > 4)
                    throw new ParseException("\"" + functionName + "\" expects 2 (start node, end node) or 3 (start node, end node, incident edge type) or 4 (start node, end node, incident edge type, adjacent node type) parameters)");
                return new SequenceExpressionIsAdjacentIncident(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), getArgument(argExprs, 3), SequenceExpressionType.IsAdjacentNodes);
            }
            else if(functionName == "isAdjacentIncoming" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 2 || argExprs.Count > 4)
                    throw new ParseException("\"" + functionName + "\" expects 2 (start node, end node) or 3 (start node, end node, incident edge type) or 4 (start node, end node, incident edge type, adjacent node type) parameters)");
                return new SequenceExpressionIsAdjacentIncident(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), getArgument(argExprs, 3), SequenceExpressionType.IsAdjacentNodesViaIncoming);
            }
            else if(functionName == "isAdjacentOutgoing" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 2 || argExprs.Count > 4)
                    throw new ParseException("\"" + functionName + "\" expects 2 (start node, end node) or 3 (start node, end node, incident edge type) or 4 (start node, end node, incident edge type, adjacent node type) parameters)");
                return new SequenceExpressionIsAdjacentIncident(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), getArgument(argExprs, 3), SequenceExpressionType.IsAdjacentNodesViaOutgoing);
            }
            else if(functionName == "isIncident" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 2 || argExprs.Count > 4)
                    throw new ParseException("\"" + functionName + "\" expects 2 (start node, end node) or 3 (start node, end node, incident edge type) or 4 (start node, end node, incident edge type, adjacent node type) parameters)");
                return new SequenceExpressionIsAdjacentIncident(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), getArgument(argExprs, 3), SequenceExpressionType.IsIncidentEdges);
            }
            else if(functionName == "isIncoming" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 2 || argExprs.Count > 4)
                    throw new ParseException("\"" + functionName + "\" expects 2 (start node, end node) or 3 (start node, end node, incident edge type) or 4 (start node, end node, incident edge type, adjacent node type) parameters)");
                return new SequenceExpressionIsAdjacentIncident(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), getArgument(argExprs, 3), SequenceExpressionType.IsIncomingEdges);
            }
            else if(functionName == "isOutgoing" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 2 || argExprs.Count > 4)
                    throw new ParseException("\"" + functionName + "\" expects 2 (start node, end node) or 3 (start node, end node, incident edge type) or 4 (start node, end node, incident edge type, adjacent node type) parameters)");
                return new SequenceExpressionIsAdjacentIncident(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), getArgument(argExprs, 3), SequenceExpressionType.IsOutgoingEdges);
            }
            else if(functionName == "isReachable" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 2 || argExprs.Count > 4)
                    throw new ParseException("\"" + functionName + "\" expects 2 (start node, end node) or 3 (start node, end node, incident edge type) or 4 (start node, end node, incident edge type, adjacent node type) parameters)");
                return new SequenceExpressionIsReachable(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), getArgument(argExprs, 3), SequenceExpressionType.IsReachableNodes);
            }
            else if(functionName == "isReachableIncoming" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 2 || argExprs.Count > 4)
                    throw new ParseException("\"" + functionName + "\" expects 2 (start node, end node) or 3 (start node, end node, incident edge type) or 4 (start node, end node, incident edge type, adjacent node type) parameters)");
                return new SequenceExpressionIsReachable(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), getArgument(argExprs, 3), SequenceExpressionType.IsReachableNodesViaIncoming);
            }
            else if(functionName == "isReachableOutgoing" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 2 || argExprs.Count > 4)
                    throw new ParseException("\"" + functionName + "\" expects 2 (start node, end node) or 3 (start node, end node, incident edge type) or 4 (start node, end node, incident edge type, adjacent node type) parameters)");
                return new SequenceExpressionIsReachable(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), getArgument(argExprs, 3), SequenceExpressionType.IsReachableNodesViaOutgoing);
            }
            else if(functionName == "isReachableEdges" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 2 || argExprs.Count > 4)
                    throw new ParseException("\"" + functionName + "\" expects 2 (start node, end node) or 3 (start node, end node, incident edge type) or 4 (start node, end node, incident edge type, adjacent node type) parameters)");
                return new SequenceExpressionIsReachable(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), getArgument(argExprs, 3), SequenceExpressionType.IsReachableEdges);
            }
            else if(functionName == "isReachableEdgesIncoming" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 2 || argExprs.Count > 4)
                    throw new ParseException("\"" + functionName + "\" expects 2 (start node, end node) or 3 (start node, end node, incident edge type) or 4 (start node, end node, incident edge type, adjacent node type) parameters)");
                return new SequenceExpressionIsReachable(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), getArgument(argExprs, 3), SequenceExpressionType.IsReachableEdgesViaIncoming);
            }
            else if(functionName == "isReachableEdgesOutgoing" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 2 || argExprs.Count > 4)
                    throw new ParseException("\"" + functionName + "\" expects 2 (start node, end node) or 3 (start node, end node, incident edge type) or 4 (start node, end node, incident edge type, adjacent node type) parameters)");
                return new SequenceExpressionIsReachable(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), getArgument(argExprs, 3), SequenceExpressionType.IsReachableEdgesViaOutgoing);
            }
            else if(functionName == "isBoundedReachable" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 3 || argExprs.Count > 5)
                    throw new ParseException("\"" + functionName + "\" expects 3 (start node, end node, depth) or 4 (start node, end node, depth, incident edge type) or 5 (start node, end node, depth, incident edge type, adjacent node type) parameters)");
                return new SequenceExpressionIsBoundedReachable(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), getArgument(argExprs, 3), getArgument(argExprs, 4), SequenceExpressionType.IsBoundedReachableNodes);
            }
            else if(functionName == "isBoundedReachableIncoming" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 3 || argExprs.Count > 5)
                    throw new ParseException("\"" + functionName + "\" expects 3 (start node, end node, depth) or 4 (start node, end node, depth, incident edge type) or 5 (start node, end node, depth, incident edge type, adjacent node type) parameters)");
                return new SequenceExpressionIsBoundedReachable(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), getArgument(argExprs, 3), getArgument(argExprs, 4), SequenceExpressionType.IsBoundedReachableNodesViaIncoming);
            }
            else if(functionName == "isBoundedReachableOutgoing" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 3 || argExprs.Count > 5)
                    throw new ParseException("\"" + functionName + "\" expects 3 (start node, end node, depth) or 4 (start node, end node, depth, incident edge type) or 5 (start node, end node, depth, incident edge type, adjacent node type) parameters)");
                return new SequenceExpressionIsBoundedReachable(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), getArgument(argExprs, 3), getArgument(argExprs, 4), SequenceExpressionType.IsBoundedReachableNodesViaOutgoing);
            }
            else if(functionName == "isBoundedReachableEdges" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 3 || argExprs.Count > 5)
                    throw new ParseException("\"" + functionName + "\" expects 3 (start node, end node, depth) or 4 (start node, end node, depth, incident edge type) or 5 (start node, end node, depth, incident edge type, adjacent node type) parameters)");
                return new SequenceExpressionIsBoundedReachable(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), getArgument(argExprs, 3), getArgument(argExprs, 4), SequenceExpressionType.IsBoundedReachableEdges);
            }
            else if(functionName == "isBoundedReachableEdgesIncoming" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 3 || argExprs.Count > 5)
                    throw new ParseException("\"" + functionName + "\" expects 3 (start node, end node, depth) or 4 (start node, end node, depth, incident edge type) or 5 (start node, end node, depth, incident edge type, adjacent node type) parameters)");
                return new SequenceExpressionIsBoundedReachable(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), getArgument(argExprs, 3), getArgument(argExprs, 4), SequenceExpressionType.IsBoundedReachableEdgesViaIncoming);
            }
            else if(functionName == "isBoundedReachableEdgesOutgoing" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 3 || argExprs.Count > 5)
                    throw new ParseException("\"" + functionName + "\" expects 3 (start node, end node, depth) or 4 (start node, end node, depth, incident edge type) or 5 (start node, end node, depth, incident edge type, adjacent node type) parameters)");
                return new SequenceExpressionIsBoundedReachable(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), getArgument(argExprs, 3), getArgument(argExprs, 4), SequenceExpressionType.IsBoundedReachableEdgesViaOutgoing);
            }
            else if(functionName == "inducedSubgraph" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count != 1)
                    throw new ParseException("\"" + functionName + "\" expects 1 parameter (the set of nodes to construct the induced subgraph from)");
                return new SequenceExpressionInducedSubgraph(getArgument(argExprs, 0));
            }
            else if(functionName == "definedSubgraph" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count != 1)
                    throw new ParseException("\"" + functionName + "\" expects 1 parameter (the set of edges to construct the defined subgraph from)");
                return new SequenceExpressionDefinedSubgraph(getArgument(argExprs, 0));
            }
            else if(functionName == "equalsAny" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count != 2)
                    throw new ParseException("\"" + functionName + "\" expects 2 parameters (the subgraph, and the set of subgraphs to compare against)");
                return new SequenceExpressionEqualsAny(getArgument(argExprs, 0), getArgument(argExprs, 1), true);
            }
            else if(functionName == "equalsAnyStructurally" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count != 2)
                    throw new ParseException("\"" + functionName + "\" expects 2 parameters (the subgraph, and the set of subgraphs to compare against)");
                return new SequenceExpressionEqualsAny(getArgument(argExprs, 0), getArgument(argExprs, 1), false);
            }
            else if(functionName == "getEquivalent" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count != 2)
                    throw new ParseException("\"" + functionName + "\" expects 2 parameters (the subgraph, and the set of subgraphs to compare against)");
                return new SequenceExpressionGetEquivalent(getArgument(argExprs, 0), getArgument(argExprs, 1), true);
            }
            else if(functionName == "getEquivalentStructurally" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count != 2)
                    throw new ParseException("\"" + functionName + "\" expects 2 parameters (the subgraph, and the set of subgraphs to compare against)");
                return new SequenceExpressionGetEquivalent(getArgument(argExprs, 0), getArgument(argExprs, 1), false);
            }
            else if(functionName == "source" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count != 1)
                    throw new ParseException("\"" + functionName + "\" expects 1 parameter (the edge to get the source node from)");
                return new SequenceExpressionSource(getArgument(argExprs, 0));
            }
            else if(functionName == "target" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count != 1)
                    throw new ParseException("\"" + functionName + "\" expects 1 parameter (the edge to get the target node from)");
                return new SequenceExpressionTarget(getArgument(argExprs, 0));
            }
            else if(functionName == "opposite" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count != 2)
                    throw new ParseException("\"" + functionName + "\" expects 2 parameters (the edge and the node to get the opposite node from)");
                return new SequenceExpressionOpposite(getArgument(argExprs, 0), getArgument(argExprs, 1));
            }
            else if(functionName == "nameof" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count > 1)
                    throw new ParseException("\"" + functionName + "\" expects none (for the name of the current graph) or 1 parameter (for the name of the node/edge/subgraph given as parameter)");
                return new SequenceExpressionNameof(getArgument(argExprs, 0));
            }
            else if(functionName == "uniqueof" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count > 1)
                    throw new ParseException("\"" + functionName + "\" expects none (for the unique id of of the current graph) or 1 parameter (for the unique if of the node/edge/subgraph given as parameter)");
                return new SequenceExpressionUniqueof(getArgument(argExprs, 0));
            }
            else if(functionName == "typeof" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count != 1)
                    throw new ParseException("\"" + functionName + "\" expects 1 parameter (the entity to get the type of)");
                return new SequenceExpressionTypeof(getArgument(argExprs, 0));
            }
            else if(functionName == "exists" && package != null && package == "File")
            {
                if(argExprs.Count != 1)
                    throw new ParseException("\"File::exists\" expects 1 parameter (the path as string)");
                return new SequenceExpressionExistsFile(getArgument(argExprs, 0));
            }
            else if(functionName == "import" && package != null && package == "File")
            {
                if(argExprs.Count != 1)
                    throw new ParseException("\"File::import\" expects 1 parameter (the path as string to the grs file containing the subgraph to import)");
                return new SequenceExpressionImport(getArgument(argExprs, 0));
            }
            else if(functionName == "now" && package != null && package == "Time")
            {
                if(argExprs.Count > 0)
                    throw new ParseException("\"Time::now\" expects no parameters");
                return new SequenceExpressionNow();
            }
            else if(functionName == "copy" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count != 1)
                    throw new ParseException("\"" + functionName + "\" expects 1 parameter (the subgraph or match or container or class object or transient class object to copy)");
                return new SequenceExpressionCopy(getArgument(argExprs, 0), true);
            }
            else if(functionName == "clone" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count != 1)
                    throw new ParseException("\"" + functionName + "\" expects 1 parameter (the subgraph or match or container or class object or transient class object to clone)");
                return new SequenceExpressionCopy(getArgument(argExprs, 0), false);
            }
            else if(functionName == "random" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count > 1)
                    throw new ParseException("\"" + functionName + "\" expects none (returns double in [0..1[) or 1 parameter (returns int in [0..parameter[)");
                return new SequenceExpressionRandom(getArgument(argExprs, 0));
            }
            else if(functionName == "canonize" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count != 1)
                    throw new ParseException("\"" + functionName + "\" expects 1 parameter (the graph to generate the canonical string representation for)");
                return new SequenceExpressionCanonize(getArgument(argExprs, 0));
            }
            else if(functionName == "nodeByName" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 1 || argExprs.Count > 2)
                    throw new ParseException("\"" + functionName + "\" expects 1 parameter (the name of the node to retrieve) or 2 parameters (name of node, type of node)");
                return new SequenceExpressionNodeByName(getArgument(argExprs, 0), getArgument(argExprs, 1));
            }
            else if(functionName == "edgeByName" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 1 || argExprs.Count > 2)
                    throw new ParseException("\"" + functionName + "\" expects 1 parameter (the name of the edge to retrieve) or 2 parameters (name of edge, type of edge)");
                return new SequenceExpressionEdgeByName(getArgument(argExprs, 0), getArgument(argExprs, 1));
            }
            else if(functionName == "nodeByUnique" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 1 || argExprs.Count > 2)
                    throw new ParseException("\"" + functionName + "\" expects 1 parameter (the unique id of the node to retrieve) or 2 parameters (unique id of node, type of node)");
                return new SequenceExpressionNodeByUnique(getArgument(argExprs, 0), getArgument(argExprs, 1));
            }
            else if(functionName == "edgeByUnique" && PackageIsNullOrGlobal(package))
            {
                if(argExprs.Count < 1 || argExprs.Count > 2)
                    throw new ParseException("\"" + functionName + "\" expects 1 parameter (the unique if of the edge to retrieve) or 2 parameters (unique id of edge, type of edge)");
                return new SequenceExpressionEdgeByUnique(getArgument(argExprs, 0), getArgument(argExprs, 1));
            }
            else if(functionName == "min" && package != null && package == "Math")
            {
                if(argExprs.Count != 2)
                    throw new ParseException("\"Math::min\" expects 2 parameters");
                return new SequenceExpressionMathMin(getArgument(argExprs, 0), getArgument(argExprs, 1));
            }
            else if(functionName == "max" && package != null && package == "Math")
            {
                if(argExprs.Count != 2)
                    throw new ParseException("\"Math::max\" expects 2 parameters");
                return new SequenceExpressionMathMax(getArgument(argExprs, 0), getArgument(argExprs, 1));
            }
            else if(functionName == "abs" && package != null && package == "Math")
            {
                if(argExprs.Count != 1)
                    throw new ParseException("\"Math::abs\" expects 1 parameter");
                return new SequenceExpressionMathAbs(getArgument(argExprs, 0));
            }
            else if(functionName == "ceil" && package != null && package == "Math")
            {
                if(argExprs.Count != 1)
                    throw new ParseException("\"Math::ceil\" expects 1 parameter");
                return new SequenceExpressionMathCeil(getArgument(argExprs, 0));
            }
            else if(functionName == "floor" && package != null && package == "Math")
            {
                if(argExprs.Count != 1)
                    throw new ParseException("\"Math::floor\" expects 1 parameter");
                return new SequenceExpressionMathFloor(getArgument(argExprs, 0));
            }
            else if(functionName == "round" && package != null && package == "Math")
            {
                if(argExprs.Count != 1)
                    throw new ParseException("\"Math::round\" expects 1 parameter");
                return new SequenceExpressionMathRound(getArgument(argExprs, 0));
            }
            else if(functionName == "truncate" && package != null && package == "Math")
            {
                if(argExprs.Count != 1)
                    throw new ParseException("\"Math::truncate\" expects 1 parameter");
                return new SequenceExpressionMathTruncate(getArgument(argExprs, 0));
            }
            else if(functionName == "sqr" && package != null && package == "Math")
            {
                if(argExprs.Count != 1)
                    throw new ParseException("\"Math::sqr\" expects 1 parameter");
                return new SequenceExpressionMathSqr(getArgument(argExprs, 0));
            }
            else if(functionName == "sqrt" && package != null && package == "Math")
            {
                if(argExprs.Count != 1)
                    throw new ParseException("\"Math::sqrt\" expects 1 parameter");
                return new SequenceExpressionMathSqrt(getArgument(argExprs, 0));
            }
            else if(functionName == "pow" && package != null && package == "Math")
            {
                if(argExprs.Count != 1 && argExprs.Count != 2)
                    throw new ParseException("\"Math::pow\" expects 1 or 2 parameters (first to the power of the second, or e to the power of the first)");
                return new SequenceExpressionMathPow(getArgument(argExprs, 0), getArgument(argExprs, 1));
            }
            else if(functionName == "log" && package != null && package == "Math")
            {
                if(argExprs.Count != 1 && argExprs.Count != 2)
                    throw new ParseException("\"Math::log\" expects 1 or 2 parameters (logarithm of the first regarding the base of the second, or regarding the base of e)");
                return new SequenceExpressionMathLog(getArgument(argExprs, 0), getArgument(argExprs, 1));
            }
            else if(functionName == "sgn" && package != null && package == "Math")
            {
                if(argExprs.Count != 1)
                    throw new ParseException("\"Math::sgn\" expects 1 parameter");
                return new SequenceExpressionMathSgn(getArgument(argExprs, 0));
            }
            else if(functionName == "sin" && package != null && package == "Math")
            {
                if(argExprs.Count != 1)
                    throw new ParseException("\"Math::sin\" expects 1 parameter");
                return new SequenceExpressionMathSin(getArgument(argExprs, 0));
            }
            else if(functionName == "cos" && package != null && package == "Math")
            {
                if(argExprs.Count != 1)
                    throw new ParseException("\"Math::cos\" expects 1 parameter");
                return new SequenceExpressionMathCos(getArgument(argExprs, 0));
            }
            else if(functionName == "tan" && package != null && package == "Math")
            {
                if(argExprs.Count != 1)
                    throw new ParseException("\"Math::tan\" expects 1 parameter");
                return new SequenceExpressionMathTan(getArgument(argExprs, 0));
            }
            else if(functionName == "arcsin" && package != null && package == "Math")
            {
                if(argExprs.Count != 1)
                    throw new ParseException("\"Math::arcsin\" expects 1 parameter");
                return new SequenceExpressionMathArcSin(getArgument(argExprs, 0));
            }
            else if(functionName == "arccos" && package != null && package == "Math")
            {
                if(argExprs.Count != 1)
                    throw new ParseException("\"Math::arccos\" expects 1 parameter");
                return new SequenceExpressionMathArcCos(getArgument(argExprs, 0));
            }
            else if(functionName == "arctan" && package != null && package == "Math")
            {
                if(argExprs.Count != 1)
                    throw new ParseException("\"Math::arctan\" expects 1 parameter");
                return new SequenceExpressionMathArcTan(getArgument(argExprs, 0));
            }
            else if(functionName == "pi" && package != null && package == "Math")
            {
                if(argExprs.Count != 0)
                    throw new ParseException("\"Math::pi\" expects no parameter");
                return new SequenceExpressionMathPi();
            }
            else if(functionName == "e" && package != null && package == "Math")
            {
                if(argExprs.Count != 0)
                    throw new ParseException("\"Math::e\" expects no parameter");
                return new SequenceExpressionMathE();
            }
            else if(functionName == "byteMin" && package != null && package == "Math")
            {
                if(argExprs.Count != 0)
                    throw new ParseException("\"Math::byteMin\" expects no parameter");
                return new SequenceExpressionMathByteMin();
            }
            else if(functionName == "byteMax" && package != null && package == "Math")
            {
                if(argExprs.Count != 0)
                    throw new ParseException("\"Math::byteMax\" expects no parameter");
                return new SequenceExpressionMathByteMax();
            }
            else if(functionName == "shortMin" && package != null && package == "Math")
            {
                if(argExprs.Count != 0)
                    throw new ParseException("\"Math::shortMin\" expects no parameter");
                return new SequenceExpressionMathShortMin();
            }
            else if(functionName == "shortMax" && package != null && package == "Math")
            {
                if(argExprs.Count != 0)
                    throw new ParseException("\"Math::shortMax\" expects no parameter");
                return new SequenceExpressionMathShortMax();
            }
            else if(functionName == "intMin" && package != null && package == "Math")
            {
                if(argExprs.Count != 0)
                    throw new ParseException("\"Math::intMin\" expects no parameter");
                return new SequenceExpressionMathIntMin();
            }
            else if(functionName == "intMax" && package != null && package == "Math")
            {
                if(argExprs.Count != 0)
                    throw new ParseException("\"Math::intMax\" expects no parameter");
                return new SequenceExpressionMathIntMax();
            }
            else if(functionName == "longMin" && package != null && package == "Math")
            {
                if(argExprs.Count != 0)
                    throw new ParseException("\"Math::longMin\" expects no parameter");
                return new SequenceExpressionMathLongMin();
            }
            else if(functionName == "longMax" && package != null && package == "Math")
            {
                if(argExprs.Count != 0)
                    throw new ParseException("\"Math::longMax\" expects no parameter");
                return new SequenceExpressionMathLongMax();
            }
            else if(functionName == "floatMin" && package != null && package == "Math")
            {
                if(argExprs.Count != 0)
                    throw new ParseException("\"Math::floatMin\" expects no parameter");
                return new SequenceExpressionMathFloatMin();
            }
            else if(functionName == "floatMax" && package != null && package == "Math")
            {
                if(argExprs.Count != 0)
                    throw new ParseException("\"Math::floatMax\" expects no parameter");
                return new SequenceExpressionMathFloatMax();
            }
            else if(functionName == "doubleMin" && package != null && package == "Math")
            {
                if(argExprs.Count != 0)
                    throw new ParseException("\"Math::doubleMin\" expects no parameter");
                return new SequenceExpressionMathDoubleMin();
            }
            else if(functionName == "doubleMax" && package != null && package == "Math")
            {
                if(argExprs.Count != 0)
                    throw new ParseException("\"Math::doubleMax\" expects no parameter");
                return new SequenceExpressionMathDoubleMax();
            }
            else
            {
                if(IsFunctionName(functionName, package))
                    return CreateSequenceExpressionFunctionCallUserFunction(functionName, package, argExprs);
                else
                {
                    if(functionName == "valloc" || functionName == "add" || functionName == "retype" || functionName == "insertInduced" || functionName == "insertDefined")
                        throw new ParseException("\"" + functionName + "\" is a procedure, call with (var)=" + functionName + "();");
                    else
                        throw new ParseException("Unknown function name: \"" + functionName + "\"!"
                            + " (available are nodes|edges|empty|size|adjacent|adjacentIncoming|adjacentOutgoing|incident|incoming|outgoing"
                            + "|reachable|reachableIncoming|reachableOutgoing|reachableEdges|reachableEdgesIncoming|reachableEdgesOutgoing"
                            + "|boundedReachable|boundedReachableIncoming|boundedReachableOutgoing|boundedReachableEdges|boundedReachableEdgesIncoming|boundedReachableEdgesOutgoing"
                            + "|boundedReachableWithRemainingDepth|boundedReachableWithRemainingDepthIncoming|boundedReachableWithRemainingDepthOutgoing"
                            + "|countNodes|countEdges|countAdjacent|countAdjacentIncoming|countAdjacentOutgoing|countIncident|countIncoming|countOutgoing"
                            + "|countReachable|countReachableIncoming|countReachableOutgoing|countReachableEdges|countReachableEdgesIncoming|countReachableEdgesOutgoing"
                            + "|countBoundedReachable|countBoundedReachableIncoming|countBoundedReachableOutgoing|countBoundedReachableEdges|countBoundedReachableEdgesIncoming|countBoundedReachableEdgesOutgoing"
                            + "|isAdjacent|isAdjacentIncoming|isAdjacentOutgoing|isIncident|isIncoming|isOutgoing"
                            + "|isReachable|isReachableIncoming|isReachableOutgoing|isReachableEdges|isReachableEdgeIncoming|isReachableEdgesOutgoing"
                            + "|isBoundedReachable|isBoundedReachableIncoming|isBoundedReachableOutgoing|isBoundedReachableEdges|isBoundedReachableEdgeIncoming|isBoundedReachableEdgesOutgoing"
                            + "|inducedSubgraph|definedSubgraph|equalsAny|equalsAnyStructurally|getEquivalent|getEquivalentStructurally"
                            + "|source|target|opposite|nameof|uniqueof"
                            + "|File::exists|File::import|copy|random|canonize|nodeByName|edgeByName|nodeByUnique|edgeByUnique|typeof"
                            + " or one of the functionNames defined in the .grg:" + GetFunctionNames() + ")");
                }
            }
        }

        public SequenceExpression CreateSequenceExpressionFunctionMethodCall(SequenceExpression targetExpr,
            String functionMethodName, List<SequenceExpression> argExprs)
        {
            if(functionMethodName == "size")
            {
                if(argExprs.Count != 0)
                    throw new ParseException("\"" + functionMethodName + "\" expects no parameters)");
                return new SequenceExpressionContainerSize(targetExpr);
            }
            else if(functionMethodName == "empty")
            {
                if(argExprs.Count != 0)
                    throw new ParseException("\"" + functionMethodName + "\" expects no parameters)");
                return new SequenceExpressionContainerEmpty(targetExpr);
            }
            else if(functionMethodName == "peek")
            {
                if(argExprs.Count != 0 && argExprs.Count != 1)
                    throw new ParseException("\"" + functionMethodName + "\" expects none or one parameter)");
                return new SequenceExpressionContainerPeek(targetExpr, argExprs.Count != 0 ? argExprs[0] : null);
            }
            else if(functionMethodName == "sum")
            {
                if(argExprs.Count != 0)
                    throw new ParseException("\"" + functionMethodName + "\" expects no parameters)");
                return new SequenceExpressionArraySum(targetExpr);
            }
            else if(functionMethodName == "prod")
            {
                if(argExprs.Count != 0)
                    throw new ParseException("\"" + functionMethodName + "\" expects no parameters)");
                return new SequenceExpressionArrayProd(targetExpr);
            }
            else if(functionMethodName == "min")
            {
                if(argExprs.Count != 0)
                    throw new ParseException("\"" + functionMethodName + "\" expects no parameters)");
                return new SequenceExpressionArrayMin(targetExpr);
            }
            else if(functionMethodName == "max")
            {
                if(argExprs.Count != 0)
                    throw new ParseException("\"" + functionMethodName + "\" expects no parameters)");
                return new SequenceExpressionArrayMax(targetExpr);
            }
            else if(functionMethodName == "avg")
            {
                if(argExprs.Count != 0)
                    throw new ParseException("\"" + functionMethodName + "\" expects no parameters)");
                return new SequenceExpressionArrayAvg(targetExpr);
            }
            else if(functionMethodName == "med")
            {
                if(argExprs.Count != 0)
                    throw new ParseException("\"" + functionMethodName + "\" expects no parameters)");
                return new SequenceExpressionArrayMed(targetExpr);
            }
            else if(functionMethodName == "medUnordered")
            {
                if(argExprs.Count != 0)
                    throw new ParseException("\"" + functionMethodName + "\" expects no parameters)");
                return new SequenceExpressionArrayMedUnordered(targetExpr);
            }
            else if(functionMethodName == "var")
            {
                if(argExprs.Count != 0)
                    throw new ParseException("\"" + functionMethodName + "\" expects no parameters)");
                return new SequenceExpressionArrayVar(targetExpr);
            }
            else if(functionMethodName == "dev")
            {
                if(argExprs.Count != 0)
                    throw new ParseException("\"" + functionMethodName + "\" expects no parameters)");
                return new SequenceExpressionArrayDev(targetExpr);
            }
            else if(functionMethodName == "and")
            {
                if(argExprs.Count != 0)
                    throw new ParseException("\"" + functionMethodName + "\" expects no parameters)");
                return new SequenceExpressionArrayAnd(targetExpr);
            }
            else if(functionMethodName == "or")
            {
                if(argExprs.Count != 0)
                    throw new ParseException("\"" + functionMethodName + "\" expects no parameters)");
                return new SequenceExpressionArrayOr(targetExpr);
            }
            else if(functionMethodName == "orderAscending")
            {
                if(argExprs.Count != 0)
                    throw new ParseException("\"" + functionMethodName + "\" expects no parameters)");
                return new SequenceExpressionArrayOrderAscending(targetExpr);
            }
            else if(functionMethodName == "orderDescending")
            {
                if(argExprs.Count != 0)
                    throw new ParseException("\"" + functionMethodName + "\" expects no parameters)");
                return new SequenceExpressionArrayOrderDescending(targetExpr);
            }
            else if(functionMethodName == "group")
            {
                if(argExprs.Count != 0)
                    throw new ParseException("\"" + functionMethodName + "\" expects no parameters)");
                return new SequenceExpressionArrayGroup(targetExpr);
            }
            else if(functionMethodName == "keepOneForEach")
            {
                if(argExprs.Count != 0)
                    throw new ParseException("\"" + functionMethodName + "\" expects no parameters)");
                return new SequenceExpressionArrayKeepOneForEach(targetExpr);
            }
            else if(functionMethodName == "reverse")
            {
                if(argExprs.Count != 0)
                    throw new ParseException("\"" + functionMethodName + "\" expects no parameters)");
                return new SequenceExpressionArrayReverse(targetExpr);
            }
            else if(functionMethodName == "shuffle")
            {
                if(argExprs.Count != 0)
                    throw new ParseException("\"" + functionMethodName + "\" expects no parameters)");
                return new SequenceExpressionArrayShuffle(targetExpr);
            }
            else if(functionMethodName == "subarray")
            {
                if(argExprs.Count != 2)
                    throw new ParseException("\"" + functionMethodName + "\" expects 2 parameters)");
                return new SequenceExpressionArraySubarray(targetExpr, argExprs[0], argExprs[1]);
            }
            else if(functionMethodName == "subdeque")
            {
                if(argExprs.Count != 2)
                    throw new ParseException("\"" + functionMethodName + "\" expects 2 parameters)");
                return new SequenceExpressionDequeSubdeque(targetExpr, argExprs[0], argExprs[1]);
            }
            else if(functionMethodName == "asSet")
            {
                if(argExprs.Count != 0)
                    throw new ParseException("\"" + functionMethodName + "\" expects no parameters)");
                return new SequenceExpressionArrayOrDequeAsSet(targetExpr);
            }
            else if(functionMethodName == "domain")
            {
                if(argExprs.Count != 0)
                    throw new ParseException("\"" + functionMethodName + "\" expects no parameters)");
                return new SequenceExpressionMapDomain(targetExpr);
            }
            else if(functionMethodName == "range")
            {
                if(argExprs.Count != 0)
                    throw new ParseException("\"" + functionMethodName + "\" expects no parameters)");
                return new SequenceExpressionMapRange(targetExpr);
            }
            else if(functionMethodName == "asMap")
            {
                if(argExprs.Count != 0)
                    throw new ParseException("\"" + functionMethodName + "\" expects no parameters)");
                return new SequenceExpressionArrayAsMap(targetExpr);
            }
            else if(functionMethodName == "asDeque")
            {
                if(argExprs.Count != 0)
                    throw new ParseException("\"" + functionMethodName + "\" expects no parameters)");
                return new SequenceExpressionArrayAsDeque(targetExpr);
            }
            else if(functionMethodName == "asString")
            {
                if(argExprs.Count != 1)
                    throw new ParseException("\"" + functionMethodName + "\" expects 1 parameter)");
                return new SequenceExpressionArrayAsString(targetExpr, argExprs[0]);
            }
            else if(functionMethodName == "asArray")
            {
                if(argExprs.Count != 0 && argExprs.Count != 1)
                    throw new ParseException("\"" + functionMethodName + "\" expects none or one parameter)");
                if(argExprs.Count == 0)
                    return new SequenceExpressionContainerAsArray(targetExpr);
                else
                    return new SequenceExpressionStringAsArray(targetExpr, argExprs[0]);
            }
            else if(functionMethodName == "indexOf")
            {
                if(argExprs.Count != 1 && argExprs.Count != 2)
                    throw new ParseException("\"" + functionMethodName + "\" expects one or two parameters)");
                return new SequenceExpressionArrayOrDequeOrStringIndexOf(targetExpr, argExprs[0], argExprs.Count != 1 ? argExprs[1] : null);
            }
            else if(functionMethodName == "lastIndexOf")
            {
                if(argExprs.Count != 1 && argExprs.Count != 2)
                    throw new ParseException("\"" + functionMethodName + "\" expects one or two parameters)");
                return new SequenceExpressionArrayOrDequeOrStringLastIndexOf(targetExpr, argExprs[0], argExprs.Count != 1 ? argExprs[1] : null);
            }
            else if(functionMethodName == "indexOfOrdered")
            {
                if(argExprs.Count != 1)
                    throw new ParseException("\"" + functionMethodName + "\" expects 1 parameter)");
                return new SequenceExpressionArrayIndexOfOrdered(targetExpr, argExprs[0]);
            }
            else if(functionMethodName == "length")
            {
                if(argExprs.Count != 0)
                    throw new ParseException("\"" + functionMethodName + "\" expects no parameters)");
                return new SequenceExpressionStringLength(targetExpr);
            }
            else if(functionMethodName == "startsWith")
            {
                if(argExprs.Count != 1)
                    throw new ParseException("\"" + functionMethodName + "\" expects 1 parameter)");
                return new SequenceExpressionStringStartsWith(targetExpr, argExprs[0]);
            }
            else if(functionMethodName == "endsWith")
            {
                if(argExprs.Count != 1)
                    throw new ParseException("\"" + functionMethodName + "\" expects 1 parameter)");
                return new SequenceExpressionStringEndsWith(targetExpr, argExprs[0]);
            }
            else if(functionMethodName == "substring")
            {
                if(argExprs.Count != 1 && argExprs.Count != 2)
                    throw new ParseException("\"" + functionMethodName + "\" expects one or two parameters)");
                return new SequenceExpressionStringSubstring(targetExpr, argExprs[0], argExprs.Count != 1 ? argExprs[1] : null);
            }
            else if(functionMethodName == "replace")
            {
                if(argExprs.Count != 3)
                    throw new ParseException("\"" + functionMethodName + "\" expects three parameters)");
                return new SequenceExpressionStringReplace(targetExpr, argExprs[0], argExprs[1], argExprs[2]);
            }
            else if(functionMethodName == "toLower")
            {
                if(argExprs.Count != 0)
                    throw new ParseException("\"" + functionMethodName + "\" expects no parameters)");
                return new SequenceExpressionStringToLower(targetExpr);
            }
            else if(functionMethodName == "toUpper")
            {
                if(argExprs.Count != 0)
                    throw new ParseException("\"" + functionMethodName + "\" expects no parameters)");
                return new SequenceExpressionStringToUpper(targetExpr);
            }
            else
                return CreateSequenceExpressionFunctionMethodCallUserFunction(targetExpr, functionMethodName, argExprs);
        }

        public SequenceExpression CreateSequenceExpressionArrayAttributeAccessMethodCall(SequenceExpression targetExpr,
            String functionMethodName, String memberOrAttributeName, List<SequenceExpression> argExprs)
        {
            if(functionMethodName == "extract")
            {
                if(argExprs.Count != 0)
                    throw new ParseException("\"" + functionMethodName + "\" expects no parameters)");
                return new SequenceExpressionArrayExtract(targetExpr, memberOrAttributeName);
            }
            else if(functionMethodName == "orderAscendingBy")
            {
                if(argExprs.Count != 0)
                    throw new ParseException("\"" + functionMethodName + "\" expects no parameters)");
                return new SequenceExpressionArrayOrderAscendingBy(targetExpr, memberOrAttributeName);
            }
            else if(functionMethodName == "orderDescendingBy")
            {
                if(argExprs.Count != 0)
                    throw new ParseException("\"" + functionMethodName + "\" expects no parameters)");
                return new SequenceExpressionArrayOrderDescendingBy(targetExpr, memberOrAttributeName);
            }
            else if(functionMethodName == "groupBy")
            {
                if(argExprs.Count != 0)
                    throw new ParseException("\"" + functionMethodName + "\" expects no parameters)");
                return new SequenceExpressionArrayGroupBy(targetExpr, memberOrAttributeName);
            }
            else if(functionMethodName == "keepOneForEach")
            {
                if(argExprs.Count != 0)
                    throw new ParseException("\"" + functionMethodName + "\" expects no parameters)");
                return new SequenceExpressionArrayKeepOneForEachBy(targetExpr, memberOrAttributeName);
            }
            else if(functionMethodName == "indexOfBy")
            {
                if(argExprs.Count != 1 && argExprs.Count != 2)
                    throw new ParseException("\"" + functionMethodName + "\" expects one or two parameters)");
                return new SequenceExpressionArrayIndexOfBy(targetExpr, memberOrAttributeName, argExprs[0], argExprs.Count != 1 ? argExprs[1] : null);
            }
            else if(functionMethodName == "lastIndexOfBy")
            {
                if(argExprs.Count != 1 && argExprs.Count != 2)
                    throw new ParseException("\"" + functionMethodName + "\" expects one or two parameters)");
                return new SequenceExpressionArrayLastIndexOfBy(targetExpr, memberOrAttributeName, argExprs[0], argExprs.Count != 1 ? argExprs[1] : null);
            }
            else if(functionMethodName == "indexOfOrderedBy")
            {
                if(argExprs.Count != 1 && argExprs.Count != 2)
                    throw new ParseException("\"" + functionMethodName + "\" expects one or two parameters)");
                return new SequenceExpressionArrayIndexOfOrderedBy(targetExpr, memberOrAttributeName, argExprs[0]);
            }
            throw new ParseException("Unknown array attribute access function method name: \"" + functionMethodName + "\"!"
                + " (available are extract,orderAscendingBy,orderDescendingBy,groupBy,keepOneForEach,indexOfBy,lastIndexOfBy,indexOfOrderedBy)");
        }

        public SequenceExpression CreateSequenceExpressionPerElementMethodCall(SequenceExpression targetExpr,
            String functionMethodName, String typeName, 
            SequenceVariable arrayAccess, SequenceVariable index, SequenceVariable var, SequenceExpression argExpr)
        {
            if(functionMethodName == "map")
            {
                return new SequenceExpressionArrayMap(targetExpr, typeName, arrayAccess, index, var, argExpr);
            }
            else if(functionMethodName == "removeIf")
            {
                return new SequenceExpressionArrayRemoveIf(targetExpr, arrayAccess, index, var, argExpr);
            }
            throw new ParseException("Unknown per element attribute access function method name: \"" + functionMethodName + "\"!"
                + " (available are map, removeIf, mapStartWithAccumulateBy)");
        }

        public SequenceExpression CreateSequenceExpressionPerElementMethodCall(SequenceExpression targetExpr,
            String functionMethodName, String typeName,
            SequenceVariable initArrayAccess, SequenceExpression initExpr,
            SequenceVariable arrayAccess, SequenceVariable previousAccumulationAccess, SequenceVariable index, SequenceVariable var, SequenceExpression argExpr)
        {
            if(functionMethodName == "mapStartWithAccumulateBy")
            {
                return new SequenceExpressionArrayMapStartWithAccumulateBy(targetExpr, typeName, initArrayAccess, initExpr,
                    arrayAccess, previousAccumulationAccess, index, var, argExpr);
            }
            throw new ParseException("Unknown per element attribute access function method name: \"" + functionMethodName + "\"!"
                + " (available are map, removeIf, mapStartWithAccumulateBy)");
        }

        abstract public bool IsFunctionName(String functionName, String package);

        abstract public string GetFunctionNames();

        abstract public SequenceExpressionFunctionCall CreateSequenceExpressionFunctionCallUserFunction(String functionName, String packagePrefix,
            List<SequenceExpression> argExprs);

        public SequenceExpressionFunctionMethodCall CreateSequenceExpressionFunctionMethodCallUserFunction(SequenceExpression fromExpr,
            String functionMethodName, List<SequenceExpression> argExprs)
        {
            return new SequenceExpressionFunctionMethodCall(fromExpr, functionMethodName, argExprs);
        }


        SequenceExpression getArgument(List<SequenceExpression> argExprs, int index)
        {
            if(index < argExprs.Count)
                return argExprs[index];
            else // optional argument, is not parsed into list, function constructor requires null value
                return null;
        }

        protected bool PackageIsNullOrGlobal(String package)
        {
            return package == null || package == "global";
        }

        protected string PackagePrefixedName(String name, String package)
        {
            if(package != null)
                return package + "::" + name;
            else
                return name;
        }
    }
}

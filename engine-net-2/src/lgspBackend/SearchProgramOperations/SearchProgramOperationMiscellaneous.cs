/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, based on engine-net by Moritz Kroll

//note the same define in SearchProgram.cs
//#define ENSURE_FLAGS_IN_GRAPH_ARE_EMPTY_AT_LEAVING_TOP_LEVEL_MATCHING_ACTION

using System.Diagnostics;

namespace de.unika.ipd.grGen.lgsp
{
    // some miscellaneous search operations are listed below, most are available in the files with the SearchProgramOperation prefix 

    /// <summary>
    /// Class representing "draw variable from input parameters array" operation
    /// </summary>
    class ExtractVariable : SearchProgramOperation
    {
        public ExtractVariable(string varType, string varName)
        {
            VarType = varType;
            VarName = varName;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("ExtractVariable " + VarName + ":" + VarType + "\n");
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFront(VarType + " " + NamesOfEntities.Variable(VarName) + " = (" + VarType + ")" + VarName + ";\n");
        }

        public readonly string VarType;
        public readonly string VarName;
    }

    /// <summary>
    /// Available entity types 
    /// </summary>
    enum EntityType
    {
        Node,
        Edge,
        Variable
    }

    /// <summary>
    /// Class representing "declare a def to be yielded to variable" operation
    /// </summary>
    class DeclareDefElement : SearchProgramOperation
    {
        public DeclareDefElement(EntityType entityType, string typeOfEntity, string nameOfEntity, string initialization)
        {
            Type = entityType;
            TypeOfEntity = typeOfEntity;
            NameOfEntity = nameOfEntity;
            Initialization = initialization;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("Declare def " + NamesOfEntities.ToString(Type) + " " + NameOfEntity + ":" + TypeOfEntity +  " = " + Initialization + "\n");
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(Type == EntityType.Node)
                sourceCode.AppendFront(TypeOfEntity + " " + NamesOfEntities.CandidateVariable(NameOfEntity) + " = (" + TypeOfEntity + ")" + Initialization + ";\n");
            else if(Type == EntityType.Edge)
                sourceCode.AppendFront(TypeOfEntity + " " + NamesOfEntities.CandidateVariable(NameOfEntity) + " = (" + TypeOfEntity + ")" + Initialization + ";\n");
            else //if(Type == EntityType.Variable)
                sourceCode.AppendFront(TypeOfEntity + " " + NamesOfEntities.Variable(NameOfEntity) + " = (" + TypeOfEntity + ")" + Initialization + ";\n");
        }

        public readonly EntityType Type;
        public readonly string TypeOfEntity;
        public readonly string NameOfEntity;
        public readonly string Initialization; // only valid if Variable, only not null if initialization given
    }

    /// <summary>
    /// Class representing "assign variable from expression" operation,
    /// </summary>
    class AssignVariableFromExpression : SearchProgramOperation
    {
        public AssignVariableFromExpression(
            string variableName,
            string variableType,
            string sourceExpression)
        {
            VariableName = variableName;
            VariableType = variableType;
            SourceExpression = sourceExpression;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFrontFormat("AssignVariableFromExpression {0} := {1}\n",
                VariableName, SourceExpression);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(sourceCode.CommentSourceCode)
                sourceCode.AppendFrontFormat("// Variable {0} assigned from expression {1} \n",
                    VariableName, SourceExpression);
            
            // emit declaration of variable initialized with expression
            sourceCode.AppendFrontFormat("{0} {1} = ({0}){2};\n",
                VariableType, NamesOfEntities.Variable(VariableName), SourceExpression);
        }

        public readonly string VariableName;
        public readonly string VariableType;
        public readonly string SourceExpression;
    }

    /// <summary>
    /// Available types of AdjustListHeads operations
    /// </summary>
    enum AdjustListHeadsTypes
    {
        GraphElements,
        IncidentEdges
    }

    /// <summary>
    /// Class representing "adjust list heads" operation ("listentrick")
    /// </summary>
    class AdjustListHeads : SearchProgramOperation
    {
        public AdjustListHeads(
            AdjustListHeadsTypes type,
            string patternElementName,
            bool isNode,
            bool parallel)
        {
            Debug.Assert(type == AdjustListHeadsTypes.GraphElements);
            Type = type;
            PatternElementName = patternElementName;
            IsNode = isNode;
            Parallel = parallel;
        }

        public AdjustListHeads(
            AdjustListHeadsTypes type,
            string patternElementName,
            string startingPointNodeName,
            IncidentEdgeType incidentType,
            bool parallel)
        {
            Debug.Assert(type == AdjustListHeadsTypes.IncidentEdges);
            Type = type;
            PatternElementName = patternElementName;
            StartingPointNodeName = startingPointNodeName;
            IncidentType = incidentType;
            Parallel = parallel;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("AdjustListHeads ");
            if(Type==AdjustListHeadsTypes.GraphElements)
            {
                builder.Append("GraphElements ");
                builder.AppendFormat("on {0} node:{1} {2}\n",
                    PatternElementName, IsNode, Parallel ? "Parallel " : "");
            }
            else // Type==AdjustListHeadsTypes.IncidentEdges
            {
                builder.Append("IncidentEdges ");
                builder.AppendFormat("on {0} from:{1} incident type:{2} {3}\n",
                    PatternElementName, StartingPointNodeName, IncidentType.ToString(), Parallel ? "Parallel " : "");
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(Type == AdjustListHeadsTypes.GraphElements)
            {
                if(Parallel)
                {
                    if(IsNode)
                    {
                        sourceCode.AppendFrontFormat("moveHeadAfterNodes[threadId].Add({0});\n",
                            NamesOfEntities.CandidateVariable(PatternElementName));
                    }
                    else
                    {
                        sourceCode.AppendFrontFormat("moveHeadAfterEdges[threadId].Add({0});\n",
                            NamesOfEntities.CandidateVariable(PatternElementName));
                    }
                }
                else
                {
                    sourceCode.AppendFrontFormat("graph.MoveHeadAfter({0});\n",
                         NamesOfEntities.CandidateVariable(PatternElementName));
                }
            }
            else //Type == AdjustListHeadsTypes.IncidentEdges
            {
                if(IncidentType == IncidentEdgeType.Incoming)
                {
                    if(Parallel)
                    {
                        sourceCode.AppendFrontFormat("moveInHeadAfter[threadId].Add(new KeyValuePair<GRGEN_LGSP.LGSPNode, GRGEN_LGSP.LGSPEdge>({0}, {1}));\n",
                            NamesOfEntities.CandidateVariable(StartingPointNodeName),
                            NamesOfEntities.CandidateVariable(PatternElementName));
                    }
                    else
                    {
                        sourceCode.AppendFrontFormat("{0}.MoveInHeadAfter({1});\n",
                            NamesOfEntities.CandidateVariable(StartingPointNodeName),
                            NamesOfEntities.CandidateVariable(PatternElementName));
                    }
                }
                else if(IncidentType == IncidentEdgeType.Outgoing)
                {
                    if(Parallel)
                    {
                        sourceCode.AppendFrontFormat("moveOutHeadAfter[threadId].Add(new KeyValuePair<GRGEN_LGSP.LGSPNode, GRGEN_LGSP.LGSPEdge>({0}, {1}));\n",
                            NamesOfEntities.CandidateVariable(StartingPointNodeName),
                            NamesOfEntities.CandidateVariable(PatternElementName));
                    }
                    else
                    {
                        sourceCode.AppendFrontFormat("{0}.MoveOutHeadAfter({1});\n",
                            NamesOfEntities.CandidateVariable(StartingPointNodeName),
                            NamesOfEntities.CandidateVariable(PatternElementName));
                    }
                }
                else // IncidentType == IncidentEdgeType.IncomingOrOutgoing
                {
                    sourceCode.AppendFrontFormat("if({0}==0)",
                        NamesOfEntities.DirectionRunCounterVariable(PatternElementName));
                    sourceCode.Append(" {\n");
                    sourceCode.Indent();
                    if(Parallel)
                    {
                        sourceCode.AppendFrontFormat("moveInHeadAfter[threadId].Add(new KeyValuePair<GRGEN_LGSP.LGSPNode, GRGEN_LGSP.LGSPEdge>({0}, {1}));\n",
                            NamesOfEntities.CandidateVariable(StartingPointNodeName),
                            NamesOfEntities.CandidateVariable(PatternElementName));
                    }
                    else
                    {
                        sourceCode.AppendFrontFormat("{0}.MoveInHeadAfter({1});\n",
                            NamesOfEntities.CandidateVariable(StartingPointNodeName),
                            NamesOfEntities.CandidateVariable(PatternElementName));
                    }
                    sourceCode.Unindent();
                    sourceCode.AppendFront("} else {\n");
                    sourceCode.Indent();
                    if(Parallel)
                    {
                        sourceCode.AppendFrontFormat("moveOutHeadAfter[threadId].Add(new KeyValuePair<GRGEN_LGSP.LGSPNode, GRGEN_LGSP.LGSPEdge>({0}, {1}));\n",
                            NamesOfEntities.CandidateVariable(StartingPointNodeName),
                            NamesOfEntities.CandidateVariable(PatternElementName));
                    }
                    else
                    {
                        sourceCode.AppendFrontFormat("{0}.MoveOutHeadAfter({1});\n",
                            NamesOfEntities.CandidateVariable(StartingPointNodeName),
                            NamesOfEntities.CandidateVariable(PatternElementName));
                    }
                    sourceCode.Unindent();
                    sourceCode.AppendFront("}\n");
                }
            }
        }

        public readonly AdjustListHeadsTypes Type;
        public readonly string PatternElementName;
        public readonly bool IsNode; // node|edge - only valid if GraphElements
        public readonly string StartingPointNodeName; // only valid if IncidentEdges
        public readonly IncidentEdgeType IncidentType; // only valid if IncidentEdges
        public readonly bool Parallel;
    }

    /// <summary>
    /// Available types of ContinueOperation operations
    /// </summary>
    enum ContinueOperationType
    {
        ByReturn,
        ByContinue,
        ByGoto
    }

    /// <summary>
    /// Class representing "continue matching there" control flow operations
    /// </summary>
    class ContinueOperation : SearchProgramOperation
    {
        public ContinueOperation(ContinueOperationType type,
            bool returnMatches, 
            bool inParallelizedBody)
        {
            Debug.Assert(type == ContinueOperationType.ByReturn);
            Type = type;
            ReturnMatches = returnMatches;
            InParallelizedBody = inParallelizedBody;
        }

        public ContinueOperation(ContinueOperationType type,
            bool continueAtParllelizedLoop)
        {
            Debug.Assert(type == ContinueOperationType.ByContinue);
            Type = type;
            ContinueAtParallelizedLoop = continueAtParllelizedLoop;
        }

        public ContinueOperation(ContinueOperationType type,
            string labelName)
        {
            Debug.Assert(type == ContinueOperationType.ByGoto);
            Type = type;
            LabelName = labelName;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("ContinueOperation ");
            if(Type==ContinueOperationType.ByReturn)
            {
                builder.Append("ByReturn ");
                if(InParallelizedBody)
                    builder.Append("InParallelizedBody ");
                builder.AppendFormat("return matches:{0}\n", ReturnMatches);
            }
            else if(Type==ContinueOperationType.ByContinue)
                builder.AppendFormat("ByContinue {0}\n", ContinueAtParallelizedLoop ? "AtParallelizedLoop" : "");
            else // Type==ContinueOperationType.ByGoto
            { 
                builder.Append("ByGoto ");
                builder.AppendFormat("{0}\n", LabelName);
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(Type == ContinueOperationType.ByReturn)
            {
                if(InParallelizedBody)
                    sourceCode.AppendFrontFormat("return;\n");
                else
                {
                    if(ReturnMatches)
                    {
#if ENSURE_FLAGS_IN_GRAPH_ARE_EMPTY_AT_LEAVING_TOP_LEVEL_MATCHING_ACTION
                        sourceCode.AppendFront("graph.CheckEmptyFlags();\n");
#endif
                        sourceCode.AppendFront("return matches;\n");
                    }
                    else
                        sourceCode.AppendFront("return;\n");
                }
            }
            else if(Type == ContinueOperationType.ByContinue)
            {
                if(ContinueAtParallelizedLoop) { //re-aquire parallel matching enumeration lock before entering loop header
                    //sourceCode.AppendFront("Monitor.Enter(this);\n");
                    sourceCode.AppendFront("while(Interlocked.CompareExchange(ref iterationLock, 1, 0) != 0) Thread.SpinWait(10);//lock parallel matching enumeration with iteration lock\n");
                }
                sourceCode.AppendFront("continue;\n");
            }
            else //Type == ContinueOperationType.ByGoto
                sourceCode.AppendFrontFormat("goto {0};\n", LabelName);
        }

        public readonly ContinueOperationType Type;
        public readonly bool ReturnMatches; // only valid if ByReturn
        public readonly bool InParallelizedBody; // only valid if ByReturn
        public readonly string LabelName; // only valid if ByGoto
        public readonly bool ContinueAtParallelizedLoop; // only valid if ByContinue
    }

    /// <summary>
    /// Class representing location within code named with label,
    /// potential target of goto operation
    /// </summary>
    class GotoLabel : SearchProgramOperation
    {
        public GotoLabel()
        {
            LabelName = "label" + labelId.ToString();
            ++labelId;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("GotoLabel ");
            builder.AppendFormat("{0}\n", LabelName);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFormat("{0}: ;\n", LabelName);
        }

        public readonly string LabelName;
        private static int labelId = 0;
    }

    /// <summary>
    /// Available types of RandomizeListHeads operations
    /// </summary>
    enum RandomizeListHeadsTypes
    {
        GraphElements,
        IncidentEdges
    }

    /// <summary>
    /// Class representing "adjust list heads" operation ("listentrick")
    /// </summary>
    class RandomizeListHeads : SearchProgramOperation
    {
        public RandomizeListHeads(
            RandomizeListHeadsTypes type,
            string patternElementName,
            bool isNode)
        {
            Debug.Assert(type == RandomizeListHeadsTypes.GraphElements);
            Type = type;
            PatternElementName = patternElementName;
            IsNode = isNode;
        }

        public RandomizeListHeads(
            RandomizeListHeadsTypes type,
            string patternElementName,
            string startingPointNodeName,
            bool isIncoming)
        {
            Debug.Assert(type == RandomizeListHeadsTypes.IncidentEdges);
            Type = type;
            PatternElementName = patternElementName;
            StartingPointNodeName = startingPointNodeName;
            IsIncoming = isIncoming;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("RandomizeListHeads ");
            if(Type == RandomizeListHeadsTypes.GraphElements)
            {
                builder.Append("GraphElements ");
                builder.AppendFormat("on {0} node:{1}\n",
                    PatternElementName, IsNode);
            }
            else // Type==RandomizeListHeadsTypes.IncidentEdges
            {
                builder.Append("IncidentEdges ");
                builder.AppendFormat("on {0} from:{1} incoming:{2}\n",
                    PatternElementName, StartingPointNodeName, IsIncoming);
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            // --- move list head from current position to random position ---

            if(Type == RandomizeListHeadsTypes.GraphElements)
            {
                // emit declaration of variable containing random position to move list head to
                string variableContainingRandomPosition =
                    "random_position_" + PatternElementName;
                sourceCode.AppendFormat("int {0}", variableContainingRandomPosition);
                // emit initialization with ramdom position
                string graphMemberContainingElementListCountsByType =
                    IsNode ? "nodesByTypeCounts" : "edgesByTypeCounts";
                string variableContainingTypeIDForCandidate = 
                    NamesOfEntities.TypeIdForCandidateVariable(PatternElementName);
                sourceCode.AppendFormat(" = random.Next(graph.{0}[{1}]);\n",
                    graphMemberContainingElementListCountsByType,
                    variableContainingTypeIDForCandidate);
                // emit declaration of variable containing element at random position
                string typeOfVariableContainingElementAtRandomPosition = "GRGEN_LGSP."
                    + (IsNode ? "LGSPNode" : "LGSPEdge");
                string variableContainingElementAtRandomPosition =
                    "random_element_" + PatternElementName;
                sourceCode.AppendFrontFormat("{0} {1}",
                    typeOfVariableContainingElementAtRandomPosition,
                    variableContainingElementAtRandomPosition);
                // emit initialization with element list head
                string graphMemberContainingElementListHeadByType =
                    IsNode ? "nodesByTypeHeads" : "edgesByTypeHeads";
                sourceCode.AppendFormat(" = graph.{0}[{1}];\n",
                    graphMemberContainingElementListHeadByType, variableContainingTypeIDForCandidate);
                // emit iteration to get element at random position
                sourceCode.AppendFrontFormat(
                    "for(int i = 0; i < {0}; ++i) {1} = {1}.Next;\n",
                    variableContainingRandomPosition, variableContainingElementAtRandomPosition);
                // iteration left, element is the one at the requested random position
                // move list head after element at random position, 
                sourceCode.AppendFrontFormat("graph.MoveHeadAfter({0});\n",
                    variableContainingElementAtRandomPosition);
                // effect is new random starting point for following iteration
            }
            else //Type == RandomizeListHeadsTypes.IncidentEdges
            {
                // emit "randomization only if list is not empty"
                string variableContainingStartingPointNode =
                    NamesOfEntities.CandidateVariable(StartingPointNodeName);
                string memberOfNodeContainingListHead =
                    IsIncoming ? "lgspInhead" : "lgspOuthead";
                sourceCode.AppendFrontFormat("if({0}.{1}!=null)\n",
                    variableContainingStartingPointNode, memberOfNodeContainingListHead);
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                // emit declaration of variable containing random position to move list head to, initialize it to 0 
                string variableContainingRandomPosition =
                    "random_position_" + PatternElementName;
                sourceCode.AppendFrontFormat("int {0} = 0;", variableContainingRandomPosition);
                // misuse variable to store length of list which is computed within the follwing iteration
                string memberOfEdgeContainingNextEdge =
                    IsIncoming ? "lgspInNext" : "lgspOutNext";
                sourceCode.AppendFrontFormat("for(GRGEN_LGSP.LGSPEdge edge = {0}.{1}; edge!={0}.{1}; edge=edge.{2}) ++{3};\n",
                    variableContainingStartingPointNode, memberOfNodeContainingListHead,
                    memberOfEdgeContainingNextEdge, variableContainingRandomPosition);
                // emit initialization of variable containing ramdom position
                // now that the necessary length of the list is known after the iteration
                // given in the variable itself
                sourceCode.AppendFrontFormat("{0} = random.Next({0});\n",
                    variableContainingRandomPosition);
                // emit declaration of variable containing edge at random position
                string variableContainingEdgeAtRandomPosition =
                    "random_element_" + PatternElementName;
                sourceCode.AppendFrontFormat("GRGEN_LGSP.LGSPEdge {0}",
                    variableContainingEdgeAtRandomPosition);
                // emit initialization with edge list head
                sourceCode.AppendFormat(" = {0}.{1};\n",
                    variableContainingStartingPointNode, memberOfNodeContainingListHead);
                // emit iteration to get edge at random position
                sourceCode.AppendFrontFormat(
                    "for(int i = 0; i < {0}; ++i) {1} = {1}.{2};\n",
                    variableContainingRandomPosition,
                    variableContainingEdgeAtRandomPosition,
                    memberOfEdgeContainingNextEdge);
                // iteration left, edge is the one at the requested random position
                // move list head after edge at random position, 
                if(IsIncoming)
                {
                    sourceCode.AppendFrontFormat("{0}.MoveInHeadAfter({1});\n",
                        variableContainingStartingPointNode,
                        variableContainingEdgeAtRandomPosition);
                }
                else
                {
                    sourceCode.AppendFrontFormat("{0}.MoveOutHeadAfter({1});\n",
                        variableContainingStartingPointNode,
                        variableContainingEdgeAtRandomPosition);
                }

                // close list is not empty check
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");

                // effect is new random starting point for following iteration
            }
        }

        public readonly RandomizeListHeadsTypes Type;
        public readonly string PatternElementName;
        public readonly bool IsNode; // node|edge - only valid if GraphElements
        public readonly string StartingPointNodeName; // only valid if IncidentEdges
        public readonly bool IsIncoming; // only valid if IncidentEdges
    }

    /// <summary>
    /// Class representing "push match for patternpath" operation
    /// push match to the match objects stack used for patternpath checking
    /// </summary>
    class PushMatchForPatternpath : SearchProgramOperation
    {
        public PushMatchForPatternpath(
            string rulePatternClassName,
            string patternGraphName,
            string matchOfNestingPattern)
        {
            RulePatternClassName = rulePatternClassName;
            PatternGraphName = patternGraphName;
            MatchOfNestingPattern = matchOfNestingPattern;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFrontFormat("PushMatchOfNestingPattern of {0} nestingPattern in:{1}\n",
                PatternGraphName, MatchOfNestingPattern);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFrontFormat("// build match of {0} for patternpath checks\n", 
                PatternGraphName);
            sourceCode.AppendFrontFormat("if({0}==null) {0} = new {1}.{2}();\n", 
                NamesOfEntities.PatternpathMatch(PatternGraphName),
                RulePatternClassName,
                NamesOfEntities.MatchClassName(PatternGraphName));
            sourceCode.AppendFrontFormat("{0}._matchOfEnclosingPattern = {1};\n",
                NamesOfEntities.PatternpathMatch(PatternGraphName), MatchOfNestingPattern);
        }

        readonly string RulePatternClassName;
        readonly string PatternGraphName;
        readonly string MatchOfNestingPattern;
    }
}

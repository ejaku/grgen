/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, based on engine-net by Moritz Kroll

using System.Diagnostics;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// Class representing a fill operation of the current partial match into a set to prevent duplicates
    /// </summary>
    class FillPartialMatchForDuplicateChecking : SearchProgramOperation
    {
        public FillPartialMatchForDuplicateChecking(
            string rulePatternClassName,
            string patternName,
            string[] neededElements,
            string[] neededElementsUnprefixedName,
            bool[] neededElementsIsNode,
            bool parallelizedAction)
        {
            RulePatternClassName = rulePatternClassName;
            PatternName = patternName;
            NeededElements = neededElements;
            NeededElementsUnprefixedName = neededElementsUnprefixedName;
            NeededElementsIsNode = neededElementsIsNode;
            ParallelizedAction = parallelizedAction;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump local content
            builder.AppendFront("FillPartialMatchForDuplicateChecking with ");
            foreach(string neededElement in NeededElements)
            {
                builder.AppendFormat("{0} ", neededElement);
            }
            builder.Append("\n");
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFrontFormat("if({0} != null)\n", NamesOfEntities.FoundMatchesForFilteringVariable());
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            // emit check whether hash is contained in found matches hash map
            sourceCode.AppendFrontFormat("if(!{0}.ContainsKey({1}))\n",
                NamesOfEntities.FoundMatchesForFilteringVariable(),
                NamesOfEntities.DuplicateMatchHashVariable());
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            // if not, just insert it
            sourceCode.AppendFrontFormat("{0}[{1}] = match;\n",
                NamesOfEntities.FoundMatchesForFilteringVariable(),
                NamesOfEntities.DuplicateMatchHashVariable());

            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
            sourceCode.AppendFront("else\n");
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            // otherwise loop till end of collision list of the hash set, insert there
            sourceCode.AppendFrontFormat("{0} {1} = {2}[{3}];\n",
                RulePatternClassName + "." + NamesOfEntities.MatchClassName(PatternName),
                NamesOfEntities.DuplicateMatchCandidateVariable(),
                NamesOfEntities.FoundMatchesForFilteringVariable(),
                NamesOfEntities.DuplicateMatchHashVariable());
            sourceCode.AppendFrontFormat("while({0}.nextWithSameHash != null) {0} = {0}.nextWithSameHash;\n",
                NamesOfEntities.DuplicateMatchCandidateVariable());

            sourceCode.AppendFrontFormat("{0}.nextWithSameHash = match;\n",
                 NamesOfEntities.DuplicateMatchCandidateVariable());

            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");

            if(ParallelizedAction)
            {
                sourceCode.AppendFrontFormat("match.{0} = {0};\n",
                    NamesOfEntities.DuplicateMatchHashVariable());
            }

            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        public readonly string RulePatternClassName;
        public readonly string PatternName;
        public readonly string[] NeededElements;
        public readonly string[] NeededElementsUnprefixedName;
        public readonly bool[] NeededElementsIsNode;
        public readonly bool ParallelizedAction;
    }

    /// <summary>
    /// Class representing create inlined subpattern matches operations
    /// </summary>
    class CreateInlinedSubpatternMatch : SearchProgramOperation
    {
        public CreateInlinedSubpatternMatch(
            string rulePatternClassName,
            string patternName,
            string matchName,
            string matchOfEnclosingPatternName)
        {
            RulePatternClassName = rulePatternClassName;
            PatternName = patternName;
            MatchName = matchName;
            MatchOfEnclosingPatternName = matchOfEnclosingPatternName;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("CreateInlinedSubpatternMatch \n");
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFrontFormat("{0}.{1} match_{2} = new {0}.{1}();\n",
                RulePatternClassName, NamesOfEntities.MatchClassName(PatternName), MatchName);
            sourceCode.AppendFrontFormat("match_{0}.SetMatchOfEnclosingPattern({1});\n",
                MatchName, MatchOfEnclosingPatternName);
        }

        readonly string RulePatternClassName;
        readonly string PatternName;
        readonly string MatchName;
        readonly string MatchOfEnclosingPatternName;
    }

    /// <summary>
    /// Available types of BuildMatchObject operations
    /// </summary>
    enum BuildMatchObjectType
    {
        Node,        // build match object with match for node 
        Edge,        // build match object with match for edge
        Variable,    // build match object with match for variable
        Subpattern,  // build match object with match for subpattern
        InlinedSubpattern, // build match object with match of an inlined subpattern
        Iteration,   // build match object with match for iteration
        Alternative, // build match object with match for alternative
        Independent  // build match object with match for independent
    }

    /// <summary>
    /// Class representing "pattern was matched, now build match object" operation
    /// </summary>
    class BuildMatchObject : SearchProgramOperation
    {
        public BuildMatchObject(
            BuildMatchObjectType type,
            string patternElementType,
            string patternElementUnprefixedName,
            string patternElementName,
            string rulePatternClassName,
            string pathPrefixForEnum,
            string matchObjectName,
            int numSubpatterns)
        {
            Debug.Assert(type != BuildMatchObjectType.Independent);
            Type = type;
            PatternElementType = patternElementType;
            PatternElementUnprefixedName = patternElementUnprefixedName;
            PatternElementName = patternElementName;
            RulePatternClassName = rulePatternClassName;
            PathPrefixForEnum = pathPrefixForEnum;
            MatchObjectName = matchObjectName;
            NumSubpatterns = numSubpatterns; // only valid if type == Alternative
        }

        public BuildMatchObject(
            BuildMatchObjectType type,
            string patternElementUnprefixedName,
            string patternElementName,
            string rulePatternClassName,
            string matchObjectName,
            string matchClassName)
        {
            Debug.Assert(type == BuildMatchObjectType.Independent);
            Type = type;
            PatternElementUnprefixedName = patternElementUnprefixedName;
            PatternElementName = patternElementName;
            RulePatternClassName = rulePatternClassName;
            MatchObjectName = matchObjectName;
            MatchClassName = matchClassName;
        }

        public override void Dump(SourceBuilder builder)
        {
            string typeDescr;
            switch(Type)
            {
                case BuildMatchObjectType.Node:              typeDescr = "Node";        break;
                case BuildMatchObjectType.Edge:              typeDescr = "Edge";        break;
                case BuildMatchObjectType.Variable:          typeDescr = "Variable";    break;
                case BuildMatchObjectType.Subpattern:        typeDescr = "Subpattern";  break;
                case BuildMatchObjectType.InlinedSubpattern: typeDescr = "InlinedSubpattern"; break;
                case BuildMatchObjectType.Iteration:         typeDescr = "Iteration"; break;
                case BuildMatchObjectType.Alternative:       typeDescr = "Alternative"; break;
                case BuildMatchObjectType.Independent:       typeDescr = "Independent"; break;
                default:                                     typeDescr = ">>UNKNOWN<<"; break;
            }

            builder.AppendFrontFormat("BuildMatchObject {0} name {1} with {2} within {3}\n",
                typeDescr, MatchObjectName, PatternElementName, RulePatternClassName);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            string matchName = NamesOfEntities.MatchName(PatternElementUnprefixedName, Type);
            if(Type == BuildMatchObjectType.Node || Type == BuildMatchObjectType.Edge)
            {
                string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);
                sourceCode.AppendFrontFormat("{0}._{1} = {2};\n",
                    MatchObjectName, matchName, variableContainingCandidate);
            }
            else if(Type == BuildMatchObjectType.Variable)
            {
                string variableName = NamesOfEntities.Variable(PatternElementName);
                sourceCode.AppendFrontFormat("{0}._{1} = {2};\n",
                    MatchObjectName, matchName, variableName);
            }
            else if(Type == BuildMatchObjectType.Subpattern)
            {
                sourceCode.AppendFrontFormat("{0}._{1} = (@{2})currentFoundPartialMatch.Pop();\n",
                    MatchObjectName, matchName, PatternElementType);
                sourceCode.AppendFrontFormat("{0}._{1}._matchOfEnclosingPattern = {0};\n",
                    MatchObjectName, matchName);
            }
            else if(Type == BuildMatchObjectType.InlinedSubpattern)
            {
                sourceCode.AppendFrontFormat("{0}._{1} = match_{2};\n",
                    MatchObjectName, matchName, PatternElementName);
            }
            else if(Type == BuildMatchObjectType.Iteration)
            {
                sourceCode.AppendFrontFormat("{0}._{1} = new GRGEN_LGSP.LGSPMatchesList<{2}.{3}, {2}.{4}>(null);\n",
                    MatchObjectName, matchName,
                    RulePatternClassName, NamesOfEntities.MatchClassName(PatternElementType), NamesOfEntities.MatchInterfaceName(PatternElementType));
                sourceCode.AppendFrontFormat("while(currentFoundPartialMatch.Count>0 && currentFoundPartialMatch.Peek() is {0}.{1}) ", 
                    RulePatternClassName, NamesOfEntities.MatchInterfaceName(PatternElementType));
                sourceCode.Append("{\n");
                sourceCode.Indent();
                sourceCode.AppendFrontFormat("{0}.{1} cfpm = ({0}.{1})currentFoundPartialMatch.Pop();\n",
                    RulePatternClassName, NamesOfEntities.MatchClassName(PatternElementType));
                sourceCode.AppendFront("if(cfpm.IsNullMatch) break;\n");
                sourceCode.AppendFrontFormat("cfpm.SetMatchOfEnclosingPattern({0});\n",
                    MatchObjectName);
                sourceCode.AppendFrontFormat("{0}._{1}.Add(cfpm);\n",
                    MatchObjectName, matchName);
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
            }
            else if(Type == BuildMatchObjectType.Alternative)
            {
                sourceCode.AppendFrontFormat("{0}._{1} = ({2}.{3})currentFoundPartialMatch.Pop();\n",
                    MatchObjectName, matchName, RulePatternClassName, NamesOfEntities.MatchInterfaceName(PatternElementType));
                sourceCode.AppendFrontFormat("{0}._{1}.SetMatchOfEnclosingPattern({0});\n",
                    MatchObjectName, matchName);
            }
            else //if(Type == BuildMatchObjectType.Independent)
            {
                sourceCode.AppendFrontFormat("{0}._{1} = {2};\n",
                    MatchObjectName, matchName, NamesOfEntities.MatchedIndependentVariable(PatternElementName));
                sourceCode.AppendFrontFormat("{0} = new {1}({0});\n",
                    NamesOfEntities.MatchedIndependentVariable(PatternElementName),
                    RulePatternClassName + "." + NamesOfEntities.MatchClassName(MatchClassName));
                sourceCode.AppendFrontFormat("{0}._{1}.SetMatchOfEnclosingPattern({0});\n",
                    MatchObjectName, matchName);
            }
        }

        public readonly BuildMatchObjectType Type;
        public readonly string PatternElementType;
        public readonly string PatternElementUnprefixedName;
        public readonly string PatternElementName;
        public readonly string RulePatternClassName;
        public readonly string MatchClassName;
        public readonly string PathPrefixForEnum;
        public readonly string MatchObjectName;
        public readonly int NumSubpatterns;
    }

    /// <summary>
    /// Class representing implicit yield assignment operations,
    /// to bubble up values from nested patterns and subpatterns to the containing pattern
    /// </summary>
    class BubbleUpYieldAssignment : SearchProgramOperation
    {
        public BubbleUpYieldAssignment(
            EntityType type,
            string targetPatternElementName,
            string nestedMatchObjectName,
            string sourcePatternElementUnprefixedName
            )
        {
            Type = type;
            TargetPatternElementName = targetPatternElementName;
            NestedMatchOrTaskObjectName = nestedMatchObjectName;
            SourcePatternElementUnprefixedName = sourcePatternElementUnprefixedName;
        }

        public BubbleUpYieldAssignment(
            EntityType type,
            string targetPatternElementName,
            string taskObjectName
            )
        {
            Type = type;
            TargetPatternElementName = targetPatternElementName;
            NestedMatchOrTaskObjectName = taskObjectName;
            SourcePatternElementUnprefixedName = null;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFrontFormat("BubbleUpYieldAssignemt {0} {1} = {2}.{3}\n",
                NamesOfEntities.ToString(Type), TargetPatternElementName, NestedMatchOrTaskObjectName, SourcePatternElementUnprefixedName);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            string targetPatternElement = Type == EntityType.Variable ? NamesOfEntities.Variable(TargetPatternElementName) : NamesOfEntities.CandidateVariable(TargetPatternElementName);
            if(SourcePatternElementUnprefixedName != null)
            {
                string sourcePatternElement = NamesOfEntities.MatchName(SourcePatternElementUnprefixedName, Type);
                sourceCode.AppendFrontFormat("{0} = {1}._{2}; // bubble up (from match)\n",
                    targetPatternElement, NestedMatchOrTaskObjectName, sourcePatternElement);
            }
            else
            {
                sourceCode.AppendFrontFormat("{0} = {1}.{0}; // bubble up (from task)\n",
                    targetPatternElement, NestedMatchOrTaskObjectName);
            }
        }

        readonly EntityType Type;
        readonly string TargetPatternElementName;
        readonly string NestedMatchOrTaskObjectName;
        readonly string SourcePatternElementUnprefixedName;
    }

    /// <summary>
    /// Class representing a block around implicit yield bubble up assignments for nested iterateds
    /// </summary>
    class BubbleUpYieldIterated : SearchProgramOperation
    {
        public BubbleUpYieldIterated(string nestedMatchObjectName)
        {
            NestedMatchObjectName = nestedMatchObjectName;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump local content
            builder.AppendFrontFormat("BubbleUpYieldIterated on {0}\n", NestedMatchObjectName);

            // then nested content
            if(NestedOperationsList != null)
            {
                builder.Indent();
                NestedOperationsList.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFrontFormat("if({0}.Count>0) {{\n", NestedMatchObjectName);
            sourceCode.Indent();

            NestedOperationsList.Emit(sourceCode);

            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        public override bool IsSearchNestingOperation()
        {
            return true;
        }

        public override SearchProgramOperation GetNestedSearchOperationsList()
        {
            return NestedOperationsList;
        }

        readonly string NestedMatchObjectName;

        public SearchProgramList NestedOperationsList;
    }

    /// <summary>
    /// Class representing a block around explicit yield accumulate up assignments for nested iterateds
    /// </summary>
    class AccumulateUpYieldIterated : SearchProgramOperation
    {
        public AccumulateUpYieldIterated(string nestedMatchObjectName, string iteratedMatchTypeName, string helperMatchName)
        {
            NestedMatchObjectName = nestedMatchObjectName;
            IteratedMatchTypeName = iteratedMatchTypeName;
            HelperMatchName = helperMatchName;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump local content
            builder.AppendFrontFormat("BubbleUpYieldIterated on {0} type {1}\n", 
                NestedMatchObjectName, IteratedMatchTypeName);

            // then nested content
            if(NestedOperationsList != null)
            {
                builder.Indent();
                NestedOperationsList.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFrontFormat("foreach({0} {1} in {2}) {{\n", 
                IteratedMatchTypeName, HelperMatchName, NestedMatchObjectName);
            sourceCode.Indent();

            NestedOperationsList.Emit(sourceCode);

            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        public override bool IsSearchNestingOperation()
        {
            return true;
        }

        public override SearchProgramOperation GetNestedSearchOperationsList()
        {
            return NestedOperationsList;
        }

        readonly string NestedMatchObjectName;
        readonly string IteratedMatchTypeName;
        readonly string HelperMatchName;

        public SearchProgramList NestedOperationsList;
    }

    /// <summary>
    /// Class representing a block around implicit yield bubble up assignments for nested alternatives
    /// </summary>
    class BubbleUpYieldAlternativeCase : SearchProgramOperation
    {
        public BubbleUpYieldAlternativeCase(string matchObjectName, string nestedMatchObjectName,
            string alternativeCaseMatchTypeName, string helperMatchName, bool first)
        {
            MatchObjectName = matchObjectName;
            NestedMatchObjectName = nestedMatchObjectName;
            AlternativeCaseMatchTypeName = alternativeCaseMatchTypeName;
            HelperMatchName = helperMatchName;
            First = first;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump local content
            builder.AppendFrontFormat("BubbleUpYieldAlternativeCase on {0}.{1} case match type {2} {3}\n",
                MatchObjectName, NestedMatchObjectName, AlternativeCaseMatchTypeName, First ? "first" : "");

            // then nested content
            if(NestedOperationsList != null)
            {
                builder.Indent();
                NestedOperationsList.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFrontFormat("{0}if({1}.{2} is {3}) {{\n",
                First ? "" : "else ", MatchObjectName, NestedMatchObjectName, AlternativeCaseMatchTypeName);
            sourceCode.Indent();
            sourceCode.AppendFrontFormat("{0} {1} = ({0}){2}.{3};\n",
                AlternativeCaseMatchTypeName, HelperMatchName, MatchObjectName, NestedMatchObjectName);

            NestedOperationsList.Emit(sourceCode);

            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        public override bool IsSearchNestingOperation()
        {
            return true;
        }

        public override SearchProgramOperation GetNestedSearchOperationsList()
        {
            return NestedOperationsList;
        }

        readonly string MatchObjectName;
        readonly string NestedMatchObjectName;
        readonly string AlternativeCaseMatchTypeName;
        readonly string HelperMatchName;
        readonly bool First;

        public SearchProgramList NestedOperationsList;
    }

    /// <summary>
    /// Class representing (explicit) local yielding operations
    /// </summary>
    class LocalYielding : SearchProgramOperation
    {
        public LocalYielding(string yielding)
        {
            Yielding = yielding;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFrontFormat("LocalYielding {0}\n", Yielding);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFrontFormat("{0}", Yielding);
        }

        readonly string Yielding;
    }

    /// <summary>
    /// Class representing a yielding block
    /// </summary>
    class YieldingBlock : SearchProgramOperation
    {
        public YieldingBlock(string name)
        {
            Name = name;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump local content
            builder.AppendFrontFormat("YieldingBlock {0}\n", Name);
            
            // then nested content
            if(NestedOperationsList != null)
            {
                builder.Indent();
                NestedOperationsList.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFront("{ // " + Name + "\n");

            sourceCode.Indent();
            NestedOperationsList.Emit(sourceCode);
            sourceCode.Unindent();
            
            sourceCode.AppendFront("}\n");
        }

        public override bool IsSearchNestingOperation()
        {
            return true;
        }

        public override SearchProgramOperation GetNestedSearchOperationsList()
        {
            return NestedOperationsList;
        }

        readonly string Name;

        public SearchProgramList NestedOperationsList;
    }

    /// <summary>
    /// Class representing "create new matches list for following matches" operation
    /// </summary>
    class NewMatchesListForFollowingMatches : SearchProgramOperation
    {
        public NewMatchesListForFollowingMatches(bool onlyIfMatchWasFound)
        {
            OnlyIfMatchWasFound = onlyIfMatchWasFound;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFrontFormat("NewMatchesListForFollowingMatches {0}\n",
                OnlyIfMatchWasFound ? "if match was found" : "");
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(OnlyIfMatchWasFound)
            {
                sourceCode.AppendFront("if(matchesList.Count>0) {\n");
                sourceCode.Indent();
            }

            sourceCode.AppendFront("if(matchesList==foundPartialMatches) {\n");
            sourceCode.AppendFront("    matchesList = new List<Stack<GRGEN_LIBGR.IMatch>>();\n");
            sourceCode.AppendFront("} else {\n");
            sourceCode.AppendFront("    foreach(Stack<GRGEN_LIBGR.IMatch> match in matchesList) {\n");
            sourceCode.AppendFront("        foundPartialMatches.Add(match);\n");
            sourceCode.AppendFront("    }\n");
            sourceCode.AppendFront("    matchesList.Clear();\n");
            sourceCode.AppendFront("}\n");

            if(OnlyIfMatchWasFound)
            {
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
            }
        }

        public readonly bool OnlyIfMatchWasFound;
    }
}

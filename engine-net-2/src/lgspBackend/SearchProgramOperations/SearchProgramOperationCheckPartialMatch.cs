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
    /// Base class for search program operations
    /// filtering partial match
    /// (of the pattern part under construction)
    /// </summary>
    abstract class CheckPartialMatch : CheckOperation
    {
    }

    /// <summary>
    /// Base class for search program operations
    /// filtering partial match by searching further patterns based on found one
    /// i.e. by negative or independent patterns (nac/pac)
    /// </summary>
    abstract class CheckPartialMatchByNegativeOrIndependent : CheckPartialMatch
    {
        public CheckPartialMatchByNegativeOrIndependent(string[] neededElements)
        {
            NeededElements = neededElements;
        }

        public override bool IsSearchNestingOperation()
        {
            return true;
        }

        public override SearchProgramOperation GetNestedSearchOperationsList()
        {
            return NestedOperationsList;
        }

        public readonly string[] NeededElements;

        // search program of the negative/independent pattern
        public SearchProgramList NestedOperationsList;
    }

    /// <summary>
    /// Class representing "check whether the negative pattern applies" operation
    /// </summary>
    class CheckPartialMatchByNegative : CheckPartialMatchByNegativeOrIndependent
    {
        public CheckPartialMatchByNegative(string[] neededElements)
            : base(neededElements)
        {
        }

        public override void Dump(SourceBuilder builder)
        {
            Debug.Assert(CheckFailedOperations == null, "check negative without direct check failed code");
            // first dump local content
            builder.AppendFront("CheckPartialMatch ByNegative with ");
            foreach(string neededElement in NeededElements)
            {
                builder.AppendFormat("{0} ", neededElement);
            }
            builder.Append("\n");
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
            if(sourceCode.CommentSourceCode)
                sourceCode.AppendFront("// NegativePattern \n");
            // currently needed because of multiple negMapped backup variables with same name
            // todo: assign names to negatives, mangle that name in, then remove block again
            // todo: remove (neg)mapped backup variables altogether, then remove block again
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            NestedOperationsList.Emit(sourceCode);

            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");

            //if(sourceCode.CommentSourceCode) reinsert when block is removed
            //    sourceCode.AppendFront("// NegativePattern end\n");
        }
    }

    /// <summary>
    /// Class representing "check whether the independent pattern applies" operation
    /// </summary>
    class CheckPartialMatchByIndependent : CheckPartialMatchByNegativeOrIndependent
    {
        public CheckPartialMatchByIndependent(string[] neededElements)
            : base(neededElements)
        {
        }

        public override void Dump(SourceBuilder builder)
        {
            Debug.Assert(CheckFailedOperations == null, "check independent without direct check failed code");
            // first dump local content
            builder.AppendFront("CheckPartialMatch ByIndependent with ");
            foreach(string neededElement in NeededElements)
            {
                builder.AppendFormat("{0} ", neededElement);
            }
            builder.Append("\n");
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
            if(sourceCode.CommentSourceCode)
                sourceCode.AppendFront("// IndependentPattern \n");
            // currently needed because of multiple idptMapped backup variables with same name
            // todo: assign names to independents, mangle that name in, then remove block again
            // todo: remove (idpt)mapped backup variables altogether, then remove block again
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            NestedOperationsList.Emit(sourceCode);

            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");

            //if(sourceCode.CommentSourceCode) reinsert when block is removed
            //    sourceCode.AppendFront("// IndependentPattern end\n");
        }

        // the corresponding check failed operation located after this one (in contrast to the check succeeded operation nested inside this one)
        public CheckContinueMatchingOfIndependentFailed CheckIndependentFailed;
    }

    /// <summary>
    /// Class representing "check whether the condition applies" operation
    /// </summary>
    class CheckPartialMatchByCondition : CheckPartialMatch
    {
        public CheckPartialMatchByCondition(
            string conditionExpression,
            string[] neededNodes,
            string[] neededEdges,
            string[] neededVariables)
        {
            ConditionExpression = conditionExpression;
            NeededVariables = neededVariables;
            
            int i = 0;
            NeededElements = new string[neededNodes.Length + neededEdges.Length];
            NeededElementIsNode = new bool[neededNodes.Length + neededEdges.Length];
            foreach(string neededNode in neededNodes)
            {
                NeededElements[i] = neededNode;
                NeededElementIsNode[i] = true;
                ++i;
            }
            foreach(string neededEdge in neededEdges)
            {
                NeededElements[i] = neededEdge;
                NeededElementIsNode[i] = false;
                ++i;
            }
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump check
            builder.AppendFront("CheckPartialMatch ByCondition ");
            builder.AppendFormat("{0} with ", ConditionExpression);
            foreach(string neededElement in NeededElements)
            {
                builder.Append(neededElement);
                builder.Append(" ");
            }
            foreach(string neededVar in NeededVariables)
            {
                builder.Append(neededVar);
                builder.Append(" ");
            }
            builder.Append("\n");
            // then operations for case check failed
            if(CheckFailedOperations != null)
            {
                builder.Indent();
                CheckFailedOperations.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(sourceCode.CommentSourceCode)
                sourceCode.AppendFront("// Condition \n");

            // open decision
            sourceCode.AppendFront("if(");
            // emit condition expression
            sourceCode.AppendFormat("!({0})", ConditionExpression);
            // close decision
            sourceCode.Append(") ");

            // emit check failed code
            sourceCode.Append("{\n");
            sourceCode.Indent();
            CheckFailedOperations.Emit(sourceCode);
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        public readonly string ConditionExpression;
        public readonly string[] NeededElements;
        public readonly bool[] NeededElementIsNode;
        public readonly string[] NeededVariables;
    }

    /// <summary>
    /// Class representing "check whether the subpatterns of the pattern were found" operation
    /// </summary>
    class CheckPartialMatchForSubpatternsFound : CheckPartialMatch
    {
        public CheckPartialMatchForSubpatternsFound(
            string negativeIndependentNamePrefix)
        {
            NegativeIndependentNamePrefix = negativeIndependentNamePrefix;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump check
            builder.AppendFront("CheckPartialMatch ForSubpatternsFound\n");
            // then operations for case check failed
            if(CheckFailedOperations != null)
            {
                builder.Indent();
                CheckFailedOperations.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(sourceCode.CommentSourceCode)
                sourceCode.AppendFront("// Check whether subpatterns were found \n");

            // emit decision
            sourceCode.AppendFrontFormat("if({0}matchesList.Count>0) ", NegativeIndependentNamePrefix);

            // emit check failed code
            sourceCode.Append("{\n");
            sourceCode.Indent();
            CheckFailedOperations.Emit(sourceCode);
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        public readonly string NegativeIndependentNamePrefix;
    }

    /// <summary>
    /// Class representing "check whether the current partial match is a duplicate" operation
    /// </summary>
    class CheckPartialMatchForDuplicate : CheckPartialMatch
    {
        public CheckPartialMatchForDuplicate(
            string rulePatternClassName, 
            string patternName, 
            string[] neededElements,
            string[] matchObjectPaths,
            string[] neededElementsUnprefixedName,
            bool[] neededElementsIsNode)
        {
            RulePatternClassName = rulePatternClassName;
            PatternName = patternName;
            NeededElements = neededElements;
            MatchObjectPaths = matchObjectPaths;
            NeededElementsUnprefixedName = neededElementsUnprefixedName;
            NeededElementsIsNode = neededElementsIsNode;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump local content
            builder.AppendFront("CheckPartialMatch ForDuplicate with ");
            foreach(string neededElement in NeededElements)
            {
                builder.AppendFormat("{0} ", neededElement);
            }
            builder.Append("\n");
            // then operations for case check failed
            if(CheckFailedOperations != null)
            {
                builder.Indent();
                CheckFailedOperations.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(sourceCode.CommentSourceCode)
                sourceCode.AppendFront("// Check whether a duplicate match to be purged was found or will be found\n");

            // emit hash variable declaration
            sourceCode.AppendFrontFormat("int {0} = 0;\n", NamesOfEntities.DuplicateMatchHashVariable());

            // only do the rest if more than one match is requested
            sourceCode.AppendFront("if(maxMatches!=1) {\n");
            sourceCode.Indent();

            // emit found matches hash map initialization as needed
            sourceCode.AppendFrontFormat("if({0}==null) {0} = new Dictionary<int, {1}>();\n",
                NamesOfEntities.FoundMatchesForFilteringVariable(),
                RulePatternClassName + "." + NamesOfEntities.MatchClassName(PatternName));

            // emit hash variable initialization with result of hash computation 
            sourceCode.AppendFrontFormat("{0} = unchecked(", 
                NamesOfEntities.DuplicateMatchHashVariable());
            EmitHashComputation(sourceCode, NeededElements.Length - 1);
            sourceCode.Append(");\n");

            // emit check whether hash is contained in found matches hash map
            sourceCode.AppendFrontFormat("if({0}.ContainsKey({1}))\n", 
                NamesOfEntities.FoundMatchesForFilteringVariable(), 
                NamesOfEntities.DuplicateMatchHashVariable());
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            // emit check whether one of the matches in the hash map with same hash is equal to the locally matched elements
            sourceCode.AppendFrontFormat("{0} {1} = {2}[{3}];\n", 
                RulePatternClassName + "." + NamesOfEntities.MatchClassName(PatternName),
                NamesOfEntities.DuplicateMatchCandidateVariable(),
                NamesOfEntities.FoundMatchesForFilteringVariable(),
                NamesOfEntities.DuplicateMatchHashVariable());
            sourceCode.AppendFront("do {\n");
            sourceCode.Indent();

            // emit check for same elements
            sourceCode.AppendFront("if(");
            for(int i = 0; i < NeededElements.Length; ++i)
            {
                if(i != 0)
                    sourceCode.Append(" && ");
                sourceCode.AppendFormat("{0}._{1} == {2}",
                    NamesOfEntities.DuplicateMatchCandidateVariable() + MatchObjectPaths[i],
                    NamesOfEntities.MatchName(NeededElementsUnprefixedName[i], NeededElementsIsNode[i] ? BuildMatchObjectType.Node : BuildMatchObjectType.Edge), 
                    NamesOfEntities.CandidateVariable(NeededElements[i]));
            }
            sourceCode.Append(")\n");

            // emit check failed code, i.e. the current local match is equivalent to one of the already found ones, a duplicate
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();
            CheckFailedOperations.Emit(sourceCode);
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");

            // close "emit check whether one of the matches in the hash map with same hash is equal to the locally matched elements"
            // switching to next match with the same hash, if available
            sourceCode.Unindent();
            sourceCode.AppendFront("} ");
            sourceCode.AppendFormat("while(({0} = {0}.nextWithSameHash) != null);\n", 
                NamesOfEntities.DuplicateMatchCandidateVariable());

            // close "emit check whether hash is contained in found matches hash map"
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");

            // close "only do the rest if more than one match is requested"
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        void EmitHashComputation(SourceBuilder sourceCode, int i)
        {
            if(i < 0)
                sourceCode.Append("23");
            else
            {
                sourceCode.Append("(");
                EmitHashComputation(sourceCode, i - 1);
                sourceCode.AppendFormat("*17 + {0}.GetHashCode()",
                    NamesOfEntities.CandidateVariable(NeededElements[i]));
                sourceCode.Append(")");
            }
        }

        public readonly string RulePatternClassName;
        public readonly string PatternName;
        public readonly string[] NeededElements;
        public readonly string[] MatchObjectPaths;
        public readonly string[] NeededElementsUnprefixedName;
        public readonly bool[] NeededElementsIsNode;
    }
}

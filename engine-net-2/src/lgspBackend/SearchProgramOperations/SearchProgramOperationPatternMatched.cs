/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, based on engine-net by Moritz Kroll

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// Class yielding operations to be executed 
    /// when a positive pattern without contained subpatterns was matched
    /// </summary>
    class PositivePatternWithoutSubpatternsMatched : SearchProgramOperation
    {
        public PositivePatternWithoutSubpatternsMatched(
            string rulePatternClassName,
            string patternName,
            bool inParallelizedBody)
        {
            RulePatternClassName = rulePatternClassName;
            PatternName = patternName;
            InParallelizedBody = inParallelizedBody;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("PositivePatternMatched \n");
            if(InParallelizedBody)
                builder.AppendFront("InParallelizedBody \n");

            if(MatchBuildingOperations != null)
            {
                builder.Indent();
                MatchBuildingOperations.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(InParallelizedBody)
            {
                sourceCode.AppendFrontFormat("{0}.{1} match = parallelTaskMatches[threadId].GetNextUnfilledPosition();\n",
                    RulePatternClassName, NamesOfEntities.MatchClassName(PatternName));
            }
            else
            {
                sourceCode.AppendFrontFormat("{0}.{1} match = matches.GetNextUnfilledPosition();\n",
                    RulePatternClassName, NamesOfEntities.MatchClassName(PatternName));
            }

            // emit match building operations
            MatchBuildingOperations.Emit(sourceCode);

            if(InParallelizedBody)
            {
                sourceCode.AppendFront("match.IterationNumber = currentIterationNumber;\n");
                sourceCode.AppendFront("parallelTaskMatches[threadId].PositionWasFilledFixIt();\n");
            }
            else
                sourceCode.AppendFront("matches.PositionWasFilledFixIt();\n");
        }

        readonly string RulePatternClassName;
        readonly string PatternName;
        readonly bool InParallelizedBody;

        public SearchProgramList MatchBuildingOperations;
    }

    /// <summary>
    /// Class yielding operations to be executed 
    /// when a subpattern without contained subpatterns was matched (as the last element of the search)
    /// </summary>
    class LeafSubpatternMatched : SearchProgramOperation
    {
        public LeafSubpatternMatched(
            string rulePatternClassName, 
            string patternName,
            bool isIteratedNullMatch)
        {
            RulePatternClassName = rulePatternClassName;
            PatternName = patternName;
            IsIteratedNullMatch = isIteratedNullMatch;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFrontFormat("LeafSubpatternMatched {0}\n", IsIteratedNullMatch ? "IteratedNullMatch " : "");

            if(MatchBuildingOperations != null)
            {
                builder.Indent();
                MatchBuildingOperations.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFront("Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();\n");
            sourceCode.AppendFront("foundPartialMatches.Add(currentFoundPartialMatch);\n");

            sourceCode.AppendFrontFormat("{0}.{1} match = new {0}.{1}();\n",
                RulePatternClassName, NamesOfEntities.MatchClassName(PatternName));
            if(IsIteratedNullMatch)
                sourceCode.AppendFront("match._isNullMatch = true; // null match of iterated pattern\n");
            else
                MatchBuildingOperations.Emit(sourceCode); // emit match building operations
            sourceCode.AppendFront("currentFoundPartialMatch.Push(match);\n");
        }

        readonly string RulePatternClassName;
        readonly string PatternName;
        readonly bool IsIteratedNullMatch;

        public SearchProgramList MatchBuildingOperations;
    }

    /// <summary>
    /// Available types of PatternAndSubpatternsMatched operations
    /// </summary>
    enum PatternAndSubpatternsMatchedType
    {
        Action,
        Iterated,
        IteratedNullMatch,
        SubpatternOrAlternative
    }

    /// <summary>
    /// Class yielding operations to be executed 
    /// when a positive pattern was matched and all of it's subpatterns were matched at least once
    /// </summary>
    class PatternAndSubpatternsMatched : SearchProgramOperation
    {
        public PatternAndSubpatternsMatched(
            string rulePatternClassName,
            string patternName,
            bool inParallelizedBody,
            PatternAndSubpatternsMatchedType type)
        {
            RulePatternClassName = rulePatternClassName;
            PatternName = patternName;
            InParallelizedBody = inParallelizedBody;
            Type = type;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("PatternAndSubpatternsMatched ");
            if(InParallelizedBody)
                builder.Append("InParallelizedBody ");
            switch(Type)
            {
            case PatternAndSubpatternsMatchedType.Action:
                builder.Append("Action \n");
                break;
            case PatternAndSubpatternsMatchedType.Iterated:
                builder.Append("Iterated \n");
                break;
            case PatternAndSubpatternsMatchedType.IteratedNullMatch:
                builder.Append("IteratedNullMatch \n");
                break;
            case PatternAndSubpatternsMatchedType.SubpatternOrAlternative:
                builder.Append("SubpatternOrAlternative \n");
                break;
            default:
                builder.Append("INTERNAL ERROR\n");
                break;
            }

            if(MatchBuildingOperations != null)
            {
                builder.Indent();
                MatchBuildingOperations.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(Type==PatternAndSubpatternsMatchedType.Iterated)
            {
                sourceCode.AppendFront("patternFound = true;\n");
            }

            if(Type!=PatternAndSubpatternsMatchedType.Action)
            {
                if(sourceCode.CommentSourceCode)
                    sourceCode.AppendFront("// subpatterns/alternatives were found, extend the partial matches by our local match object\n");
                sourceCode.AppendFront("foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)\n");
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                sourceCode.AppendFrontFormat("{0}.{1} match = new {0}.{1}();\n",
                    RulePatternClassName, NamesOfEntities.MatchClassName(PatternName));
                if(Type == PatternAndSubpatternsMatchedType.IteratedNullMatch)
                    sourceCode.AppendFront("match._isNullMatch = true; // null match of iterated pattern\n");

                MatchBuildingOperations.Emit(sourceCode); // emit match building operations
                sourceCode.AppendFront("currentFoundPartialMatch.Push(match);\n");

                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
            }
            else // top-level pattern with subpatterns/alternatives
            {
                if(sourceCode.CommentSourceCode)
                    sourceCode.AppendFront("// subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it\n");
                sourceCode.AppendFront("foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)\n");
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                if(InParallelizedBody)
                {
                    sourceCode.AppendFrontFormat("{0}.{1} match = parallelTaskMatches[threadId].GetNextUnfilledPosition();\n",
                        RulePatternClassName, NamesOfEntities.MatchClassName(PatternName));
                }
                else
                {
                    sourceCode.AppendFrontFormat("{0}.{1} match = matches.GetNextUnfilledPosition();\n",
                        RulePatternClassName, NamesOfEntities.MatchClassName(PatternName));
                }
                MatchBuildingOperations.Emit(sourceCode); // emit match building operations
                if(InParallelizedBody)
                {
                    sourceCode.AppendFront("match.IterationNumber = currentIterationNumber;\n");
                    sourceCode.AppendFront("parallelTaskMatches[threadId].PositionWasFilledFixIt();\n");
                }
                else
                    sourceCode.AppendFront("matches.PositionWasFilledFixIt();\n");
                
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
                sourceCode.AppendFront("matchesList.Clear();\n");
            }
        }

        readonly string RulePatternClassName;
        readonly string PatternName;
        readonly bool InParallelizedBody;
        readonly PatternAndSubpatternsMatchedType Type;

        public SearchProgramList MatchBuildingOperations;
    }

    /// <summary>
    /// Available types of NegativeIndependentPatternMatched operations
    /// </summary>
    enum NegativeIndependentPatternMatchedType
    {
        WithoutSubpatterns,
        ContainingSubpatterns
    };

    /// <summary>
    /// Class yielding operations to be executed 
    /// when a negative pattern was matched
    /// </summary>
    class NegativePatternMatched : SearchProgramOperation
    {
        public NegativePatternMatched(
            NegativeIndependentPatternMatchedType type,
            string negativeIndependentNamePrefix)
        {
            Type = type;
            NegativeIndependentNamePrefix = negativeIndependentNamePrefix;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("NegativePatternMatched ");
            builder.Append(Type == NegativeIndependentPatternMatchedType.WithoutSubpatterns ?
                "WithoutSubpatterns\n" : "ContainingSubpatterns\n");
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(Type == NegativeIndependentPatternMatchedType.WithoutSubpatterns)
            {
                if(sourceCode.CommentSourceCode)
                    sourceCode.AppendFront("// negative pattern found\n");
            }
            else
            {
                if(sourceCode.CommentSourceCode)
                    sourceCode.AppendFront("// negative pattern with contained subpatterns found\n");

                sourceCode.AppendFrontFormat("{0}matchesList.Clear();\n", NegativeIndependentNamePrefix);
            }
        }

        public readonly NegativeIndependentPatternMatchedType Type;
        public readonly string NegativeIndependentNamePrefix;
    }

    /// <summary>
    /// Class yielding operations to be executed 
    /// when a independent pattern was matched
    /// </summary>
    class IndependentPatternMatched : SearchProgramOperation
    {
        public IndependentPatternMatched(
            NegativeIndependentPatternMatchedType type,
            string negativeIndependentNamePrefix)
        {
            Type = type;
            NegativeIndependentNamePrefix = negativeIndependentNamePrefix;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("IndependentPatternMatched ");
            builder.Append(Type == NegativeIndependentPatternMatchedType.WithoutSubpatterns ?
                "WithoutSubpatterns\n" : "ContainingSubpatterns\n");
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(Type == NegativeIndependentPatternMatchedType.WithoutSubpatterns)
            {
                if(sourceCode.CommentSourceCode)
                    sourceCode.AppendFront("// independent pattern found\n");
            }
            else
            {
                if(sourceCode.CommentSourceCode)
                    sourceCode.AppendFront("// independent pattern with contained subpatterns found\n");
                sourceCode.AppendFrontFormat("Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = {0}matchesList[0];\n",
                    NegativeIndependentNamePrefix);
                sourceCode.AppendFrontFormat("{0}matchesList.Clear();\n", NegativeIndependentNamePrefix);
            }
        }

        public readonly NegativeIndependentPatternMatchedType Type;
        public readonly string NegativeIndependentNamePrefix;
    }
}

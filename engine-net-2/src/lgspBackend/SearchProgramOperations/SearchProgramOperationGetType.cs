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
    /// Base class for search program type determining operations,
    /// setting current type for following get candidate operation
    /// </summary>
    abstract class GetType : SearchProgramOperation
    {
    }

    /// <summary>
    /// Available types of GetTypeByIteration operations
    /// </summary>
    enum GetTypeByIterationType
    {
        ExplicitelyGiven, // iterate the explicitely given types
        AllCompatible // iterate all compatible types of the pattern element type
    }

    /// <summary>
    /// Class representing "iterate over the allowed types" operation,
    /// setting type id to use in the following get candidate by element iteration
    /// </summary>
    class GetTypeByIteration : GetType
    {
        public GetTypeByIteration(
            GetTypeByIterationType type,
            string patternElementName,
            string rulePatternTypeNameOrTypeName,
            bool isNode)
        {
            Type = type;
            PatternElementName = patternElementName;
            if(type == GetTypeByIterationType.ExplicitelyGiven)
                TypeName = rulePatternTypeNameOrTypeName;
            else // type == GetTypeByIterationType.AllCompatible
                RulePatternTypeName = rulePatternTypeNameOrTypeName;
            IsNode = isNode;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump local content
            builder.AppendFront("GetType ByIteration ");
            if(Type==GetTypeByIterationType.ExplicitelyGiven)
            {
                builder.Append("ExplicitelyGiven ");
                builder.AppendFormat("on {0} in {1} node:{2}\n", 
                    PatternElementName, TypeName, IsNode);
            }
            else // Type==GetTypeByIterationType.AllCompatible
            {
                builder.Append("AllCompatible ");
                builder.AppendFormat("on {0} in {1} node:{2}\n",
                    PatternElementName, RulePatternTypeName, IsNode);
            }
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
                sourceCode.AppendFrontFormat("// Lookup {0} \n", PatternElementName);

            // todo: randomisierte auswahl des typen wenn RANDOM_LOOKUP_LIST_START ?

            // emit type iteration loop header
            string typeOfVariableContainingType = NamesOfEntities.TypeOfVariableContainingType(IsNode);
            string variableContainingTypeForCandidate = 
                NamesOfEntities.TypeForCandidateVariable(PatternElementName);
            string containerWithAvailableTypes;
            if(Type == GetTypeByIterationType.ExplicitelyGiven)
            {
                containerWithAvailableTypes = TypeName
                    + "." + PatternElementName + "_AllowedTypes";
            }
            else //(Type == GetTypeByIterationType.AllCompatible)
            {
                containerWithAvailableTypes = RulePatternTypeName
                    + ".typeVar.SubOrSameTypes";
            }
            
            sourceCode.AppendFrontFormat("foreach({0} {1} in {2})\n",
                typeOfVariableContainingType, variableContainingTypeForCandidate,
                containerWithAvailableTypes);

            // open loop
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            // emit type id setting and loop body 
            string variableContainingTypeIDForCandidate = 
                NamesOfEntities.TypeIdForCandidateVariable(PatternElementName);
            sourceCode.AppendFrontFormat("int {0} = {1}.TypeID;\n",
                variableContainingTypeIDForCandidate, variableContainingTypeForCandidate);

            NestedOperationsList.Emit(sourceCode);

            // close loop
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

        public readonly GetTypeByIterationType Type;
        public readonly string PatternElementName;
        public readonly string RulePatternTypeName; // only valid if ExplicitelyGiven
        public readonly string TypeName; // only valid if AllCompatible
        public readonly bool IsNode; // node|edge

        public SearchProgramList NestedOperationsList;
    }

    /// <summary>
    /// Class representing "get the allowed type" operation,
    /// setting type id to use in the following get candidate by element iteration
    /// </summary>
    class GetTypeByDrawing : GetType
    {
        public GetTypeByDrawing(
            string patternElementName,
            string typeID,
            bool isNode)
        {
            PatternElementName = patternElementName;
            TypeID = typeID;
            IsNode = isNode;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("GetType GetTypeByDrawing ");
            builder.AppendFormat("on {0} id:{1} node:{2}\n",
                PatternElementName, TypeID, IsNode);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(sourceCode.CommentSourceCode)
                sourceCode.AppendFrontFormat("// Lookup {0} \n", PatternElementName);

            string variableContainingTypeIDForCandidate = 
                NamesOfEntities.TypeIdForCandidateVariable(PatternElementName);
            sourceCode.AppendFrontFormat("int {0} = {1};\n",
                variableContainingTypeIDForCandidate, TypeID);
        }

        public readonly string PatternElementName;
        public readonly string TypeID;
        public readonly bool IsNode; // node|edge
    }
}

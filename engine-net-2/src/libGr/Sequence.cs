/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 */

using System;
using System.Collections.Generic;
using System.Collections;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// Specifies the actual subtype used for a Sequence.
    /// </summary>
    public enum SequenceType
    {
        ThenLeft, ThenRight, LazyOr, LazyAnd, StrictOr, Xor, StrictAnd, Not, Min, MinMax,
        Rule, RuleAll, Def, True, False, VarPredicate,
        AssignSetmapSizeToVar, AssignSetmapEmptyToVar, AssignMapAccessToVar,
        AssignSetCreationToVar, AssignMapCreationToVar,
        AssignVarToVar, AssignElemToVar, AssignSequenceResultToVar,
        AssignConstToVar, AssignAttributeToVar, AssignVarToAttribute,
        SetmapAdd, SetmapRem, SetmapClear, InSetmap, 
        Transaction, IfThenElse, IfThen, For
    }

    /// <summary>
    /// A sequence object with references to child sequences.
    /// </summary>
    public abstract class Sequence
    {
        /// <summary>
        /// A common random number generator for all sequence objects.
        /// It uses a time-dependent seed.
        /// </summary>
        public static Random randomGenerator = new Random();

        /// <summary>
        /// The type of the sequence (e.g. LazyOr or Transaction)
        /// </summary>
        public SequenceType SequenceType;

        /// <summary>
        /// Initializes a new Sequence object with the given sequence type.
        /// </summary>
        /// <param name="seqType">The sequence type.</param>
        public Sequence(SequenceType seqType)
        {
            SequenceType = seqType;
        }

        /// <summary>
        /// Applies this sequence.
        /// </summary>
        /// <param name="graph">The graph on which this sequence is to be applied.
        ///     The rules will only be chosen during the Sequence object instantiation, so
        ///     exchanging rules will have no effect for already existing Sequence objects.</param>
        /// <returns>True, iff the sequence succeeded</returns>
        public bool Apply(IGraph graph)
        {
            graph.EnteringSequence(this);
            bool res = ApplyImpl(graph);
            graph.ExitingSequence(this);
            return res;
        }

        /// <summary>
        /// Applies this sequence. This function represents the actual implementation of the sequence.
        /// </summary>
        /// <param name="graph">The graph on which this sequence is to be applied.</param>
        /// <returns>True, iff the sequence succeeded</returns>
        protected abstract bool ApplyImpl(IGraph graph);

        /// <summary>
        /// Enumerates all child sequence objects
        /// </summary>
        public abstract IEnumerable<Sequence> Children { get; }

        /// <summary>
        /// The precedence of this operator. Zero is the highest priority, int.MaxValue the lowest.
        /// Used to add needed parentheses for printing sequences
        /// TODO: WTF? das ist im Parser genau umgekehrt implementiert!
        /// </summary>
        public abstract int Precedence { get; }

        /// <summary>
        /// A string symbol representing this sequence type.
        /// </summary>
        public abstract String Symbol { get; }
    }

    /// <summary>
    /// A Sequence with a Special flag
    /// </summary>
    public abstract class SequenceSpecial : Sequence
    {
        /// <summary>
        /// The "Special" flag. Usage is implementation specific.
        /// GrShell uses this flag to indicate breakpoints when in debug mode and
        /// to dump matches when in normal mode.
        /// </summary>
        public bool Special;

        /// <summary>
        /// Initializes a new instance of the SequenceSpecial class.
        /// </summary>
        /// <param name="special">The initial value for the "Special" flag.</param>
        /// <param name="seqType">The sequence type.</param>
        public SequenceSpecial(bool special, SequenceType seqType)
            : base(seqType)
        {
            Special = special;
        }
    }

    /// <summary>
    /// A sequence consisting of a unary operator and another sequence.
    /// </summary>
    public abstract class SequenceUnary : Sequence
    {
        public Sequence Seq;

        public SequenceUnary(Sequence seq, SequenceType seqType) : base(seqType)
        {
            Seq = seq;
        }

        public override IEnumerable<Sequence> Children
        {
            get { yield return Seq; }
        }
    }

    /// <summary>
    /// A sequence consisting of a binary operator and two sequences.
    /// </summary>
    public abstract class SequenceBinary : Sequence
    {
        public Sequence Left;
        public Sequence Right;
        public bool Randomize;

        public SequenceBinary(Sequence left, Sequence right, bool random, SequenceType seqType)
            : base(seqType)
        {
            Left = left;
            Right = right;
            Randomize = random;
        }

        public override IEnumerable<Sequence> Children
        {
            get { yield return Left; yield return Right; }
        }
    }

    public class SequenceThenLeft : SequenceBinary
    {
        public SequenceThenLeft(Sequence left, Sequence right, bool random)
            : base(left, right, random, SequenceType.ThenLeft)
        {
        }

        protected override bool ApplyImpl(IGraph graph)
        {
            bool res;
            if (Randomize && randomGenerator.Next(2) == 1) {
                Right.Apply(graph);
                res = Left.Apply(graph);
            } else {
                res = Left.Apply(graph);
                Right.Apply(graph);
            }
            return res;
        }

        public override int Precedence { get { return 0; } }
        public override string Symbol { get { return "<;"; } }
    }

    public class SequenceThenRight : SequenceBinary
    {
        public SequenceThenRight(Sequence left, Sequence right, bool random)
            : base(left, right, random, SequenceType.ThenRight)
        {
        }

        protected override bool ApplyImpl(IGraph graph)
        {
            bool res;
            if (Randomize && randomGenerator.Next(2) == 1) {
                res = Right.Apply(graph);
                Left.Apply(graph);
            } else {
                Left.Apply(graph);
                res = Right.Apply(graph);
            }
            return res;
        }

        public override int Precedence { get { return 0; } }
        public override string Symbol { get { return ";>"; } }
    }

    public class SequenceLazyOr : SequenceBinary
    {
        public SequenceLazyOr(Sequence left, Sequence right, bool random)
            : base(left, right, random, SequenceType.LazyOr)
        {
        }

        protected override bool ApplyImpl(IGraph graph)
        {
            if(Randomize && randomGenerator.Next(2) == 1)
                return Right.Apply(graph) || Left.Apply(graph);
            else
                return Left.Apply(graph) || Right.Apply(graph);
        }

        public override int Precedence { get { return 1; } }
        public override string Symbol { get { return "||"; } }
    }

    public class SequenceLazyAnd : SequenceBinary
    {
        public SequenceLazyAnd(Sequence left, Sequence right, bool random)
            : base(left, right, random, SequenceType.LazyAnd)
        {
        }

        protected override bool ApplyImpl(IGraph graph)
        {
            if(Randomize && randomGenerator.Next(2) == 1)
                return Right.Apply(graph) && Left.Apply(graph);
            else
                return Left.Apply(graph) && Right.Apply(graph);
        }

        public override int Precedence { get { return 2; } }
        public override string Symbol { get { return "&&"; } }
    }

    public class SequenceStrictOr : SequenceBinary
    {
        public SequenceStrictOr(Sequence left, Sequence right, bool random)
            : base(left, right, random, SequenceType.StrictOr)
        {
        }

        protected override bool ApplyImpl(IGraph graph)
        {
            if(Randomize && randomGenerator.Next(2) == 1)
                return Right.Apply(graph) | Left.Apply(graph);
            else
                return Left.Apply(graph) | Right.Apply(graph);
        }

        public override int Precedence { get { return 3; } }
        public override string Symbol { get { return "|"; } }
    }

    public class SequenceXor : SequenceBinary
    {
        public SequenceXor(Sequence left, Sequence right, bool random)
            : base(left, right, random, SequenceType.Xor)
        {
        }

        protected override bool ApplyImpl(IGraph graph)
        {
            if(Randomize && randomGenerator.Next(2) == 1)
                return Right.Apply(graph) ^ Left.Apply(graph);
            else
                return Left.Apply(graph) ^ Right.Apply(graph);
        }

        public override int Precedence { get { return 4; } }
        public override string Symbol { get { return "^"; } }
    }

    public class SequenceStrictAnd : SequenceBinary
    {
        public SequenceStrictAnd(Sequence left, Sequence right, bool random)
            : base(left, right, random, SequenceType.StrictAnd)
        {
        }

        protected override bool ApplyImpl(IGraph graph)
        {
            if(Randomize && randomGenerator.Next(2) == 1)
                return Right.Apply(graph) & Left.Apply(graph);
            else
                return Left.Apply(graph) & Right.Apply(graph);
        }

        public override int Precedence { get { return 5; } }
        public override string Symbol { get { return "&"; } }
    }

    public class SequenceNot : SequenceUnary
    {
        public SequenceNot(Sequence seq) : base(seq, SequenceType.Not) {}

        protected override bool ApplyImpl(IGraph graph)
        {
            return !Seq.Apply(graph);
        }

        public override int Precedence { get { return 6; } }
        public override string Symbol { get { return "!"; } }
    }

    public class SequenceMin : SequenceUnary
    {
        public long Min;

        public SequenceMin(Sequence seq, long min) : base(seq, SequenceType.Min)
        {
            Min = min;
        }

        protected override bool ApplyImpl(IGraph graph)
        {
            long i = 0;
            while(Seq.Apply(graph))
                i++;
            return i >= Min;
        }

        public override int Precedence { get { return 7; } }
        public override string Symbol { get { return "[" + Min + ":*]"; } }
    }

    public class SequenceMinMax : SequenceUnary
    {
        public long Min, Max;

        public SequenceMinMax(Sequence seq, long min, long max) : base(seq, SequenceType.MinMax)
        {
            Min = min;
            Max = max;
        }

        protected override bool ApplyImpl(IGraph graph)
        {
            long i;
            for(i = 0; i < Max; i++)
            {
                if(!Seq.Apply(graph)) break;
            }
            return i >= Min;
        }

        public override int Precedence { get { return 7; } }
        public override string Symbol { get { return "[" + Min + ":" + Max + "]"; } }
    }

    public class SequenceRule : SequenceSpecial
    {
        public RuleObject RuleObj;
        public bool Test;

        public SequenceRule(RuleObject ruleObj, bool special, bool test)
            : base(special, SequenceType.Rule)
        {
            RuleObj = ruleObj;
            Test = test;
        }

        protected override bool ApplyImpl(IGraph graph)
        {
            return graph.ApplyRewrite(RuleObj, 0, 1, Special, Test) > 0;
        }

        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }

        protected String GetRuleString()
        {
            String sym = "";
            if(RuleObj.ReturnVars.Length > 0 && RuleObj.ReturnVars[0] != null)
                sym = "(" + String.Join(", ", RuleObj.ReturnVars) + ")=";
            sym += RuleObj.Action.Name;
            if(RuleObj.ParamVars.Length > 0)
                sym += "(" + String.Join(", ", RuleObj.ParamVars) + ")";
            return sym;
        }

        public override string Symbol      
        {
            get
            {
                String prefix;
                if(Special)
                {
                    if(Test) prefix = "%?";
                    else prefix = "%";
                }
                else
                {
                    if(Test) prefix = "?";
                    else prefix = "";
                }
                return prefix + GetRuleString();
            }
        }
    }

    public class SequenceRuleAll : SequenceRule
    {
		public int NumChooseRandom;

        public SequenceRuleAll(RuleObject ruleObj, bool special, bool test, int numChooseRandom)
            : base(ruleObj, special, test)
        {
            SequenceType = SequenceType.RuleAll;
			NumChooseRandom = numChooseRandom;
        }

        protected override bool ApplyImpl(IGraph graph)
        {
			if(NumChooseRandom <= 0)
				return graph.ApplyRewrite(RuleObj, -1, -1, Special, Test) > 0;
			else
			{
                // TODO: Code duplication! Compare with BaseGraph.ApplyRewrite.

				int curMaxMatches = graph.MaxMatches;

				object[] parameters;
				if(RuleObj.ParamVars.Length > 0)
				{
					parameters = RuleObj.Parameters;
                    for(int i = 0; i < RuleObj.ParamVars.Length; i++)
                    {
                        // If this parameter is not constant, the according ParamVars entry holds the
                        // name of a variable to be used for the parameter.
                        // Otherwise the parameters entry remains unchanged (it already contains the constant)
                        if(RuleObj.ParamVars[i] != null)
                            parameters[i] = graph.GetVariableValue(RuleObj.ParamVars[i]);
                    }
				}
				else parameters = null;

				if(graph.PerformanceInfo != null) graph.PerformanceInfo.StartLocal();
				IMatches matches = RuleObj.Action.Match(graph, curMaxMatches, parameters);
				if(graph.PerformanceInfo != null)
				{
					graph.PerformanceInfo.StopMatch();              // total match time does NOT include listeners anymore
					graph.PerformanceInfo.MatchesFound += matches.Count;
				}

				graph.Matched(matches, Special);
				if(matches.Count == 0) return false;

				if(Test) return false;

				graph.Finishing(matches, Special);

				if(graph.PerformanceInfo != null) graph.PerformanceInfo.StartLocal();

				object[] retElems = null;
				for(int i = 0; i < NumChooseRandom; i++)
				{
					if(i != 0) graph.RewritingNextMatch();
					IMatch match = matches.RemoveMatch(randomGenerator.Next(matches.Count));
					retElems = matches.Producer.Modify(graph, match);
					if(graph.PerformanceInfo != null) graph.PerformanceInfo.RewritesPerformed++;
				}
				if(retElems == null) retElems = BaseGraph.NoElems;

				for(int i = 0; i < RuleObj.ReturnVars.Length; i++)
					graph.SetVariableValue(RuleObj.ReturnVars[i], retElems[i]);
				if(graph.PerformanceInfo != null) graph.PerformanceInfo.StopRewrite();            // total rewrite time does NOT include listeners anymore

				graph.Finished(matches, Special);

				return true;
			}
        }

        public override string Symbol
        { 
            get 
            {
                String prefix;
				if(NumChooseRandom > 0)
					prefix = "$" + NumChooseRandom;
				else
					prefix = "";
                if(Special)
                {
                    if(Test) prefix += "[%?";
                    else prefix += "[%";
                }
                else
                {
                    if(Test) prefix += "[?";
                    else prefix += "[";
                }
                return prefix + GetRuleString() + "]"; 
            }
        }
    }

    public class SequenceDef : Sequence
    {
        public String[] DefVars;

        public SequenceDef(String[] defVars)
            : base(SequenceType.Def)
        {
            DefVars = defVars;
        }

        protected override bool ApplyImpl(IGraph graph)
        {
            foreach(String defVar in DefVars)
                if(graph.GetVariableValue(defVar) == null) 
                    return false;

            return true;
        }

        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "def(" + String.Join(", ", DefVars) + ")"; } }
    }

    public class SequenceTrue : SequenceSpecial
    {
        public SequenceTrue(bool special)
            : base(special, SequenceType.True)
        {
        }

        protected override bool ApplyImpl(IGraph graph) { return true; }
        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return Special ? "%true" : "true"; } }
    }

    public class SequenceFalse : SequenceSpecial
    {
        public SequenceFalse(bool special)
            : base(special, SequenceType.False)
        {
        }

        protected override bool ApplyImpl(IGraph graph) { return false; }
        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return Special ? "%false" : "false"; } }
    }

    public class SequenceVarPredicate : SequenceSpecial
    {
        public String PredicateVar;

        public SequenceVarPredicate(String varName, bool special)
            : base(special, SequenceType.VarPredicate)
        {
            PredicateVar = varName;
        }

        protected override bool ApplyImpl(IGraph graph)
        {
            object val = graph.GetVariableValue(PredicateVar);
            if(val is bool) return (bool) val;

            throw new InvalidOperationException("The variable '" + PredicateVar + "' is not boolean!");
        }
        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return PredicateVar; } }
    }

    public class SequenceAssignSetmapSizeToVar : Sequence
    {
        public String DestVar;
        public String Setmap;

        public SequenceAssignSetmapSizeToVar(String destVar, String setmap)
            : base(SequenceType.AssignSetmapSizeToVar)
        {
            DestVar = destVar;
            Setmap = setmap;
        }

        protected override bool ApplyImpl(IGraph graph)
        {
            IDictionary setmap = (IDictionary)graph.GetVariableValue(Setmap);
            graph.SetVariableValue(DestVar, setmap.Count);
            return true;
        }

        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return DestVar + "=" + Setmap + ".size()"; } }
    }

    public class SequenceAssignSetmapEmptyToVar : Sequence
    {
        public String DestVar;
        public String Setmap;

        public SequenceAssignSetmapEmptyToVar(String destVar, String setmap)
            : base(SequenceType.AssignSetmapEmptyToVar)
        {
            DestVar = destVar;
            Setmap = setmap;
        }

        protected override bool ApplyImpl(IGraph graph)
        {
            IDictionary setmap = (IDictionary)graph.GetVariableValue(Setmap);
            graph.SetVariableValue(DestVar, setmap.Count==0);
            return true;
        }

        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return DestVar + "=" + Setmap + ".empty()"; } }
    }

    public class SequenceAssignMapAccessToVar : Sequence
        {
        public String DestVar;
        public String Setmap;
        public String KeyVar;

        public SequenceAssignMapAccessToVar(String destVar, String setmap, String keyVar)
            : base(SequenceType.AssignMapAccessToVar)
        {
            DestVar = destVar;
            Setmap = setmap;
            KeyVar = keyVar;
        }

        protected override bool ApplyImpl(IGraph graph)
        {
            IDictionary setmap = (IDictionary)graph.GetVariableValue(Setmap);
            if(!setmap.Contains(graph.GetVariableValue(KeyVar))) return false;
            graph.SetVariableValue(DestVar, setmap[graph.GetVariableValue(KeyVar)]);
            return true;
        }

        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return DestVar + "=" + Setmap + "[" + KeyVar + "]"; } }
    }
    
    public class SequenceAssignSetCreationToVar : Sequence
    {
        public String DestVar;
        public String TypeName;

        public SequenceAssignSetCreationToVar(String destVar, String typeName)
            : base(SequenceType.AssignSetCreationToVar)
        {
            DestVar = destVar;
            TypeName = typeName;
        }

        protected override bool ApplyImpl(IGraph graph)
        {
            Type srcType = DictionaryHelper.GetTypeFromNameForDictionary(TypeName, graph);
            Type dstType = typeof(de.unika.ipd.grGen.libGr.SetValueType);
            graph.SetVariableValue(DestVar, DictionaryHelper.NewDictionary(srcType, dstType));
            return true;
        }

        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return DestVar + "= set<" + TypeName + ">"; } }
    }
    
    public class SequenceAssignMapCreationToVar : Sequence
    {
        public String DestVar;
        public String TypeName;
        public String TypeNameDst;

        public SequenceAssignMapCreationToVar(String destVar, String typeName, String typeNameDst)
            : base(SequenceType.AssignMapCreationToVar)
        {
            DestVar = destVar;
            TypeName = typeName;
            TypeNameDst = typeNameDst;
        }

        protected override bool ApplyImpl(IGraph graph)
        {
            Type srcType = DictionaryHelper.GetTypeFromNameForDictionary(TypeName, graph);
            Type dstType = DictionaryHelper.GetTypeFromNameForDictionary(TypeNameDst, graph);
            graph.SetVariableValue(DestVar, DictionaryHelper.NewDictionary(srcType, dstType));
            return true;
        }

        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return DestVar + "= map<" + TypeName + "," + TypeNameDst + ">"; } }
    }

    public class SequenceAssignVarToVar : Sequence
    {
        public String DestVar;
        public String SourceVar;

        public SequenceAssignVarToVar(String destVar, String sourceVar)
            : base(SequenceType.AssignVarToVar)
        {
            DestVar = destVar;
            SourceVar = sourceVar;
        }

        protected override bool ApplyImpl(IGraph graph)
        {
            graph.SetVariableValue(DestVar, graph.GetVariableValue(SourceVar));
            return true;                    // Semantics changed! Now always returns true, as it is always successful!
        }

        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return DestVar + "=" + SourceVar; } }
    }

    public class SequenceAssignConstToVar : Sequence
    {
        public String DestVar;
        public object Constant;

        public SequenceAssignConstToVar(String destVar, object constant)
            : base(SequenceType.AssignConstToVar)
        {
            DestVar = destVar;
            Constant = constant;
        }

        protected override bool ApplyImpl(IGraph graph)
        {
            graph.SetVariableValue(DestVar, Constant);
            return true;
        }

        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return DestVar + "=" + Constant; } }
    }

    public class SequenceAssignVarToAttribute : Sequence
    {
        public String DestVar;
        public String AttributeName;
        public String SourceVar;

        public SequenceAssignVarToAttribute(String destVar, String attributeName, String sourceVar)
            : base(SequenceType.AssignVarToAttribute)
        {
            DestVar = destVar;
            AttributeName = attributeName;
            SourceVar = sourceVar;
        }

        protected override bool ApplyImpl(IGraph graph)
        {
            object value = graph.GetVariableValue(SourceVar);
            IGraphElement elem = (IGraphElement)graph.GetVariableValue(DestVar);
            AttributeType attrType = elem.Type.GetAttributeType(AttributeName);
            if(attrType.Kind==AttributeKind.SetAttr || attrType.Kind==AttributeKind.MapAttr)
            {
                Type keyType, valueType;
                DictionaryHelper.GetDictionaryTypes(elem.GetAttribute(AttributeName), out keyType, out valueType);
                value = DictionaryHelper.NewDictionary(keyType, valueType, value); // by-value-semantics -> clone dictionary
            }
            AttributeChangeType changeType = AttributeChangeType.Assign;
            if(elem is INode)
                graph.ChangingNodeAttribute((INode)elem, attrType, changeType, value, null);
            else
                graph.ChangingEdgeAttribute((IEdge)elem, attrType, changeType, value, null);
            elem.SetAttribute(AttributeName, value);
            return true;
        }

        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return DestVar + "." + AttributeName + "=" + SourceVar; } }
    }

    public class SequenceAssignAttributeToVar : Sequence
    {
        public String DestVar;
        public String SourceVar;
        public String AttributeName;

        public SequenceAssignAttributeToVar(String destVar, String sourceVar, String attributeName)
            : base(SequenceType.AssignAttributeToVar)
        {
            DestVar = destVar;
            SourceVar = sourceVar;
            AttributeName = attributeName;
        }

        protected override bool ApplyImpl(IGraph graph)
        {
            IGraphElement elem = (IGraphElement)graph.GetVariableValue(SourceVar);
            object value = elem.GetAttribute(AttributeName);
            AttributeType attrType = elem.Type.GetAttributeType(AttributeName);
            if(attrType.Kind==AttributeKind.SetAttr || attrType.Kind==AttributeKind.MapAttr)
            {
                Type keyType, valueType;
                IDictionary dict = DictionaryHelper.GetDictionaryTypes(value, out keyType, out valueType);
                value = DictionaryHelper.NewDictionary(keyType, valueType, dict); // by-value-semantics -> clone dictionary
            }
            graph.SetVariableValue(DestVar, value);
            return true;
        }

        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return DestVar + "=" + SourceVar + "." + AttributeName; } }
    }

    public class SequenceAssignElemToVar : Sequence
    {
        public String DestVar;
        public IGraphElement Element;

        public SequenceAssignElemToVar(String destVar, IGraphElement elem)
            : base(SequenceType.AssignElemToVar)
        {
            DestVar = destVar;
            Element = elem;
        }

        protected override bool ApplyImpl(IGraph graph)
        {
            graph.SetVariableValue(DestVar, Element);
            return true;                    // Semantics changed! Now always returns true, as it is always successful!
        }

        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return DestVar + "=[<someelem>]"; } }
    }

    public class SequenceAssignSequenceResultToVar : Sequence
    {
        public String DestVar;
        public Sequence Seq;

        public SequenceAssignSequenceResultToVar(String destVar, Sequence sequence)
            : base(SequenceType.AssignSequenceResultToVar)
        {
            DestVar = destVar;
            Seq = sequence;
        }

        protected override bool ApplyImpl(IGraph graph)
        {
            bool result = Seq.Apply(graph);
            graph.SetVariableValue(DestVar, result);
            return result;
        }

        public override IEnumerable<Sequence> Children { get { yield return Seq; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return DestVar + "="; } }
    }

    public class SequenceTransaction : SequenceUnary
    {
        public SequenceTransaction(Sequence seq) : base(seq, SequenceType.Transaction) { }

        protected override bool ApplyImpl(IGraph graph)
        {
            int transactionID = graph.TransactionManager.StartTransaction();
            int oldRewritesPerformed;

            if(graph.PerformanceInfo != null) oldRewritesPerformed = graph.PerformanceInfo.RewritesPerformed;
            else oldRewritesPerformed = -1;

            bool res = Seq.Apply(graph);

            if(res) graph.TransactionManager.Commit(transactionID);
            else
            {
                graph.TransactionManager.Rollback(transactionID);
                if(graph.PerformanceInfo != null)
                    graph.PerformanceInfo.RewritesPerformed = oldRewritesPerformed;
            }

            return res;
        }

        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "< ... >"; } }
    }

    public class SequenceIfThenElse : Sequence
    {
        public Sequence Condition;
        public Sequence TrueCase;
        public Sequence FalseCase;

        public SequenceIfThenElse(Sequence condition, Sequence trueCase, Sequence falseCase)
            : base(SequenceType.IfThenElse)
        {
            Condition = condition;
            TrueCase = trueCase;
            FalseCase = falseCase;
        }

        protected override bool ApplyImpl(IGraph graph)
        {
            return Condition.Apply(graph) ? TrueCase.Apply(graph) : FalseCase.Apply(graph);
        }

        public override IEnumerable<Sequence> Children { get { yield return Condition; yield return TrueCase; yield return FalseCase; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "if{ ... ; ... ; ...}"; } }
    }

    public class SequenceIfThen : SequenceBinary
    {
        public SequenceIfThen(Sequence condition, Sequence trueCase)
            : base(condition, trueCase, false, SequenceType.IfThen)
        {
        }

        protected override bool ApplyImpl(IGraph graph)
        {
            return Left.Apply(graph) ? Right.Apply(graph) : true; // lazy implication
        }

        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "if{ ... ; ...}"; } }
    }

    public class SequenceFor : SequenceUnary
    {
        public String Var;
        public String VarDst;
        public String Setmap;

        public SequenceFor(String var, String varDst, String setmap, Sequence seq)
            : base(seq, SequenceType.For)
        {
            Var = var;
            VarDst = varDst;
            Setmap = setmap;
        }

        protected override bool ApplyImpl(IGraph graph)
        {
            IDictionary setmap = (IDictionary)graph.GetVariableValue(Setmap);
            bool res = true;
            foreach(DictionaryEntry entry in setmap)
            {
                graph.SetVariableValue(Var, entry.Key);
                if(VarDst != null) {
                    graph.SetVariableValue(VarDst, entry.Value);
                }
                res &= Seq.Apply(graph);
            }
            return res;
        }

        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "for{"+Var+(VarDst!=null?"->"+VarDst:"")+" in "+Setmap+"; ...}"; } }
    }

    public class SequenceSetmapAdd : Sequence
    {
        public String Setmap;
        public String Var;
        public String VarDst;

        public SequenceSetmapAdd(String setmap, String var, String varDst)
            : base(SequenceType.SetmapAdd)
        {
            Setmap = setmap;
            Var = var;
            VarDst = varDst;
        }

        protected override bool ApplyImpl(IGraph graph)
        {
            IDictionary setmap = (IDictionary)graph.GetVariableValue(Setmap);
            if(setmap.Contains(graph.GetVariableValue(Var))) {
                setmap[graph.GetVariableValue(Var)] = (VarDst == null ? null : graph.GetVariableValue(VarDst));
            } else {
                setmap.Add(graph.GetVariableValue(Var), (VarDst == null ? null : graph.GetVariableValue(VarDst)));
            }
            return true;
        }

        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return Setmap+".add("+Var+(VarDst!=null?"->"+VarDst:"")+")"; } }
    }

    public class SequenceSetmapRem : Sequence
    {
        public String Setmap;
        public String Var;

        public SequenceSetmapRem(String setmap, String var)
            : base(SequenceType.SetmapRem)
        {
            Setmap = setmap;
            Var = var;
        }

        protected override bool ApplyImpl(IGraph graph)
        {
            IDictionary setmap = (IDictionary)graph.GetVariableValue(Setmap);
            setmap.Remove(graph.GetVariableValue(Var));
            return true;
        }

        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return Setmap+".rem("+Var+")"; } }
    }

    public class SequenceSetmapClear : Sequence
    {
        public String Setmap;

        public SequenceSetmapClear(String setmap)
            : base(SequenceType.SetmapClear)
        {
            Setmap = setmap;
        }

        protected override bool ApplyImpl(IGraph graph)
        {
            IDictionary setmap = (IDictionary)graph.GetVariableValue(Setmap);
            setmap.Clear();
            return true;
        }

        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return Setmap + ".clear()"; } }
    }

    public class SequenceIn: Sequence
    {
        public String Var;
        public String Setmap;

        public SequenceIn(String var, String setmap)
            : base(SequenceType.InSetmap)
        {
            Var = var;
            Setmap = setmap;
        }

        protected override bool ApplyImpl(IGraph graph)
        {
            IDictionary setmap = (IDictionary)graph.GetVariableValue(Setmap);
            return setmap.Contains(graph.GetVariableValue(Var));
        }

        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return Var + " in " + Setmap; } }
    }
}

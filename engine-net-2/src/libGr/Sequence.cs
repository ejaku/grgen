/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2010 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// Specifies the actual subtype used for a Sequence.
    /// A new sequence type -> you must adapt lgspSequenceChecker and lgspSequenceGenerator, 
    /// SequenceChecker and Sequence (add the corresponding class down below), the Debugger
    /// </summary>
    public enum SequenceType
    {
        ThenLeft, ThenRight, LazyOr, LazyAnd, StrictOr, Xor, StrictAnd, Not, IterationMin, IterationMinMax,
        Rule, RuleAll, Def, True, False, VarPredicate,
        AssignVAllocToVar, AssignSetmapSizeToVar, AssignSetmapEmptyToVar, AssignMapAccessToVar,
        AssignVarToVar, AssignElemToVar, AssignSequenceResultToVar,
        AssignConstToVar, AssignAttributeToVar, AssignVarToAttribute,
        IsVisited, SetVisited, VFree, VReset, Emit,
        SetmapAdd, SetmapRem, SetmapClear, InSetmap, 
        Transaction, IfThenElse, IfThen, For
    }

    /// <summary>
    /// Environment for sequence exection giving access to graph element names and user interface 
    /// </summary>
    public interface SequenceExecutionEnvironment
    {
        /// <summary>
        /// returns the named graph on which the sequence is to be executed, containing the names
        /// </summary>
        NamedGraph GetNamedGraph();
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

            id = idSource;
            ++idSource;
        }

        /// <summary>
        /// Applies this sequence.
        /// </summary>
        /// <param name="graph">The graph on which this sequence is to be applied.
        ///     The rules will only be chosen during the Sequence object instantiation, so
        ///     exchanging rules will have no effect for already existing Sequence objects.</param>
        /// <param name="env">The execution environment giving access to the names and user interface (null if not available)</param>
        /// <returns>True, iff the sequence succeeded</returns>
        public bool Apply(IGraph graph, SequenceExecutionEnvironment env)
        {
            graph.EnteringSequence(this);
            bool res = ApplyImpl(graph, env);
            graph.ExitingSequence(this);
            return res;
        }

        /// <summary>
        /// Applies this sequence. This function represents the actual implementation of the sequence.
        /// </summary>
        /// <param name="graph">The graph on which this sequence is to be applied.</param>
        /// <param name="env">The execution environment giving access to the names and user interface (null if not available)</param>
        /// <returns>True, iff the sequence succeeded</returns>
        protected abstract bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env);

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

        /// <summary>
        /// returns the sequence id - every sequence is assigned a unique id used in xgrs code generation
        /// </summary>
        public int Id { get { return id; } }

        /// <summary>
        /// stores the sequence unique id
        /// </summary>
        private int id;

        /// <summary>
        /// the static member used to assign the unique ids to the sequence instances
        /// </summary>
        private static int idSource = 0;
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

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            bool res;
            if (Randomize && randomGenerator.Next(2) == 1) {
                Right.Apply(graph, env);
                res = Left.Apply(graph, env);
            } else {
                res = Left.Apply(graph, env);
                Right.Apply(graph, env);
            }
            return res;
        }

        public override int Precedence { get { return 0; } }
        public override string Symbol { get { return Randomize ? "$<;" : "<;"; } }
    }

    public class SequenceThenRight : SequenceBinary
    {
        public SequenceThenRight(Sequence left, Sequence right, bool random)
            : base(left, right, random, SequenceType.ThenRight)
        {
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            bool res;
            if (Randomize && randomGenerator.Next(2) == 1) {
                res = Right.Apply(graph, env);
                Left.Apply(graph, env);
            } else {
                Left.Apply(graph, env);
                res = Right.Apply(graph, env);
            }
            return res;
        }

        public override int Precedence { get { return 0; } }
        public override string Symbol { get { return Randomize ? "$;>" : ";>"; } }
    }

    public class SequenceLazyOr : SequenceBinary
    {
        public SequenceLazyOr(Sequence left, Sequence right, bool random)
            : base(left, right, random, SequenceType.LazyOr)
        {
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            if(Randomize && randomGenerator.Next(2) == 1)
                return Right.Apply(graph, env) || Left.Apply(graph, env);
            else
                return Left.Apply(graph, env) || Right.Apply(graph, env);
        }

        public override int Precedence { get { return 1; } }
        public override string Symbol { get { return Randomize ? "$||" : "||"; } }
    }

    public class SequenceLazyAnd : SequenceBinary
    {
        public SequenceLazyAnd(Sequence left, Sequence right, bool random)
            : base(left, right, random, SequenceType.LazyAnd)
        {
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            if(Randomize && randomGenerator.Next(2) == 1)
                return Right.Apply(graph, env) && Left.Apply(graph, env);
            else
                return Left.Apply(graph, env) && Right.Apply(graph, env);
        }

        public override int Precedence { get { return 2; } }
        public override string Symbol { get { return Randomize ? "$&&" : "&&"; } }
    }

    public class SequenceStrictOr : SequenceBinary
    {
        public SequenceStrictOr(Sequence left, Sequence right, bool random)
            : base(left, right, random, SequenceType.StrictOr)
        {
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            if(Randomize && randomGenerator.Next(2) == 1)
                return Right.Apply(graph, env) | Left.Apply(graph, env);
            else
                return Left.Apply(graph, env) | Right.Apply(graph, env);
        }

        public override int Precedence { get { return 3; } }
        public override string Symbol { get { return Randomize ? "$|" : "|"; } }
    }

    public class SequenceXor : SequenceBinary
    {
        public SequenceXor(Sequence left, Sequence right, bool random)
            : base(left, right, random, SequenceType.Xor)
        {
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            if(Randomize && randomGenerator.Next(2) == 1)
                return Right.Apply(graph, env) ^ Left.Apply(graph, env);
            else
                return Left.Apply(graph, env) ^ Right.Apply(graph, env);
        }

        public override int Precedence { get { return 4; } }
        public override string Symbol { get { return Randomize ? "$^" : "^"; } }
    }

    public class SequenceStrictAnd : SequenceBinary
    {
        public SequenceStrictAnd(Sequence left, Sequence right, bool random)
            : base(left, right, random, SequenceType.StrictAnd)
        {
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            if(Randomize && randomGenerator.Next(2) == 1)
                return Right.Apply(graph, env) & Left.Apply(graph, env);
            else
                return Left.Apply(graph, env) & Right.Apply(graph, env);
        }

        public override int Precedence { get { return 5; } }
        public override string Symbol { get { return Randomize ? "$&" : "&"; } }
    }

    public class SequenceNot : SequenceUnary
    {
        public SequenceNot(Sequence seq) : base(seq, SequenceType.Not) {}

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            return !Seq.Apply(graph, env);
        }

        public override int Precedence { get { return 6; } }
        public override string Symbol { get { return "!"; } }
    }

    public class SequenceIterationMin : SequenceUnary
    {
        public long Min;

        public SequenceIterationMin(Sequence seq, long min) : base(seq, SequenceType.IterationMin)
        {
            Min = min;
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            long i = 0;
            while (Seq.Apply(graph, env))
                i++;
            return i >= Min;
        }

        public override int Precedence { get { return 7; } }
        public override string Symbol { get { return "[" + Min + ":*]"; } }
    }

    public class SequenceIterationMinMax : SequenceUnary
    {
        public long Min, Max;

        public SequenceIterationMinMax(Sequence seq, long min, long max) : base(seq, SequenceType.IterationMinMax)
        {
            Min = min;
            Max = max;
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            long i;
            for(i = 0; i < Max; i++)
            {
                if (!Seq.Apply(graph, env)) break;
            }
            return i >= Min;
        }

        public override int Precedence { get { return 7; } }
        public override string Symbol { get { return "[" + Min + ":" + Max + "]"; } }
    }

    public class SequenceRule : SequenceSpecial
    {
        public RuleInvocationParameterBindings ParamBindings;

        public bool Test;

        public SequenceRule(RuleInvocationParameterBindings paramBindings, bool special, bool test)
            : base(special, SequenceType.Rule)
        {
            ParamBindings = paramBindings;
            Test = test;
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            return graph.ApplyRewrite(ParamBindings, 0, 1, Special, Test) > 0;
        }

        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }

        protected String GetRuleString()
        {
            StringBuilder sb = new StringBuilder();
            if(ParamBindings.ReturnVars.Length > 0 && ParamBindings.ReturnVars[0] != null)
            {
                sb.Append("(");
                for(int i = 0; i < ParamBindings.ReturnVars.Length; ++i)
                {
                    sb.Append(ParamBindings.ReturnVars[i].Name);
                    if(i != ParamBindings.ReturnVars.Length - 1) sb.Append(",");
                }
                sb.Append(")=");
            }
            sb.Append(ParamBindings.Action.Name);
            if(ParamBindings.ParamVars.Length > 0)
            {
                sb.Append("(");
                for(int i = 0; i < ParamBindings.ParamVars.Length; ++i)
                {
                    sb.Append(ParamBindings.ParamVars[i].Name);
                    if(i != ParamBindings.ParamVars.Length - 1) sb.Append(",");
                }
                sb.Append(")");
            }
            return sb.ToString();
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
        public bool ChooseRandom;
		public SequenceVariable VarChooseRandom;

        public SequenceRuleAll(RuleInvocationParameterBindings paramBindings, bool special, bool test, 
            bool chooseRandom, SequenceVariable varChooseRandom)
            : base(paramBindings, special, test)
        {
            SequenceType = SequenceType.RuleAll;
            ChooseRandom = chooseRandom;
			VarChooseRandom = varChooseRandom;
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
			if(!ChooseRandom)
				return graph.ApplyRewrite(ParamBindings, -1, -1, Special, Test) > 0;
			else
			{
                // TODO: Code duplication! Compare with BaseGraph.ApplyRewrite.

				int curMaxMatches = graph.MaxMatches;

				object[] parameters;
				if(ParamBindings.ParamVars.Length > 0)
				{
					parameters = ParamBindings.Parameters;
                    for(int i = 0; i < ParamBindings.ParamVars.Length; i++)
                    {
                        // If this parameter is not constant, the according ParamVars entry holds the
                        // name of a variable to be used for the parameter.
                        // Otherwise the parameters entry remains unchanged (it already contains the constant)
                        if(ParamBindings.ParamVars[i] != null)
                            parameters[i] = ParamBindings.ParamVars[i].GetVariableValue(graph);
                    }
				}
				else parameters = null;

				if(graph.PerformanceInfo != null) graph.PerformanceInfo.StartLocal();
				IMatches matches = ParamBindings.Action.Match(graph, curMaxMatches, parameters);
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

                object val = VarChooseRandom!=null ? VarChooseRandom.GetVariableValue(graph) : 1;
                if(!(val is int)) 
                    throw new InvalidOperationException("The variable '" + VarChooseRandom + "' is not of type int!");
                int numChooseRandom = (int)val;
                if(matches.Count < numChooseRandom) numChooseRandom = matches.Count;

				object[] retElems = null;
				for(int i = 0; i < numChooseRandom; i++)
				{
					if(i != 0) graph.RewritingNextMatch();
					IMatch match = matches.RemoveMatch(randomGenerator.Next(matches.Count));
					retElems = matches.Producer.Modify(graph, match);
					if(graph.PerformanceInfo != null) graph.PerformanceInfo.RewritesPerformed++;
				}
				if(retElems == null) retElems = BaseGraph.NoElems;

				for(int i = 0; i < ParamBindings.ReturnVars.Length; i++)
                    ParamBindings.ReturnVars[i].SetVariableValue(retElems[i], graph);
				if(graph.PerformanceInfo != null) graph.PerformanceInfo.StopRewrite();            // total rewrite time does NOT include listeners anymore

				graph.Finished(matches, Special);

				return true;
			}
        }

        public override string Symbol
        { 
            get 
            {
                String prefix = "";
				if(ChooseRandom) {
					prefix = "$";
                    if(VarChooseRandom != null)
                        prefix += VarChooseRandom.Name;
                }
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
        public SequenceVariable[] DefVars;

        public SequenceDef(SequenceVariable[] defVars)
            : base(SequenceType.Def)
        {
            DefVars = defVars;
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            foreach(SequenceVariable defVar in DefVars)
            {
                if(defVar.GetVariableValue(graph) == null) 
                    return false;
            }
            return true;
        }

        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get {
                StringBuilder sb = new StringBuilder(); 
                sb.Append("def(");
                for(int i=0; i<DefVars.Length; ++i)
                {
                    sb.Append(DefVars[i].Name);
                    if(i!=DefVars.Length-1) sb.Append(",");
                }
                sb.Append(")");
                return sb.ToString();
            }
        }
    }

    public class SequenceTrue : SequenceSpecial
    {
        public SequenceTrue(bool special)
            : base(special, SequenceType.True)
        {
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env) { return true; }
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

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env) { return false; }
        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return Special ? "%false" : "false"; } }
    }

    public class SequenceVarPredicate : SequenceSpecial
    {
        public SequenceVariable PredicateVar;

        public SequenceVarPredicate(SequenceVariable var, bool special)
            : base(special, SequenceType.VarPredicate)
        {
            PredicateVar = var;
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            object val = PredicateVar.GetVariableValue(graph);
            if(val is bool) return (bool)val;
            throw new InvalidOperationException("The variable '" + PredicateVar + "' is not boolean!");
        }
        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return Special ? "%"+PredicateVar.Name : PredicateVar.Name; } }
    }

    public class SequenceAssignVAllocToVar : Sequence
    {
        public SequenceVariable DestVar;

        public SequenceAssignVAllocToVar(SequenceVariable destVar)
            : base(SequenceType.AssignVAllocToVar)
        {
            DestVar = destVar;
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            DestVar.SetVariableValue(graph.AllocateVisitedFlag(), graph);
            return true;
        }

        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return DestVar.Name + "=valloc()"; } }
    }

    public class SequenceAssignSetmapSizeToVar : Sequence
    {
        public SequenceVariable DestVar;
        public SequenceVariable Setmap;

        public SequenceAssignSetmapSizeToVar(SequenceVariable destVar, SequenceVariable setmap)
            : base(SequenceType.AssignSetmapSizeToVar)
        {
            DestVar = destVar;
            Setmap = setmap;
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            IDictionary setmap = (IDictionary)Setmap.GetVariableValue(graph);
            DestVar.SetVariableValue(setmap.Count, graph);
            return true;
        }

        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return DestVar.Name + "=" + Setmap.Name + ".size()"; } }
    }

    public class SequenceAssignSetmapEmptyToVar : Sequence
    {
        public SequenceVariable DestVar;
        public SequenceVariable Setmap;

        public SequenceAssignSetmapEmptyToVar(SequenceVariable destVar, SequenceVariable setmap)
            : base(SequenceType.AssignSetmapEmptyToVar)
        {
            DestVar = destVar;
            Setmap = setmap;
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            IDictionary setmap = (IDictionary)Setmap.GetVariableValue(graph);
            DestVar.SetVariableValue(setmap.Count == 0, graph);
            return true;
        }

        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return DestVar.Name + "=" + Setmap.Name + ".empty()"; } }
    }

    public class SequenceAssignMapAccessToVar : Sequence
        {
        public SequenceVariable DestVar;
        public SequenceVariable Setmap;
        public SequenceVariable KeyVar;

        public SequenceAssignMapAccessToVar(SequenceVariable destVar, SequenceVariable setmap, SequenceVariable keyVar)
            : base(SequenceType.AssignMapAccessToVar)
        {
            DestVar = destVar;
            Setmap = setmap;
            KeyVar = keyVar;
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            IDictionary setmap = (IDictionary)Setmap.GetVariableValue(graph);
            object keyVar = KeyVar.GetVariableValue(graph);
            if(!setmap.Contains(keyVar)) return false;
            DestVar.SetVariableValue(setmap[keyVar], graph);
            return true;
        }

        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return DestVar.Name + "=" + Setmap.Name + "[" + KeyVar.Name + "]"; } }
    }
    
    public class SequenceAssignVarToVar : Sequence
    {
        public SequenceVariable DestVar;
        public SequenceVariable SourceVar;

        public SequenceAssignVarToVar(SequenceVariable destVar, SequenceVariable sourceVar)
            : base(SequenceType.AssignVarToVar)
        {
            DestVar = destVar;
            SourceVar = sourceVar;
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            DestVar.SetVariableValue(SourceVar.GetVariableValue(graph), graph);
            return true;                    // Semantics changed! Now always returns true, as it is always successful!
        }

        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return DestVar.Name + "=" + SourceVar.Name; } }
    }

    public class SequenceAssignConstToVar : Sequence
    {
        public SequenceVariable DestVar;
        public object Constant; 

        public SequenceAssignConstToVar(SequenceVariable destVar, object constant)
            : base(SequenceType.AssignConstToVar)
        {
            DestVar = destVar;
            Constant = constant;
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            DestVar.SetVariableValue(Constant, graph);
            return true;
        }

        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { 
            if(Constant.GetType().Name=="Dictionary`2")
                return DestVar.Name + "={}"; // only empty set/map assignment possible as of now
            else
                return DestVar.Name + "=" + Constant; }
        }
    }

    public class SequenceAssignVarToAttribute : Sequence
    {
        public SequenceVariable DestVar;
        public String AttributeName;
        public SequenceVariable SourceVar;

        public SequenceAssignVarToAttribute(SequenceVariable destVar, String attributeName, SequenceVariable sourceVar)
            : base(SequenceType.AssignVarToAttribute)
        {
            DestVar = destVar;
            AttributeName = attributeName;
            SourceVar = sourceVar;
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            object value = SourceVar.GetVariableValue(graph);
            IGraphElement elem = (IGraphElement)DestVar.GetVariableValue(graph);
            AttributeType attrType;
            value = DictionaryHelper.IfAttributeOfElementIsDictionaryThenCloneDictionaryValue(
                elem, AttributeName, value, out attrType);
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
        public override string Symbol { get { return DestVar.Name + "." + AttributeName + "=" + SourceVar.Name; } }
    }

    public class SequenceAssignAttributeToVar : Sequence
    {
        public SequenceVariable DestVar;
        public SequenceVariable SourceVar;
        public String AttributeName;

        public SequenceAssignAttributeToVar(SequenceVariable destVar, SequenceVariable sourceVar, String attributeName)
            : base(SequenceType.AssignAttributeToVar)
        {
            DestVar = destVar;
            SourceVar = sourceVar;
            AttributeName = attributeName;
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            IGraphElement elem = (IGraphElement)SourceVar.GetVariableValue(graph);
            object value = elem.GetAttribute(AttributeName);
            AttributeType attrType;
            value = DictionaryHelper.IfAttributeOfElementIsDictionaryThenCloneDictionaryValue(
                elem, AttributeName, value, out attrType);
            DestVar.SetVariableValue(value, graph);
            return true;
        }

        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return DestVar.Name + "=" + SourceVar.Name + "." + AttributeName; } }
    }

    public class SequenceAssignElemToVar : Sequence
    {
        public SequenceVariable DestVar;
        public String ElementName;

        public SequenceAssignElemToVar(SequenceVariable destVar, String elemName)
            : base(SequenceType.AssignElemToVar)
        {
            DestVar = destVar;
            ElementName = elemName;
            if(ElementName[0]=='\"') ElementName = ElementName.Substring(1, ElementName.Length-2); 
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            if(env==null && !(graph is NamedGraph))
                throw new InvalidOperationException("The @-operator can only be used with NamedGraphs!");
            NamedGraph namedGraph = null;
            if(env!=null) namedGraph = env.GetNamedGraph();
            if(env==null) namedGraph = (NamedGraph)graph;
            IGraphElement elem = namedGraph.GetGraphElement(ElementName);
            if(elem == null)
                throw new InvalidOperationException("Graph element does not exist: \"" + ElementName + "\"!");
            DestVar.SetVariableValue(elem, graph);
            return true;                    // Semantics changed! Now always returns true, as it is always successful!
        }

        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return DestVar.Name + "=@("+ElementName+")"; } }
    }

    public class SequenceAssignSequenceResultToVar : Sequence
    {
        public SequenceVariable DestVar;
        public Sequence Seq;

        public SequenceAssignSequenceResultToVar(SequenceVariable destVar, Sequence sequence)
            : base(SequenceType.AssignSequenceResultToVar)
        {
            DestVar = destVar;
            Seq = sequence;
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            bool result = Seq.Apply(graph, env);
            DestVar.SetVariableValue(result, graph);
            return true; // Semantics changed! Now always returns true, as it is always successful!
        }

        public override IEnumerable<Sequence> Children { get { yield return Seq; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "(" + DestVar.Name + ")=..."; } }
    }

    public class SequenceTransaction : SequenceUnary
    {
        public SequenceTransaction(Sequence seq) : base(seq, SequenceType.Transaction) { }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            int transactionID = graph.TransactionManager.StartTransaction();
            int oldRewritesPerformed;

            if(graph.PerformanceInfo != null) oldRewritesPerformed = graph.PerformanceInfo.RewritesPerformed;
            else oldRewritesPerformed = -1;

            bool res = Seq.Apply(graph, env);

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

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            return Condition.Apply(graph, env) ? TrueCase.Apply(graph, env) : FalseCase.Apply(graph, env);
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

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            return Left.Apply(graph, env) ? Right.Apply(graph, env) : true; // lazy implication
        }

        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "if{ ... ; ...}"; } }
    }

    public class SequenceFor : SequenceUnary
    {
        public SequenceVariable Var;
        public SequenceVariable VarDst;
        public SequenceVariable Setmap;

        public SequenceFor(SequenceVariable var, SequenceVariable varDst, SequenceVariable setmap, Sequence seq)
            : base(seq, SequenceType.For)
        {
            Var = var;
            VarDst = varDst;
            Setmap = setmap;
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            IDictionary setmap = (IDictionary)Setmap.GetVariableValue(graph);
            bool res = true;
            foreach(DictionaryEntry entry in setmap)
            {
                Var.SetVariableValue(entry.Key, graph);
                if(VarDst != null) {
                    VarDst.SetVariableValue(entry.Value, graph);
                }
                res &= Seq.Apply(graph, env);
            }
            return res;
        }

        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "for{"+Var.Name+(VarDst!=null?"->"+VarDst.Name:"")+" in "+Setmap.Name+"; ...}"; } }
    }

    public class SequenceIsVisited : Sequence
    {
        public SequenceVariable GraphElementVar;
        public SequenceVariable VisitedFlagVar;

        public SequenceIsVisited(SequenceVariable graphElementVar, SequenceVariable visitedFlagVar)
            : base(SequenceType.IsVisited)
        {
            GraphElementVar = graphElementVar;
            VisitedFlagVar = visitedFlagVar;
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            IGraphElement elem = (IGraphElement)GraphElementVar.GetVariableValue(graph);
            int visitedFlag = (int)VisitedFlagVar.GetVariableValue(graph);
            return graph.IsVisited(elem, visitedFlag);
        }

        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return GraphElementVar.Name+".visited["+VisitedFlagVar.Name+"]"; } }
    }

    public class SequenceSetVisited : Sequence
    {
        public SequenceVariable GraphElementVar;
        public SequenceVariable VisitedFlagVar;
        public SequenceVariable Var; // if Var!=null take Var, otherwise Val
        public bool Val;

        public SequenceSetVisited(SequenceVariable graphElementVar, SequenceVariable visitedFlagVar, SequenceVariable var)
            : base(SequenceType.SetVisited)
        {
            GraphElementVar = graphElementVar;
            VisitedFlagVar = visitedFlagVar;
            Var = var;
        }

        public SequenceSetVisited(SequenceVariable graphElementVar, SequenceVariable visitedFlagVar, bool val)
            : base(SequenceType.SetVisited)
        {
            GraphElementVar = graphElementVar;
            VisitedFlagVar = visitedFlagVar;
            Val = val;
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            IGraphElement elem = (IGraphElement)GraphElementVar.GetVariableValue(graph);
            int visitedFlag = (int)VisitedFlagVar.GetVariableValue(graph);
            bool value;
            if(Var!=null) {
                value = (bool)Var.GetVariableValue(graph);
            } else {
                value = Val;
            }
            graph.SetVisited(elem, visitedFlag, value);
            return true;
        }

        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return GraphElementVar.Name+".visited["+VisitedFlagVar.Name+"]="+(Var!=null ? Var.Name : Val.ToString()); } }
    }

    public class SequenceVFree : Sequence
    {
        public SequenceVariable VisitedFlagVar;

        public SequenceVFree(SequenceVariable visitedFlagVar)
            : base(SequenceType.VFree)
        {
            VisitedFlagVar = visitedFlagVar;
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            int visitedFlag = (int)VisitedFlagVar.GetVariableValue(graph);
            graph.FreeVisitedFlag(visitedFlag);
            return true;
        }

        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "vfree("+VisitedFlagVar.Name+")"; } }
    }

    public class SequenceVReset : Sequence
    {
        public SequenceVariable VisitedFlagVar;

        public SequenceVReset(SequenceVariable visitedFlagVar)
            : base(SequenceType.VReset)
        {
            VisitedFlagVar = visitedFlagVar;
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            int visitedFlag = (int)VisitedFlagVar.GetVariableValue(graph);
            graph.ResetVisitedFlag(visitedFlag);
            return true;
        }

        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "vreset("+VisitedFlagVar.Name+")"; } }
    }

    public class SequenceEmit : Sequence
    {
        public String Text;
        public SequenceVariable Variable;

        public SequenceEmit(String text)
            : base(SequenceType.Emit)
        {
            Text = text;
            Text = Text.Replace("\\n", "\n");
            Text = Text.Replace("\\r", "\r");
            Text = Text.Replace("\\t", "\t");
        }

        public SequenceEmit(SequenceVariable var)
            : base(SequenceType.Emit)
        {
            Variable = var;
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            if(Variable!=null) {
                object val = Variable.GetVariableValue(graph);
                if(val!=null) {
                    if(val is IDictionary) graph.EmitWriter.Write(DictionaryHelper.ToString((IDictionary)val));
                    else graph.EmitWriter.Write(val.ToString());
                }
            } else {
                graph.EmitWriter.Write(Text);
            }
            return true;
        }

        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return Variable!=null ? "emit("+Variable.Name+")" : "emit("+Text+")"; } }
    }

    public class SequenceSetmapAdd : Sequence
    {
        public SequenceVariable Setmap;
        public SequenceVariable Var;
        public SequenceVariable VarDst;

        public SequenceSetmapAdd(SequenceVariable setmap, SequenceVariable var, SequenceVariable varDst)
            : base(SequenceType.SetmapAdd)
        {
            Setmap = setmap;
            Var = var;
            VarDst = varDst;
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            IDictionary setmap = (IDictionary)Setmap.GetVariableValue(graph);
            if(setmap.Contains(Var.GetVariableValue(graph))) {
                setmap[Var.GetVariableValue(graph)] = (VarDst == null ? null : VarDst.GetVariableValue(graph));
            } else {
                setmap.Add(Var.GetVariableValue(graph), (VarDst == null ? null : VarDst.GetVariableValue(graph)));
            }
            return true;
        }

        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return Setmap.Name+".add("+Var.Name+(VarDst!=null?","+VarDst.Name:"")+")"; } }
    }

    public class SequenceSetmapRem : Sequence
    {
        public SequenceVariable Setmap;
        public SequenceVariable Var;

        public SequenceSetmapRem(SequenceVariable setmap, SequenceVariable var)
            : base(SequenceType.SetmapRem)
        {
            Setmap = setmap;
            Var = var;
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            IDictionary setmap = (IDictionary)Setmap.GetVariableValue(graph);
            setmap.Remove(Var.GetVariableValue(graph));
            return true;
        }

        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return Setmap.Name+".rem("+Var.Name+")"; } }
    }

    public class SequenceSetmapClear : Sequence
    {
        public SequenceVariable Setmap;

        public SequenceSetmapClear(SequenceVariable setmap)
            : base(SequenceType.SetmapClear)
        {
            Setmap = setmap;
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            IDictionary setmap = (IDictionary)Setmap.GetVariableValue(graph);
            setmap.Clear();
            return true;
        }

        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return Setmap.Name + ".clear()"; } }
    }

    public class SequenceIn: Sequence
    {
        public SequenceVariable Var;
        public SequenceVariable Setmap;

        public SequenceIn(SequenceVariable var, SequenceVariable setmap)
            : base(SequenceType.InSetmap)
        {
            Var = var;
            Setmap = setmap;
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            IDictionary setmap = (IDictionary)Setmap.GetVariableValue(graph);
            return setmap.Contains(Var.GetVariableValue(graph));
        }

        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return Var.Name + " in " + Setmap.Name; } }
    }
}

/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.7
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

//#define LOG_SEQUENCE_EXECUTION

using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// Specifies the actual subtype used for a Sequence.
    /// A new sequence type -> you must adapt lgspSequenceChecker and lgspSequenceGenerator, 
    /// SequenceChecker and Sequence (add the corresponding class down below), the Debugger
    /// </summary>
    public enum SequenceType
    {
        ThenLeft, ThenRight, LazyOr, LazyAnd, StrictOr, Xor, StrictAnd, Not, 
        IterationMin, IterationMinMax,
        Rule, RuleAll, Def, Yield, True, False, VarPredicate,
        AssignVAllocToVar, AssignSetmapSizeToVar, AssignSetmapEmptyToVar, AssignMapAccessToVar,
        AssignVarToVar, AssignElemToVar, AssignSequenceResultToVar,
        AssignUserInputToVar, AssignRandomToVar,
        AssignConstToVar, AssignAttributeToVar, AssignVarToAttribute,
        IsVisited, SetVisited, VFree, VReset, Emit, Record,
        SetmapAdd, SetmapRem, SetmapClear, InSetmap,
        LazyOrAll, LazyAndAll, StrictOrAll, StrictAndAll, SomeFromSet,
        Transaction, Backtrack, IfThenElse, IfThen, For
    }

    /// <summary>
    /// States of executing sequences: not (yet) executed, execution underway, successful execution, fail execution
    /// </summary>
    public enum SequenceExecutionState
    {
        NotYet, Underway, Success, Fail
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

        /// <summary>
        /// returns the maybe user altered direction of execution for the sequence given
        /// the randomly chosen directions is supplied; 0: execute left operand first, 1: execute right operand first
        /// </summary>
        int ChooseDirection(int direction, Sequence seq);

        /// <summary>
        /// returns the maybe user altered sequence to execute next for the sequence given
        /// the randomly chosen sequence is supplied; the object with all available sequences is supplied
        /// </summary>
        int ChooseSequence(int seqToExecute, List<Sequence> sequences, SequenceNAry seq);

        /// <summary>
        /// returns the maybe user altered match to execute next for the sequence given
        /// the randomly chosen total match is supplied; the sequence with the rules and matches is supplied
        /// </summary>
        int ChooseMatch(int totalMatchExecute, SequenceSomeFromSet seq);

        /// <summary>
        /// returns the maybe user altered match to apply next for the sequence given
        /// the randomly chosen match is supplied; the object with all available matches is supplied
        /// </summary>
        int ChooseMatch(int matchToApply, IMatches matches, int numFurtherMatchesToApply, Sequence seq);

        /// <summary>
        /// returns the maybe user altered random number in the range 0 - upperBound exclusive for the sequence given
        /// the random number chosen is supplied
        /// </summary>
        int ChooseRandomNumber(int randomNumber, int upperBound, Sequence seq);

        /// <summary>
        /// returns a user chosen/input value of the given type
        /// no random input value is supplied, the user must give a value
        /// </summary>
        object ChooseValue(string type, Sequence seq);

        /// <summary>
        /// informs debugger about the end of a loop iteration, so it can display the state at the end of the iteration
        /// </summary>
        void EndOfIteration(bool continueLoop, Sequence seq);
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

            executionState = SequenceExecutionState.NotYet;
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
            executionState = SequenceExecutionState.Underway;
#if LOG_SEQUENCE_EXECUTION
            writer.WriteLine("Before executing sequence " + Id + ": " + Symbol);
#endif
            bool res = ApplyImpl(graph, env);
#if LOG_SEQUENCE_EXECUTION
            writer.WriteLine("After executing sequence " + Id + ": " + Symbol + " result " + res);
#endif
            executionState = res ? SequenceExecutionState.Success : SequenceExecutionState.Fail;
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
        /// Reset the execution state after a run (for following runs).
        /// The sequence to be iterated gets reset before each iteration.
        /// </summary>
        public void ResetExecutionState()
        {
            executionState = SequenceExecutionState.NotYet;
            foreach(Sequence childSeq in Children)
                childSeq.ResetExecutionState();
        }

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

        /// <summary>
        /// the state of executing this sequence
        /// </summary>
        public SequenceExecutionState ExecutionState { get { return executionState; } }

        /// <summary>
        /// the state of executing this sequence, implementation
        /// </summary>
        internal SequenceExecutionState executionState;

#if LOG_SEQUENCE_EXECUTION
        protected static StreamWriter writer = new StreamWriter("sequence_execution_log.txt");
#endif
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
    /// A Sequence with a random decision which might be interactively overriden by a user choice.
    /// </summary>
    public interface SequenceRandomChoice
    {
        /// <summary>
        /// The "Random" flag "$" telling whether the sequence operates in random mode.
        /// </summary>
        bool Random { get; set; }

        /// <summary>
        /// The "Choice" flag "%".
        /// Only applicable to a random decision sequence.
        /// GrShell uses this flag to indicate choicepoints when in debug mode.
        /// </summary>
        bool Choice { get; set; }
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
    /// Decision on order of execution by random, by user choice possible.
    /// </summary>
    public abstract class SequenceBinary : Sequence, SequenceRandomChoice
    {
        public Sequence Left;
        public Sequence Right;
        public bool Random { get { return random; } set { random = value; } }
        private bool random;
        public bool Choice { get { return choice; } set { choice = value; } }
        private bool choice;

        public SequenceBinary(Sequence left, Sequence right, bool random, bool choice, SequenceType seqType)
            : base(seqType)
        {
            Left = left;
            Right = right;
            this.random = random;
            this.choice = choice;
        }

        public override IEnumerable<Sequence> Children
        {
            get { yield return Left; yield return Right; }
        }
    }

    /// <summary>
    /// A sequence consisting of a list of subsequences.
    /// Decision on order of execution always by random, by user choice possible.
    /// </summary>
    public abstract class SequenceNAry : Sequence, SequenceRandomChoice
    {
        public List<Sequence> Sequences;
        public virtual bool Random { get { return true; } set { throw new Exception("can't change Random on SequenceNAry"); } }
        public bool Choice { get { return choice; } set { choice = value; } }
        bool choice;
        public bool Skip { get { return skip; } set { skip = value; } }
        bool skip;

        public SequenceNAry(List<Sequence> sequences, bool choice, SequenceType seqType)
            : base(seqType)
        {
            Sequences = sequences;
            this.choice = choice;
        }

        public override IEnumerable<Sequence> Children
        { 
            get { foreach(Sequence seq in Sequences) yield return seq; }
        }
    }

    public class SequenceThenLeft : SequenceBinary
    {
        public SequenceThenLeft(Sequence left, Sequence right, bool random, bool choice)
            : base(left, right, random, choice, SequenceType.ThenLeft)
        {
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            bool res;
            int direction = 0;
            if(Random) direction = randomGenerator.Next(2);
            if(Choice && env!=null) direction = env.ChooseDirection(direction, this);
            if(direction == 1) {
                Right.Apply(graph, env);
                res = Left.Apply(graph, env);
            } else {
                res = Left.Apply(graph, env);
                Right.Apply(graph, env);
            }
            return res;
        }

        public override int Precedence { get { return 0; } }
        public override string Symbol { get { string prefix = Random ? (Choice ? "$%" : "$") : ""; return prefix + "<;"; } }
    }

    public class SequenceThenRight : SequenceBinary
    {
        public SequenceThenRight(Sequence left, Sequence right, bool random, bool choice)
            : base(left, right, random, choice, SequenceType.ThenRight)
        {
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            bool res;
            int direction = 0;
            if(Random) direction = randomGenerator.Next(2);
            if(Choice && env!=null) direction = env.ChooseDirection(direction, this);
            if(direction == 1)
            {
                res = Right.Apply(graph, env);
                Left.Apply(graph, env);
            } else {
                Left.Apply(graph, env);
                res = Right.Apply(graph, env);
            }
            return res;
        }

        public override int Precedence { get { return 0; } }
        public override string Symbol { get { string prefix = Random ? (Choice ? "$%" : "$") : ""; return prefix + ";>"; } }
    }

    public class SequenceLazyOr : SequenceBinary
    {
        public SequenceLazyOr(Sequence left, Sequence right, bool random, bool choice)
            : base(left, right, random, choice, SequenceType.LazyOr)
        {
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            int direction = 0;
            if(Random) direction = randomGenerator.Next(2);
            if(Choice && env!=null) direction = env.ChooseDirection(direction, this);
            if(direction == 1)
                return Right.Apply(graph, env) || Left.Apply(graph, env);
            else
                return Left.Apply(graph, env) || Right.Apply(graph, env);
        }

        public override int Precedence { get { return 1; } }
        public override string Symbol { get { string prefix = Random ? (Choice ? "$%" : "$") : ""; return prefix + "||"; } }
    }

    public class SequenceLazyAnd : SequenceBinary
    {
        public SequenceLazyAnd(Sequence left, Sequence right, bool random, bool choice)
            : base(left, right, random, choice, SequenceType.LazyAnd)
        {
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            int direction = 0;
            if(Random) direction = randomGenerator.Next(2);
            if(Choice && env!=null) direction = env.ChooseDirection(direction, this);
            if(direction == 1)
                return Right.Apply(graph, env) && Left.Apply(graph, env);
            else
                return Left.Apply(graph, env) && Right.Apply(graph, env);
        }

        public override int Precedence { get { return 2; } }
        public override string Symbol { get { string prefix = Random ? (Choice ? "$%" : "$") : ""; return prefix + "&&"; } }
    }

    public class SequenceStrictOr : SequenceBinary
    {
        public SequenceStrictOr(Sequence left, Sequence right, bool random, bool choice)
            : base(left, right, random, choice, SequenceType.StrictOr)
        {
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            int direction = 0;
            if(Random) direction = randomGenerator.Next(2);
            if(Choice && env!=null) direction = env.ChooseDirection(direction, this);
            if(direction == 1)
                return Right.Apply(graph, env) | Left.Apply(graph, env);
            else
                return Left.Apply(graph, env) | Right.Apply(graph, env);
        }

        public override int Precedence { get { return 3; } }
        public override string Symbol { get { string prefix = Random ? (Choice ? "$%" : "$") : ""; return prefix + "|"; } }
    }

    public class SequenceXor : SequenceBinary
    {
        public SequenceXor(Sequence left, Sequence right, bool random, bool choice)
            : base(left, right, random, choice, SequenceType.Xor)
        {
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            int direction = 0;
            if(Random) direction = randomGenerator.Next(2);
            if(Choice && env!=null) direction = env.ChooseDirection(direction, this);
            if(direction == 1)
                return Right.Apply(graph, env) ^ Left.Apply(graph, env);
            else
                return Left.Apply(graph, env) ^ Right.Apply(graph, env);
        }

        public override int Precedence { get { return 4; } }
        public override string Symbol { get { string prefix = Random ? (Choice ? "$%" : "$") : ""; return prefix + "^"; } }
    }

    public class SequenceStrictAnd : SequenceBinary
    {
        public SequenceStrictAnd(Sequence left, Sequence right, bool random, bool choice)
            : base(left, right, random, choice, SequenceType.StrictAnd)
        {
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            int direction = 0;
            if(Random) direction = randomGenerator.Next(2);
            if(Choice && env!=null) direction = env.ChooseDirection(direction, this);
            if(direction == 1)
                return Right.Apply(graph, env) & Left.Apply(graph, env);
            else
                return Left.Apply(graph, env) & Right.Apply(graph, env);
        }

        public override int Precedence { get { return 5; } }
        public override string Symbol { get { string prefix = Random ? (Choice ? "$%" : "$") : ""; return prefix + "&"; } }
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
            while(Seq.Apply(graph, env))
            {
                if(env!=null) env.EndOfIteration(true, this);
                Seq.ResetExecutionState();
                i++;
            }
            if(env!=null) env.EndOfIteration(false, this);
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
            bool first = true;
            for(i = 0; i < Max; i++)
            {
                if(env!=null && !first) env.EndOfIteration(true, this);
                Seq.ResetExecutionState();
                if(!Seq.Apply(graph, env)) break;
                first = false;
            }
            if(env!=null) env.EndOfIteration(false, this);
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
            bool res;
            try
            {
                res = graph.ApplyRewrite(ParamBindings, 0, 1, Special, Test) > 0;
            }
            catch (NullReferenceException)
            {
                System.Console.Error.WriteLine("Null reference exception during rule execution (null parameter?): " + Symbol); 
                throw;
            }

#if LOG_SEQUENCE_EXECUTION
            if(res)
            {
                writer.WriteLine("Matched/Applied " + Symbol);
                writer.Flush();
            }
#endif
            return res;
        }

        public virtual bool Rewrite(IGraph graph, IMatches matches, SequenceExecutionEnvironment env, IMatch chosenMatch)
        {
            if(matches.Count == 0) return false;
            if(Test) return false;

            IMatch match = chosenMatch!=null ? chosenMatch : matches.First;

            graph.Finishing(matches, Special);

            if(graph.PerformanceInfo != null) graph.PerformanceInfo.StartLocal();

            object[] retElems = null;
            retElems = matches.Producer.Modify(graph, match);
            if(graph.PerformanceInfo != null) graph.PerformanceInfo.RewritesPerformed++;

            if(retElems == null) retElems = BaseGraph.NoElems;

            for(int i = 0; i < ParamBindings.ReturnVars.Length; i++)
                if(ParamBindings.ReturnVars[i]!=null)
                    ParamBindings.ReturnVars[i].SetVariableValue(retElems[i], graph);

            if(graph.PerformanceInfo != null) graph.PerformanceInfo.StopRewrite(); // total rewrite time does NOT include listeners anymore

            graph.Finished(matches, Special);

#if LOG_SEQUENCE_EXECUTION
                writer.WriteLine("Matched/Applied " + Symbol);
                writer.Flush();
#endif

            return true;
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
                    if(ParamBindings.ParamVars[i] != null)
                        sb.Append(ParamBindings.ParamVars[i].Name);
                    else
                        sb.Append(ParamBindings.Parameters[i]!=null ? ParamBindings.Parameters[i] : "null");
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

    public class SequenceRuleAll : SequenceRule, SequenceRandomChoice
    {
        public bool ChooseRandom;
        public bool MinSpecified;
        public SequenceVariable MinVarChooseRandom;
        public SequenceVariable MaxVarChooseRandom;
        private bool choice;

        public SequenceRuleAll(RuleInvocationParameterBindings paramBindings, bool special, bool test, 
            bool chooseRandom, SequenceVariable varChooseRandom,
            bool chooseRandom2, SequenceVariable varChooseRandom2, bool choice)
            : base(paramBindings, special, test)
        {
            SequenceType = SequenceType.RuleAll;
            ChooseRandom = chooseRandom;
            if(chooseRandom)
            {
                MinSpecified = chooseRandom2;
                if(chooseRandom2)
                {
                    MinVarChooseRandom = varChooseRandom;
                    MaxVarChooseRandom = varChooseRandom2;
                }
                else
                    MaxVarChooseRandom = varChooseRandom;
            }
            this.choice = choice;
        }

        public bool Random { get { return ChooseRandom; } set { ChooseRandom = value; } }
        public bool Choice { get { return choice; } set { choice = value; } }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            if(!ChooseRandom)
            {
                bool res;
                try
                {
                    res = graph.ApplyRewrite(ParamBindings, -1, -1, Special, Test) > 0;
                }
                catch (NullReferenceException)
                {
                    System.Console.Error.WriteLine("Null reference exception during rule execution (null parameter?): " + Symbol);
                    throw;
                }
#if LOG_SEQUENCE_EXECUTION
                if(res)
                {
                    writer.WriteLine("Matched/Applied " + Symbol);
                    writer.Flush();
                }
#endif
                return res;
            }
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
                IMatches matches;
                try
                {
                    matches = ParamBindings.Action.Match(graph, curMaxMatches, parameters);
                }
                catch (NullReferenceException)
                {
                    System.Console.Error.WriteLine("Null reference exception during rule execution (null parameter?): " + Symbol);
                    throw;
                }
                if(graph.PerformanceInfo != null)
                {
                    graph.PerformanceInfo.StopMatch();              // total match time does NOT include listeners anymore
                    graph.PerformanceInfo.MatchesFound += matches.Count;
                }

                graph.Matched(matches, Special);

                return Rewrite(graph, matches, env, null);
            }
        }

        public override bool Rewrite(IGraph graph, IMatches matches, SequenceExecutionEnvironment env, IMatch chosenMatch)
        {
            if (matches.Count == 0) return false;
            if (Test) return false;

            if (MinSpecified)
            {
                if (!(MinVarChooseRandom.GetVariableValue(graph) is int))
                    throw new InvalidOperationException("The variable '" + MinVarChooseRandom + "' is not of type int!");
                if(matches.Count < (int)MinVarChooseRandom.GetVariableValue(graph))
                    return false;
            }

            graph.Finishing(matches, Special);

            if (graph.PerformanceInfo != null) graph.PerformanceInfo.StartLocal();

            object[] retElems = null;
            if (!ChooseRandom)
            {
                if (chosenMatch!=null)
                    throw new InvalidOperationException("Chosen match given although all matches should get rewritten");
                IEnumerator<IMatch> matchesEnum = matches.GetEnumerator();
                while (matchesEnum.MoveNext())
                {
                    IMatch match = matchesEnum.Current;
                    if (match != matches.First) graph.RewritingNextMatch();
                    retElems = matches.Producer.Modify(graph, match);
                    if (graph.PerformanceInfo != null) graph.PerformanceInfo.RewritesPerformed++;
                }
                if (retElems == null) retElems = BaseGraph.NoElems;
            }
            else
            {
                object val = MaxVarChooseRandom != null ? MaxVarChooseRandom.GetVariableValue(graph) : (MinSpecified ? 2147483647 : 1);
                if (!(val is int))
                    throw new InvalidOperationException("The variable '" + MaxVarChooseRandom.Name + "' is not of type int!");
                int numChooseRandom = (int)val;
                if (matches.Count < numChooseRandom) numChooseRandom = matches.Count;

                for (int i = 0; i < numChooseRandom; i++)
                {
                    if (i != 0) graph.RewritingNextMatch();
                    int matchToApply = randomGenerator.Next(matches.Count);
                    if (Choice && env != null) matchToApply = env.ChooseMatch(matchToApply, matches, numChooseRandom - 1 - i, this);
                    IMatch match = matches.RemoveMatch(matchToApply);
                    if (chosenMatch != null) match = chosenMatch;
                    retElems = matches.Producer.Modify(graph, match);
                    if (graph.PerformanceInfo != null) graph.PerformanceInfo.RewritesPerformed++;
                }
                if (retElems == null) retElems = BaseGraph.NoElems;
            }

            for(int i = 0; i < ParamBindings.ReturnVars.Length; i++)
                ParamBindings.ReturnVars[i].SetVariableValue(retElems[i], graph);
            if(graph.PerformanceInfo != null) graph.PerformanceInfo.StopRewrite();            // total rewrite time does NOT include listeners anymore

            graph.Finished(matches, Special);

#if LOG_SEQUENCE_EXECUTION
                writer.WriteLine("Matched/Applied " + Symbol);
                writer.Flush();
#endif

            return true;
        }

        public override string Symbol
        { 
            get 
            {
                String prefix = "";
				if(ChooseRandom) {
					prefix = "$";
                    if(Choice) prefix += "%";
                    if(MinSpecified)
                    {
                        prefix += MinVarChooseRandom.Name + ",";
                        if(MaxVarChooseRandom != null)
                            prefix += MaxVarChooseRandom.Name;
                        else
                            prefix += "*";
                    }
                    else
                    {
                        if(MaxVarChooseRandom != null)
                            prefix += MaxVarChooseRandom.Name;
                    }
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

    public class SequenceYield : Sequence
    {
        public SequenceVariable[] YieldVars;
        public String[] ExpectedYieldType;

        public SequenceYield(SequenceVariable[] yieldVars)
            : base(SequenceType.Yield)
        {
            YieldVars = yieldVars;
        }

        public void SetExpectedYieldType(String[] expectedYieldType)
        {
            ExpectedYieldType = expectedYieldType;
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            throw new Exception("yield is only available in the compiled sequences (exec)");
        }

        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("yield(");
                for(int i = 0; i < YieldVars.Length; ++i)
                {
                    sb.Append(YieldVars[i].Name);
                    if(i != YieldVars.Length - 1) sb.Append(",");
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

    public class SequenceAssignUserInputToVar : Sequence, SequenceRandomChoice
    {
        public SequenceVariable DestVar;
        public String Type;

        public bool Random { get { return false; } set { throw new Exception("can't change Random on SequenceAssignUserInputToVar"); } }
        public bool Choice { get { return true; } set { throw new Exception("can't change Choice on SequenceAssignUserInputToVar"); } }

        public SequenceAssignUserInputToVar(SequenceVariable destVar, String type)
            : base(SequenceType.AssignUserInputToVar)
        {
            DestVar = destVar;
            Type = type;
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            if (env == null)
                throw new Exception("Can only query the user for a value if a debugger is available");
            DestVar.SetVariableValue(env.ChooseValue(Type, this), graph);
            return true;
        }

        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return DestVar.Name + "=" + "$%(" + Type + ")"; } }
    }

    public class SequenceAssignRandomToVar : Sequence, SequenceRandomChoice
    {
        public SequenceVariable DestVar;
        public int Number;

        public bool Random { get { return true; } set { throw new Exception("can't change Random on SequenceAssignRandomToVar"); } }
        public bool Choice { get { return choice; } set { choice = value; } }
        private bool choice;

        public SequenceAssignRandomToVar(SequenceVariable destVar, int number, bool choice)
            : base(SequenceType.AssignRandomToVar)
        {
            DestVar = destVar;
            Number = number;
            this.choice = choice;
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            int randomNumber = randomGenerator.Next(Number);
            if(Choice && env!=null) randomNumber = env.ChooseRandomNumber(randomNumber, Number, this);
            DestVar.SetVariableValue(randomNumber, graph);
            return true;
        }

        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return DestVar.Name + "=" + (Choice ? "$%" : "$") + "(" + Number + ")"; } }
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
            if(Constant==null)
                return DestVar.Name + "=null";
            else if(Constant.GetType().Name == "Dictionary`2")
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

    public class SequenceLazyOrAll : SequenceNAry
    {
        public SequenceLazyOrAll(List<Sequence> sequences, bool choice)
            : base(sequences, choice, SequenceType.LazyOrAll)
        {
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            List<Sequence> sequences = new List<Sequence>(Sequences);
            while(sequences.Count != 0)
            {
                int seqToExecute = randomGenerator.Next(sequences.Count);
                if(Choice && !Skip && env != null) seqToExecute = env.ChooseSequence(seqToExecute, sequences, this);
                bool result = sequences[seqToExecute].Apply(graph, env);
                sequences.Remove(sequences[seqToExecute]);
                if(result) {
                    Skip = false;
                    return true;
                }
            }
            Skip = false;
            return false;
        }

        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "||"; } }
    }

    public class SequenceLazyAndAll : SequenceNAry
    {
        public SequenceLazyAndAll(List<Sequence> sequences, bool choice)
            : base(sequences, choice, SequenceType.LazyAndAll)
        {
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            List<Sequence> sequences = new List<Sequence>(Sequences);
            while(sequences.Count != 0)
            {
                int seqToExecute = randomGenerator.Next(sequences.Count);
                if(Choice && !Skip && env != null) seqToExecute = env.ChooseSequence(seqToExecute, sequences, this);
                bool result = sequences[seqToExecute].Apply(graph, env);
                sequences.Remove(sequences[seqToExecute]);
                if(!result) { 
                    Skip = false; 
                    return false; 
                }
            }
            Skip = false;
            return true;
        }

        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "&&"; } }
    }

    public class SequenceStrictOrAll : SequenceNAry
    {
        public SequenceStrictOrAll(List<Sequence> sequences, bool choice)
            : base(sequences, choice, SequenceType.StrictOrAll)
        {
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            bool result = false;
            List<Sequence> sequences = new List<Sequence>(Sequences);
            while(sequences.Count != 0)
            {
                int seqToExecute = randomGenerator.Next(sequences.Count);
                if(Choice && !Skip && env != null) seqToExecute = env.ChooseSequence(seqToExecute, sequences, this);
                result |= sequences[seqToExecute].Apply(graph, env);
                sequences.Remove(sequences[seqToExecute]);
            }
            Skip = false;
            return result;
        }

        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "|"; } }
    }

    public class SequenceStrictAndAll : SequenceNAry
    {
        public SequenceStrictAndAll(List<Sequence> sequences, bool choice)
            : base(sequences, choice, SequenceType.StrictAndAll)
        {
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            bool result = true;
            List<Sequence> sequences = new List<Sequence>(Sequences);
            while(sequences.Count != 0)
            {
                int seqToExecute = randomGenerator.Next(sequences.Count);
                if(Choice && !Skip && env != null) seqToExecute = env.ChooseSequence(seqToExecute, sequences, this);
                result &= sequences[seqToExecute].Apply(graph, env);
                sequences.Remove(sequences[seqToExecute]);
            }
            Skip = false;
            return result;
        }

        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "&"; } }
    }

    /// <summary>
    /// A sequence consisting of a list of subsequences.
    /// Decision on order of execution by random, by user choice possible.
    /// First all the contained rules are matched, then they get rewritten
    /// </summary>
    public class SequenceSomeFromSet : SequenceNAry
    {
        public List<IMatches> Matches;
        public override bool Random { get { return chooseRandom; } set { chooseRandom = value; } }
        bool chooseRandom;

        public SequenceSomeFromSet(List<Sequence> sequences, bool chooseRandom, bool choice)
            : base(sequences, choice, SequenceType.SomeFromSet)
        {
            this.chooseRandom = chooseRandom;
            Matches = new List<IMatches>(Sequences.Count);
            for (int i = 0; i < Sequences.Count; ++i)
            {
                if (Sequences[i] is SequenceRuleAll)
                {
                    SequenceRuleAll ruleAll = (SequenceRuleAll)Sequences[i];
                    if (ruleAll.Choice)
                    {
                        Console.WriteLine("Warning: No user choice % available inside {...}, removing choice modificator from " + ruleAll.Symbol + " (user choice handled by $%{...} construct)");
                        ruleAll.Choice = false;
                    }
                }
                Matches.Add(null);
            }
        }

        public bool NonRandomAll(int rule) 
        {
            return Sequences[rule] is SequenceRuleAll && !((SequenceRuleAll)Sequences[rule]).ChooseRandom;
        }

        public int NumTotalMatches { get {
            int numTotalMatches = 0;
            for (int i = 0; i < Sequences.Count; ++i)
            {
                if (NonRandomAll(i))
                    ++numTotalMatches;
                else
                    numTotalMatches += Matches[i].Count;
            }
            return numTotalMatches;
        } }
        
        public void FromTotalMatch(int totalMatch, out int rule, out int match)
        {
            int curMatch = 0;
            for (int i = 0; i < Sequences.Count; ++i)
            {
                rule = i;
                if (NonRandomAll(i))
                {
                    match = 0;
                    if (curMatch == totalMatch) 
                        return;
                    ++curMatch;
                }
                else
                {
                    for (int j = 0; j < Matches[i].Count; ++j)
                    {
                        match = j;
                        if (curMatch == totalMatch)
                            return;
                        ++curMatch;
                    }
                }
            }
            throw new Exception("Internal error: can't computer rule and match from total match");
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            MatchAll(graph);

            if (NumTotalMatches == 0)
            {
                for (int i = 0; i < Sequences.Count; ++i)
                   Sequences[i].executionState = SequenceExecutionState.Fail;
                return false;
            }

            if (chooseRandom)
            {
                int totalMatchToExecute = randomGenerator.Next(NumTotalMatches);
                if (Choice && env != null) totalMatchToExecute = env.ChooseMatch(totalMatchToExecute, this);
                int ruleToExecute; int matchToExecute;
                FromTotalMatch(totalMatchToExecute, out ruleToExecute, out matchToExecute);
                SequenceRule rule = (SequenceRule)Sequences[ruleToExecute];
                IMatch match = Matches[ruleToExecute].GetMatch(matchToExecute);
                if (!(rule is SequenceRuleAll))
                    ApplyRule(rule, graph, Matches[ruleToExecute], null);
                else if (!((SequenceRuleAll)rule).ChooseRandom)
                    ApplyRule(rule, graph, Matches[ruleToExecute], null);
                else
                    ApplyRule(rule, graph, Matches[ruleToExecute], Matches[ruleToExecute].GetMatch(matchToExecute));
                for (int i = 0; i < Sequences.Count; ++i)
                    Sequences[i].executionState = Matches[i].Count == 0 ? SequenceExecutionState.Fail : Sequences[i].executionState;
                Sequences[ruleToExecute].executionState = SequenceExecutionState.Success; // ApplyRule removed the match from the matches
            }
            else
            {
                for (int i=0; i<Sequences.Count; ++i)
                {
                    if (Matches[i].Count > 0)
                    {
                        SequenceRule rule = (SequenceRule)Sequences[i];
                        ApplyRule(rule, graph, Matches[i], null);
                    }
                    else
                        Sequences[i].executionState = SequenceExecutionState.Fail;
                }
            }

            return true;
        }

        protected void MatchAll(IGraph graph)
        {
            for (int i = 0; i < Sequences.Count; ++i)
            {
                if (!(Sequences[i] is SequenceRule))
                    throw new InvalidOperationException("Internal error: some from set containing non-rule sequences");
                SequenceRule rule = (SequenceRule)Sequences[i];
                SequenceRuleAll ruleAll = null;
                int maxMatches = 1;
                if (rule is SequenceRuleAll)
                {
                    ruleAll = (SequenceRuleAll)rule;
                    maxMatches = graph.MaxMatches;
                }

                object[] parameters;
                if (rule.ParamBindings.ParamVars.Length > 0)
                {
                    parameters = rule.ParamBindings.Parameters;
                    for (int j = 0; j < rule.ParamBindings.ParamVars.Length; j++)
                    {
                        // If this parameter is not constant, the according ParamVars entry holds the
                        // name of a variable to be used for the parameter.
                        // Otherwise the parameters entry remains unchanged (it already contains the constant)
                        if (rule.ParamBindings.ParamVars[j] != null)
                            parameters[j] = rule.ParamBindings.ParamVars[j].GetVariableValue(graph);
                    }
                }
                else parameters = null;

                if (graph.PerformanceInfo != null) graph.PerformanceInfo.StartLocal();
                IMatches matches = rule.ParamBindings.Action.Match(graph, maxMatches, parameters);
                if (graph.PerformanceInfo != null)
                {
                    graph.PerformanceInfo.StopMatch();              // total match time does NOT include listeners anymore
                    graph.PerformanceInfo.MatchesFound += matches.Count;
                }

                Matches[i] = matches;
            }
        }

        protected bool ApplyRule(SequenceRule rule, IGraph graph, IMatches matches, IMatch match)
        {
            bool result;
            graph.EnteringSequence(rule);
            rule.executionState = SequenceExecutionState.Underway;
#if LOG_SEQUENCE_EXECUTION
            writer.WriteLine("Before executing sequence " + rule.Id + ": " + rule.Symbol);
#endif
            graph.Matched(matches, rule.Special);
            result = rule.Rewrite(graph, matches, null, match);
#if LOG_SEQUENCE_EXECUTION
            writer.WriteLine("After executing sequence " + rule.Id + ": " + rule.Symbol + " result " + result);
#endif
            rule.executionState = result ? SequenceExecutionState.Success : SequenceExecutionState.Fail;
            graph.ExitingSequence(rule);
            return result;
        }

        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "{ ... }"; } }
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

    public class SequenceBacktrack : Sequence
    {
        public SequenceRule Rule;
        public Sequence Seq;

        public SequenceBacktrack(Sequence seqRule, Sequence seq) : base(SequenceType.Backtrack)
        {
            Rule = (SequenceRule)seqRule;
            Seq = seq;
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            // first get all matches of the rule
            object[] parameters;
            if(Rule.ParamBindings.ParamVars.Length > 0)
            {
                parameters = Rule.ParamBindings.Parameters;
                for(int j = 0; j < Rule.ParamBindings.ParamVars.Length; j++)
                {
                    // If this parameter is not constant, the according ParamVars entry holds the
                    // name of a variable to be used for the parameter.
                    // Otherwise the parameters entry remains unchanged (it already contains the constant)
                    if(Rule.ParamBindings.ParamVars[j] != null)
                        parameters[j] = Rule.ParamBindings.ParamVars[j].GetVariableValue(graph);
                }
            }
            else parameters = null;

            if(graph.PerformanceInfo != null) graph.PerformanceInfo.StartLocal();
            IMatches matches = Rule.ParamBindings.Action.Match(graph, graph.MaxMatches, parameters);
            if(graph.PerformanceInfo != null)
            {
                graph.PerformanceInfo.StopMatch();              // total match time does NOT include listeners anymore
                graph.PerformanceInfo.MatchesFound += matches.Count;
            }

            if(matches.Count == 0)
            {
                Rule.executionState = SequenceExecutionState.Fail;
                return false;
            }

            // apply the rule and the following sequence for every match found, 
            // until the first rule and sequence execution succeeded
            // rolling back the changes of failing executions until then
            int matchesTried = 0;
            foreach(IMatch match in matches)
            {
                ++matchesTried;

                // start a transaction
                int transactionID = graph.TransactionManager.StartTransaction();
                int oldRewritesPerformed = -1;

                if(graph.PerformanceInfo != null) oldRewritesPerformed = graph.PerformanceInfo.RewritesPerformed;

                graph.EnteringSequence(Rule);
                Rule.executionState = SequenceExecutionState.Underway;
#if LOG_SEQUENCE_EXECUTION
                writer.WriteLine("Before executing sequence " + Rule.Id + ": " + rule.Symbol);
#endif
                graph.Matched(matches, Rule.Special);
                bool result = Rule.Rewrite(graph, matches, null, match);
#if LOG_SEQUENCE_EXECUTION
                writer.WriteLine("After executing sequence " + Rule.Id + ": " + rule.Symbol + " result " + result);
#endif
                Rule.executionState = result ? SequenceExecutionState.Success : SequenceExecutionState.Fail;
                graph.ExitingSequence(Rule);

                // rule applied, now execute the sequence
                result = Seq.Apply(graph, env);

                // if sequence execution failed, roll the changes back and try the next match of the rule
                if(!result)
                {
                    graph.TransactionManager.Rollback(transactionID);
                    if(graph.PerformanceInfo != null)
                        graph.PerformanceInfo.RewritesPerformed = oldRewritesPerformed;
                    if(matchesTried < matches.Count)
                    {
                        if(env != null) env.EndOfIteration(true, this);
                        Rule.ResetExecutionState();
                        Seq.ResetExecutionState();
                        continue;
                    }
                    else
                    {
                        // all matches tried, all failed later on -> end in fail
                        if(env != null) env.EndOfIteration(false, this);
                        return false;
                    }
                }

                // if sequence execution succeeded, commit the changes so far and succeed
                graph.TransactionManager.Commit(transactionID);
                if(env != null) env.EndOfIteration(false, this);
                return true;
            }

            return false; // to satisfy the compiler, we return from inside the loop
        }

        public override IEnumerable<Sequence> Children
        {
            get { yield return Rule; yield return Seq; }
        }

        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "<< " + Rule.Symbol + " ; ... >>"; } }
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
            : base(condition, trueCase, false, false, SequenceType.IfThen)
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
            bool first = true;
            foreach(DictionaryEntry entry in setmap)
            {
                if(env!=null && !first) env.EndOfIteration(true, this);
                Var.SetVariableValue(entry.Key, graph);
                if(VarDst != null)
                    VarDst.SetVariableValue(entry.Value, graph);
                Seq.ResetExecutionState();
                res &= Seq.Apply(graph, env);
                first = false;
            }
            if(env!=null) env.EndOfIteration(false, this);
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
            Text = Text.Replace("\\#", "#");
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

    public class SequenceRecord : Sequence
    {
        public String Text;
        public SequenceVariable Variable;

        public SequenceRecord(String text)
            : base(SequenceType.Record)
        {
            Text = text;
            Text = Text.Replace("\\n", "\n");
            Text = Text.Replace("\\r", "\r");
            Text = Text.Replace("\\t", "\t");
            Text = Text.Replace("\\#", "#");
        }

        public SequenceRecord(SequenceVariable var)
            : base(SequenceType.Record)
        {
            Variable = var;
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            if(Variable != null)
            {
                object val = Variable.GetVariableValue(graph);
                if(val != null)
                {
                    if(val is IDictionary) graph.Recorder.Write(DictionaryHelper.ToString((IDictionary)val));
                    else graph.Recorder.Write(val.ToString());
                }
            }
            else
            {
                graph.Recorder.Write(Text);
            }
            return true;
        }

        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return Variable != null ? "record(" + Variable.Name + ")" : "record(" + Text + ")"; } }
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

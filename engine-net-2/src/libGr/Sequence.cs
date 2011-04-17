/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
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
        RuleCall, RuleAllCall, Def, Yield, True, False, VarPredicate,
        AssignVAllocToVar, AssignContainerSizeToVar, AssignContainerEmptyToVar, 
        AssignContainerAccessToVar, AssignVarToIndexedVar,
        AssignVarToVar, AssignElemToVar,
        AssignSequenceResultToVar, OrAssignSequenceResultToVar, AndAssignSequenceResultToVar,
        AssignUserInputToVar, AssignRandomToVar,
        AssignConstToVar, AssignAttributeToVar, AssignVarToAttribute,
        IsVisited, SetVisited, VFree, VReset, Emit, Record,
        ContainerAdd, ContainerRem, ContainerClear, InContainer,
        LazyOrAll, LazyAndAll, StrictOrAll, StrictAndAll, SomeFromSet,
        Transaction, Backtrack, IfThenElse, IfThen, For,
        SequenceDefinitionInterpreted, SequenceDefinitionCompiled, SequenceCall
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
        /// Copies the sequence deeply so that
        /// - the execution state of the copy is NotYet
        /// - the global Variables are kept
        /// - the local Variables are replaced by copies initialized to null
        /// Used for cloning defined sequenced before executing them if needed.
        /// Needed if the defined sequence is currently executed to prevent state corruption.
        /// </summary>
        /// <param name="originalToCopy">A map used to ensure that every instance of a variable is mapped to the same copy</param>
        /// <returns>The copy of the sequence</returns>
        internal abstract Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy);

        /// <summary>
        /// After a sequence definition was replaced by a new one, all references from then on will use the new one,
        /// but the old references are still there and must get replaced.
        /// </summary>
        /// <param name="oldDef">The old definition which is to be replaced</param>
        /// <param name="newDef">The new definition which replaces the old one</param>
        internal virtual void ReplaceSequenceDefinition(SequenceDefinition oldDef, SequenceDefinition newDef)
        {
            // most sequences are basic ones not referencing seqences 
            // this null implementation saves us the effort of implementing this method everywhere, needed or not
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
        /// Returns the innermost sequence beneath this as root
        /// which gets currently executed (for sequences on call stack this is the call).
        /// A path in the sequence tree gets executed, the innermost is the important one.
        /// </summary>
        /// <returns>The innermost sequence currently executed, or null if there is no such</returns>
        public virtual Sequence GetCurrentlyExecutedSequence()
        {
            if(executionState == SequenceExecutionState.Underway)
                return this;
            return null;
        }

        /// <summary>
        /// Walks the sequence tree from this on to the given target sequence (inclusive),
        /// collecting all variables found on the way into the variables dictionary.
        /// </summary>
        /// <param name="variables">Contains the variables found</param>
        /// <param name="target">The target sequence up to which to walk</param>
        /// <returns>Returns whether the target was hit, so the parent can abort walking</returns>
        public virtual bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables, Sequence target)
        {
            return this == target;
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
        /// for copies the old id is just taken over, does not cause problems as code is only generated once per defined sequence
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

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceUnary copy = (SequenceUnary)MemberwiseClone();
            copy.Seq = Seq.Copy(originalToCopy);
            copy.executionState = SequenceExecutionState.NotYet;
            return copy;
        }

        internal override void ReplaceSequenceDefinition(SequenceDefinition oldDef, SequenceDefinition newDef)
        {
            Seq.ReplaceSequenceDefinition(oldDef, newDef);
        }

        public override Sequence GetCurrentlyExecutedSequence()
        {
            if(Seq.GetCurrentlyExecutedSequence() != null)
                return Seq.GetCurrentlyExecutedSequence();
            if(executionState == SequenceExecutionState.Underway)
                return this;
            return null;
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables, Sequence target)
        {
            if(Seq.GetLocalVariables(variables, target))
                return true;
            return this == target;
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

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceBinary copy = (SequenceBinary)MemberwiseClone();
            copy.Left = Left.Copy(originalToCopy);
            copy.Right = Right.Copy(originalToCopy);
            copy.executionState = SequenceExecutionState.NotYet;
            return copy;
        }

        internal override void ReplaceSequenceDefinition(SequenceDefinition oldDef, SequenceDefinition newDef)
        {
            Left.ReplaceSequenceDefinition(oldDef, newDef);
            Right.ReplaceSequenceDefinition(oldDef, newDef);
        }

        public override Sequence GetCurrentlyExecutedSequence()
        {
            if(Left.GetCurrentlyExecutedSequence() != null)
                return Left.GetCurrentlyExecutedSequence();
            if(Right.GetCurrentlyExecutedSequence() != null)
                return Right.GetCurrentlyExecutedSequence();
            if(executionState == SequenceExecutionState.Underway)
                return this;
            return null;
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables, Sequence target)
        {
            if(Left.GetLocalVariables(variables, target))
                return true;
            if(Right.GetLocalVariables(variables, target))
                return true;
            return this == target;
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

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceNAry copy = (SequenceNAry)MemberwiseClone();
            copy.Sequences = new List<Sequence>();
            foreach(Sequence seq in Sequences)
                copy.Sequences.Add(seq.Copy(originalToCopy));
            copy.executionState = SequenceExecutionState.NotYet;
            return copy;
        }

        internal override void ReplaceSequenceDefinition(SequenceDefinition oldDef, SequenceDefinition newDef)
        {
            foreach(Sequence seq in Sequences)
                seq.ReplaceSequenceDefinition(oldDef, newDef);
        }

        public override Sequence GetCurrentlyExecutedSequence()
        {
            foreach(Sequence seq in Sequences)
                if(seq.GetCurrentlyExecutedSequence() != null)
                    return seq.GetCurrentlyExecutedSequence();
            if(executionState == SequenceExecutionState.Underway)
                return this;
            return null;
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables, Sequence target)
        {
            foreach(Sequence seq in Sequences)
                if(seq.GetLocalVariables(variables, target))
                    return true;
            return this == target;
        }

        public override IEnumerable<Sequence> Children
        { 
            get { foreach(Sequence seq in Sequences) yield return seq; }
        }
    }

    /// <summary>
    /// A sequence which assigns something to a destination variable.
    /// </summary>
    public abstract class SequenceAssign : Sequence
    {
        public SequenceVariable DestVar;

        public SequenceAssign(SequenceVariable destVar, SequenceType seqType)
            : base(seqType)
        {
            DestVar = destVar;
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceAssign copy = (SequenceAssign)MemberwiseClone();
            copy.DestVar = DestVar.Copy(originalToCopy);
            copy.executionState = SequenceExecutionState.NotYet;
            return copy;
        }

        protected bool Assign(object value, IGraph graph)
        {
            DestVar.SetVariableValue(value, graph);
            return true;
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables, Sequence target)
        {
            DestVar.GetLocalVariables(variables);
            return this == target;
        }

        public override IEnumerable<Sequence> Children { get { yield break; } } // nearly always no children
        public override int Precedence { get { return 8; } } // nearly always a top prio assignment factor
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

    public class SequenceRuleCall : SequenceSpecial
    {
        public RuleInvocationParameterBindings ParamBindings;

        public bool Test;

        public SequenceRuleCall(RuleInvocationParameterBindings paramBindings, bool special, bool test)
            : base(special, SequenceType.RuleCall)
        {
            ParamBindings = paramBindings;
            Test = test;
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceRuleCall copy = (SequenceRuleCall)MemberwiseClone();
            copy.ParamBindings = ParamBindings.Copy(originalToCopy);
            copy.executionState = SequenceExecutionState.NotYet;
            return copy;
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
                ParamBindings.ReturnVars[i].SetVariableValue(retElems[i], graph);

            if(graph.PerformanceInfo != null) graph.PerformanceInfo.StopRewrite(); // total rewrite time does NOT include listeners anymore

            graph.Finished(matches, Special);

#if LOG_SEQUENCE_EXECUTION
                writer.WriteLine("Matched/Applied " + Symbol);
                writer.Flush();
#endif

            return true;
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables, Sequence target)
        {
            ParamBindings.GetLocalVariables(variables);
            return this == target;
        }

        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }

        protected String GetRuleString()
        {
            StringBuilder sb = new StringBuilder();
            if(ParamBindings.ReturnVars.Length > 0)
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

    public class SequenceRuleAllCall : SequenceRuleCall, SequenceRandomChoice
    {
        public bool ChooseRandom;
        public bool MinSpecified;
        public SequenceVariable MinVarChooseRandom;
        public SequenceVariable MaxVarChooseRandom;
        private bool choice;

        public SequenceRuleAllCall(RuleInvocationParameterBindings paramBindings, bool special, bool test, 
            bool chooseRandom, SequenceVariable varChooseRandom,
            bool chooseRandom2, SequenceVariable varChooseRandom2, bool choice)
            : base(paramBindings, special, test)
        {
            SequenceType = SequenceType.RuleAllCall;
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

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceRuleAllCall copy = (SequenceRuleAllCall)MemberwiseClone();
            copy.ParamBindings = ParamBindings.Copy(originalToCopy);
            copy.MinVarChooseRandom = MinVarChooseRandom.Copy(originalToCopy);
            copy.MaxVarChooseRandom = MaxVarChooseRandom.Copy(originalToCopy);
            copy.executionState = SequenceExecutionState.NotYet;
            return copy;
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

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables, Sequence target)
        {
            ParamBindings.GetLocalVariables(variables);
            if(MinVarChooseRandom!=null) MinVarChooseRandom.GetLocalVariables(variables);
            if(MaxVarChooseRandom!=null) MaxVarChooseRandom.GetLocalVariables(variables);
            return this == target;
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

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceDef copy = (SequenceDef)MemberwiseClone();
            copy.DefVars = new SequenceVariable[DefVars.Length];
            for(int i=0; i<DefVars.Length; ++i)
                copy.DefVars[i] = DefVars[i].Copy(originalToCopy);
            copy.executionState = SequenceExecutionState.NotYet;
            return copy;
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

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables, Sequence target)
        {
            foreach(SequenceVariable seqVar in DefVars)
                seqVar.GetLocalVariables(variables);
            return this == target;
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
        public SequenceVariable ToVar;
        public SequenceVariable FromVar;

        public SequenceYield(SequenceVariable toVar, SequenceVariable fromVar)
            : base(SequenceType.Yield)
        {
            ToVar = toVar;
            FromVar = fromVar;
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceYield copy = (SequenceYield)MemberwiseClone();
            copy.ToVar = ToVar;
            copy.FromVar = FromVar;
            copy.executionState = SequenceExecutionState.NotYet;
            return copy;
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            throw new Exception("yield is only available in the compiled sequences (exec)");
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables, Sequence target)
        {
            ToVar.GetLocalVariables(variables);
            FromVar.GetLocalVariables(variables);
            return this == target;
        }

        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "yield " + ToVar.Name + "=" + FromVar.Name; } }
    }

    public class SequenceTrue : SequenceSpecial
    {
        public SequenceTrue(bool special)
            : base(special, SequenceType.True)
        {
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceTrue copy = (SequenceTrue)MemberwiseClone();
            copy.executionState = SequenceExecutionState.NotYet;
            return copy;
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

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceFalse copy = (SequenceFalse)MemberwiseClone();
            copy.executionState = SequenceExecutionState.NotYet;
            return copy;
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

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceVarPredicate copy = (SequenceVarPredicate)MemberwiseClone();
            copy.PredicateVar = PredicateVar.Copy(originalToCopy);
            copy.executionState = SequenceExecutionState.NotYet;
            return copy;
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            object val = PredicateVar.GetVariableValue(graph);
            if(val is bool) return (bool)val;
            throw new InvalidOperationException("The variable '" + PredicateVar + "' is not boolean!");
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables, Sequence target)
        {
            PredicateVar.GetLocalVariables(variables);
            return this == target;
        }

        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return Special ? "%"+PredicateVar.Name : PredicateVar.Name; } }
    }

    public class SequenceAssignVAllocToVar : SequenceAssign
    {
        public SequenceAssignVAllocToVar(SequenceVariable destVar)
            : base(destVar, SequenceType.AssignVAllocToVar)
        {
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            return Assign(graph.AllocateVisitedFlag(), graph);
        }

        public override string Symbol { get { return DestVar.Name + "=valloc()"; } }
    }

    public class SequenceAssignContainerSizeToVar : SequenceAssign
    {
        public SequenceVariable Container;

        public SequenceAssignContainerSizeToVar(SequenceVariable destVar, SequenceVariable container)
            : base(destVar, SequenceType.AssignContainerSizeToVar)
        {
            Container = container;
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceAssignContainerSizeToVar copy = (SequenceAssignContainerSizeToVar)MemberwiseClone();
            copy.Container = Container.Copy(originalToCopy);
            copy.DestVar = DestVar.Copy(originalToCopy);
            copy.executionState = SequenceExecutionState.NotYet;
            return copy;
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            if(Container.GetVariableValue(graph) is IList)
            {
                IList array = (IList)Container.GetVariableValue(graph);
                return Assign(array.Count, graph);
            }
            else
            {
                IDictionary setmap = (IDictionary)Container.GetVariableValue(graph);
                return Assign(setmap.Count, graph);
            }
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables, Sequence target)
        {
            DestVar.GetLocalVariables(variables);
            Container.GetLocalVariables(variables);
            return this == target;
        }

        public override string Symbol { get { return DestVar.Name + "=" + Container.Name + ".size()"; } }
    }

    public class SequenceAssignContainerEmptyToVar : SequenceAssign
    {
        public SequenceVariable Container;

        public SequenceAssignContainerEmptyToVar(SequenceVariable destVar, SequenceVariable container)
            : base(destVar, SequenceType.AssignContainerEmptyToVar)
        {
            Container = container;
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceAssignContainerEmptyToVar copy = (SequenceAssignContainerEmptyToVar)MemberwiseClone();
            copy.Container = Container.Copy(originalToCopy);
            copy.DestVar = DestVar.Copy(originalToCopy);
            copy.executionState = SequenceExecutionState.NotYet;
            return copy;
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            if(Container.GetVariableValue(graph) is IList)
            {
                IList array = (IList)Container.GetVariableValue(graph);
                return Assign(array.Count == 0, graph);
            }
            else
            {
                IDictionary setmap = (IDictionary)Container.GetVariableValue(graph);
                return Assign(setmap.Count == 0, graph);
            }
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables, Sequence target)
        {
            DestVar.GetLocalVariables(variables);
            Container.GetLocalVariables(variables);
            return this == target;
        }

        public override string Symbol { get { return DestVar.Name + "=" + Container.Name + ".empty()"; } }
    }

    public class SequenceAssignContainerAccessToVar : SequenceAssign
    {
        public SequenceVariable Container;
        public SequenceVariable KeyVar;

        public SequenceAssignContainerAccessToVar(SequenceVariable destVar, SequenceVariable container, SequenceVariable keyVar)
            : base(destVar, SequenceType.AssignContainerAccessToVar)
        {
            Container = container;
            KeyVar = keyVar;
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceAssignContainerAccessToVar copy = (SequenceAssignContainerAccessToVar)MemberwiseClone();
            copy.Container = Container.Copy(originalToCopy);
            copy.KeyVar = KeyVar.Copy(originalToCopy);
            copy.DestVar = DestVar.Copy(originalToCopy);
            copy.executionState = SequenceExecutionState.NotYet;
            return copy;
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            if(Container.GetVariableValue(graph) is IList)
            {
                IList array = (IList)Container.GetVariableValue(graph);
                int keyVar = (int)KeyVar.GetVariableValue(graph);
                if(keyVar >= array.Count) return false;
                return Assign(array[keyVar], graph);
            }
            else
            {
                IDictionary setmap = (IDictionary)Container.GetVariableValue(graph);
                object keyVar = KeyVar.GetVariableValue(graph);
                if(!setmap.Contains(keyVar)) return false;
                return Assign(setmap[keyVar], graph);
            }
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables, Sequence target)
        {
            DestVar.GetLocalVariables(variables);
            Container.GetLocalVariables(variables);
            KeyVar.GetLocalVariables(variables);
            return this == target;
        }

        public override string Symbol { get { return DestVar.Name + "=" + Container.Name + "[" + KeyVar.Name + "]"; } }
    }

    public class SequenceAssignVarToIndexedVar : SequenceAssign
    {
        public SequenceVariable KeyVar;
        public SequenceVariable Var;

        public SequenceAssignVarToIndexedVar(SequenceVariable destVar, SequenceVariable keyVar, SequenceVariable var)
            : base(destVar, SequenceType.AssignVarToIndexedVar)
        {
            KeyVar = keyVar;
            Var = var;
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceAssignVarToIndexedVar copy = (SequenceAssignVarToIndexedVar)MemberwiseClone();
            copy.Var = Var.Copy(originalToCopy);
            copy.KeyVar = KeyVar.Copy(originalToCopy);
            copy.DestVar = DestVar.Copy(originalToCopy);
            copy.executionState = SequenceExecutionState.NotYet;
            return copy;
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            if(DestVar.GetVariableValue(graph) is IList)
            {
                IList array = (IList)DestVar.GetVariableValue(graph);
                int keyVar = (int)KeyVar.GetVariableValue(graph);
                if(keyVar >= array.Count) return false;
                array[keyVar] = Var.GetVariableValue(graph);
            }
            else
            {
                IDictionary setmap = (IDictionary)DestVar.GetVariableValue(graph);
                object keyVar = KeyVar.GetVariableValue(graph);
                if(!setmap.Contains(keyVar)) return false;
                setmap[keyVar] = Var.GetVariableValue(graph);
            }
            return true;
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables, Sequence target)
        {
            DestVar.GetLocalVariables(variables);
            Var.GetLocalVariables(variables);
            KeyVar.GetLocalVariables(variables);
            return this == target;
        }

        public override string Symbol { get { return DestVar.Name + "[" + KeyVar.Name + "] = " + Var.Name; } }
    }

    public class SequenceAssignVarToVar : SequenceAssign
    {
        public SequenceVariable SourceVar;

        public SequenceAssignVarToVar(SequenceVariable destVar, SequenceVariable sourceVar)
            : base(destVar, SequenceType.AssignVarToVar)
        {
            SourceVar = sourceVar;
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceAssignVarToVar copy = (SequenceAssignVarToVar)MemberwiseClone();
            copy.SourceVar = SourceVar.Copy(originalToCopy);
            copy.DestVar = DestVar.Copy(originalToCopy);
            copy.executionState = SequenceExecutionState.NotYet;
            return copy;
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            return Assign(SourceVar.GetVariableValue(graph), graph);
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables, Sequence target)
        {
            DestVar.GetLocalVariables(variables);
            SourceVar.GetLocalVariables(variables);
            return this == target;
        }

        public override string Symbol { get { return DestVar.Name + "=" + SourceVar.Name; } }
    }

    public class SequenceAssignUserInputToVar : SequenceAssign, SequenceRandomChoice
    {
        public String Type;

        public bool Random { get { return false; } set { throw new Exception("can't change Random on SequenceAssignUserInputToVar"); } }
        public bool Choice { get { return true; } set { throw new Exception("can't change Choice on SequenceAssignUserInputToVar"); } }

        public SequenceAssignUserInputToVar(SequenceVariable destVar, String type)
            : base(destVar, SequenceType.AssignUserInputToVar)
        {
            Type = type;
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            if (env == null)
                throw new Exception("Can only query the user for a value if a debugger is available");
            return Assign(env.ChooseValue(Type, this), graph);
        }

        public override string Symbol { get { return DestVar.Name + "=" + "$%(" + Type + ")"; } }
    }

    public class SequenceAssignRandomToVar : SequenceAssign, SequenceRandomChoice
    {
        public int Number;

        public bool Random { get { return true; } set { throw new Exception("can't change Random on SequenceAssignRandomToVar"); } }
        public bool Choice { get { return choice; } set { choice = value; } }
        private bool choice;

        public SequenceAssignRandomToVar(SequenceVariable destVar, int number, bool choice)
            : base(destVar, SequenceType.AssignRandomToVar)
        {
            Number = number;
            this.choice = choice;
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            int randomNumber = randomGenerator.Next(Number);
            if(Choice && env!=null) randomNumber = env.ChooseRandomNumber(randomNumber, Number, this);
            return Assign(randomNumber, graph);
        }

        public override string Symbol { get { return DestVar.Name + "=" + (Choice ? "$%" : "$") + "(" + Number + ")"; } }
    }

    public class SequenceAssignConstToVar : SequenceAssign
    {
        public object Constant; 

        public SequenceAssignConstToVar(SequenceVariable destVar, object constant)
            : base(destVar, SequenceType.AssignConstToVar)
        {
            Constant = constant;
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            return Assign(Constant, graph);
        }

        public override string Symbol { get {
            if(Constant==null)
                return DestVar.Name + "=null";
            else if(Constant.GetType().Name == "Dictionary`2")
                return DestVar.Name + "={}"; // only empty set/map assignment possible as of now
            else if(Constant.GetType().Name == "List`1")
                return DestVar.Name + "=[]"; // only empty array assignment possible as of now
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

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceAssignVarToAttribute copy = (SequenceAssignVarToAttribute)MemberwiseClone();
            copy.DestVar = DestVar.Copy(originalToCopy);
            copy.SourceVar = SourceVar.Copy(originalToCopy);
            copy.executionState = SequenceExecutionState.NotYet;
            return copy;
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            object value = SourceVar.GetVariableValue(graph);
            IGraphElement elem = (IGraphElement)DestVar.GetVariableValue(graph);
            AttributeType attrType;
            value = DictionaryListHelper.IfAttributeOfElementIsDictionaryOrListThenCloneDictionaryOrListValue(
                elem, AttributeName, value, out attrType);
            AttributeChangeType changeType = AttributeChangeType.Assign;
            if(elem is INode)
                graph.ChangingNodeAttribute((INode)elem, attrType, changeType, value, null);
            else
                graph.ChangingEdgeAttribute((IEdge)elem, attrType, changeType, value, null);
            elem.SetAttribute(AttributeName, value);
            return true;
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables, Sequence target)
        {
            DestVar.GetLocalVariables(variables);
            SourceVar.GetLocalVariables(variables);
            return this == target;
        }

        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return DestVar.Name + "." + AttributeName + "=" + SourceVar.Name; } }
    }

    public class SequenceAssignAttributeToVar : SequenceAssign
    {
        public SequenceVariable SourceVar;
        public String AttributeName;

        public SequenceAssignAttributeToVar(SequenceVariable destVar, SequenceVariable sourceVar, String attributeName)
            : base(destVar, SequenceType.AssignAttributeToVar)
        {
            SourceVar = sourceVar;
            AttributeName = attributeName;
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceAssignAttributeToVar copy = (SequenceAssignAttributeToVar)MemberwiseClone();
            copy.SourceVar = SourceVar.Copy(originalToCopy);
            copy.DestVar = DestVar.Copy(originalToCopy);
            copy.executionState = SequenceExecutionState.NotYet;
            return copy;
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            IGraphElement elem = (IGraphElement)SourceVar.GetVariableValue(graph);
            object value = elem.GetAttribute(AttributeName);
            AttributeType attrType;
            value = DictionaryListHelper.IfAttributeOfElementIsDictionaryOrListThenCloneDictionaryOrListValue(
                elem, AttributeName, value, out attrType);
            return Assign(value, graph);
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables, Sequence target)
        {
            DestVar.GetLocalVariables(variables);
            SourceVar.GetLocalVariables(variables);
            return this == target;
        }

        public override string Symbol { get { return DestVar.Name + "=" + SourceVar.Name + "." + AttributeName; } }
    }

    public class SequenceAssignElemToVar : SequenceAssign
    {
        public String ElementName;

        public SequenceAssignElemToVar(SequenceVariable destVar, String elemName)
            : base(destVar, SequenceType.AssignElemToVar)
        {
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
            return Assign(elem, graph);
        }

        public override string Symbol { get { return DestVar.Name + "=@("+ElementName+")"; } }
    }

    public class SequenceAssignSequenceResultToVar : SequenceAssign
    {
        public Sequence Seq;

        public SequenceAssignSequenceResultToVar(SequenceVariable destVar, Sequence sequence)
            : base(destVar, SequenceType.AssignSequenceResultToVar)
        {
            Seq = sequence;
        }

        public SequenceAssignSequenceResultToVar(SequenceType seqType, SequenceVariable destVar, Sequence sequence)
            : base(destVar, seqType)
        {
            Seq = sequence;
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceAssignSequenceResultToVar copy = (SequenceAssignSequenceResultToVar)MemberwiseClone();
            copy.Seq = Seq.Copy(originalToCopy);
            copy.DestVar = DestVar.Copy(originalToCopy);
            copy.executionState = SequenceExecutionState.NotYet;
            return copy;
        }

        internal override void ReplaceSequenceDefinition(SequenceDefinition oldDef, SequenceDefinition newDef)
        {
            Seq.ReplaceSequenceDefinition(oldDef, newDef);
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            bool result = Seq.Apply(graph, env);
            return Assign(result, graph);
        }

        public override Sequence GetCurrentlyExecutedSequence()
        {
            if(Seq.GetCurrentlyExecutedSequence() != null)
                return Seq.GetCurrentlyExecutedSequence();
            if(executionState == SequenceExecutionState.Underway)
                return this;
            return null;
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables, Sequence target)
        {
            DestVar.GetLocalVariables(variables);
            if(Seq.GetLocalVariables(variables, target))
                return true;
            return this == target;
        }

        public override IEnumerable<Sequence> Children { get { yield return Seq; } }
        public override int Precedence { get { return 6; } }
        public override string Symbol { get { return "... => " + DestVar.Name; } }
    }

    public class SequenceOrAssignSequenceResultToVar : SequenceAssignSequenceResultToVar
    {
        public SequenceOrAssignSequenceResultToVar(SequenceVariable destVar, Sequence sequence)
            : base(SequenceType.OrAssignSequenceResultToVar, destVar, sequence)
        {
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            bool result = Seq.Apply(graph, env);
            return Assign(result || (bool)DestVar.GetVariableValue(graph), graph);
        }

        public override string Symbol { get { return "... |> " + DestVar.Name; } }
    }

    public class SequenceAndAssignSequenceResultToVar : SequenceAssignSequenceResultToVar
    {
        public SequenceAndAssignSequenceResultToVar(SequenceVariable destVar, Sequence sequence)
            : base(SequenceType.AndAssignSequenceResultToVar, destVar, sequence)
        {
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            bool result = Seq.Apply(graph, env);
            return Assign(result && (bool)DestVar.GetVariableValue(graph), graph);
        }

        public override string Symbol { get { return "... &> " + DestVar.Name; } }
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
                if (Sequences[i] is SequenceRuleAllCall)
                {
                    SequenceRuleAllCall ruleAll = (SequenceRuleAllCall)Sequences[i];
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
            return Sequences[rule] is SequenceRuleAllCall && !((SequenceRuleAllCall)Sequences[rule]).ChooseRandom;
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
                SequenceRuleCall rule = (SequenceRuleCall)Sequences[ruleToExecute];
                IMatch match = Matches[ruleToExecute].GetMatch(matchToExecute);
                if (!(rule is SequenceRuleAllCall))
                    ApplyRule(rule, graph, Matches[ruleToExecute], null);
                else if (!((SequenceRuleAllCall)rule).ChooseRandom)
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
                        SequenceRuleCall rule = (SequenceRuleCall)Sequences[i];
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
                if (!(Sequences[i] is SequenceRuleCall))
                    throw new InvalidOperationException("Internal error: some from set containing non-rule sequences");
                SequenceRuleCall rule = (SequenceRuleCall)Sequences[i];
                SequenceRuleAllCall ruleAll = null;
                int maxMatches = 1;
                if (rule is SequenceRuleAllCall)
                {
                    ruleAll = (SequenceRuleAllCall)rule;
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

        protected bool ApplyRule(SequenceRuleCall rule, IGraph graph, IMatches matches, IMatch match)
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
        public SequenceTransaction(Sequence seq) : base(seq, SequenceType.Transaction)
        {
        }

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
        public SequenceRuleCall Rule;
        public Sequence Seq;

        public SequenceBacktrack(Sequence seqRule, Sequence seq) : base(SequenceType.Backtrack)
        {
            Rule = (SequenceRuleCall)seqRule;
            Seq = seq;
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceBacktrack copy = (SequenceBacktrack)MemberwiseClone();
            copy.Rule = (SequenceRuleCall)Rule.Copy(originalToCopy);
            copy.Seq = Seq.Copy(originalToCopy);
            copy.executionState = SequenceExecutionState.NotYet;
            return copy;
        }

        internal override void ReplaceSequenceDefinition(SequenceDefinition oldDef, SequenceDefinition newDef)
        {
            Seq.ReplaceSequenceDefinition(oldDef, newDef);
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

        public override Sequence GetCurrentlyExecutedSequence()
        {
            if(Rule.GetCurrentlyExecutedSequence() != null)
                return Rule.GetCurrentlyExecutedSequence();
            if(Seq.GetCurrentlyExecutedSequence() != null)
                return Seq.GetCurrentlyExecutedSequence();
            if(executionState == SequenceExecutionState.Underway)
                return this;
            return null;
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables, Sequence target)
        {
            if(Rule.GetLocalVariables(variables, target))
                return true;
            if(Seq.GetLocalVariables(variables, target))
                return true;
            return this == target;
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

        public List<SequenceVariable> VariablesFallingOutOfScopeOnLeavingIf;
        public List<SequenceVariable> VariablesFallingOutOfScopeOnLeavingTrueCase;

        public SequenceIfThenElse(Sequence condition, Sequence trueCase, Sequence falseCase,
            List<SequenceVariable> variablesFallingOutOfScopeOnLeavingIf,
            List<SequenceVariable> variablesFallingOutOfScopeOnLeavingTrueCase)
            : base(SequenceType.IfThenElse)
        {
            Condition = condition;
            TrueCase = trueCase;
            FalseCase = falseCase;
            VariablesFallingOutOfScopeOnLeavingIf = variablesFallingOutOfScopeOnLeavingIf;
            VariablesFallingOutOfScopeOnLeavingTrueCase = variablesFallingOutOfScopeOnLeavingTrueCase;
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceIfThenElse copy = (SequenceIfThenElse)MemberwiseClone();
            copy.Condition = Condition.Copy(originalToCopy);
            copy.TrueCase = TrueCase.Copy(originalToCopy);
            copy.FalseCase = FalseCase.Copy(originalToCopy);
            copy.VariablesFallingOutOfScopeOnLeavingIf = new List<SequenceVariable>(VariablesFallingOutOfScopeOnLeavingIf.Count);
            foreach(SequenceVariable var in VariablesFallingOutOfScopeOnLeavingIf)
                copy.VariablesFallingOutOfScopeOnLeavingIf.Add(var.Copy(originalToCopy));
            copy.VariablesFallingOutOfScopeOnLeavingTrueCase = new List<SequenceVariable>(VariablesFallingOutOfScopeOnLeavingTrueCase.Count);
            foreach(SequenceVariable var in VariablesFallingOutOfScopeOnLeavingTrueCase)
                copy.VariablesFallingOutOfScopeOnLeavingTrueCase.Add(var.Copy(originalToCopy));
            copy.executionState = SequenceExecutionState.NotYet;
            return copy;
        }

        internal override void ReplaceSequenceDefinition(SequenceDefinition oldDef, SequenceDefinition newDef)
        {
            Condition.ReplaceSequenceDefinition(oldDef, newDef);
            TrueCase.ReplaceSequenceDefinition(oldDef, newDef);
            FalseCase.ReplaceSequenceDefinition(oldDef, newDef);
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            return Condition.Apply(graph, env) ? TrueCase.Apply(graph, env) : FalseCase.Apply(graph, env);
        }

        public override Sequence GetCurrentlyExecutedSequence()
        {
            if(Condition.GetCurrentlyExecutedSequence() != null)
                return Condition.GetCurrentlyExecutedSequence();
            if(TrueCase.GetCurrentlyExecutedSequence() != null)
                return TrueCase.GetCurrentlyExecutedSequence();
            if(FalseCase.GetCurrentlyExecutedSequence() != null)
                return FalseCase.GetCurrentlyExecutedSequence();
            if(executionState == SequenceExecutionState.Underway)
                return this;
            return null;
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables, Sequence target)
        {
            if(Condition.GetLocalVariables(variables, target))
                return true;
            if(TrueCase.GetLocalVariables(variables, target))
                return true;
            foreach(SequenceVariable seqVar in VariablesFallingOutOfScopeOnLeavingTrueCase)
                variables.Remove(seqVar);
            if(FalseCase.GetLocalVariables(variables, target))
                return true;
            foreach(SequenceVariable seqVar in VariablesFallingOutOfScopeOnLeavingIf)
                variables.Remove(seqVar);
            return this == target;
        }

        public override IEnumerable<Sequence> Children { get { yield return Condition; yield return TrueCase; yield return FalseCase; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "if{ ... ; ... ; ...}"; } }
    }

    public class SequenceIfThen : SequenceBinary
    {
        public List<SequenceVariable> VariablesFallingOutOfScopeOnLeavingIf;
        public List<SequenceVariable> VariablesFallingOutOfScopeOnLeavingTrueCase;

        public SequenceIfThen(Sequence condition, Sequence trueCase,
            List<SequenceVariable> variablesFallingOutOfScopeOnLeavingIf,
            List<SequenceVariable> variablesFallingOutOfScopeOnLeavingTrueCase)
            : base(condition, trueCase, false, false, SequenceType.IfThen)
        {
            VariablesFallingOutOfScopeOnLeavingIf = variablesFallingOutOfScopeOnLeavingIf;
            VariablesFallingOutOfScopeOnLeavingTrueCase = variablesFallingOutOfScopeOnLeavingTrueCase;
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceIfThen copy = (SequenceIfThen)MemberwiseClone();
            copy.Left = Left.Copy(originalToCopy);
            copy.Right = Right.Copy(originalToCopy);
            copy.VariablesFallingOutOfScopeOnLeavingIf = new List<SequenceVariable>(VariablesFallingOutOfScopeOnLeavingIf.Count);
            foreach(SequenceVariable var in VariablesFallingOutOfScopeOnLeavingIf)
                copy.VariablesFallingOutOfScopeOnLeavingIf.Add(var.Copy(originalToCopy));
            copy.VariablesFallingOutOfScopeOnLeavingTrueCase = new List<SequenceVariable>(VariablesFallingOutOfScopeOnLeavingTrueCase.Count);
            foreach(SequenceVariable var in VariablesFallingOutOfScopeOnLeavingTrueCase)
                copy.VariablesFallingOutOfScopeOnLeavingTrueCase.Add(var.Copy(originalToCopy));
            copy.executionState = SequenceExecutionState.NotYet;
            return copy;
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            return Left.Apply(graph, env) ? Right.Apply(graph, env) : true; // lazy implication
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables, Sequence target)
        {
            if(Left.GetLocalVariables(variables, target))
                return true;
            if(Right.GetLocalVariables(variables, target))
                return true;
            foreach(SequenceVariable seqVar in VariablesFallingOutOfScopeOnLeavingTrueCase)
                variables.Remove(seqVar);
            foreach(SequenceVariable seqVar in VariablesFallingOutOfScopeOnLeavingIf)
                variables.Remove(seqVar);
            return this == target;
        }

        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "if{ ... ; ...}"; } }
    }

    public class SequenceFor : SequenceUnary
    {
        public SequenceVariable Var;
        public SequenceVariable VarDst;
        public SequenceVariable Container;

        public List<SequenceVariable> VariablesFallingOutOfScopeOnLeavingFor;

        public SequenceFor(SequenceVariable var, SequenceVariable varDst, SequenceVariable container, Sequence seq,
            List<SequenceVariable> variablesFallingOutOfScopeOnLeavingFor)
            : base(seq, SequenceType.For)
        {
            Var = var;
            VarDst = varDst;
            Container = container;
            VariablesFallingOutOfScopeOnLeavingFor = variablesFallingOutOfScopeOnLeavingFor;
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceFor copy = (SequenceFor)MemberwiseClone();
            copy.Var = Var.Copy(originalToCopy);
            if(VarDst!=null)
                copy.VarDst = VarDst.Copy(originalToCopy);
            copy.Container = Container.Copy(originalToCopy);
            copy.Seq = Seq.Copy(originalToCopy);
            copy.VariablesFallingOutOfScopeOnLeavingFor = new List<SequenceVariable>(VariablesFallingOutOfScopeOnLeavingFor.Count);
            foreach(SequenceVariable var in VariablesFallingOutOfScopeOnLeavingFor)
                copy.VariablesFallingOutOfScopeOnLeavingFor.Add(var.Copy(originalToCopy));
            copy.executionState = SequenceExecutionState.NotYet;
            return copy;
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            bool res = true;
            if(Container.GetVariableValue(graph) is IList)
            {
                IList array = (IList)Container.GetVariableValue(graph);
                bool first = true;
                for(int i = 0; i < array.Count; ++i)
                {
                    if(env != null && !first) env.EndOfIteration(true, this);
                    if(VarDst != null)
                    {
                        Var.SetVariableValue(i, graph);
                        VarDst.SetVariableValue(array[i], graph);
                    }
                    else
                    {
                        Var.SetVariableValue(array[i], graph);
                    }
                    Seq.ResetExecutionState();
                    res &= Seq.Apply(graph, env);
                    first = false;
                }
                if(env != null) env.EndOfIteration(false, this);
            }
            else
            {
                IDictionary setmap = (IDictionary)Container.GetVariableValue(graph);
                bool first = true;
                foreach(DictionaryEntry entry in setmap)
                {
                    if(env != null && !first) env.EndOfIteration(true, this);
                    Var.SetVariableValue(entry.Key, graph);
                    if(VarDst != null)
                        VarDst.SetVariableValue(entry.Value, graph);
                    Seq.ResetExecutionState();
                    res &= Seq.Apply(graph, env);
                    first = false;
                }
                if(env != null) env.EndOfIteration(false, this);
            }
            return res;
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables, Sequence target)
        {
            Var.GetLocalVariables(variables);
            if(VarDst != null)
                VarDst.GetLocalVariables(variables); 
            if(Seq.GetLocalVariables(variables, target))
                return true;
            foreach(SequenceVariable seqVar in VariablesFallingOutOfScopeOnLeavingFor)
                variables.Remove(seqVar);
            if(VarDst != null)
                variables.Remove(VarDst);
            variables.Remove(Var);
            return this == target;
        }

        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "for{"+Var.Name+(VarDst!=null?"->"+VarDst.Name:"")+" in "+Container.Name+"; ...}"; } }
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

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceIsVisited copy = (SequenceIsVisited)MemberwiseClone();
            copy.GraphElementVar = GraphElementVar.Copy(originalToCopy);
            copy.VisitedFlagVar = VisitedFlagVar.Copy(originalToCopy);
            copy.executionState = SequenceExecutionState.NotYet;
            return copy;
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            IGraphElement elem = (IGraphElement)GraphElementVar.GetVariableValue(graph);
            int visitedFlag = (int)VisitedFlagVar.GetVariableValue(graph);
            return graph.IsVisited(elem, visitedFlag);
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables, Sequence target)
        {
            GraphElementVar.GetLocalVariables(variables);
            VisitedFlagVar.GetLocalVariables(variables);
            return this == target;
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

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceSetVisited copy = (SequenceSetVisited)MemberwiseClone();
            copy.GraphElementVar = GraphElementVar.Copy(originalToCopy);
            copy.VisitedFlagVar = VisitedFlagVar.Copy(originalToCopy);
            if(Var!=null)
                copy.Var = Var.Copy(originalToCopy);
            copy.executionState = SequenceExecutionState.NotYet;
            return copy;
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

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables, Sequence target)
        {
            GraphElementVar.GetLocalVariables(variables);
            VisitedFlagVar.GetLocalVariables(variables);
            if(Var != null)
                Var.GetLocalVariables(variables);
            return this == target;
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

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceVFree copy = (SequenceVFree)MemberwiseClone();
            copy.VisitedFlagVar = VisitedFlagVar.Copy(originalToCopy);
            copy.executionState = SequenceExecutionState.NotYet;
            return copy;
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            int visitedFlag = (int)VisitedFlagVar.GetVariableValue(graph);
            graph.FreeVisitedFlag(visitedFlag);
            return true;
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables, Sequence target)
        {
            VisitedFlagVar.GetLocalVariables(variables);
            return this == target;
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

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceVReset copy = (SequenceVReset)MemberwiseClone();
            copy.VisitedFlagVar = VisitedFlagVar.Copy(originalToCopy);
            copy.executionState = SequenceExecutionState.NotYet;
            return copy;
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            int visitedFlag = (int)VisitedFlagVar.GetVariableValue(graph);
            graph.ResetVisitedFlag(visitedFlag);
            return true;
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables, Sequence target)
        {
            VisitedFlagVar.GetLocalVariables(variables);
            return this == target;
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

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceEmit copy = (SequenceEmit)MemberwiseClone();
            if(Variable!=null)
                copy.Variable = Variable.Copy(originalToCopy);
            copy.executionState = SequenceExecutionState.NotYet;
            return copy;
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            if(Variable!=null) {
                object val = Variable.GetVariableValue(graph);
                if(val!=null) {
                    if(val is IDictionary)
                        graph.EmitWriter.Write(DictionaryListHelper.ToString((IDictionary)val, env!=null ? env.GetNamedGraph() : graph));
                    else if(val is IList)
                        graph.EmitWriter.Write(DictionaryListHelper.ToString((IList)val, env != null ? env.GetNamedGraph() : graph));
                    else
                        graph.EmitWriter.Write(DictionaryListHelper.ToString(val, env!=null ? env.GetNamedGraph() : graph));
                }
            } else {
                graph.EmitWriter.Write(Text);
            }
            return true;
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables, Sequence target)
        {
            if(Variable!=null)
                Variable.GetLocalVariables(variables);
            return this == target;
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

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceRecord copy = (SequenceRecord)MemberwiseClone();
            if(Variable!=null)
                copy.Variable = Variable.Copy(originalToCopy);
            copy.executionState = SequenceExecutionState.NotYet;
            return copy;
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            if(Variable!=null) {
                object val = Variable.GetVariableValue(graph);
                if(val!=null) {
                    if(val is IDictionary)
                        graph.Recorder.Write(DictionaryListHelper.ToString((IDictionary)val, env!=null ? env.GetNamedGraph() : graph));
                    else
                        graph.Recorder.Write(DictionaryListHelper.ToString(val, env!=null ? env.GetNamedGraph() : graph));
                }
            } else {
                graph.Recorder.Write(Text);
            }
            return true;
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables, Sequence target)
        {
            if(Variable!=null)
                Variable.GetLocalVariables(variables);
            return this == target;
        }

        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return Variable != null ? "record(" + Variable.Name + ")" : "record(" + Text + ")"; } }
    }

    public class SequenceContainerAdd : Sequence
    {
        public SequenceVariable Container;
        public SequenceVariable Var;
        public SequenceVariable VarDst;

        public SequenceContainerAdd(SequenceVariable container, SequenceVariable var, SequenceVariable varDst)
            : base(SequenceType.ContainerAdd)
        {
            Container = container;
            Var = var;
            VarDst = varDst;
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceContainerAdd copy = (SequenceContainerAdd)MemberwiseClone();
            copy.Container = Container.Copy(originalToCopy);
            copy.Var = Var.Copy(originalToCopy);
            if(VarDst!=null)
                copy.VarDst = VarDst.Copy(originalToCopy);
            copy.executionState = SequenceExecutionState.NotYet;
            return copy;
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            if(Container.GetVariableValue(graph) is IList)
            {
                IList array = (IList)Container.GetVariableValue(graph);
                if(VarDst == null)
                    array.Add(Var.GetVariableValue(graph));
                else
                    array.Insert((int)VarDst.GetVariableValue(graph), Var.GetVariableValue(graph));
            }
            else
            {
                IDictionary setmap = (IDictionary)Container.GetVariableValue(graph);
                if(setmap.Contains(Var.GetVariableValue(graph)))
                {
                    setmap[Var.GetVariableValue(graph)] = (VarDst == null ? null : VarDst.GetVariableValue(graph));
                }
                else
                {
                    setmap.Add(Var.GetVariableValue(graph), (VarDst == null ? null : VarDst.GetVariableValue(graph)));
                }
            }
            return true;
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables, Sequence target)
        {
            Container.GetLocalVariables(variables);
            Var.GetLocalVariables(variables);
            if(VarDst != null)
                VarDst.GetLocalVariables(variables);
            return this == target;
        }

        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return Container.Name+".add("+Var.Name+(VarDst!=null?","+VarDst.Name:"")+")"; } }
    }

    public class SequenceContainerRem : Sequence
    {
        public SequenceVariable Container;
        public SequenceVariable Var;

        public SequenceContainerRem(SequenceVariable container, SequenceVariable var)
            : base(SequenceType.ContainerRem)
        {
            Container = container;
            Var = var;
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceContainerRem copy = (SequenceContainerRem)MemberwiseClone();
            copy.Container = Container.Copy(originalToCopy);
            if(Var!=null)
                copy.Var = Var.Copy(originalToCopy);
            copy.executionState = SequenceExecutionState.NotYet;
            return copy;
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            if(Container.GetVariableValue(graph) is IList)
            {
                IList array = (IList)Container.GetVariableValue(graph);
                if(Var == null)
                    array.RemoveAt(array.Count - 1);
                else
                    array.RemoveAt((int)Var.GetVariableValue(graph));
            }
            else
            {
                IDictionary setmap = (IDictionary)Container.GetVariableValue(graph);
                setmap.Remove(Var.GetVariableValue(graph));
            }
            return true;
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables, Sequence target)
        {
            Container.GetLocalVariables(variables);
            if(Var!=null)
                Var.GetLocalVariables(variables);
            return this == target;
        }

        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return Container.Name+".rem("+Var.Name+")"; } }
    }

    public class SequenceContainerClear : Sequence
    {
        public SequenceVariable Container;

        public SequenceContainerClear(SequenceVariable container)
            : base(SequenceType.ContainerClear)
        {
            Container = container;
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceContainerClear copy = (SequenceContainerClear)MemberwiseClone();
            copy.Container = Container.Copy(originalToCopy);
            copy.executionState = SequenceExecutionState.NotYet;
            return copy;
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            if(Container.GetVariableValue(graph) is IList)
            {
                IList array = (IList)Container.GetVariableValue(graph);
                array.Clear();
            }
            else
            {
                IDictionary setmap = (IDictionary)Container.GetVariableValue(graph);
                setmap.Clear();
            }
            return true;
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables, Sequence target)
        {
            Container.GetLocalVariables(variables);
            return this == target;
        }

        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return Container.Name + ".clear()"; } }
    }

    public class SequenceIn : Sequence
    {
        public SequenceVariable Var;
        public SequenceVariable Container;

        public SequenceIn(SequenceVariable var, SequenceVariable container)
            : base(SequenceType.InContainer)
        {
            Var = var;
            Container = container;
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceIn copy = (SequenceIn)MemberwiseClone();
            copy.Container = Container.Copy(originalToCopy);
            copy.Var = Var.Copy(originalToCopy);
            copy.executionState = SequenceExecutionState.NotYet;
            return copy;
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            if(Container.GetVariableValue(graph) is IList)
            {
                IList array = (IList)Container.GetVariableValue(graph);
                return array.Contains(Var.GetVariableValue(graph));
            }
            else
            {
                IDictionary setmap = (IDictionary)Container.GetVariableValue(graph);
                return setmap.Contains(Var.GetVariableValue(graph));
            }
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables, Sequence target)
        {
            Container.GetLocalVariables(variables);
            Var.GetLocalVariables(variables);
            return this == target;
        }

        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return Var.Name + " in " + Container.Name; } }
    }

    /// <summary>
    /// An sequence representing a sequence definition.
    /// It must be applied with a different method than the other sequences because it requires the parameter information.
    /// </summary>
    public abstract class SequenceDefinition : Sequence
    {
        public String SequenceName;

        public SequenceDefinition(SequenceType seqType, String sequenceName)
            : base(seqType)
        {
            SequenceName = sequenceName;
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            throw new Exception("Can't apply sequence definition like a normal sequence");
        }

        /// <summary>
        /// Applies this sequence.
        /// </summary>
        /// <param name="sequenceInvocation">Sequence invocation object for this sequence application,
        ///     containing the input parameter sources and output parameter targets</param>
        /// <param name="graph">The graph on which this sequence is to be applied.
        ///     The rules will only be chosen during the Sequence object instantiation, so
        ///     exchanging rules will have no effect for already existing Sequence objects.</param>
        /// <param name="env">The execution environment giving access to the names and user interface (null if not available)</param>
        /// <returns>True, iff the sequence succeeded</returns>
        public abstract bool Apply(SequenceInvocationParameterBindings sequenceInvocation,
            IGraph graph, SequenceExecutionEnvironment env);

        public override int Precedence { get { return -1; } }
        public override string Symbol { get { return SequenceName; } }
    }

    /// <summary>
    /// An sequence representing an interpreted sequence definition.
    /// Like the other sequences it can be directly interpreted (but with a different apply method),
    /// in contrast to the others it always must be the root sequence.
    /// </summary>
    public class SequenceDefinitionInterpreted : SequenceDefinition
    {
        public SequenceVariable[] InputVariables;
        public SequenceVariable[] OutputVariables;
        public Sequence Seq;

        // a cache for copies of sequence definitions, accessed by the name
        private static Dictionary<String, Stack<SequenceDefinition>> nameToCopies = 
            new Dictionary<string, Stack<SequenceDefinition>>(); 

        // an empty stack to return an iterator if the copies cache does not contain a value for a given name
        private static Stack<SequenceDefinition> emptyStack =
            new Stack<SequenceDefinition>();

        public SequenceDefinitionInterpreted(String sequenceName,
            SequenceVariable[] inputVariables,
            SequenceVariable[] outputVariables,
            Sequence seq)
            : base(SequenceType.SequenceDefinitionInterpreted, sequenceName)
        {
            InputVariables = inputVariables;
            OutputVariables = outputVariables;
            Seq = seq;
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceDefinitionInterpreted copy = (SequenceDefinitionInterpreted)MemberwiseClone();
            copy.InputVariables = new SequenceVariable[InputVariables.Length];
            for(int i = 0; i < InputVariables.Length; ++i)
                copy.InputVariables[i] = InputVariables[i].Copy(originalToCopy);
            copy.OutputVariables = new SequenceVariable[OutputVariables.Length];
            for(int i = 0; i < OutputVariables.Length; ++i)
                copy.OutputVariables[i] = OutputVariables[i].Copy(originalToCopy);
            copy.Seq = Seq.Copy(originalToCopy);
            copy.executionState = SequenceExecutionState.NotYet;
            return copy;
        }

        internal override void ReplaceSequenceDefinition(SequenceDefinition oldDef, SequenceDefinition newDef)
        {
            Seq.ReplaceSequenceDefinition(oldDef, newDef);
        }

        public override bool Apply(SequenceInvocationParameterBindings sequenceInvocation,
            IGraph graph, SequenceExecutionEnvironment env)
        {
            // If this sequence definition is currently executed
            // we must copy it and use the copy in its place
            // to prevent state corruption.
            if(executionState == SequenceExecutionState.Underway)
            {
                return ApplyCopy(sequenceInvocation, graph, env);
            }

            graph.EnteringSequence(this);
            executionState = SequenceExecutionState.Underway;
#if LOG_SEQUENCE_EXECUTION
            writer.WriteLine("Before executing sequence definition " + Id + ": " + Symbol);
#endif
            bool res = ApplyImpl(sequenceInvocation, graph, env);
#if LOG_SEQUENCE_EXECUTION
            writer.WriteLine("After executing sequence definition " + Id + ": " + Symbol + " result " + res);
#endif
            executionState = res ? SequenceExecutionState.Success : SequenceExecutionState.Fail;

            if(env != null) env.EndOfIteration(false, this);
            
            graph.ExitingSequence(this);

            ResetExecutionState(); // state is shown by call, we don't exist any more for the debugger

            return res;
        }

        // creates or reuses a copy and applies the copy
        protected bool ApplyCopy(SequenceInvocationParameterBindings sequenceInvocation,
            IGraph graph, SequenceExecutionEnvironment env)
        {
            // To improve performance we recycle copies in nameToCopies.
            SequenceDefinition seqCopy;
            if(nameToCopies.ContainsKey(SequenceName) && nameToCopies[SequenceName].Count > 0)
            {
                seqCopy = nameToCopies[SequenceName].Pop();
            }
            else
            {
                Dictionary<SequenceVariable, SequenceVariable> originalToCopy
                    = new Dictionary<SequenceVariable, SequenceVariable>();
                seqCopy = (SequenceDefinition)Copy(originalToCopy);
            }
            sequenceInvocation.SequenceDef = seqCopy;
            bool success = seqCopy.Apply(sequenceInvocation, graph, env);
            sequenceInvocation.SequenceDef = this;
            if(!nameToCopies.ContainsKey(SequenceName))
                nameToCopies.Add(SequenceName, new Stack<SequenceDefinition>());
            nameToCopies[SequenceName].Push(seqCopy);
            return success;
        }

        // applies the sequence of/in the sequence definition
        protected bool ApplyImpl(SequenceInvocationParameterBindings sequenceInvocation,
            IGraph graph, SequenceExecutionEnvironment env)
        {
            if(sequenceInvocation.ParamVars.Length != InputVariables.Length)
                throw new Exception("Number of input parameters given and expected differ for " + Symbol);
            if(sequenceInvocation.ReturnVars.Length != OutputVariables.Length)
                throw new Exception("Number of output parameters given and expected differ for " + Symbol);

            // prefill the local input variables with the invocation values, read from parameter variables of the caller
            for(int i=0; i<sequenceInvocation.ParamVars.Length; ++i)
            {
                if(sequenceInvocation.ParamVars[i] != null)
                    InputVariables[i].SetVariableValue(sequenceInvocation.ParamVars[i].GetVariableValue(graph), graph);
                else
                    InputVariables[i].SetVariableValue(sequenceInvocation.Parameters[i], graph);
            }

            bool success = Seq.Apply(graph, env);

            if(success)
            {
                // postfill the return-to variables of the caller with the return values, read from the local output variables
                for(int i = 0; i < sequenceInvocation.ReturnVars.Length; i++)
                    sequenceInvocation.ReturnVars[i].SetVariableValue(OutputVariables[i].GetVariableValue(graph), graph);
            }

            return success;
        }

        public void WasReplacedBy(SequenceDefinition newSeq)
        {
            if(newSeq.SequenceName != SequenceName)
                throw new Exception("Internal Failure: name mismatch on sequence replacement");
            nameToCopies.Remove(SequenceName);
        }

        public IEnumerable<SequenceDefinition> CachedSequenceCopies
        {
            get
            {
                if(nameToCopies.ContainsKey(SequenceName))
                    return nameToCopies[SequenceName];
                else
                    return emptyStack;
            }
        }

        public override Sequence GetCurrentlyExecutedSequence()
        {
            if(Seq.GetCurrentlyExecutedSequence() != null)
                return Seq.GetCurrentlyExecutedSequence();
            if(executionState == SequenceExecutionState.Underway)
                return this;
            return null;
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables, Sequence target)
        {
            foreach(SequenceVariable seqVar in InputVariables)
                seqVar.GetLocalVariables(variables);
            foreach(SequenceVariable seqVar in OutputVariables)
                seqVar.GetLocalVariables(variables);
            if(Seq.GetLocalVariables(variables, target))
                return true;
            return this == target;
        }

        public override IEnumerable<Sequence> Children { get { yield return Seq; } }
        public override string Symbol
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(SequenceName);
                if(InputVariables.Length > 0)
                {
                    sb.Append("(");
                    for(int i = 0; i < InputVariables.Length; ++i)
                    {
                        sb.Append(InputVariables[i].Name + ":" + InputVariables[i].Type);
                        if(i != InputVariables.Length - 1) sb.Append(",");
                    }
                    sb.Append(")");
                }
                if(OutputVariables.Length > 0)
                {
                    sb.Append(":(");
                    for(int i = 0; i < OutputVariables.Length; ++i)
                    {
                        sb.Append(OutputVariables[i].Name + ":" + OutputVariables[i].Type);
                        if(i != OutputVariables.Length - 1) sb.Append(",");
                    }
                    sb.Append(")");
                }
                return sb.ToString();
            }
        }
    }

    /// <summary>
    /// A sequence representing a compiled sequence definition.
    /// The subclass contains the method implementing the real sequence,
    /// and ApplyImpl calling that method mapping SequenceInvocationParameterBindings to the exact parameters of that method.
    /// </summary>
    public abstract class SequenceDefinitionCompiled : SequenceDefinition
    {
        public DefinedSequenceInfo SeqInfo;

        public SequenceDefinitionCompiled(String sequenceName, DefinedSequenceInfo seqInfo)
            : base(SequenceType.SequenceDefinitionCompiled, sequenceName)
        {
            SeqInfo = seqInfo;
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            throw new Exception("Copy not supported on compiled sequences");
        }

        internal override void ReplaceSequenceDefinition(SequenceDefinition oldDef, SequenceDefinition newDef)
        {
            throw new Exception("ReplaceSequenceDefinition not supported on compiled sequences");
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            throw new Exception("Can't apply compiled sequence definition like a normal sequence");
        }

        public abstract override bool Apply(SequenceInvocationParameterBindings sequenceInvocation,
            IGraph graph, SequenceExecutionEnvironment env);

        public override Sequence GetCurrentlyExecutedSequence()
        {
            throw new Exception("GetCurrentlyExecutedSequence not supported on compiled sequences");
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables, Sequence target)
        {
            throw new Exception("GetLocalVariables not supported on compiled sequences");
        }

        public override IEnumerable<Sequence> Children { get { yield break; } }
    }

    public class SequenceSequenceCall : SequenceSpecial
    {
        public SequenceInvocationParameterBindings ParamBindings;

        public SequenceSequenceCall(SequenceInvocationParameterBindings paramBindings, bool special)
            : base(special, SequenceType.SequenceCall)
        {
            ParamBindings = paramBindings;
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceSequenceCall copy = (SequenceSequenceCall)MemberwiseClone();
            copy.ParamBindings = ParamBindings.Copy(originalToCopy);
            copy.executionState = SequenceExecutionState.NotYet;
            return copy;
        }

        internal override void ReplaceSequenceDefinition(SequenceDefinition oldDef, SequenceDefinition newDef)
        {
            if(ParamBindings.SequenceDef==oldDef)
                ParamBindings.SequenceDef = newDef;
        }

        protected override bool ApplyImpl(IGraph graph, SequenceExecutionEnvironment env)
        {
            SequenceDefinition seqDef = ParamBindings.SequenceDef;
            bool res = seqDef.Apply(ParamBindings, graph, env);

#if LOG_SEQUENCE_EXECUTION
            if(res)
            {
                writer.WriteLine("Applied sequence " + Symbol + " successfully");
                writer.Flush();
            }
#endif
            return res;
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables, Sequence target)
        {
            ParamBindings.GetLocalVariables(variables);
            return this == target;
        }

        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }

        protected String GetSequenceString()
        {
            StringBuilder sb = new StringBuilder();
            if(ParamBindings.ReturnVars.Length > 0)
            {
                sb.Append("(");
                for(int i = 0; i < ParamBindings.ReturnVars.Length; ++i)
                {
                    sb.Append(ParamBindings.ReturnVars[i].Name);
                    if(i != ParamBindings.ReturnVars.Length - 1) sb.Append(",");
                }
                sb.Append(")=");
            }
            sb.Append(ParamBindings.SequenceDef.SequenceName);
            if(ParamBindings.ParamVars.Length > 0)
            {
                sb.Append("(");
                for(int i = 0; i < ParamBindings.ParamVars.Length; ++i)
                {
                    if(ParamBindings.ParamVars[i] != null)
                        sb.Append(ParamBindings.ParamVars[i].Name);
                    else
                        sb.Append(ParamBindings.Parameters[i] != null ? ParamBindings.Parameters[i] : "null");
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
                return (Special ? "%" : "") + GetSequenceString();
            }
        }
    }
}

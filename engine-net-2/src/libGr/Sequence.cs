/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

//#define LOG_SEQUENCE_EXECUTION // you must uncomment it in SequenceBase.cs, too

using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;

namespace de.unika.ipd.grGen.libGr
{
    // todo: set/map/array constructors as sequence expression?
    // todo: (optional) execution environment for InvocationParameterBindings, so parameter evaluation can access named graph?

    /// <summary>
    /// Specifies the actual subtype used for a Sequence.
    /// A new sequence type -> you must add the corresponding class down below,
    /// and adapt the lgspSequenceGenerator and the Debugger.
    /// </summary>
    public enum SequenceType
    {
        ThenLeft, ThenRight, LazyOr, LazyAnd, StrictOr, Xor, StrictAnd, Not,
        LazyOrAll, LazyAndAll, StrictOrAll, StrictAndAll, SomeFromSet,
        IfThenElse, IfThen, For,
        Transaction, Backtrack, Pause,
        IterationMin, IterationMinMax,
        RuleCall, RuleAllCall,
        AssignSequenceResultToVar, OrAssignSequenceResultToVar, AndAssignSequenceResultToVar,
        AssignUserInputToVar, AssignRandomToVar, // needed as sequence because of debugger integration
        DeclareVariable, AssignConstToVar, AssignVarToVar, // needed as sequence to allow variable declaration and initialization in sequence scope (VarToVar for embedded sequences, assigning rule elements to a variable)
        SequenceDefinitionInterpreted, SequenceDefinitionCompiled, SequenceCall,
        BooleanComputation
    }

    /// <summary>
    /// States of executing sequences: not (yet) executed, execution underway, successful execution, fail execution
    /// </summary>
    public enum SequenceExecutionState
    {
        NotYet, Underway, Success, Fail
    }

    /// <summary>
    /// A sequence object with references to child sequences.
    /// A sequence is basically a rule application or a computation, or an operator combining them.
    /// </summary>
    public abstract class Sequence : SequenceBase
    {
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
        /// Checks the sequence for errors utilizing the given checking environment
        /// reports them by exception
        /// default behavior: check all the children 
        /// </summary>
        public override void Check(SequenceCheckingEnvironment env)
        {
            foreach(Sequence childSeq in Children)
                childSeq.Check(env);
        }

        /// <summary>
        /// Returns the type of the sequence, which is "boolean"
        /// </summary>
        public override string Type(SequenceCheckingEnvironment env)
        {
            return "boolean";
        }

        /// <summary>
        /// Copies the sequence deeply so that
        /// - the execution state of the copy is NotYet
        /// - the global Variables are kept
        /// - the local Variables are replaced by copies initialized to null
        /// Used for cloning defined sequences before executing them if needed.
        /// Needed if the defined sequence is currently executed to prevent state corruption.
        /// </summary>
        /// <param name="originalToCopy">A map used to ensure that every instance of a variable is mapped to the same copy</param>
        /// <returns>The copy of the sequence</returns>
        internal abstract Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv);

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
        /// <param name="procEnv">The graph processing environment on which this sequence is to be applied.
        ///     Contains especially the graph on which this sequence is to be applied.
        ///     And the user proxy queried when choices are due.
        ///     The rules will only be chosen during the Sequence object instantiation, so
        ///     exchanging rules will have no effect for already existing Sequence objects.</param>
        /// <returns>True, iff the sequence succeeded</returns>
        public bool Apply(IGraphProcessingEnvironment procEnv)
        {
            procEnv.EnteringSequence(this);
            executionState = SequenceExecutionState.Underway;
#if LOG_SEQUENCE_EXECUTION
            procEnv.Recorder.WriteLine("Before executing sequence " + Id + ": " + Symbol);
#endif
            bool res = ApplyImpl(procEnv);
#if LOG_SEQUENCE_EXECUTION
            procEnv.Recorder.WriteLine("After executing sequence " + Id + ": " + Symbol + " result " + res);
#endif
            executionState = res ? SequenceExecutionState.Success : SequenceExecutionState.Fail;
            procEnv.ExitingSequence(this);
            return res;
        }

        /// <summary>
        /// Applies this sequence. This function represents the actual implementation of the sequence.
        /// </summary>
        /// <param name="procEnv">The graph processing environment on which this sequence is to be applied.
        ///     Contains especially the graph on which this sequence is to be applied.
        ///     And the user proxy queried when choices are due.</param>
        /// <returns>True, iff the sequence succeeded</returns>
        protected abstract bool ApplyImpl(IGraphProcessingEnvironment procEnv);

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
        /// the state of executing this sequence
        /// </summary>
        public SequenceExecutionState ExecutionState { get { return executionState; } }

        /// <summary>
        /// the state of executing this sequence, implementation
        /// </summary>
        internal SequenceExecutionState executionState;

        public static readonly object[] NoElems = new object[] { }; // A singleton object array used when no elements are returned.
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

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceUnary copy = (SequenceUnary)MemberwiseClone();
            copy.Seq = Seq.Copy(originalToCopy, procEnv);
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

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceBinary copy = (SequenceBinary)MemberwiseClone();
            copy.Left = Left.Copy(originalToCopy, procEnv);
            copy.Right = Right.Copy(originalToCopy, procEnv);
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

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceNAry copy = (SequenceNAry)MemberwiseClone();
            copy.Sequences = new List<Sequence>();
            foreach(Sequence seq in Sequences)
                copy.Sequences.Add(seq.Copy(originalToCopy, procEnv));
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
    public abstract class SequenceAssignToVar : Sequence
    {
        public SequenceVariable DestVar;

        public SequenceAssignToVar(SequenceVariable destVar, SequenceType seqType)
            : base(seqType)
        {
            DestVar = destVar;
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceAssignToVar copy = (SequenceAssignToVar)MemberwiseClone();
            copy.DestVar = DestVar.Copy(originalToCopy, procEnv);
            copy.executionState = SequenceExecutionState.NotYet;
            return copy;
        }

        protected bool Assign(object value, IGraphProcessingEnvironment procEnv)
        {
            DestVar.SetVariableValue(value, procEnv);
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

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            bool res;
            int direction = 0;
            if(Random) direction = randomGenerator.Next(2);
            if(Choice) direction = procEnv.UserProxy.ChooseDirection(direction, this);
            if(direction == 1) {
                Right.Apply(procEnv);
                res = Left.Apply(procEnv);
            } else {
                res = Left.Apply(procEnv);
                Right.Apply(procEnv);
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

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            bool res;
            int direction = 0;
            if(Random) direction = randomGenerator.Next(2);
            if(Choice) direction = procEnv.UserProxy.ChooseDirection(direction, this);
            if(direction == 1)
            {
                res = Right.Apply(procEnv);
                Left.Apply(procEnv);
            } else {
                Left.Apply(procEnv);
                res = Right.Apply(procEnv);
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

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            int direction = 0;
            if(Random) direction = randomGenerator.Next(2);
            if(Choice) direction = procEnv.UserProxy.ChooseDirection(direction, this);
            if(direction == 1)
                return Right.Apply(procEnv) || Left.Apply(procEnv);
            else
                return Left.Apply(procEnv) || Right.Apply(procEnv);
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

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            int direction = 0;
            if(Random) direction = randomGenerator.Next(2);
            if(Choice) direction = procEnv.UserProxy.ChooseDirection(direction, this);
            if(direction == 1)
                return Right.Apply(procEnv) && Left.Apply(procEnv);
            else
                return Left.Apply(procEnv) && Right.Apply(procEnv);
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

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            int direction = 0;
            if(Random) direction = randomGenerator.Next(2);
            if(Choice) direction = procEnv.UserProxy.ChooseDirection(direction, this);
            if(direction == 1)
                return Right.Apply(procEnv) | Left.Apply(procEnv);
            else
                return Left.Apply(procEnv) | Right.Apply(procEnv);
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

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            int direction = 0;
            if(Random) direction = randomGenerator.Next(2);
            if(Choice) direction = procEnv.UserProxy.ChooseDirection(direction, this);
            if(direction == 1)
                return Right.Apply(procEnv) ^ Left.Apply(procEnv);
            else
                return Left.Apply(procEnv) ^ Right.Apply(procEnv);
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

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            int direction = 0;
            if(Random) direction = randomGenerator.Next(2);
            if(Choice) direction = procEnv.UserProxy.ChooseDirection(direction, this);
            if(direction == 1)
                return Right.Apply(procEnv) & Left.Apply(procEnv);
            else
                return Left.Apply(procEnv) & Right.Apply(procEnv);
        }

        public override int Precedence { get { return 5; } }
        public override string Symbol { get { string prefix = Random ? (Choice ? "$%" : "$") : ""; return prefix + "&"; } }
    }

    public class SequenceNot : SequenceUnary
    {
        public SequenceNot(Sequence seq) : base(seq, SequenceType.Not) {}

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            return !Seq.Apply(procEnv);
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

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            long i = 0;
            while(Seq.Apply(procEnv))
            {
                procEnv.EndOfIteration(true, this);
                Seq.ResetExecutionState();
                i++;
            }
            procEnv.EndOfIteration(false, this);
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

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            long i;
            bool first = true;
            for(i = 0; i < Max; i++)
            {
                if(!first) procEnv.EndOfIteration(true, this);
                Seq.ResetExecutionState();
                if(!Seq.Apply(procEnv)) break;
                first = false;
            }
            procEnv.EndOfIteration(false, this);
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

        public override void Check(SequenceCheckingEnvironment env)
        {
            env.CheckRuleCallRuleAllCallSequenceCall(this);
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceRuleCall copy = (SequenceRuleCall)MemberwiseClone();
            copy.ParamBindings = ParamBindings.Copy(originalToCopy, procEnv);
            copy.executionState = SequenceExecutionState.NotYet;
            return copy;
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            bool res;
            try
            {
#if LOG_SEQUENCE_EXECUTION
                procEnv.Recorder.WriteLine("Applying rule " + GetRuleCallString(procEnv));
#endif
                res = procEnv.ApplyRewrite(ParamBindings, 0, 1, Special, Test) > 0;
            }
            catch (NullReferenceException)
            {
                System.Console.Error.WriteLine("Null reference exception during rule execution (null parameter?): " + Symbol);
                throw;
            }

#if LOG_SEQUENCE_EXECUTION
            if(res)
            {
                procEnv.Recorder.WriteLine("Matched/Applied " + Symbol);
                procEnv.Recorder.Flush();
            }
#endif
            return res;
        }

        public virtual bool Rewrite(IGraphProcessingEnvironment procEnv, IMatches matches, IMatch chosenMatch)
        {
            if(matches.Count == 0) return false;
            if(Test) return false;

            IMatch match = chosenMatch!=null ? chosenMatch : matches.First;

            procEnv.Finishing(matches, Special);

            if(procEnv.PerformanceInfo != null) procEnv.PerformanceInfo.StartLocal();

            object[] retElems = null;
            retElems = matches.Producer.Modify(procEnv, match);
            if(procEnv.PerformanceInfo != null) procEnv.PerformanceInfo.RewritesPerformed++;

            if(retElems == null) retElems = NoElems;

            for(int i = 0; i < ParamBindings.ReturnVars.Length; i++)
                ParamBindings.ReturnVars[i].SetVariableValue(retElems[i], procEnv);

            if(procEnv.PerformanceInfo != null) procEnv.PerformanceInfo.StopRewrite(); // total rewrite time does NOT include listeners anymore

            procEnv.Finished(matches, Special);

#if LOG_SEQUENCE_EXECUTION
                procEnv.Recorder.WriteLine("Matched/Applied " + Symbol);
                procEnv.Recorder.Flush();
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

        public String GetRuleCallString(IGraphProcessingEnvironment procEnv)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(ParamBindings.Action.Name);
            if(ParamBindings.ArgumentExpressions.Length > 0)
            {
                sb.Append("(");
                for(int i = 0; i < ParamBindings.ArgumentExpressions.Length; ++i)
                {
                    if(ParamBindings.ArgumentExpressions[i] != null)
                        sb.Append(DictionaryListHelper.ToStringAutomatic(ParamBindings.ArgumentExpressions[i].Evaluate(procEnv), procEnv.Graph));
                    else
                        sb.Append(ParamBindings.Arguments[i] != null ? ParamBindings.Arguments[i] : "null");
                    if(i != ParamBindings.ArgumentExpressions.Length - 1) sb.Append(",");
                }
                sb.Append(")");
            }
            return sb.ToString();
        }

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
            if(ParamBindings.ArgumentExpressions.Length > 0)
            {
                sb.Append("(");
                for(int i = 0; i < ParamBindings.ArgumentExpressions.Length; ++i)
                {
                    if(ParamBindings.ArgumentExpressions[i] != null)
                        sb.Append(ParamBindings.ArgumentExpressions[i].Symbol);
                    else
                        sb.Append(ParamBindings.Arguments[i]!=null ? ParamBindings.Arguments[i] : "null");
                    if(i != ParamBindings.ArgumentExpressions.Length - 1) sb.Append(",");
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

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceRuleAllCall copy = (SequenceRuleAllCall)MemberwiseClone();
            copy.ParamBindings = ParamBindings.Copy(originalToCopy, procEnv);
            copy.MinVarChooseRandom = MinVarChooseRandom.Copy(originalToCopy, procEnv);
            copy.MaxVarChooseRandom = MaxVarChooseRandom.Copy(originalToCopy, procEnv);
            copy.executionState = SequenceExecutionState.NotYet;
            return copy;
        }

        public bool Random { get { return ChooseRandom; } set { ChooseRandom = value; } }
        public bool Choice { get { return choice; } set { choice = value; } }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            if(!ChooseRandom)
            {
                bool res;
                try
                {
#if LOG_SEQUENCE_EXECUTION
                    procEnv.Recorder.WriteLine("Applying rule all " + GetRuleCallString(procEnv));
#endif
                    res = procEnv.ApplyRewrite(ParamBindings, -1, -1, Special, Test) > 0;
                }
                catch (NullReferenceException)
                {
                    System.Console.Error.WriteLine("Null reference exception during rule execution (null parameter?): " + Symbol);
                    throw;
                }
#if LOG_SEQUENCE_EXECUTION
                if(res)
                {
                    procEnv.Recorder.WriteLine("Matched/Applied " + Symbol);
                    procEnv.Recorder.Flush();
                }
#endif
                return res;
            }
            else
            {
                // TODO: Code duplication! Compare with BaseGraph.ApplyRewrite.

                int curMaxMatches = procEnv.MaxMatches;

                object[] parameters;
                if(ParamBindings.ArgumentExpressions.Length > 0)
                {
                    parameters = ParamBindings.Arguments;
                    for(int i = 0; i < ParamBindings.ArgumentExpressions.Length; i++)
                    {
                        if(ParamBindings.ArgumentExpressions[i] != null)
                            parameters[i] = ParamBindings.ArgumentExpressions[i].Evaluate(procEnv);
                    }
                }
                else parameters = null;

                if(procEnv.PerformanceInfo != null) procEnv.PerformanceInfo.StartLocal();
                IMatches matches;
                try
                {
                    matches = ParamBindings.Action.Match(procEnv, curMaxMatches, parameters);
                }
                catch (NullReferenceException)
                {
                    System.Console.Error.WriteLine("Null reference exception during rule execution (null parameter?): " + Symbol);
                    throw;
                }
                if(procEnv.PerformanceInfo != null)
                {
                    procEnv.PerformanceInfo.StopMatch();              // total match time does NOT include listeners anymore
                    procEnv.PerformanceInfo.MatchesFound += matches.Count;
                }

                procEnv.Matched(matches, Special);

                return Rewrite(procEnv, matches, null);
            }
        }

        public override bool Rewrite(IGraphProcessingEnvironment procEnv, IMatches matches, IMatch chosenMatch)
        {
            if (matches.Count == 0) return false;
            if (Test) return false;

            if (MinSpecified)
            {
                if(!(MinVarChooseRandom.GetVariableValue(procEnv) is int))
                    throw new InvalidOperationException("The variable '" + MinVarChooseRandom + "' is not of type int!");
                if(matches.Count < (int)MinVarChooseRandom.GetVariableValue(procEnv))
                    return false;
            }

            procEnv.Finishing(matches, Special);

            if(procEnv.PerformanceInfo != null) procEnv.PerformanceInfo.StartLocal();

            object[] retElems = null;
            if (!ChooseRandom)
            {
                if (chosenMatch!=null)
                    throw new InvalidOperationException("Chosen match given although all matches should get rewritten");
                IEnumerator<IMatch> matchesEnum = matches.GetEnumerator();
                while (matchesEnum.MoveNext())
                {
                    IMatch match = matchesEnum.Current;
                    if (match != matches.First) procEnv.RewritingNextMatch();
                    retElems = matches.Producer.Modify(procEnv, match);
                    if(procEnv.PerformanceInfo != null) procEnv.PerformanceInfo.RewritesPerformed++;
                }
                if (retElems == null) retElems = NoElems;
            }
            else
            {
                object val = MaxVarChooseRandom != null ? MaxVarChooseRandom.GetVariableValue(procEnv) : (MinSpecified ? 2147483647 : 1);
                if (!(val is int))
                    throw new InvalidOperationException("The variable '" + MaxVarChooseRandom.Name + "' is not of type int!");
                int numChooseRandom = (int)val;
                if (matches.Count < numChooseRandom) numChooseRandom = matches.Count;

                for (int i = 0; i < numChooseRandom; i++)
                {
                    if (i != 0) procEnv.RewritingNextMatch();
                    int matchToApply = randomGenerator.Next(matches.Count);
                    if (Choice) matchToApply = procEnv.UserProxy.ChooseMatch(matchToApply, matches, numChooseRandom - 1 - i, this);
                    IMatch match = matches.RemoveMatch(matchToApply);
                    if (chosenMatch != null) match = chosenMatch;
                    retElems = matches.Producer.Modify(procEnv, match);
                    if (procEnv.PerformanceInfo != null) procEnv.PerformanceInfo.RewritesPerformed++;
                }
                if (retElems == null) retElems = NoElems;
            }

            for(int i = 0; i < ParamBindings.ReturnVars.Length; i++)
                ParamBindings.ReturnVars[i].SetVariableValue(retElems[i], procEnv);
            if(procEnv.PerformanceInfo != null) procEnv.PerformanceInfo.StopRewrite();            // total rewrite time does NOT include listeners anymore

            procEnv.Finished(matches, Special);

#if LOG_SEQUENCE_EXECUTION
                procEnv.Recorder.WriteLine("Matched/Applied " + Symbol);
                procEnv.Recorder.Flush();
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

    public class SequenceAssignUserInputToVar : SequenceAssignToVar, SequenceRandomChoice
    {
        public String Type;

        public bool Random { get { return false; } set { throw new Exception("can't change Random on SequenceAssignUserInputToVar"); } }
        public bool Choice { get { return true; } set { throw new Exception("can't change Choice on SequenceAssignUserInputToVar"); } }

        public SequenceAssignUserInputToVar(SequenceVariable destVar, String type)
            : base(destVar, SequenceType.AssignUserInputToVar)
        {
            Type = type;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            if(!TypesHelper.IsSameOrSubtype(Type, DestVar.Type, env.Model))
            {
                throw new SequenceParserException(Symbol, DestVar.Type, Type);
            }
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            return Assign(procEnv.UserProxy.ChooseValue(Type, this), procEnv);
        }

        public override string Symbol { get { return DestVar.Name + "=" + "$%(" + Type + ")"; } }
    }

    public class SequenceAssignRandomToVar : SequenceAssignToVar, SequenceRandomChoice
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

        public override void Check(SequenceCheckingEnvironment env)
        {
            if(!TypesHelper.IsSameOrSubtype(DestVar.Type, "int", env.Model))
            {
                throw new SequenceParserException(Symbol, "int", DestVar.Type);
            }
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            int randomNumber = randomGenerator.Next(Number);
            if(Choice) randomNumber = procEnv.UserProxy.ChooseRandomNumber(randomNumber, Number, this);
            return Assign(randomNumber, procEnv);
        }

        public override string Symbol { get { return DestVar.Name + "=" + (Choice ? "$%" : "$") + "(" + Number + ")"; } }
    }

    public class SequenceDeclareVariable : SequenceAssignConstToVar
    {
        public SequenceDeclareVariable(SequenceVariable destVar)
            : base(destVar, null)
        {
            SequenceType = SequenceType.DeclareVariable;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            Constant = TypesHelper.DefaultValue(DestVar.Type, env.Model);
        }

        public override string Symbol { get { return DestVar.Name + ":" + DestVar.Type; } }
    }

    public class SequenceAssignConstToVar : SequenceAssignToVar
    {
        public object Constant;

        public SequenceAssignConstToVar(SequenceVariable destVar, object constant)
            : base(destVar, SequenceType.AssignConstToVar)
        {
            Constant = constant;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            if(!TypesHelper.IsSameOrSubtype(TypesHelper.XgrsTypeOfConstant(Constant, env.Model), DestVar.Type, env.Model))
            {
                throw new SequenceParserException(Symbol, DestVar.Type, TypesHelper.XgrsTypeOfConstant(Constant, env.Model));
            }
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            return Assign(Constant, procEnv);
        }

        public override string Symbol
        {
            get
            {
                if(Constant == null)
                    return DestVar.Name + "=" + "null";
                else if(Constant.GetType().Name == "Dictionary`2")
                    return DestVar.Name + "=" + "{}"; // only empty set/map assignment possible as of now
                else if(Constant.GetType().Name == "List`1")
                    return DestVar.Name + "=" + "[]"; // only empty array assignment possible as of now
                else
                    return DestVar.Name + "=" + Constant.ToString();
            }
        }
    }

    public class SequenceAssignVarToVar : SequenceAssignToVar
    {
        public SequenceVariable Variable;

        public SequenceAssignVarToVar(SequenceVariable destVar, SequenceVariable srcVar)
            : base(destVar, SequenceType.AssignVarToVar)
        {
            Variable = srcVar;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            if(!TypesHelper.IsSameOrSubtype(Variable.Type, DestVar.Type, env.Model))
            {
                throw new SequenceParserException(Symbol, DestVar.Type, Variable.Type);
            }
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceAssignVarToVar copy = (SequenceAssignVarToVar)MemberwiseClone();
            copy.DestVar = DestVar.Copy(originalToCopy, procEnv);
            copy.Variable = Variable.Copy(originalToCopy, procEnv);
            copy.executionState = SequenceExecutionState.NotYet;
            return copy;
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables, Sequence target)
        {
            DestVar.GetLocalVariables(variables);
            Variable.GetLocalVariables(variables);
            return this == target;
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            return Assign(Variable.GetVariableValue(procEnv), procEnv);
        }

        public override string Symbol { get { return DestVar.Name + "=" + Variable.Name; } }
    }

    public class SequenceAssignSequenceResultToVar : SequenceAssignToVar
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

        public override void Check(SequenceCheckingEnvironment env)
        {
            Seq.Check(env);
            if(!TypesHelper.IsSameOrSubtype(DestVar.Type, "boolean", env.Model))
            {
                throw new SequenceParserException("sequence => " + DestVar.Name, "boolean", DestVar.Type);
            }
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceAssignSequenceResultToVar copy = (SequenceAssignSequenceResultToVar)MemberwiseClone();
            copy.Seq = Seq.Copy(originalToCopy, procEnv);
            copy.DestVar = DestVar.Copy(originalToCopy, procEnv);
            copy.executionState = SequenceExecutionState.NotYet;
            return copy;
        }

        internal override void ReplaceSequenceDefinition(SequenceDefinition oldDef, SequenceDefinition newDef)
        {
            Seq.ReplaceSequenceDefinition(oldDef, newDef);
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            bool result = Seq.Apply(procEnv);
            return Assign(result, procEnv);
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

        public override void Check(SequenceCheckingEnvironment env)
        {
            Seq.Check(env);
            if(!TypesHelper.IsSameOrSubtype(DestVar.Type, "boolean", env.Model))
            {
                throw new SequenceParserException("sequence |> " + DestVar.Name, "boolean", DestVar.Type);
            }
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            bool result = Seq.Apply(procEnv);
            return Assign(result || (bool)DestVar.GetVariableValue(procEnv), procEnv);
        }

        public override string Symbol { get { return "... |> " + DestVar.Name; } }
    }

    public class SequenceAndAssignSequenceResultToVar : SequenceAssignSequenceResultToVar
    {
        public SequenceAndAssignSequenceResultToVar(SequenceVariable destVar, Sequence sequence)
            : base(SequenceType.AndAssignSequenceResultToVar, destVar, sequence)
        {
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            Seq.Check(env);
            if(!TypesHelper.IsSameOrSubtype(DestVar.Type, "boolean", env.Model))
            {
                throw new SequenceParserException("sequence &> " + DestVar.Name, "boolean", DestVar.Type);
            }
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            bool result = Seq.Apply(procEnv);
            return Assign(result && (bool)DestVar.GetVariableValue(procEnv), procEnv);
        }

        public override string Symbol { get { return "... &> " + DestVar.Name; } }
    }

    public class SequenceLazyOrAll : SequenceNAry
    {
        public SequenceLazyOrAll(List<Sequence> sequences, bool choice)
            : base(sequences, choice, SequenceType.LazyOrAll)
        {
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            List<Sequence> sequences = new List<Sequence>(Sequences);
            while(sequences.Count != 0)
            {
                int seqToExecute = randomGenerator.Next(sequences.Count);
                if(Choice && !Skip) seqToExecute = procEnv.UserProxy.ChooseSequence(seqToExecute, sequences, this);
                bool result = sequences[seqToExecute].Apply(procEnv);
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

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            List<Sequence> sequences = new List<Sequence>(Sequences);
            while(sequences.Count != 0)
            {
                int seqToExecute = randomGenerator.Next(sequences.Count);
                if(Choice && !Skip) seqToExecute = procEnv.UserProxy.ChooseSequence(seqToExecute, sequences, this);
                bool result = sequences[seqToExecute].Apply(procEnv);
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

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            bool result = false;
            List<Sequence> sequences = new List<Sequence>(Sequences);
            while(sequences.Count != 0)
            {
                int seqToExecute = randomGenerator.Next(sequences.Count);
                if(Choice && !Skip) seqToExecute = procEnv.UserProxy.ChooseSequence(seqToExecute, sequences, this);
                result |= sequences[seqToExecute].Apply(procEnv);
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

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            bool result = true;
            List<Sequence> sequences = new List<Sequence>(Sequences);
            while(sequences.Count != 0)
            {
                int seqToExecute = randomGenerator.Next(sequences.Count);
                if(Choice && !Skip) seqToExecute = procEnv.UserProxy.ChooseSequence(seqToExecute, sequences, this);
                result &= sequences[seqToExecute].Apply(procEnv);
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

        public override void Check(SequenceCheckingEnvironment env)
        {
            foreach(Sequence seqChild in Sequences)
            {
                seqChild.Check(env);
                if(seqChild is SequenceRuleAllCall
                    && ((SequenceRuleAllCall)seqChild).MinVarChooseRandom != null
                    && ((SequenceRuleAllCall)seqChild).MaxVarChooseRandom != null)
                    throw new Exception("Sequence SomeFromSet (e.g. {r1,[r2],$[r3]}) can't contain a select with variable from all construct (e.g. $v[r4], e.g. $v1,v2[r4])");
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

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            MatchAll(procEnv);

            if (NumTotalMatches == 0)
            {
                for (int i = 0; i < Sequences.Count; ++i)
                   Sequences[i].executionState = SequenceExecutionState.Fail;
                return false;
            }

            if (chooseRandom)
            {
                int totalMatchToExecute = randomGenerator.Next(NumTotalMatches);
                if (Choice) totalMatchToExecute = procEnv.UserProxy.ChooseMatch(totalMatchToExecute, this);
                int ruleToExecute; int matchToExecute;
                FromTotalMatch(totalMatchToExecute, out ruleToExecute, out matchToExecute);
                SequenceRuleCall rule = (SequenceRuleCall)Sequences[ruleToExecute];
                IMatch match = Matches[ruleToExecute].GetMatch(matchToExecute);
                if (!(rule is SequenceRuleAllCall))
                    ApplyRule(rule, procEnv, Matches[ruleToExecute], null);
                else if (!((SequenceRuleAllCall)rule).ChooseRandom)
                    ApplyRule(rule, procEnv, Matches[ruleToExecute], null);
                else
                    ApplyRule(rule, procEnv, Matches[ruleToExecute], Matches[ruleToExecute].GetMatch(matchToExecute));
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
                        ApplyRule(rule, procEnv, Matches[i], null);
                    }
                    else
                        Sequences[i].executionState = SequenceExecutionState.Fail;
                }
            }

            return true;
        }

        protected void MatchAll(IGraphProcessingEnvironment procEnv)
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
                    maxMatches = procEnv.MaxMatches;
                }

                object[] parameters;
                if (rule.ParamBindings.ArgumentExpressions.Length > 0)
                {
                    parameters = rule.ParamBindings.Arguments;
                    for (int j = 0; j < rule.ParamBindings.ArgumentExpressions.Length; j++)
                    {
                        if (rule.ParamBindings.ArgumentExpressions[j] != null)
                            parameters[j] = rule.ParamBindings.ArgumentExpressions[j].Evaluate(procEnv);
                    }
                }
                else parameters = null;

                if (procEnv.PerformanceInfo != null) procEnv.PerformanceInfo.StartLocal();
                IMatches matches = rule.ParamBindings.Action.Match(procEnv, maxMatches, parameters);
                if (procEnv.PerformanceInfo != null)
                {
                    procEnv.PerformanceInfo.StopMatch();              // total match time does NOT include listeners anymore
                    procEnv.PerformanceInfo.MatchesFound += matches.Count;
                }

                Matches[i] = matches;
            }
        }

        protected bool ApplyRule(SequenceRuleCall rule, IGraphProcessingEnvironment procEnv, IMatches matches, IMatch match)
        {
            bool result;
            procEnv.EnteringSequence(rule);
            rule.executionState = SequenceExecutionState.Underway;
#if LOG_SEQUENCE_EXECUTION
            procEnv.Recorder.WriteLine("Before executing sequence " + rule.Id + ": " + rule.Symbol);
#endif
            procEnv.Matched(matches, rule.Special);
            result = rule.Rewrite(procEnv, matches, match);
#if LOG_SEQUENCE_EXECUTION
            procEnv.Recorder.WriteLine("After executing sequence " + rule.Id + ": " + rule.Symbol + " result " + result);
#endif
            rule.executionState = result ? SequenceExecutionState.Success : SequenceExecutionState.Fail;
            procEnv.ExitingSequence(rule);
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

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            int transactionID = procEnv.TransactionManager.StartTransaction();
            int oldRewritesPerformed;

            if(procEnv.PerformanceInfo != null) oldRewritesPerformed = procEnv.PerformanceInfo.RewritesPerformed;
            else oldRewritesPerformed = -1;

            bool res = Seq.Apply(procEnv);

            if(res) procEnv.TransactionManager.Commit(transactionID);
            else
            {
                procEnv.TransactionManager.Rollback(transactionID);
                if(procEnv.PerformanceInfo != null)
                    procEnv.PerformanceInfo.RewritesPerformed = oldRewritesPerformed;
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

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceBacktrack copy = (SequenceBacktrack)MemberwiseClone();
            copy.Rule = (SequenceRuleCall)Rule.Copy(originalToCopy, procEnv);
            copy.Seq = Seq.Copy(originalToCopy, procEnv);
            copy.executionState = SequenceExecutionState.NotYet;
            return copy;
        }

        internal override void ReplaceSequenceDefinition(SequenceDefinition oldDef, SequenceDefinition newDef)
        {
            Seq.ReplaceSequenceDefinition(oldDef, newDef);
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            // first get all matches of the rule
            object[] parameters;
            if(Rule.ParamBindings.ArgumentExpressions.Length > 0)
            {
                parameters = Rule.ParamBindings.Arguments;
                for(int j = 0; j < Rule.ParamBindings.ArgumentExpressions.Length; j++)
                {
                    if(Rule.ParamBindings.ArgumentExpressions[j] != null)
                        parameters[j] = Rule.ParamBindings.ArgumentExpressions[j].Evaluate(procEnv);
                }
            }
            else parameters = null;

#if LOG_SEQUENCE_EXECUTION
            procEnv.Recorder.WriteLine("Matching backtrack all " + Rule.GetRuleCallString(procEnv));
#endif

            if(procEnv.PerformanceInfo != null) procEnv.PerformanceInfo.StartLocal();
            IMatches matches = Rule.ParamBindings.Action.Match(procEnv, procEnv.MaxMatches, parameters);
            if(procEnv.PerformanceInfo != null)
            {
                procEnv.PerformanceInfo.StopMatch();              // total match time does NOT include listeners anymore
                procEnv.PerformanceInfo.MatchesFound += matches.Count;
            }

            if(matches.Count == 0)
            {
                Rule.executionState = SequenceExecutionState.Fail;
                return false;
            }

#if LOG_SEQUENCE_EXECUTION
            for(int i = 0; i < matches.Count; ++i)
            {
                procEnv.Recorder.WriteLine("match " + i + ": " + MatchPrinter.ToString(matches.GetMatch(i), procEnv.Graph, ""));
            }
#endif

            // the rule might be called again in the sequence, overwriting the matches object of the action
            // normally it's safe to assume the rule is not called again until its matches were processed,
            // allowing for the one matches object memory optimization, but here we must clone to prevent bad side effect
            // TODO: optimization; if it's ensured the sequence doesn't call this action again, we can omit this, requires call analysis
            matches = matches.Clone();

            // apply the rule and the following sequence for every match found,
            // until the first rule and sequence execution succeeded
            // rolling back the changes of failing executions until then
            int matchesTried = 0;
            foreach(IMatch match in matches)
            {
                ++matchesTried;
#if LOG_SEQUENCE_EXECUTION
                procEnv.Recorder.WriteLine("Applying backtrack match " + matchesTried + "/" + matches.Count + " of " + Rule.GetRuleCallString(procEnv));
                procEnv.Recorder.WriteLine("match: " + MatchPrinter.ToString(match, procEnv.Graph, ""));
#endif

                // start a transaction
                int transactionID = procEnv.TransactionManager.StartTransaction();
                int oldRewritesPerformed = -1;

                if(procEnv.PerformanceInfo != null) oldRewritesPerformed = procEnv.PerformanceInfo.RewritesPerformed;

                procEnv.EnteringSequence(Rule);
                Rule.executionState = SequenceExecutionState.Underway;
#if LOG_SEQUENCE_EXECUTION
                procEnv.Recorder.WriteLine("Before executing sequence " + Rule.Id + ": " + Rule.Symbol);
#endif
                procEnv.Matched(matches, Rule.Special);
                bool result = Rule.Rewrite(procEnv, matches, match);
#if LOG_SEQUENCE_EXECUTION
                procEnv.Recorder.WriteLine("After executing sequence " + Rule.Id + ": " + Rule.Symbol + " result " + result);
#endif
                Rule.executionState = result ? SequenceExecutionState.Success : SequenceExecutionState.Fail;
                procEnv.ExitingSequence(Rule);

                // rule applied, now execute the sequence
                result = Seq.Apply(procEnv);

                // if sequence execution failed, roll the changes back and try the next match of the rule
                if(!result)
                {
                    procEnv.TransactionManager.Rollback(transactionID);
                    if(procEnv.PerformanceInfo != null)
                        procEnv.PerformanceInfo.RewritesPerformed = oldRewritesPerformed;
                    if(matchesTried < matches.Count)
                    {
                        procEnv.EndOfIteration(true, this);
                        Rule.ResetExecutionState();
                        Seq.ResetExecutionState();
                        continue;
                    }
                    else
                    {
                        // all matches tried, all failed later on -> end in fail
#if LOG_SEQUENCE_EXECUTION
                        procEnv.Recorder.WriteLine("Applying backtrack match exhausted " + Rule.GetRuleCallString(procEnv));
#endif
                        procEnv.EndOfIteration(false, this);
                        return false;
                    }
                }

                // if sequence execution succeeded, commit the changes so far and succeed
                procEnv.TransactionManager.Commit(transactionID);
                procEnv.EndOfIteration(false, this);
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
        public override string Symbol { get { return "<< " + Rule.Symbol + " ;; ... >>"; } }
    }

    public class SequencePause : SequenceUnary
    {
        public SequencePause(Sequence seq)
            : base(seq, SequenceType.Pause)
        {
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            procEnv.TransactionManager.Pause();

            bool res = Seq.Apply(procEnv);

            procEnv.TransactionManager.Resume();

            return res;
        }

        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "/ ... /"; } }
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

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceIfThenElse copy = (SequenceIfThenElse)MemberwiseClone();
            copy.Condition = Condition.Copy(originalToCopy, procEnv);
            copy.TrueCase = TrueCase.Copy(originalToCopy, procEnv);
            copy.FalseCase = FalseCase.Copy(originalToCopy, procEnv);
            copy.VariablesFallingOutOfScopeOnLeavingIf = new List<SequenceVariable>(VariablesFallingOutOfScopeOnLeavingIf.Count);
            foreach(SequenceVariable var in VariablesFallingOutOfScopeOnLeavingIf)
                copy.VariablesFallingOutOfScopeOnLeavingIf.Add(var.Copy(originalToCopy, procEnv));
            copy.VariablesFallingOutOfScopeOnLeavingTrueCase = new List<SequenceVariable>(VariablesFallingOutOfScopeOnLeavingTrueCase.Count);
            foreach(SequenceVariable var in VariablesFallingOutOfScopeOnLeavingTrueCase)
                copy.VariablesFallingOutOfScopeOnLeavingTrueCase.Add(var.Copy(originalToCopy, procEnv));
            copy.executionState = SequenceExecutionState.NotYet;
            return copy;
        }

        internal override void ReplaceSequenceDefinition(SequenceDefinition oldDef, SequenceDefinition newDef)
        {
            Condition.ReplaceSequenceDefinition(oldDef, newDef);
            TrueCase.ReplaceSequenceDefinition(oldDef, newDef);
            FalseCase.ReplaceSequenceDefinition(oldDef, newDef);
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            return Condition.Apply(procEnv) ? TrueCase.Apply(procEnv) : FalseCase.Apply(procEnv);
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

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceIfThen copy = (SequenceIfThen)MemberwiseClone();
            copy.Left = Left.Copy(originalToCopy, procEnv);
            copy.Right = Right.Copy(originalToCopy, procEnv);
            copy.VariablesFallingOutOfScopeOnLeavingIf = new List<SequenceVariable>(VariablesFallingOutOfScopeOnLeavingIf.Count);
            foreach(SequenceVariable var in VariablesFallingOutOfScopeOnLeavingIf)
                copy.VariablesFallingOutOfScopeOnLeavingIf.Add(var.Copy(originalToCopy, procEnv));
            copy.VariablesFallingOutOfScopeOnLeavingTrueCase = new List<SequenceVariable>(VariablesFallingOutOfScopeOnLeavingTrueCase.Count);
            foreach(SequenceVariable var in VariablesFallingOutOfScopeOnLeavingTrueCase)
                copy.VariablesFallingOutOfScopeOnLeavingTrueCase.Add(var.Copy(originalToCopy, procEnv));
            copy.executionState = SequenceExecutionState.NotYet;
            return copy;
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            return Left.Apply(procEnv) ? Right.Apply(procEnv) : true; // lazy implication
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

        public override void Check(SequenceCheckingEnvironment env)
        {
            if(Container == null)
            {
                if(Var.Type == "")
                {
                    throw new SequenceParserException(Var.Name, "a node or edge type", "statically unknown type");
                }
                if(TypesHelper.GetNodeOrEdgeType(Var.Type, env.Model) == null)
                {
                    throw new SequenceParserException(Var.Name, "a node or edge type", Var.Type);
                }
            }
            base.Check(env);
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceFor copy = (SequenceFor)MemberwiseClone();
            copy.Var = Var.Copy(originalToCopy, procEnv);
            if(VarDst!=null)
                copy.VarDst = VarDst.Copy(originalToCopy, procEnv);
            if(Container!=null)
                copy.Container = Container.Copy(originalToCopy, procEnv);
            copy.Seq = Seq.Copy(originalToCopy, procEnv);
            copy.VariablesFallingOutOfScopeOnLeavingFor = new List<SequenceVariable>(VariablesFallingOutOfScopeOnLeavingFor.Count);
            foreach(SequenceVariable var in VariablesFallingOutOfScopeOnLeavingFor)
                copy.VariablesFallingOutOfScopeOnLeavingFor.Add(var.Copy(originalToCopy, procEnv));
            copy.executionState = SequenceExecutionState.NotYet;
            return copy;
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            bool res = true;
            if(Container == null)
            {
                NodeType nodeType = TypesHelper.GetNodeType(Var.Type, procEnv.Graph.Model);
                if(nodeType!=null)
                {
                    bool first = true;
                    foreach(INode node in procEnv.Graph.GetCompatibleNodes(nodeType))
                    {
                        if(!first) procEnv.EndOfIteration(true, this);
                        Var.SetVariableValue(node, procEnv);
                        Seq.ResetExecutionState();
                        res &= Seq.Apply(procEnv);
                        first = false;
                    }
                    procEnv.EndOfIteration(false, this);
                }
                else
                {
                    EdgeType edgeType = TypesHelper.GetEdgeType(Var.Type, procEnv.Graph.Model);
                    bool first = true;
                    foreach(IEdge edge in procEnv.Graph.GetCompatibleEdges(edgeType))
                    {
                        if(!first) procEnv.EndOfIteration(true, this);
                        Var.SetVariableValue(edge, procEnv);
                        Seq.ResetExecutionState();
                        res &= Seq.Apply(procEnv);
                        first = false;
                    }
                    procEnv.EndOfIteration(false, this);
                }
            }
            else if(Container.GetVariableValue(procEnv) is IList)
            {
                IList array = (IList)Container.GetVariableValue(procEnv);
                bool first = true;
                for(int i = 0; i < array.Count; ++i)
                {
                    if(!first) procEnv.EndOfIteration(true, this);
                    if(VarDst != null)
                    {
                        Var.SetVariableValue(i, procEnv);
                        VarDst.SetVariableValue(array[i], procEnv);
                    }
                    else
                    {
                        Var.SetVariableValue(array[i], procEnv);
                    }
                    Seq.ResetExecutionState();
                    res &= Seq.Apply(procEnv);
                    first = false;
                }
                procEnv.EndOfIteration(false, this);
            }
            else
            {
                IDictionary setmap = (IDictionary)Container.GetVariableValue(procEnv);
                bool first = true;
                foreach(DictionaryEntry entry in setmap)
                {
                    if(!first) procEnv.EndOfIteration(true, this);
                    Var.SetVariableValue(entry.Key, procEnv);
                    if(VarDst != null)
                        VarDst.SetVariableValue(entry.Value, procEnv);
                    Seq.ResetExecutionState();
                    res &= Seq.Apply(procEnv);
                    first = false;
                }
                procEnv.EndOfIteration(false, this);
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
        public override string Symbol { get { return "for{"+Var.Name+(VarDst!=null?"->"+VarDst.Name:"")+(Container!=null?" in "+Container.Name:"")+"; ...}"; } }
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

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            throw new Exception("Can't apply sequence definition like a normal sequence");
        }

        /// <summary>
        /// Applies this sequence.
        /// </summary>
        /// <param name="sequenceInvocation">Sequence invocation object for this sequence application,
        ///     containing the input parameter sources and output parameter targets</param>
        /// <param name="procEnv">The graph processing environment on which this sequence is to be applied.
        ///     Contains especially the graph on which this sequence is to be applied.
        ///     The rules will only be chosen during the Sequence object instantiation, so
        ///     exchanging rules will have no effect for already existing Sequence objects.</param>
        /// <param name="env">The execution environment giving access to the names and user interface (null if not available)</param>
        /// <returns>True, iff the sequence succeeded</returns>
        public abstract bool Apply(SequenceInvocationParameterBindings sequenceInvocation,
            IGraphProcessingEnvironment procEnv);

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

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceDefinitionInterpreted copy = (SequenceDefinitionInterpreted)MemberwiseClone();
            copy.InputVariables = new SequenceVariable[InputVariables.Length];
            for(int i = 0; i < InputVariables.Length; ++i)
                copy.InputVariables[i] = InputVariables[i].Copy(originalToCopy, procEnv);
            copy.OutputVariables = new SequenceVariable[OutputVariables.Length];
            for(int i = 0; i < OutputVariables.Length; ++i)
                copy.OutputVariables[i] = OutputVariables[i].Copy(originalToCopy, procEnv);
            copy.Seq = Seq.Copy(originalToCopy, procEnv);
            copy.executionState = SequenceExecutionState.NotYet;
            return copy;
        }

        internal override void ReplaceSequenceDefinition(SequenceDefinition oldDef, SequenceDefinition newDef)
        {
            Seq.ReplaceSequenceDefinition(oldDef, newDef);
        }

        public override bool Apply(SequenceInvocationParameterBindings sequenceInvocation,
            IGraphProcessingEnvironment procEnv)
        {
            // If this sequence definition is currently executed
            // we must copy it and use the copy in its place
            // to prevent state corruption.
            if(executionState == SequenceExecutionState.Underway)
            {
                return ApplyCopy(sequenceInvocation, procEnv);
            }

            procEnv.EnteringSequence(this);
            executionState = SequenceExecutionState.Underway;
#if LOG_SEQUENCE_EXECUTION
            procEnv.Recorder.WriteLine("Before executing sequence definition " + Id + ": " + Symbol);
#endif
            bool res = ApplyImpl(sequenceInvocation, procEnv);
#if LOG_SEQUENCE_EXECUTION
            procEnv.Recorder.WriteLine("After executing sequence definition " + Id + ": " + Symbol + " result " + res);
#endif
            executionState = res ? SequenceExecutionState.Success : SequenceExecutionState.Fail;

            procEnv.EndOfIteration(false, this);

            procEnv.ExitingSequence(this);

            ResetExecutionState(); // state is shown by call, we don't exist any more for the debugger

            return res;
        }

        // creates or reuses a copy and applies the copy
        protected bool ApplyCopy(SequenceInvocationParameterBindings sequenceInvocation,
            IGraphProcessingEnvironment procEnv)
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
                seqCopy = (SequenceDefinition)Copy(originalToCopy, procEnv);
            }
            sequenceInvocation.SequenceDef = seqCopy;
            bool success = seqCopy.Apply(sequenceInvocation, procEnv);
            sequenceInvocation.SequenceDef = this;
            if(!nameToCopies.ContainsKey(SequenceName))
                nameToCopies.Add(SequenceName, new Stack<SequenceDefinition>());
            nameToCopies[SequenceName].Push(seqCopy);
            return success;
        }

        // applies the sequence of/in the sequence definition
        protected bool ApplyImpl(SequenceInvocationParameterBindings sequenceInvocation,
            IGraphProcessingEnvironment procEnv)
        {
            if(sequenceInvocation.ArgumentExpressions.Length != InputVariables.Length)
                throw new Exception("Number of input parameters given and expected differ for " + Symbol);
            if(sequenceInvocation.ReturnVars.Length != OutputVariables.Length)
                throw new Exception("Number of output parameters given and expected differ for " + Symbol);

            // prefill the local input variables with the invocation values, read from parameter variables of the caller
            for(int i=0; i<sequenceInvocation.ArgumentExpressions.Length; ++i)
            {
                if(sequenceInvocation.ArgumentExpressions[i] != null)
                    InputVariables[i].SetVariableValue(sequenceInvocation.ArgumentExpressions[i].Evaluate(procEnv), procEnv);
                else
                    InputVariables[i].SetVariableValue(sequenceInvocation.Arguments[i], procEnv);
            }

            bool success = Seq.Apply(procEnv);

            if(success)
            {
                // postfill the return-to variables of the caller with the return values, read from the local output variables
                for(int i = 0; i < sequenceInvocation.ReturnVars.Length; i++)
                    sequenceInvocation.ReturnVars[i].SetVariableValue(OutputVariables[i].GetVariableValue(procEnv), procEnv);
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

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            throw new Exception("Copy not supported on compiled sequences");
        }

        internal override void ReplaceSequenceDefinition(SequenceDefinition oldDef, SequenceDefinition newDef)
        {
            throw new Exception("ReplaceSequenceDefinition not supported on compiled sequences");
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            throw new Exception("Can't apply compiled sequence definition like a normal sequence");
        }

        public abstract override bool Apply(SequenceInvocationParameterBindings sequenceInvocation,
            IGraphProcessingEnvironment procEnv);

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

        public override void Check(SequenceCheckingEnvironment env)
        {
            env.CheckRuleCallRuleAllCallSequenceCall(this);
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceSequenceCall copy = (SequenceSequenceCall)MemberwiseClone();
            copy.ParamBindings = ParamBindings.Copy(originalToCopy, procEnv);
            copy.executionState = SequenceExecutionState.NotYet;
            return copy;
        }

        internal override void ReplaceSequenceDefinition(SequenceDefinition oldDef, SequenceDefinition newDef)
        {
            if(ParamBindings.SequenceDef==oldDef)
                ParamBindings.SequenceDef = newDef;
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            SequenceDefinition seqDef = ParamBindings.SequenceDef;
#if LOG_SEQUENCE_EXECUTION
            procEnv.Recorder.WriteLine("Applying sequence " + GetSequenceCallString(procEnv));
#endif

            bool res = seqDef.Apply(ParamBindings, procEnv);

#if LOG_SEQUENCE_EXECUTION
            if(res)
            {
                procEnv.Recorder.WriteLine("Applied sequence " + Symbol + " successfully");
                procEnv.Recorder.Flush();
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

        public String GetSequenceCallString(IGraphProcessingEnvironment procEnv)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(ParamBindings.SequenceDef.SequenceName);
            if(ParamBindings.ArgumentExpressions.Length > 0)
            {
                sb.Append("(");
                for(int i = 0; i < ParamBindings.ArgumentExpressions.Length; ++i)
                {
                    if(ParamBindings.ArgumentExpressions[i] != null)
                        sb.Append(DictionaryListHelper.ToStringAutomatic(ParamBindings.ArgumentExpressions[i].Evaluate(procEnv), procEnv.Graph));
                    else
                        sb.Append(ParamBindings.Arguments[i] != null ? ParamBindings.Arguments[i] : "null");
                    if(i != ParamBindings.ArgumentExpressions.Length - 1) sb.Append(",");
                }
                sb.Append(")");
            }
            return sb.ToString();
        }

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
            if(ParamBindings.ArgumentExpressions.Length > 0)
            {
                sb.Append("(");
                for(int i = 0; i < ParamBindings.ArgumentExpressions.Length; ++i)
                {
                    if(ParamBindings.ArgumentExpressions[i] != null)
                        sb.Append(ParamBindings.ArgumentExpressions[i].Symbol);
                    else
                        sb.Append(ParamBindings.Arguments[i] != null ? ParamBindings.Arguments[i] : "null");
                    if(i != ParamBindings.ArgumentExpressions.Length - 1) sb.Append(",");
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

    public class SequenceBooleanComputation : SequenceSpecial
    {
        public SequenceComputation Computation;
        public List<SequenceVariable> VariablesFallingOutOfScopeOnLeavingComputation;

        public SequenceBooleanComputation(SequenceComputation comp, List<SequenceVariable> variablesFallingOutOfScopeOnLeavingComputation, bool special)
            : base(special, SequenceType.BooleanComputation)
        {
            Computation = comp;
            VariablesFallingOutOfScopeOnLeavingComputation = variablesFallingOutOfScopeOnLeavingComputation;
            if(VariablesFallingOutOfScopeOnLeavingComputation == null)
                VariablesFallingOutOfScopeOnLeavingComputation = new List<SequenceVariable>();
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            Computation.Check(env);
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceBooleanComputation copy = (SequenceBooleanComputation)MemberwiseClone();
            copy.Computation = Computation.Copy(originalToCopy, procEnv);
            copy.VariablesFallingOutOfScopeOnLeavingComputation = new List<SequenceVariable>(VariablesFallingOutOfScopeOnLeavingComputation.Count);
            foreach(SequenceVariable var in VariablesFallingOutOfScopeOnLeavingComputation)
                copy.VariablesFallingOutOfScopeOnLeavingComputation.Add(var.Copy(originalToCopy, procEnv));
            return copy;
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            object val = Computation.Execute(procEnv);
            if(Computation.ReturnsValue)
                return !TypesHelper.IsDefaultValue(val);
            else
                return true;
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables, Sequence target)
        {
            Computation.GetLocalVariables(variables);
            foreach(SequenceVariable seqVar in VariablesFallingOutOfScopeOnLeavingComputation)
                variables.Remove(seqVar);
            return this == target;
        }

        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return Special ? "%{ " + Computation.Symbol + " }" : "{ " + Computation.Symbol + " }"; } }
    }
}

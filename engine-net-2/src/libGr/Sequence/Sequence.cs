/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

//#define LOG_SEQUENCE_EXECUTION

using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace de.unika.ipd.grGen.libGr
{
    // todo: container constructors as sequence expression?
    // todo: (optional) execution environment for InvocationParameterBindings, so parameter evaluation can access named graph?

    /// <summary>
    /// Specifies the actual subtype used for a Sequence.
    /// A new sequence type -> you must add the corresponding class down below,
    /// and adapt the lgspSequenceGenerator and the Debugger.
    /// </summary>
    public enum SequenceType
    {
        ThenLeft, ThenRight, LazyOr, LazyAnd, StrictOr, Xor, StrictAnd, Not,
        LazyOrAll, LazyAndAll, StrictOrAll, StrictAndAll,
        WeightedOne, SomeFromSet, MultiRuleAllCall, MultiRulePrefixedSequence,
        IfThenElse, IfThen,
        ForContainer, ForMatch, ForIntegerRange,
        ForIndexAccessEquality, ForIndexAccessOrdering,
        ForIncidentEdges, ForIncomingEdges, ForOutgoingEdges,
        ForAdjacentNodes, ForAdjacentNodesViaIncoming, ForAdjacentNodesViaOutgoing,
        ForReachableNodes, ForReachableNodesViaIncoming, ForReachableNodesViaOutgoing,
        ForReachableEdges, ForReachableEdgesViaIncoming, ForReachableEdgesViaOutgoing,
        ForBoundedReachableNodes, ForBoundedReachableNodesViaIncoming, ForBoundedReachableNodesViaOutgoing,
        ForBoundedReachableEdges, ForBoundedReachableEdgesViaIncoming, ForBoundedReachableEdgesViaOutgoing,
        ForNodes, ForEdges,
        Transaction, Backtrack, MultiBacktrack, MultiSequenceBacktrack, Pause,
        IterationMin, IterationMinMax,
        RuleCall, RuleAllCall, RuleCountAllCall, RulePrefixedSequence,
        AssignSequenceResultToVar, OrAssignSequenceResultToVar, AndAssignSequenceResultToVar,
        AssignUserInputToVar, AssignRandomIntToVar, AssignRandomDoubleToVar, // needed as sequence because of debugger integration
        DeclareVariable, AssignConstToVar, AssignContainerConstructorToVar, AssignObjectConstructorToVar, AssignVarToVar, // needed as sequence to allow variable declaration and initialization in sequence scope (VarToVar for embedded sequences, assigning rule elements to a variable)
        SequenceDefinitionInterpreted, SequenceDefinitionCompiled, SequenceCall,
        ExecuteInSubgraph,
        ParallelExecute, ParallelArrayExecute,
        Lock,
        BooleanComputation,
        Dummy
    }

    public enum RelOpDirection
    {
        Undefined,
        Smaller,
        SmallerEqual,
        Greater,
        GreaterEqual
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
        public readonly SequenceType SequenceType;

        /// <summary>
        /// Initializes a new Sequence object with the given sequence type.
        /// </summary>
        /// <param name="seqType">The sequence type.</param>
        protected Sequence(SequenceType seqType)
            : base()
        {
            SequenceType = seqType;
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="that">The sequence to be copied.</param>
        protected Sequence(Sequence that)
            : base(that)
        {
            SequenceType = that.SequenceType;
        }

        public override bool HasSequenceType(SequenceType sequenceType)
        {
            return SequenceType == sequenceType;
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
        /// <param name="procEnv">The graph processing environment</param>
        /// <returns>The copy of the sequence</returns>
        internal abstract Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv);

        /// <summary>
        /// Checks the sequence for errors utilizing the given checking environment
        /// reports them by exception
        /// default behavior: check all the children 
        /// </summary>
        public override void Check(SequenceCheckingEnvironment env)
        {
            foreach(Sequence childSeq in Children)
            {
                childSeq.Check(env);
            }
        }

        /// <summary>
        /// Returns the type of the sequence, which is "boolean"
        /// </summary>
        public override string Type(SequenceCheckingEnvironment env)
        {
            return "boolean";
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
            FireEnteringSequenceEvent(procEnv);
            executionState = SequenceExecutionState.Underway;
#if LOG_SEQUENCE_EXECUTION
            procEnv.Recorder.WriteLine("Before executing sequence " + Id + ": " + Symbol);
#endif
            bool res = ApplyImpl(procEnv);
#if LOG_SEQUENCE_EXECUTION
            procEnv.Recorder.WriteLine("After executing sequence " + Id + ": " + Symbol + " result " + res);
#endif
            executionState = res ? SequenceExecutionState.Success : SequenceExecutionState.Fail;
            FireExitingSequenceEvent(procEnv);
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
            {
                childSeq.ResetExecutionState();
            }
        }

        /// <summary>
        /// Enumerates all child sequence objects
        /// </summary>
        public abstract IEnumerable<Sequence> Children { get; }

        public override IEnumerable<SequenceBase> ChildrenBase
        {
            get
            {
                foreach(Sequence child in Children)
                {
                    yield return child;
                }
            }
        }
    }


    /// <summary>
    /// A Sequence with a Special flag.
    /// </summary>
    public abstract class SequenceSpecial : Sequence, ISequenceSpecial
    {
        public bool special;

        public bool Special
        {
            get { return special; }
            set { special = value; }
        }

        /// <summary>
        /// Initializes a new instance of the SequenceSpecial class.
        /// </summary>
        /// <param name="seqType">The sequence type.</param>
        /// <param name="special">The initial value for the "Special" flag.</param>
        protected SequenceSpecial(SequenceType seqType, bool special)
            : base(seqType)
        {
            Special = special;
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="that">The sequence special to be copied.</param>
        public SequenceSpecial(SequenceSpecial that)
            : base(that)
        {
            Special = that.Special;
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
        public readonly Sequence Seq;

        protected SequenceUnary(SequenceType seqType, Sequence seq) : base(seqType)
        {
            Seq = seq;
        }

        protected SequenceUnary(SequenceUnary that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            Seq = that.Seq.Copy(originalToCopy, procEnv);
        }

        public override SequenceBase GetCurrentlyExecutedSequenceBase()
        {
            if(Seq.GetCurrentlyExecutedSequenceBase() != null)
                return Seq.GetCurrentlyExecutedSequenceBase();
            if(executionState == SequenceExecutionState.Underway)
                return this;
            return null;
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables, 
            List<SequenceExpressionConstructor> constructors, SequenceBase target)
        {
            if(Seq.GetLocalVariables(variables, constructors, target))
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
        public readonly Sequence Left;
        public readonly Sequence Right;
        public bool Random { get { return random; } set { random = value; } }
        private bool random;
        public bool Choice { get { return choice; } set { choice = value; } }
        private bool choice;

        protected SequenceBinary(SequenceType seqType, Sequence left, Sequence right, bool random, bool choice)
            : base(seqType)
        {
            Left = left;
            Right = right;
            this.random = random;
            this.choice = choice;
        }

        protected SequenceBinary(SequenceBinary that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            Left = that.Left.Copy(originalToCopy, procEnv);
            Right = that.Right.Copy(originalToCopy, procEnv);
            random = that.random;
            choice = that.choice;
        }

        public override SequenceBase GetCurrentlyExecutedSequenceBase()
        {
            if(Left.GetCurrentlyExecutedSequenceBase() != null)
                return Left.GetCurrentlyExecutedSequenceBase();
            if(Right.GetCurrentlyExecutedSequenceBase() != null)
                return Right.GetCurrentlyExecutedSequenceBase();
            if(executionState == SequenceExecutionState.Underway)
                return this;
            return null;
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors, SequenceBase target)
        {
            if(Left.GetLocalVariables(variables, constructors, target))
                return true;
            if(Right.GetLocalVariables(variables, constructors, target))
                return true;
            return this == target;
        }

        public override IEnumerable<Sequence> Children
        {
            get
            {
                yield return Left;
                yield return Right;
            }
        }

        public abstract String OperatorSymbol { get; }

        public override string Symbol
        {
            get { return Left.Symbol + OperatorSymbol + Right.Symbol; }
        }
    }

    /// <summary>
    /// A sequence consisting of a list of subsequences.
    /// Decision on order of execution always by random, by user choice possible.
    /// </summary>
    public abstract class SequenceGeneralNAry : Sequence, SequenceRandomChoice
    {
        public readonly List<Sequence> Sequences;
        public virtual bool Random { get { return true; } set { throw new Exception("can't change Random on SequenceNAry"); } }
        public bool Choice { get { return choice; } set { choice = value; } }
        bool choice;
        public bool Skip { get { return skip; } set { skip = value; } }
        bool skip;

        protected SequenceGeneralNAry(SequenceType seqType, List<Sequence> sequences, bool choice)
            : base(seqType)
        {
            Sequences = sequences;
            this.choice = choice;
        }

        protected SequenceGeneralNAry(SequenceGeneralNAry that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            Sequences = new List<Sequence>();
            foreach(Sequence seq in that.Sequences)
            {
                Sequences.Add(seq.Copy(originalToCopy, procEnv));
            }
            choice = that.choice;
        }

        public override SequenceBase GetCurrentlyExecutedSequenceBase()
        {
            foreach(Sequence seq in Sequences)
            {
                if(seq.GetCurrentlyExecutedSequenceBase() != null)
                    return seq.GetCurrentlyExecutedSequenceBase();
            }
            if(executionState == SequenceExecutionState.Underway)
                return this;
            return null;
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors, SequenceBase target)
        {
            foreach(Sequence seq in Sequences)
            {
                if(seq.GetLocalVariables(variables, constructors, target))
                    return true;
            }
            return this == target;
        }

        public override IEnumerable<Sequence> Children
        {
            get
            {
                foreach(Sequence seq in Sequences)
                {
                    yield return seq;
                }
            }
        }
    }

    /// <summary>
    /// A sequence consisting of a list of subsequences.
    /// Decision on order of execution always by random, by user choice possible.
    /// </summary>
    public abstract class SequenceNAry : SequenceGeneralNAry
    {
        protected SequenceNAry(SequenceType seqType, List<Sequence> sequences, bool choice)
            : base(seqType, sequences, choice)
        {
        }

        protected SequenceNAry(SequenceNAry that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
        }

        public abstract string OperatorSymbol { get; }

        public override string Symbol
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(OperatorSymbol);
                sb.Append("(");
                bool first = true;
                foreach(Sequence seq in Sequences)
                {
                    if(first)
                        first = false;
                    else
                        sb.Append(",");
                    sb.Append(seq);
                }
                sb.Append(")");
                return sb.ToString();
            }
        }
    }

    /// <summary>
    /// A sequence which assigns something to a destination variable.
    /// </summary>
    public abstract class SequenceAssignToVar : Sequence
    {
        public readonly SequenceVariable DestVar;

        protected SequenceAssignToVar(SequenceType seqType, SequenceVariable destVar)
            : base(seqType)
        {
            DestVar = destVar;
        }

        protected SequenceAssignToVar(SequenceAssignToVar that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            DestVar = that.DestVar.Copy(originalToCopy, procEnv);
        }

        protected bool Assign(object value, IGraphProcessingEnvironment procEnv)
        {
            DestVar.SetVariableValue(value, procEnv);
            return true;
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors, SequenceBase target)
        {
            DestVar.GetLocalVariables(variables);
            return this == target;
        }

        public override IEnumerable<Sequence> Children
        {
            get { yield break; } // nearly always no children
        }

        public override int Precedence
        {
            get { return 8; } // nearly always a top prio assignment factor
        }
    }


    public class SequenceThenLeft : SequenceBinary
    {
        public SequenceThenLeft(Sequence left, Sequence right, bool random, bool choice)
            : base(SequenceType.ThenLeft, left, right, random, choice)
        {
        }

        protected SequenceThenLeft(SequenceThenLeft that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceThenLeft(this, originalToCopy, procEnv);
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            bool res;
            int direction = 0;
            if(Random)
                direction = randomGenerator.Next(2);
            if(Choice)
                direction = procEnv.UserProxy.ChooseDirection(direction, this);
            if(direction == 1)
            {
                Right.Apply(procEnv);
                res = Left.Apply(procEnv);
            }
            else
            {
                res = Left.Apply(procEnv);
                Right.Apply(procEnv);
            }
            return res;
        }

        public override int Precedence
        {
            get { return 0; }
        }

        public override string OperatorSymbol
        {
            get
            {
                string prefix = Random ? (Choice ? "$%" : "$") : "";
                return prefix + "<;";
            }
        }
    }

    public class SequenceThenRight : SequenceBinary
    {
        public SequenceThenRight(Sequence left, Sequence right, bool random, bool choice)
            : base(SequenceType.ThenRight, left, right, random, choice)
        {
        }

        protected SequenceThenRight(SequenceThenRight that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceThenRight(this, originalToCopy, procEnv);
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            bool res;
            int direction = 0;
            if(Random)
                direction = randomGenerator.Next(2);
            if(Choice)
                direction = procEnv.UserProxy.ChooseDirection(direction, this);
            if(direction == 1)
            {
                res = Right.Apply(procEnv);
                Left.Apply(procEnv);
            }
            else
            {
                Left.Apply(procEnv);
                res = Right.Apply(procEnv);
            }
            return res;
        }

        public override int Precedence
        {
            get { return 0; }
        }

        public override string OperatorSymbol
        {
            get
            {
                string prefix = Random ? (Choice ? "$%" : "$") : "";
                return prefix + ";>";
            }
        }
    }

    public class SequenceLazyOr : SequenceBinary
    {
        public SequenceLazyOr(Sequence left, Sequence right, bool random, bool choice)
            : base(SequenceType.LazyOr, left, right, random, choice)
        {
        }

        protected SequenceLazyOr(SequenceLazyOr that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceLazyOr(this, originalToCopy, procEnv);
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            int direction = 0;
            if(Random)
                direction = randomGenerator.Next(2);
            if(Choice)
                direction = procEnv.UserProxy.ChooseDirection(direction, this);
            if(direction == 1)
                return Right.Apply(procEnv) || Left.Apply(procEnv);
            else
                return Left.Apply(procEnv) || Right.Apply(procEnv);
        }

        public override int Precedence
        {
            get { return 1; }
        }

        public override string OperatorSymbol
        {
            get
            {
                string prefix = Random ? (Choice ? "$%" : "$") : "";
                return prefix + "||";
            }
        }
    }

    public class SequenceLazyAnd : SequenceBinary
    {
        public SequenceLazyAnd(Sequence left, Sequence right, bool random, bool choice)
            : base(SequenceType.LazyAnd, left, right, random, choice)
        {
        }

        protected SequenceLazyAnd(SequenceLazyAnd that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceLazyAnd(this, originalToCopy, procEnv);
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            int direction = 0;
            if(Random)
                direction = randomGenerator.Next(2);
            if(Choice)
                direction = procEnv.UserProxy.ChooseDirection(direction, this);
            if(direction == 1)
                return Right.Apply(procEnv) && Left.Apply(procEnv);
            else
                return Left.Apply(procEnv) && Right.Apply(procEnv);
        }

        public override int Precedence
        {
            get { return 2; }
        }

        public override string OperatorSymbol
        {
            get
            {
                string prefix = Random ? (Choice ? "$%" : "$") : "";
                return prefix + "&&";
            }
        }
    }

    public class SequenceStrictOr : SequenceBinary
    {
        public SequenceStrictOr(Sequence left, Sequence right, bool random, bool choice)
            : base(SequenceType.StrictOr, left, right, random, choice)
        {
        }

        protected SequenceStrictOr(SequenceStrictOr that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceStrictOr(this, originalToCopy, procEnv);
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            int direction = 0;
            if(Random)
                direction = randomGenerator.Next(2);
            if(Choice)
                direction = procEnv.UserProxy.ChooseDirection(direction, this);
            if(direction == 1)
                return Right.Apply(procEnv) | Left.Apply(procEnv);
            else
                return Left.Apply(procEnv) | Right.Apply(procEnv);
        }

        public override int Precedence
        {
            get { return 3; }
        }

        public override string OperatorSymbol
        {
            get
            {
                string prefix = Random ? (Choice ? "$%" : "$") : "";
                return prefix + "|";
            }
        }
    }

    public class SequenceXor : SequenceBinary
    {
        public SequenceXor(Sequence left, Sequence right, bool random, bool choice)
            : base(SequenceType.Xor, left, right, random, choice)
        {
        }

        protected SequenceXor(SequenceXor that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceXor(this, originalToCopy, procEnv);
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            int direction = 0;
            if(Random)
                direction = randomGenerator.Next(2);
            if(Choice)
                direction = procEnv.UserProxy.ChooseDirection(direction, this);
            if(direction == 1)
                return Right.Apply(procEnv) ^ Left.Apply(procEnv);
            else
                return Left.Apply(procEnv) ^ Right.Apply(procEnv);
        }

        public override int Precedence
        {
            get { return 4; }
        }

        public override string OperatorSymbol
        {
            get
            {
                string prefix = Random ? (Choice ? "$%" : "$") : "";
                return prefix + "^";
            }
        }
    }

    public class SequenceStrictAnd : SequenceBinary
    {
        public SequenceStrictAnd(Sequence left, Sequence right, bool random, bool choice)
            : base(SequenceType.StrictAnd, left, right, random, choice)
        {
        }

        protected SequenceStrictAnd(SequenceStrictAnd that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceStrictAnd(this, originalToCopy, procEnv);
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            int direction = 0;
            if(Random)
                direction = randomGenerator.Next(2);
            if(Choice)
                direction = procEnv.UserProxy.ChooseDirection(direction, this);
            if(direction == 1)
                return Right.Apply(procEnv) & Left.Apply(procEnv);
            else
                return Left.Apply(procEnv) & Right.Apply(procEnv);
        }

        public override int Precedence
        {
            get { return 5; }
        }

        public override string OperatorSymbol
        {
            get
            {
                string prefix = Random ? (Choice ? "$%" : "$") : "";
                return prefix + "&";
            }
        }
    }

    public class SequenceNot : SequenceUnary
    {
        public SequenceNot(Sequence seq) : base(SequenceType.Not, seq)
        {
        }

        protected SequenceNot(SequenceNot that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceNot(this, originalToCopy, procEnv);
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            return !Seq.Apply(procEnv);
        }

        public override int Precedence
        {
            get { return 6; }
        }

        public string OperatorSymbol
        {
            get { return "!"; }
        }

        public override string Symbol
        {
            get { return OperatorSymbol + Seq.Symbol; }
        }
    }

    public class SequenceIterationMin : SequenceUnary
    {
        public readonly SequenceExpression MinExpr;

        public SequenceIterationMin(Sequence seq, SequenceExpression minExpr) : base(SequenceType.IterationMin, seq)
        {
            MinExpr = minExpr;
        }

        protected SequenceIterationMin(SequenceIterationMin that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
            MinExpr = that.MinExpr.CopyExpression(originalToCopy, procEnv);
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceIterationMin(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env);

            if(!TypesHelper.IsSameOrSubtype(MinExpr.Type(env), "int", env.Model))
                throw new SequenceParserExceptionTypeMismatch(Symbol, "int", MinExpr.Type(env));
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            int min = (int)MinExpr.Evaluate(procEnv);
            if(min < 0)
                throw new Exception("Loop iteration lower bound must be larger or equal 0");
            int i = 0;
            while(Seq.Apply(procEnv))
            {
                procEnv.EndOfIteration(true, this);
                Seq.ResetExecutionState();
                ++i;
            }
            procEnv.EndOfIteration(false, this);
            return i >= min;
        }

        public override int Precedence
        {
            get { return 7; }
        }

        public override string Symbol
        {
            get { return "[" + MinExpr.Symbol + ":*]"; }
        }
    }

    public class SequenceIterationMinMax : SequenceUnary
    {
        public readonly SequenceExpression MinExpr;
        public readonly SequenceExpression MaxExpr;

        public SequenceIterationMinMax(Sequence seq, SequenceExpression minExpr, SequenceExpression maxExpr) : base(SequenceType.IterationMinMax, seq)
        {
            MinExpr = minExpr;
            MaxExpr = maxExpr;
        }

        protected SequenceIterationMinMax(SequenceIterationMinMax that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
            MinExpr = that.MinExpr.CopyExpression(originalToCopy, procEnv);
            MaxExpr = that.MaxExpr.CopyExpression(originalToCopy, procEnv);
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceIterationMinMax(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env);

            if(!TypesHelper.IsSameOrSubtype(MinExpr.Type(env), "int", env.Model))
                throw new SequenceParserExceptionTypeMismatch(Symbol, "int", MinExpr.Type(env));
            if(!TypesHelper.IsSameOrSubtype(MaxExpr.Type(env), "int", env.Model))
                throw new SequenceParserExceptionTypeMismatch(Symbol, "int", MaxExpr.Type(env));
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            int min = (int)MinExpr.Evaluate(procEnv);
            if(min < 0)
                throw new Exception("Loop iteration lower bound must be larger or equal 0");
            int max = (int)MaxExpr.Evaluate(procEnv);
            if(min > max)
                throw new Exception("Loop iteration upper bound must be larger or equal lower bound");
            int i;
            bool first = true;
            for(i = 0; i < max; ++i)
            {
                if(!first)
                    procEnv.EndOfIteration(true, this);
                Seq.ResetExecutionState();
                if(!Seq.Apply(procEnv))
                    break;
                first = false;
            }
            procEnv.EndOfIteration(false, this);
            return i >= min;
        }

        public override int Precedence
        {
            get { return 7; }
        }

        public override string Symbol
        {
            get { return "[" + MinExpr.Symbol + ":" + MaxExpr.Symbol + "]"; }
        }
    }

    public abstract class SequenceFilterCallBase : FilterInvocation
    {
        public abstract String Name { get; }

        public abstract String Package { get; }

        public abstract String PackagePrefixedName { get; }
    }

    public abstract class SequenceFilterCall : SequenceFilterCallBase
    {
        public IFilter Filter; // the filter called, set after resoving, if failed null

        /// <summary>
        /// An array of expressions used to compute the input arguments for a filter function or auto-supplied filter.
        /// It must have the same length as Arguments in the filter call object.
        /// The according entry in Arguments is filled with the evaluation result of the expression.
        /// </summary>
        public readonly SequenceExpression[] ArgumentExpressions;


        public override String Name
        {
            get { return Filter.Name; }
        }

        public override String Package
        {
            get { return Filter.Package; }
        }

        public override String PackagePrefixedName
        {
            get { return Filter.PackagePrefixedName; }
        }


        protected SequenceFilterCall(IFilter filter, SequenceExpression[] argExprs)
        {
            Filter = filter;
            ArgumentExpressions = argExprs;
        }
    }

    public class SequenceFilterCallInterpreted : SequenceFilterCall
    {
        public MatchClassFilterer MatchClass; // only set in case of a match class filter call

        public FilterCallWithArguments FilterCall;

        public SequenceFilterCallInterpreted(/*IAction action, */IFilter filter, SequenceExpression[] argExprs)
            : base(filter, argExprs)
        {
            FilterCall = new FilterCallWithArguments(filter.PackagePrefixedName, argExprs.Length);
        }

        public SequenceFilterCallInterpreted(MatchClassFilterer matchClass, IFilter filter, SequenceExpression[] argExprs)
            : base(filter, argExprs)
        {
            MatchClass = matchClass;
            FilterCall = new FilterCallWithArguments(filter.PackagePrefixedName, argExprs.Length);
        }

        public void Execute(IGraphProcessingEnvironment procEnv, IAction action, IMatches matches)
        {
            for(int i = 0; i < ArgumentExpressions.Length; ++i)
            {
                FilterCall.Arguments[i] = ArgumentExpressions[i].Evaluate(procEnv);
            }
            action.Filter(procEnv, matches, FilterCall);
        }

        public void Execute(IGraphProcessingEnvironment procEnv, List<IMatch> matchList)
        {
            for(int i = 0; i < ArgumentExpressions.Length; ++i)
            {
                FilterCall.Arguments[i] = ArgumentExpressions[i].Evaluate(procEnv);
            }
            MatchClass.Filter(procEnv, matchList, FilterCall);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if(MatchClass != null)
                sb.Append(MatchClass.info.PackagePrefixedName + ".");
            sb.Append(PackagePrefixedName);
            sb.Append("(");
            for(int i = 0; i < ArgumentExpressions.Length; ++i)
            {
                sb.Append(ArgumentExpressions[i].Symbol);
            }
            sb.Append(")");
            return sb.ToString();
        }
    }

    /// <summary>
    /// A sequence representing a filter call at compile time (of an action filter or a match class filter).
    /// It specifies the filter and potential arguments.
    /// Only used internally for code generation.
    /// </summary>
    public class SequenceFilterCallCompiled : SequenceFilterCall
    {
        /// <summary>
        /// null if this is a single-rule filter, otherwise the match class of the filter.
        /// </summary>
        public readonly String MatchClassName;

        /// <summary>
        /// null if the match class is global, otherwise the (resolved) package the match class is contained in.
        /// </summary>
        public String MatchClassPackage;

        /// <summary>
        /// The name of the match class, prefixed by the (resolved) package it is contained in (separated by a double colon), if it is contained in a package.
        /// </summary>
        public String MatchClassPackagePrefixedName;

        public SequenceFilterCallCompiled(IFilter filter, SequenceExpression[] argExprs)
            : base(filter, argExprs)
        {
            MatchClassName = null;
            MatchClassPackage = null;
            MatchClassPackagePrefixedName = null;
        }

        public SequenceFilterCallCompiled(String matchClassName, String matchClassPackage, String matchClassPackagePrefixedName,
            IFilter filter, SequenceExpression[] argExprs)
            : base(filter, argExprs)
        {
            MatchClassName = matchClassName;
            MatchClassPackage = matchClassPackage;
            MatchClassPackagePrefixedName = matchClassPackagePrefixedName;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if(MatchClassName != null)
                sb.Append(MatchClassPackagePrefixedName + ".");
            sb.Append(PackagePrefixedName);
            sb.Append("(");
            for(int i = 0; i < ArgumentExpressions.Length; ++i)
            {
                sb.Append(ArgumentExpressions[i].Symbol);
            }
            sb.Append(")");
            return sb.ToString();
        }
    }

    public class SequenceFilterCallLambdaExpression : SequenceFilterCallBase
    {
        public FilterCallWithLambdaExpression FilterCall;

        public override String Name
        {
            get { return FilterCall.PackagePrefixedName; }
        }

        public override String Package
        {
            get { return ""; } // todo: maybe null
        }

        public override String PackagePrefixedName
        {
            get { return FilterCall.PackagePrefixedName; }
        }

        public int Id { get { return id; } }

        protected readonly int id;

        protected static int idSource = 0;

        public SequenceFilterCallLambdaExpression(String filterBase, String entity,
            SequenceVariable arrayAccess, SequenceVariable index, SequenceVariable element, SequenceExpression lambdaExpr)
        {
            FilterCall = new FilterCallWithLambdaExpression(filterBase + (entity != null ? "<" + entity + ">" : ""),
                arrayAccess, index, element, lambdaExpr);

            id = idSource;
            ++idSource;
        }

        public SequenceFilterCallLambdaExpression(String filterBase, String entity,
            SequenceVariable initArrayAccess, SequenceExpression initExpr,
            SequenceVariable arrayAccess, SequenceVariable previousAccumulationAccess,
            SequenceVariable index, SequenceVariable element, SequenceExpression lambdaExpr)
        {
            FilterCall = new FilterCallWithLambdaExpression(filterBase + (entity != null ? "<" + entity + ">" : ""),
                initArrayAccess, initExpr,
                arrayAccess, previousAccumulationAccess, index, element, lambdaExpr);

            id = idSource;
            ++idSource;
        }
    }

    public class SequenceFilterCallLambdaExpressionInterpreted : SequenceFilterCallLambdaExpression
    {
        public MatchClassFilterer MatchClass; // only set in case of a match class filter call

        // entity may be null
        public SequenceFilterCallLambdaExpressionInterpreted(/*IAction action, */String filterBase, String entity,
            SequenceVariable arrayAccess, SequenceVariable index, SequenceVariable element, SequenceExpression lambdaExpr)
            : base(filterBase, entity, arrayAccess, index, element, lambdaExpr)
        {
        }

        public SequenceFilterCallLambdaExpressionInterpreted(/*IAction action, */String filterBase, String entity,
            SequenceVariable initArrayAccess, SequenceExpression initExpr,
            SequenceVariable arrayAccess, SequenceVariable previousAccumulationAccess,
            SequenceVariable index, SequenceVariable element, SequenceExpression lambdaExpr)
            : base(filterBase, entity, initArrayAccess, initExpr, arrayAccess, previousAccumulationAccess, index, element, lambdaExpr)
        {
        }

        public SequenceFilterCallLambdaExpressionInterpreted(MatchClassFilterer matchClass, String filterBase, String entity,
            SequenceVariable arrayAccess, SequenceVariable index, SequenceVariable element, SequenceExpression lambdaExpr)
            : base(filterBase, entity, arrayAccess, index, element, lambdaExpr)
        {
            MatchClass = matchClass;
        }

        public SequenceFilterCallLambdaExpressionInterpreted(MatchClassFilterer matchClass, String filterBase, String entity,
            SequenceVariable initArrayAccess, SequenceExpression initExpr,
            SequenceVariable arrayAccess, SequenceVariable previousAccumulationAccess, 
            SequenceVariable index, SequenceVariable element, SequenceExpression lambdaExpr)
            : base(filterBase, entity, initArrayAccess, initExpr, arrayAccess, previousAccumulationAccess, index, element, lambdaExpr)
        {
            MatchClass = matchClass;
        }

        public void Execute(IGraphProcessingEnvironment procEnv, IAction action, IMatches matches)
        {
            procEnv.Filter(matches, FilterCall);
        }

        public void Execute(IGraphProcessingEnvironment procEnv, List<IMatch> matchList)
        {
            MatchClass.Filter(procEnv, matchList, FilterCall);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if(MatchClass != null)
                sb.Append(MatchClass.info.PackagePrefixedName + ".");
            sb.Append(Name);
            //if(FilterCall.Entity != null)
            //    sb.Append("<" + FilterCall.Entity + ">");
            if(FilterCall.initExpression != null)
            {
                sb.Append("{");
                if(FilterCall.initArrayAccess != null)
                    sb.Append(FilterCall.initArrayAccess.Name + "; ");
                sb.Append(FilterCall.initExpression.Symbol);
                sb.Append("}");
            }
            sb.Append("{");
            if(FilterCall.arrayAccess != null)
                sb.Append(FilterCall.arrayAccess.Name + "; ");
            if(FilterCall.previousAccumulationAccess != null)
                sb.Append(FilterCall.previousAccumulationAccess + ", ");
            if(FilterCall.index != null)
                sb.Append(FilterCall.index.Name + " -> ");
            sb.Append(FilterCall.element.Name + " -> ");
            sb.Append(FilterCall.lambdaExpression.Symbol);
            sb.Append("}");
            return sb.ToString();
        }
    }

    /// <summary>
    /// A sequence representing a lambda expression filter call at compile time (of an action filter or a match class filter).
    /// It specifies the filter and optionally a target entity (plus the lambda expression and the per-element variables - in the parent class).
    /// Only used internally for code generation.
    /// </summary>
    public class SequenceFilterCallLambdaExpressionCompiled : SequenceFilterCallLambdaExpression
    {
        /// <summary>
        /// null if this is a single-rule filter, otherwise the match class of the filter.
        /// </summary>
        public readonly String MatchClassName;

        /// <summary>
        /// null if the match class is global, otherwise the (resolved) package the match class is contained in.
        /// </summary>
        public String MatchClassPackage;

        /// <summary>
        /// The name of the match class, prefixed by the (resolved) package it is contained in (separated by a double colon), if it is contained in a package.
        /// </summary>
        public String MatchClassPackagePrefixedName;

        ///////////////////////////////////////////////////////////////

        public string Entity
        {
            get
            {
                if(PackagePrefixedName.IndexOf('<') == -1)
                    return null;
                int beginIndexOfEntity = PackagePrefixedName.IndexOf('<') + 1;
                int length = PackagePrefixedName.Length - beginIndexOfEntity - 1; // removes closing '>'
                return PackagePrefixedName.Substring(beginIndexOfEntity, length);
            }
        }

        public string PlainName
        {
            get
            {
                if(PackagePrefixedName.IndexOf('<') == -1)
                    return PackagePrefixedName;
                return PackagePrefixedName.Substring(0, PackagePrefixedName.IndexOf('<'));
            }
        }

        ///////////////////////////////////////////////////////////////

        public SequenceFilterCallLambdaExpressionCompiled(String filterBase, String entity,
            SequenceVariable arrayAccess, SequenceVariable index, SequenceVariable element, SequenceExpression lambdaExpr)
            : base(filterBase, entity, arrayAccess, index, element, lambdaExpr)
        {
            MatchClassName = null;
            MatchClassPackage = null;
            MatchClassPackagePrefixedName = null;

            FilterCall = new FilterCallWithLambdaExpression(filterBase + (entity != null ? "<" + entity + ">": ""),
                arrayAccess, index, element, lambdaExpr);
        }

        public SequenceFilterCallLambdaExpressionCompiled(String filterBase, String entity,
            SequenceVariable initArrayAccess, SequenceExpression initExpr,
            SequenceVariable arrayAccess, SequenceVariable previousAccumulationAccess,
            SequenceVariable index, SequenceVariable element, SequenceExpression lambdaExpr)
            : base(filterBase, entity, initArrayAccess, initExpr, arrayAccess, previousAccumulationAccess, index, element, lambdaExpr)
        {
            MatchClassName = null;
            MatchClassPackage = null;
            MatchClassPackagePrefixedName = null;

            FilterCall = new FilterCallWithLambdaExpression(filterBase + (entity != null ? "<" + entity + ">" : ""),
                initArrayAccess, initExpr,
                arrayAccess, previousAccumulationAccess, index, element, lambdaExpr);
        }

        public SequenceFilterCallLambdaExpressionCompiled(String matchClassName, String matchClassPackage, String matchClassPackagePrefixedName,
            String filterBase, String entity,
            SequenceVariable arrayAccess, SequenceVariable index, SequenceVariable element, SequenceExpression lambdaExpr)
            : base(filterBase, entity, arrayAccess, index, element, lambdaExpr)
        {
            MatchClassName = matchClassName;
            MatchClassPackage = matchClassPackage;
            MatchClassPackagePrefixedName = matchClassPackagePrefixedName;

            FilterCall = new FilterCallWithLambdaExpression(filterBase + (entity != null ? "<" + entity + ">": ""),
                arrayAccess, index, element, lambdaExpr);
        }

        public SequenceFilterCallLambdaExpressionCompiled(String matchClassName, String matchClassPackage, String matchClassPackagePrefixedName,
            String filterBase, String entity,
            SequenceVariable initArrayAccess, SequenceExpression initExpr,
            SequenceVariable arrayAccess, SequenceVariable previousAccumulationAccess,
            SequenceVariable index, SequenceVariable element, SequenceExpression lambdaExpr)
            : base(filterBase, entity, initArrayAccess, initExpr, arrayAccess, previousAccumulationAccess, index, element, lambdaExpr)
        {
            MatchClassName = matchClassName;
            MatchClassPackage = matchClassPackage;
            MatchClassPackagePrefixedName = matchClassPackagePrefixedName;

            FilterCall = new FilterCallWithLambdaExpression(filterBase + (entity != null ? "<" + entity + ">" : ""),
                initArrayAccess, initExpr,
                arrayAccess, previousAccumulationAccess, index, element, lambdaExpr);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if(MatchClassName != null)
                sb.Append(MatchClassPackagePrefixedName + ".");
            sb.Append(Name);
            //if(FilterCall.Entity != null)
            //    sb.Append("<" + FilterCall.Entity + ">");
            if(FilterCall.initExpression != null)
            {
                sb.Append("{");
                if(FilterCall.initArrayAccess != null)
                    sb.Append(FilterCall.initArrayAccess.Name + "; ");
                sb.Append(FilterCall.initExpression.Symbol);
                sb.Append("}");
            }
            sb.Append("{");
            if(FilterCall.arrayAccess != null)
                sb.Append(FilterCall.arrayAccess + "; ");
            if(FilterCall.previousAccumulationAccess != null)
                sb.Append(FilterCall.previousAccumulationAccess + ", ");
            if(FilterCall.index != null)
                sb.Append(FilterCall.index.Name + " -> ");
            sb.Append(FilterCall.element.Name + " -> ");
            sb.Append(FilterCall.lambdaExpression.Symbol);
            sb.Append("}");
            return sb.ToString();
        }
    }

    public abstract class SequenceRuleCall : SequenceSpecial, RuleInvocation, IPatternMatchingConstruct
    {
        /// <summary>
        /// An array of expressions used to compute the input arguments.
        /// </summary>
        public readonly SequenceExpression[] ArgumentExpressions;

        /// <summary>
        /// Buffer to store the argument values for the call; used to avoid unneccessary memory allocations.
        /// </summary>
        public readonly object[] Arguments;

        /// <summary>
        /// An array of variables used for the return values. Might be empty if the caller is not interested in available returns values.
        /// </summary>
        public readonly SequenceVariable[] ReturnVars;

        /// <summary>
        /// The subgraph to be switched to for rule execution.
        /// </summary>
        public readonly SequenceVariable subgraph;

        public virtual PatternMatchingConstructType ConstructType
        {
            get { return PatternMatchingConstructType.RuleCall; }
        }

        public abstract String Name { get; }
        public abstract String Package { get; }
        public abstract String PackagePrefixedName { get; }

        public SequenceVariable Subgraph
        {
            get { return subgraph; }
        }

        public readonly bool Test;
        public readonly List<SequenceFilterCallBase> Filters;

        public readonly bool IsRuleForMultiRuleAllCallReturningArrays;

        public IPatternMatchingConstruct Parent;


        protected SequenceRuleCall(List<SequenceExpression> argExprs, List<SequenceVariable> returnVars, SequenceVariable subgraph,
            bool special, bool test, bool isRuleForMultiRuleAllCallReturningArrays)
            : this(SequenceType.RuleCall, argExprs, returnVars, subgraph, special, test)
        {
            IsRuleForMultiRuleAllCallReturningArrays = isRuleForMultiRuleAllCallReturningArrays;
        }

        protected SequenceRuleCall(SequenceType seqType, 
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars, SequenceVariable subgraph,
            bool special, bool test)
            : base(seqType, special)
        {
            InitializeArgumentExpressionsAndArguments(argExprs, out ArgumentExpressions, out Arguments);
            InitializeReturnVariables(returnVars, out ReturnVars);
            this.subgraph = subgraph;
            Test = test;
            Filters = new List<SequenceFilterCallBase>();
            IsRuleForMultiRuleAllCallReturningArrays = false;
        }

        protected SequenceRuleCall(SequenceRuleCall that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            CopyArgumentExpressionsAndArguments(originalToCopy, procEnv, that.ArgumentExpressions,
                out ArgumentExpressions, out Arguments);
            CopyVars(originalToCopy, procEnv, that.ReturnVars,
                out ReturnVars);
            if(that.subgraph != null)
                subgraph = that.subgraph.Copy(originalToCopy, procEnv);
            Test = that.Test;
            Filters = that.Filters;
            IsRuleForMultiRuleAllCallReturningArrays = that.IsRuleForMultiRuleAllCallReturningArrays;
        }

        public void AddFilterCall(SequenceFilterCallBase sequenceFilterCall)
        {
            Filters.Add(sequenceFilterCall);
        }

        readonly static List<object[]> emptyList = new List<object[]>(); // performance optimization (for ApplyRewrite, empty list is only created once)

        public static List<object[]> ApplyRewrite(IGraphProcessingEnvironment procEnv, IAction action, IGraph subgraph, object[] arguments, int which,
            int localMaxMatches, bool special, bool test, List<SequenceFilterCallBase> filters, out int numMatches)
        {
            if(subgraph != null)
                procEnv.SwitchToSubgraph(subgraph);

            IMatches matches = procEnv.MatchWithoutEvent(action, arguments, localMaxMatches, FireDebugEvents);

            for(int i = 0; i < filters.Count; ++i)
            {
                SequenceFilterCallInterpreted filter = (SequenceFilterCallInterpreted)filters[i];
                filter.Execute(procEnv, action, matches);
            }

            if(matches.Count > 0) // ensure that Matched is only called when a match exists
                FireMatchedAfterFilteringEvent(procEnv, matches, special);

            if(matches.Count == 0)
            {
                if(subgraph != null)
                    procEnv.ReturnFromSubgraph();
                numMatches = 0;
                return emptyList;
            }

            if(test)
            {
                if(subgraph != null)
                    procEnv.ReturnFromSubgraph();
                numMatches = matches.Count;
                return emptyList;
            }

#if DEBUGACTIONS || MATCHREWRITEDETAIL // spread over multiple files now, search for the corresponding defines to reactivate
            PerformanceInfo.StartLocal();
#endif
            List<object[]> retElemsList = procEnv.Replace(matches, which, special, FireDebugEvents);
#if DEBUGACTIONS || MATCHREWRITEDETAIL
            PerformanceInfo.StopRewrite();
#endif
            FireFinishedEvent(procEnv, matches, special);

            if(subgraph != null)
                procEnv.ReturnFromSubgraph();

            numMatches = matches.Count;
            return retElemsList;
        }

        // Modify of the Action is normally called by Rewrite of the graph processing environment, delegated to from the ApplyImpl method
        // this Rewrite is called by Backtracking, SomeFromSet, and a rule all call in case of random choice
        // those are constructs that first compute several matches, potentially of multiple rules, and then rewrite some of them
        // overriden in RuleAllCall and RuleCountAllCall to handle their differences, including/esp. list returns
        // (the backtracking computes all matches but rewrites only one of them as in case of a single rule (and then the next one; implemented with a rule, not a rule all))
        public virtual bool Rewrite(IGraphProcessingEnvironment procEnv, IMatches matches, IMatch chosenMatch)
        {
            if(matches.Count == 0)
                return false;
            if(Test)
                return false;

            IMatch match = chosenMatch != null ? chosenMatch : matches.First;
            FireMatchSelectedEvent(procEnv, match, Special, matches);

#if DEBUGACTIONS || MATCHREWRITEDETAIL // spread over multiple files now, search for the corresponding defines to reactivate
            procEnv.PerformanceInfo.StartLocal();
#endif
            FireRewritingSelectedMatchEvent(procEnv);
            object[] retElems = matches.Producer.Modify(procEnv, match);
            ++procEnv.PerformanceInfo.RewritesPerformed;

            FillReturnVariablesFromValues(ReturnVars, procEnv, retElems);

#if DEBUGACTIONS || MATCHREWRITEDETAIL
            procEnv.PerformanceInfo.StopRewrite(); // total rewrite time does NOT include listeners anymore
#endif
            FireFinishedSelectedMatchEvent(procEnv);

#if LOG_SEQUENCE_EXECUTION
            procEnv.Recorder.WriteLine("Matched/Applied " + Symbol);
            procEnv.Recorder.Flush();
#endif

            return true;
        }

        protected List<object[]> RewriteAllMatches(IGraphProcessingEnvironment procEnv, IMatches matches)
        {
            List<object[]> returns = matches.Producer.Reserve(matches.Count);

            int curResultNum = 0;
            foreach(IMatch match in matches)
            {
                FireMatchSelectedEvent(procEnv, match, Special, matches);
                FireRewritingSelectedMatchEvent(procEnv);
                object[] retElems = matches.Producer.Modify(procEnv, match);
                object[] curResult = returns[curResultNum];
                for(int i = 0; i < retElems.Length; ++i)
                {
                    curResult[i] = retElems[i];
                }
                ++procEnv.PerformanceInfo.RewritesPerformed;
                ++curResultNum;
                FireFinishedSelectedMatchEvent(procEnv);
            }

            return returns;
        }

        protected static IMatches MatchForQuery(IGraphProcessingEnvironment procEnv, IAction action, IGraph subgraph, object[] arguments,
            int localMaxMatches, bool special, List<SequenceFilterCallBase> filters)
        {
            if(subgraph != null)
                procEnv.SwitchToSubgraph(subgraph);

            IMatches matches = procEnv.MatchForQuery(action, arguments, localMaxMatches, FireDebugEvents);

            for(int i = 0; i < filters.Count; ++i)
            {
                SequenceFilterCallBase filter = filters[i];
                if(filter is SequenceFilterCallLambdaExpressionInterpreted)
                {
                    SequenceFilterCallLambdaExpressionInterpreted lambdaExpressionFilterCall = (SequenceFilterCallLambdaExpressionInterpreted)filter;
                    procEnv.Filter(matches, lambdaExpressionFilterCall.FilterCall);
                }
                else
                {
                    SequenceFilterCallInterpreted filterCall = (SequenceFilterCallInterpreted)filter;
                    filterCall.Execute(procEnv, action, matches);
                }
            }

            //subrule debugging must be changed to allow this
            //if(matches.Count > 0) {// ensure that Matched is only called when a match exists
            //    FireMatchedAfterFilteringEvent(procEnv, matches, special);
            //}

            if(subgraph != null)
                procEnv.ReturnFromSubgraph();

            return matches;
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors, SequenceBase target)
        {
            GetLocalVariables(ArgumentExpressions, variables, constructors);
            GetLocalVariables(ReturnVars, variables, constructors);
            if(subgraph != null)
                subgraph.GetLocalVariables(variables);
            GetLocalVariables(Filters, variables, constructors);
            return this == target;
        }

        public override IEnumerable<Sequence> Children
        {
            get { yield break; }
        }

        public override IEnumerable<SequenceBase> ChildrenBase
        { 
            get
            {
                foreach(SequenceExpression expr in ArgumentExpressions)
                {
                    yield return expr;
                }
                foreach(SequenceFilterCallBase filterCall in Filters)
                {
                    if(filterCall is SequenceFilterCall)
                    {
                        SequenceFilterCall filterCallFilterAvailable = (SequenceFilterCall)filterCall;
                        foreach(SequenceExpression expr in filterCallFilterAvailable.ArgumentExpressions)
                        {
                            yield return expr;
                        }
                    }
                    else
                    {
                        SequenceFilterCallLambdaExpression filterCallLambdaExpression = (SequenceFilterCallLambdaExpression)filterCall;
                        yield return filterCallLambdaExpression.FilterCall.lambdaExpression;
                    }
                }
            } 
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public string TestDebugPrefix
        {
            get
            {
                if(Special)
                {
                    if(Test)
                        return "%?";
                    else
                        return "%";
                }
                else
                {
                    if(Test)
                        return "?";
                    else
                        return "";
                }
            }
        }

        public string DebugPrefix
        {
            get
            {
                if(Special)
                    return "%";
                else
                    return "";
            }
        }

        protected String ReturnAssignmentString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if(ReturnVars.Length > 0)
                {
                    sb.Append("(");
                    for(int i = 0; i < ReturnVars.Length; ++i)
                    {
                        sb.Append(ReturnVars[i].Name);
                        if(i != ReturnVars.Length - 1)
                            sb.Append(",");
                    }
                    sb.Append(")=");
                }
                return sb.ToString();
            }
        }

        protected String RuleCallString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if(subgraph != null)
                    sb.Append(subgraph.Name + ".");
                sb.Append(Name);
                if(ArgumentExpressions.Length > 0)
                {
                    sb.Append("(");
                    for(int i = 0; i < ArgumentExpressions.Length; ++i)
                    {
                        sb.Append(ArgumentExpressions[i].Symbol);
                        if(i != ArgumentExpressions.Length - 1)
                            sb.Append(",");
                    }
                    sb.Append(")");
                }
                for(int i = 0; i < Filters.Count; ++i)
                {
                    sb.Append("\\").Append(Filters[i].ToString());
                }
                return sb.ToString();
            }
        }

        public override string Symbol
        {
            get
            {
                return ReturnAssignmentString + TestDebugPrefix + RuleCallString;
            }
        }

        public string SymbolNoTestPrefix
        {
            get
            {
                return ReturnAssignmentString + (Special ? "%" : "") + RuleCallString;
            }
        }
    }

    public class SequenceRuleCallInterpreted : SequenceRuleCall
    {
        /// <summary>
        /// The IAction instance to be used
        /// </summary>
        public readonly IAction Action;

        public override String Name
        {
            get { return Action.Name; }
        }

        public override String Package
        {
            get { return Action.Package; }
        }

        public override string PackagePrefixedName
        {
            get { return Action.PackagePrefixedName; }
        }

        public SequenceRuleCallInterpreted(IAction action,
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars, SequenceVariable subgraph,
            bool special, bool test, bool isRuleForMultiRuleAllCallReturningArrays)
            : base(argExprs, returnVars, subgraph, special, test, isRuleForMultiRuleAllCallReturningArrays)
        {
            Action = action;
        }

        protected SequenceRuleCallInterpreted(SequenceRuleCallInterpreted that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
            Action = that.Action;
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceRuleCallInterpreted(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            env.CheckRuleCall(this);
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            try
            {
                int numMatches;
#if LOG_SEQUENCE_EXECUTION
                procEnv.Recorder.WriteLine("Applying rule " + GetRuleCallString(procEnv));
#endif
                FireBeginExecutionEvent(procEnv);

                FillArgumentsFromArgumentExpressions(ArgumentExpressions, Arguments, procEnv);

                List<object[]> retElemsList = ApplyRewrite(procEnv, Action,
                    subgraph != null ? (IGraph)subgraph.GetVariableValue(procEnv) : null,
                    Arguments, 0, 1, Special, Test, Filters, out numMatches);

                if(retElemsList.Count > 0)
                    FillReturnVariablesFromValues(ReturnVars, Action, procEnv, retElemsList, 0);
#if LOG_SEQUENCE_EXECUTION
                if(res)
                {
                    procEnv.Recorder.WriteLine("Matched/Applied " + Symbol);
                    procEnv.Recorder.Flush();
                }
#endif
                FireEndExecutionEvent(procEnv, null);
                return numMatches > 0;
            }
            catch(NullReferenceException)
            {
                ConsoleUI.errorOutWriter.WriteLine("Null reference exception during rule execution (null parameter?): " + Symbol);
                throw;
            }
        }

        public override IEnumerable<Sequence> Children
        {
            get { yield break; }
        }

        public override int Precedence
        {
            get { return 8; }
        }
    }

    public class SequenceRuleCallCompiled : SequenceRuleCall, RuleInvocation
    {
        public readonly String name;
        public readonly String package;
        public readonly String packagePrefixedName;

        public override String Name
        {
            get { return name; }
        }

        public override String Package
        {
            get { return package; }
        }

        public override string PackagePrefixedName
        {
            get { return packagePrefixedName; }
        }

        public SequenceRuleCallCompiled(String Name, String Package, String PackagePrefixedName,
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars, SequenceVariable subgraph,
            bool special, bool test, bool isRuleForMultiRuleAllCallReturningArrays)
            : base(argExprs, returnVars, subgraph, special, test, isRuleForMultiRuleAllCallReturningArrays)
        {
            this.name = Name;
            this.package = Package;
            this.packagePrefixedName = PackagePrefixedName;
        }

        protected SequenceRuleCallCompiled(SequenceRuleCallCompiled that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
            name = that.name;
            package = that.package;
            packagePrefixedName = that.packagePrefixedName;
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceRuleCallCompiled(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            env.CheckRuleCall(this);
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Sequence> Children
        {
            get { yield break; }
        }

        public override int Precedence
        {
            get { return 8; }
        }
    }

    public abstract class SequenceRuleAllCall : SequenceRuleCall, SequenceRandomChoice
    {
        public bool ChooseRandom;
        public readonly bool MinSpecified;
        public readonly SequenceVariable MinVarChooseRandom;
        public readonly SequenceVariable MaxVarChooseRandom;
        private bool choice;

        public override PatternMatchingConstructType ConstructType
        {
            get { return PatternMatchingConstructType.RuleAllCall; }
        }

        protected SequenceRuleAllCall(List<SequenceExpression> argExprs, List<SequenceVariable> returnVars, SequenceVariable subgraph,
            bool special, bool test,
            bool chooseRandom, SequenceVariable varChooseRandom,
            bool chooseRandom2, SequenceVariable varChooseRandom2, bool choice)
            : base(SequenceType.RuleAllCall, argExprs, returnVars, subgraph, special, test)
        {
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

        protected SequenceRuleAllCall(SequenceRuleAllCall that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
            ChooseRandom = that.ChooseRandom;
            MinSpecified = that.MinSpecified;
            if(that.MinVarChooseRandom != null)
                MinVarChooseRandom = that.MinVarChooseRandom.Copy(originalToCopy, procEnv);
            if(that.MaxVarChooseRandom != null)
                MaxVarChooseRandom = that.MaxVarChooseRandom.Copy(originalToCopy, procEnv);
            choice = that.choice;
        }

        public bool Random { get { return ChooseRandom; } set { ChooseRandom = value; } }
        public bool Choice { get { return choice; } set { choice = value; } }

        public override bool Rewrite(IGraphProcessingEnvironment procEnv, IMatches matches, IMatch chosenMatch)
        {
            if(matches.Count == 0)
                return false;
            if(Test)
                return false;

            if(MinSpecified)
            {
                if(!(MinVarChooseRandom.GetVariableValue(procEnv) is int))
                    throw new InvalidOperationException("The variable '" + MinVarChooseRandom + "' is not of type int!");
                if(matches.Count < (int)MinVarChooseRandom.GetVariableValue(procEnv))
                    return false;
            }

#if DEBUGACTIONS || MATCHREWRITEDETAIL
            procEnv.PerformanceInfo.StartLocal();
#endif
            List<object[]> returns;
            if(!ChooseRandom)
            {
                if(chosenMatch!=null)
                    throw new InvalidOperationException("Chosen match given although all matches should get rewritten");
                returns = RewriteAllMatches(procEnv, matches);
            }
            else
                returns = RewriteSomeMatches(procEnv, matches, chosenMatch);

            FillReturnVariablesFromValues(ReturnVars, matches.Producer, procEnv, returns, -1);

#if DEBUGACTIONS || MATCHREWRITEDETAIL
            procEnv.PerformanceInfo.StopRewrite(); // total rewrite time does NOT include listeners anymore
#endif

#if LOG_SEQUENCE_EXECUTION
            procEnv.Recorder.WriteLine("Matched/Applied " + Symbol);
            procEnv.Recorder.Flush();
#endif

            return true;
        }

        protected List<object[]> RewriteSomeMatches(IGraphProcessingEnvironment procEnv, IMatches matches, IMatch chosenMatch)
        {
            object val = MaxVarChooseRandom != null ? MaxVarChooseRandom.GetVariableValue(procEnv) : (MinSpecified ? 2147483647 : 1);
            if(!(val is int))
                throw new InvalidOperationException("The variable '" + MaxVarChooseRandom.Name + "' is not of type int!");
            int numChooseRandom = (int)val;
            if(matches.Count < numChooseRandom) numChooseRandom = matches.Count;

            List<object[]> returns = matches.Producer.Reserve(numChooseRandom);

            int curResultNum = 0;
            for(int i = 0; i < numChooseRandom; ++i)
            {
                int matchToApply = randomGenerator.Next(matches.Count);
                if(Choice)
                    matchToApply = procEnv.UserProxy.ChooseMatch(matchToApply, matches, numChooseRandom - 1 - i, this);
                IMatch match = matches.RemoveMatch(matchToApply);
                if(chosenMatch != null)
                    match = chosenMatch;
                FireMatchSelectedEvent(procEnv, match, Special, matches);
                FireRewritingSelectedMatchEvent(procEnv);
                object[] retElems = matches.Producer.Modify(procEnv, match);
                object[] curResult = returns[curResultNum];
                retElems.CopyTo(curResult, 0);
                ++procEnv.PerformanceInfo.RewritesPerformed;
                ++curResultNum;
                FireFinishedSelectedMatchEvent(procEnv);
            }

            return returns;
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors, SequenceBase target)
        {
            base.GetLocalVariables(variables, constructors, target);
            if(MinVarChooseRandom!=null)
                MinVarChooseRandom.GetLocalVariables(variables);
            if(MaxVarChooseRandom!=null)
                MaxVarChooseRandom.GetLocalVariables(variables);
            return this == target;
        }

        public string RandomChoicePrefix
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if(ChooseRandom)
                {
                    sb.Append("$");
                    if(Choice)
                        sb.Append("%");
                    if(MinSpecified)
                    {
                        sb.Append(MinVarChooseRandom.Name + ",");
                        if(MaxVarChooseRandom != null)
                            sb.Append(MaxVarChooseRandom.Name);
                        else
                            sb.Append("*");
                    }
                    else
                    {
                        if(MaxVarChooseRandom != null)
                            sb.Append(MaxVarChooseRandom.Name);
                    }
                }
                return sb.ToString();
            }
        }

        public override string Symbol
        {
            get
            {
                return ReturnAssignmentString + RandomChoicePrefix + "[" + TestDebugPrefix + RuleCallString + "]";
            }
        }
    }

    public class SequenceRuleAllCallInterpreted : SequenceRuleAllCall, RuleInvocation
    {
        /// <summary>
        /// The IAction instance to be used
        /// </summary>
        public readonly IAction Action;

        public override String Name
        {
            get { return Action.Name; }
        }

        public override String Package
        {
            get { return Action.Package; }
        }

        public override string PackagePrefixedName
        {
            get { return PackagePrefixedName; }
        }

        public SequenceRuleAllCallInterpreted(IAction Action,
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars, SequenceVariable subgraph,
            bool special, bool test,
            bool chooseRandom, SequenceVariable varChooseRandom,
            bool chooseRandom2, SequenceVariable varChooseRandom2, bool choice)
            : base(argExprs, returnVars, subgraph,
                    special, test,
                    chooseRandom, varChooseRandom, chooseRandom2, varChooseRandom2, choice)
        {
            this.Action = Action;
        }

        protected SequenceRuleAllCallInterpreted(SequenceRuleAllCallInterpreted that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
            Action = that.Action;
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceRuleAllCallInterpreted(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            env.CheckRuleAllCall(this);
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            try
            {
                if(!ChooseRandom)
                {
                    int numMatches;
#if LOG_SEQUENCE_EXECUTION
                    procEnv.Recorder.WriteLine("Applying rule all " + GetRuleCallString(procEnv));
#endif
                    FireBeginExecutionEvent(procEnv);

                    FillArgumentsFromArgumentExpressions(ArgumentExpressions, Arguments, procEnv);

                    List<object[]> retElemsList = ApplyRewrite(procEnv, Action,
                        subgraph != null ? (IGraph)subgraph.GetVariableValue(procEnv) : null,
                        Arguments, -1, -1, Special, Test, Filters, out numMatches);

                    if(retElemsList.Count > 0)
                        FillReturnVariablesFromValues(ReturnVars, Action, procEnv, retElemsList, -1);
#if LOG_SEQUENCE_EXECUTION
                    if(res)
                    {
                        procEnv.Recorder.WriteLine("Matched/Applied " + Symbol);
                        procEnv.Recorder.Flush();
                    }
#endif
                    FireEndExecutionEvent(procEnv, null);
                    return numMatches > 0;
                }
                else
                {
                    FireBeginExecutionEvent(procEnv);

                    FillArgumentsFromArgumentExpressions(ArgumentExpressions, Arguments, procEnv);

                    if(subgraph != null)
                        procEnv.SwitchToSubgraph((IGraph)subgraph.GetVariableValue(procEnv));

                    IMatches matches = procEnv.MatchWithoutEvent(Action, Arguments, procEnv.MaxMatches, FireDebugEvents);

                    for(int i = 0; i < Filters.Count; ++i)
                    {
                        SequenceFilterCallInterpreted filter = (SequenceFilterCallInterpreted)Filters[i];
                        filter.Execute(procEnv, Action, matches);
                    }

                    if(MinSpecified)
                    {
                        if(!(MinVarChooseRandom.GetVariableValue(procEnv) is int))
                            throw new InvalidOperationException("The variable '" + MinVarChooseRandom + "' is not of type int!");
                        if(matches.Count < (int)MinVarChooseRandom.GetVariableValue(procEnv))
                        {
                            FireEndExecutionEvent(procEnv, null);
                            return false;
                        }
                    }

                    bool result = false;
                    if(matches.Count > 0)
                    {
                        FireMatchedAfterFilteringEvent(procEnv, matches, Special); // only called when at least one match is existing, or the minimum number of matches was reached if a lower bound was specified

                        result = Rewrite(procEnv, matches, null);

                        FireFinishedEvent(procEnv, matches, Special);
                    }

                    if(subgraph != null)
                        procEnv.ReturnFromSubgraph();

                    FireEndExecutionEvent(procEnv, null);
                    return result;
                }
            }
            catch(NullReferenceException)
            {
                ConsoleUI.errorOutWriter.WriteLine("Null reference exception during rule execution (null parameter?): " + Symbol);
                throw;
            }
        }

        public List<IMatch> MatchForQuery(IGraphProcessingEnvironment procEnv)
        {
            try
            {
#if LOG_SEQUENCE_EXECUTION
                procEnv.Recorder.WriteLine("Matching rule " + GetRuleCallString(procEnv) + " for expression");
#endif
                FillArgumentsFromArgumentExpressions(ArgumentExpressions, Arguments, procEnv);

                // MatchForQuery clones all matches, as query matches may be stored,
                // or the action may be called again before processing of the matches finished (simple [?r] + [?r] sufficient)
                IMatches matches = MatchForQuery(procEnv, Action,
                    subgraph != null ? (IGraph)subgraph.GetVariableValue(procEnv) : null,
                    Arguments, 0, Special, Filters);

#if LOG_SEQUENCE_EXECUTION
                if(res)
                {
                    procEnv.Recorder.WriteLine("Matched/Applied " + Symbol);
                    procEnv.Recorder.Flush();
                }
#endif

                return matches.ToListCopy();
            }
            catch(NullReferenceException)
            {
                ConsoleUI.errorOutWriter.WriteLine("Null reference exception during rule execution (null parameter?): " + Symbol);
                throw;
            }
        }

        public List<IMatch> MatchForQuery(IGraphProcessingEnvironment procEnv, out IMatches matches)
        {
            try
            {
#if LOG_SEQUENCE_EXECUTION
                procEnv.Recorder.WriteLine("Matching rule " + GetRuleCallString(procEnv) + " for expression");
#endif
                FillArgumentsFromArgumentExpressions(ArgumentExpressions, Arguments, procEnv);

                // MatchForQuery clones all matches, as query matches may be stored,
                // or the action may be called again before processing of the matches finished (simple [?r] + [?r] sufficient)
                matches = MatchForQuery(procEnv, Action,
                    subgraph != null ? (IGraph)subgraph.GetVariableValue(procEnv) : null,
                    Arguments, 0, Special, Filters);

#if LOG_SEQUENCE_EXECUTION
                if(res)
                {
                    procEnv.Recorder.WriteLine("Matched/Applied " + Symbol);
                    procEnv.Recorder.Flush();
                }
#endif

                return matches.ToListCopy();
            }
            catch(NullReferenceException)
            {
                ConsoleUI.errorOutWriter.WriteLine("Null reference exception during rule execution (null parameter?): " + Symbol);
                throw;
            }
        }
    }

    public class SequenceRuleAllCallCompiled : SequenceRuleAllCall, RuleInvocation
    {
        public readonly String name;
        public readonly String package;
        public readonly String packagePrefixedName;

        public override String Name
        {
            get { return name; }
        }

        public override String Package
        {
            get { return package; }
        }

        public override string PackagePrefixedName
        {
            get { return packagePrefixedName; }
        }

        public SequenceRuleAllCallCompiled(String Name, String Package, String PackagePrefixedName,
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars, SequenceVariable subgraph,
            bool special, bool test,
            bool chooseRandom, SequenceVariable varChooseRandom,
            bool chooseRandom2, SequenceVariable varChooseRandom2, bool choice)
            : base(argExprs, returnVars, subgraph,
                  special, test,
                  chooseRandom, varChooseRandom, chooseRandom2, varChooseRandom2, choice)
        {
            this.name = Name;
            this.package = Package;
            this.packagePrefixedName = PackagePrefixedName;
        }

        protected SequenceRuleAllCallCompiled(SequenceRuleAllCallCompiled that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
            name = that.name;
            package = that.package;
            packagePrefixedName = that.packagePrefixedName;
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceRuleAllCallCompiled(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            env.CheckRuleAllCall(this);
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            throw new NotImplementedException();
        }
    }

    public abstract class SequenceRuleCountAllCall : SequenceRuleCall
    {
        public SequenceVariable CountResult;

        public override PatternMatchingConstructType ConstructType
        {
            get { return PatternMatchingConstructType.RuleCountAllCall; }
        }

        protected SequenceRuleCountAllCall(List<SequenceExpression> argExprs, List<SequenceVariable> returnVars, SequenceVariable subgraph,
            bool special, bool test)
            : base(SequenceType.RuleCountAllCall, argExprs, returnVars, subgraph, special, test)
        {
        }

        protected SequenceRuleCountAllCall(SequenceRuleCountAllCall that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
            CountResult = that.CountResult.Copy(originalToCopy, procEnv);
        }

        public void AddCountResult(SequenceVariable countResult)
        {
            CountResult = countResult;
        }

        public override bool Rewrite(IGraphProcessingEnvironment procEnv, IMatches matches, IMatch chosenMatch)
        {
            CountResult.SetVariableValue(matches.Count, procEnv);

            if(matches.Count == 0)
                return false;
            if(Test)
                return false;

#if DEBUGACTIONS || MATCHREWRITEDETAIL
            procEnv.PerformanceInfo.StartLocal();
#endif

            if(chosenMatch != null)
                throw new InvalidOperationException("Chosen match given although all matches should get rewritten");

            List<object[]> returns = RewriteAllMatches(procEnv, matches);

            FillReturnVariablesFromValues(ReturnVars, matches.Producer, procEnv, returns, -1);

#if DEBUGACTIONS || MATCHREWRITEDETAIL
            procEnv.PerformanceInfo.StopRewrite(); // total rewrite time does NOT include listeners anymore
#endif
            FireFinishedEvent(procEnv, matches, Special);

#if LOG_SEQUENCE_EXECUTION
            procEnv.Recorder.WriteLine("Matched/Applied " + Symbol);
            procEnv.Recorder.Flush();
#endif

            return true;
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors, SequenceBase target)
        {
            base.GetLocalVariables(variables, constructors, target);
            CountResult.GetLocalVariables(variables);
            return this == target;
        }

        public override string Symbol
        {
            get
            {
                return ReturnAssignmentString + "count[" + TestDebugPrefix + RuleCallString + "]" + "=>" + CountResult.Name;
            }
        }
    }

    public class SequenceRuleCountAllCallInterpreted : SequenceRuleCountAllCall
    {
        /// <summary>
        /// The IAction instance to be used
        /// </summary>
        public readonly IAction Action;

        public override String Name
        {
            get { return Action.Name; }
        }

        public override String Package
        {
            get { return Action.Package; }
        }

        public override string PackagePrefixedName
        {
            get { return Action.PackagePrefixedName; }
        }

        public SequenceRuleCountAllCallInterpreted(IAction Action,
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars, SequenceVariable subgraph,
            bool special, bool test)
            : base(argExprs, returnVars, subgraph,
                    special, test)
        {
            this.Action = Action;
        }

        protected SequenceRuleCountAllCallInterpreted(SequenceRuleCountAllCallInterpreted that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
            Action = that.Action;
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceRuleCountAllCallInterpreted(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            env.CheckRuleCall(this);
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            try
            {
                int numMatches;
#if LOG_SEQUENCE_EXECUTION
                procEnv.Recorder.WriteLine("Applying rule all " + GetRuleCallString(procEnv));
#endif
                FireBeginExecutionEvent(procEnv);

                FillArgumentsFromArgumentExpressions(ArgumentExpressions, Arguments, procEnv);

                List<object[]> retElemsList = ApplyRewrite(procEnv, Action, 
                    subgraph != null ? (IGraph)subgraph.GetVariableValue(procEnv) : null,
                    Arguments, -1, -1, Special, Test, Filters, out numMatches);

                if(retElemsList.Count > 0)
                    FillReturnVariablesFromValues(ReturnVars, Action, procEnv, retElemsList, -1);
#if LOG_SEQUENCE_EXECUTION
                if(res > 0)
                {
                    procEnv.Recorder.WriteLine("Matched/Applied " + Symbol + " yielding " + res + " matches");
                    procEnv.Recorder.Flush();
                }
#endif
                CountResult.SetVariableValue(numMatches, procEnv);
                FireEndExecutionEvent(procEnv, null);
                return numMatches > 0;
            }
            catch(NullReferenceException)
            {
                ConsoleUI.errorOutWriter.WriteLine("Null reference exception during rule execution (null parameter?): " + Symbol);
                throw;
            }
        }
    }

    public class SequenceRuleCountAllCallCompiled : SequenceRuleCountAllCall
    {
        public readonly String name;
        public readonly String package;
        public readonly String packagePrefixedName;

        public override String Name
        {
            get { return name; }
        }

        public override String Package
        {
            get { return package; }
        }

        public override string PackagePrefixedName
        {
            get { return packagePrefixedName; }
        }

        public SequenceRuleCountAllCallCompiled(String Name, String Package, String PackagePrefixedName,
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars, SequenceVariable subgraph,
            bool special, bool test)
            : base(argExprs, returnVars, subgraph,
                  special, test)
        {
            this.name = Name;
            this.package = Package;
            this.packagePrefixedName = PackagePrefixedName;
        }

        protected SequenceRuleCountAllCallCompiled(SequenceRuleCountAllCallCompiled that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
            name = that.name;
            package = that.package;
            packagePrefixedName = that.packagePrefixedName;
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceRuleCountAllCallCompiled(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            env.CheckRuleCall(this);
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            throw new NotImplementedException();
        }
    }

    public class SequenceAssignUserInputToVar : SequenceAssignToVar, SequenceRandomChoice
    {
        public readonly String UserInputType;

        public bool Random { get { return false; } set { throw new Exception("can't change Random on SequenceAssignUserInputToVar"); } }
        public bool Choice { get { return true; } set { throw new Exception("can't change Choice on SequenceAssignUserInputToVar"); } }

        public SequenceAssignUserInputToVar(SequenceVariable destVar, String type)
            : base(SequenceType.AssignUserInputToVar, destVar)
        {
            this.UserInputType = type;
        }

        protected SequenceAssignUserInputToVar(SequenceAssignUserInputToVar that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
            UserInputType = that.UserInputType;
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceAssignUserInputToVar(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            if(!TypesHelper.IsSameOrSubtype(UserInputType, DestVar.Type, env.Model))
                throw new SequenceParserExceptionTypeMismatch(Symbol, DestVar.Type, UserInputType);
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            return Assign(procEnv.UserProxy.ChooseValue(UserInputType, this), procEnv);
        }

        public override string Symbol
        {
            get { return DestVar.Name + "=" + "$%(" + UserInputType + ")"; }
        }
    }

    public class SequenceAssignRandomIntToVar : SequenceAssignToVar, SequenceRandomChoice
    {
        public readonly int Number;

        public bool Random { get { return true; } set { throw new Exception("can't change Random on SequenceAssignRandomIntToVar"); } }
        public bool Choice { get { return choice; } set { choice = value; } }
        private bool choice;

        public SequenceAssignRandomIntToVar(SequenceVariable destVar, int number, bool choice)
            : base(SequenceType.AssignRandomIntToVar, destVar)
        {
            Number = number;
            this.choice = choice;
        }

        protected SequenceAssignRandomIntToVar(SequenceAssignRandomIntToVar that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
            Number = that.Number;
            choice = that.choice;
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceAssignRandomIntToVar(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            if(!TypesHelper.IsSameOrSubtype(DestVar.Type, "int", env.Model))
                throw new SequenceParserExceptionTypeMismatch(Symbol, "int", DestVar.Type);
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            int randomNumber = randomGenerator.Next(Number);
            if(Choice)
                randomNumber = procEnv.UserProxy.ChooseRandomNumber(randomNumber, Number, this);
            return Assign(randomNumber, procEnv);
        }

        public override string Symbol
        {
            get { return DestVar.Name + "=" + (Choice ? "$%" : "$") + "(" + Number + ")"; }
        }
    }

    public class SequenceAssignRandomDoubleToVar : SequenceAssignToVar, SequenceRandomChoice
    {
        public bool Random { get { return true; } set { throw new Exception("can't change Random on SequenceAssignRandomDoubleToVar"); } }
        public bool Choice { get { return choice; } set { choice = value; } }
        private bool choice;

        public SequenceAssignRandomDoubleToVar(SequenceVariable destVar, bool choice)
            : base(SequenceType.AssignRandomDoubleToVar, destVar)
        {
            this.choice = choice;
        }

        protected SequenceAssignRandomDoubleToVar(SequenceAssignRandomDoubleToVar that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
            choice = that.choice;
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceAssignRandomDoubleToVar(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            if(!TypesHelper.IsSameOrSubtype(DestVar.Type, "double", env.Model))
                throw new SequenceParserExceptionTypeMismatch(Symbol, "double", DestVar.Type);
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            double randomNumber = randomGenerator.NextDouble();
            if(Choice)
                randomNumber = procEnv.UserProxy.ChooseRandomNumber(randomNumber, this);
            return Assign(randomNumber, procEnv);
        }

        public override string Symbol
        {
            get { return DestVar.Name + "=" + (Choice ? "$%" : "$") + "(1.0)"; }
        }
    }

    public class SequenceDeclareVariable : SequenceAssignConstToVar
    {
        public SequenceDeclareVariable(SequenceVariable destVar)
            : base(SequenceType.DeclareVariable, destVar, null)
        {
        }

        protected SequenceDeclareVariable(SequenceDeclareVariable that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceDeclareVariable(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            Constant = TypesHelper.DefaultValue(DestVar.Type, env.Model);
        }

        public override string Symbol
        {
            get { return DestVar.Name + ":" + DestVar.Type; }
        }
    }

    public class SequenceAssignConstToVar : SequenceAssignToVar
    {
        public object Constant;

        public SequenceAssignConstToVar(SequenceVariable destVar, object constant)
            : this(SequenceType.AssignConstToVar, destVar, constant)
        {
        }

        protected SequenceAssignConstToVar(SequenceType seqType, SequenceVariable destVar, object constant)
            : base(seqType, destVar)
        {
            Constant = constant;
        }

        protected SequenceAssignConstToVar(SequenceAssignConstToVar that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
            Constant = that.Constant;
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceAssignConstToVar(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            if(!TypesHelper.IsSameOrSubtype(TypesHelper.XgrsTypeOfConstant(Constant, env.Model), DestVar.Type, env.Model))
                throw new SequenceParserExceptionTypeMismatch(Symbol, DestVar.Type, TypesHelper.XgrsTypeOfConstant(Constant, env.Model));
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
                else
                    return DestVar.Name + "=" + Constant.ToString();
            }
        }
    }

    public class SequenceAssignContainerConstructorToVar : SequenceAssignToVar
    {
        public readonly SequenceExpression Constructor;

        public SequenceAssignContainerConstructorToVar(SequenceVariable destVar, SequenceExpression constructor)
            : base(SequenceType.AssignContainerConstructorToVar, destVar)
        {
            Constructor = constructor;
        }

        protected SequenceAssignContainerConstructorToVar(SequenceAssignContainerConstructorToVar that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
            Constructor = that.Constructor.CopyExpression(originalToCopy, procEnv);
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceAssignContainerConstructorToVar(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            Constructor.Check(env);

            if(!TypesHelper.IsSameOrSubtype(Constructor.Type(env), DestVar.Type, env.Model))
                throw new SequenceParserExceptionTypeMismatch(Constructor.Symbol, DestVar.Type, Constructor.Type(env));
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            return Assign(Constructor.Evaluate(procEnv), procEnv);
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors, SequenceBase target)
        {
            DestVar.GetLocalVariables(variables);
            Constructor.GetLocalVariables(variables, constructors);
            return this == target;
        }

        public override IEnumerable<SequenceBase> ChildrenBase
        {
            get
            {
                foreach(Sequence child in Children)
                {
                    yield return child;
                }
            }
        }

        public override string Symbol
        {
            get
            {
                return DestVar.Name + "=" + Constructor.Symbol;
            }
        }
    }

    public class SequenceAssignObjectConstructorToVar : SequenceAssignToVar
    {
        public readonly SequenceExpression Constructor;

        public SequenceAssignObjectConstructorToVar(SequenceVariable destVar, SequenceExpression constructor)
            : base(SequenceType.AssignObjectConstructorToVar, destVar)
        {
            Constructor = constructor;
        }

        protected SequenceAssignObjectConstructorToVar(SequenceAssignObjectConstructorToVar that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
            Constructor = that.Constructor.CopyExpression(originalToCopy, procEnv);
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceAssignObjectConstructorToVar(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            Constructor.Check(env);

            if(!TypesHelper.IsSameOrSubtype(Constructor.Type(env), DestVar.Type, env.Model))
                throw new SequenceParserExceptionTypeMismatch(Constructor.Symbol, DestVar.Type, Constructor.Type(env));
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            return Assign(Constructor.Evaluate(procEnv), procEnv);
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors, SequenceBase target)
        {
            DestVar.GetLocalVariables(variables);
            Constructor.GetLocalVariables(variables, constructors);
            return this == target;
        }

        public override IEnumerable<SequenceBase> ChildrenBase
        {
            get
            {
                foreach(Sequence child in Children)
                {
                    yield return child;
                }
            }
        }

        public override string Symbol
        {
            get
            {
                return DestVar.Name + "=" + Constructor.Symbol;
            }
        }
    }

    public class SequenceAssignVarToVar : SequenceAssignToVar
    {
        public readonly SequenceVariable Variable;

        public SequenceAssignVarToVar(SequenceVariable destVar, SequenceVariable srcVar)
            : base(SequenceType.AssignVarToVar, destVar)
        {
            Variable = srcVar;
        }

        protected SequenceAssignVarToVar(SequenceAssignVarToVar that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
            Variable = that.Variable.Copy(originalToCopy, procEnv);
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceAssignVarToVar(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            if(!TypesHelper.IsSameOrSubtype(Variable.Type, DestVar.Type, env.Model))
                throw new SequenceParserExceptionTypeMismatch(Symbol, DestVar.Type, Variable.Type);
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors, SequenceBase target)
        {
            DestVar.GetLocalVariables(variables);
            Variable.GetLocalVariables(variables);
            return this == target;
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            return Assign(Variable.GetVariableValue(procEnv), procEnv);
        }

        public override string Symbol
        {
            get { return DestVar.Name + "=" + Variable.Name; }
        }
    }

    public class SequenceAssignSequenceResultToVar : SequenceAssignToVar
    {
        public readonly Sequence Seq;

        public SequenceAssignSequenceResultToVar(SequenceVariable destVar, Sequence sequence)
            : base(SequenceType.AssignSequenceResultToVar, destVar)
        {
            Seq = sequence;
        }

        public SequenceAssignSequenceResultToVar(SequenceType seqType, SequenceVariable destVar, Sequence sequence)
            : base(seqType, destVar)
        {
            Seq = sequence;
        }

        protected SequenceAssignSequenceResultToVar(SequenceAssignSequenceResultToVar that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
            Seq = that.Seq.Copy(originalToCopy, procEnv);
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceAssignSequenceResultToVar(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            Seq.Check(env);
            if(!TypesHelper.IsSameOrSubtype(DestVar.Type, "boolean", env.Model))
                throw new SequenceParserExceptionTypeMismatch("sequence => " + DestVar.Name, "boolean", DestVar.Type);
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            bool result = Seq.Apply(procEnv);
            return Assign(result, procEnv);
        }

        public override SequenceBase GetCurrentlyExecutedSequenceBase()
        {
            if(Seq.GetCurrentlyExecutedSequenceBase() != null)
                return Seq.GetCurrentlyExecutedSequenceBase();
            if(executionState == SequenceExecutionState.Underway)
                return this;
            return null;
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors, SequenceBase target)
        {
            DestVar.GetLocalVariables(variables);
            if(Seq.GetLocalVariables(variables, constructors, target))
                return true;
            return this == target;
        }

        public override IEnumerable<Sequence> Children
        {
            get { yield return Seq; }
        }

        public override int Precedence
        {
            get { return 6; }
        }

        public virtual string OperatorSymbol
        {
            get { return " => "; }
        }

        public override string Symbol
        {
            get { return Seq.Symbol + OperatorSymbol + DestVar.Name; }
        }
    }

    public class SequenceOrAssignSequenceResultToVar : SequenceAssignSequenceResultToVar
    {
        public SequenceOrAssignSequenceResultToVar(SequenceVariable destVar, Sequence sequence)
            : base(SequenceType.OrAssignSequenceResultToVar, destVar, sequence)
        {
        }

        protected SequenceOrAssignSequenceResultToVar(SequenceOrAssignSequenceResultToVar that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceOrAssignSequenceResultToVar(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            Seq.Check(env);
            if(!TypesHelper.IsSameOrSubtype(DestVar.Type, "boolean", env.Model))
                throw new SequenceParserExceptionTypeMismatch("sequence |> " + DestVar.Name, "boolean", DestVar.Type);
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            bool result = Seq.Apply(procEnv);
            return Assign(result || (bool)DestVar.GetVariableValue(procEnv), procEnv);
        }

        public override string OperatorSymbol
        {
            get { return " |> "; }
        }
    }

    public class SequenceAndAssignSequenceResultToVar : SequenceAssignSequenceResultToVar
    {
        public SequenceAndAssignSequenceResultToVar(SequenceVariable destVar, Sequence sequence)
            : base(SequenceType.AndAssignSequenceResultToVar, destVar, sequence)
        {
        }

        protected SequenceAndAssignSequenceResultToVar(SequenceAndAssignSequenceResultToVar that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceAndAssignSequenceResultToVar(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            Seq.Check(env);
            if(!TypesHelper.IsSameOrSubtype(DestVar.Type, "boolean", env.Model))
                throw new SequenceParserExceptionTypeMismatch("sequence &> " + DestVar.Name, "boolean", DestVar.Type);
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            bool result = Seq.Apply(procEnv);
            return Assign(result && (bool)DestVar.GetVariableValue(procEnv), procEnv);
        }

        public override string OperatorSymbol
        {
            get { return " &> "; }
        }
    }

    public class SequenceLazyOrAll : SequenceNAry
    {
        public SequenceLazyOrAll(List<Sequence> sequences, bool choice)
            : base(SequenceType.LazyOrAll, sequences, choice)
        {
        }

        protected SequenceLazyOrAll(SequenceLazyOrAll that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceLazyOrAll(this, originalToCopy, procEnv);
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            List<Sequence> sequences = new List<Sequence>(Sequences);
            while(sequences.Count != 0)
            {
                int seqToExecute = randomGenerator.Next(sequences.Count);
                if(Choice && !Skip)
                    seqToExecute = procEnv.UserProxy.ChooseSequence(seqToExecute, sequences, this);
                bool result = sequences[seqToExecute].Apply(procEnv);
                sequences.Remove(sequences[seqToExecute]);
                if(result)
                {
                    Skip = false;
                    return true;
                }
            }
            Skip = false;
            return false;
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string OperatorSymbol
        {
            get { return "||"; }
        }
    }

    public class SequenceLazyAndAll : SequenceNAry
    {
        public SequenceLazyAndAll(List<Sequence> sequences, bool choice)
            : base(SequenceType.LazyAndAll, sequences, choice)
        {
        }

        protected SequenceLazyAndAll(SequenceLazyAndAll that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceLazyAndAll(this, originalToCopy, procEnv);
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            List<Sequence> sequences = new List<Sequence>(Sequences);
            while(sequences.Count != 0)
            {
                int seqToExecute = randomGenerator.Next(sequences.Count);
                if(Choice && !Skip)
                    seqToExecute = procEnv.UserProxy.ChooseSequence(seqToExecute, sequences, this);
                bool result = sequences[seqToExecute].Apply(procEnv);
                sequences.Remove(sequences[seqToExecute]);
                if(!result)
                {
                    Skip = false;
                    return false;
                }
            }
            Skip = false;
            return true;
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string OperatorSymbol
        {
            get { return "&&"; }
        }
    }

    public class SequenceStrictOrAll : SequenceNAry
    {
        public SequenceStrictOrAll(List<Sequence> sequences, bool choice)
            : base(SequenceType.StrictOrAll, sequences, choice)
        {
        }

        protected SequenceStrictOrAll(SequenceStrictOrAll that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceStrictOrAll(this, originalToCopy, procEnv);
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            bool result = false;
            List<Sequence> sequences = new List<Sequence>(Sequences);
            while(sequences.Count != 0)
            {
                int seqToExecute = randomGenerator.Next(sequences.Count);
                if(Choice && !Skip)
                    seqToExecute = procEnv.UserProxy.ChooseSequence(seqToExecute, sequences, this);
                result |= sequences[seqToExecute].Apply(procEnv);
                sequences.Remove(sequences[seqToExecute]);
            }
            Skip = false;
            return result;
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string OperatorSymbol
        {
            get { return "|"; }
        }
    }

    public class SequenceStrictAndAll : SequenceNAry
    {
        public SequenceStrictAndAll(List<Sequence> sequences, bool choice)
            : base(SequenceType.StrictAndAll, sequences, choice)
        {
        }

        protected SequenceStrictAndAll(SequenceStrictAndAll that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceStrictAndAll(this, originalToCopy, procEnv);
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            bool result = true;
            List<Sequence> sequences = new List<Sequence>(Sequences);
            while(sequences.Count != 0)
            {
                int seqToExecute = randomGenerator.Next(sequences.Count);
                if(Choice && !Skip)
                    seqToExecute = procEnv.UserProxy.ChooseSequence(seqToExecute, sequences, this);
                result &= sequences[seqToExecute].Apply(procEnv);
                sequences.Remove(sequences[seqToExecute]);
            }
            Skip = false;
            return result;
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string OperatorSymbol
        {
            get { return "&"; }
        }
    }

    public class SequenceWeightedOne : SequenceGeneralNAry
    {
        public readonly List<double> Numbers;

        public SequenceWeightedOne(List<Sequence> sequences, List<double> numbers, bool choice)
            : base(SequenceType.WeightedOne, sequences, choice)
        {
            Numbers = numbers;
            // map individual weights to a sequence of ascending intervals, with end-begin of each interval equalling the weight
            for(int i = Numbers.Count - 1; i >= 0; --i)
            {
                for(int j = i + 1; j < Numbers.Count; ++j)
                {
                    Numbers[j] += Numbers[i];
                }
            }
        }

        protected SequenceWeightedOne(SequenceWeightedOne that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
            Numbers = that.Numbers;
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceWeightedOne(this, originalToCopy, procEnv);
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            double pointToExecute = randomGenerator.NextDouble() * Numbers[Numbers.Count - 1];
            if(Choice)
                pointToExecute = procEnv.UserProxy.ChoosePoint(pointToExecute, this);
            return Sequences[GetSequenceFromPoint(pointToExecute)].Apply(procEnv);
        }

        public int GetSequenceFromPoint(double point)
        {
            for(int i = 0; i < Sequences.Count; ++i)
            {
                if(point <= Numbers[i])
                    return i;
            }
            return Sequences.Count - 1;
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public string OperatorSymbol
        {
            get { return "."; }
        }

        public override string Symbol
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(OperatorSymbol);
                sb.Append("(");
                bool first = true;
                for(int i=0; i<Sequences.Count; ++i)
                {
                    if(first)
                        first = false;
                    else
                        sb.Append(",");
                    sb.Append(Numbers[i]);
                    sb.Append(" ");
                    sb.Append(Sequences[i].Symbol);
                }
                sb.Append(")");
                return sb.ToString();
            }
        }
    }

    /// <summary>
    /// A sequence consisting of a list of subsequences in the form of rule calls (and inherited rule all and count rule all calls).
    /// Decision on order of execution by random, by user choice possible.
    /// First all the contained rules are matched, then they get rewritten
    /// </summary>
    public class SequenceSomeFromSet : SequenceGeneralNAry, IPatternMatchingConstruct
    {
        public IMatches[] Matches;
        public override bool Random { get { return chooseRandom; } set { chooseRandom = value; } }
        bool chooseRandom;

        public PatternMatchingConstructType ConstructType
        {
            get { return PatternMatchingConstructType.SomeFromSet; }
        }

        public SequenceSomeFromSet(List<Sequence> sequences, bool chooseRandom, bool choice)
            : base(SequenceType.SomeFromSet, sequences, choice)
        {
            this.chooseRandom = chooseRandom;
            Matches = new IMatches[Sequences.Count];
            for(int i = 0; i < Sequences.Count; ++i)
            {
                if(Sequences[i] is SequenceRuleAllCall)
                {
                    SequenceRuleAllCall ruleAll = (SequenceRuleAllCall)Sequences[i];
                    if(ruleAll.Choice)
                    {
                        ConsoleUI.outWriter.WriteLine("Warning: No user choice % available inside {<...>}, removing choice modificator from " + ruleAll.Symbol + " (user choice handled by $%{<...>} construct)");
                        ruleAll.Choice = false;
                    }
                }
            }
            foreach(SequenceRuleCall ruleCall in Sequences)
            {
                ruleCall.Parent = this;
            }
        }

        protected SequenceSomeFromSet(SequenceSomeFromSet that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
            chooseRandom = that.chooseRandom;
            Matches = new IMatches[that.Sequences.Count];
            foreach(SequenceRuleCall ruleCall in Sequences)
            {
                ruleCall.Parent = this;
            }
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceSomeFromSet(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            foreach(Sequence seqChild in Sequences)
            {
                seqChild.Check(env);
                if(seqChild is SequenceRuleAllCall
                    && ((SequenceRuleAllCall)seqChild).MinVarChooseRandom != null
                    && ((SequenceRuleAllCall)seqChild).MaxVarChooseRandom != null)
                {
                    throw new Exception("Sequence SomeFromSet (e.g. {<r1,[r2],$[r3>)} can't contain a select with variable from all construct (e.g. $v[r4], e.g. $v1,v2[r4])");
                }
                if(((SequenceRuleCall)seqChild).Subgraph != null)
                    throw new Exception("Sequence SomeFromSet (e.g. {<r1,[r2],$[r3>)} can't contain a call with subgraph prefix (e.g. sg.r4, e.g. $[sg.r4])");
            }
        }

        public bool IsNonRandomRuleAllCall(int rule)
        {
            return Sequences[rule] is SequenceRuleAllCall && !((SequenceRuleAllCall)Sequences[rule]).ChooseRandom;
        }

        public int NumTotalMatches
        {
            get
            {
                int numTotalMatches = 0;
                for(int i = 0; i < Sequences.Count; ++i)
                {
                    if(IsNonRandomRuleAllCall(i))
                        numTotalMatches += Math.Min(Matches[i].Count, 1);
                    else
                        numTotalMatches += Matches[i].Count;
                }
                return numTotalMatches;
            }
        }

        // maybe todo: replace by match-to-construct-index access
        public void FromTotalMatch(int totalMatch, out int rule, out int match)
        {
            int curMatch = 0;
            for(int i = 0; i < Sequences.Count; ++i)
            {
                rule = i;
                if(IsNonRandomRuleAllCall(i))
                {
                    if(Matches[i].Count > 0)
                    {
                        match = 0;
                        if(curMatch == totalMatch)
                            return;
                        ++curMatch;
                    }
                }
                else
                {
                    for(int j = 0; j < Matches[i].Count; ++j)
                    {
                        match = j;
                        if(curMatch == totalMatch)
                            return;
                        ++curMatch;
                    }
                }
            }
            throw new Exception("Internal error: can't computer rule and match from total match");
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            FireBeginExecutionEvent(procEnv);

            MatchAll(procEnv);

            if(NumTotalMatches == 0)
            {
                for(int i = 0; i < Sequences.Count; ++i)
                   Sequences[i].executionState = SequenceExecutionState.Fail;
                FireEndExecutionEvent(procEnv, null);
                return false;
            }

            bool[] special = new bool[Sequences.Count];
            for(int i = 0; i < Sequences.Count; ++i)
            {
                SequenceRuleCall rule = (SequenceRuleCall)Sequences[i];
                special[i] = rule.Special;
            }
            FireMatchedAfterFilteringEvent(procEnv, Matches, special); // only called on an existing match, as ApplyRule is only called in that case

            if(chooseRandom)
            {
                int totalMatchToExecute = randomGenerator.Next(NumTotalMatches);
                if(Choice)
                    totalMatchToExecute = procEnv.UserProxy.ChooseMatch(totalMatchToExecute, this);
                int ruleToExecute;
                int matchToExecute;
                FromTotalMatch(totalMatchToExecute, out ruleToExecute, out matchToExecute);
                SequenceRuleCall rule = (SequenceRuleCall)Sequences[ruleToExecute];
                IMatch match = Matches[ruleToExecute].GetMatch(matchToExecute);
                if(rule is SequenceRuleAllCall && ((SequenceRuleAllCall)rule).ChooseRandom)
                    ApplyRule(rule, procEnv, Matches[ruleToExecute], Matches[ruleToExecute].GetMatch(matchToExecute));
                else
                    ApplyRule(rule, procEnv, Matches[ruleToExecute], null);
                for(int i = 0; i < Sequences.Count; ++i)
                {
                    Sequences[i].executionState = Matches[i].Count == 0 ? SequenceExecutionState.Fail : Sequences[i].executionState;
                }
                Sequences[ruleToExecute].executionState = SequenceExecutionState.Success; // ApplyRule removed the match from the matches
            }
            else
            {
                for(int i = 0; i < Sequences.Count; ++i)
                {
                    if(Matches[i].Count > 0)
                    {
                        SequenceRuleCall rule = (SequenceRuleCall)Sequences[i];
                        ApplyRule(rule, procEnv, Matches[i], null);
                    }
                    else
                        Sequences[i].executionState = SequenceExecutionState.Fail;
                }
            }

            FireFinishedEvent(procEnv, Matches, special);
            FireEndExecutionEvent(procEnv, null);
            return true;
        }

        public void MatchAll(IGraphProcessingEnvironment procEnv)
        {
            SequenceRuleCall[] rules = new SequenceRuleCall[Sequences.Count];

            for(int i = 0; i < Sequences.Count; ++i)
            {
                if(!(Sequences[i] is SequenceRuleCall))
                    throw new InvalidOperationException("Internal error: some from set containing non-rule sequences");

                SequenceRuleCall rule = (SequenceRuleCall)Sequences[i];
                rules[i] = rule;
            }

            List<IMatch> matchList;
            Dictionary<IMatch, int> matchToConstructIndex;
            SequenceMultiRuleAllCall.MatchAll(procEnv, rules, true,
                out Matches, out matchList, out matchToConstructIndex);
        }

        protected bool ApplyRule(SequenceRuleCall rule, IGraphProcessingEnvironment procEnv, IMatches matches, IMatch match)
        {
            bool result;
            rule.FireEnteringSequenceEvent(procEnv);
            rule.executionState = SequenceExecutionState.Underway;
#if LOG_SEQUENCE_EXECUTION
            procEnv.Recorder.WriteLine("Before executing sequence " + rule.Id + ": " + rule.Symbol);
#endif
            result = rule.Rewrite(procEnv, matches, match);
#if LOG_SEQUENCE_EXECUTION
            procEnv.Recorder.WriteLine("After executing sequence " + rule.Id + ": " + rule.Symbol + " result " + result);
#endif
            rule.executionState = result ? SequenceExecutionState.Success : SequenceExecutionState.Fail;
            rule.FireExitingSequenceEvent(procEnv);
            return result;
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("{<");
                bool first = true;
                for(int i = 0; i < Sequences.Count; ++i)
                {
                    if(first)
                        first = false;
                    else
                        sb.Append(",");
                    sb.Append(Sequences[i].Symbol);
                }
                sb.Append(">}");
                return sb.ToString();
            }
        }
    }

    /// <summary>
    /// A sequence consisting of a list of subsequences in the form of rule calls.
    /// First all the contained rules are matched, then they get rewritten.
    /// </summary>
    public class SequenceMultiRuleAllCall : Sequence, IPatternMatchingConstruct
    {
        public readonly List<Sequence> Sequences;
        public readonly List<SequenceFilterCallBase> Filters;

        public PatternMatchingConstructType ConstructType
        {
            get { return PatternMatchingConstructType.MultiRuleAllCall; }
        }

        public SequenceMultiRuleAllCall(List<Sequence> sequences)
            : base(SequenceType.MultiRuleAllCall)
        {
            Sequences = sequences;
            Filters = new List<SequenceFilterCallBase>();
            foreach(SequenceRuleCall ruleCall in Sequences)
            {
                ruleCall.Parent = this;
            }
        }

        protected SequenceMultiRuleAllCall(SequenceMultiRuleAllCall that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            Sequences = new List<Sequence>();
            foreach(Sequence seq in that.Sequences)
            {
                Sequences.Add(seq.Copy(originalToCopy, procEnv));
            }
            Filters = that.Filters;
            foreach(SequenceRuleCall ruleCall in Sequences)
            {
                ruleCall.Parent = this;
            }
        }

        public void AddFilterCall(SequenceFilterCallBase sequenceFilterCall)
        {
            Filters.Add(sequenceFilterCall);
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceMultiRuleAllCall(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            List<SequenceRuleCall> RuleCalls = new List<SequenceRuleCall>();
            foreach(Sequence seqChild in Sequences)
            {
                seqChild.Check(env);
                if(seqChild is SequenceRuleAllCall)
                    throw new Exception("Sequence MultiRuleAllCall (e.g. [[r1,r2(x),(y)=r3]] can't contain a rule all call (e.g. [r4]");
                if(seqChild is SequenceRuleCountAllCall)
                    throw new Exception("Sequence MultiRuleAllCall (e.g. [[r1,r2(x),(y)=r3]] can't contain a rule count all call (e.g. count[r4] => ct");
                if(((SequenceRuleCall)seqChild).Subgraph != null)
                    throw new Exception("Sequence MultiRuleAllCall (e.g. [[r1,r2(x),(y)=r3]]  can't contain a call with subgraph prefix (e.g. sg.r4)");
                RuleCalls.Add((SequenceRuleCall)seqChild);
            }

            env.CheckMatchClassFilterCalls(Filters, RuleCalls);
        }

        private static List<object[]> Copy(List<object[]> returnValues)
        {
            List<object[]> copy = new List<object[]>();
            foreach(object[] array in returnValues)
            {
                copy.Add((object[])array.Clone());
            }
            return copy;
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            FireBeginExecutionEvent(procEnv);

            IMatches[] MatchesArray;
            List<IMatch> MatchList;
            Dictionary<IMatch, int> MatchToConstructIndex;
            MatchAll(procEnv,
                out MatchesArray, out MatchList, out MatchToConstructIndex);

            foreach(SequenceFilterCallBase filter in Filters)
            {
                SequenceFilterCallInterpreted filterInterpreted = (SequenceFilterCallInterpreted)filter;
                filterInterpreted.Execute(procEnv, MatchList);
            }

            bool[] SpecialArray = new bool[Sequences.Count];
            for(int i = 0; i < Sequences.Count; ++i)
            {
                SequenceRuleCall rule = (SequenceRuleCall)Sequences[i];
                SpecialArray[i] = rule.Special;
            }
            MatchListHelper.RemoveUnavailable(MatchList, MatchesArray);
            FireMatchedAfterFilteringEvent(procEnv, MatchesArray, SpecialArray);

            List<List<object[]>> ReturnValues = new List<List<object[]>>();
            List<int> ResultNums = new List<int>();
            for(int i = 0; i < Sequences.Count; ++i)
            {
                SequenceRuleCall rule = (SequenceRuleCall)Sequences[i];
                IMatches matches = MatchesArray[i];
                if(matches.Count == 0)
                    rule.executionState = SequenceExecutionState.Fail;

                ReturnValues.Add(Copy(matches.Producer.Reserve(matches.Count))); // performance todo: only clone if a rule appears multiple times
                ResultNums.Add(0);
            }

            foreach(IMatch match in MatchList)
            {
                int constructIndex = MatchToConstructIndex[match];
                SequenceRuleCall rule = (SequenceRuleCall)Sequences[constructIndex];
                IMatches matches = MatchesArray[constructIndex];
                List<object[]> returnValues = ReturnValues[constructIndex];
                int resultNum = ResultNums[constructIndex];
                ApplyMatch(rule, procEnv, matches, match, returnValues, ref resultNum);
                ResultNums[constructIndex] = resultNum;
            }

            for(int i = 0; i < Sequences.Count; ++i)
            {
                SequenceRuleCall rule = (SequenceRuleCall)Sequences[i];
                IMatches matches = MatchesArray[i];
                List<object[]> returnValues = ReturnValues[i];
                FillReturnVariablesFromValues(rule.ReturnVars, matches.Producer, procEnv, returnValues, -1);
            }

            FireFinishedEvent(procEnv, MatchesArray, SpecialArray);
            FireEndExecutionEvent(procEnv, null);
            return MatchList.Count > 0;
        }

        public void MatchAll(IGraphProcessingEnvironment procEnv,
            out IMatches[] MatchesArray, out List<IMatch> MatchList, out Dictionary<IMatch, int> MatchToConstructIndex)
        {
            SequenceRuleCall[] rules = new SequenceRuleCall[Sequences.Count];

            for(int i = 0; i < Sequences.Count; ++i)
            {
                if(!(Sequences[i] is SequenceRuleCall))
                    throw new InvalidOperationException("Internal error: multi rule all call containing non-rule sequences");

                SequenceRuleCall rule = (SequenceRuleCall)Sequences[i];
                rules[i] = rule;
            }

            MatchAll(procEnv, rules, false, out MatchesArray, out MatchList, out MatchToConstructIndex);
        }

        public static void MatchAll(IGraphProcessingEnvironment procEnv, SequenceRuleCall[] rules, bool defineMaxMatches,
            out IMatches[] MatchesArray, out List<IMatch> MatchList, out Dictionary<IMatch, int> MatchToConstructIndex)
        {
            ActionCall[] actions = new ActionCall[rules.Length]; // performance TODO: don't allocate, use buffer like with arguments
            for(int i = 0; i < rules.Length; ++i)
            {
                SequenceRuleCall rule = rules[i];

                FillArgumentsFromArgumentExpressions(rule.ArgumentExpressions, rule.Arguments, procEnv);

                IAction action = null;
                if(rule is SequenceRuleAllCallInterpreted)
                    action = ((SequenceRuleAllCallInterpreted)rule).Action;
                else if(rule is SequenceRuleCountAllCallInterpreted)
                    action = ((SequenceRuleCountAllCallInterpreted)rule).Action;
                else
                    action = ((SequenceRuleCallInterpreted)rule).Action;

                int maxMatches = procEnv.MaxMatches;
                if(defineMaxMatches)
                {
                    maxMatches = 1;
                    if(rule is SequenceRuleAllCall || rule is SequenceRuleCountAllCall)
                        maxMatches = procEnv.MaxMatches;
                }

                actions[i] = new ActionCall(action, maxMatches, rule.Arguments); // performance TODO: don't allocate, use buffer like with arguments
            }

            MatchesArray = procEnv.MatchForQuery(FireDebugEvents, actions);

            for(int i = 0; i < rules.Length; ++i)
            {
                SequenceRuleCall rule = rules[i];

                IAction action = null;
                if(rule is SequenceRuleAllCallInterpreted)
                    action = ((SequenceRuleAllCallInterpreted)rule).Action;
                else if(rule is SequenceRuleCountAllCallInterpreted)
                    action = ((SequenceRuleCountAllCallInterpreted)rule).Action;
                else
                    action = ((SequenceRuleCallInterpreted)rule).Action;

                IMatches matches = MatchesArray[i];
                for(int j = 0; j < rule.Filters.Count; ++j)
                {
                    SequenceFilterCallInterpreted filter = (SequenceFilterCallInterpreted)rule.Filters[j];
                    filter.Execute(procEnv, action, matches);
                }
            }

            MatchList = new List<IMatch>();
            MatchToConstructIndex = new Dictionary<IMatch, int>();
            MatchListHelper.Add(MatchList, MatchesArray, MatchToConstructIndex);
        }

        public bool ApplyMatch(SequenceRuleCall rule, IGraphProcessingEnvironment procEnv, IMatches matches, IMatch match, List<object[]> returnValues, ref int curResultNum)
        {
            bool result;
            rule.FireEnteringSequenceEvent(procEnv);
            rule.executionState = SequenceExecutionState.Underway;
#if LOG_SEQUENCE_EXECUTION
            procEnv.Recorder.WriteLine("Before executing sequence " + rule.Id + ": " + rule.Symbol);
#endif
            FireMatchSelectedEvent(procEnv, match, rule.Special, matches);
            result = RewriteMatch(rule, procEnv, matches, match, returnValues, ref curResultNum);
#if LOG_SEQUENCE_EXECUTION
            procEnv.Recorder.WriteLine("After executing sequence " + rule.Id + ": " + rule.Symbol + " result " + result);
#endif
            rule.executionState = result ? SequenceExecutionState.Success : SequenceExecutionState.Fail;
            rule.FireExitingSequenceEvent(procEnv);
            return result;
        }

        public bool RewriteMatch(SequenceRuleCall rule, IGraphProcessingEnvironment procEnv, IMatches matches, IMatch match, List<object[]> returnValues, ref int curResultNum)
        {
            if(matches.Count == 0)
                return false;
            if(rule.Test)
                return false;

#if DEBUGACTIONS || MATCHREWRITEDETAIL
            procEnv.PerformanceInfo.StartLocal();
#endif

            FireRewritingSelectedMatchEvent(procEnv);
            object[] retElems = matches.Producer.Modify(procEnv, match);
            object[] curResult = returnValues[curResultNum];
            for(int i = 0; i < retElems.Length; ++i)
            {
                curResult[i] = retElems[i];
            }
            ++procEnv.PerformanceInfo.RewritesPerformed;
            ++curResultNum;

#if DEBUGACTIONS || MATCHREWRITEDETAIL
            procEnv.PerformanceInfo.StopRewrite(); // total rewrite time does NOT include listeners anymore
#endif
            FireFinishedSelectedMatchEvent(procEnv);

#if LOG_SEQUENCE_EXECUTION
            procEnv.Recorder.WriteLine("Matched/Applied " + Symbol);
            procEnv.Recorder.Flush();
#endif

            return true;
        }

        public override SequenceBase GetCurrentlyExecutedSequenceBase()
        {
            foreach(Sequence seq in Sequences)
            {
                if(seq.GetCurrentlyExecutedSequenceBase() != null)
                    return seq.GetCurrentlyExecutedSequenceBase();
            }
            if(executionState == SequenceExecutionState.Underway)
                return this;
            return null;
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors, SequenceBase target)
        {
            foreach(Sequence seq in Sequences)
            {
                if(seq.GetLocalVariables(variables, constructors, target))
                    return true;
            }
            GetLocalVariables(Filters, variables, constructors);
            return this == target;
        }

        public override IEnumerable<Sequence> Children
        {
            get
            {
                foreach(Sequence seq in Sequences)
                {
                    yield return seq;
                }
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public string FilterSymbol
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach(SequenceFilterCallBase filterCall in Filters)
                {
                    sb.Append("\\").Append(filterCall.ToString());
                }
                return sb.ToString();
            }
        }

        public string CoreSymbol
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                bool first = true;
                foreach(Sequence seq in Sequences)
                {
                    if(first)
                        first = false;
                    else
                        sb.Append(",");
                    sb.Append(seq.Symbol);
                }
                return sb.ToString();
            }
        }

        public string CoreSymbolNoTestPrefix
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                bool first = true;
                foreach(Sequence seq in Sequences)
                {
                    if(first)
                        first = false;
                    else
                        sb.Append(",");
                    sb.Append(((SequenceRuleCall)seq).SymbolNoTestPrefix);
                }
                return sb.ToString();
            }
        }

        public override string Symbol
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("[[");
                sb.Append(CoreSymbol);
                sb.Append("]");
                sb.Append(FilterSymbol);
                sb.Append("]");
                return sb.ToString();
            }
        }
    }

    public class SequenceRulePrefixedSequence : Sequence, IPatternMatchingConstruct
    {
        public readonly SequenceRuleCall Rule;
        public readonly Sequence Sequence;
        public readonly List<SequenceVariable> VariablesFallingOutOfScopeOnLeaving;

        public PatternMatchingConstructType ConstructType
        {
            get { return PatternMatchingConstructType.RulePrefixedSequence; }
        }

        public SequenceRulePrefixedSequence(SequenceRuleCall rule, Sequence sequence,
            List<SequenceVariable> variablesFallingOutOfScopeOnLeaving)
            : base(SequenceType.RulePrefixedSequence)
        {
            Rule = rule;
            Sequence = sequence;
            VariablesFallingOutOfScopeOnLeaving = variablesFallingOutOfScopeOnLeaving;
            Rule.Parent = this;
        }

        protected SequenceRulePrefixedSequence(SequenceRulePrefixedSequence that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            Rule = (SequenceRuleCall)that.Rule.Copy(originalToCopy, procEnv);
            Sequence = that.Sequence.Copy(originalToCopy, procEnv);
            VariablesFallingOutOfScopeOnLeaving = CopyVars(originalToCopy, procEnv, that.VariablesFallingOutOfScopeOnLeaving);
            Rule.Parent = this;
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceRulePrefixedSequence(this, originalToCopy, procEnv);
        }

        public override SequenceBase GetCurrentlyExecutedSequenceBase()
        {
            if(Rule.GetCurrentlyExecutedSequenceBase() != null)
                return Rule.GetCurrentlyExecutedSequenceBase();
            if(Sequence.GetCurrentlyExecutedSequenceBase() != null)
                return Sequence.GetCurrentlyExecutedSequenceBase();
            if(executionState == SequenceExecutionState.Underway)
                return this;
            return null;
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            try
            {
                bool result = false;
#if LOG_SEQUENCE_EXECUTION
                procEnv.Recorder.WriteLine("Applying rule prefixed sequence " + GetRuleCallString(procEnv));
#endif
                FireBeginExecutionEvent(procEnv);

                IMatches matches = Match(procEnv);

                if(matches.Count == 0)
                {
                    Rule.executionState = SequenceExecutionState.Fail;
                    FireEndExecutionEvent(procEnv, null);
                    return false;
                }

                FireMatchedAfterFilteringEvent(procEnv, matches, Rule.Special);

#if LOG_SEQUENCE_EXECUTION
                if(res)
                {
                    procEnv.Recorder.WriteLine("Matched/Applied " + Symbol);
                    procEnv.Recorder.Flush();
                }
#endif

                // the rule might be called again in the sequence, overwriting the matches object of the action
                // normally it's safe to assume the rule is not called again until its matches were processed,
                // allowing for the one matches object memory optimization, but here we must clone to prevent bad side effect
                // TODO: optimization; if it's ensured the sequence doesn't call this action again, we can omit this, requires call analysis
                matches = matches.Clone();

                int matchesTried = 0;
                foreach(IMatch match in matches)
                {
                    ++matchesTried;
#if LOG_SEQUENCE_EXECUTION
                    procEnv.Recorder.WriteLine("Applying match " + matchesTried + "/" + matches.Count + " of " + Rule.GetRuleCallString(procEnv));
                    procEnv.Recorder.WriteLine("match: " + MatchPrinter.ToString(match, procEnv.Graph, ""));
#endif
                    Rule.FireEnteringSequenceEvent(procEnv);
                    Rule.executionState = SequenceExecutionState.Underway;

                    Rule.Rewrite(procEnv, matches, match);

                    Rule.executionState = SequenceExecutionState.Success;
                    Rule.FireExitingSequenceEvent(procEnv);

                    result |= Sequence.Apply(procEnv);

                    if(matchesTried < matches.Count)
                    {
                        procEnv.EndOfIteration(true, this);
                        Rule.ResetExecutionState();
                        Sequence.ResetExecutionState();
                        continue;
                    }
                    else
                    {
#if LOG_SEQUENCE_EXECUTION
                        procEnv.Recorder.WriteLine("Applying match exhausted " + rule.GetRuleCallString(procEnv));
#endif
                        procEnv.EndOfIteration(false, this);
                        FireFinishedEvent(procEnv, matches, Rule.Special);
                        FireEndExecutionEvent(procEnv, null);
                        return result;
                    }
                }

                FireFinishedEvent(procEnv, matches, Rule.Special);
                FireEndExecutionEvent(procEnv, null);
                return result;
            }
            catch(NullReferenceException)
            {
                ConsoleUI.errorOutWriter.WriteLine("Null reference exception during rule prefixed sequence execution (null parameter?): " + Symbol);
                throw;
            }
        }

        public IMatches Match(IGraphProcessingEnvironment procEnv)
        {
            int maxMatches = procEnv.MaxMatches;

            FillArgumentsFromArgumentExpressions(Rule.ArgumentExpressions, Rule.Arguments, procEnv);

            SequenceRuleCallInterpreted ruleInterpreted = (SequenceRuleCallInterpreted)Rule;
            IMatches matches = procEnv.MatchWithoutEvent(ruleInterpreted.Action, Rule.Arguments, maxMatches, FireDebugEvents);

            for(int i = 0; i < Rule.Filters.Count; ++i)
            {
                SequenceFilterCallInterpreted filter = (SequenceFilterCallInterpreted)Rule.Filters[i];
                filter.Execute(procEnv, ruleInterpreted.Action, matches);
            }

            return matches;
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors, SequenceBase target)
        {
            if(Rule.GetLocalVariables(variables, constructors, target))
                return true;
            if(Sequence.GetLocalVariables(variables, constructors, target))
                return true;
            RemoveVariablesFallingOutOfScope(variables, VariablesFallingOutOfScopeOnLeaving);
            return this == target;
        }

        public override IEnumerable<Sequence> Children
        {
            get
            {
                yield return Rule;
                yield return Sequence;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "for{" + Rule.Symbol + ";" + Sequence.Symbol + "}"; }
        }
    }

    public class SequenceMultiRulePrefixedSequence : Sequence, IPatternMatchingConstruct
    {
        public readonly List<SequenceRulePrefixedSequence> RulePrefixedSequences;
        public readonly List<SequenceFilterCallBase> Filters;

        public PatternMatchingConstructType ConstructType
        {
            get { return PatternMatchingConstructType.MultiRulePrefixedSequence; }
        }

        public SequenceMultiRulePrefixedSequence(List<SequenceRulePrefixedSequence> rulePrefixedSequences) : base(SequenceType.MultiRulePrefixedSequence)
        {
            RulePrefixedSequences = rulePrefixedSequences;
            Filters = new List<SequenceFilterCallBase>();
            foreach(SequenceRulePrefixedSequence rulePrefixedSequence in RulePrefixedSequences)
            {
                rulePrefixedSequence.Rule.Parent = this;
            }
        }

        protected SequenceMultiRulePrefixedSequence(SequenceMultiRulePrefixedSequence that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            RulePrefixedSequences = new List<SequenceRulePrefixedSequence>();
            foreach(SequenceRulePrefixedSequence rulePrefixedSequence in that.RulePrefixedSequences)
            {
                RulePrefixedSequences.Add((SequenceRulePrefixedSequence)rulePrefixedSequence.Copy(originalToCopy, procEnv));
            }
            Filters = that.Filters;
            foreach(SequenceRulePrefixedSequence rulePrefixedSequence in RulePrefixedSequences)
            {
                rulePrefixedSequence.Rule.Parent = this;
            }
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceMultiRulePrefixedSequence(this, originalToCopy, procEnv);
        }

        public void AddFilterCall(SequenceFilterCallBase sequenceFilterCall)
        {
            Filters.Add(sequenceFilterCall);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env);

            List<SequenceRuleCall> RuleCalls = new List<SequenceRuleCall>();
            foreach(SequenceRulePrefixedSequence seqChild in RulePrefixedSequences)
            {
                RuleCalls.Add(seqChild.Rule);
            }

            env.CheckMatchClassFilterCalls(Filters, RuleCalls);
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            // first get all matches of the rule
#if LOG_SEQUENCE_EXECUTION
            procEnv.Recorder.WriteLine("Matching rule prefixed multi sequence " + GetRuleCallString(procEnv));
#endif
            FireBeginExecutionEvent(procEnv);

            IMatches[] MatchesArray;
            List<IMatch> MatchList;
            Dictionary<IMatch, int> MatchToConstructIndex;
            MatchAll(procEnv,
                out MatchesArray, out MatchList, out MatchToConstructIndex);

            foreach(SequenceFilterCallBase filter in Filters)
            {
                SequenceFilterCallInterpreted filterInterpreted = (SequenceFilterCallInterpreted)filter;
                filterInterpreted.Execute(procEnv, MatchList);
            }

            int matchesCount = MatchList.Count;
            if(matchesCount == 0)
            {
                // todo: sequence, single rules?
                foreach(SequenceRulePrefixedSequence rulePrefixedSequence in RulePrefixedSequences)
                {
                    rulePrefixedSequence.executionState = SequenceExecutionState.Fail;
                }
                executionState = SequenceExecutionState.Fail;
                FireEndExecutionEvent(procEnv, null);
                return false;
            }

            bool[] SpecialArray = new bool[RulePrefixedSequences.Count];
            for(int i = 0; i < RulePrefixedSequences.Count; ++i)
            {
                SequenceRuleCall rule = (SequenceRuleCall)RulePrefixedSequences[i].Rule;
                SpecialArray[i] = rule.Special;
            }
            MatchListHelper.RemoveUnavailable(MatchList, MatchesArray);
            FireMatchedAfterFilteringEvent(procEnv, MatchesArray, SpecialArray);

#if LOG_SEQUENCE_EXECUTION
            for(int i = 0; i < matchesCount; ++i)
            {
                procEnv.Recorder.WriteLine("match " + i + ": " + MatchPrinter.ToString(MatchList[i], procEnv.Graph, ""));
            }
#endif

            // cloning already occurred to allow multiple calls of the same rule

            // apply the rule and its sequence for every match found
            int matchesTried = 0;

            for(int i = 0; i < RulePrefixedSequences.Count; ++i)
            {
                SequenceRuleCall rule = (SequenceRuleCall)RulePrefixedSequences[i].Rule;
                IMatches matches = MatchesArray[i];
                if(matches.Count == 0)
                    rule.executionState = SequenceExecutionState.Fail;
            }

            bool result = false;
            foreach(IMatch match in MatchList)
            {
                ++matchesTried;
#if LOG_SEQUENCE_EXECUTION
                procEnv.Recorder.WriteLine("Applying match " + matchesTried + "/" + matchesCount + " of " + rule.GetRuleCallString(procEnv));
                procEnv.Recorder.WriteLine("match: " + MatchPrinter.ToString(match, procEnv.Graph, ""));
#endif

                int constructIndex = MatchToConstructIndex[match];
                SequenceRuleCall rule = (SequenceRuleCall)RulePrefixedSequences[constructIndex].Rule;
                Sequence seq = RulePrefixedSequences[constructIndex].Sequence;
                IMatches matches = MatchesArray[constructIndex];

                rule.FireEnteringSequenceEvent(procEnv);
                rule.executionState = SequenceExecutionState.Underway;
#if LOG_SEQUENCE_EXECUTION
                procEnv.Recorder.WriteLine("Before executing sequence " + rule.Id + ": " + rule.Symbol);
#endif
                rule.Rewrite(procEnv, matches, match);
#if LOG_SEQUENCE_EXECUTION
                procEnv.Recorder.WriteLine("After executing sequence " + rule.Id + ": " + rule.Symbol + " result " + result);
#endif
                rule.executionState = SequenceExecutionState.Success;
                rule.FireExitingSequenceEvent(procEnv);

                // rule applied, now execute its sequence
                result |= seq.Apply(procEnv);

                if(matchesTried < matchesCount)
                {
                    procEnv.EndOfIteration(true, this);
                    rule.ResetExecutionState();
                    seq.ResetExecutionState();
                }
                else
                {
#if LOG_SEQUENCE_EXECUTION
                    procEnv.Recorder.WriteLine("Applying match exhausted " + rule.GetRuleCallString(procEnv));
#endif
                    procEnv.EndOfIteration(false, this);
                }
            }

            FireFinishedEvent(procEnv, MatchesArray, SpecialArray);
            FireEndExecutionEvent(procEnv, null);
            return result;
        }

        public void MatchAll(IGraphProcessingEnvironment procEnv,
            out IMatches[] MatchesArray, out List<IMatch> MatchList, out Dictionary<IMatch, int> MatchToConstructIndex)
        {
            SequenceRuleCall[] rules = new SequenceRuleCall[RulePrefixedSequences.Count];

            for(int i = 0; i < RulePrefixedSequences.Count; ++i)
            {
                if(!(RulePrefixedSequences[i] is SequenceRulePrefixedSequence))
                    throw new InvalidOperationException("Internal error: rule prefixed multi sequence containing non-rule prefixed sequence");

                SequenceRulePrefixedSequence rulePrefixedSequence = (SequenceRulePrefixedSequence)RulePrefixedSequences[i];
                SequenceRuleCall rule = rulePrefixedSequence.Rule;
                rules[i] = rule;
            }

            SequenceMultiRuleAllCall.MatchAll(procEnv, rules, false,
                out MatchesArray, out MatchList, out MatchToConstructIndex);
        }

        public override SequenceBase GetCurrentlyExecutedSequenceBase()
        {
            foreach(SequenceRulePrefixedSequence rulePrefixedSequence in RulePrefixedSequences)
            {
                if(rulePrefixedSequence.GetCurrentlyExecutedSequenceBase() != null)
                    return rulePrefixedSequence.GetCurrentlyExecutedSequenceBase();
            }
            if(executionState == SequenceExecutionState.Underway)
                return this;
            return null;
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors, SequenceBase target)
        {
            foreach(SequenceRulePrefixedSequence rulePrefixedSequence in RulePrefixedSequences)
            {
                if(rulePrefixedSequence.GetLocalVariables(variables, constructors, target))
                    return true;
            }
            GetLocalVariables(Filters, variables, constructors);
            return this == target;
        }

        public override IEnumerable<Sequence> Children
        {
            get
            {
                foreach(SequenceRulePrefixedSequence rulePrefixedSequence in RulePrefixedSequences)
                {
                    yield return rulePrefixedSequence;
                }
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public string FilterSymbol
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach(SequenceFilterCallBase filterCall in Filters)
                {
                    sb.Append("\\").Append(filterCall.ToString());
                }
                return sb.ToString();
            }
        }

        public string CoreSymbol
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                bool first = true;
                foreach(SequenceRulePrefixedSequence rulePrefixedSequence in RulePrefixedSequences)
                {
                    if(first)
                        first = false;
                    else
                        sb.Append(",");
                    sb.Append(rulePrefixedSequence.Symbol);
                }
                return sb.ToString();
            }
        }

        public override string Symbol
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("[[");
                sb.Append(CoreSymbol);
                sb.Append("]");
                sb.Append(FilterSymbol);
                sb.Append("]");
                return sb.ToString();
            }
        }
    }

    public class SequenceTransaction : SequenceUnary
    {
        public SequenceTransaction(Sequence seq) : base(SequenceType.Transaction, seq)
        {
        }

        protected SequenceTransaction(SequenceTransaction that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceTransaction(this, originalToCopy, procEnv);
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            int transactionID = procEnv.TransactionManager.Start();
            int oldRewritesPerformed = procEnv.PerformanceInfo.RewritesPerformed;

            bool res = Seq.Apply(procEnv);

            if(res)
                procEnv.TransactionManager.Commit(transactionID);
            else
            {
                procEnv.TransactionManager.Rollback(transactionID);
                procEnv.PerformanceInfo.RewritesPerformed = oldRewritesPerformed;
            }

            return res;
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "<" + Seq.Symbol + ">"; }
        }
    }

    public class SequenceBacktrack : Sequence, IPatternMatchingConstruct
    {
        public readonly SequenceRuleCall Rule;
        public readonly Sequence Seq;

        public PatternMatchingConstructType ConstructType
        {
            get { return PatternMatchingConstructType.Backtrack; }
        }

        public SequenceBacktrack(Sequence seqRule, Sequence seq) : base(SequenceType.Backtrack)
        {
            Rule = (SequenceRuleCall)seqRule;
            Seq = seq;
            Rule.Parent = this;
        }

        protected SequenceBacktrack(SequenceBacktrack that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            Rule = (SequenceRuleCall)that.Rule.Copy(originalToCopy, procEnv);
            Seq = that.Seq.Copy(originalToCopy, procEnv);
            Rule.Parent = this;
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceBacktrack(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            if(Rule is SequenceRuleAllCall)
                throw new Exception("Sequence Backtrack can't contain a bracketed rule all call");
            if(Rule is SequenceRuleCountAllCall)
                throw new Exception("Sequence Backtrack can't contain a counted rule all call");
            if(Rule.Test)
                throw new Exception("Sequence Backtrack can't contain a call to a rule reduced to a test");
            if(Rule.Subgraph != null)
                throw new Exception("Sequence Backtrack can't employ a call with subgraph prefix (no <<sg.r; seq>> possible)");
            base.Check(env);
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            // first get all matches of the rule
#if LOG_SEQUENCE_EXECUTION
            procEnv.Recorder.WriteLine("Matching backtrack all " + Rule.GetRuleCallString(procEnv));
#endif
            FireBeginExecutionEvent(procEnv);

            FillArgumentsFromArgumentExpressions(Rule.ArgumentExpressions, Rule.Arguments, procEnv);

            SequenceRuleCallInterpreted ruleInterpreted = (SequenceRuleCallInterpreted)Rule;
            IMatches matches = procEnv.MatchWithoutEvent(ruleInterpreted.Action, Rule.Arguments, procEnv.MaxMatches, FireDebugEvents);

            for(int i = 0; i < Rule.Filters.Count; ++i)
            {
                SequenceFilterCallInterpreted filter = (SequenceFilterCallInterpreted)Rule.Filters[i];
                filter.Execute(procEnv, ruleInterpreted.Action, matches);
            }

            if(matches.Count == 0)
            {
                Rule.executionState = SequenceExecutionState.Fail;
                FireEndExecutionEvent(procEnv, null);
                return false;
            }

            FireMatchedAfterFilteringEvent(procEnv, matches, Rule.Special);

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

#if LOG_SEQUENCE_EXECUTION
            for(int i = 0; i < matches.Count; ++i)
            {
                procEnv.Recorder.WriteLine("cloned match " + i + ": " + MatchPrinter.ToString(matches.GetMatch(i), procEnv.Graph, ""));
            }
#endif

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
                int transactionID = procEnv.TransactionManager.Start();
                int oldRewritesPerformed = procEnv.PerformanceInfo.RewritesPerformed;

                Rule.FireEnteringSequenceEvent(procEnv);
                Rule.executionState = SequenceExecutionState.Underway;
#if LOG_SEQUENCE_EXECUTION
                procEnv.Recorder.WriteLine("Before executing sequence " + Rule.Id + ": " + Rule.Symbol);
#endif
                bool result = Rule.Rewrite(procEnv, matches, match);
#if LOG_SEQUENCE_EXECUTION
                procEnv.Recorder.WriteLine("After executing sequence " + Rule.Id + ": " + Rule.Symbol + " result " + result);
#endif
                Rule.executionState = result ? SequenceExecutionState.Success : SequenceExecutionState.Fail;
                Rule.FireExitingSequenceEvent(procEnv);

                // rule applied, now execute the sequence
                result = Seq.Apply(procEnv);

                // if sequence execution failed, roll the changes back and try the next match of the rule
                if(!result)
                {
                    procEnv.TransactionManager.Rollback(transactionID);
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
                        FireFinishedEvent(procEnv, matches, Rule.Special);
                        FireEndExecutionEvent(procEnv, null);
                        return false;
                    }
                }

                // if sequence execution succeeded, commit the changes so far and succeed
                procEnv.TransactionManager.Commit(transactionID);
                procEnv.EndOfIteration(false, this);
                FireFinishedEvent(procEnv, matches, Rule.Special);
                FireEndExecutionEvent(procEnv, null);
                return true;
            }

            return false; // to satisfy the compiler, we return from inside the loop
        }

        public override SequenceBase GetCurrentlyExecutedSequenceBase()
        {
            if(Rule.GetCurrentlyExecutedSequenceBase() != null)
                return Rule.GetCurrentlyExecutedSequenceBase();
            if(Seq.GetCurrentlyExecutedSequenceBase() != null)
                return Seq.GetCurrentlyExecutedSequenceBase();
            if(executionState == SequenceExecutionState.Underway)
                return this;
            return null;
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors, SequenceBase target)
        {
            if(Rule.GetLocalVariables(variables, constructors, target))
                return true;
            if(Seq.GetLocalVariables(variables, constructors, target))
                return true;
            return this == target;
        }

        public override IEnumerable<Sequence> Children
        {
            get
            {
                yield return Rule;
                yield return Seq;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "<< " + Rule.Symbol + ";;" + Seq.Symbol + ">>"; }
        }
    }

    public class SequenceMultiBacktrack : Sequence, IPatternMatchingConstruct
    {
        public readonly SequenceMultiRuleAllCall Rules;
        public readonly Sequence Seq;

        public PatternMatchingConstructType ConstructType
        {
            get { return PatternMatchingConstructType.MultiBacktrack; }
        }

        public SequenceMultiBacktrack(SequenceMultiRuleAllCall seqMulti, Sequence seq) : base(SequenceType.MultiBacktrack)
        {
            Rules = seqMulti;
            Seq = seq;
            foreach(SequenceRuleCall ruleCall in Rules.Sequences)
            {
                ruleCall.Parent = this;
            }
        }

        protected SequenceMultiBacktrack(SequenceMultiBacktrack that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            Rules = (SequenceMultiRuleAllCall)that.Rules.Copy(originalToCopy, procEnv);
            Seq = that.Seq.Copy(originalToCopy, procEnv);
            foreach(SequenceRuleCall ruleCall in Rules.Sequences)
            {
                ruleCall.Parent = this;
            }
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceMultiBacktrack(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env);
            Rules.Check(env);
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            // first get all matches of the rule
#if LOG_SEQUENCE_EXECUTION
            procEnv.Recorder.WriteLine("Matching multi backtrack all " + Rule.GetRuleCallString(procEnv));
#endif
            FireBeginExecutionEvent(procEnv);

            IMatches[] MatchesArray;
            List<IMatch> MatchList;
            Dictionary<IMatch, int> MatchToConstructIndex;
            Rules.MatchAll(procEnv,
                out MatchesArray, out MatchList, out MatchToConstructIndex);

            foreach(SequenceFilterCallBase filter in Rules.Filters)
            {
                SequenceFilterCallInterpreted filterInterpreted = (SequenceFilterCallInterpreted)filter;
                filterInterpreted.Execute(procEnv, MatchList);
            }

            int matchesCount = MatchList.Count;
            if(matchesCount == 0)
            {
                // todo: sequence, single rules?
                Rules.executionState = SequenceExecutionState.Fail;
                FireEndExecutionEvent(procEnv, null);
                return false;
            }

            bool[] SpecialArray = new bool[Rules.Sequences.Count];
            for(int i = 0; i < Rules.Sequences.Count; ++i)
            {
                SequenceRuleCall rule = (SequenceRuleCall)Rules.Sequences[i];
                SpecialArray[i] = rule.Special; 
            }
            MatchListHelper.RemoveUnavailable(MatchList, MatchesArray);
            FireMatchedAfterFilteringEvent(procEnv, MatchesArray, SpecialArray);

#if LOG_SEQUENCE_EXECUTION
            for(int i = 0; i < matchesCount; ++i)
            {
                procEnv.Recorder.WriteLine("match " + i + ": " + MatchPrinter.ToString(MatchList[i], procEnv.Graph, ""));
            }
#endif

            // cloning already occurred to allow multiple calls of the same rule

            // apply the rule and the following sequence for every match found,
            // until the first rule and sequence execution succeeded
            // rolling back the changes of failing executions until then
            int matchesTried = 0;

            for(int i = 0; i < Rules.Sequences.Count; ++i)
            {
                SequenceRuleCall rule = (SequenceRuleCall)Rules.Sequences[i];
                IMatches matches = MatchesArray[i];
                if(matches.Count == 0)
                    rule.executionState = SequenceExecutionState.Fail;
            }

            foreach(IMatch match in MatchList)
            {
                ++matchesTried;
#if LOG_SEQUENCE_EXECUTION
                procEnv.Recorder.WriteLine("Applying backtrack match " + matchesTried + "/" + matchesCount + " of " + rule.GetRuleCallString(procEnv));
                procEnv.Recorder.WriteLine("match: " + MatchPrinter.ToString(match, procEnv.Graph, ""));
#endif

                // start a transaction
                int transactionID = procEnv.TransactionManager.Start();
                int oldRewritesPerformed = procEnv.PerformanceInfo.RewritesPerformed;

                int constructIndex = MatchToConstructIndex[match];
                SequenceRuleCall rule = (SequenceRuleCall)Rules.Sequences[constructIndex];
                IMatches matches = MatchesArray[constructIndex];

                rule.FireEnteringSequenceEvent(procEnv);
                rule.executionState = SequenceExecutionState.Underway;
#if LOG_SEQUENCE_EXECUTION
                procEnv.Recorder.WriteLine("Before executing sequence " + rule.Id + ": " + rule.Symbol);
#endif
                bool result = rule.Rewrite(procEnv, matches, match);
#if LOG_SEQUENCE_EXECUTION
                procEnv.Recorder.WriteLine("After executing sequence " + rule.Id + ": " + rule.Symbol + " result " + result);
#endif
                rule.executionState = result ? SequenceExecutionState.Success : SequenceExecutionState.Fail;
                rule.FireExitingSequenceEvent(procEnv);

                // rule applied, now execute the sequence
                result = Seq.Apply(procEnv);

                // if sequence execution failed, roll the changes back and try the next match of the rule
                if(!result)
                {
                    procEnv.TransactionManager.Rollback(transactionID);
                    procEnv.PerformanceInfo.RewritesPerformed = oldRewritesPerformed;
                    if(matchesTried < matchesCount)
                    {
                        procEnv.EndOfIteration(true, this);
                        rule.ResetExecutionState();
                        Seq.ResetExecutionState();
                        continue;
                    }
                    else
                    {
                        // all matches tried, all failed later on -> end in fail
#if LOG_SEQUENCE_EXECUTION
                        procEnv.Recorder.WriteLine("Applying backtrack match exhausted " + rule.GetRuleCallString(procEnv));
#endif
                        procEnv.EndOfIteration(false, this);
                        FireFinishedEvent(procEnv, MatchesArray, SpecialArray);
                        FireEndExecutionEvent(procEnv, null);
                        return false;
                    }
                }

                // if sequence execution succeeded, commit the changes so far and succeed
                procEnv.TransactionManager.Commit(transactionID);
                procEnv.EndOfIteration(false, this);
                FireFinishedEvent(procEnv, MatchesArray, SpecialArray);
                FireEndExecutionEvent(procEnv, null);
                return true;
            }

            return false; // to satisfy the compiler, we return from inside the loop
        }

        public override SequenceBase GetCurrentlyExecutedSequenceBase()
        {
            if(Rules.GetCurrentlyExecutedSequenceBase() != null)
                return Rules.GetCurrentlyExecutedSequenceBase();
            if(Seq.GetCurrentlyExecutedSequenceBase() != null)
                return Seq.GetCurrentlyExecutedSequenceBase();
            if(executionState == SequenceExecutionState.Underway)
                return this;
            return null;
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors, SequenceBase target)
        {
            if(Rules.GetLocalVariables(variables, constructors, target))
                return true;
            if(Seq.GetLocalVariables(variables, constructors, target))
                return true;
            return this == target;
        }

        public override IEnumerable<Sequence> Children
        {
            get
            {
                foreach(Sequence child in Rules.Children)
                {
                    yield return child;
                }
                yield return Seq;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "<<" + Rules.Symbol + ";;" + Seq.Symbol + ">>"; }
        }
    }

    public class SequenceMultiSequenceBacktrack : Sequence, IPatternMatchingConstruct
    {
        public readonly SequenceMultiRulePrefixedSequence MultiRulePrefixedSequence;

        public PatternMatchingConstructType ConstructType
        {
            get { return PatternMatchingConstructType.MultiSequenceBacktrack; }
        }

        public SequenceMultiSequenceBacktrack(SequenceMultiRulePrefixedSequence multiRulePrefixedSequence) : base(SequenceType.MultiSequenceBacktrack)
        {
            MultiRulePrefixedSequence = multiRulePrefixedSequence;
            foreach(SequenceRulePrefixedSequence rulePrefixedSequence in MultiRulePrefixedSequence.RulePrefixedSequences)
            {
                rulePrefixedSequence.Rule.Parent = this;
            }
        }

        protected SequenceMultiSequenceBacktrack(SequenceMultiSequenceBacktrack that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            MultiRulePrefixedSequence = ((SequenceMultiRulePrefixedSequence)that.MultiRulePrefixedSequence.Copy(originalToCopy, procEnv));
            foreach(SequenceRulePrefixedSequence rulePrefixedSequence in MultiRulePrefixedSequence.RulePrefixedSequences)
            {
                rulePrefixedSequence.Rule.Parent = this;
            }
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceMultiSequenceBacktrack(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env);
            MultiRulePrefixedSequence.Check(env);
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            // first get all matches of the rule
#if LOG_SEQUENCE_EXECUTION
            procEnv.Recorder.WriteLine("Matching multi rule prefixed backtrack " + GetRuleCallString(procEnv));
#endif

            FireBeginExecutionEvent(procEnv);

            IMatches[] MatchesArray;
            List<IMatch> MatchList;
            Dictionary<IMatch, int> MatchToConstructIndex;
            MultiRulePrefixedSequence.MatchAll(procEnv,
                out MatchesArray, out MatchList, out MatchToConstructIndex);

            foreach(SequenceFilterCallBase filter in MultiRulePrefixedSequence.Filters)
            {
                SequenceFilterCallInterpreted filterInterpreted = (SequenceFilterCallInterpreted)filter;
                filterInterpreted.Execute(procEnv, MatchList);
            }

            int matchesCount = MatchList.Count;
            if(matchesCount == 0)
            {
                // todo: sequence, single rules?
                executionState = SequenceExecutionState.Fail;
                FireEndExecutionEvent(procEnv, null);
                return false;
            }

            bool[] SpecialArray = new bool[MultiRulePrefixedSequence.RulePrefixedSequences.Count];
            for(int i = 0; i < MultiRulePrefixedSequence.RulePrefixedSequences.Count; ++i)
            {
                SequenceRuleCall rule = (SequenceRuleCall)MultiRulePrefixedSequence.RulePrefixedSequences[i].Rule;
                SpecialArray[i] = rule.Special;
            }
            MatchListHelper.RemoveUnavailable(MatchList, MatchesArray);
            FireMatchedAfterFilteringEvent(procEnv, MatchesArray, SpecialArray);

#if LOG_SEQUENCE_EXECUTION
            for(int i = 0; i < matchesCount; ++i)
            {
                procEnv.Recorder.WriteLine("match " + i + ": " + MatchPrinter.ToString(MatchList[i], procEnv.Graph, ""));
            }
#endif

            // cloning already occurred to allow multiple calls of the same rule

            // apply the rule and its sequence for every match found,
            // until the first rule and sequence execution succeeded
            // rolling back the changes of failing executions until then
            int matchesTried = 0;

            for(int i = 0; i < MultiRulePrefixedSequence.RulePrefixedSequences.Count; ++i)
            {
                SequenceRuleCall rule = (SequenceRuleCall)MultiRulePrefixedSequence.RulePrefixedSequences[i].Rule;
                IMatches matches = MatchesArray[i];
                if(matches.Count == 0)
                    rule.executionState = SequenceExecutionState.Fail;
            }

            foreach(IMatch match in MatchList)
            {
                ++matchesTried;
#if LOG_SEQUENCE_EXECUTION
                procEnv.Recorder.WriteLine("Applying backtrack match " + matchesTried + "/" + matchesCount + " of " + rule.GetRuleCallString(procEnv));
                procEnv.Recorder.WriteLine("match: " + MatchPrinter.ToString(match, procEnv.Graph, ""));
#endif

                // start a transaction
                int transactionID = procEnv.TransactionManager.Start();
                int oldRewritesPerformed = procEnv.PerformanceInfo.RewritesPerformed;

                int constructIndex = MatchToConstructIndex[match];
                SequenceRuleCall rule = (SequenceRuleCall)MultiRulePrefixedSequence.RulePrefixedSequences[constructIndex].Rule;
                Sequence seq = MultiRulePrefixedSequence.RulePrefixedSequences[constructIndex].Sequence;
                IMatches matches = MatchesArray[constructIndex];

                rule.FireEnteringSequenceEvent(procEnv);
                rule.executionState = SequenceExecutionState.Underway;
#if LOG_SEQUENCE_EXECUTION
                procEnv.Recorder.WriteLine("Before executing sequence " + rule.Id + ": " + rule.Symbol);
#endif
                rule.Rewrite(procEnv, matches, match);
#if LOG_SEQUENCE_EXECUTION
                procEnv.Recorder.WriteLine("After executing sequence " + rule.Id + ": " + rule.Symbol + " result " + result);
#endif
                rule.executionState = SequenceExecutionState.Success;
                rule.FireExitingSequenceEvent(procEnv);

                // rule applied, now execute its sequence
                bool result = seq.Apply(procEnv);

                // if sequence execution failed, roll the changes back and try the next match of the rule
                if(!result)
                {
                    procEnv.TransactionManager.Rollback(transactionID);
                    procEnv.PerformanceInfo.RewritesPerformed = oldRewritesPerformed;
                    if(matchesTried < matchesCount)
                    {
                        procEnv.EndOfIteration(true, this);
                        rule.ResetExecutionState();
                        seq.ResetExecutionState();
                        continue;
                    }
                    else
                    {
                        // all matches tried, all failed later on -> end in fail
#if LOG_SEQUENCE_EXECUTION
                        procEnv.Recorder.WriteLine("Applying backtrack match exhausted " + rule.GetRuleCallString(procEnv));
#endif
                        procEnv.EndOfIteration(false, this);
                        FireFinishedEvent(procEnv, MatchesArray, SpecialArray);
                        FireEndExecutionEvent(procEnv, null);
                        return false;
                    }
                }

                // if sequence execution succeeded, commit the changes so far and succeed
                procEnv.TransactionManager.Commit(transactionID);
                procEnv.EndOfIteration(false, this);
                FireFinishedEvent(procEnv, MatchesArray, SpecialArray);
                FireEndExecutionEvent(procEnv, null);
                return true;
            }

            return false; // to satisfy the compiler, we return from inside the loop
        }

        public override SequenceBase GetCurrentlyExecutedSequenceBase()
        {
            if(MultiRulePrefixedSequence.GetCurrentlyExecutedSequenceBase() != null)
                return MultiRulePrefixedSequence.GetCurrentlyExecutedSequenceBase();
            if(executionState == SequenceExecutionState.Underway)
                return this;
            return null;
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors, SequenceBase target)
        {
            if(MultiRulePrefixedSequence.GetLocalVariables(variables, constructors, target))
                return true;
            return this == target;
        }

        public override IEnumerable<Sequence> Children
        {
            get { yield return MultiRulePrefixedSequence; }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "<<" + MultiRulePrefixedSequence.Symbol + ">>"; }
        }
    }

    public class SequencePause : SequenceUnary
    {
        public SequencePause(Sequence seq)
            : base(SequenceType.Pause, seq)
        {
        }

        protected SequencePause(SequencePause that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequencePause(this, originalToCopy, procEnv);
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            procEnv.TransactionManager.Pause();

            bool res = Seq.Apply(procEnv);

            procEnv.TransactionManager.Resume();

            return res;
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "/ " + Seq.Symbol + " /"; }
        }
    }

    public class SequenceIfThenElse : Sequence
    {
        public readonly Sequence Condition;
        public readonly Sequence TrueCase;
        public readonly Sequence FalseCase;

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

        protected SequenceIfThenElse(SequenceIfThenElse that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            Condition = that.Condition.Copy(originalToCopy, procEnv);
            TrueCase = that.TrueCase.Copy(originalToCopy, procEnv);
            FalseCase = that.FalseCase.Copy(originalToCopy, procEnv);
            VariablesFallingOutOfScopeOnLeavingIf = CopyVars(originalToCopy, procEnv, that.VariablesFallingOutOfScopeOnLeavingIf);
            VariablesFallingOutOfScopeOnLeavingTrueCase = CopyVars(originalToCopy, procEnv, that.VariablesFallingOutOfScopeOnLeavingTrueCase);
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceIfThenElse(this, originalToCopy, procEnv);
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            return Condition.Apply(procEnv) ? TrueCase.Apply(procEnv) : FalseCase.Apply(procEnv);
        }

        public override SequenceBase GetCurrentlyExecutedSequenceBase()
        {
            if(Condition.GetCurrentlyExecutedSequenceBase() != null)
                return Condition.GetCurrentlyExecutedSequenceBase();
            if(TrueCase.GetCurrentlyExecutedSequenceBase() != null)
                return TrueCase.GetCurrentlyExecutedSequenceBase();
            if(FalseCase.GetCurrentlyExecutedSequenceBase() != null)
                return FalseCase.GetCurrentlyExecutedSequenceBase();
            if(executionState == SequenceExecutionState.Underway)
                return this;
            return null;
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors, SequenceBase target)
        {
            if(Condition.GetLocalVariables(variables, constructors, target))
                return true;
            if(TrueCase.GetLocalVariables(variables, constructors, target))
                return true;
            RemoveVariablesFallingOutOfScope(variables, VariablesFallingOutOfScopeOnLeavingTrueCase);
            if(FalseCase.GetLocalVariables(variables, constructors, target))
                return true;
            RemoveVariablesFallingOutOfScope(variables, VariablesFallingOutOfScopeOnLeavingIf);
            return this == target;
        }

        public override IEnumerable<Sequence> Children
        {
            get
            {
                yield return Condition;
                yield return TrueCase;
                yield return FalseCase;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "if{" + Condition.Symbol + ";" + TrueCase.Symbol + ";" + FalseCase.Symbol + "}"; }
        }
    }

    public class SequenceIfThen : Sequence
    {
        public readonly Sequence Left;
        public readonly Sequence Right;
        public readonly List<SequenceVariable> VariablesFallingOutOfScopeOnLeavingIf;
        public readonly List<SequenceVariable> VariablesFallingOutOfScopeOnLeavingTrueCase;

        public SequenceIfThen(Sequence condition, Sequence trueCase,
            List<SequenceVariable> variablesFallingOutOfScopeOnLeavingIf,
            List<SequenceVariable> variablesFallingOutOfScopeOnLeavingTrueCase)
            : base(SequenceType.IfThen)
        {
            Left = condition;
            Right = trueCase;
            VariablesFallingOutOfScopeOnLeavingIf = variablesFallingOutOfScopeOnLeavingIf;
            VariablesFallingOutOfScopeOnLeavingTrueCase = variablesFallingOutOfScopeOnLeavingTrueCase;
        }

        protected SequenceIfThen(SequenceIfThen that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            Left = that.Left.Copy(originalToCopy, procEnv);
            Right = that.Right.Copy(originalToCopy, procEnv);
            VariablesFallingOutOfScopeOnLeavingIf = CopyVars(originalToCopy, procEnv, that.VariablesFallingOutOfScopeOnLeavingIf);
            VariablesFallingOutOfScopeOnLeavingTrueCase = CopyVars(originalToCopy, procEnv, that.VariablesFallingOutOfScopeOnLeavingTrueCase);
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceIfThen(this, originalToCopy, procEnv);
        }

        public override SequenceBase GetCurrentlyExecutedSequenceBase()
        {
            if(Left.GetCurrentlyExecutedSequenceBase() != null)
                return Left.GetCurrentlyExecutedSequenceBase();
            if(Right.GetCurrentlyExecutedSequenceBase() != null)
                return Right.GetCurrentlyExecutedSequenceBase();
            if(executionState == SequenceExecutionState.Underway)
                return this;
            return null;
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            return Left.Apply(procEnv) ? Right.Apply(procEnv) : true; // lazy implication
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors, SequenceBase target)
        {
            if(Left.GetLocalVariables(variables, constructors, target))
                return true;
            if(Right.GetLocalVariables(variables, constructors, target))
                return true;
            RemoveVariablesFallingOutOfScope(variables, VariablesFallingOutOfScopeOnLeavingTrueCase);
            RemoveVariablesFallingOutOfScope(variables, VariablesFallingOutOfScopeOnLeavingIf);
            return this == target;
        }

        public override IEnumerable<Sequence> Children
        {
            get
            {
                yield return Left;
                yield return Right;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "if{" + Left.Symbol + ";" + Right.Symbol + "}"; }
        }
    }

    public class SequenceForContainer : SequenceUnary
    {
        public readonly SequenceVariable Var;
        public readonly SequenceVariable VarDst;
        public readonly SequenceVariable Container;

        public readonly List<SequenceVariable> VariablesFallingOutOfScopeOnLeavingFor;

        public SequenceForContainer(SequenceVariable var, SequenceVariable varDst, SequenceVariable container, Sequence seq,
            List<SequenceVariable> variablesFallingOutOfScopeOnLeavingFor)
            : base(SequenceType.ForContainer, seq)
        {
            Var = var;
            VarDst = varDst;
            Container = container;
            VariablesFallingOutOfScopeOnLeavingFor = variablesFallingOutOfScopeOnLeavingFor;
        }

        protected SequenceForContainer(SequenceForContainer that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
            Var = that.Var.Copy(originalToCopy, procEnv);
            if(that.VarDst != null)
                VarDst = that.VarDst.Copy(originalToCopy, procEnv);
            Container = that.Container.Copy(originalToCopy, procEnv);
            VariablesFallingOutOfScopeOnLeavingFor = CopyVars(originalToCopy, procEnv, that.VariablesFallingOutOfScopeOnLeavingFor);
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceForContainer(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env);
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            if(Container.GetVariableValue(procEnv) is IList)
                return ApplyImplArray(procEnv, (IList)Container.GetVariableValue(procEnv));
            else if(Container.GetVariableValue(procEnv) is IDeque)
                return ApplyImplDeque(procEnv, (IDeque)Container.GetVariableValue(procEnv));
            else
                return ApplyImplSetMap(procEnv, (IDictionary)Container.GetVariableValue(procEnv));
        }

        private bool ApplyImplArray(IGraphProcessingEnvironment procEnv, IList array)
        {
            bool res = true;

            bool first = true;
            for(int i = 0; i < array.Count; ++i)
            {
                if(!first)
                    procEnv.EndOfIteration(true, this);
                if(VarDst != null)
                {
                    Var.SetVariableValue(i, procEnv);
                    VarDst.SetVariableValue(array[i], procEnv);
                }
                else
                    Var.SetVariableValue(array[i], procEnv);
                Seq.ResetExecutionState();
                res &= Seq.Apply(procEnv);
                first = false;
            }
            procEnv.EndOfIteration(false, this);

            return res;
        }

        private bool ApplyImplDeque(IGraphProcessingEnvironment procEnv, IDeque deque)
        {
            bool res = true;

            bool first = true;
            for(int i = 0; i < deque.Count; ++i)
            {
                if(!first)
                    procEnv.EndOfIteration(true, this);
                if(VarDst != null)
                {
                    Var.SetVariableValue(i, procEnv);
                    VarDst.SetVariableValue(deque[i], procEnv);
                }
                else
                    Var.SetVariableValue(deque[i], procEnv);
                Seq.ResetExecutionState();
                res &= Seq.Apply(procEnv);
                first = false;
            }
            procEnv.EndOfIteration(false, this);

            return res;
        }

        private bool ApplyImplSetMap(IGraphProcessingEnvironment procEnv, IDictionary setmap)
        {
            bool res = true;

            bool first = true;
            foreach(DictionaryEntry entry in setmap)
            {
                if(!first)
                    procEnv.EndOfIteration(true, this);
                Var.SetVariableValue(entry.Key, procEnv);
                if(VarDst != null)
                    VarDst.SetVariableValue(entry.Value, procEnv);
                Seq.ResetExecutionState();
                res &= Seq.Apply(procEnv);
                first = false;
            }
            procEnv.EndOfIteration(false, this);

            return res;
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables, 
            List<SequenceExpressionConstructor> constructors, SequenceBase target)
        {
            Var.GetLocalVariables(variables);
            if(VarDst != null)
                VarDst.GetLocalVariables(variables);
            if(Seq.GetLocalVariables(variables, constructors, target))
                return true;
            RemoveVariablesFallingOutOfScope(variables, VariablesFallingOutOfScopeOnLeavingFor);
            if(VarDst != null)
                variables.Remove(VarDst);
            variables.Remove(Var);
            return this == target;
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "for{" + Var.Name + (VarDst != null ? "->" + VarDst.Name : "") + " in " + Container.Name + ";" + Seq.Symbol + "}"; }
        }
    }

    public class SequenceForIntegerRange : SequenceUnary
    {
        public readonly SequenceVariable Var;
        public readonly SequenceExpression Left;
        public readonly SequenceExpression Right;

        public readonly List<SequenceVariable> VariablesFallingOutOfScopeOnLeavingFor;

        public SequenceForIntegerRange(SequenceVariable var, SequenceExpression left, SequenceExpression right, Sequence seq,
            List<SequenceVariable> variablesFallingOutOfScopeOnLeavingFor)
            : base(SequenceType.ForIntegerRange, seq)
        {
            Var = var;
            Left = left;
            Right = right;
            VariablesFallingOutOfScopeOnLeavingFor = variablesFallingOutOfScopeOnLeavingFor;
        }

        protected SequenceForIntegerRange(SequenceForIntegerRange that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
            Var = that.Var.Copy(originalToCopy, procEnv);
            Left = that.Left.CopyExpression(originalToCopy, procEnv);
            Right = that.Right.CopyExpression(originalToCopy, procEnv);
            VariablesFallingOutOfScopeOnLeavingFor = CopyVars(originalToCopy, procEnv, that.VariablesFallingOutOfScopeOnLeavingFor);
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceForIntegerRange(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            if(!TypesHelper.IsSameOrSubtype(Var.Type, "int", env.Model))
                throw new SequenceParserExceptionTypeMismatch(Symbol + " - " + Var.Name, "int", Var.Type);
            if(!TypesHelper.IsSameOrSubtype(Left.Type(env), "int", env.Model))
                throw new SequenceParserExceptionTypeMismatch(Symbol + ", left bound" + Var.Name, "int", Var.Type);
            if(!TypesHelper.IsSameOrSubtype(Right.Type(env), "int", env.Model))
                throw new SequenceParserExceptionTypeMismatch(Symbol + ", right bound" + Var.Name, "int", Var.Type);

            Left.Check(env);
            Right.Check(env);

            base.Check(env);
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            bool res = true;

            int entry = (int)Left.Evaluate(procEnv);
            int limit = (int)Right.Evaluate(procEnv);
            bool ascending = entry <= limit;

            bool first = true;
            while(ascending ? entry <= limit : entry >= limit)
            {
                if(!first)
                    procEnv.EndOfIteration(true, this);
                Var.SetVariableValue(entry, procEnv);
                Seq.ResetExecutionState();
                res &= Seq.Apply(procEnv);
                if(ascending)
                    ++entry;
                else
                    --entry;;
                first = false;
            }
            procEnv.EndOfIteration(false, this);

            return res;
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors, SequenceBase target)
        {
            Var.GetLocalVariables(variables);
            Left.GetLocalVariables(variables, constructors);
            Right.GetLocalVariables(variables, constructors);
            if(Seq.GetLocalVariables(variables, constructors, target))
                return true;
            RemoveVariablesFallingOutOfScope(variables, VariablesFallingOutOfScopeOnLeavingFor);
            variables.Remove(Var);
            return this == target;
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "for{" + Var.Name + " in [" + Left.Symbol + ":" + Right.Symbol + "]; " + Seq.Symbol + "}"; }
        }
    }

    public class SequenceForIndexAccessEquality : SequenceUnary
    {
        public readonly SequenceVariable Var;
        public readonly String IndexName;
        public readonly SequenceExpression Expr;

        public readonly List<SequenceVariable> VariablesFallingOutOfScopeOnLeavingFor;

        public bool EmitProfiling;

        public SequenceForIndexAccessEquality(SequenceVariable var, String indexName, SequenceExpression expr, 
            Sequence seq, List<SequenceVariable> variablesFallingOutOfScopeOnLeavingFor)
            : base(SequenceType.ForIndexAccessEquality, seq)
        {
            Var = var;
            IndexName = indexName;
            Expr = expr;
            VariablesFallingOutOfScopeOnLeavingFor = variablesFallingOutOfScopeOnLeavingFor;
        }

        protected SequenceForIndexAccessEquality(SequenceForIndexAccessEquality that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
            Var = that.Var.Copy(originalToCopy, procEnv);
            Expr = that.Expr.CopyExpression(originalToCopy, procEnv);
            VariablesFallingOutOfScopeOnLeavingFor = CopyVars(originalToCopy, procEnv, that.VariablesFallingOutOfScopeOnLeavingFor);
            EmitProfiling = that.EmitProfiling;
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceForIndexAccessEquality(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            if(Var.Type == "")
                throw new SequenceParserExceptionTypeMismatch(Var.Name, "a node or edge type", "statically unknown type");
            if(!TypesHelper.IsSameOrSubtype(Var.Type, "Node", env.Model) && !TypesHelper.IsSameOrSubtype(Var.Type, "AEdge", env.Model))
                throw new SequenceParserExceptionTypeMismatch(Symbol + " - " + Var.Name, "Node or Edge", Var.Type);
            
            Expr.Check(env);
            
            base.Check(env);
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            bool res = true;
            IAttributeIndex index = (IAttributeIndex)procEnv.Graph.Indices.GetIndex(IndexName);
            bool first = true;
            foreach(IGraphElement elem in index.LookupElements(Expr.Evaluate(procEnv)))
            {
                if(EmitProfiling)
                    ++procEnv.PerformanceInfo.SearchSteps;
                if(!first)
                    procEnv.EndOfIteration(true, this);
                Var.SetVariableValue(elem, procEnv);
                Seq.ResetExecutionState();
                res &= Seq.Apply(procEnv);
                first = false;
            }
            procEnv.EndOfIteration(false, this);
            return res;
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors, SequenceBase target)
        {
            Var.GetLocalVariables(variables);
            Expr.GetLocalVariables(variables, constructors);
            if(Seq.GetLocalVariables(variables, constructors, target))
                return true;
            RemoveVariablesFallingOutOfScope(variables, VariablesFallingOutOfScopeOnLeavingFor);
            variables.Remove(Var);
            return this == target;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            EmitProfiling = profiling;
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "for{" + Var.Name + " in { " + IndexName + " == " + Expr.Symbol + " }; " + Seq.Symbol + "}"; }
        }
    }

    public class SequenceForIndexAccessOrdering : SequenceUnary
    {
        public readonly SequenceVariable Var;
        public readonly bool Ascending;
        public readonly String IndexName;
        public readonly SequenceExpression Expr;
        public readonly RelOpDirection Direction;
        public readonly SequenceExpression Expr2;
        public readonly RelOpDirection Direction2;

        public readonly List<SequenceVariable> VariablesFallingOutOfScopeOnLeavingFor;

        public bool EmitProfiling;

        public SequenceForIndexAccessOrdering(SequenceVariable var, bool ascending, String indexName, 
            SequenceExpression expr, RelOpDirection dir, SequenceExpression expr2, RelOpDirection dir2,
            Sequence seq, List<SequenceVariable> variablesFallingOutOfScopeOnLeavingFor)
            : base(SequenceType.ForIndexAccessOrdering, seq)
        {
            Var = var;
            Ascending = ascending;
            IndexName = indexName;
            Expr = expr;
            Direction = dir;
            Expr2 = expr2;
            Direction2 = dir2;
            VariablesFallingOutOfScopeOnLeavingFor = variablesFallingOutOfScopeOnLeavingFor;
        }

        protected SequenceForIndexAccessOrdering(SequenceForIndexAccessOrdering that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
            Var = that.Var.Copy(originalToCopy, procEnv);
            Ascending = that.Ascending;
            IndexName = that.IndexName;
            Expr = that.Expr != null ? that.Expr.CopyExpression(originalToCopy, procEnv) : null;
            Direction = that.Direction;
            Expr2 = that.Expr2 != null ? that.Expr2.CopyExpression(originalToCopy, procEnv) : null;
            Direction2 = that.Direction2;
            VariablesFallingOutOfScopeOnLeavingFor = CopyVars(originalToCopy, procEnv, that.VariablesFallingOutOfScopeOnLeavingFor);
            EmitProfiling = that.EmitProfiling;
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceForIndexAccessOrdering(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            if(Direction == RelOpDirection.Smaller || Direction == RelOpDirection.SmallerEqual)
            {
                if(Expr2 != null && (Direction2 == RelOpDirection.Smaller || Direction2 == RelOpDirection.SmallerEqual))
                    throw new SequenceParserExceptionIndexTwoLowerBounds(IndexName);
            }
            if(Direction == RelOpDirection.Greater || Direction == RelOpDirection.GreaterEqual)
            {
                if(Expr2 != null && (Direction2 == RelOpDirection.Greater || Direction2 == RelOpDirection.GreaterEqual))
                    throw new SequenceParserExceptionIndexTwoUpperBounds(IndexName);
            }

            if(Expr != null)
                Expr.Check(env);
            if(Expr2 != null)
                Expr2.Check(env);

            base.Check(env);
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            bool res = true;
            IAttributeIndex index = (IAttributeIndex)procEnv.Graph.Indices.GetIndex(IndexName);
            IEnumerable<IGraphElement> enumerator;
            if(Ascending)
            {
                if(From() != null && To() != null)
                {
                    if(IncludingFrom())
                    {
                        if(IncludingTo())
                            enumerator = index.LookupElementsAscendingFromInclusiveToInclusive(From().Evaluate(procEnv), To().Evaluate(procEnv));
                        else
                            enumerator = index.LookupElementsAscendingFromInclusiveToExclusive(From().Evaluate(procEnv), To().Evaluate(procEnv));
                    }
                    else
                    {
                        if(IncludingTo())
                            enumerator = index.LookupElementsAscendingFromExclusiveToInclusive(From().Evaluate(procEnv), To().Evaluate(procEnv));
                        else
                            enumerator = index.LookupElementsAscendingFromExclusiveToExclusive(From().Evaluate(procEnv), To().Evaluate(procEnv));
                    }
                }
                else if(From() != null)
                {
                    if(IncludingFrom())
                        enumerator = index.LookupElementsAscendingFromInclusive(From().Evaluate(procEnv));
                    else
                        enumerator = index.LookupElementsAscendingFromExclusive(From().Evaluate(procEnv));
                }
                else if(To() != null)
                {
                    if(IncludingTo())
                        enumerator = index.LookupElementsAscendingToInclusive(To().Evaluate(procEnv));
                    else
                        enumerator = index.LookupElementsAscendingToExclusive(To().Evaluate(procEnv));
                }
                else
                    enumerator = index.LookupElementsAscending();
            }
            else
            {
                if(From() != null && To() != null)
                {
                    if(IncludingFrom())
                    {
                        if(IncludingTo())
                            enumerator = index.LookupElementsDescendingFromInclusiveToInclusive(From().Evaluate(procEnv), To().Evaluate(procEnv));
                        else
                            enumerator = index.LookupElementsDescendingFromInclusiveToExclusive(From().Evaluate(procEnv), To().Evaluate(procEnv));
                    }
                    else
                    {
                        if(IncludingTo())
                            enumerator = index.LookupElementsDescendingFromExclusiveToInclusive(From().Evaluate(procEnv), To().Evaluate(procEnv));
                        else
                            enumerator = index.LookupElementsDescendingFromExclusiveToExclusive(From().Evaluate(procEnv), To().Evaluate(procEnv));
                    }
                }
                else if(From() != null)
                {
                    if(IncludingFrom())
                        enumerator = index.LookupElementsDescendingFromInclusive(From().Evaluate(procEnv));
                    else
                        enumerator = index.LookupElementsDescendingFromExclusive(From().Evaluate(procEnv));
                }
                else if(To() != null)
                {
                    if(IncludingTo())
                        enumerator = index.LookupElementsDescendingToInclusive(To().Evaluate(procEnv));
                    else
                        enumerator = index.LookupElementsDescendingToExclusive(To().Evaluate(procEnv));
                }
                else
                    enumerator = index.LookupElementsDescending();
            }

            bool first = true;
            foreach(IGraphElement elem in enumerator)
            {
                if(EmitProfiling)
                    ++procEnv.PerformanceInfo.SearchSteps;
                if(!first)
                    procEnv.EndOfIteration(true, this);
                Var.SetVariableValue(elem, procEnv);
                Seq.ResetExecutionState();
                res &= Seq.Apply(procEnv);
                first = false;
            }
            procEnv.EndOfIteration(false, this);

            return res;
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors, SequenceBase target)
        {
            Var.GetLocalVariables(variables);
            if(Expr!=null)
                Expr.GetLocalVariables(variables, constructors);
            if(Expr2!=null)
                Expr2.GetLocalVariables(variables, constructors);
            if(Seq.GetLocalVariables(variables, constructors, target))
                return true;
            RemoveVariablesFallingOutOfScope(variables, VariablesFallingOutOfScopeOnLeavingFor);
            variables.Remove(Var);
            return this == target;
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol { 
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("for{" + Var.Name + " in { " + (Ascending ? "ascending(" : "descending(") + IndexName);
                if(Expr != null)
                {
                    sb.Append(DirectionAsString(Direction));
                    sb.Append(Expr.Symbol);
                }
                if(Expr2 != null)
                {
                    sb.Append(", " + IndexName);
                    sb.Append(DirectionAsString(Direction2));
                    sb.Append(Expr2.Symbol);
                }
                sb.Append(") }; " + Seq.Symbol + "}");
                return sb.ToString();
            } 
        }

        public string DirectionAsString(RelOpDirection dir)
        {
            if(dir == RelOpDirection.Greater)
                return ">";
            else if(dir == RelOpDirection.GreaterEqual)
                return ">=";
            else if(dir == RelOpDirection.Smaller)
                return "<";
            else if(dir == RelOpDirection.SmallerEqual)
                return "<=";
            else
                return "UNDEFINED";
        }

        public SequenceExpression From()
        {
            if(Ascending)
            {
                if(Expr != null)
                {
                    if(Direction == RelOpDirection.Greater || Direction == RelOpDirection.GreaterEqual)
                        return Expr;
                    if(Expr2 != null)
                    {
                        if(Direction2 == RelOpDirection.Greater || Direction2 == RelOpDirection.GreaterEqual)
                            return Expr2;
                    }
                }
                return null;
            }
            else
            {
                if(Expr != null)
                {
                    if(Direction == RelOpDirection.Smaller || Direction == RelOpDirection.SmallerEqual)
                        return Expr;
                    if(Expr2 != null)
                    {
                        if(Direction2 == RelOpDirection.Smaller || Direction2 == RelOpDirection.SmallerEqual)
                            return Expr2;
                    }
                }
                return null;
            }
        }

        public SequenceExpression To()
        {
            if(Ascending)
            {
                if(Expr != null)
                {
                    if(Direction == RelOpDirection.Smaller || Direction == RelOpDirection.SmallerEqual)
                        return Expr;
                    if(Expr2 != null)
                    {
                        if(Direction2 == RelOpDirection.Smaller || Direction2 == RelOpDirection.SmallerEqual)
                            return Expr2;
                    }
                }
                return null;
            }
            else
            {
                if(Expr != null)
                {
                    if(Direction == RelOpDirection.Greater || Direction == RelOpDirection.GreaterEqual)
                        return Expr;
                    if(Expr2 != null)
                    {
                        if(Direction2 == RelOpDirection.Greater || Direction2 == RelOpDirection.GreaterEqual)
                            return Expr2;
                    }
                }
                return null;
            }
        }

        public bool IncludingFrom()
        {
            if(Ascending)
            {
                if(Expr != null)
                {
                    if(Direction == RelOpDirection.Greater || Direction == RelOpDirection.GreaterEqual)
                        return Direction == RelOpDirection.GreaterEqual;
                    if(Expr2 != null)
                    {
                        if(Direction2 == RelOpDirection.Greater || Direction2 == RelOpDirection.GreaterEqual)
                            return Direction2 == RelOpDirection.GreaterEqual;
                    }
                }
                return false; // dummy/don't care
            }
            else
            {
                if(Expr != null)
                {
                    if(Direction == RelOpDirection.Smaller || Direction == RelOpDirection.SmallerEqual)
                        return Direction == RelOpDirection.SmallerEqual;
                    if(Expr2 != null)
                    {
                        if(Direction2 == RelOpDirection.Smaller || Direction2 == RelOpDirection.SmallerEqual)
                            return Direction2 == RelOpDirection.SmallerEqual;
                    }
                }
                return false; // dummy/don't care
            }
        }

        public bool IncludingTo()
        {
            if(Ascending)
            {
                if(Expr != null)
                {
                    if(Direction == RelOpDirection.Smaller || Direction == RelOpDirection.SmallerEqual)
                        return Direction == RelOpDirection.SmallerEqual;
                    if(Expr2 != null)
                    {
                        if(Direction2 == RelOpDirection.Smaller || Direction2 == RelOpDirection.SmallerEqual)
                            return Direction2 == RelOpDirection.SmallerEqual;
                    }
                }
                return false; // dummy/don't care
            }
            else
            {
                if(Expr != null)
                {
                    if(Direction == RelOpDirection.Greater || Direction == RelOpDirection.GreaterEqual)
                        return Direction == RelOpDirection.GreaterEqual;
                    if(Expr2 != null)
                    {
                        if(Direction2 == RelOpDirection.Greater || Direction2 == RelOpDirection.GreaterEqual)
                            return Direction2 == RelOpDirection.GreaterEqual;
                    }
                }
                return false; // dummy/don't care
            }
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            EmitProfiling = profiling;
        }
    }

    public class SequenceForFunction : SequenceUnary
    {
        public readonly SequenceVariable Var;
        public readonly List<SequenceExpression> ArgExprs;

        public readonly List<SequenceVariable> VariablesFallingOutOfScopeOnLeavingFor;

        public bool EmitProfiling;

        public SequenceForFunction(SequenceType sequenceType, SequenceVariable var,
            List<SequenceExpression> argExprs, Sequence seq,
            List<SequenceVariable> variablesFallingOutOfScopeOnLeavingFor)
            : base(sequenceType, seq)
        {
            Var = var;
            ArgExprs = argExprs;
            VariablesFallingOutOfScopeOnLeavingFor = variablesFallingOutOfScopeOnLeavingFor;
        }

        protected SequenceForFunction(SequenceForFunction that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
            Var = that.Var.Copy(originalToCopy, procEnv);
            ArgExprs = new List<SequenceExpression>();
            foreach(SequenceExpression seqExpr in that.ArgExprs)
            {
                ArgExprs.Add(seqExpr.CopyExpression(originalToCopy, procEnv));
            }
            VariablesFallingOutOfScopeOnLeavingFor = CopyVars(originalToCopy, procEnv, that.VariablesFallingOutOfScopeOnLeavingFor);
            EmitProfiling = that.EmitProfiling;
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceForFunction(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            if(SequenceType == SequenceType.ForNodes || SequenceType == SequenceType.ForEdges)
            {
                if(ArgExprs.Count > 2)
                    throw new Exception("\"" + FunctionSymbol + "\" expects 0 (root node/edge type preset) or 1 (node type/edge type) parameters)");

                if(SequenceType == SequenceType.ForNodes)
                {
                    if(!TypesHelper.IsSameOrSubtype(Var.Type, "Node", env.Model))
                        throw new SequenceParserExceptionTypeMismatch(Var.Name, "a node type", Var.Type);
                    //if(Expr != null && TypesHelper.GetNodeType(Expr.Type(env), env.Model) == null)
                    //    throw new SequenceParserException(Expr.Symbol, "a node type", Expr.Type(env));
                }
                if(SequenceType == SequenceType.ForEdges)
                {
                    if(!TypesHelper.IsSameOrSubtype(Var.Type, "AEdge", env.Model))
                        throw new SequenceParserExceptionTypeMismatch(Var.Name, "an edge type", Var.Type);
                    //if(Expr != null && TypesHelper.GetEdgeType(Expr.Type(env), env.Model) == null)
                    //    throw new SequenceParserException(Expr.Symbol, "an edge type", Expr.Type(env));
                }
            }
            else
            {
                if(SequenceType == SequenceType.ForBoundedReachableNodes
                    || SequenceType == SequenceType.ForBoundedReachableNodesViaIncoming
                    || SequenceType == SequenceType.ForBoundedReachableNodesViaOutgoing
                    || SequenceType == SequenceType.ForBoundedReachableEdges
                    || SequenceType == SequenceType.ForBoundedReachableEdgesViaIncoming
                    || SequenceType == SequenceType.ForBoundedReachableEdgesViaOutgoing)
                {
                    if(ArgExprs.Count < 2 || ArgExprs.Count > 4)
                        throw new Exception("\"" + FunctionSymbol + "\" expects 2 (start node and depth) or 3 (start node and depth, incident edge type) or 4 (start node and depth, incident edge type, adjacent node type) parameters)");
                }
                else
                {
                    if(ArgExprs.Count < 1 || ArgExprs.Count > 3)
                        throw new Exception("\"" + FunctionSymbol + "\" expects 1 (start node only) or 2 (start node, incident edge type) or 3 (start node, incident edge type, adjacent node type) parameters)");
                }

                if(SequenceType == SequenceType.ForAdjacentNodes
                    || SequenceType == SequenceType.ForAdjacentNodesViaIncoming
                    || SequenceType == SequenceType.ForAdjacentNodesViaOutgoing
                    || SequenceType == SequenceType.ForReachableNodes
                    || SequenceType == SequenceType.ForReachableNodesViaIncoming
                    || SequenceType == SequenceType.ForReachableNodesViaOutgoing
                    || SequenceType == SequenceType.ForBoundedReachableNodes
                    || SequenceType == SequenceType.ForBoundedReachableNodesViaIncoming
                    || SequenceType == SequenceType.ForBoundedReachableNodesViaOutgoing)
                {
                    if(!TypesHelper.IsSameOrSubtype(Var.Type, "Node", env.Model))
                        throw new SequenceParserExceptionTypeMismatch(Var.Name, "a node type", Var.Type);
                }
                if(SequenceType == SequenceType.ForIncidentEdges
                    || SequenceType == SequenceType.ForIncomingEdges
                    || SequenceType == SequenceType.ForOutgoingEdges
                    || SequenceType == SequenceType.ForReachableEdges
                    || SequenceType == SequenceType.ForReachableEdgesViaIncoming
                    || SequenceType == SequenceType.ForReachableEdgesViaOutgoing
                    || SequenceType == SequenceType.ForBoundedReachableEdges
                    || SequenceType == SequenceType.ForBoundedReachableEdgesViaIncoming
                    || SequenceType == SequenceType.ForBoundedReachableEdgesViaOutgoing)
                {
                    if(!TypesHelper.IsSameOrSubtype(Var.Type, "AEdge", env.Model))
                        throw new SequenceParserExceptionTypeMismatch(Var.Name, "an edge type", Var.Type);
                }

                SequenceExpression Expr = ArgExprs[0];
                if(!TypesHelper.IsSameOrSubtype(Expr.Type(env), "Node", env.Model))
                    throw new SequenceParserExceptionTypeMismatch(Expr.Symbol, "a node type", Expr.Type(env));

                if(SequenceType == SequenceType.ForBoundedReachableNodes
                    || SequenceType == SequenceType.ForBoundedReachableNodesViaIncoming
                    || SequenceType == SequenceType.ForBoundedReachableNodesViaOutgoing
                    || SequenceType == SequenceType.ForBoundedReachableEdges
                    || SequenceType == SequenceType.ForBoundedReachableEdgesViaIncoming
                    || SequenceType == SequenceType.ForBoundedReachableEdgesViaOutgoing)
                {
                    SequenceExpression DepthExpr = ArgExprs[1];
                    if(!TypesHelper.IsSameOrSubtype(DepthExpr.Type(env), "int", env.Model))
                        throw new SequenceParserExceptionTypeMismatch(DepthExpr.Symbol, "int", DepthExpr.Type(env));

                    CheckEdgeTypeIsKnown(env, ArgExprs.Count >= 3 ? ArgExprs[2] : null, ", third argument");
                    CheckNodeTypeIsKnown(env, ArgExprs.Count >= 4 ? ArgExprs[3] : null, ", fourth argument");
                }
                else
                {
                    CheckEdgeTypeIsKnown(env, ArgExprs.Count >= 2 ? ArgExprs[1] : null, ", second argument");
                    CheckNodeTypeIsKnown(env, ArgExprs.Count >= 3 ? ArgExprs[2] : null, ", third argument");
                }
            }

            base.Check(env);
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            if(SequenceType == SequenceType.ForNodes)
            {
                if(EmitProfiling)
                    return ApplyImplNodesProfiling(procEnv);
                else
                    return ApplyImplNodes(procEnv);
            }
            if(SequenceType == SequenceType.ForEdges)
            {
                if(EmitProfiling)
                    return ApplyImplEdgesProfiling(procEnv);
                else
                    return ApplyImplEdges(procEnv);
            }

            if(EmitProfiling)
                return ApplyImplProfiling(procEnv);
            
            INode node = (INode)ArgExprs[0].Evaluate(procEnv);
            int depth = -1;
            EdgeType edgeType;
            NodeType nodeType;

            if(SequenceType == SequenceType.ForBoundedReachableNodes
                || SequenceType == SequenceType.ForBoundedReachableNodesViaIncoming
                || SequenceType == SequenceType.ForBoundedReachableNodesViaOutgoing
                || SequenceType == SequenceType.ForBoundedReachableEdges
                || SequenceType == SequenceType.ForBoundedReachableEdgesViaIncoming
                || SequenceType == SequenceType.ForBoundedReachableEdgesViaOutgoing)
            {
                depth = (int)ArgExprs[1].Evaluate(procEnv);
                edgeType = GetEdgeType(procEnv, ArgExprs.Count >= 3 ? ArgExprs[2] : null, FunctionSymbol + " iteration");
                nodeType = GetNodeType(procEnv, ArgExprs.Count >= 4 ? ArgExprs[3] : null, FunctionSymbol + " iteration");
            }
            else
            {
                edgeType = GetEdgeType(procEnv, ArgExprs.Count >= 2 ? ArgExprs[1] : null, FunctionSymbol + " iteration");
                nodeType = GetNodeType(procEnv, ArgExprs.Count >= 3 ? ArgExprs[2] : null, FunctionSymbol + " iteration");
            }

            switch(SequenceType)
            {
                case SequenceType.ForAdjacentNodes:
                    return ApplyImplForAdjacentNodes(procEnv, node, edgeType, nodeType);
                case SequenceType.ForAdjacentNodesViaIncoming:
                    return ApplyImplForAdjacentNodesViaIncoming(procEnv, node, edgeType, nodeType);
                case SequenceType.ForAdjacentNodesViaOutgoing:
                    return ApplyImplForAdjacentNodesViaOutgoing(procEnv, node, edgeType, nodeType);
                case SequenceType.ForIncidentEdges:
                    return ApplyImplForIncidentEdges(procEnv, node, edgeType, nodeType);
                case SequenceType.ForIncomingEdges:
                    return ApplyImplForIncomingEdges(procEnv, node, edgeType, nodeType);
                case SequenceType.ForOutgoingEdges:
                    return ApplyImplForOutgoingEdges(procEnv, node, edgeType, nodeType);
                case SequenceType.ForReachableNodes:
                    return ApplyImplForReachableNodes(procEnv, node, edgeType, nodeType);
                case SequenceType.ForReachableNodesViaIncoming:
                    return ApplyImplForReachableNodesViaIncoming(procEnv, node, edgeType, nodeType);
                case SequenceType.ForReachableNodesViaOutgoing:
                    return ApplyImplForReachableNodesViaOutgoing(procEnv, node, edgeType, nodeType);
                case SequenceType.ForReachableEdges:
                    return ApplyImplForReachableEdges(procEnv, node, edgeType, nodeType);
                case SequenceType.ForReachableEdgesViaIncoming:
                    return ApplyImplForReachableEdgesViaIncoming(procEnv, node, edgeType, nodeType);
                case SequenceType.ForReachableEdgesViaOutgoing:
                    return ApplyImplForReachableEdgesViaOutgoing(procEnv, node, edgeType, nodeType);
                case SequenceType.ForBoundedReachableNodes:
                    return ApplyImplForBoundedReachableNodes(procEnv, node, depth, edgeType, nodeType);
                case SequenceType.ForBoundedReachableNodesViaIncoming:
                    return ApplyImplForBoundedReachableNodesViaIncoming(procEnv, node, depth, edgeType, nodeType);
                case SequenceType.ForBoundedReachableNodesViaOutgoing:
                    return ApplyImplForBoundedReachableNodesViaOutgoing(procEnv, node, depth, edgeType, nodeType);
                case SequenceType.ForBoundedReachableEdges:
                    return ApplyImplForBoundedReachableEdges(procEnv, node, depth, edgeType, nodeType);
                case SequenceType.ForBoundedReachableEdgesViaIncoming:
                    return ApplyImplForBoundedReachableEdgesViaIncoming(procEnv, node, depth, edgeType, nodeType);
                case SequenceType.ForBoundedReachableEdgesViaOutgoing:
                    return ApplyImplForBoundedReachableEdgesViaOutgoing(procEnv, node, depth, edgeType, nodeType);
                default:
                    throw new Exception("Unknown for loop over function type");
            }
        }

        bool ApplyImplForAdjacentNodes(IGraphProcessingEnvironment procEnv, INode node, EdgeType edgeType, NodeType nodeType)
        {
            bool res = true;
            bool first = true;
            foreach(IEdge edge in node.GetCompatibleIncident(edgeType))
            {
                if(!edge.Opposite(node).InstanceOf(nodeType))
                    continue;
                if(!first)
                    procEnv.EndOfIteration(true, this);
                Var.SetVariableValue(edge.Opposite(node), procEnv);
                Seq.ResetExecutionState();
                res &= Seq.Apply(procEnv);
                first = false;
            }
            procEnv.EndOfIteration(false, this);
            return res;
        }

        bool ApplyImplForAdjacentNodesViaIncoming(IGraphProcessingEnvironment procEnv, INode node, EdgeType edgeType, NodeType nodeType)
        {
            bool res = true;
            bool first = true;
            foreach(IEdge edge in node.GetCompatibleIncoming(edgeType))
            {
                if(!edge.Source.InstanceOf(nodeType))
                    continue;
                if(!first)
                    procEnv.EndOfIteration(true, this);
                Var.SetVariableValue(edge.Source, procEnv);
                Seq.ResetExecutionState();
                res &= Seq.Apply(procEnv);
                first = false;
            }
            procEnv.EndOfIteration(false, this);
            return res;
        }

        bool ApplyImplForAdjacentNodesViaOutgoing(IGraphProcessingEnvironment procEnv, INode node, EdgeType edgeType, NodeType nodeType)
        {
            bool res = true;
            bool first = true;
            foreach(IEdge edge in node.GetCompatibleOutgoing(edgeType))
            {
                if(!edge.Target.InstanceOf(nodeType))
                    continue;
                if(!first)
                    procEnv.EndOfIteration(true, this);
                Var.SetVariableValue(edge.Target, procEnv);
                Seq.ResetExecutionState();
                res &= Seq.Apply(procEnv);
                first = false;
            }
            procEnv.EndOfIteration(false, this);
            return res;
        }

        bool ApplyImplForIncidentEdges(IGraphProcessingEnvironment procEnv, INode node, EdgeType edgeType, NodeType nodeType)
        {
            bool res = true;
            bool first = true;
            foreach(IEdge edge in node.GetCompatibleIncident(edgeType))
            {
                if(!edge.Opposite(node).InstanceOf(nodeType))
                    continue;
                if(!first)
                    procEnv.EndOfIteration(true, this);
                Var.SetVariableValue(edge, procEnv);
                Seq.ResetExecutionState();
                res &= Seq.Apply(procEnv);
                first = false;
            }
            procEnv.EndOfIteration(false, this);
            return res;
        }

        bool ApplyImplForIncomingEdges(IGraphProcessingEnvironment procEnv, INode node, EdgeType edgeType, NodeType nodeType)
        {
            bool res = true;
            bool first = true;
            foreach(IEdge edge in node.GetCompatibleIncoming(edgeType))
            {
                if(!edge.Source.InstanceOf(nodeType))
                    continue;
                if(!first)
                    procEnv.EndOfIteration(true, this);
                Var.SetVariableValue(edge, procEnv);
                Seq.ResetExecutionState();
                res &= Seq.Apply(procEnv);
                first = false;
            }
            procEnv.EndOfIteration(false, this);
            return res;
        }

        bool ApplyImplForOutgoingEdges(IGraphProcessingEnvironment procEnv, INode node, EdgeType edgeType, NodeType nodeType)
        {
            bool res = true;
            bool first = true;
            foreach(IEdge edge in node.GetCompatibleOutgoing(edgeType))
            {
                if(!edge.Target.InstanceOf(nodeType))
                    continue;
                if(!first)
                    procEnv.EndOfIteration(true, this);
                Var.SetVariableValue(edge, procEnv);
                Seq.ResetExecutionState();
                res &= Seq.Apply(procEnv);
                first = false;
            }
            procEnv.EndOfIteration(false, this);
            return res;
        }

        bool ApplyImplForReachableNodes(IGraphProcessingEnvironment procEnv, INode node, EdgeType edgeType, NodeType nodeType)
        {
            bool res = true;
            bool first = true;
            foreach(INode iter in GraphHelper.Reachable(node, edgeType, nodeType, procEnv.Graph))
            {
                if(!first)
                    procEnv.EndOfIteration(true, this);
                Var.SetVariableValue(iter, procEnv);
                Seq.ResetExecutionState();
                res &= Seq.Apply(procEnv);
                first = false;
            }
            procEnv.EndOfIteration(false, this);
            return res;
        }

        bool ApplyImplForReachableNodesViaIncoming(IGraphProcessingEnvironment procEnv, INode node, EdgeType edgeType, NodeType nodeType)
        {
            bool res = true;
            bool first = true;
            foreach(INode iter in GraphHelper.ReachableIncoming(node, edgeType, nodeType, procEnv.Graph))
            {
                if(!first)
                    procEnv.EndOfIteration(true, this);
                Var.SetVariableValue(iter, procEnv);
                Seq.ResetExecutionState();
                res &= Seq.Apply(procEnv);
                first = false;
            }
            procEnv.EndOfIteration(false, this);
            return res;
        }

        bool ApplyImplForReachableNodesViaOutgoing(IGraphProcessingEnvironment procEnv, INode node, EdgeType edgeType, NodeType nodeType)
        {
            bool res = true;
            bool first = true;
            foreach(INode iter in GraphHelper.ReachableOutgoing(node, edgeType, nodeType, procEnv.Graph))
            {
                if(!first)
                    procEnv.EndOfIteration(true, this);
                Var.SetVariableValue(iter, procEnv);
                Seq.ResetExecutionState();
                res &= Seq.Apply(procEnv);
                first = false;
            }
            procEnv.EndOfIteration(false, this);
            return res;
        }

        bool ApplyImplForReachableEdges(IGraphProcessingEnvironment procEnv, INode node, EdgeType edgeType, NodeType nodeType)
        {
            bool res = true;
            bool first = true;
            foreach(IEdge edge in GraphHelper.ReachableEdges(node, edgeType, nodeType, procEnv.Graph))
            {
                if(!first)
                    procEnv.EndOfIteration(true, this);
                Var.SetVariableValue(edge, procEnv);
                Seq.ResetExecutionState();
                res &= Seq.Apply(procEnv);
                first = false;
            }
            procEnv.EndOfIteration(false, this);
            return res;
        }

        bool ApplyImplForReachableEdgesViaIncoming(IGraphProcessingEnvironment procEnv, INode node, EdgeType edgeType, NodeType nodeType)
        {
            bool res = true;
            bool first = true;
            foreach(IEdge edge in GraphHelper.ReachableEdgesIncoming(node, edgeType, nodeType, procEnv.Graph))
            {
                if(!first)
                    procEnv.EndOfIteration(true, this);
                Var.SetVariableValue(edge, procEnv);
                Seq.ResetExecutionState();
                res &= Seq.Apply(procEnv);
                first = false;
            }
            procEnv.EndOfIteration(false, this);
            return res;
        }

        bool ApplyImplForReachableEdgesViaOutgoing(IGraphProcessingEnvironment procEnv, INode node, EdgeType edgeType, NodeType nodeType)
        {
            bool res = true;
            bool first = true;
            foreach(IEdge edge in GraphHelper.ReachableEdgesOutgoing(node, edgeType, nodeType, procEnv.Graph))
            {
                if(!first)
                    procEnv.EndOfIteration(true, this);
                Var.SetVariableValue(edge, procEnv);
                Seq.ResetExecutionState();
                res &= Seq.Apply(procEnv);
                first = false;
            }
            procEnv.EndOfIteration(false, this);
            return res;
        }

        bool ApplyImplForBoundedReachableNodes(IGraphProcessingEnvironment procEnv, INode node, int depth, EdgeType edgeType, NodeType nodeType)
        {
            bool res = true;
            bool first = true;
            foreach(INode iter in GraphHelper.BoundedReachable(node, depth, edgeType, nodeType, procEnv.Graph))
            {
                if(!first)
                    procEnv.EndOfIteration(true, this);
                Var.SetVariableValue(iter, procEnv);
                Seq.ResetExecutionState();
                res &= Seq.Apply(procEnv);
                first = false;
            }
            procEnv.EndOfIteration(false, this);
            return res;
        }

        bool ApplyImplForBoundedReachableNodesViaIncoming(IGraphProcessingEnvironment procEnv, INode node, int depth, EdgeType edgeType, NodeType nodeType)
        {
            bool res = true;
            bool first = true;
            foreach(INode iter in GraphHelper.BoundedReachableIncoming(node, depth, edgeType, nodeType, procEnv.Graph))
            {
                if(!first)
                    procEnv.EndOfIteration(true, this);
                Var.SetVariableValue(iter, procEnv);
                Seq.ResetExecutionState();
                res &= Seq.Apply(procEnv);
                first = false;
            }
            procEnv.EndOfIteration(false, this);
            return res;
        }

        bool ApplyImplForBoundedReachableNodesViaOutgoing(IGraphProcessingEnvironment procEnv, INode node, int depth, EdgeType edgeType, NodeType nodeType)
        {
            bool res = true;
            bool first = true;
            foreach(INode iter in GraphHelper.BoundedReachableOutgoing(node, depth, edgeType, nodeType, procEnv.Graph))
            {
                if(!first)
                    procEnv.EndOfIteration(true, this);
                Var.SetVariableValue(iter, procEnv);
                Seq.ResetExecutionState();
                res &= Seq.Apply(procEnv);
                first = false;
            }
            procEnv.EndOfIteration(false, this);
            return res;
        }

        bool ApplyImplForBoundedReachableEdges(IGraphProcessingEnvironment procEnv, INode node, int depth, EdgeType edgeType, NodeType nodeType)
        {
            bool res = true;
            bool first = true;
            foreach(IEdge edge in GraphHelper.BoundedReachableEdges(node, depth, edgeType, nodeType, procEnv.Graph))
            {
                if(!first)
                    procEnv.EndOfIteration(true, this);
                Var.SetVariableValue(edge, procEnv);
                Seq.ResetExecutionState();
                res &= Seq.Apply(procEnv);
                first = false;
            }
            procEnv.EndOfIteration(false, this);
            return res;
        }

        bool ApplyImplForBoundedReachableEdgesViaIncoming(IGraphProcessingEnvironment procEnv, INode node, int depth, EdgeType edgeType, NodeType nodeType)
        {
            bool res = true;
            bool first = true;
            foreach(IEdge edge in GraphHelper.BoundedReachableEdgesIncoming(node, depth, edgeType, nodeType, procEnv.Graph))
            {
                if(!first)
                    procEnv.EndOfIteration(true, this);
                Var.SetVariableValue(edge, procEnv);
                Seq.ResetExecutionState();
                res &= Seq.Apply(procEnv);
                first = false;
            }
            procEnv.EndOfIteration(false, this);
            return res;
        }

        bool ApplyImplForBoundedReachableEdgesViaOutgoing(IGraphProcessingEnvironment procEnv, INode node, int depth, EdgeType edgeType, NodeType nodeType)
        {
            bool res = true;
            bool first = true;
            foreach(IEdge edge in GraphHelper.BoundedReachableEdgesOutgoing(node, depth, edgeType, nodeType, procEnv.Graph))
            {
                if(!first)
                    procEnv.EndOfIteration(true, this);
                Var.SetVariableValue(edge, procEnv);
                Seq.ResetExecutionState();
                res &= Seq.Apply(procEnv);
                first = false;
            }
            procEnv.EndOfIteration(false, this);
            return res;
        }

        protected bool ApplyImplProfiling(IGraphProcessingEnvironment procEnv)
        {
            INode node = (INode)ArgExprs[0].Evaluate(procEnv);
            int depth = -1;
            EdgeType edgeType;
            NodeType nodeType;

            if(SequenceType == SequenceType.ForBoundedReachableNodes
                || SequenceType == SequenceType.ForBoundedReachableNodesViaIncoming
                || SequenceType == SequenceType.ForBoundedReachableNodesViaOutgoing
                || SequenceType == SequenceType.ForBoundedReachableEdges
                || SequenceType == SequenceType.ForBoundedReachableEdgesViaIncoming
                || SequenceType == SequenceType.ForBoundedReachableEdgesViaOutgoing)
            {
                depth = (int)ArgExprs[1].Evaluate(procEnv);
                edgeType = GetEdgeType(procEnv, ArgExprs.Count >= 3 ? ArgExprs[2] : null, FunctionSymbol + " iteration");
                nodeType = GetNodeType(procEnv, ArgExprs.Count >= 4 ? ArgExprs[3] : null, FunctionSymbol + " iteration");
            }
            else
            {
                edgeType = GetEdgeType(procEnv, ArgExprs.Count >= 2 ? ArgExprs[1] : null, FunctionSymbol + " iteration");
                nodeType = GetNodeType(procEnv, ArgExprs.Count >= 3 ? ArgExprs[2] : null, FunctionSymbol + " iteration");
            }

            switch(SequenceType)
            {
                case SequenceType.ForAdjacentNodes:
                    return ApplyImplProfilingForAdjacentNodes(procEnv, node, edgeType, nodeType);
                case SequenceType.ForAdjacentNodesViaIncoming:
                    return ApplyImplProfilingForAdjacentNodesViaIncoming(procEnv, node, edgeType, nodeType);
                case SequenceType.ForAdjacentNodesViaOutgoing:
                    return ApplyImplProfilingForAdjacentNodesViaOutgoing(procEnv, node, edgeType, nodeType);
                case SequenceType.ForIncidentEdges:
                    return ApplyImplProfilingForIncidentEdges(procEnv, node, edgeType, nodeType);
                case SequenceType.ForIncomingEdges:
                    return ApplyImplProfilingForIncomingEdges(procEnv, node, edgeType, nodeType);
                case SequenceType.ForOutgoingEdges:
                    return ApplyImplProfilingForOutgoingEdges(procEnv, node, edgeType, nodeType);
                case SequenceType.ForReachableNodes:
                    return ApplyImplProfilingForReachableNodes(procEnv, node, edgeType, nodeType);
                case SequenceType.ForReachableNodesViaIncoming:
                    return ApplyImplProfilingForReachableNodesViaIncoming(procEnv, node, edgeType, nodeType);
                case SequenceType.ForReachableNodesViaOutgoing:
                    return ApplyImplProfilingForReachableNodesViaOutgoing(procEnv, node, edgeType, nodeType);
                case SequenceType.ForReachableEdges:
                    return ApplyImplProfilingForReachableEdges(procEnv, node, edgeType, nodeType);
                case SequenceType.ForReachableEdgesViaIncoming:
                    return ApplyImplProfilingForReachableEdgesViaIncoming(procEnv, node, edgeType, nodeType);
                case SequenceType.ForReachableEdgesViaOutgoing:
                    return ApplyImplProfilingForReachableEdgesViaOutgoing(procEnv, node, edgeType, nodeType);
                case SequenceType.ForBoundedReachableNodes:
                    return ApplyImplProfilingForBoundedReachableNodes(procEnv, node, depth, edgeType, nodeType);
                case SequenceType.ForBoundedReachableNodesViaIncoming:
                    return ApplyImplProfilingForBoundedReachableNodesViaIncoming(procEnv, node, depth, edgeType, nodeType);
                case SequenceType.ForBoundedReachableNodesViaOutgoing:
                    return ApplyImplProfilingForBoundedReachableNodesViaOutgoing(procEnv, node, depth, edgeType, nodeType);
                case SequenceType.ForBoundedReachableEdges:
                    return ApplyImplProfilingForBoundedReachableEdges(procEnv, node, depth, edgeType, nodeType);
                case SequenceType.ForBoundedReachableEdgesViaIncoming:
                    return ApplyImplProfilingForBoundedReachableEdgesViaIncoming(procEnv, node, depth, edgeType, nodeType);
                case SequenceType.ForBoundedReachableEdgesViaOutgoing:
                    return ApplyImplProfilingForBoundedReachableEdgesViaOutgoing(procEnv, node, depth, edgeType, nodeType);
                default:
                    throw new Exception("Unknown for loop over function type (with profiling on)");
            }
        }

        bool ApplyImplProfilingForAdjacentNodes(IGraphProcessingEnvironment procEnv, INode node, EdgeType edgeType, NodeType nodeType)
        {
            bool res = true;
            bool first = true;
            foreach(IEdge edge in node.Incident)
            {
                ++procEnv.PerformanceInfo.SearchSteps;
                if(!edge.InstanceOf(edgeType))
                    continue;
                if(!edge.Opposite(node).InstanceOf(nodeType))
                    continue;
                if(!first)
                    procEnv.EndOfIteration(true, this);
                Var.SetVariableValue(edge.Opposite(node), procEnv);
                Seq.ResetExecutionState();
                res &= Seq.Apply(procEnv);
                first = false;
            }
            procEnv.EndOfIteration(false, this);
            return res;
        }

        bool ApplyImplProfilingForAdjacentNodesViaIncoming(IGraphProcessingEnvironment procEnv, INode node, EdgeType edgeType, NodeType nodeType)
        {
            bool res = true;
            bool first = true;
            foreach(IEdge edge in node.Incoming)
            {
                ++procEnv.PerformanceInfo.SearchSteps;
                if(!edge.InstanceOf(edgeType))
                    continue;
                if(!edge.Source.InstanceOf(nodeType))
                    continue;
                if(!first)
                    procEnv.EndOfIteration(true, this);
                Var.SetVariableValue(edge.Source, procEnv);
                Seq.ResetExecutionState();
                res &= Seq.Apply(procEnv);
                first = false;
            }
            procEnv.EndOfIteration(false, this);
            return res;
        }

        bool ApplyImplProfilingForAdjacentNodesViaOutgoing(IGraphProcessingEnvironment procEnv, INode node, EdgeType edgeType, NodeType nodeType)
        {
            bool res = true;
            bool first = true;
            foreach(IEdge edge in node.Outgoing)
            {
                ++procEnv.PerformanceInfo.SearchSteps;
                if(!edge.InstanceOf(edgeType))
                    continue;
                if(!edge.Target.InstanceOf(nodeType))
                    continue;
                if(!first)
                    procEnv.EndOfIteration(true, this);
                Var.SetVariableValue(edge.Target, procEnv);
                Seq.ResetExecutionState();
                res &= Seq.Apply(procEnv);
                first = false;
            }
            procEnv.EndOfIteration(false, this);
            return res;
        }

        bool ApplyImplProfilingForIncidentEdges(IGraphProcessingEnvironment procEnv, INode node, EdgeType edgeType, NodeType nodeType)
        {
            bool res = true;
            bool first = true;
            foreach(IEdge edge in node.Incident)
            {
                ++procEnv.PerformanceInfo.SearchSteps;
                if(!edge.InstanceOf(edgeType))
                    continue;
                if(!edge.Opposite(node).InstanceOf(nodeType))
                    continue;
                if(!first)
                    procEnv.EndOfIteration(true, this);
                Var.SetVariableValue(edge, procEnv);
                Seq.ResetExecutionState();
                res &= Seq.Apply(procEnv);
                first = false;
            }
            procEnv.EndOfIteration(false, this);
            return res;
        }

        bool ApplyImplProfilingForIncomingEdges(IGraphProcessingEnvironment procEnv, INode node, EdgeType edgeType, NodeType nodeType)
        {
            bool res = true;
            bool first = true;
            foreach(IEdge edge in node.Incoming)
            {
                ++procEnv.PerformanceInfo.SearchSteps;
                if(!edge.InstanceOf(edgeType))
                    continue;
                if(!edge.Source.InstanceOf(nodeType))
                    continue;
                if(!first)
                    procEnv.EndOfIteration(true, this);
                Var.SetVariableValue(edge, procEnv);
                Seq.ResetExecutionState();
                res &= Seq.Apply(procEnv);
                first = false;
            }
            procEnv.EndOfIteration(false, this);
            return res;
        }

        bool ApplyImplProfilingForOutgoingEdges(IGraphProcessingEnvironment procEnv, INode node, EdgeType edgeType, NodeType nodeType)
        {
            bool res = true;
            bool first = true;
            foreach(IEdge edge in node.Outgoing)
            {
                ++procEnv.PerformanceInfo.SearchSteps;
                if(!edge.InstanceOf(edgeType))
                    continue;
                if(!edge.Target.InstanceOf(nodeType))
                    continue;
                if(!first)
                    procEnv.EndOfIteration(true, this);
                Var.SetVariableValue(edge, procEnv);
                Seq.ResetExecutionState();
                res &= Seq.Apply(procEnv);
                first = false;
            }
            procEnv.EndOfIteration(false, this);
            return res;
        }

        bool ApplyImplProfilingForReachableNodes(IGraphProcessingEnvironment procEnv, INode node, EdgeType edgeType, NodeType nodeType)
        {
            bool res = true;
            bool first = true;
            foreach(INode iter in GraphHelper.Reachable(node, edgeType, nodeType, procEnv.Graph, procEnv))
            {
                if(!first)
                    procEnv.EndOfIteration(true, this);
                Var.SetVariableValue(iter, procEnv);
                Seq.ResetExecutionState();
                res &= Seq.Apply(procEnv);
                first = false;
            }
            procEnv.EndOfIteration(false, this);
            return res;
        }

        bool ApplyImplProfilingForReachableNodesViaIncoming(IGraphProcessingEnvironment procEnv, INode node, EdgeType edgeType, NodeType nodeType)
        {
            bool res = true;
            bool first = true;
            foreach(INode iter in GraphHelper.ReachableIncoming(node, edgeType, nodeType, procEnv.Graph, procEnv))
            {
                if(!first)
                    procEnv.EndOfIteration(true, this);
                Var.SetVariableValue(iter, procEnv);
                Seq.ResetExecutionState();
                res &= Seq.Apply(procEnv);
                first = false;
            }
            procEnv.EndOfIteration(false, this);
            return res;
        }

        bool ApplyImplProfilingForReachableNodesViaOutgoing(IGraphProcessingEnvironment procEnv, INode node, EdgeType edgeType, NodeType nodeType)
        {
            bool res = true;
            bool first = true;
            foreach(INode iter in GraphHelper.ReachableOutgoing(node, edgeType, nodeType, procEnv.Graph, procEnv))
            {
                if(!first)
                    procEnv.EndOfIteration(true, this);
                Var.SetVariableValue(iter, procEnv);
                Seq.ResetExecutionState();
                res &= Seq.Apply(procEnv);
                first = false;
            }
            procEnv.EndOfIteration(false, this);
            return res;
        }

        bool ApplyImplProfilingForReachableEdges(IGraphProcessingEnvironment procEnv, INode node, EdgeType edgeType, NodeType nodeType)
        {
            bool res = true;
            bool first = true;
            foreach(IEdge edge in GraphHelper.ReachableEdges(node, edgeType, nodeType, procEnv.Graph, procEnv))
            {
                if(!first)
                    procEnv.EndOfIteration(true, this);
                Var.SetVariableValue(edge, procEnv);
                Seq.ResetExecutionState();
                res &= Seq.Apply(procEnv);
                first = false;
            }
            procEnv.EndOfIteration(false, this);
            return res;
        }

        bool ApplyImplProfilingForReachableEdgesViaIncoming(IGraphProcessingEnvironment procEnv, INode node, EdgeType edgeType, NodeType nodeType)
        {
            bool res = true;
            bool first = true;
            foreach(IEdge edge in GraphHelper.ReachableEdgesIncoming(node, edgeType, nodeType, procEnv.Graph, procEnv))
            {
                if(!first)
                    procEnv.EndOfIteration(true, this);
                Var.SetVariableValue(edge, procEnv);
                Seq.ResetExecutionState();
                res &= Seq.Apply(procEnv);
                first = false;
            }
            procEnv.EndOfIteration(false, this);
            return res;
        }

        bool ApplyImplProfilingForReachableEdgesViaOutgoing(IGraphProcessingEnvironment procEnv, INode node, EdgeType edgeType, NodeType nodeType)
        {
            bool res = true;
            bool first = true;
            foreach(IEdge edge in GraphHelper.ReachableEdgesOutgoing(node, edgeType, nodeType, procEnv.Graph, procEnv))
            {
                if(!first)
                    procEnv.EndOfIteration(true, this);
                Var.SetVariableValue(edge, procEnv);
                Seq.ResetExecutionState();
                res &= Seq.Apply(procEnv);
                first = false;
            }
            procEnv.EndOfIteration(false, this);
            return res;
        }

        bool ApplyImplProfilingForBoundedReachableNodes(IGraphProcessingEnvironment procEnv, INode node, int depth, EdgeType edgeType, NodeType nodeType)
        {
            bool res = true;
            bool first = true;
            foreach(INode iter in GraphHelper.BoundedReachable(node, depth, edgeType, nodeType, procEnv.Graph, procEnv))
            {
                if(!first)
                    procEnv.EndOfIteration(true, this);
                Var.SetVariableValue(iter, procEnv);
                Seq.ResetExecutionState();
                res &= Seq.Apply(procEnv);
                first = false;
            }
            procEnv.EndOfIteration(false, this);
            return res;
        }

        bool ApplyImplProfilingForBoundedReachableNodesViaIncoming(IGraphProcessingEnvironment procEnv, INode node, int depth, EdgeType edgeType, NodeType nodeType)
        {
            bool res = true;
            bool first = true;
            foreach(INode iter in GraphHelper.BoundedReachableIncoming(node, depth, edgeType, nodeType, procEnv.Graph, procEnv))
            {
                if(!first)
                    procEnv.EndOfIteration(true, this);
                Var.SetVariableValue(iter, procEnv);
                Seq.ResetExecutionState();
                res &= Seq.Apply(procEnv);
                first = false;
            }
            procEnv.EndOfIteration(false, this);
            return res;
        }

        bool ApplyImplProfilingForBoundedReachableNodesViaOutgoing(IGraphProcessingEnvironment procEnv, INode node, int depth, EdgeType edgeType, NodeType nodeType)
        {
            bool res = true;
            bool first = true;
            foreach(INode iter in GraphHelper.BoundedReachableOutgoing(node, depth, edgeType, nodeType, procEnv.Graph, procEnv))
            {
                if(!first)
                    procEnv.EndOfIteration(true, this);
                Var.SetVariableValue(iter, procEnv);
                Seq.ResetExecutionState();
                res &= Seq.Apply(procEnv);
                first = false;
            }
            procEnv.EndOfIteration(false, this);
            return res;
        }

        bool ApplyImplProfilingForBoundedReachableEdges(IGraphProcessingEnvironment procEnv, INode node, int depth, EdgeType edgeType, NodeType nodeType)
        {
            bool res = true;
            bool first = true;
            foreach(IEdge edge in GraphHelper.BoundedReachableEdges(node, depth, edgeType, nodeType, procEnv.Graph, procEnv))
            {
                if(!first)
                    procEnv.EndOfIteration(true, this);
                Var.SetVariableValue(edge, procEnv);
                Seq.ResetExecutionState();
                res &= Seq.Apply(procEnv);
                first = false;
            }
            procEnv.EndOfIteration(false, this);
            return res;
        }

        bool ApplyImplProfilingForBoundedReachableEdgesViaIncoming(IGraphProcessingEnvironment procEnv, INode node, int depth, EdgeType edgeType, NodeType nodeType)
        {
            bool res = true;
            bool first = true;
            foreach(IEdge edge in GraphHelper.BoundedReachableEdgesIncoming(node, depth, edgeType, nodeType, procEnv.Graph, procEnv))
            {
                if(!first)
                    procEnv.EndOfIteration(true, this);
                Var.SetVariableValue(edge, procEnv);
                Seq.ResetExecutionState();
                res &= Seq.Apply(procEnv);
                first = false;
            }
            procEnv.EndOfIteration(false, this);
            return res;
        }

        bool ApplyImplProfilingForBoundedReachableEdgesViaOutgoing(IGraphProcessingEnvironment procEnv, INode node, int depth, EdgeType edgeType, NodeType nodeType)
        {
            bool res = true;
            bool first = true;
            foreach(IEdge edge in GraphHelper.BoundedReachableEdgesOutgoing(node, depth, edgeType, nodeType, procEnv.Graph, procEnv))
            {
                if(!first)
                    procEnv.EndOfIteration(true, this);
                Var.SetVariableValue(edge, procEnv);
                Seq.ResetExecutionState();
                res &= Seq.Apply(procEnv);
                first = false;
            }
            procEnv.EndOfIteration(false, this);
            return res;
        }

        protected bool ApplyImplNodes(IGraphProcessingEnvironment procEnv)
        {
            NodeType nodeType = GetNodeType(procEnv, ArgExprs.Count >= 1 ? ArgExprs[0] : null, FunctionSymbol + " iteration");
            bool res = true;
            bool first = true;
            foreach(INode node in procEnv.Graph.GetCompatibleNodes(nodeType))
            {
                if(!first)
                    procEnv.EndOfIteration(true, this);
                Var.SetVariableValue(node, procEnv);
                Seq.ResetExecutionState();
                res &= Seq.Apply(procEnv);
                first = false;
            }
            procEnv.EndOfIteration(false, this);
            return res;
        }

        protected bool ApplyImplEdges(IGraphProcessingEnvironment procEnv)
        {
            EdgeType edgeType = GetEdgeType(procEnv, ArgExprs.Count >= 1 ? ArgExprs[0] : null, FunctionSymbol + " iteration");
            bool res = true;
            bool first = true;
            foreach(IEdge edge in procEnv.Graph.GetCompatibleEdges(edgeType))
            {
                if(!first)
                    procEnv.EndOfIteration(true, this);
                Var.SetVariableValue(edge, procEnv);
                Seq.ResetExecutionState();
                res &= Seq.Apply(procEnv);
                first = false;
            }
            procEnv.EndOfIteration(false, this);
            return res;
        }

        protected bool ApplyImplNodesProfiling(IGraphProcessingEnvironment procEnv)
        {
            NodeType nodeType = GetNodeType(procEnv, ArgExprs.Count >= 1 ? ArgExprs[0] : null, FunctionSymbol + " iteration");
            bool res = true;
            bool first = true;
            foreach(INode node in procEnv.Graph.GetCompatibleNodes(nodeType))
            {
                ++procEnv.PerformanceInfo.SearchSteps;
                if(!first)
                    procEnv.EndOfIteration(true, this);
                Var.SetVariableValue(node, procEnv);
                Seq.ResetExecutionState();
                res &= Seq.Apply(procEnv);
                first = false;
            }
            procEnv.EndOfIteration(false, this);
            return res;
        }

        protected bool ApplyImplEdgesProfiling(IGraphProcessingEnvironment procEnv)
        {
            EdgeType edgeType = GetEdgeType(procEnv, ArgExprs.Count >= 1 ? ArgExprs[0] : null, FunctionSymbol + " iteration");
            bool res = true;
            bool first = true;
            foreach(IEdge edge in procEnv.Graph.GetCompatibleEdges(edgeType))
            {
                ++procEnv.PerformanceInfo.SearchSteps;
                if(!first)
                    procEnv.EndOfIteration(true, this);
                Var.SetVariableValue(edge, procEnv);
                Seq.ResetExecutionState();
                res &= Seq.Apply(procEnv);
                first = false;
            }
            procEnv.EndOfIteration(false, this);
            return res;
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors, SequenceBase target)
        {
            Var.GetLocalVariables(variables);
            foreach(SequenceExpression seqExpr in ArgExprs)
            {
                seqExpr.GetLocalVariables(variables, constructors);
            }
            if(Seq.GetLocalVariables(variables, constructors, target))
                return true;
            RemoveVariablesFallingOutOfScope(variables, VariablesFallingOutOfScopeOnLeavingFor);
            variables.Remove(Var);
            return this == target;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            EmitProfiling = profiling;
        }

        public override IEnumerable<SequenceBase> ChildrenBase
        { 
            get
            { 
                yield return Seq;
                foreach(SequenceExpression expr in ArgExprs)
                {
                    yield return expr;
                }
            } 
        }

        public string FunctionSymbol
        {
            get
            {
                switch(SequenceType)
                {
                case SequenceType.ForAdjacentNodes: return "adjacent";
                case SequenceType.ForAdjacentNodesViaIncoming: return "adjacentIncoming";
                case SequenceType.ForAdjacentNodesViaOutgoing: return "adjacentOutgoing";
                case SequenceType.ForIncidentEdges: return "incident";
                case SequenceType.ForIncomingEdges: return "incoming";
                case SequenceType.ForOutgoingEdges: return "outgoing";
                case SequenceType.ForReachableNodes: return "reachable";
                case SequenceType.ForReachableNodesViaIncoming: return "reachableIncoming";
                case SequenceType.ForReachableNodesViaOutgoing: return "reachableOutgoing";
                case SequenceType.ForReachableEdges: return "reachableEdges";
                case SequenceType.ForReachableEdgesViaIncoming: return "reachableEdgesIncoming";
                case SequenceType.ForReachableEdgesViaOutgoing: return "reachableEdgesOutgoing";
                case SequenceType.ForNodes: return "nodes";
                case SequenceType.ForEdges: return "edges";
                default: return "INTERNAL FAILURE!";
                }
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("for{");
                sb.Append(Var.Name);
                sb.Append(" in ");
                sb.Append(FunctionSymbol);
                sb.Append("(");
                bool first = true;
                foreach(SequenceExpression seqExpr in ArgExprs)
                {
                    if(!first)
                        sb.Append(", ");
                    else
                        first = false;
                    sb.Append(seqExpr.Symbol);
                }
                sb.Append("); " + Seq.Symbol + "}");
                return sb.ToString();
            }
        }
    }

    public class SequenceForMatch : SequenceUnary, IPatternMatchingConstruct
    {
        public readonly SequenceVariable Var;
        public readonly SequenceRuleCall Rule;

        public readonly List<SequenceVariable> VariablesFallingOutOfScopeOnLeavingFor;

        public PatternMatchingConstructType ConstructType
        {
            get { return PatternMatchingConstructType.ForMatch; }
        }

        public SequenceForMatch(SequenceVariable var, Sequence rule, Sequence seq,
            List<SequenceVariable> variablesFallingOutOfScopeOnLeavingFor)
            : base(SequenceType.ForMatch, seq)
        {
            Var = var;
            Rule = (SequenceRuleCall)rule;
            VariablesFallingOutOfScopeOnLeavingFor = variablesFallingOutOfScopeOnLeavingFor;
            Rule.Parent = this;
        }

        protected SequenceForMatch(SequenceForMatch that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
            Var = that.Var.Copy(originalToCopy, procEnv);
            Rule = (SequenceRuleCall)that.Rule.Copy(originalToCopy, procEnv);
            VariablesFallingOutOfScopeOnLeavingFor = CopyVars(originalToCopy, procEnv, that.VariablesFallingOutOfScopeOnLeavingFor);
            Rule.Parent = this;
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceForMatch(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            if(Rule is SequenceRuleAllCall || Rule is SequenceRuleCountAllCall)
                throw new Exception("Sequence ForMatch must be of the form for{v in [?r]; seq}, rule maybe with input parameters, or a breakpoint");
            if(Rule.ReturnVars.Length > 0)
                throw new Exception("No output parameters allowed for the rule used in the for matches iteration sequence");
            if(Var.Type == "")
                throw new SequenceParserExceptionTypeMismatch(Var.Name, "a match type (match<rulename>)", "statically unknown type");
            if(!TypesHelper.IsSameOrSubtype(Var.Type, "match<"+Rule.Name+">", env.Model))
                throw new SequenceParserExceptionTypeMismatch(Symbol, "match<" + Rule.Name + ">", Var.Type);
            if(Rule.Subgraph != null)
                throw new Exception("Sequence ForMatch can't employ a call with subgraph prefix (no for{v in [?sg.r]; seq} possible)");
            base.Check(env);
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            bool res = true;

            // first get all matches of the rule
#if LOG_SEQUENCE_EXECUTION
            procEnv.Recorder.WriteLine("Matching for rule " + Rule.GetRuleCallString(procEnv));
#endif

            FireBeginExecutionEvent(procEnv);

            FillArgumentsFromArgumentExpressions(Rule.ArgumentExpressions, Rule.Arguments, procEnv);

            SequenceRuleCallInterpreted ruleInterpreted = (SequenceRuleCallInterpreted)Rule;
            IMatches matches = procEnv.MatchWithoutEvent(ruleInterpreted.Action, Rule.Arguments, procEnv.MaxMatches, FireDebugEvents);

            for(int i = 0; i < Rule.Filters.Count; ++i)
            {
                SequenceFilterCallInterpreted filter = (SequenceFilterCallInterpreted)Rule.Filters[i];
                filter.Execute(procEnv, ruleInterpreted.Action, matches);
            }

            FireMatchedAfterFilteringEvent(procEnv, matches, Rule.Special);

            if(matches.Count == 0)
            {
                procEnv.EndOfIteration(false, this);
                Rule.executionState = SequenceExecutionState.Fail;
                FireEndExecutionEvent(procEnv, null);
                return res;
            }

#if LOG_SEQUENCE_EXECUTION
            procEnv.Recorder.WriteLine("Rule " + Rule.GetRuleCallString(procEnv) + " matched " + matches.Count + " times");
#endif

            // the rule might be called again in the sequence, overwriting the matches object of the action
            // normally it's safe to assume the rule is not called again until its matches were processed,
            // allowing for the one matches object memory optimization, but here we must clone to prevent bad side effect
            // TODO: optimization; if it's ensured the sequence doesn't call this action again, we can omit this, requires call analysis
            matches = matches.Clone();

            // apply the sequence for every match found
            bool first = true;
            foreach(IMatch match in matches)
            {
                if(!first)
                    procEnv.EndOfIteration(true, this);
                Var.SetVariableValue(match, procEnv);
#if LOG_SEQUENCE_EXECUTION
                procEnv.Recorder.WriteLine("Iterating match: " + MatchPrinter.ToString(match, procEnv.Graph, ""));
#endif

                Rule.FireEnteringSequenceEvent(procEnv);
                Rule.executionState = SequenceExecutionState.Underway;
                FireMatchSelectedEvent(procEnv, match, Rule.Special, matches); // only called on an existing match
                FireFinishedSelectedMatchEvent(procEnv);
                Rule.executionState = SequenceExecutionState.Success;
                Rule.FireExitingSequenceEvent(procEnv);

                // rule matching simulated so it can be shown in the debugger, now execute the sequence
                Seq.ResetExecutionState();
                res &= Seq.Apply(procEnv);
                first = false;
            }
            FireFinishedEvent(procEnv, matches, Rule.Special);
            procEnv.EndOfIteration(false, this);
            FireEndExecutionEvent(procEnv, null);
            return res;
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors, SequenceBase target)
        {
            Var.GetLocalVariables(variables);
            if(Rule.GetLocalVariables(variables, constructors, target))
                return true;
            if(Seq.GetLocalVariables(variables, constructors, target))
                return true;
            RemoveVariablesFallingOutOfScope(variables, VariablesFallingOutOfScopeOnLeavingFor);
            variables.Remove(Var);
            return this == target;
        }

        public override IEnumerable<Sequence> Children
        {
            get
            {
                yield return Rule;
                yield return Seq;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "for{" + Var.Name + " in [?" + Rule.Symbol + "]; " + Seq.Symbol + "}"; }
        }
    }

    /// <summary>
    /// A sequence representing a sequence definition.
    /// It must be applied with a different method than the other sequences because it requires the parameter information.
    /// </summary>
    public abstract class SequenceDefinition : Sequence, ISequenceDefinition
    {
        public readonly String SequenceName;
        public readonly Annotations SequenceAnnotations;

        protected SequenceDefinition(SequenceType seqType, String sequenceName)
            : base(seqType)
        {
            SequenceName = sequenceName;
            SequenceAnnotations = new libGr.Annotations();
        }

        protected SequenceDefinition(SequenceDefinition that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            SequenceName = that.SequenceName;
            SequenceAnnotations = that.SequenceAnnotations;
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            throw new Exception("Can't apply sequence definition like a normal sequence");
        }

        /// <summary>
        /// Applies this sequence.
        /// </summary>
        /// <param name="procEnv">The graph processing environment on which this sequence is to be applied.
        ///     Contains especially the graph on which this sequence is to be applied.
        ///     The rules will only be chosen during the Sequence object instantiation, so
        ///     exchanging rules will have no effect for already existing Sequence objects.</param>
        /// <param name="arguments">Input arguments</param>
        /// <param name="returnValues">Output return values</param>
        /// <returns>True, iff the sequence succeeded</returns>
        public abstract bool Apply(IGraphProcessingEnvironment procEnv, object[] arguments, out object[] returnValues);

        public String Name { get { return SequenceName; } }

        /// <summary>
        /// The annotations of the sequence
        /// </summary>
        public Annotations Annotations
        {
            get { return SequenceAnnotations; }
        }

        public override int Precedence
        {
            get { return -1; }
        }
    }

    /// <summary>
    /// An sequence representing an interpreted sequence definition.
    /// Like the other sequences it can be directly interpreted (but with a different apply method),
    /// in contrast to the others it always must be the root sequence.
    /// </summary>
    public class SequenceDefinitionInterpreted : SequenceDefinition
    {
        public readonly SequenceVariable[] InputVariables;
        public readonly SequenceVariable[] OutputVariables;
        public readonly object[] ReturnValues;
        public readonly Sequence Seq;

        // a cache for copies of sequence definitions, accessed by the name
        private static readonly Dictionary<String, Stack<SequenceDefinition>> nameToCopies =
            new Dictionary<string, Stack<SequenceDefinition>>();

        // an empty stack to return an iterator if the copies cache does not contain a value for a given name
        private static readonly Stack<SequenceDefinition> emptyStack =
            new Stack<SequenceDefinition>();

        public SequenceDefinitionInterpreted(String sequenceName,
            SequenceVariable[] inputVariables,
            SequenceVariable[] outputVariables,
            Sequence seq)
            : base(SequenceType.SequenceDefinitionInterpreted, sequenceName)
        {
            InputVariables = inputVariables;
            OutputVariables = outputVariables;
            ReturnValues = new object[OutputVariables.Length];
            Seq = seq;
        }

        protected SequenceDefinitionInterpreted(SequenceDefinitionInterpreted that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
            CopyVars(originalToCopy, procEnv, that.InputVariables,
                out InputVariables);
            CopyVars(originalToCopy, procEnv, that.OutputVariables,
                out OutputVariables);
            ReturnValues = new object[that.OutputVariables.Length];
            Seq = that.Seq.Copy(originalToCopy, procEnv);
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceDefinitionInterpreted(this, originalToCopy, procEnv);
        }

        public override bool Apply(IGraphProcessingEnvironment procEnv, object[] arguments, out object[] returnValues)
        {
            // If this sequence definition is currently executed
            // we must copy it and use the copy in its place
            // to prevent state corruption.
            if(executionState == SequenceExecutionState.Underway)
            {
                return ApplyCopy(procEnv, arguments, out returnValues);
            }

            //FireBeginExecutionEvent(procEnv);
            FireEnteringSequenceEvent(procEnv);
            executionState = SequenceExecutionState.Underway;
#if LOG_SEQUENCE_EXECUTION
            procEnv.Recorder.WriteLine("Before executing sequence definition " + Id + ": " + Symbol);
#endif
            bool res = ApplyImpl(procEnv, arguments, out returnValues);
#if LOG_SEQUENCE_EXECUTION
            procEnv.Recorder.WriteLine("After executing sequence definition " + Id + ": " + Symbol + " result " + res);
#endif
            executionState = res ? SequenceExecutionState.Success : SequenceExecutionState.Fail;

            procEnv.EndOfIteration(false, this);

            FireExitingSequenceEvent(procEnv);
            //FireEndExecutionEvent(procEnv, null);

            ResetExecutionState(); // state is shown by call, we don't exist any more for the debugger

            return res;
        }

        // creates or reuses a copy and applies the copy
        protected bool ApplyCopy(IGraphProcessingEnvironment procEnv, object[] arguments, out object[] returnValues)
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
            //sequenceInvocation.SequenceDef = seqCopy;
            bool success = seqCopy.Apply(procEnv, arguments, out returnValues);
            //sequenceInvocation.SequenceDef = this;
            if(!nameToCopies.ContainsKey(SequenceName))
                nameToCopies.Add(SequenceName, new Stack<SequenceDefinition>());
            nameToCopies[SequenceName].Push(seqCopy);
            return success;
        }

        // applies the sequence of/in the sequence definition
        protected bool ApplyImpl(IGraphProcessingEnvironment procEnv, object[] arguments, out object[] returnValues)
        {
            if(arguments.Length != InputVariables.Length)
                throw new Exception("Number of input parameters given and expected differ for " + Symbol);

            // prefill the local input variables with the invocation values, read from parameter variables of the caller
            for(int i=0; i<arguments.Length; ++i)
            {
                InputVariables[i].SetVariableValue(arguments[i], procEnv);
            }

            bool success = Seq.Apply(procEnv);

            returnValues = ReturnValues;

            if(success)
            {
                // postfill the return-to variables of the caller with the return values, read from the local output variables
                for(int i = 0; i < OutputVariables.Length; ++i)
                {
                    returnValues[i] = OutputVariables[i].GetVariableValue(procEnv);
                }
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

        public override SequenceBase GetCurrentlyExecutedSequenceBase()
        {
            if(Seq.GetCurrentlyExecutedSequenceBase() != null)
                return Seq.GetCurrentlyExecutedSequenceBase();
            if(executionState == SequenceExecutionState.Underway)
                return this;
            return null;
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors, SequenceBase target)
        {
            foreach(SequenceVariable seqVar in InputVariables)
            {
                seqVar.GetLocalVariables(variables);
            }
            foreach(SequenceVariable seqVar in OutputVariables)
            {
                seqVar.GetLocalVariables(variables);
            }
            if(Seq.GetLocalVariables(variables, constructors, target))
                return true;
            return this == target;
        }

        public override IEnumerable<Sequence> Children
        {
            get { yield return Seq; }
        }

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
                        if(i != InputVariables.Length - 1)
                            sb.Append(",");
                    }
                    sb.Append(")");
                }
                if(OutputVariables.Length > 0)
                {
                    sb.Append(":(");
                    for(int i = 0; i < OutputVariables.Length; ++i)
                    {
                        sb.Append(OutputVariables[i].Name + ":" + OutputVariables[i].Type);
                        if(i != OutputVariables.Length - 1)
                            sb.Append(",");
                    }
                    sb.Append(")");
                }
                return sb.ToString();
            }
        }
    }

    /// <summary>
    /// A sequence representing a compiled sequence definition.
    /// The generated subclass contains the method implementing the real sequence,
    /// and ApplyImpl calling that method mapping SequenceInvocationParameterBindings to the exact parameters of that method.
    /// </summary>
    public abstract class SequenceDefinitionCompiled : SequenceDefinition
    {
        public readonly DefinedSequenceInfo SeqInfo;

        protected SequenceDefinitionCompiled(String sequenceName, DefinedSequenceInfo seqInfo)
            : base(SequenceType.SequenceDefinitionCompiled, sequenceName)
        {
            SeqInfo = seqInfo;
            foreach(KeyValuePair<string, string> annotation in seqInfo.annotations)
            {
                SequenceAnnotations.annotations.Add(annotation.Key, annotation.Value);
            }
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

        public abstract override bool Apply(IGraphProcessingEnvironment procEnv, object[] arguments, out object[] returnValues);

        public override SequenceBase GetCurrentlyExecutedSequenceBase()
        {
            throw new Exception("GetCurrentlyExecutedSequence not supported on compiled sequences");
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors, SequenceBase target)
        {
            throw new Exception("GetLocalVariables not supported on compiled sequences");
        }

        public override IEnumerable<Sequence> Children
        {
            get { yield break; }
        }

        public override string Symbol
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(SequenceName);
                if(SeqInfo.Parameters.Length > 0)
                {
                    sb.Append("(");
                    for(int i = 0; i < SeqInfo.Parameters.Length; ++i)
                    {
                        sb.Append(SeqInfo.Parameters[i] + ":" + SeqInfo.ParameterTypes[i].ToString());
                        if(i != SeqInfo.Parameters.Length - 1)
                            sb.Append(",");
                    }
                    sb.Append(")");
                }
                if(SeqInfo.OutParameters.Length > 0)
                {
                    sb.Append(":(");
                    for(int i = 0; i < SeqInfo.OutParameters.Length; ++i)
                    {
                        sb.Append(SeqInfo.OutParameters[i] + ":" + SeqInfo.OutParameterTypes[i].ToString());
                        if(i != SeqInfo.OutParameters.Length - 1)
                            sb.Append(",");
                    }
                    sb.Append(")");
                }
                return sb.ToString();
            }
        }
    }

    public abstract class SequenceSequenceCall : SequenceSpecial, SequenceInvocation
    {
        /// <summary>
        /// An array of expressions used to compute the input arguments.
        /// </summary>
        public readonly SequenceExpression[] ArgumentExpressions;

        /// <summary>
        /// Buffer to store the argument values for the call; used to avoid unneccessary memory allocations.
        /// </summary>
        public readonly object[] Arguments;

        /// <summary>
        /// An array of variables used for the return values. Might be empty if the caller is not interested in available returns values.
        /// </summary>
        public readonly SequenceVariable[] ReturnVars;

        /// <summary>
        /// The subgraph to be switched to for sequence execution.
        /// </summary>
        public readonly SequenceVariable subgraph;

        public abstract String Name { get; }
        public abstract String Package { get; }
        public abstract String PackagePrefixedName { get; }

        public SequenceVariable Subgraph
        {
            get { return subgraph; }
        }

        protected SequenceSequenceCall(List<SequenceExpression> argExprs, List<SequenceVariable> returnVars, SequenceVariable subgraph,
            bool special)
            : base(SequenceType.SequenceCall, special)
        {
            InitializeArgumentExpressionsAndArguments(argExprs, out ArgumentExpressions, out Arguments);
            InitializeReturnVariables(returnVars, out ReturnVars);
            this.subgraph = subgraph;
        }

        protected SequenceSequenceCall(SequenceSequenceCall that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            CopyArgumentExpressionsAndArguments(originalToCopy, procEnv, that.ArgumentExpressions,
                out ArgumentExpressions, out Arguments);
            CopyVars(originalToCopy, procEnv, that.ReturnVars,
                out ReturnVars);
            if(that.subgraph != null)
                subgraph = that.subgraph.Copy(originalToCopy, procEnv);
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors, SequenceBase target)
        {
            GetLocalVariables(ArgumentExpressions, variables, constructors);
            GetLocalVariables(ReturnVars, variables, constructors);
            if(subgraph != null)
                subgraph.GetLocalVariables(variables);
            return this == target;
        }

        public override IEnumerable<SequenceBase> ChildrenBase
        { 
            get 
            {
                foreach(SequenceExpression expr in ArgumentExpressions)
                {
                    yield return expr;
                }
            }
        }
        public override IEnumerable<Sequence> Children
        {
            get { yield break; }
        }

        public override int Precedence
        {
            get { return 8; }
        }
    }

    public class SequenceSequenceCallInterpreted : SequenceSequenceCall
    {
        /// <summary>
        /// The defined sequence to be used
        /// </summary>
        public ISequenceDefinition SequenceDef;

        public override String Name
        {
            get { return SequenceDef.Name; }
        }

        public override String Package
        {
            get { return SequenceDef is SequenceDefinitionCompiled ? ((SequenceDefinitionCompiled)SequenceDef).SeqInfo.Package : null; }
        }

        public override String PackagePrefixedName
        {
            get { return Package != null ? ((SequenceDefinitionCompiled)SequenceDef).SeqInfo.Package + "::" + SequenceDef.Name : SequenceDef.Name; }
        }

        public SequenceSequenceCallInterpreted(ISequenceDefinition SequenceDef,
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars, SequenceVariable subgraph,
            bool special)
            : base(argExprs, returnVars, subgraph, special)
        {
            this.SequenceDef = SequenceDef;
        }

        protected SequenceSequenceCallInterpreted(SequenceSequenceCallInterpreted that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
            SequenceDef = that.SequenceDef;
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceSequenceCallInterpreted(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            env.CheckSequenceCall(this);
        }

        internal override void ReplaceSequenceDefinition(SequenceDefinition oldDef, SequenceDefinition newDef)
        {
            if(SequenceDef == oldDef)
                SequenceDef = newDef;
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            ISequenceDefinition seqDef = SequenceDef;
#if LOG_SEQUENCE_EXECUTION
            procEnv.Recorder.WriteLine("Applying sequence " + GetSequenceCallString(procEnv));
#endif

            FillArgumentsFromArgumentExpressions(ArgumentExpressions, Arguments, procEnv);

            if(subgraph != null)
                procEnv.SwitchToSubgraph((IGraph)subgraph.GetVariableValue(procEnv));

            object[] returnValues;
            bool res = seqDef.Apply(procEnv, Arguments, out returnValues);

            if(res)
                FillReturnVariablesFromValues(ReturnVars, procEnv, returnValues);

#if LOG_SEQUENCE_EXECUTION
            if(res)
            {
                procEnv.Recorder.WriteLine("Applied sequence " + Symbol + " successfully");
                procEnv.Recorder.Flush();
            }
#endif
            return res;
        }

        public override IEnumerable<SequenceBase> ChildrenBase
        {
            get
            {
                foreach(SequenceExpression expr in ArgumentExpressions)
                {
                    yield return expr;
                }
            }
        }

        public override IEnumerable<Sequence> Children
        {
            get { yield break; }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public String GetSequenceCallString(IGraphProcessingEnvironment procEnv)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(SequenceDef.Name);
            if(ArgumentExpressions.Length > 0)
            {
                sb.Append("(");
                for(int i = 0; i < ArgumentExpressions.Length; ++i)
                {
                    sb.Append(EmitHelper.ToStringAutomatic(ArgumentExpressions[i].Evaluate(procEnv), procEnv.Graph, false, null, null));
                    if(i != ArgumentExpressions.Length - 1)
                        sb.Append(",");
                }
                sb.Append(")");
            }
            return sb.ToString();
        }

        protected String GetSequenceString()
        {
            StringBuilder sb = new StringBuilder();
            if(ReturnVars.Length > 0)
            {
                sb.Append("(");
                for(int i = 0; i < ReturnVars.Length; ++i)
                {
                    sb.Append(ReturnVars[i].Name);
                    if(i != ReturnVars.Length - 1)
                        sb.Append(",");
                }
                sb.Append(")=");
            }
            if(subgraph != null)
                sb.Append(subgraph.Name + ".");
            sb.Append(SequenceDef.Name);
            if(ArgumentExpressions.Length > 0)
            {
                sb.Append("(");
                for(int i = 0; i < ArgumentExpressions.Length; ++i)
                {
                    sb.Append(ArgumentExpressions[i].Symbol);
                    if(i != ArgumentExpressions.Length - 1)
                        sb.Append(",");
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

    public class SequenceSequenceCallCompiled : SequenceSequenceCall
    {
        public readonly String name;
        public readonly String package;
        public readonly String packagePrefixedName;

        public override String Name
        {
            get { return name; }
        }

        public override String Package
        {
            get { return package; }
        }

        public override String PackagePrefixedName
        {
            get { return packagePrefixedName; }
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            env.CheckSequenceCall(this);
        }

        public SequenceSequenceCallCompiled(String Name, String Package, String PackagePrefixedName,
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars, SequenceVariable subgraph,
            bool special)
            : base(argExprs, returnVars, subgraph, special)
        {
            this.name = Name;
            this.package = Package;
            this.packagePrefixedName = PackagePrefixedName;
        }

        protected SequenceSequenceCallCompiled(SequenceSequenceCallCompiled that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
            name = that.name;
            package = that.package;
            packagePrefixedName = that.packagePrefixedName;
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceSequenceCallCompiled(this, originalToCopy, procEnv);
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<SequenceBase> ChildrenBase
        {
            get
            {
                foreach(SequenceExpression expr in ArgumentExpressions)
                {
                    yield return expr;
                }
            }
        }

        public override IEnumerable<Sequence> Children
        {
            get { yield break; }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        protected String GetSequenceString()
        {
            StringBuilder sb = new StringBuilder();
            if(ReturnVars.Length > 0)
            {
                sb.Append("(");
                for(int i = 0; i < ReturnVars.Length; ++i)
                {
                    sb.Append(ReturnVars[i].Name);
                    if(i != ReturnVars.Length - 1)
                        sb.Append(",");
                }
                sb.Append(")=");
            }
            if(subgraph != null)
                sb.Append(subgraph.Name + ".");
            sb.Append(name);
            if(ArgumentExpressions.Length > 0)
            {
                sb.Append("(");
                for(int i = 0; i < ArgumentExpressions.Length; ++i)
                {
                    sb.Append(ArgumentExpressions[i].Symbol);
                    if(i != ArgumentExpressions.Length - 1)
                        sb.Append(",");
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

    public class SequenceExecuteInSubgraph : SequenceUnary
    {
        public readonly SequenceExpression SubgraphExpr;
        public readonly SequenceExpression ValueExpr;

        bool InParallel;
        public IGraph Subgraph; // only execution helper
        public readonly SequenceVariable ValueVariable; // only of relevance if ValueExpr != null and contained in SequenceParallelExecute

        public SequenceExecuteInSubgraph(SequenceExpression subgraphExpr, SequenceExpression valueExpr, SequenceVariable valueVariable, Sequence seq, bool inParallel)
            : base(SequenceType.ExecuteInSubgraph, seq)
        {
            SubgraphExpr = subgraphExpr;
            ValueExpr = valueExpr;
            ValueVariable = valueVariable;
            InParallel = inParallel;
        }

        public SequenceExecuteInSubgraph(SequenceExpression subgraphExpr, Sequence seq, bool inParallel)
            : this(subgraphExpr, null, null, seq, inParallel)
        {
        }

        protected SequenceExecuteInSubgraph(SequenceExecuteInSubgraph that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
            SubgraphExpr = that.SubgraphExpr.CopyExpression(originalToCopy, procEnv);
            if(that.ValueExpr != null)
                ValueExpr = that.ValueExpr.CopyExpression(originalToCopy, procEnv);
            if(that.ValueVariable != null)
                ValueVariable = that.ValueVariable.Copy(originalToCopy, procEnv);
            InParallel = that.InParallel;
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExecuteInSubgraph(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env);

            if(InParallel && SubgraphExpr.Type(env).StartsWith("array<"))
            {
                if(SubgraphExpr.Type(env) != "array<graph>")
                    throw new SequenceParserExceptionTypeMismatch(SubgraphExpr.Symbol, "array<graph>", SubgraphExpr.Type(env));

                if(ValueExpr != null && !ValueExpr.Type(env).StartsWith("array<"))
                    throw new SequenceParserExceptionTypeMismatch(ValueExpr.Symbol, "array type", ValueExpr.Type(env));

                return;
            }

            if(!TypesHelper.IsSameOrSubtype(SubgraphExpr.Type(env), "graph", env.Model))
                throw new SequenceParserExceptionTypeMismatch(SubgraphExpr.Symbol, "graph", SubgraphExpr.Type(env));

            if(ValueExpr != null && ValueExpr.Type(env) == "")
                throw new SequenceParserExceptionTypeMismatch(ValueExpr.Symbol, "non untyped type", ValueExpr.Type(env));
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors, SequenceBase target)
        {
            Seq.GetLocalVariables(variables, constructors, target);
            SubgraphExpr.GetLocalVariables(variables, constructors, target);
            if(ValueExpr != null)
                ValueExpr.GetLocalVariables(variables, constructors, target);
            return this == target;
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            if(!InParallel)
            {
                IGraph subgraph = (IGraph)SubgraphExpr.Evaluate(procEnv);
                procEnv.SwitchToSubgraph(subgraph);
            }

            bool res = Seq.Apply(procEnv);

            if(!InParallel)
                procEnv.ReturnFromSubgraph();

            return res;
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "in " + (SubgraphExpr.Symbol) + (ValueExpr != null ? ", " + ValueExpr.Symbol : "") + " { " + Seq.Symbol + " }" ; }
        }
    }

    public abstract class SequenceParallel : Sequence
    {
        protected SequenceParallel(SequenceType seqType)
            : base(seqType)
        {
        }

        protected SequenceParallel(Sequence that)
            : base(that)
        {
        }

        public abstract IEnumerable<SequenceExecuteInSubgraph> ParallelChildren { get; }
    }

    public class SequenceParallelExecute : SequenceParallel
    {
        public readonly List<SequenceExecuteInSubgraph> InSubgraphExecutions;
        public readonly List<SequenceVariable> ResultVariables;

        public SequenceParallelExecute(List<SequenceExecuteInSubgraph> inSubgraphExecutions, List<SequenceVariable> resultVariables) : base(SequenceType.ParallelExecute)
        {
            InSubgraphExecutions = inSubgraphExecutions;
            ResultVariables = resultVariables;
        }

        protected SequenceParallelExecute(SequenceParallelExecute that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            InSubgraphExecutions = new List<SequenceExecuteInSubgraph>();
            foreach(SequenceExecuteInSubgraph inSubgraphExecution in that.InSubgraphExecutions)
            {
                InSubgraphExecutions.Add((SequenceExecuteInSubgraph)inSubgraphExecution.Copy(originalToCopy, procEnv));
            }
            ResultVariables = new List<SequenceVariable>();
            foreach(SequenceVariable resultVariable in that.ResultVariables)
            {
                ResultVariables.Add(resultVariable.Copy(originalToCopy, procEnv));
            }
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceParallelExecute(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            foreach(SequenceExecuteInSubgraph inSubgraphExecution in InSubgraphExecutions)
            {
                if(inSubgraphExecution.ValueVariable != null)
                    inSubgraphExecution.ValueVariable.RedefineLocalVariableType(inSubgraphExecution.ValueExpr.Type(env));
            }

            base.Check(env);

            if(ResultVariables.Count > 0)
            {
                if(ResultVariables.Count != InSubgraphExecutions.Count)
                    throw new Exception("Mismatch in number of result assignments (/variables) and parallel execution parts (" + ResultVariables.Count + " vs. " + InSubgraphExecutions.Count + ")");
                foreach(SequenceVariable resultVariable in ResultVariables)
                {
                    if(!TypesHelper.IsSameOrSubtype(resultVariable.Type, "boolean", env.Model))
                        throw new SequenceParserExceptionTypeMismatch(resultVariable.Name, "boolean", resultVariable.Type);
                }
            }
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
#if LOG_SEQUENCE_EXECUTION
            procEnv.Recorder.WriteLine("Parallel execution");
#endif

            foreach(SequenceExecuteInSubgraph inSubgraphExecution in InSubgraphExecutions)
            {
                inSubgraphExecution.Subgraph = (IGraph)inSubgraphExecution.SubgraphExpr.Evaluate(procEnv);
                if(inSubgraphExecution.ValueExpr != null)
                    inSubgraphExecution.ValueVariable.SetVariableValue(inSubgraphExecution.ValueExpr.Evaluate(procEnv), procEnv);
            }

            List<bool> result = procEnv.ParallelApplyGraphRewriteSequences(this);

            for(int i = 0; i < ResultVariables.Count; ++i)
            {
                ResultVariables[i].SetVariableValue(result[i], procEnv);
            }

            return true;
        }

        public override SequenceBase GetCurrentlyExecutedSequenceBase()
        {
            foreach(SequenceExecuteInSubgraph inSubgraphExecution in InSubgraphExecutions)
            {
                if(inSubgraphExecution.GetCurrentlyExecutedSequenceBase() != null)
                    return inSubgraphExecution.GetCurrentlyExecutedSequenceBase();
            }
            if(executionState == SequenceExecutionState.Underway)
                return this;
            return null;
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors, SequenceBase target)
        {
            foreach(SequenceExecuteInSubgraph inSubgraphExecution in InSubgraphExecutions)
            {
                if(inSubgraphExecution.GetLocalVariables(variables, constructors, target))
                    return true;
            }
            return this == target;
        }

        public override IEnumerable<Sequence> Children
        {
            get
            {
                foreach(SequenceExecuteInSubgraph inSubgraphExecution in InSubgraphExecutions)
                {
                    yield return inSubgraphExecution;
                }
            }
        }

        public override IEnumerable<SequenceExecuteInSubgraph> ParallelChildren
        {
            get
            {
                foreach(SequenceExecuteInSubgraph inSubgraphExecution in InSubgraphExecutions)
                {
                    yield return inSubgraphExecution;
                }
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("parallel ");
                bool first = true;
                foreach(SequenceExecuteInSubgraph inSubgraphExecution in InSubgraphExecutions)
                {
                    if(first)
                        first = false;
                    else
                        sb.Append(", ");
                    sb.Append(inSubgraphExecution.Symbol);
                }
                return sb.ToString();
            }
        }
    }

    public class SequenceParallelArrayExecute : SequenceParallel
    {
        public readonly SequenceExecuteInSubgraph InSubgraphExecution;
        public readonly SequenceVariable ResultVariable;

        public readonly List<SequenceExecuteInSubgraph> InSubgraphExecutions; // copied runtime sequences executed in parallel

        public SequenceParallelArrayExecute(SequenceExecuteInSubgraph inSubgraphExecution, SequenceVariable resultVariable) : base(SequenceType.ParallelArrayExecute)
        {
            InSubgraphExecution = inSubgraphExecution;
            ResultVariable = resultVariable;
            InSubgraphExecutions = new List<SequenceExecuteInSubgraph>();
        }

        protected SequenceParallelArrayExecute(SequenceParallelArrayExecute that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            InSubgraphExecution = (SequenceExecuteInSubgraph)InSubgraphExecution.Copy(originalToCopy, procEnv);
            ResultVariable = ResultVariable.Copy(originalToCopy, procEnv);
            InSubgraphExecutions = new List<SequenceExecuteInSubgraph>();
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceParallelArrayExecute(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            if(InSubgraphExecution.ValueVariable != null)
                InSubgraphExecution.ValueVariable.RedefineLocalVariableType(TypesHelper.ExtractSrc(InSubgraphExecution.ValueExpr.Type(env)));

            if(InSubgraphExecution.SubgraphExpr.Type(env) != "" && !InSubgraphExecution.SubgraphExpr.Type(env).StartsWith("array<"))
                throw new SequenceParserExceptionTypeMismatch(InSubgraphExecution.SubgraphExpr.Symbol, "array<graph>", InSubgraphExecution.SubgraphExpr.Type(env));

            base.Check(env);

            if(ResultVariable != null)
            {
                if(!TypesHelper.IsSameOrSubtype(ResultVariable.Type, "array<boolean>", env.Model))
                    throw new SequenceParserExceptionTypeMismatch(ResultVariable.Name, "array<boolean>", ResultVariable.Type);
            }
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
#if LOG_SEQUENCE_EXECUTION
            procEnv.Recorder.WriteLine("Parallel execution");
#endif

            List<IGraph> subgraphs = (List<IGraph>)InSubgraphExecution.SubgraphExpr.Evaluate(procEnv);
            InSubgraphExecutions.Clear();
            foreach(IGraph subgraph in subgraphs)
            {
                SequenceExecuteInSubgraph inSubgraph = (SequenceExecuteInSubgraph)InSubgraphExecution.Copy(new Dictionary<SequenceVariable, SequenceVariable>(), procEnv);
                inSubgraph.Subgraph = subgraph;
                InSubgraphExecutions.Add(inSubgraph);
            }

            if(InSubgraphExecution.ValueExpr != null)
            {
                IList values = (IList)InSubgraphExecution.ValueExpr.Evaluate(procEnv);

                if(values.Count != subgraphs.Count)
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "amount of values same as amount of subgraphs: " + subgraphs.Count, "amount of values: " + values.Count);

                for(int i=0; i < InSubgraphExecutions.Count; ++i)
                {
                    InSubgraphExecutions[i].ValueVariable.SetVariableValue(values[i], procEnv);
                }
            }

            List<bool> result = procEnv.ParallelApplyGraphRewriteSequences(this);

            if(ResultVariable != null)
                ResultVariable.SetVariableValue(result, procEnv);

            return true;
        }

        public override SequenceBase GetCurrentlyExecutedSequenceBase()
        {
            if(InSubgraphExecution.GetCurrentlyExecutedSequenceBase() != null)
                return InSubgraphExecution.GetCurrentlyExecutedSequenceBase();
            if(executionState == SequenceExecutionState.Underway)
                return this;
            return null;
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors, SequenceBase target)
        {
            if(InSubgraphExecution.GetLocalVariables(variables, constructors, target))
                return true;
            return this == target;
        }

        public override IEnumerable<Sequence> Children
        {
            get
            {
                yield return InSubgraphExecution;
            }
        }

        public override IEnumerable<SequenceExecuteInSubgraph> ParallelChildren
        {
            get
            {
                foreach(SequenceExecuteInSubgraph inSubgraphExecution in InSubgraphExecutions)
                {
                    yield return inSubgraphExecution;
                }
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("parallel array ");
                sb.Append(InSubgraphExecution.Symbol);
                return sb.ToString();
            }
        }
    }

    public class SequenceLock : SequenceUnary
    {
        public readonly SequenceExpression LockObjectExpr;

        public SequenceLock(SequenceExpression lockObjectExpr, Sequence seq)
            : base(SequenceType.Lock, seq)
        {
            LockObjectExpr = lockObjectExpr;
        }

        protected SequenceLock(SequenceLock that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
            LockObjectExpr = that.LockObjectExpr.CopyExpression(originalToCopy, procEnv);
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceLock(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env);

            if(!TypesHelper.IsLockableType(LockObjectExpr.Type(env), env.Model))
                throw new SequenceParserExceptionTypeMismatch(Symbol, "lockable type (value types are not lockable)", LockObjectExpr.Type(env));
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors, SequenceBase target)
        {
            Seq.GetLocalVariables(variables, constructors, target);
            LockObjectExpr.GetLocalVariables(variables, constructors, target);
            return this == target;
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            object lockObject = LockObjectExpr.Evaluate(procEnv);

            lock(lockObject)
            {
                return Seq.Apply(procEnv);
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "lock(" + LockObjectExpr.Symbol + ") { " + Seq.Symbol + " }"; }
        }
    }

    public class SequenceBooleanComputation : SequenceSpecial
    {
        public readonly SequenceComputation Computation;
        public readonly List<SequenceVariable> VariablesFallingOutOfScopeOnLeavingComputation;

        public SequenceBooleanComputation(SequenceComputation comp, List<SequenceVariable> variablesFallingOutOfScopeOnLeavingComputation, bool special)
            : base(SequenceType.BooleanComputation, special)
        {
            Computation = comp;
            VariablesFallingOutOfScopeOnLeavingComputation = variablesFallingOutOfScopeOnLeavingComputation;
            if(VariablesFallingOutOfScopeOnLeavingComputation == null)
                VariablesFallingOutOfScopeOnLeavingComputation = new List<SequenceVariable>();
        }

        protected SequenceBooleanComputation(SequenceBooleanComputation that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            Computation = that.Computation.Copy(originalToCopy, procEnv);
            VariablesFallingOutOfScopeOnLeavingComputation = CopyVars(originalToCopy, procEnv, that.VariablesFallingOutOfScopeOnLeavingComputation);
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceBooleanComputation(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            Computation.Check(env);
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            object val = Computation.Execute(procEnv);
            if(Computation.ReturnsValue)
                return !TypesHelper.IsDefaultValue(val);
            else
                return true;
        }

        public override bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors, SequenceBase target)
        {
            Computation.GetLocalVariables(variables, constructors);
            RemoveVariablesFallingOutOfScope(variables, VariablesFallingOutOfScopeOnLeavingComputation);
            return this == target;
        }

        public override IEnumerable<SequenceBase> ChildrenBase
        {
            get { yield return Computation; }
        }

        public override IEnumerable<Sequence> Children
        {
            get { yield break; }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get
            {
                return Special ? "%{ " + (Computation is SequenceExpression ? "{" + Computation.Symbol + "}" : Computation.Symbol) + " }" 
                    : "{ " + (Computation is SequenceExpression ? "{" + Computation.Symbol + "}" : Computation.Symbol) + " }";
            }
        }
    }

    /// <summary>
    /// A dummy sequence to be used in contexts where a sequence is needed formally but not available (null-element/guard).
    /// </summary>
    public class SequenceDummy : Sequence
    {
        public SequenceDummy()
            : base(SequenceType.Dummy)
        {
        }

        protected SequenceDummy(SequenceDummy that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
        }

        internal override Sequence Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceDummy(this, originalToCopy, procEnv);
        }

        protected override bool ApplyImpl(IGraphProcessingEnvironment procEnv)
        {
            return false;
        }

        public override IEnumerable<Sequence> Children
        {
            get { yield break; }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "dummy"; }
        }
    }
}

using System;
using System.Collections.Generic;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// Specifies the actual subtype used for a Sequence.
    /// </summary>
    public enum SequenceType
    {
        LazyOr, LazyAnd, StrictOr, Xor, StrictAnd, Not, Min, MinMax,
        Rule, RuleAll, Def, True, False, AssignVarToVar, AssignElemToVar, Transaction
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
        protected static Random randomGenerator = new Random();

        /// <summary>
        /// The type of the sequence (e.g. LazyOr or Transaction)
        /// </summary>
        public SequenceType SequenceType;

        /// <summary>
        /// Applies this sequence
        /// </summary>
        /// <param name="actions">The actions object containing the graph and the events to be used.
        ///     The rules will only be choosen during the Sequence object instantiation, so
        ///     exchanging rules will have no effect for already existing Sequence objects.</param>
        /// <returns>True, iff the sequence succeeded</returns>
        public bool Apply(BaseActions actions)
        {
            actions.EnteringSequence(this);
            bool res = ApplyImpl(actions);
            actions.ExitingSequence(this);
            return res;
        }

        /// <summary>
        /// Applies this sequence. This function represents the actual implementation of the sequence.
        /// </summary>
        /// <param name="actions">The actions object containing the graph and the events to be used.</param>
        /// <returns>True, iff the sequence succeeded</returns>
        protected abstract bool ApplyImpl(BaseActions actions);

        /// <summary>
        /// Enumerates all child sequence objects
        /// </summary>
        public abstract IEnumerable<Sequence> Children { get; }

        /// <summary>
        /// The precedence of this operator. Zero is the highest priority, int.MaxValue the lowest.
        /// Used to add needed parentheses for printing sequences
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
        public SequenceSpecial(bool special) { Special = special; }
    }

    public class SequenceLazyOr : Sequence
    {
        public Sequence Left, Right;
        public bool Randomize;

        public SequenceLazyOr(Sequence left, Sequence right, bool random)
        {
            Left = left;
            Right = right;
            Randomize = random;
            SequenceType = SequenceType.LazyOr;
        }

        protected override bool ApplyImpl(BaseActions actions)
        {
            if(Randomize && randomGenerator.Next(2) == 1)
                return Right.Apply(actions) || Left.Apply(actions);
            else
                return Left.Apply(actions) || Right.Apply(actions);
        }

        public override IEnumerable<Sequence> Children
        {
            get { yield return Left; yield return Right; }
        }

        public override int Precedence { get { return 0; } }
        public override string Symbol { get { return "||"; } }
    }

    public class SequenceLazyAnd : Sequence
    {
        public Sequence Left, Right;
        public bool Randomize;

        public SequenceLazyAnd(Sequence left, Sequence right, bool random)
        {
            Left = left;
            Right = right;
            Randomize = random;
            SequenceType = SequenceType.LazyAnd;
        }

        protected override bool ApplyImpl(BaseActions actions)
        {
            if(Randomize && randomGenerator.Next(2) == 1)
                return Right.Apply(actions) && Left.Apply(actions);
            else
                return Left.Apply(actions) && Right.Apply(actions);
        }

        public override IEnumerable<Sequence> Children
        {
            get { yield return Left; yield return Right; }
        }

        public override int Precedence { get { return 1; } }
        public override string Symbol { get { return "&&"; } }
    }

    public class SequenceStrictOr : Sequence
    {
        public Sequence Left, Right;
        public bool Randomize;

        public SequenceStrictOr(Sequence left, Sequence right, bool random)
        {
            Left = left;
            Right = right;
            Randomize = random;
            SequenceType = SequenceType.StrictOr;
        }

        protected override bool ApplyImpl(BaseActions actions)
        {
            if(Randomize && randomGenerator.Next(2) == 1)
                return Right.Apply(actions) | Left.Apply(actions);
            else
                return Left.Apply(actions) | Right.Apply(actions);
        }

        public override IEnumerable<Sequence> Children
        {
            get { yield return Left; yield return Right; }
        }

        public override int Precedence { get { return 2; } }
        public override string Symbol { get { return "|"; } }
    }

    public class SequenceXor : Sequence
    {
        public Sequence Left, Right;
        public bool Randomize;

        public SequenceXor(Sequence left, Sequence right, bool random)
        {
            Left = left;
            Right = right;
            Randomize = random;
            SequenceType = SequenceType.Xor;
        }

        protected override bool ApplyImpl(BaseActions actions)
        {
            if(Randomize && randomGenerator.Next(2) == 1)
                return Right.Apply(actions) ^ Left.Apply(actions);
            else
                return Left.Apply(actions) ^ Right.Apply(actions);
        }

        public override IEnumerable<Sequence> Children
        {
            get { yield return Left; yield return Right; }
        }

        public override int Precedence { get { return 3; } }
        public override string Symbol { get { return "^"; } }
    }

    public class SequenceStrictAnd : Sequence
    {
        public Sequence Left, Right;
        public bool Randomize;

        public SequenceStrictAnd(Sequence left, Sequence right, bool random)
        {
            Left = left;
            Right = right;
            Randomize = random;
            SequenceType = SequenceType.StrictAnd;
        }

        protected override bool ApplyImpl(BaseActions actions)
        {
            if(Randomize && randomGenerator.Next(2) == 1)
                return Right.Apply(actions) & Left.Apply(actions);
            else
                return Left.Apply(actions) & Right.Apply(actions);
        }

        public override IEnumerable<Sequence> Children
        {
            get { yield return Left; yield return Right; }
        }

        public override int Precedence { get { return 4; } }
        public override string Symbol { get { return "&"; } }
    }

    public class SequenceNot : Sequence
    {
        public Sequence Seq;

        public SequenceNot(Sequence seq)
        {
            Seq = seq;
            SequenceType = SequenceType.Not;
        }

        protected override bool ApplyImpl(BaseActions actions)
        {
            return !Seq.Apply(actions);
        }

        public override IEnumerable<Sequence> Children { get { yield return Seq; } }
        public override int Precedence { get { return 5; } }
        public override string Symbol { get { return "!"; } }
    }

    public class SequenceMin : Sequence
    {
        public Sequence Seq;
        public int Min;

        public SequenceMin(Sequence seq, int min)
        {
            Seq = seq;
            Min = min;
            SequenceType = SequenceType.Min;
        }

        protected override bool ApplyImpl(BaseActions actions)
        {
            int i = 0;
            while(Seq.Apply(actions))
                i++;
            return i >= Min;
        }

        public override IEnumerable<Sequence> Children { get { yield return Seq; } }
        public override int Precedence { get { return 6; } }
        public override string Symbol { get { return "[" + Min + ":*]"; } }
    }

    public class SequenceMinMax : Sequence
    {
        public Sequence Seq;
        public int Min, Max;

        public SequenceMinMax(Sequence seq, int min, int max)
        {
            Seq = seq;
            Min = min;
            Max = max;
            SequenceType = SequenceType.MinMax;
        }

        protected override bool ApplyImpl(BaseActions actions)
        {
            int i;
            for(i = 0; i < Max; i++)
            {
                if(!Seq.Apply(actions)) break;
            }
            return i >= Min;
        }

        public override IEnumerable<Sequence> Children { get { yield return Seq; } }
        public override int Precedence { get { return 6; } }
        public override string Symbol { get { return "[" + Min + ":" + Max + "]"; } }
    }

    public class SequenceRule : SequenceSpecial
    {
        public RuleObject RuleObj;
        public bool Test;

        public SequenceRule(RuleObject ruleObj, bool special, bool test) : base(special)
        {
            RuleObj = ruleObj;
            Test = test;
            SequenceType = SequenceType.Rule;
        }

        protected override bool ApplyImpl(BaseActions actions)
        {
            return actions.ApplyRewrite(RuleObj, 0, 1, Special, Test) > 0;
        }

        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 7; } }

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
        public SequenceRuleAll(RuleObject ruleObj, bool special, bool test)
            : base(ruleObj, special, test)
        {
            SequenceType = SequenceType.RuleAll;
        }

        protected override bool ApplyImpl(BaseActions actions)
        {
            return actions.ApplyRewrite(RuleObj, -1, -1, Special, Test) > 0;
        }
        public override string Symbol
        { 
            get 
            {
                String prefix;
                if(Special)
                {
                    if(Test) prefix = "%?[";
                    else prefix = "%[";
                }
                else
                {
                    if(Test) prefix = "?[";
                    else prefix = "[";
                }
                return prefix + GetRuleString() + "]"; 
            }
        }
    }

    public class SequenceDef : Sequence
    {
        public String[] DefVars;

        public SequenceDef(String[] defVars)
        {
            DefVars = defVars;
            SequenceType = SequenceType.Def;
        }

        protected override bool ApplyImpl(BaseActions actions)
        {
            foreach(String defVar in DefVars)
                if(actions.Graph.GetVariableValue(defVar) == null) 
                    return false;

            return true;
        }

        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 7; } }
        public override string Symbol { get { return "def(" + String.Join(", ", DefVars) + ")"; } }
    }

    public class SequenceTrue : SequenceSpecial
    {
        public SequenceTrue(bool special) : base(special)
        {
            SequenceType = SequenceType.True;
        }

        protected override bool ApplyImpl(BaseActions actions) { return true; }
        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 7; } }
        public override string Symbol { get { return Special ? "%true" : "true"; } }
    }

    public class SequenceFalse : SequenceSpecial
    {
        public SequenceFalse(bool special) : base(special)
        {
            SequenceType = SequenceType.False;
        }

        protected override bool ApplyImpl(BaseActions actions) { return false; }
        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 7; } }
        public override string Symbol { get { return Special ? "%false" : "false"; } }
    }

    public class SequenceAssignVarToVar : Sequence
    {
        public String DestVar;
        public String SourceVar;

        public SequenceAssignVarToVar(String destVar, String sourceVar)
        {
            DestVar = destVar;
            SourceVar = sourceVar;
            SequenceType = SequenceType.AssignVarToVar;
        }

        protected override bool ApplyImpl(BaseActions actions)
        {
            actions.Graph.SetVariableValue(DestVar, actions.Graph.GetVariableValue(SourceVar));
            return true;                    // Semantics changed! Now always returns true, as it is always successful!
        }

        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 7; } }
        public override string Symbol { get { return DestVar + "=" + SourceVar; } }
    }

    public class SequenceAssignElemToVar : Sequence
    {
        public String DestVar;
        public IGraphElement Element;

        public SequenceAssignElemToVar(String destVar, IGraphElement elem)
        {
            DestVar = destVar;
            Element = elem;
            SequenceType = SequenceType.AssignElemToVar;
        }

        protected override bool ApplyImpl(BaseActions actions)
        {
            actions.Graph.SetVariableValue(DestVar, Element);
            return true;                    // Semantics changed! Now always returns true, as it is always successful!
        }

        public override IEnumerable<Sequence> Children { get { yield break; } }
        public override int Precedence { get { return 7; } }
        public override string Symbol { get { return DestVar + "=[<someelem>]"; } }
    }

    public class SequenceTransaction : Sequence
    {
        public Sequence Seq;

        public SequenceTransaction(Sequence seq)
        {
            Seq = seq;
            SequenceType = SequenceType.Transaction;
        }

        protected override bool ApplyImpl(BaseActions actions)
        {
            int transactionID = actions.Graph.TransactionManager.StartTransaction();
            int oldRewritesPerformed;

            if(actions.PerformanceInfo != null) oldRewritesPerformed = actions.PerformanceInfo.RewritesPerformed;
            else oldRewritesPerformed = -1;

            bool res = Seq.Apply(actions);

            if(res) actions.Graph.TransactionManager.Commit(transactionID);
            else
            {
                actions.Graph.TransactionManager.Rollback(transactionID);
                if(actions.PerformanceInfo != null)
                    actions.PerformanceInfo.RewritesPerformed = oldRewritesPerformed;
            }

            return res;
        }

        public override IEnumerable<Sequence> Children { get { yield return Seq; } }
        public override int Precedence { get { return 7; } }
        public override string Symbol { get { return "<>"; } }
    }
}

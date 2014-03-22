/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.3
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections.Generic;
using System.Text;

using de.unika.ipd.grGen.libGr;
using System.Diagnostics;
using de.unika.ipd.grGen.lgsp;

namespace de.unika.ipd.grGen.expression
{
    /// <summary>
    /// Base class of expressions and yieldings
    /// which allows to emit code and to iterate over the contained children
    /// </summary>
    public abstract class ExpressionOrYielding
    {
        /// <summary>
        /// emits c# code implementing the construct into the source builder
        /// to be implemented by concrete subclasses
        /// </summary>
        public abstract void Emit(SourceBuilder sourceCode);

        /// <summary>
        /// returns an enumerator over the contained children of this construct
        /// </summary>
        public virtual IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield break;
        }

        /// <summary>
        /// sets for the very node the parallelized flag (does not recurse)
        /// </summary>
        public virtual void SetNeedForParallelizedVersion(bool parallel)
        {
            // NOP, sufficient for most expressions and yieldings,
            // only the reachable-constructs and the user defined functions need to call a special version
            // that prevents issues due to the shared use of visited flags
        }
    }


    /// <summary>
    /// Base class of expressions used in conditions to constrain the pattern
    /// </summary>
    public abstract class Expression : ExpressionOrYielding
    {
        /// <summary>
        /// copies the expression, renaming all variables with the given suffix
        /// </summary>
        public abstract Expression Copy(string renameSuffix);
    }

    /// <summary>
    /// Base class of operator expressions
    /// </summary>
    public abstract class Operator : Expression
    {
    }

    /// <summary>
    /// Class representing an binary infix operator.
    /// </summary>
    public abstract class BinInfixOperator : Operator
    {
        public BinInfixOperator(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("(");
            Left.Emit(sourceCode);
            sourceCode.Append(GetInfixOperator());
            Right.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Left;
            yield return Right;
        }

        public abstract String GetInfixOperator();

        protected Expression Left;
        protected Expression Right;
    }

    /// <summary>
    /// Class representing an binary prefix operator in function notation.
    /// </summary>
    public abstract class BinFuncOperator : Operator
    {
        public BinFuncOperator(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append(GetFuncOperatorAndLParen());
            Left.Emit(sourceCode);
            sourceCode.Append(", ");
            Right.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Left;
            yield return Right;
        }

        public abstract String GetFuncOperatorAndLParen();

        protected Expression Left;
        protected Expression Right;
    }

    /// <summary>
    /// Class representing a conditional operator (a ? b : c).
    /// </summary>
    public class COND : Operator
    {
        public COND(Expression condition, Expression left, Expression right)
        {
            Condition = condition;
            Left = left;
            Right = right;
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("(");
            Condition.Emit(sourceCode);
            sourceCode.Append(" ? ");
            Left.Emit(sourceCode);
            sourceCode.Append(" : ");
            Right.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override Expression Copy(string renameSuffix)
        {
            return new COND(Condition.Copy(renameSuffix), Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Condition;
            yield return Left;
            yield return Right;
        }

        Expression Condition;
        Expression Left;
        Expression Right;
    }

    /// <summary>
    /// Class representing a logical or.
    /// </summary>
    public class LOG_OR : BinInfixOperator
    {
        public LOG_OR(Expression left, Expression right) : base(left, right) { }

        public override Expression Copy(string renameSuffix)
        {
            return new LOG_OR(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override string  GetInfixOperator()
        {
 	        return " || ";
        }
    }

    /// <summary>
    /// Class representing a logical and.
    /// </summary>
    public class LOG_AND : BinInfixOperator
    {
        public LOG_AND(Expression left, Expression right) : base(left, right) { }

        public override Expression Copy(string renameSuffix)
        {
            return new LOG_AND(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override string  GetInfixOperator()
        {
 	        return " && ";
        }
    }

    /// <summary>
    /// Class representing a bitwise or.
    /// </summary>
    public class BIT_OR : BinInfixOperator
    {
        public BIT_OR(Expression left, Expression right) : base(left, right) { }

        public override Expression Copy(string renameSuffix)
        {
            return new BIT_OR(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override string GetInfixOperator()
        {
            return " | ";
        }
    }

    /// <summary>
    /// Class representing the set/map union operator.
    /// </summary>
    public class DICT_BIT_OR : BinFuncOperator
    {
        public DICT_BIT_OR(Expression left, Expression right) : base(left, right) { }

        public override Expression Copy(string renameSuffix)
        {
            return new DICT_BIT_OR(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override string GetFuncOperatorAndLParen()
        {
            return "GRGEN_LIBGR.ContainerHelper.Union(";
        }
    }

    /// <summary>
    /// Class representing the set/map intersection operator.
    /// </summary>
    public class DICT_BIT_AND : BinFuncOperator
    {
        public DICT_BIT_AND(Expression left, Expression right) : base(left, right) { }

        public override Expression Copy(string renameSuffix)
        {
            return new DICT_BIT_AND(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override string GetFuncOperatorAndLParen()
        {
            return "GRGEN_LIBGR.ContainerHelper.Intersect(";
        }
    }

    /// <summary>
    /// Class representing the set/map except operator.
    /// </summary>
    public class DICT_EXCEPT : BinFuncOperator
    {
        public DICT_EXCEPT(Expression left, Expression right) : base(left, right) { }

        public override Expression Copy(string renameSuffix)
        {
            return new DICT_EXCEPT(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override string GetFuncOperatorAndLParen()
        {
            return "GRGEN_LIBGR.ContainerHelper.Except(";
        }
    }

    /// <summary>
    /// Class representing a bitwise xor.
    /// </summary>
    public class BIT_XOR : BinInfixOperator
    {
        public BIT_XOR(Expression left, Expression right) : base(left, right) { }

        public override Expression Copy(string renameSuffix)
        {
            return new BIT_XOR(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override string GetInfixOperator()
        {
            return " ^ ";
        }
    }

    /// <summary>
    /// Class representing a bitwise and.
    /// </summary>
    public class BIT_AND : BinInfixOperator
    {
        public BIT_AND(Expression left, Expression right) : base(left, right) { }

        public override Expression Copy(string renameSuffix)
        {
            return new BIT_AND(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override string GetInfixOperator()
        {
            return " & ";
        }
    }

    /// <summary>
    /// Class representing an equality comparison.
    /// </summary>
    public class EQ : BinInfixOperator
    {
        public EQ(Expression left, Expression right) : base(left, right) { }

        public override Expression Copy(string renameSuffix)
        {
            return new EQ(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override string GetInfixOperator()
        {
            return " == ";
        }
    }

    /// <summary>
    /// Class representing an inequality comparison.
    /// </summary>
    public class NE : BinInfixOperator
    {
        public NE(Expression left, Expression right) : base(left, right) { }

        public override Expression Copy(string renameSuffix)
        {
            return new NE(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override string GetInfixOperator()
        {
            return " != ";
        }
    }

    /// <summary>
    /// Class representing a less than comparison.
    /// </summary>
    public class LT : BinInfixOperator
    {
        public LT(Expression left, Expression right) : base(left, right) { }

        public override Expression Copy(string renameSuffix)
        {
            return new LT(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override string GetInfixOperator()
        {
            return " < ";
        }
    }

    /// <summary>
    /// Class representing a less than or equal comparison.
    /// </summary>
    public class LE : BinInfixOperator
    {
        public LE(Expression left, Expression right) : base(left, right) { }

        public override Expression Copy(string renameSuffix)
        {
            return new LE(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override string GetInfixOperator()
        {
            return " <= ";
        }
    }

    /// <summary>
    /// Class representing a greater than comparison.
    /// </summary>
    public class GT : BinInfixOperator
    {
        public GT(Expression left, Expression right) : base(left, right) { }

        public override Expression Copy(string renameSuffix)
        {
            return new GT(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override string GetInfixOperator()
        {
            return " > ";
        }
    }

    /// <summary>
    /// Class representing a greater than or equal comparison.
    /// </summary>
    public class GE : BinInfixOperator
    {
        public GE(Expression left, Expression right) : base(left, right) { }

        public override Expression Copy(string renameSuffix)
        {
            return new GE(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override string GetInfixOperator()
        {
            return " >= ";
        }
    }

    // external comparisons

    /// <summary>
    /// Class representing an equality comparison.
    /// </summary>
    public class EXTERNAL_EQ : BinFuncOperator
    {
        public EXTERNAL_EQ(Expression left, Expression right) : base(left, right) { }

        public override Expression Copy(string renameSuffix)
        {
            return new EXTERNAL_EQ(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override string GetFuncOperatorAndLParen()
        {
            return "GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsEqual(";
        }
    }

    /// <summary>
    /// Class representing an inequality comparison.
    /// </summary>
    public class EXTERNAL_NE : BinFuncOperator
    {
        public EXTERNAL_NE(Expression left, Expression right) : base(left, right) { }

        public override Expression Copy(string renameSuffix)
        {
            return new EXTERNAL_NE(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override string GetFuncOperatorAndLParen()
        {
            return "!GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsEqual(";
        }
    }

    /// <summary>
    /// Class representing a less than comparison.
    /// </summary>
    public class EXTERNAL_LT : BinFuncOperator
    {
        public EXTERNAL_LT(Expression left, Expression right) : base(left, right) { }

        public override Expression Copy(string renameSuffix)
        {
            return new EXTERNAL_LT(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override string GetFuncOperatorAndLParen()
        {
            return "GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsLower(";
        }
    }

    /// <summary>
    /// Class representing a less than or equal comparison.
    /// </summary>
    public class EXTERNAL_LE : Operator
    {
        public EXTERNAL_LE(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new EXTERNAL_LE(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("(GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsLower(");
            Left.Emit(sourceCode);
            sourceCode.Append(", ");
            Right.Emit(sourceCode);
            sourceCode.Append(")");
            sourceCode.Append("|| GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsEqual(");
            Left.Emit(sourceCode);
            sourceCode.Append(", ");
            Right.Emit(sourceCode);
            sourceCode.Append("))");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Left;
            yield return Right;
        }

        protected Expression Left;
        protected Expression Right;
    }

    /// <summary>
    /// Class representing a greater than comparison.
    /// </summary>
    public class EXTERNAL_GT : Operator
    {
        public EXTERNAL_GT(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new EXTERNAL_GT(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("(!GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsLower(");
            Left.Emit(sourceCode);
            sourceCode.Append(", ");
            Right.Emit(sourceCode);
            sourceCode.Append(")");
            sourceCode.Append("&& !GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsEqual(");
            Left.Emit(sourceCode);
            sourceCode.Append(", ");
            Right.Emit(sourceCode);
            sourceCode.Append("))");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Left;
            yield return Right;
        }

        protected Expression Left;
        protected Expression Right;
    }

    /// <summary>
    /// Class representing a greater than or equal comparison.
    /// </summary>
    public class EXTERNAL_GE : BinFuncOperator
    {
        public EXTERNAL_GE(Expression left, Expression right) : base(left, right) { }

        public override Expression Copy(string renameSuffix)
        {
            return new EXTERNAL_GE(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override string GetFuncOperatorAndLParen()
        {
            return "!GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsLower(";
        }
    }

    // TODO: a lot of the functions for the containers are mapping to the same code, 
    // helper functions with the same name resolved by the types in the generated code,
    // would make sense, safe code to not distinguish them

    /// <summary>
    /// Class representing set/map equality comparison.
    /// </summary>
    public class DICT_EQ : BinFuncOperator
    {
        public DICT_EQ(Expression left, Expression right) : base(left, right) { }

        public override Expression Copy(string renameSuffix)
        {
            return new DICT_EQ(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override string GetFuncOperatorAndLParen()
        {
            return "GRGEN_LIBGR.ContainerHelper.Equal(";
        }
    }

    /// <summary>
    /// Class representing set/map inequality comparison.
    /// </summary>
    public class DICT_NE : BinFuncOperator
    {
        public DICT_NE(Expression left, Expression right) : base(left, right) { }

        public override Expression Copy(string renameSuffix)
        {
            return new DICT_NE(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override string GetFuncOperatorAndLParen()
        {
            return "GRGEN_LIBGR.ContainerHelper.NotEqual(";
        }
    }

    /// <summary>
    /// Class representing proper subset/map comparison.
    /// </summary>
    public class DICT_LT : BinFuncOperator
    {
        public DICT_LT(Expression left, Expression right) : base(left, right) { }

        public override Expression Copy(string renameSuffix)
        {
            return new DICT_LT(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override string GetFuncOperatorAndLParen()
        {
            return "GRGEN_LIBGR.ContainerHelper.LessThan(";
        }
    }

    /// <summary>
    /// Class representing subset/map comparison.
    /// </summary>
    public class DICT_LE : BinFuncOperator
    {
        public DICT_LE(Expression left, Expression right) : base(left, right) { }

        public override Expression Copy(string renameSuffix)
        {
            return new DICT_LE(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override string GetFuncOperatorAndLParen()
        {
            return "GRGEN_LIBGR.ContainerHelper.LessOrEqual(";
        }
    }

    /// <summary>
    /// Class representing proper superset comparison.
    /// </summary>
    public class DICT_GT : BinFuncOperator
    {
        public DICT_GT(Expression left, Expression right) : base(left, right) { }

        public override Expression Copy(string renameSuffix)
        {
            return new DICT_GT(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override string GetFuncOperatorAndLParen()
        {
            return "GRGEN_LIBGR.ContainerHelper.GreaterThan(";
        }
    }

    /// <summary>
    /// Class representing superset comparison.
    /// </summary>
    public class DICT_GE : BinFuncOperator
    {
        public DICT_GE(Expression left, Expression right) : base(left, right) { }

        public override Expression Copy(string renameSuffix)
        {
            return new DICT_GE(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override string GetFuncOperatorAndLParen()
        {
            return "GRGEN_LIBGR.ContainerHelper.GreaterOrEqual(";
        }
    }

    /// <summary>
    /// Class representing array equality comparison.
    /// </summary>
    public class LIST_EQ : BinFuncOperator
    {
        public LIST_EQ(Expression left, Expression right) : base(left, right) { }

        public override Expression Copy(string renameSuffix)
        {
            return new LIST_EQ(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override string GetFuncOperatorAndLParen()
        {
            return "GRGEN_LIBGR.ContainerHelper.Equal(";
        }
    }

    /// <summary>
    /// Class representing array inequality comparison.
    /// </summary>
    public class LIST_NE : BinFuncOperator
    {
        public LIST_NE(Expression left, Expression right) : base(left, right) { }

        public override Expression Copy(string renameSuffix)
        {
            return new LIST_NE(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override string GetFuncOperatorAndLParen()
        {
            return "GRGEN_LIBGR.ContainerHelper.NotEqual(";
        }
    }

    /// <summary>
    /// Class representing proper subarray comparison.
    /// </summary>
    public class LIST_LT : BinFuncOperator
    {
        public LIST_LT(Expression left, Expression right) : base(left, right) { }

        public override Expression Copy(string renameSuffix)
        {
            return new LIST_LT(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override string GetFuncOperatorAndLParen()
        {
            return "GRGEN_LIBGR.ContainerHelper.LessThan(";
        }
    }

    /// <summary>
    /// Class representing subarray comparison.
    /// </summary>
    public class LIST_LE : BinFuncOperator
    {
        public LIST_LE(Expression left, Expression right) : base(left, right) { }

        public override Expression Copy(string renameSuffix)
        {
            return new LIST_LE(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override string GetFuncOperatorAndLParen()
        {
            return "GRGEN_LIBGR.ContainerHelper.LessOrEqual(";
        }
    }

    /// <summary>
    /// Class representing proper superarray comparison.
    /// </summary>
    public class LIST_GT : BinFuncOperator
    {
        public LIST_GT(Expression left, Expression right) : base(left, right) { }

        public override Expression Copy(string renameSuffix)
        {
            return new LIST_GT(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override string GetFuncOperatorAndLParen()
        {
            return "GRGEN_LIBGR.ContainerHelper.GreaterThan(";
        }
    }

    /// <summary>
    /// Class representing superarray comparison.
    /// </summary>
    public class LIST_GE : BinFuncOperator
    {
        public LIST_GE(Expression left, Expression right) : base(left, right) { }

        public override Expression Copy(string renameSuffix)
        {
            return new LIST_GE(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override string GetFuncOperatorAndLParen()
        {
            return "GRGEN_LIBGR.ContainerHelper.GreaterOrEqual(";
        }
    }

    /// <summary>
    /// Class representing deque equality comparison.
    /// </summary>
    public class DEQUE_EQ : BinFuncOperator
    {
        public DEQUE_EQ(Expression left, Expression right) : base(left, right) { }

        public override Expression Copy(string renameSuffix)
        {
            return new DEQUE_EQ(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override string GetFuncOperatorAndLParen()
        {
            return "GRGEN_LIBGR.ContainerHelper.Equal(";
        }
    }

    /// <summary>
    /// Class representing deque inequality comparison.
    /// </summary>
    public class DEQUE_NE : BinFuncOperator
    {
        public DEQUE_NE(Expression left, Expression right) : base(left, right) { }

        public override Expression Copy(string renameSuffix)
        {
            return new DEQUE_NE(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override string GetFuncOperatorAndLParen()
        {
            return "GRGEN_LIBGR.ContainerHelper.NotEqual(";
        }
    }

    /// <summary>
    /// Class representing proper subdeque comparison.
    /// </summary>
    public class DEQUE_LT : BinFuncOperator
    {
        public DEQUE_LT(Expression left, Expression right) : base(left, right) { }

        public override Expression Copy(string renameSuffix)
        {
            return new DEQUE_LT(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override string GetFuncOperatorAndLParen()
        {
            return "GRGEN_LIBGR.ContainerHelper.LessThan(";
        }
    }

    /// <summary>
    /// Class representing subdeque comparison.
    /// </summary>
    public class DEQUE_LE : BinFuncOperator
    {
        public DEQUE_LE(Expression left, Expression right) : base(left, right) { }

        public override Expression Copy(string renameSuffix)
        {
            return new DEQUE_LE(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override string GetFuncOperatorAndLParen()
        {
            return "GRGEN_LIBGR.ContainerHelper.LessOrEqual(";
        }
    }

    /// <summary>
    /// Class representing proper superdeque comparison.
    /// </summary>
    public class DEQUE_GT : BinFuncOperator
    {
        public DEQUE_GT(Expression left, Expression right) : base(left, right) { }

        public override Expression Copy(string renameSuffix)
        {
            return new DEQUE_GT(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override string GetFuncOperatorAndLParen()
        {
            return "GRGEN_LIBGR.ContainerHelper.GreaterThan(";
        }
    }

    /// <summary>
    /// Class representing superdeque comparison.
    /// </summary>
    public class DEQUE_GE : BinFuncOperator
    {
        public DEQUE_GE(Expression left, Expression right) : base(left, right) { }

        public override Expression Copy(string renameSuffix)
        {
            return new DEQUE_GE(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override string GetFuncOperatorAndLParen()
        {
            return "GRGEN_LIBGR.ContainerHelper.GreaterOrEqual(";
        }
    }

    /// <summary>
    /// Class representing a graph is-isomorph comparison.
    /// </summary>
    public class GRAPH_EQ : Operator
    {
        public GRAPH_EQ(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new GRAPH_EQ(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("((GRGEN_LIBGR.IGraph)");
            Left.Emit(sourceCode);
            sourceCode.Append(").IsIsomorph((GRGEN_LIBGR.IGraph)");
            Right.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Left;
            yield return Right;
        }

        protected Expression Left;
        protected Expression Right;
    }

    /// <summary>
    /// Class representing a graph is-not-isomorph comparison.
    /// </summary>
    public class GRAPH_NE : Operator
    {
        public GRAPH_NE(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new GRAPH_NE(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("!((GRGEN_LIBGR.IGraph)");
            Left.Emit(sourceCode);
            sourceCode.Append(").IsIsomorph((GRGEN_LIBGR.IGraph)");
            Right.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Left;
            yield return Right;
        }

        protected Expression Left;
        protected Expression Right;
    }

    /// <summary>
    /// Class representing a graph is-structural-equal (isomorph disregarding attributes) comparison.
    /// </summary>
    public class GRAPH_SE : Operator
    {
        public GRAPH_SE(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new GRAPH_SE(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("((GRGEN_LIBGR.IGraph)");
            Left.Emit(sourceCode);
            sourceCode.Append(").HasSameStructure((GRGEN_LIBGR.IGraph)");
            Right.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Left;
            yield return Right;
        }

        protected Expression Left;
        protected Expression Right;
    }

    /// <summary>
    /// Class representing a shift left expression.
    /// </summary>
    public class SHL : BinInfixOperator
    {
        public SHL(Expression left, Expression right) : base(left, right) { }

        public override Expression Copy(string renameSuffix)
        {
            return new SHL(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override string GetInfixOperator()
        {
            return " << ";
        }
    }

    /// <summary>
    /// Class representing an arithmetic shift right expression.
    /// </summary>
    public class SHR : BinInfixOperator
    {
        public SHR(Expression left, Expression right) : base(left, right) { }

        public override Expression Copy(string renameSuffix)
        {
            return new SHR(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override string GetInfixOperator()
        {
            return " >> ";
        }
    }

    /// <summary>
    /// Class representing a bit shift right expression, i.e. 0-extending, not sign-extending.
    /// </summary>
    public class BIT_SHR : BinInfixOperator
    {
        public BIT_SHR(Expression left, Expression right) : base(left, right) { }

        public override Expression Copy(string renameSuffix)
        {
            return new BIT_SHR(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("((int)(((uint)");
            Left.Emit(sourceCode);
            sourceCode.Append(")" + GetInfixOperator());
            Right.Emit(sourceCode);
            sourceCode.Append("))");
        }

        public override string GetInfixOperator()
        {
            return " >> ";
        }
    }

    /// <summary>
    /// Class representing an addition.
    /// </summary>
    public class ADD : BinInfixOperator
    {
        public ADD(Expression left, Expression right) : base(left, right) { }

        public override Expression Copy(string renameSuffix)
        {
            return new ADD(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override string GetInfixOperator()
        {
            return " + ";
        }
    }

    /// <summary>
    /// Class representing an array concatenation.
    /// </summary>
    public class LIST_ADD : BinFuncOperator
    {
        public LIST_ADD(Expression left, Expression right) : base(left, right) { }

        public override Expression Copy(string renameSuffix)
        {
            return new LIST_ADD(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override string GetFuncOperatorAndLParen()
        {
            return "GRGEN_LIBGR.ContainerHelper.Concatenate(";
        }
    }

    /// <summary>
    /// Class representing a Deque concatenation.
    /// </summary>
    public class DEQUE_ADD : BinFuncOperator
    {
        public DEQUE_ADD(Expression left, Expression right) : base(left, right) { }

        public override Expression Copy(string renameSuffix)
        {
            return new DEQUE_ADD(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override string GetFuncOperatorAndLParen()
        {
            return "GRGEN_LIBGR.ContainerHelper.Concatenate(";
        }
    }

    /// <summary>
    /// Class representing a subtraction.
    /// </summary>
    public class SUB : BinInfixOperator
    {
        public SUB(Expression left, Expression right) : base(left, right) { }

        public override Expression Copy(string renameSuffix)
        {
            return new SUB(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override string GetInfixOperator()
        {
            return " - ";
        }
    }

    /// <summary>
    /// Class representing a multiplication.
    /// </summary>
    public class MUL : BinInfixOperator
    {
        public MUL(Expression left, Expression right) : base(left, right) { }

        public override Expression Copy(string renameSuffix)
        {
            return new MUL(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override string GetInfixOperator()
        {
            return " * ";
        }
    }

    /// <summary>
    /// Class representing a division.
    /// </summary>
    public class DIV : BinInfixOperator
    {
        public DIV(Expression left, Expression right) : base(left, right) { }

        public override Expression Copy(string renameSuffix)
        {
            return new DIV(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override string GetInfixOperator()
        {
            return " / ";
        }
    }


    /// <summary>
    /// Class representing a modulo expression.
    /// </summary>
    public class MOD : BinInfixOperator
    {
        public MOD(Expression left, Expression right) : base(left, right) { }

        public override Expression Copy(string renameSuffix)
        {
            return new MOD(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override string GetInfixOperator()
        {
            return " % ";
        }
    }

    /// <summary>
    /// Class representing a logical negation.
    /// </summary>
    public class LOG_NOT : Operator
    {
        public LOG_NOT(Expression nested)
        {
            Nested = nested;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new LOG_NOT(Nested.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("!");
            Nested.Emit(sourceCode);
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Nested;
        }

        Expression Nested;
    }

    /// <summary>
    /// Class representing a bitwise negation.
    /// </summary>
    public class BIT_NOT : Operator
    {
        public BIT_NOT(Expression nested)
        {
            Nested = nested;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new BIT_NOT(Nested.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("~");
            Nested.Emit(sourceCode);
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Nested;
        }

        Expression Nested;
    }

    /// <summary>
    /// Class representing an arithmetic negation.
    /// </summary>
    public class NEG : Operator
    {
        public NEG(Expression nested)
        {
            Nested = nested;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new NEG(Nested.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("-");
            Nested.Emit(sourceCode);
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Nested;
        }

        Expression Nested;
    }

    /// <summary>
    /// Class representing a map/set inclusion query.
    /// </summary>
    public class IN : BinInfixOperator
    {
        // Switch operands as "right" is the dictionary
        public IN(Expression left, Expression right, bool isDictionary)
            : base(right, left)
        {
            IsDictionary = isDictionary;
        }

        // Switch operands as "right" is the dictionary
        public IN(Expression left, Expression right, String type, bool isDictionary)
            : base(right, left)
        {
            Type = type;
            IsDictionary = isDictionary;
        }

        public override Expression Copy(string renameSuffix)
        {
            if(Type != null) // the constructor is reversing left and right so it fits for emit, reverse twice to keep order of already constructed object
                return new IN(Right.Copy(renameSuffix), Left.Copy(renameSuffix), Type, IsDictionary);
            else
                return new IN(Right.Copy(renameSuffix), Left.Copy(renameSuffix), IsDictionary);
        }

        public override string GetInfixOperator()
        {
            if(IsDictionary)
            {
                if(Type != null)
                    return ").ContainsKey((" + Type + ")";
                else
                    return ").ContainsKey(";
            }
            else
            {
                if(Type != null)
                    return ").Contains((" + Type + ")";
                else
                    return ").Contains(";
            }
        }

        String Type;
        bool IsDictionary;
    }

    /// <summary>
    /// Class representing cast expression
    /// </summary>
    public class Cast : Expression
    {
        public Cast(String typeName, Expression nested, bool isContainer)
        {
            TypeName = typeName;
            Nested = nested;
            IsContainer = isContainer;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new Cast(TypeName, Nested.Copy(renameSuffix), IsContainer);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if (TypeName == "string")
            {
                if(IsContainer)
                {
                    sourceCode.Append("GRGEN_LIBGR.EmitHelper.ToString(");
                    Nested.Emit(sourceCode);
                    sourceCode.Append(", graph)");
                }
                else
                {
                    Nested.Emit(sourceCode);
                    sourceCode.Append(".ToString()");
                }
            }
            else
            {
                sourceCode.Append("(" + TypeName + ")");
                Nested.Emit(sourceCode);
            }
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Nested;
        }

        String TypeName;
        Expression Nested;
        bool IsContainer;
    }

    /// <summary>
    /// Class representing constant expression
    /// </summary>
    public class Constant : Expression
    {
        public Constant(string value)
        {
            Value = value;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new Constant(Value);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append(Value);
        }

        String Value;
    }

    /// <summary>
    /// Class representing enum constant expression
    /// </summary>
    public class ConstantEnumExpression : Expression
    {
        public ConstantEnumExpression(string enumType, string enumItem)
        {
            EnumType = enumType;
            EnumItem = enumItem;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ConstantEnumExpression(EnumType, EnumItem);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_MODEL.ENUM_" + EnumType + ".@" + EnumItem);
        }

        string EnumType;
        string EnumItem;
    }

    /// <summary>
    /// Class representing graph entity expression
    /// </summary>
    public class GraphEntityExpression : Expression
    {
        public GraphEntityExpression(String entity)
        {
            Entity = entity;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new GraphEntityExpression(Entity + renameSuffix);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append(NamesOfEntities.CandidateVariable(Entity));
        }

        public String Entity;
    }

    /// <summary>
    /// Class representing nameof expression
    /// </summary>
    public class Nameof : Expression
    {
        public Nameof(Expression entity)
        {
            Entity = entity;
        }

        public override Expression Copy(string renameSuffix)
        {
            if(Entity != null)
                return new Nameof(Entity.Copy(renameSuffix));
            else
                return new Nameof(null);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if (Entity != null)
            {
                sourceCode.Append("GRGEN_LIBGR.GraphHelper.Nameof(");
                Entity.Emit(sourceCode);
                sourceCode.Append(", graph)");
            }
            else
                sourceCode.Append("GRGEN_LIBGR.GraphHelper.Nameof(null, graph)");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            if(Entity != null)
                yield return Entity;
        }

        Expression Entity;
    }

    /// <summary>
    /// Class representing unique id expression
    /// </summary>
    public class Uniqueof : Expression
    {
        public Uniqueof(Expression entity, bool isNode, bool isGraph)
        {
            Entity = entity;
            IsNode = isNode;
            IsGraph = isGraph;
        }

        public override Expression Copy(string renameSuffix)
        {
            if(Entity != null)
                return new Uniqueof(Entity.Copy(renameSuffix), IsNode, IsGraph);
            else
                return new Uniqueof(null, IsNode, IsGraph);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(Entity == null)
            {
                sourceCode.Append("((GRGEN_LGSP.LGSPGraph)graph).GraphID");
            }
            else
            {
                sourceCode.Append("(");
                if(IsNode && !IsGraph)
                    sourceCode.Append("(GRGEN_LGSP.LGSPNode)");
                else if(!IsNode && !IsGraph)
                    sourceCode.Append("(GRGEN_LGSP.LGSPEdge)");
                else
                    sourceCode.Append("(GRGEN_LGSP.LGSPGraph)");
                Entity.Emit(sourceCode);
                if(IsGraph)
                    sourceCode.Append(").GraphID");
                else
                    sourceCode.Append(").uniqueId");
            }
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            if(Entity != null)
                yield return Entity;
        }

        Expression Entity;
        bool IsNode;
        bool IsGraph;
    }

    /// <summary>
    /// Class representing exists file expression
    /// </summary>
    public class ExistsFileExpression : Expression
    {
        public ExistsFileExpression(Expression path)
        {
            Path = path;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ExistsFileExpression(Path.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("System.IO.File.Exists(");
            Path.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Path;
        }

        Expression Path;
    }

    /// <summary>
    /// Class representing import expression
    /// </summary>
    public class ImportExpression : Expression
    {
        public ImportExpression(Expression path)
        {
            Path = path;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ImportExpression(Path.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.Import(");
            Path.Emit(sourceCode);
            sourceCode.Append(", graph)");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Path;
        }

        Expression Path;
    }

    /// <summary>
    /// Class representing copy expression
    /// </summary>
    public class CopyExpression : Expression
    {
        public CopyExpression(Expression graph)
        {
            Graph = graph;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new CopyExpression(Graph.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.Copy(");
            Graph.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Graph;
        }

        Expression Graph;
    }

    /// <summary>
    /// Class representing count of instances of an iterated pattern expression
    /// </summary>
    public class Count : Expression
    {
        public Count(String iterated)
        {
            Iterated = iterated;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new Count(Iterated + renameSuffix);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("match." + NamesOfEntities.MatchName(Iterated, BuildMatchObjectType.Iteration) + ".Count");
        }

        String Iterated;
    }

    /// <summary>
    /// Class representing qualification expression
    /// </summary>
    public class Qualification : Expression
    {
        public Qualification(String ownerType, String owner, String member)
        {
            OwnerType = ownerType;
            Owner = owner;
            Member = member;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new Qualification(OwnerType, Owner + renameSuffix, Member);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("((" + OwnerType + ")"+ NamesOfEntities.CandidateVariable(Owner) + ").@" + Member);
        }

        String OwnerType;
        String Owner;
        String Member;
    }

    /// <summary>
    /// Class representing global variable qualification expression
    /// </summary>
    public class GlobalVariableQualification : Expression
    {
        public GlobalVariableQualification(String ownerType, String owner, String member)
        {
            OwnerType = ownerType;
            Owner = owner;
            Member = member;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new GlobalVariableQualification(OwnerType, Owner, Member);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFormat("(({0})((GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv).GetVariableValue(\"{1}\")).@{2}", OwnerType, Owner, Member);
        }

        String OwnerType;
        String Owner;
        String Member;
    }

    /// <summary>
    /// Class representing typeof expression
    /// </summary>
    public class Typeof : Expression
    {
        public Typeof(String entity)
        {
            Entity = entity;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new Typeof(Entity + renameSuffix);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append(NamesOfEntities.CandidateVariable(Entity) + ".lgspType");
        }

        String Entity;
    }

    /// <summary>
    /// Class representing variable expression
    /// </summary>
    public class VariableExpression : Expression
    {
        public VariableExpression(String entity)
        {
            Entity = entity;
            MatchEntity = null;
        }

        public override Expression Copy(string renameSuffix)
        {
            VariableExpression tmp = new VariableExpression(Entity + renameSuffix);
            if(MatchEntity != null)
                tmp.MatchEntity = MatchEntity + renameSuffix;
            return tmp;
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(MatchEntity!=null)
                sourceCode.Append(MatchEntity);
            else
                sourceCode.Append(NamesOfEntities.Variable(Entity));
        }

        public String Entity;
        public String MatchEntity;
    }

    /// <summary>
    /// Class representing global variable expression
    /// </summary>
    public class GlobalVariableExpression : Expression
    {
        public GlobalVariableExpression(String name, String type)
        {
            GlobalVariableName = name;
            Type = type;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new GlobalVariableExpression(GlobalVariableName, Type);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFormat("(({0})((GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv).GetVariableValue(\"{1}\"))", Type, GlobalVariableName);
        }

        public String GlobalVariableName;
        public String Type;
    }

    /// <summary>
    /// Class representing visited expression
    /// </summary>
    public class Visited : Expression
    {
        public Visited(Expression entity, Expression nested) // nested = expression computing visited-id
        {
            Entity = entity;
            Nested = nested;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new Visited(Entity.Copy(renameSuffix), Nested.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("graph.IsVisited(");
            Entity.Emit(sourceCode);
            sourceCode.Append(", ");
            Nested.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Entity;
            yield return Nested;
        }

        Expression Entity;
        Expression Nested;
    }

    /// <summary>
    /// Class representing random expression
    /// </summary>
    public class Random : Expression
    {
        public Random(Expression nested) // nested = expression computing maximum value for int random
        {
            Nested = nested;
        }

        public Random() // double random
        {
            Nested = null;
        }

        public override Expression Copy(string renameSuffix)
        {
            if(Nested != null)
                return new Random(Nested.Copy(renameSuffix));
            else
                return new Random();
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(Nested != null)
            {
                sourceCode.Append("GRGEN_LIBGR.Sequence.randomGenerator.Next(");
                Nested.Emit(sourceCode);
            }
            else
            {
                sourceCode.Append("GRGEN_LIBGR.Sequence.randomGenerator.NextDouble(");
            }
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            if(Nested != null)
                yield return Nested;
            else
                yield break;
        }

        Expression Nested;
    }

    /// <summary>
    /// Class representing this expression
    /// </summary>
    public class This : Expression
    {
        public This()
        {
        }

        public override Expression Copy(string renameSuffix)
        {
            return new This();
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("graph");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield break;
        }
    }

    /// <summary>
    /// Class representing a string length expression.
    /// </summary>
    public class StringLength : Expression
    {
        public StringLength(Expression stringExpr)
        {
            StringExpr = stringExpr;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new StringLength(StringExpr.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("(");
            StringExpr.Emit(sourceCode);
            sourceCode.Append(").Length");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return StringExpr;
        }

        Expression StringExpr;
    }

    /// <summary>
    /// Class representing a substring expression.
    /// </summary>
    public class StringSubstring : Expression
    {
        public StringSubstring(Expression stringExpr, Expression startExpr, Expression lengthExpr)
        {
            StringExpr = stringExpr;
            StartExpr = startExpr;
            LengthExpr = lengthExpr;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new StringSubstring(StringExpr.Copy(renameSuffix), 
                StartExpr.Copy(renameSuffix), LengthExpr.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("(");
            StringExpr.Emit(sourceCode);
            sourceCode.Append(").Substring(");
            StartExpr.Emit(sourceCode);
            sourceCode.Append(", ");
            LengthExpr.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return StringExpr;
            yield return StartExpr;
            yield return LengthExpr;
        }

        Expression StringExpr;
        Expression StartExpr;
        Expression LengthExpr;
    }

    /// <summary>
    /// Class representing a string indexOf expression.
    /// </summary>
    public class StringIndexOf : Expression
    {
        public StringIndexOf(Expression stringExpr, Expression stringToSearchForExpr)
        {
            StringExpr = stringExpr;
            StringToSearchForExpr = stringToSearchForExpr;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new StringIndexOf(StringExpr.Copy(renameSuffix), StringToSearchForExpr.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("(");
            StringExpr.Emit(sourceCode);
            sourceCode.Append(").IndexOf(");
            StringToSearchForExpr.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return StringExpr;
            yield return StringToSearchForExpr;
        }

        Expression StringExpr;
        Expression StringToSearchForExpr;
    }

    /// <summary>
    /// Class representing a string lastIndexOf expression.
    /// </summary>
    public class StringLastIndexOf : Expression
    {
        public StringLastIndexOf(Expression stringExpr, Expression stringToSearchForExpr)
        {
            StringExpr = stringExpr;
            StringToSearchForExpr = stringToSearchForExpr;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new StringLastIndexOf(StringExpr.Copy(renameSuffix), StringToSearchForExpr.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("(");
            StringExpr.Emit(sourceCode);
            sourceCode.Append(").LastIndexOf(");
            StringToSearchForExpr.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return StringExpr;
            yield return StringToSearchForExpr;
        }

        Expression StringExpr;
        Expression StringToSearchForExpr;
    }

    /// <summary>
    /// Class representing a string replace expression.
    /// </summary>
    public class StringReplace : Expression
    {
        public StringReplace(Expression stringExpr, Expression startExpr, Expression lengthExpr, Expression replaceStrExpr)
        {
            StringExpr = stringExpr;
            StartExpr = startExpr;
            LengthExpr = lengthExpr;
            ReplaceStrExpr = replaceStrExpr;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new StringReplace(StringExpr.Copy(renameSuffix), StartExpr.Copy(renameSuffix),
                LengthExpr.Copy(renameSuffix), ReplaceStrExpr.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("((");
            StringExpr.Emit(sourceCode);
            sourceCode.Append(").Substring(0, ");
            StartExpr.Emit(sourceCode);
            sourceCode.Append(") + ");
            ReplaceStrExpr.Emit(sourceCode);
            sourceCode.Append(" + (");
            StringExpr.Emit(sourceCode);
            sourceCode.Append(").Substring(");
            StartExpr.Emit(sourceCode);
            sourceCode.Append(" + ");
            LengthExpr.Emit(sourceCode);
            sourceCode.Append("))");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return StringExpr;
            yield return StartExpr;
            yield return LengthExpr;
            yield return ReplaceStrExpr;
        }

        Expression StringExpr;
        Expression StartExpr;
        Expression LengthExpr;
        Expression ReplaceStrExpr;
    }

    /// <summary>
    /// Class representing a map access expression.
    /// </summary>
    public class MapAccess : Expression
    {
        public MapAccess(Expression target, Expression keyExpr)
        {
            Target = target;
            KeyExpr = keyExpr;
        }

        public MapAccess(Expression target, Expression keyExpr, String type)
        {
            Target = target;
            KeyExpr = keyExpr;
            Type = type;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new MapAccess(Target.Copy(renameSuffix), KeyExpr.Copy(renameSuffix), Type);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("(");
            Target.Emit(sourceCode);
            sourceCode.Append("[");
            if(Type != null)
                sourceCode.Append("(" + Type + ")");
            KeyExpr.Emit(sourceCode);
            sourceCode.Append("])");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
            yield return KeyExpr;
        }

        Expression Target;
        Expression KeyExpr;
        String Type;
    }

    /// <summary>
    /// Class representing an array access expression.
    /// </summary>
    public class ArrayAccess : Expression
    {
        public ArrayAccess(Expression target, Expression keyExpr)
        {
            Target = target;
            KeyExpr = keyExpr;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ArrayAccess(Target.Copy(renameSuffix), KeyExpr.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("(");
            Target.Emit(sourceCode);
            sourceCode.Append("[");
            KeyExpr.Emit(sourceCode);
            sourceCode.Append("])");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
            yield return KeyExpr;
        }

        Expression Target;
        Expression KeyExpr;
    }

    /// <summary>
    /// Class representing a deque access expression.
    /// </summary>
    public class DequeAccess : Expression
    {
        public DequeAccess(Expression target, Expression keyExpr)
        {
            Target = target;
            KeyExpr = keyExpr;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new DequeAccess(Target.Copy(renameSuffix), KeyExpr.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("(");
            Target.Emit(sourceCode);
            sourceCode.Append("[");
            KeyExpr.Emit(sourceCode);
            sourceCode.Append("])");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
            yield return KeyExpr;
        }

        Expression Target;
        Expression KeyExpr;
    }

    /// <summary>
    /// Class representing an incidence index access expression.
    /// </summary>
    public class IncidenceIndexAccess : Expression
    {
        public IncidenceIndexAccess(String target, Expression keyExpr, String type)
        {
            Target = target;
            KeyExpr = keyExpr;
            Type = type;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new IncidenceIndexAccess(Target, KeyExpr.Copy(renameSuffix), Type);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("((GRGEN_LIBGR.IIncidenceIndex)graph.Indices.GetIndex(\"" + Target + "\")).GetIncidenceCount(");
            sourceCode.Append("(" + Type + ")");
            KeyExpr.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return KeyExpr;
        }

        String Target;
        Expression KeyExpr;
        String Type;
    }

    /// <summary>
    /// Class representing a map size expression.
    /// </summary>
    public class MapSize : Expression
    {
        public MapSize(Expression target)
        {
            Target = target;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new MapSize(Target.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("(");
            Target.Emit(sourceCode);
            sourceCode.Append(").Count");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
        }

        Expression Target;
    }

    /// <summary>
    /// Class representing a map empty expression.
    /// </summary>
    public class MapEmpty : Expression
    {
        public MapEmpty(Expression target)
        {
            Target = target;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new MapEmpty(Target.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("((");
            Target.Emit(sourceCode);
            sourceCode.Append(").Count==0)");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
        }

        Expression Target;
    }

    /// <summary>
    /// Class representing a map peek expression.
    /// </summary>
    public class MapPeek : Expression
    {
        public MapPeek(Expression target, Expression number)
        {
            Target = target;
            Number = number;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new MapPeek(Target.Copy(renameSuffix), Number.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.ContainerHelper.Peek(");
            Target.Emit(sourceCode);
            sourceCode.Append(",");
            Number.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
            yield return Number;
        }

        Expression Target;
        Expression Number;
    }

    /// <summary>
    /// Class representing a map domain expression.
    /// </summary>
    public class MapDomain : Expression
    {
        public MapDomain(Expression target)
        {
            Target = target;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new MapDomain(Target.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.ContainerHelper.Domain(");
            Target.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
        }

        Expression Target;
    }

    /// <summary>
    /// Class representing a map range expression.
    /// </summary>
    public class MapRange : Expression
    {
        public MapRange(Expression target)
        {
            Target = target;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new MapRange(Target.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.ContainerHelper.Range(");
            Target.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
        }

        Expression Target;
    }

    /// <summary>
    /// Class representing a set size expression.
    /// </summary>
    public class SetSize : Expression
    {
        public SetSize(Expression target)
        {
            Target = target;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new SetSize(Target.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("(");
            Target.Emit(sourceCode);
            sourceCode.Append(").Count");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
        }

        Expression Target;
    }

    /// <summary>
    /// Class representing a set empty expression.
    /// </summary>
    public class SetEmpty: Expression
    {
        public SetEmpty(Expression target)
        {
            Target = target;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new SetEmpty(Target.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("((");
            Target.Emit(sourceCode);
            sourceCode.Append(").Count==0)");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
        }

        Expression Target;
    }

    /// <summary>
    /// Class representing a set peek expression.
    /// </summary>
    public class SetPeek : Expression
    {
        public SetPeek(Expression target, Expression number)
        {
            Target = target;
            Number = number;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new SetPeek(Target.Copy(renameSuffix), Number.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.ContainerHelper.Peek(");
            Target.Emit(sourceCode);
            sourceCode.Append(",");
            Number.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
            yield return Number;
        }

        Expression Target;
        Expression Number;
    }

    /// <summary>
    /// Class representing an array size expression.
    /// </summary>
    public class ArraySize : Expression
    {
        public ArraySize(Expression target)
        {
            Target = target;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ArraySize(Target.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("(");
            Target.Emit(sourceCode);
            sourceCode.Append(").Count");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
        }

        Expression Target;
    }

    /// <summary>
    /// Class representing an array empty expression.
    /// </summary>
    public class ArrayEmpty : Expression
    {
        public ArrayEmpty(Expression target)
        {
            Target = target;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ArrayEmpty(Target.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("((");
            Target.Emit(sourceCode);
            sourceCode.Append(").Count==0)");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
        }

        Expression Target;
    }

    /// <summary>
    /// Class representing an array peek expression.
    /// </summary>
    public class ArrayPeek : Expression
    {
        public ArrayPeek(Expression target, Expression number)
        {
            Target = target;
            Number = number;
        }

        public ArrayPeek(Expression target)
        {
            Target = target;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ArrayPeek(Target.Copy(renameSuffix), Number!=null ? Number.Copy(renameSuffix) : null);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            Target.Emit(sourceCode);
            sourceCode.Append("[");
            if(Number != null)
                Number.Emit(sourceCode);
            else
            {
                sourceCode.Append("(");
                Target.Emit(sourceCode);
                sourceCode.Append(").Count-1");
            }
            sourceCode.Append("]");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
            if(Number!=null) yield return Number;
            else yield break;
        }

        Expression Target;
        Expression Number;
    }

    /// <summary>
    /// Class representing an array index of expression.
    /// </summary>
    public class ArrayIndexOf : Expression
    {
        public ArrayIndexOf(Expression target, Expression value)
        {
            Target = target;
            Value = value;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ArrayIndexOf(Target.Copy(renameSuffix), Value.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.ContainerHelper.IndexOf(");
            Target.Emit(sourceCode);
            sourceCode.Append(", ");
            Value.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
            yield return Value;
        }

        Expression Target;
        Expression Value;
    }

    /// <summary>
    /// Class representing an array last index of expression.
    /// </summary>
    public class ArrayLastIndexOf : Expression
    {
        public ArrayLastIndexOf(Expression target, Expression value)
        {
            Target = target;
            Value = value;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ArrayLastIndexOf(Target.Copy(renameSuffix), Value.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.ContainerHelper.LastIndexOf(");
            Target.Emit(sourceCode);
            sourceCode.Append(", ");
            Value.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
            yield return Value;
        }

        Expression Target;
        Expression Value;
    }

    /// <summary>
    /// Class representing an array subarray expression.
    /// </summary>
    public class ArraySubarray : Expression
    {
        public ArraySubarray(Expression target, Expression start, Expression length)
        {
            Target = target;
            Start = start;
            Length = length;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ArraySubarray(Target.Copy(renameSuffix), Start.Copy(renameSuffix), Length.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.ContainerHelper.Subarray(");
            Target.Emit(sourceCode);
            sourceCode.Append(", ");
            Start.Emit(sourceCode);
            sourceCode.Append(", ");
            Length.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
            yield return Start;
            yield return Length;
        }

        Expression Target;
        Expression Start;
        Expression Length;
    }

    /// <summary>
    /// Class representing a deque size expression.
    /// </summary>
    public class DequeSize : Expression
    {
        public DequeSize(Expression target)
        {
            Target = target;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new DequeSize(Target.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("(");
            Target.Emit(sourceCode);
            sourceCode.Append(").Count");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
        }

        Expression Target;
    }

    /// <summary>
    /// Class representing a deque empty expression.
    /// </summary>
    public class DequeEmpty : Expression
    {
        public DequeEmpty(Expression target)
        {
            Target = target;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new DequeEmpty(Target.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("((");
            Target.Emit(sourceCode);
            sourceCode.Append(").Count==0)");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
        }

        Expression Target;
    }

    /// <summary>
    /// Class representing a deque peek expression.
    /// </summary>
    public class DequePeek : Expression
    {
        public DequePeek(Expression target, Expression number)
        {
            Target = target;
            Number = number;
        }

        public DequePeek(Expression target)
        {
            Target = target;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new DequePeek(Target.Copy(renameSuffix), Number!=null ? Number.Copy(renameSuffix) : null);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            Target.Emit(sourceCode);
            sourceCode.Append("[");
            if(Number != null)
                Number.Emit(sourceCode);
            else
                sourceCode.Append("0");
            sourceCode.Append("]");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
            if(Number!=null) yield return Number;
            else yield break;
        }

        Expression Target;
        Expression Number;
    }

    /// <summary>
    /// Class representing a deque index of expression.
    /// </summary>
    public class DequeIndexOf : Expression
    {
        public DequeIndexOf(Expression target, Expression value)
        {
            Target = target;
            Value = value;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new DequeIndexOf(Target.Copy(renameSuffix), Value.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.ContainerHelper.IndexOf(");
            Target.Emit(sourceCode);
            sourceCode.Append(", ");
            Value.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
            yield return Value;
        }

        Expression Target;
        Expression Value;
    }

    /// <summary>
    /// Class representing a deque last index of expression.
    /// </summary>
    public class DequeLastIndexOf : Expression
    {
        public DequeLastIndexOf(Expression target, Expression value)
        {
            Target = target;
            Value = value;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new DequeLastIndexOf(Target.Copy(renameSuffix), Value.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.ContainerHelper.LastIndexOf(");
            Target.Emit(sourceCode);
            sourceCode.Append(", ");
            Value.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
            yield return Value;
        }

        Expression Target;
        Expression Value;
    }

    /// <summary>
    /// Class representing a deque subdeque expression.
    /// </summary>
    public class DequeSubdeque : Expression
    {
        public DequeSubdeque(Expression target, Expression start, Expression length)
        {
            Target = target;
            Start = start;
            Length = length;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new DequeSubdeque(Target.Copy(renameSuffix), Start.Copy(renameSuffix), Length.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.ContainerHelper.Subdeque(");
            Target.Emit(sourceCode);
            sourceCode.Append(", ");
            Start.Emit(sourceCode);
            sourceCode.Append(", ");
            Length.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
            yield return Start;
            yield return Length;
        }

        Expression Target;
        Expression Start;
        Expression Length;
    }

    /// <summary>
    /// Class representing a constant rule-local map, available as initalized static class member.
    /// </summary>
    public class StaticMap : Expression
    {
        public StaticMap(String className, String mapName)
        {
            ClassName = className;
            MapName = mapName;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new StaticMap(ClassName, MapName);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append(ClassName + "." + MapName);
        }

        String ClassName;
        String MapName;
    }

    /// <summary>
    /// Class representing a constant rule-local set, available as initialized static class member.
    /// </summary>
    public class StaticSet : Expression
    {
        public StaticSet(String className, String setName)
        {
            ClassName = className;
            SetName = setName;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new StaticSet(ClassName, SetName);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append(ClassName + "." + SetName);
        }

        String ClassName;
        String SetName;
    }

    /// <summary>
    /// Class representing a constant rule-local array, available as initialized static class member.
    /// </summary>
    public class StaticArray : Expression
    {
        public StaticArray(String className, String arrayName)
        {
            ClassName = className;
            ArrayName = arrayName;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new StaticArray(ClassName, ArrayName);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append(ClassName + "." + ArrayName);
        }

        String ClassName;
        String ArrayName;
    }

    /// <summary>
    /// Class representing a constant rule-local deque, available as initialized static class member.
    /// </summary>
    public class StaticDeque : Expression
    {
        public StaticDeque(String className, String dequeName)
        {
            ClassName = className;
            DequeName = dequeName;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new StaticDeque(ClassName, DequeName);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append(ClassName + "." + DequeName);
        }

        String ClassName;
        String DequeName;
    }

    /// <summary>
    /// Class representing a rule-local map to be filled with the given map items.
    /// </summary>
    public class MapConstructor : Expression
    {
        public MapConstructor(String className, String mapName, MapItem first)
        {
            ClassName = className;
            MapName = mapName;
            First = first;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new MapConstructor(ClassName, MapName, First!=null ? (MapItem)First.Copy(renameSuffix) : null);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append(ClassName + ".fill_" + MapName + "(");
            if(First!=null) First.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            if(First!=null) yield return First;
        }

        String ClassName;
        String MapName;
        MapItem First;
    }

    /// <summary>
    /// Class representing a map item.
    /// </summary>
    public class MapItem : Expression
    {
        public MapItem(Expression key, String keyType,
            Expression value, String valueType, MapItem next)
        {
            Key = key;
            KeyType = keyType;
            Value = value;
            ValueType = valueType;
            Next = next;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new MapItem(Key.Copy(renameSuffix), KeyType, 
                Value.Copy(renameSuffix), ValueType,
                Next!=null ? (MapItem)Next.Copy(renameSuffix) : null);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(KeyType != null)
                sourceCode.Append("(" + KeyType + ")(");
            Key.Emit(sourceCode);
            if(KeyType != null)
                sourceCode.Append(")");

            sourceCode.Append(", ");

            if(ValueType != null)
                sourceCode.Append("(" + ValueType + ")(");
            Value.Emit(sourceCode);
            if(ValueType != null)
                sourceCode.Append(")");

            if(Next != null)
            {
                sourceCode.Append(", ");
                Next.Emit(sourceCode);
            }
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Key;
            yield return Value;
            if(Next!=null)
                yield return Next;
        }

        Expression Key;
        String KeyType;
        Expression Value;
        String ValueType;
        MapItem Next;
    }

    /// <summary>
    /// Class representing a rule-local set to be filled with the given set items.
    /// </summary>
    public class SetConstructor : Expression
    {
        public SetConstructor(String className, String setName, SetItem first)
        {
            ClassName = className;
            SetName = setName;
            First = first;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new SetConstructor(ClassName, SetName, First!=null ? (SetItem)First.Copy(renameSuffix) : null);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append(ClassName + ".fill_" + SetName + "(");
            if(First!=null) First.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            if(First!=null) yield return First;
        }

        String ClassName;
        String SetName;
        SetItem First;
    }

    /// <summary>
    /// Class representing a set item.
    /// </summary>
    public class SetItem : Expression
    {
        public SetItem(Expression value, String valueType, SetItem next)
        {
            Value = value;
            ValueType = valueType;
            Next = next;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new SetItem(Value.Copy(renameSuffix), ValueType, Next!=null ? (SetItem)Next.Copy(renameSuffix) : null);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(ValueType != null)
                sourceCode.Append("(" + ValueType + ")(");
            Value.Emit(sourceCode);
            if(ValueType!=null)
                sourceCode.Append(")");

            if (Next != null)
            {
                sourceCode.Append(", ");
                Next.Emit(sourceCode);
            }
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Value;
            if(Next!=null) yield return Next;
        }

        Expression Value;
        String ValueType;
        SetItem Next;
    }

    /// <summary>
    /// Class representing a rule-local array to be filled with the given array items.
    /// </summary>
    public class ArrayConstructor : Expression
    {
        public ArrayConstructor(String className, String arrayName, ArrayItem first)
        {
            ClassName = className;
            ArrayName = arrayName;
            First = first;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ArrayConstructor(ClassName, ArrayName, First!=null ? (ArrayItem)First.Copy(renameSuffix) : null);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append(ClassName + ".fill_" + ArrayName + "(");
            if(First!=null) First.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            if(First!=null) yield return First;
        }

        String ClassName;
        String ArrayName;
        ArrayItem First;
    }

    /// <summary>
    /// Class representing an array item.
    /// </summary>
    public class ArrayItem : Expression
    {
        public ArrayItem(Expression value, String valueType, ArrayItem next)
        {
            Value = value;
            ValueType = valueType;
            Next = next;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ArrayItem(Value.Copy(renameSuffix), ValueType, Next!=null ? (ArrayItem)Next.Copy(renameSuffix) : null);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(ValueType != null)
                sourceCode.Append("(" + ValueType + ")(");
            Value.Emit(sourceCode);
            if(ValueType != null)
                sourceCode.Append(")");

            if(Next != null)
            {
                sourceCode.Append(", ");
                Next.Emit(sourceCode);
            }
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Value;
            if(Next!=null) yield return Next;
        }

        Expression Value;
        String ValueType;
        ArrayItem Next;
    }

    /// <summary>
    /// Class representing a rule-local deque to be filled with the given deque items.
    /// </summary>
    public class DequeConstructor : Expression
    {
        public DequeConstructor(String className, String dequeName, DequeItem first)
        {
            ClassName = className;
            DequeName = dequeName;
            First = first;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new DequeConstructor(ClassName, DequeName, First!=null ? (DequeItem)First.Copy(renameSuffix) : null);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append(ClassName + ".fill_" + DequeName + "(");
            if(First!=null) First.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            if(First!=null) yield return First;
        }

        String ClassName;
        String DequeName;
        DequeItem First;
    }

    /// <summary>
    /// Class representing a deque item.
    /// </summary>
    public class DequeItem : Expression
    {
        public DequeItem(Expression value, String valueType, DequeItem next)
        {
            Value = value;
            ValueType = valueType;
            Next = next;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new DequeItem(Value.Copy(renameSuffix), ValueType, Next != null ? (DequeItem)Next.Copy(renameSuffix) : null);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(ValueType != null)
                sourceCode.Append("(" + ValueType + ")(");
            Value.Emit(sourceCode);
            if(ValueType != null)
                sourceCode.Append(")");

            if(Next != null)
            {
                sourceCode.Append(", ");
                Next.Emit(sourceCode);
            }
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Value;
            if(Next!=null) yield return Next;
        }

        Expression Value;
        String ValueType;
        DequeItem Next;
    }

    /// <summary>
    /// Class representing a function invocation (of an internal attribute evaluation function).
    /// </summary>
    public class FunctionInvocation : Expression
    {
        public FunctionInvocation(String packageName, String functionName, Expression[] arguments, String[] argumentTypes)
        {
            PackageName = packageName;
            FunctionName = functionName;
            Arguments = arguments;
            ArgumentTypes = argumentTypes;
        }

        public override Expression Copy(string renameSuffix)
        {
            Expression[] newArguments = new Expression[Arguments.Length];
            for(int i = 0; i < Arguments.Length; ++i) newArguments[i] = (Expression)Arguments[i].Copy(renameSuffix);
            return new FunctionInvocation(PackageName, FunctionName, newArguments, (String[])ArgumentTypes.Clone());
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append(PackageName + "Functions." + FunctionName + "(actionEnv, graph");
            for(int i = 0; i < Arguments.Length; ++i)
            {
                sourceCode.Append(", ");
                Expression argument = Arguments[i];
                if(ArgumentTypes[i] != null) sourceCode.Append("(" + ArgumentTypes[i] + ")");
                argument.Emit(sourceCode);
            }
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            foreach(Expression argument in Arguments)
                yield return argument;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public String PackageName;
        public String FunctionName;
        public Expression[] Arguments;
        public String[] ArgumentTypes; // for each argument: if node/edge: the interface type, otherwise: null
        public bool Parallel;
    }

    /// <summary>
    /// Class representing a function invocation (of an external attribute evaluation function).
    /// </summary>
    public class ExternalFunctionInvocation : Expression
    {
        public ExternalFunctionInvocation(String functionName, Expression[] arguments, String[] argumentTypes)
        {
            FunctionName = functionName;
            Arguments = arguments;
            ArgumentTypes = argumentTypes;
        }

        public override Expression Copy(string renameSuffix)
        {
            Expression[] newArguments = new Expression[Arguments.Length];
            for(int i = 0; i < Arguments.Length; ++i) newArguments[i] = (Expression)Arguments[i].Copy(renameSuffix);
            return new ExternalFunctionInvocation(FunctionName, newArguments, (String[])ArgumentTypes.Clone());
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_EXPR.ExternalFunctions." + FunctionName + "(actionEnv, graph");
            for(int i=0; i<Arguments.Length; ++i)
            {
                sourceCode.Append(", ");
                Expression argument = Arguments[i];
                if(ArgumentTypes[i]!=null) 
                    sourceCode.Append("("+ArgumentTypes[i]+")");
                argument.Emit(sourceCode);
            }
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            foreach(Expression argument in Arguments)
                yield return argument;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public String FunctionName;
        public Expression[] Arguments;
        public String[] ArgumentTypes; // for each argument: if node/edge: the interface type, otherwise: null
        public bool Parallel;
    }

    /// <summary>
    /// Class representing a function method invocation (of an internal attribute evaluation function method).
    /// </summary>
    public class FunctionMethodInvocation : FunctionInvocation
    {
        public FunctionMethodInvocation(String ownerType, String owner, String functionName, Expression[] arguments, String[] argumentTypes)
            : base("", functionName, arguments, argumentTypes)
        {
            OwnerType = ownerType;
            Owner = owner; 
        }

        public override Expression Copy(string renameSuffix)
        {
            Expression[] newArguments = new Expression[Arguments.Length];
            for(int i = 0; i < Arguments.Length; ++i) newArguments[i] = (Expression)Arguments[i].Copy(renameSuffix);
            return new FunctionMethodInvocation(OwnerType, Owner + renameSuffix, FunctionName, newArguments, (String[])ArgumentTypes.Clone());
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("((" + OwnerType + ")" + NamesOfEntities.CandidateVariable(Owner) + ").@");
            sourceCode.Append(FunctionName + "(actionEnv, graph");
            for(int i = 0; i < Arguments.Length; ++i)
            {
                sourceCode.Append(", ");
                Expression argument = Arguments[i];
                if(ArgumentTypes[i] != null) sourceCode.Append("(" + ArgumentTypes[i] + ")");
                argument.Emit(sourceCode);
            }
            sourceCode.Append(")");
        }

        String OwnerType;
        String Owner; 
    }

    /// <summary>
    /// Class representing a function method invocation (of an external attribute evaluation function method).
    /// </summary>
    public class ExternalFunctionMethodInvocation : ExternalFunctionInvocation
    {
        public ExternalFunctionMethodInvocation(String ownerType, String owner, String functionName, Expression[] arguments, String[] argumentTypes)
            : base(functionName, arguments, argumentTypes)
        {
            OwnerType = ownerType;
            Owner = owner;
        }

        public override Expression Copy(string renameSuffix)
        {
            Expression[] newArguments = new Expression[Arguments.Length];
            for(int i = 0; i < Arguments.Length; ++i) newArguments[i] = (Expression)Arguments[i].Copy(renameSuffix);
            return new ExternalFunctionMethodInvocation(OwnerType, Owner + renameSuffix, FunctionName, newArguments, (String[])ArgumentTypes.Clone());
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("((" + OwnerType + ")" + NamesOfEntities.CandidateVariable(Owner) + ").@");
            sourceCode.Append(FunctionName + "(actionEnv, graph");
            for(int i = 0; i < Arguments.Length; ++i)
            {
                sourceCode.Append(", ");
                Expression argument = Arguments[i];
                if(ArgumentTypes[i] != null)
                    sourceCode.Append("(" + ArgumentTypes[i] + ")");
                argument.Emit(sourceCode);
            }
            sourceCode.Append(")");
        }

        String OwnerType;
        String Owner; 
    }

    /// <summary>
    /// Class representing expression returning the nodes of a node type (as set)
    /// </summary>
    public class Nodes : Expression
    {
        public Nodes(Expression nodeType)
        {
            NodeType = nodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new Nodes(NodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.Nodes(graph, ");
            NodeType.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return NodeType;
        }

        public Expression NodeType;
    }

    /// <summary>
    /// Class representing expression returning the edges of an edge type (as set)
    /// </summary>
    public class Edges : Expression
    {
        public Edges(Expression edgeType)
        {
            EdgeType = edgeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new Edges(EdgeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.Edges(graph, ");
            EdgeType.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return EdgeType;
        }

        public Expression EdgeType;
    }

    /// <summary>
    /// Class representing expression returning whether the graph is empty
    /// </summary>
    public class Empty : Expression
    {
        public Empty()
        {
        }

        public override Expression Copy(string renameSuffix)
        {
            return new Empty();
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("(graph.NumNodes+graph.NumEdges==0)");
        }
    }

    /// <summary>
    /// Class representing expression returning the number of graph elements
    /// </summary>
    public class Size : Expression
    {
        public Size()
        {
        }

        public override Expression Copy(string renameSuffix)
        {
            return new Size();
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("(graph.NumNodes+graph.NumEdges)");
        }
    }

    /// <summary>
    /// Class representing expression returning the source node of an edge
    /// </summary>
    public class Source : Expression
    {
        public Source(Expression edge)
        {
            Edge = edge;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new Source(Edge.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("((");
            Edge.Emit(sourceCode);
            sourceCode.Append(").Source)");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Edge;
        }

        Expression Edge;
    }

    /// <summary>
    /// Class representing expression returning the target node of an edge
    /// </summary>
    public class Target : Expression
    {
        public Target(Expression edge)
        {
            Edge = edge;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new Target(Edge.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("((");
            Edge.Emit(sourceCode);
            sourceCode.Append(").Target)");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Edge;
        }

        Expression Edge;
    }

    /// <summary>
    /// Class representing expression returning the opposite node of an edge and a node
    /// </summary>
    public class Opposite : Expression
    {
        public Opposite(Expression edge, Expression node)
        {
            Edge = edge;
            Node = node;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new Opposite(Edge.Copy(renameSuffix), Node.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("((");
            Edge.Emit(sourceCode);
            sourceCode.Append(").Opposite(");
            Node.Emit(sourceCode);
            sourceCode.Append("))");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Edge;
            yield return Node;
        }

        Expression Edge;
        Expression Node;
    }

    /// <summary>
    /// Class representing expression returning the node for a name (or null)
    /// </summary>
    public class NodeByName : Expression
    {
        public NodeByName(Expression name)
        {
            Name = name;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new NodeByName(Name.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("((GRGEN_LIBGR.INamedGraph)graph).GetNode(");
            Name.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Name;
        }

        Expression Name;
    }

    /// <summary>
    /// Class representing expression returning the edge for a name (or null)
    /// </summary>
    public class EdgeByName : Expression
    {
        public EdgeByName(Expression name)
        {
            Name = name;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new EdgeByName(Name.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("((GRGEN_LIBGR.INamedGraph)graph).GetEdge(");
            Name.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Name;
        }

        Expression Name;
    }

    /// <summary>
    /// Class representing expression returning the node for a unique id(or null)
    /// </summary>
    public class NodeByUnique : Expression
    {
        public NodeByUnique(Expression unique)
        {
            Unique = unique;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new NodeByUnique(Unique.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("graph.GetNode(");
            Unique.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Unique;
        }

        Expression Unique;
    }

    /// <summary>
    /// Class representing expression returning the edge for a unique id(or null)
    /// </summary>
    public class EdgeByUnique : Expression
    {
        public EdgeByUnique(Expression unique)
        {
            Unique = unique;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new EdgeByUnique(Unique.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("graph.GetEdge(");
            Unique.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Unique;
        }

        Expression Unique;
    }

    /// <summary>
    /// Class representing expression returning the outgoing edges of a node (as set)
    /// </summary>
    public class Outgoing : Expression
    {
        public Outgoing(Expression node, Expression incidentEdgeType, Expression adjacentNodeType)
        {
            Node = node;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new Outgoing(Node.Copy(renameSuffix), IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.Outgoing((GRGEN_LIBGR.INode)");
            Node.Emit(sourceCode);
            sourceCode.Append(", ");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", ");
            AdjacentNodeType.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Node;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public Expression Node;
        public Expression IncidentEdgeType;
        public Expression AdjacentNodeType;
    }

    /// <summary>
    /// Class representing expression returning the incoming edges of a node (as set)
    /// </summary>
    public class Incoming : Expression
    {
        public Incoming(Expression node, Expression incidentEdgeType, Expression adjacentNodeType)
        {
            Node = node;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new Incoming(Node.Copy(renameSuffix), IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.Incoming((GRGEN_LIBGR.INode)");
            Node.Emit(sourceCode);
            sourceCode.Append(", ");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", ");
            AdjacentNodeType.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Node;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public Expression Node;
        public Expression IncidentEdgeType;
        public Expression AdjacentNodeType;
    }

    /// <summary>
    /// Class representing expression returning the incident edges of a node (as set)
    /// </summary>
    public class Incident : Expression
    {
        public Incident(Expression node, Expression incidentEdgeType, Expression adjacentNodeType)
        {
            Node = node;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new Incident(Node.Copy(renameSuffix), IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.Incident((GRGEN_LIBGR.INode)");
            Node.Emit(sourceCode);
            sourceCode.Append(", ");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", ");
            AdjacentNodeType.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Node;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public Expression Node;
        public Expression IncidentEdgeType;
        public Expression AdjacentNodeType;
    }

    /// <summary>
    /// Class representing expression returning the adjacent nodes of a node (as set) reachable via outgoing edges
    /// </summary>
    public class AdjacentOutgoing : Expression
    {
        public AdjacentOutgoing(Expression node, Expression incidentEdgeType, Expression adjacentNodeType)
        {
            Node = node;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new AdjacentOutgoing(Node.Copy(renameSuffix), IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.AdjacentOutgoing((GRGEN_LIBGR.INode)");
            Node.Emit(sourceCode);
            sourceCode.Append(", ");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", ");
            AdjacentNodeType.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Node;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public Expression Node;
        public Expression IncidentEdgeType;
        public Expression AdjacentNodeType;
    }

    /// <summary>
    /// Class representing expression returning the adjacent nodes of a node (as set) reachable via incoming edges
    /// </summary>
    public class AdjacentIncoming : Expression
    {
        public AdjacentIncoming(Expression node, Expression incidentEdgeType, Expression adjacentNodeType)
        {
            Node = node;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new AdjacentIncoming(Node.Copy(renameSuffix), IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.AdjacentIncoming((GRGEN_LIBGR.INode)");
            Node.Emit(sourceCode);
            sourceCode.Append(", ");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", ");
            AdjacentNodeType.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Node;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public Expression Node;
        public Expression IncidentEdgeType;
        public Expression AdjacentNodeType;
    }

    /// <summary>
    /// Class representing expression returning the adjacent nodes of a node (as set) reachable via incident edges
    /// </summary>
    public class Adjacent : Expression
    {
        public Adjacent(Expression node, Expression incidentEdgeType, Expression adjacentNodeType)
        {
            Node = node;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new Adjacent(Node.Copy(renameSuffix), IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.Adjacent((GRGEN_LIBGR.INode)");
            Node.Emit(sourceCode);
            sourceCode.Append(", ");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", ");
            AdjacentNodeType.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Node;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public Expression Node;
        public Expression IncidentEdgeType;
        public Expression AdjacentNodeType;
    }

    /// <summary>
    /// Class representing expression returning whether the end node is adjacent to the start node with an outgoing edge
    /// </summary>
    public class IsAdjacentOutgoing : Expression
    {
        public IsAdjacentOutgoing(Expression startNode, Expression endNode,
            Expression incidentEdgeType, Expression adjacentNodeType)
        {
            StartNode = startNode;
            EndNode = endNode;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new IsAdjacentOutgoing(StartNode.Copy(renameSuffix), EndNode.Copy(renameSuffix),
                IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.IsAdjacentOutgoing(");
            sourceCode.Append("(GRGEN_LIBGR.INode)");
            StartNode.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.INode)");
            EndNode.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.EdgeType)");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.NodeType)");
            AdjacentNodeType.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return StartNode;
            yield return EndNode;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        Expression StartNode;
        Expression EndNode;
        Expression IncidentEdgeType;
        Expression AdjacentNodeType;
    }

    /// <summary>
    /// Class representing expression returning whether the end node is adjacent to the start node with an incoming edge
    /// </summary>
    public class IsAdjacentIncoming : Expression
    {
        public IsAdjacentIncoming(Expression startNode, Expression endNode,
            Expression incidentEdgeType, Expression adjacentNodeType)
        {
            StartNode = startNode;
            EndNode = endNode;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new IsAdjacentIncoming(StartNode.Copy(renameSuffix), EndNode.Copy(renameSuffix),
                IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.IsAdjacentIncoming(");
            sourceCode.Append("(GRGEN_LIBGR.INode)");
            StartNode.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.INode)");
            EndNode.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.EdgeType)");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.NodeType)");
            AdjacentNodeType.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return StartNode;
            yield return EndNode;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        Expression StartNode;
        Expression EndNode;
        Expression IncidentEdgeType;
        Expression AdjacentNodeType;
    }

    /// <summary>
    /// Class representing expression returning whether the end node is adjacent to the start node with an incident edge
    /// </summary>
    public class IsAdjacent : Expression
    {
        public IsAdjacent(Expression startNode, Expression endNode,
            Expression incidentEdgeType, Expression adjacentNodeType)
        {
            StartNode = startNode;
            EndNode = endNode;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new IsAdjacent(StartNode.Copy(renameSuffix), EndNode.Copy(renameSuffix),
                IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.IsAdjacent(");
            sourceCode.Append("(GRGEN_LIBGR.INode)");
            StartNode.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.INode)");
            EndNode.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.EdgeType)");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.NodeType)");
            AdjacentNodeType.Emit(sourceCode);
            sourceCode.Append(")");
        }
        
        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return StartNode;
            yield return EndNode;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        Expression StartNode;
        Expression EndNode;
        Expression IncidentEdgeType;
        Expression AdjacentNodeType;
    }

    /// <summary>
    /// Class representing expression returning whether the end edge is incident to the start node with an outgoing edge
    /// </summary>
    public class IsOutgoing : Expression
    {
        public IsOutgoing(Expression startNode, Expression endEdge,
            Expression incidentEdgeType, Expression adjacentNodeType)
        {
            StartNode = startNode;
            EndEdge = endEdge;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new IsOutgoing(StartNode.Copy(renameSuffix), EndEdge.Copy(renameSuffix),
                IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.IsOutgoing(");
            sourceCode.Append("(GRGEN_LIBGR.INode)");
            StartNode.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.IEdge)");
            EndEdge.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.EdgeType)");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.NodeType)");
            AdjacentNodeType.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return StartNode;
            yield return EndEdge;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        Expression StartNode;
        Expression EndEdge;
        Expression IncidentEdgeType;
        Expression AdjacentNodeType;
    }

    /// <summary>
    /// Class representing expression returning whether the end edge is incident to the start node with an incoming edge
    /// </summary>
    public class IsIncoming : Expression
    {
        public IsIncoming(Expression startNode, Expression endEdge,
            Expression incidentEdgeType, Expression adjacentNodeType)
        {
            StartNode = startNode;
            EndEdge = endEdge;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new IsIncoming(StartNode.Copy(renameSuffix), EndEdge.Copy(renameSuffix),
                IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.IsIncoming(");
            sourceCode.Append("(GRGEN_LIBGR.INode)");
            StartNode.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.IEdge)");
            EndEdge.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.EdgeType)");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.NodeType)");
            AdjacentNodeType.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return StartNode;
            yield return EndEdge;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        Expression StartNode;
        Expression EndEdge;
        Expression IncidentEdgeType;
        Expression AdjacentNodeType;
    }

    /// <summary>
    /// Class representing expression returning whether the end edge is incident to the start node with an incident edge
    /// </summary>
    public class IsIncident : Expression
    {
        public IsIncident(Expression startNode, Expression endEdge,
            Expression incidentEdgeType, Expression adjacentNodeType)
        {
            StartNode = startNode;
            EndEdge = endEdge;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new IsIncident(StartNode.Copy(renameSuffix), EndEdge.Copy(renameSuffix),
                IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.IsIncident(");
            sourceCode.Append("(GRGEN_LIBGR.INode)");
            StartNode.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.IEdge)");
            EndEdge.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.EdgeType)");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.NodeType)");
            AdjacentNodeType.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return StartNode;
            yield return EndEdge;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        Expression StartNode;
        Expression EndEdge;
        Expression IncidentEdgeType;
        Expression AdjacentNodeType;
    }

    /// <summary>
    /// Class representing expression returning the reachable edges via outgoing edges of a node (as set)
    /// </summary>
    public class ReachableEdgesOutgoing : Expression
    {
        public ReachableEdgesOutgoing(Expression node, Expression incidentEdgeType, Expression adjacentNodeType)
        {
            Node = node;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ReachableEdgesOutgoing(Node.Copy(renameSuffix), IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.ReachableEdgesOutgoing(graph, (GRGEN_LIBGR.INode)");
            Node.Emit(sourceCode);
            sourceCode.Append(", ");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", ");
            AdjacentNodeType.Emit(sourceCode);
            if(Parallel) sourceCode.Append(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Node;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public Expression Node;
        public Expression IncidentEdgeType;
        public Expression AdjacentNodeType;
        public bool Parallel;
    }

    /// <summary>
    /// Class representing expression returning the reachable edges via incoming edges of a node (as set)
    /// </summary>
    public class ReachableEdgesIncoming : Expression
    {
        public ReachableEdgesIncoming(Expression node, Expression incidentEdgeType, Expression adjacentNodeType)
        {
            Node = node;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ReachableEdgesIncoming(Node.Copy(renameSuffix), IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.ReachableEdgesIncoming(graph, (GRGEN_LIBGR.INode)");
            Node.Emit(sourceCode);
            sourceCode.Append(", ");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", ");
            AdjacentNodeType.Emit(sourceCode);
            if(Parallel) sourceCode.Append(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Node;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public Expression Node;
        public Expression IncidentEdgeType;
        public Expression AdjacentNodeType;
        public bool Parallel;
    }

    /// <summary>
    /// Class representing expression returning the reachable edges via incident edges of a node (as set)
    /// </summary>
    public class ReachableEdges : Expression
    {
        public ReachableEdges(Expression node, Expression incidentEdgeType, Expression adjacentNodeType)
        {
            Node = node;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ReachableEdges(Node.Copy(renameSuffix), IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.ReachableEdges(graph, (GRGEN_LIBGR.INode)");
            Node.Emit(sourceCode);
            sourceCode.Append(", ");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", ");
            AdjacentNodeType.Emit(sourceCode);
            if(Parallel) sourceCode.Append(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Node;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public Expression Node;
        public Expression IncidentEdgeType;
        public Expression AdjacentNodeType;
        public bool Parallel;
    }

    /// <summary>
    /// Class representing expression returning the reachable nodes of a node (as set) reachable via outgoing edges
    /// </summary>
    public class ReachableOutgoing : Expression
    {
        public ReachableOutgoing(Expression node, Expression incidentEdgeType, Expression adjacentNodeType)
        {
            Node = node;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ReachableOutgoing(Node.Copy(renameSuffix), IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.ReachableOutgoing((GRGEN_LIBGR.INode)");
            Node.Emit(sourceCode);
            sourceCode.Append(", ");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", ");
            AdjacentNodeType.Emit(sourceCode);
            if(Parallel) sourceCode.Append(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Node;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public Expression Node;
        public Expression IncidentEdgeType;
        public Expression AdjacentNodeType;
        public bool Parallel;
    }

    /// <summary>
    /// Class representing expression returning the reachable nodes of a node (as set) reachable via incoming edges
    /// </summary>
    public class ReachableIncoming : Expression
    {
        public ReachableIncoming(Expression node, Expression incidentEdgeType, Expression adjacentNodeType)
        {
            Node = node;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ReachableIncoming(Node.Copy(renameSuffix), IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.ReachableIncoming((GRGEN_LIBGR.INode)");
            Node.Emit(sourceCode);
            sourceCode.Append(", ");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", ");
            AdjacentNodeType.Emit(sourceCode);
            if(Parallel) sourceCode.Append(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Node;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public Expression Node;
        public Expression IncidentEdgeType;
        public Expression AdjacentNodeType;
        public bool Parallel;
    }

    /// <summary>
    /// Class representing expression returning the reachable nodes of a node (as set) reachable via incident edges
    /// </summary>
    public class Reachable : Expression
    {
        public Reachable(Expression node, Expression incidentEdgeType, Expression adjacentNodeType)
        {
            Node = node;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new Reachable(Node.Copy(renameSuffix), IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.Reachable((GRGEN_LIBGR.INode)");
            Node.Emit(sourceCode);
            sourceCode.Append(", ");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", ");
            AdjacentNodeType.Emit(sourceCode);
            if(Parallel) sourceCode.Append(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Node;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public Expression Node;
        public Expression IncidentEdgeType;
        public Expression AdjacentNodeType;
        public bool Parallel;
    }

    /// <summary>
    /// Class representing expression returning whether the end node is reachable from the start node via outgoing edges
    /// </summary>
    public class IsReachableOutgoing : Expression
    {
        public IsReachableOutgoing(Expression startNode, Expression endNode,
            Expression incidentEdgeType, Expression adjacentNodeType)
        {
            StartNode = startNode;
            EndNode = endNode;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new IsReachableOutgoing(StartNode.Copy(renameSuffix), EndNode.Copy(renameSuffix),
                IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.IsReachableOutgoing(graph, ");
            sourceCode.Append("(GRGEN_LIBGR.INode)");
            StartNode.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.INode)");
            EndNode.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.EdgeType)");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.NodeType)");
            AdjacentNodeType.Emit(sourceCode);
            if(Parallel) sourceCode.Append(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return StartNode;
            yield return EndNode;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        Expression StartNode;
        Expression EndNode;
        Expression IncidentEdgeType;
        Expression AdjacentNodeType;
        bool Parallel;
    }

    /// <summary>
    /// Class representing expression returning whether the end node is reachable from the start node via incoming edges
    /// </summary>
    public class IsReachableIncoming : Expression
    {
        public IsReachableIncoming(Expression startNode, Expression endNode,
            Expression incidentEdgeType, Expression adjacentNodeType)
        {
            StartNode = startNode;
            EndNode = endNode;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new IsReachableIncoming(StartNode.Copy(renameSuffix), EndNode.Copy(renameSuffix),
                IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.IsReachableIncoming(graph, ");
            sourceCode.Append("(GRGEN_LIBGR.INode)");
            StartNode.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.INode)");
            EndNode.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.EdgeType)");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.NodeType)");
            AdjacentNodeType.Emit(sourceCode);
            if(Parallel) sourceCode.Append(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return StartNode;
            yield return EndNode;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        Expression StartNode;
        Expression EndNode;
        Expression IncidentEdgeType;
        Expression AdjacentNodeType;
        bool Parallel;
    }

    /// <summary>
    /// Class representing expression returning whether the end node is reachable from the start node via incident edges
    /// </summary>
    public class IsReachable : Expression
    {
        public IsReachable(Expression startNode, Expression endNode,
            Expression incidentEdgeType, Expression adjacentNodeType)
        {
            StartNode = startNode;
            EndNode = endNode;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new IsReachable(StartNode.Copy(renameSuffix), EndNode.Copy(renameSuffix),
                IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return StartNode;
            yield return EndNode;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.IsReachable(graph, ");
            sourceCode.Append("(GRGEN_LIBGR.INode)");
            StartNode.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.INode)");
            EndNode.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.EdgeType)");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.NodeType)");
            AdjacentNodeType.Emit(sourceCode);
            if(Parallel) sourceCode.Append(", threadId");
            sourceCode.Append(")");
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        Expression StartNode;
        Expression EndNode;
        Expression IncidentEdgeType;
        Expression AdjacentNodeType;
        bool Parallel;
    }

    /// <summary>
    /// Class representing expression returning whether the end edge is reachable from the start node via outgoing edges
    /// </summary>
    public class IsReachableEdgesOutgoing : Expression
    {
        public IsReachableEdgesOutgoing(Expression startNode, Expression endEdge,
            Expression incidentEdgeType, Expression adjacentNodeType)
        {
            StartNode = startNode;
            EndEdge = endEdge;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new IsReachableEdgesOutgoing(StartNode.Copy(renameSuffix), EndEdge.Copy(renameSuffix),
                IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.IsReachableEdgesOutgoing(graph, ");
            sourceCode.Append("(GRGEN_LIBGR.INode)");
            StartNode.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.IEdge)");
            EndEdge.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.EdgeType)");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.NodeType)");
            AdjacentNodeType.Emit(sourceCode);
            if(Parallel) sourceCode.Append(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return StartNode;
            yield return EndEdge;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        Expression StartNode;
        Expression EndEdge;
        Expression IncidentEdgeType;
        Expression AdjacentNodeType;
        bool Parallel;
    }

    /// <summary>
    /// Class representing expression returning whether the end edge is reachable from the start node via incoming edges
    /// </summary>
    public class IsReachableEdgesIncoming : Expression
    {
        public IsReachableEdgesIncoming(Expression startNode, Expression endEdge,
            Expression incidentEdgeType, Expression adjacentNodeType)
        {
            StartNode = startNode;
            EndEdge = endEdge;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new IsReachableEdgesIncoming(StartNode.Copy(renameSuffix), EndEdge.Copy(renameSuffix),
                IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.IsReachableEdgesIncoming(graph, ");
            sourceCode.Append("(GRGEN_LIBGR.INode)");
            StartNode.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.IEdge)");
            EndEdge.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.EdgeType)");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.NodeType)");
            AdjacentNodeType.Emit(sourceCode);
            if(Parallel) sourceCode.Append(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return StartNode;
            yield return EndEdge;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        Expression StartNode;
        Expression EndEdge;
        Expression IncidentEdgeType;
        Expression AdjacentNodeType;
        bool Parallel;
    }

    /// <summary>
    /// Class representing expression returning whether the end edge is reachable from the start node via incident edges
    /// </summary>
    public class IsReachableEdges : Expression
    {
        public IsReachableEdges(Expression startNode, Expression endEdge,
            Expression incidentEdgeType, Expression adjacentNodeType)
        {
            StartNode = startNode;
            EndEdge = endEdge;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new IsReachableEdges(StartNode.Copy(renameSuffix), EndEdge.Copy(renameSuffix),
                IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.IsReachableEdges(graph, ");
            sourceCode.Append("(GRGEN_LIBGR.INode)");
            StartNode.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.IEdge)");
            EndEdge.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.EdgeType)");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.NodeType)");
            AdjacentNodeType.Emit(sourceCode);
            if(Parallel) sourceCode.Append(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return StartNode;
            yield return EndEdge;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        Expression StartNode;
        Expression EndEdge;
        Expression IncidentEdgeType;
        Expression AdjacentNodeType;
        bool Parallel;
    }

    /// <summary>
    /// Class representing expression returning the induced subgraph from the given set of nodes
    /// </summary>
    public class InducedSubgraph : Expression
    {
        public InducedSubgraph(Expression nodeSet)
        {
            NodeSet = nodeSet;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new InducedSubgraph(NodeSet.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.InducedSubgraph((IDictionary<GRGEN_LIBGR.INode, GRGEN_LIBGR.SetValueType>)");
            NodeSet.Emit(sourceCode);
            sourceCode.Append(", graph)");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return NodeSet;
        }

        Expression NodeSet;
    }

    /// <summary>
    /// Class representing expression returning the defined subgraph from the given set of edges
    /// </summary>
    public class DefinedSubgraph : Expression
    {
        public DefinedSubgraph(Expression edgeSet)
        {
            EdgeSet = edgeSet;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new DefinedSubgraph(EdgeSet.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.DefinedSubgraph((IDictionary<GRGEN_LIBGR.IEdge, GRGEN_LIBGR.SetValueType>)");
            EdgeSet.Emit(sourceCode);
            sourceCode.Append(", graph)");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return EdgeSet;
        }

        Expression EdgeSet;
    }

    /// <summary>
    /// Class representing the max operator.
    /// </summary>
    public class Max : BinFuncOperator
    {
        public Max(Expression left, Expression right) : base(left, right) { }

        public override Expression Copy(string renameSuffix)
        {
            return new Max(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override string GetFuncOperatorAndLParen()
        {
            return "Math.Max(";
        }
    }

    /// <summary>
    /// Class representing the min operator.
    /// </summary>
    public class Min : BinFuncOperator
    {
        public Min(Expression left, Expression right) : base(left, right) { }

        public override Expression Copy(string renameSuffix)
        {
            return new Min(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override string GetFuncOperatorAndLParen()
        {
            return "Math.Min(";
        }
    }

    /// <summary>
    /// Class representing expression returning the absolute value
    /// </summary>
    public class Abs : Expression
    {
        public Abs(Expression expr)
        {
            Expr = expr;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new Abs(Expr.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("Math.Abs(");
            Expr.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Expr;
        }

        Expression Expr;
    }

    /// <summary>
    /// Class representing expression returning the sinus value
    /// </summary>
    public class Sin : Expression
    {
        public Sin(Expression expr)
        {
            Expr = expr;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new Sin(Expr.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("Math.Sin(");
            Expr.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Expr;
        }

        Expression Expr;
    }

    /// <summary>
    /// Class representing expression returning the cosinus value
    /// </summary>
    public class Cos : Expression
    {
        public Cos(Expression expr)
        {
            Expr = expr;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new Cos(Expr.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("Math.Cos(");
            Expr.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Expr;
        }

        Expression Expr;
    }

    /// <summary>
    /// Class representing expression returning the tangens value
    /// </summary>
    public class Tan : Expression
    {
        public Tan(Expression expr)
        {
            Expr = expr;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new Tan(Expr.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("Math.Tan(");
            Expr.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Expr;
        }

        Expression Expr;
    }

    /// <summary>
    /// Class representing expression returning the arcus sinus value
    /// </summary>
    public class ArcSin : Expression
    {
        public ArcSin(Expression expr)
        {
            Expr = expr;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ArcSin(Expr.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("Math.Asin(");
            Expr.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Expr;
        }

        Expression Expr;
    }

    /// <summary>
    /// Class representing expression returning the arcus cosinus value
    /// </summary>
    public class ArcCos : Expression
    {
        public ArcCos(Expression expr)
        {
            Expr = expr;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ArcCos(Expr.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("Math.Acos(");
            Expr.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Expr;
        }

        Expression Expr;
    }

    /// <summary>
    /// Class representing expression returning the arcus tangens value
    /// </summary>
    public class ArcTan : Expression
    {
        public ArcTan(Expression expr)
        {
            Expr = expr;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ArcTan(Expr.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("Math.Atan(");
            Expr.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Expr;
        }

        Expression Expr;
    }

    /// <summary>
    /// Class representing expression returning a canonical string representation of a graph
    /// </summary>
    public class Canonize : Expression
    {
        public Canonize(Expression expr)
        {
            Expr = expr;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new Abs(Expr.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("(");
            Expr.Emit(sourceCode);
            sourceCode.Append(").Canonize()");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Expr;
        }

        Expression Expr;
    }

    /// <summary>
    /// Class representing the logarithm function.
    /// </summary>
    public class Log : Expression
    {
        public Log(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }

        public Log(Expression left)
        {
            Left = left;
        }

        public override Expression Copy(string renameSuffix)
        {
            if(Right != null)
                return new Log(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
            else
                return new Log(Left.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("Math.Log(");
            Left.Emit(sourceCode);
            if(Right != null)
            {
                sourceCode.Append(", ");
                Right.Emit(sourceCode);
            }
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Left;
            if(Right != null) yield return Right;
        }

        public Expression Left;
        public Expression Right;
    }

    /// <summary>
    /// Class representing the to-the-power-of operator.
    /// </summary>
    public class Pow : Expression
    {
        public Pow(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }

        public Pow(Expression right)
        {
            Right = right;
        }

        public override Expression Copy(string renameSuffix)
        {
            if(Left != null)
                return new Pow(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
            else
                return new Pow(Right.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(Left != null)
            {
                sourceCode.Append("Math.Pow(");
                Left.Emit(sourceCode);
                sourceCode.Append(", ");
                Right.Emit(sourceCode);
                sourceCode.Append(")");
            }
            else
            {
                sourceCode.Append("Math.Exp(");
                Right.Emit(sourceCode);
                sourceCode.Append(")");
            }
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            if(Left != null) yield return Left;
            yield return Right;
        }

        public Expression Left;
        public Expression Right;
    }


    /////////////////////////////////////////////////////////////////////////////////////


    /// <summary>
    /// Class representing a comparison of all the attributes.
    /// Is not generated into code, does not exist at source level.
    /// An internal thing only used for the interpretation plan, isomorphy checking.
    /// (todo: Makes sense to offer sth like this at source level, too?)
    /// </summary>
    public class AreAttributesEqual : Expression
    {
        public AreAttributesEqual(IGraphElement this_, PatternElement thisInPattern)
        {
            this.this_ = this_;
            this.thisInPattern = thisInPattern;
        }

        public override Expression Copy(string renameSuffix)
        {
            throw new Exception("Not implemented!");
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            throw new Exception("Not implemented!");
        }

        public bool Execute(IGraphElement that)
        {
            return this_.AreAttributesEqual(that);
        }

        public IGraphElement this_;
        public PatternElement thisInPattern;
    }
}

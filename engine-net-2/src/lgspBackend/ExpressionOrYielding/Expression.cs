/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System;
using System.Collections.Generic;

using de.unika.ipd.grGen.lgsp;

namespace de.unika.ipd.grGen.expression
{
    /// <summary>
    /// Base class of expressions and yieldings
    /// which allows to emit code and to iterate over the contained children
    /// </summary>
    public abstract class ExpressionOrYielding
    {
        public ExpressionOrYielding()
        {
            Id = ++IdSource;
        }

        /// <summary>
        /// pre-run for emitting array per element methods and filters employing lambda expressions (that are then called in the expression/yielding)
        /// default implementation: recursively walk the tree, real functionality is to be attached to the nodes that require it
        /// </summary>
        public virtual void EmitLambdaExpressionImplementationMethods(SourceBuilder sourceCode)
        {
            foreach(ExpressionOrYielding exprOrYield in this)
            {
                exprOrYield.EmitLambdaExpressionImplementationMethods(sourceCode);
            }
        }

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
            // and all version that support profiling, cause that must be different for the parallel version
        }

        /// <summary>
        /// sets for the very node the profiling flag (does not recurse)
        /// </summary>
        public virtual void SetNeedForProfiling(bool profiling)
        {
            // NOP, sufficient for most expressions and yieldings,
            // only the node/edge and the incident/adjacent/reachable-constructs need to call a special version
            // counting up the search steps with each visited element/graph element accessed (but not the implicit operations)
        }

        private static int IdSource = 0;
        protected readonly int Id;
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
        protected BinInfixOperator(Expression left, Expression right)
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

        protected readonly Expression Left;
        protected readonly Expression Right;
    }

    /// <summary>
    /// Class representing an binary prefix operator in function notation.
    /// </summary>
    public abstract class BinFuncOperator : Operator
    {
        protected BinFuncOperator(Expression left, Expression right)
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

        protected readonly Expression Left;
        protected readonly Expression Right;
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

        readonly Expression Condition;
        readonly Expression Left;
        readonly Expression Right;
    }

    /// <summary>
    /// Class representing a logical or.
    /// </summary>
    public class LOG_OR : BinInfixOperator
    {
        public LOG_OR(Expression left, Expression right)
            : base(left, right)
        {
        }

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
        public LOG_AND(Expression left, Expression right)
            : base(left, right)
        {
        }

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
        public BIT_OR(Expression left, Expression right)
            : base(left, right)
        {
        }

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
    /// Class representing a bitwise xor.
    /// </summary>
    public class BIT_XOR : BinInfixOperator
    {
        public BIT_XOR(Expression left, Expression right)
            : base(left, right)
        {
        }

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
        public BIT_AND(Expression left, Expression right)
            : base(left, right)
        {
        }

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
        public EQ(Expression left, Expression right)
            : base(left, right)
        {
        }

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
        public NE(Expression left, Expression right)
            : base(left, right)
        {
        }

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
    /// Class representing an equality comparison (of object classes).
    /// </summary>
    public class OBJECT_CLASS_EQ : BinFuncOperator
    {
        public OBJECT_CLASS_EQ(Expression left, Expression right)
            : base(left, right)
        {
        }

        public override Expression Copy(string renameSuffix)
        {
            return new OBJECT_CLASS_EQ(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override string GetFuncOperatorAndLParen()
        {
            return "GRGEN_LIBGR.ContainerHelper.IsEqual(";
        }
    }

    /// <summary>
    /// Class representing a structural equality comparison (of object classes).
    /// </summary>
    public class OBJECT_CLASS_SE : BinFuncOperator
    {
        public OBJECT_CLASS_SE(Expression left, Expression right)
            : base(left, right)
        {
        }

        public override Expression Copy(string renameSuffix)
        {
            return new OBJECT_CLASS_SE(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.ContainerHelper.DeeplyEqual(");
            Left.Emit(sourceCode);
            sourceCode.Append(", ");
            Right.Emit(sourceCode);
            sourceCode.Append(", new Dictionary<object, object>()");
            sourceCode.Append(")");
        }

        public override string GetFuncOperatorAndLParen()
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Class representing an equality comparison (of transient object classes).
    /// </summary>
    public class TRANSIENT_OBJECT_CLASS_EQ : BinFuncOperator
    {
        public TRANSIENT_OBJECT_CLASS_EQ(Expression left, Expression right)
            : base(left, right)
        {
        }

        public override Expression Copy(string renameSuffix)
        {
            return new TRANSIENT_OBJECT_CLASS_EQ(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override string GetFuncOperatorAndLParen()
        {
            return "GRGEN_LIBGR.ContainerHelper.IsEqual(";
        }
    }

    /// <summary>
    /// Class representing a structural equality comparison (of transient object classes).
    /// </summary>
    public class TRANSIENT_OBJECT_CLASS_SE : BinFuncOperator
    {
        public TRANSIENT_OBJECT_CLASS_SE(Expression left, Expression right)
            : base(left, right)
        {
        }

        public override Expression Copy(string renameSuffix)
        {
            return new TRANSIENT_OBJECT_CLASS_SE(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.ContainerHelper.DeeplyEqual(");
            Left.Emit(sourceCode);
            sourceCode.Append(", ");
            Right.Emit(sourceCode);
            sourceCode.Append(", new Dictionary<object, object>()");
            sourceCode.Append(")");
        }

        public override string GetFuncOperatorAndLParen()
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Class representing an inequality comparison (of object classes).
    /// </summary>
    public class OBJECT_CLASS_NE : BinFuncOperator
    {
        public OBJECT_CLASS_NE(Expression left, Expression right)
            : base(left, right)
        {
        }

        public override Expression Copy(string renameSuffix)
        {
            return new OBJECT_CLASS_NE(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override string GetFuncOperatorAndLParen()
        {
            return "!GRGEN_LIBGR.ContainerHelper.IsEqual(";
        }
    }

    /// <summary>
    /// Class representing an inequality comparison (of transient object classes).
    /// </summary>
    public class TRANSIENT_OBJECT_CLASS_NE : BinFuncOperator
    {
        public TRANSIENT_OBJECT_CLASS_NE(Expression left, Expression right)
            : base(left, right)
        {
        }

        public override Expression Copy(string renameSuffix)
        {
            return new TRANSIENT_OBJECT_CLASS_NE(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override string GetFuncOperatorAndLParen()
        {
            return "!GRGEN_LIBGR.ContainerHelper.IsEqual(";
        }
    }

    /// <summary>
    /// Class representing a less than comparison.
    /// </summary>
    public class LT : BinInfixOperator
    {
        public LT(Expression left, Expression right)
            : base(left, right)
        {
        }

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
        public LE(Expression left, Expression right)
            : base(left, right)
        {
        }

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
        public GT(Expression left, Expression right)
            : base(left, right)
        {
        }

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
        public GE(Expression left, Expression right)
            : base(left, right)
        {
        }

        public override Expression Copy(string renameSuffix)
        {
            return new GE(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override string GetInfixOperator()
        {
            return " >= ";
        }
    }

    /// <summary>
    /// Class representing a less than comparison on strings.
    /// </summary>
    public class STRING_LT : Operator
    {
        public STRING_LT(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new STRING_LT(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("(String.Compare(");
            Left.Emit(sourceCode);
            sourceCode.Append(", ");
            Right.Emit(sourceCode);
            sourceCode.Append(", StringComparison.InvariantCulture)");
            sourceCode.Append("<0)");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Left;
            yield return Right;
        }

        protected readonly Expression Left;
        protected readonly Expression Right;
    }

    /// <summary>
    /// Class representing a less than or equal comparison on strings.
    /// </summary>
    public class STRING_LE : Operator
    {
        public STRING_LE(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new STRING_LE(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("(String.Compare(");
            Left.Emit(sourceCode);
            sourceCode.Append(", ");
            Right.Emit(sourceCode);
            sourceCode.Append(", StringComparison.InvariantCulture)");
            sourceCode.Append("<=0)");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Left;
            yield return Right;
        }

        protected readonly Expression Left;
        protected readonly Expression Right;
    }

    /// <summary>
    /// Class representing a greater than comparison on strings.
    /// </summary>
    public class STRING_GT : Operator
    {
        public STRING_GT(Expression left, Expression right) 
        {
            Left = left;
            Right = right;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new STRING_GT(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("(String.Compare(");
            Left.Emit(sourceCode);
            sourceCode.Append(", ");
            Right.Emit(sourceCode);
            sourceCode.Append(", StringComparison.InvariantCulture)");
            sourceCode.Append(">0)");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Left;
            yield return Right;
        }

        protected readonly Expression Left;
        protected readonly Expression Right;
    }

    /// <summary>
    /// Class representing a greater than or equal comparison on strings.
    /// </summary>
    public class STRING_GE : Operator
    {
        public STRING_GE(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new STRING_GE(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("(String.Compare(");
            Left.Emit(sourceCode);
            sourceCode.Append(", ");
            Right.Emit(sourceCode);
            sourceCode.Append(", StringComparison.InvariantCulture)");
            sourceCode.Append(">=0)");
        }
        
        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Left;
            yield return Right;
        }

        protected readonly Expression Left;
        protected readonly Expression Right;
    }

    // external comparisons

    /// <summary>
    /// Class representing an equality comparison.
    /// </summary>
    public class EXTERNAL_EQ : BinInfixOperator
    {
        public EXTERNAL_EQ(Expression left, Expression right) : base(left, right)
        {
        }

        public override Expression Copy(string renameSuffix)
        {
            return new EXTERNAL_EQ(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override String GetInfixOperator()
        {
            return "==";
        }
    }

    /// <summary>
    /// Class representing an inequality comparison.
    /// </summary>
    public class EXTERNAL_NE : BinInfixOperator
    {
        public EXTERNAL_NE(Expression left, Expression right) : base(left, right)
        {
        }

        public override Expression Copy(string renameSuffix)
        {
            return new EXTERNAL_NE(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override String GetInfixOperator()
        {
            return "!=";
        }
    }

    /// <summary>
    /// Class representing a structural equality comparison.
    /// </summary>
    public class EXTERNAL_SE : Operator
    {
        public EXTERNAL_SE(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new EXTERNAL_SE(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsEqual(");
            Left.Emit(sourceCode);
            sourceCode.Append(", ");
            Right.Emit(sourceCode);
            sourceCode.Append(", new Dictionary<object, object>())");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Left;
            yield return Right;
        }

        protected readonly Expression Left;
        protected readonly Expression Right;
    }

    /// <summary>
    /// Class representing a less than comparison.
    /// </summary>
    public class EXTERNAL_LT : Operator
    {
        public EXTERNAL_LT(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new EXTERNAL_LT(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsLower(");
            Left.Emit(sourceCode);
            sourceCode.Append(", ");
            Right.Emit(sourceCode);
            sourceCode.Append(", new Dictionary<object, object>())");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Left;
            yield return Right;
        }

        protected readonly Expression Left;
        protected readonly Expression Right;
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
            sourceCode.Append(", new Dictionary<object, object>())");
            sourceCode.Append("|| GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsEqual(");
            Left.Emit(sourceCode);
            sourceCode.Append(", ");
            Right.Emit(sourceCode);
            sourceCode.Append(", new Dictionary<object, object>()))");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Left;
            yield return Right;
        }

        protected readonly Expression Left;
        protected readonly Expression Right;
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
            sourceCode.Append(", new Dictionary<object, object>())");
            sourceCode.Append("&& !GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsEqual(");
            Left.Emit(sourceCode);
            sourceCode.Append(", ");
            Right.Emit(sourceCode);
            sourceCode.Append(", new Dictionary<object, object>()))");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Left;
            yield return Right;
        }

        protected readonly Expression Left;
        protected readonly Expression Right;
    }

    /// <summary>
    /// Class representing a greater than or equal comparison.
    /// </summary>
    public class EXTERNAL_GE : Operator
    {
        public EXTERNAL_GE(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new EXTERNAL_GE(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("!GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsLower(");
            Left.Emit(sourceCode);
            sourceCode.Append(", ");
            Right.Emit(sourceCode);
            sourceCode.Append(", new Dictionary<object, object>())");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Left;
            yield return Right;
        }

        protected readonly Expression Left;
        protected readonly Expression Right;
    }

    // TODO: a lot of the functions for the containers are mapping to the same code, 
    // helper functions with the same name resolved by the types in the generated code,
    // would make sense, safe code to not distinguish them

    /// <summary>
    /// Class representing a shift left expression.
    /// </summary>
    public class SHL : BinInfixOperator
    {
        public SHL(Expression left, Expression right)
            : base(left, right)
        {
        }

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
        public SHR(Expression left, Expression right)
            : base(left, right)
        {
        }

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
        public BIT_SHR(Expression left, Expression right)
            : base(left, right)
        {
        }

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
        public ADD(Expression left, Expression right)
            : base(left, right)
        {
        }

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
    /// Class representing a subtraction.
    /// </summary>
    public class SUB : BinInfixOperator
    {
        public SUB(Expression left, Expression right)
            : base(left, right)
        {
        }

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
        public MUL(Expression left, Expression right)
            : base(left, right)
        {
        }

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
        public DIV(Expression left, Expression right)
            : base(left, right)
        {
        }

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
        public MOD(Expression left, Expression right)
            : base(left, right)
        {
        }

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

        readonly Expression Nested;
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

        readonly Expression Nested;
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

        readonly Expression Nested;
    }

    /// <summary>
    /// Class representing a container or string inclusion query.
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

        readonly String Type;
        readonly bool IsDictionary;
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
            if(TypeName == "string")
            {
                if(IsContainer)
                {
                    sourceCode.Append("GRGEN_LIBGR.EmitHelper.ToString(");
                    Nested.Emit(sourceCode);
                    sourceCode.Append(", graph, false, null, null)");
                }
                else
                {
                    sourceCode.Append("GRGEN_LIBGR.EmitHelper.ToStringNonNull(");
                    Nested.Emit(sourceCode);
                    sourceCode.Append(", graph, false, null, null)");
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

        readonly String TypeName;
        readonly Expression Nested;
        readonly bool IsContainer;
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

        readonly String Value;
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

        readonly string EnumType;
        readonly string EnumItem;
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

        readonly Expression Path;
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
            sourceCode.Append(", actionEnv.Backend, graph.Model)");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Path;
        }

        readonly Expression Path;
    }

    public enum CopyKind
    {
        Container, Graph, ClassObject, TransientClassObject, ExternalObject
    }

    /// <summary>
    /// Class representing copy expression
    /// </summary>
    public class CopyExpression : Expression
    {
        public CopyExpression(Expression source, CopyKind copyKind, String type, bool deep, bool isObjectCopierExisting)
        {
            Source = source;
            CopyKind = copyKind;
            Type = type;
            Deep = deep;
            IsObjectCopierExisting = isObjectCopierExisting;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new CopyExpression(Source.Copy(renameSuffix), CopyKind, Type, Deep, IsObjectCopierExisting);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(Deep)
            {
                if(CopyKind == CopyKind.Container)
                {
                    sourceCode.Append("GRGEN_LIBGR.ContainerHelper.Copy(");
                    Source.Emit(sourceCode);
                    sourceCode.Append(", graph, new Dictionary<object, object>())");
                }
                else if(CopyKind == CopyKind.Graph)
                {
                    sourceCode.Append("GRGEN_LIBGR.GraphHelper.Copy(");
                    Source.Emit(sourceCode);
                    sourceCode.Append(")");
                }
                else if(CopyKind == CopyKind.ClassObject)
                {
                    sourceCode.Append("(");
                    Source.Emit(sourceCode);
                    sourceCode.Append(").Copy(graph, new Dictionary<object, object>())");
                }
                else if(CopyKind == CopyKind.TransientClassObject)
                {
                    sourceCode.Append("(");
                    Source.Emit(sourceCode);
                    sourceCode.Append(").Copy(graph, new Dictionary<object, object>())");
                }
                else if(CopyKind == CopyKind.ExternalObject)
                {
                    if(IsObjectCopierExisting)
                    {
                        sourceCode.Append("GRGEN_MODEL.AttributeTypeObjectCopierComparer.Copy(");
                        Source.Emit(sourceCode);
                        sourceCode.Append(", graph, new Dictionary<object, object>())");
                    }
                    else
                        sourceCode.Append("GRGEN_MODEL.ExternalObjectType_object.ThrowCopyClassMissingException()");
                }
            }
            else
            {
                if(CopyKind == CopyKind.Container)
                {
                    sourceCode.Append("new " + Type + "(");
                    Source.Emit(sourceCode);
                    sourceCode.Append(")");
                }
                else if(CopyKind == CopyKind.Graph)
                {
                    sourceCode.Append("GRGEN_LIBGR.GraphHelper.Copy(");
                    Source.Emit(sourceCode);
                    sourceCode.Append(")");
                }
                else if(CopyKind == CopyKind.ClassObject)
                {
                    sourceCode.Append("(");
                    Source.Emit(sourceCode);
                    sourceCode.Append(").Clone(graph)");
                }
                else if(CopyKind == CopyKind.TransientClassObject)
                {
                    sourceCode.Append("(");
                    Source.Emit(sourceCode);
                    sourceCode.Append(").Clone()");
                }
                else if(CopyKind == CopyKind.ExternalObject)
                {
                    if(IsObjectCopierExisting)
                    {
                        sourceCode.Append("GRGEN_MODEL.AttributeTypeObjectCopierComparer.Copy(");
                        Source.Emit(sourceCode);
                        sourceCode.Append(", graph, null)");
                    }
                    else
                        sourceCode.Append("GRGEN_MODEL.ExternalObjectType_object.ThrowCopyClassMissingException()");
                }
            }
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Source;
        }

        readonly CopyKind CopyKind;
        readonly String Type; // if non-null, gives the container type to copy, if null it's a graph or class object
        readonly Expression Source;
        readonly bool Deep;
        readonly bool IsObjectCopierExisting;
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

        readonly String Iterated;
    }

    /// <summary>
    /// Class representing the access of a match (of an iterated)
    /// </summary>
    public class MatchAccess : Expression
    {
        public MatchAccess(Expression match, String entity)
        {
            Match = match;
            Entity = entity;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new MatchAccess(Match, Entity);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            Match.Emit(sourceCode);
            sourceCode.Append(".@" + Entity);
        }

        readonly Expression Match;
        readonly String Entity;
    }

    /// <summary>
    /// Class representing the matches array of an iterated pattern expression / iterated query
    /// </summary>
    public class IteratedQuery : Expression
    {
        public IteratedQuery(String iterated)
        {
            Iterated = iterated;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new IteratedQuery(Iterated + renameSuffix);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("match." + NamesOfEntities.MatchName(Iterated, BuildMatchObjectType.Iteration) + ".ToListExact()");
        }

        readonly String Iterated;
    }

    /// <summary>
    /// Class representing qualification expression
    /// </summary>
    public class Qualification : Expression
    {
        public Qualification(String ownerType, bool isGraphElementType, String owner, String member)
        {
            OwnerType = ownerType;
            IsGraphElementType = isGraphElementType;
            Owner = owner;
            Member = member;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new Qualification(OwnerType, IsGraphElementType, Owner + renameSuffix, Member);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            string candidateVariable;
            if(IsGraphElementType)
                candidateVariable = NamesOfEntities.CandidateVariable(Owner);
            else
                candidateVariable = NamesOfEntities.Variable(Owner);
            sourceCode.Append("((" + OwnerType + ")" + candidateVariable + ").@" + Member);
        }

        readonly String OwnerType;
        readonly bool IsGraphElementType;
        readonly String Owner;
        readonly String Member;
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

        readonly String OwnerType;
        readonly String Owner;
        readonly String Member;
    }

    /// <summary>
    /// Class representing qualification after cast expression
    /// </summary>
    public class CastQualification : Expression
    {
        public CastQualification(Expression owner, String member)
        {
            Owner = owner;
            Member = member;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new CastQualification(Owner.Copy(renameSuffix), Member);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("(");
            Owner.Emit(sourceCode);
            sourceCode.Append(").@" + Member);
        }

        readonly Expression Owner;
        readonly String Member;
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

        readonly String Entity;
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

        readonly public String Entity;
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

        readonly public String GlobalVariableName;
        readonly public String Type;
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

        readonly Expression Entity;
        readonly Expression Nested;
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

        readonly Expression Nested;
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

        readonly Expression StringExpr;
    }

    /// <summary>
    /// Class representing a string to uppercase expression.
    /// </summary>
    public class StringToUpper : Expression
    {
        public StringToUpper(Expression stringExpr)
        {
            StringExpr = stringExpr;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new StringToUpper(StringExpr.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("(");
            StringExpr.Emit(sourceCode);
            sourceCode.Append(").ToUpperInvariant()");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return StringExpr;
        }

        readonly Expression StringExpr;
    }

    /// <summary>
    /// Class representing a string to lowercase expression.
    /// </summary>
    public class StringToLower : Expression
    {
        public StringToLower(Expression stringExpr)
        {
            StringExpr = stringExpr;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new StringToLower(StringExpr.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("(");
            StringExpr.Emit(sourceCode);
            sourceCode.Append(").ToLowerInvariant()");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return StringExpr;
        }

        readonly Expression StringExpr;
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

        public StringSubstring(Expression stringExpr, Expression startExpr)
        {
            StringExpr = stringExpr;
            StartExpr = startExpr;
            LengthExpr = null;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new StringSubstring(StringExpr.Copy(renameSuffix), 
                StartExpr.Copy(renameSuffix), LengthExpr!=null ? LengthExpr.Copy(renameSuffix) : null);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("(");
            StringExpr.Emit(sourceCode);
            sourceCode.Append(").Substring(");
            StartExpr.Emit(sourceCode);
            if(LengthExpr != null)
            {
                sourceCode.Append(", ");
                LengthExpr.Emit(sourceCode);
            }
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return StringExpr;
            yield return StartExpr;
            if(LengthExpr != null)
                yield return LengthExpr;
        }

        readonly Expression StringExpr;
        readonly Expression StartExpr;
        readonly Expression LengthExpr;
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

        public StringIndexOf(Expression stringExpr, Expression stringToSearchForExpr, Expression startIndexExpr)
        {
            StringExpr = stringExpr;
            StringToSearchForExpr = stringToSearchForExpr;
            StartIndexExpr = startIndexExpr;
        }

        public override Expression Copy(string renameSuffix)
        {
            if(StartIndexExpr!=null)
                return new StringIndexOf(StringExpr.Copy(renameSuffix), StringToSearchForExpr.Copy(renameSuffix), StartIndexExpr.Copy(renameSuffix));
            else
                return new StringIndexOf(StringExpr.Copy(renameSuffix), StringToSearchForExpr.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("(");
            StringExpr.Emit(sourceCode);
            sourceCode.Append(").IndexOf(");
            StringToSearchForExpr.Emit(sourceCode);
            if(StartIndexExpr != null)
            {
                sourceCode.Append(", ");
                StartIndexExpr.Emit(sourceCode);
            }
            sourceCode.Append(", StringComparison.InvariantCulture");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return StringExpr;
            yield return StringToSearchForExpr;
            if(StartIndexExpr != null)
                yield return StartIndexExpr;
        }

        readonly Expression StringExpr;
        readonly Expression StringToSearchForExpr;
        readonly Expression StartIndexExpr;
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

        public StringLastIndexOf(Expression stringExpr, Expression stringToSearchForExpr, Expression startIndexExpr)
        {
            StringExpr = stringExpr;
            StringToSearchForExpr = stringToSearchForExpr;
            StartIndexExpr = startIndexExpr;
        }

        public override Expression Copy(string renameSuffix)
        {
            if(StartIndexExpr != null)
                return new StringLastIndexOf(StringExpr.Copy(renameSuffix), StringToSearchForExpr.Copy(renameSuffix), StartIndexExpr.Copy(renameSuffix));
            else
                return new StringLastIndexOf(StringExpr.Copy(renameSuffix), StringToSearchForExpr.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("(");
            StringExpr.Emit(sourceCode);
            sourceCode.Append(").LastIndexOf(");
            StringToSearchForExpr.Emit(sourceCode);
            if(StartIndexExpr != null)
            {
                sourceCode.Append(", ");
                StartIndexExpr.Emit(sourceCode);
            }
            sourceCode.Append(", StringComparison.InvariantCulture");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return StringExpr;
            yield return StringToSearchForExpr;
            if(StartIndexExpr != null)
                yield return StartIndexExpr;
        }

        readonly Expression StringExpr;
        readonly Expression StringToSearchForExpr;
        readonly Expression StartIndexExpr;
    }

    /// <summary>
    /// Class representing a string starts with expression.
    /// </summary>
    public class StringStartsWith : Expression
    {
        public StringStartsWith(Expression stringExpr, Expression stringToSearchForExpr)
        {
            StringExpr = stringExpr;
            StringToSearchForExpr = stringToSearchForExpr;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new StringStartsWith(StringExpr.Copy(renameSuffix), StringToSearchForExpr.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("(");
            StringExpr.Emit(sourceCode);
            sourceCode.Append(").StartsWith(");
            StringToSearchForExpr.Emit(sourceCode);
            sourceCode.Append(", StringComparison.InvariantCulture");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return StringExpr;
            yield return StringToSearchForExpr;
        }

        readonly Expression StringExpr;
        readonly Expression StringToSearchForExpr;
    }

    /// <summary>
    /// Class representing a string ends with expression.
    /// </summary>
    public class StringEndsWith : Expression
    {
        public StringEndsWith(Expression stringExpr, Expression stringToSearchForExpr)
        {
            StringExpr = stringExpr;
            StringToSearchForExpr = stringToSearchForExpr;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new StringEndsWith(StringExpr.Copy(renameSuffix), StringToSearchForExpr.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("(");
            StringExpr.Emit(sourceCode);
            sourceCode.Append(").EndsWith(");
            StringToSearchForExpr.Emit(sourceCode);
            sourceCode.Append(", StringComparison.InvariantCulture");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return StringExpr;
            yield return StringToSearchForExpr;
        }

        readonly Expression StringExpr;
        readonly Expression StringToSearchForExpr;
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

        readonly Expression StringExpr;
        readonly Expression StartExpr;
        readonly Expression LengthExpr;
        readonly Expression ReplaceStrExpr;
    }

    /// <summary>
    /// Class representing a string as array expression.
    /// </summary>
    public class StringAsArray : Expression
    {
        public StringAsArray(Expression stringExpr, Expression stringToSplitAtExpr)
        {
            StringExpr = stringExpr;
            StringToSplitAtExpr = stringToSplitAtExpr;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new StringAsArray(StringExpr.Copy(renameSuffix), StringToSplitAtExpr.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.ContainerHelper.StringAsArray(");
            StringExpr.Emit(sourceCode);
            sourceCode.Append(", ");
            StringToSplitAtExpr.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return StringExpr;
            yield return StringToSplitAtExpr;
        }

        readonly Expression StringExpr;
        readonly Expression StringToSplitAtExpr;
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
            for(int i = 0; i < Arguments.Length; ++i)
            {
                newArguments[i] = (Expression)Arguments[i].Copy(renameSuffix);
            }
            return new FunctionInvocation(PackageName, FunctionName, newArguments, (String[])ArgumentTypes.Clone());
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append(PackageName + "Functions." + FunctionName + "(actionEnv, graph");
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

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            foreach(Expression argument in Arguments)
            {
                yield return argument;
            }
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public readonly String PackageName;
        public readonly String FunctionName;
        public readonly Expression[] Arguments;
        public readonly String[] ArgumentTypes; // for each argument: if node/edge: the interface type, otherwise: null
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
            for(int i = 0; i < Arguments.Length; ++i)
            {
                newArguments[i] = (Expression)Arguments[i].Copy(renameSuffix);
            }
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
            if(Parallel)
                sourceCode.AppendFront(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            foreach(Expression argument in Arguments)
            {
                yield return argument;
            }
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public readonly String FunctionName;
        public readonly Expression[] Arguments;
        public readonly String[] ArgumentTypes; // for each argument: if node/edge: the interface type, otherwise: null
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
            for(int i = 0; i < Arguments.Length; ++i)
            {
                newArguments[i] = (Expression)Arguments[i].Copy(renameSuffix);
            }
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
                if(ArgumentTypes[i] != null)
                    sourceCode.Append("(" + ArgumentTypes[i] + ")");
                argument.Emit(sourceCode);
            }
            if(Parallel)
                sourceCode.AppendFront(", threadId");
            sourceCode.Append(")");
        }

        readonly String OwnerType;
        readonly String Owner;
    }

    /// <summary>
    /// Class representing a function method invocation (of an external attribute evaluation function method).
    /// </summary>
    public class ExternalFunctionMethodInvocation : ExternalFunctionInvocation
    {
        public ExternalFunctionMethodInvocation(Expression owner, String functionName, Expression[] arguments, String[] argumentTypes)
            : base(functionName, arguments, argumentTypes)
        {
            Owner = owner;
        }

        public override Expression Copy(string renameSuffix)
        {
            Expression[] newArguments = new Expression[Arguments.Length];
            for(int i = 0; i < Arguments.Length; ++i)
            {
                newArguments[i] = (Expression)Arguments[i].Copy(renameSuffix);
            }
            return new ExternalFunctionMethodInvocation(Owner.Copy(renameSuffix), FunctionName, newArguments, (String[])ArgumentTypes.Clone());
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("(");
            Owner.Emit(sourceCode);
            sourceCode.Append(").@");
            sourceCode.Append(FunctionName + "(actionEnv, graph");
            for(int i = 0; i < Arguments.Length; ++i)
            {
                sourceCode.Append(", ");
                Expression argument = Arguments[i];
                if(ArgumentTypes[i] != null)
                    sourceCode.Append("(" + ArgumentTypes[i] + ")");
                argument.Emit(sourceCode);
            }
            if(Parallel)
                sourceCode.AppendFront(", threadId");
            sourceCode.Append(")");
        }

        readonly Expression Owner;
    }

    /// <summary>
    /// Class representing expression returning the current time, measured as windows file time
    /// </summary>
    public class Now : Expression
    {
        public Now()
        {
        }

        public override Expression Copy(string renameSuffix)
        {
            return new Now();
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("DateTime.UtcNow.ToFileTime()");
        }
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

        readonly Expression Expr;
    }

    /// <summary>
    /// Class representing expression returning the signum
    /// </summary>
    public class Sgn : Expression
    {
        public Sgn(Expression expr)
        {
            Expr = expr;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new Sgn(Expr.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("Math.Sign(");
            Expr.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Expr;
        }

        readonly Expression Expr;
    }

    /// <summary>
    /// Class representing expression returning pi
    /// </summary>
    public class Pi : Expression
    {
        public Pi()
        {
        }

        public override Expression Copy(string renameSuffix)
        {
            return new Pi();
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("Math.PI");
        }
    }

    /// <summary>
    /// Class representing expression returning e
    /// </summary>
    public class E : Expression
    {
        public E()
        {
        }

        public override Expression Copy(string renameSuffix)
        {
            return new E();
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("Math.E");
        }
    }

    /// <summary>
    /// Class representing expression returning the minimum value of type byte
    /// </summary>
    public class ByteMin : Expression
    {
        public ByteMin()
        {
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ByteMin();
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("SByte.MinValue");
        }
    }

    /// <summary>
    /// Class representing expression returning the maximum value of type byte
    /// </summary>
    public class ByteMax : Expression
    {
        public ByteMax()
        {
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ByteMax();
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("SByte.MaxValue");
        }
    }

    /// <summary>
    /// Class representing expression returning the minimum value of type short
    /// </summary>
    public class ShortMin : Expression
    {
        public ShortMin()
        {
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ShortMin();
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("Int16.MinValue");
        }
    }

    /// <summary>
    /// Class representing expression returning the maximum value of type short
    /// </summary>
    public class ShortMax : Expression
    {
        public ShortMax()
        {
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ShortMax();
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("Int16.MaxValue");
        }
    }

    /// <summary>
    /// Class representing expression returning the minimum value of type int
    /// </summary>
    public class IntMin : Expression
    {
        public IntMin()
        {
        }

        public override Expression Copy(string renameSuffix)
        {
            return new IntMin();
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("Int32.MinValue");
        }
    }

    /// <summary>
    /// Class representing expression returning the maximum value of type int
    /// </summary>
    public class IntMax : Expression
    {
        public IntMax()
        {
        }

        public override Expression Copy(string renameSuffix)
        {
            return new IntMax();
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("Int32.MaxValue");
        }
    }

    /// <summary>
    /// Class representing expression returning the minimum value of type long
    /// </summary>
    public class LongMin : Expression
    {
        public LongMin()
        {
        }

        public override Expression Copy(string renameSuffix)
        {
            return new LongMin();
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("Int64.MinValue");
        }
    }

    /// <summary>
    /// Class representing expression returning the maximum value of type long
    /// </summary>
    public class LongMax : Expression
    {
        public LongMax()
        {
        }

        public override Expression Copy(string renameSuffix)
        {
            return new LongMax();
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("Int64.MaxValue");
        }
    }

    /// <summary>
    /// Class representing expression returning the minimum value of type float
    /// </summary>
    public class FloatMin : Expression
    {
        public FloatMin()
        {
        }

        public override Expression Copy(string renameSuffix)
        {
            return new FloatMin();
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("Single.MinValue");
        }
    }

    /// <summary>
    /// Class representing expression returning the maximum value of type float
    /// </summary>
    public class FloatMax : Expression
    {
        public FloatMax()
        {
        }

        public override Expression Copy(string renameSuffix)
        {
            return new FloatMax();
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("Single.MaxValue");
        }
    }

    /// <summary>
    /// Class representing expression returning the minimum value of type double
    /// </summary>
    public class DoubleMin : Expression
    {
        public DoubleMin()
        {
        }

        public override Expression Copy(string renameSuffix)
        {
            return new DoubleMin();
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("Double.MinValue");
        }
    }

    /// <summary>
    /// Class representing expression returning the maximum value of type double
    /// </summary>
    public class DoubleMax : Expression
    {
        public DoubleMax()
        {
        }

        public override Expression Copy(string renameSuffix)
        {
            return new DoubleMax();
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("Double.MaxValue");
        }
    }

    /// <summary>
    /// Class representing expression returning the ceil value
    /// </summary>
    public class Ceil : Expression
    {
        public Ceil(Expression expr)
        {
            Expr = expr;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new Ceil(Expr.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("Math.Ceiling(");
            Expr.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Expr;
        }

        readonly Expression Expr;
    }

    /// <summary>
    /// Class representing expression returning the floor value
    /// </summary>
    public class Floor : Expression
    {
        public Floor(Expression expr)
        {
            Expr = expr;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new Floor(Expr.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("Math.Floor(");
            Expr.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Expr;
        }

        readonly Expression Expr;
    }

    /// <summary>
    /// Class representing expression returning the rounded value
    /// </summary>
    public class Round : Expression
    {
        public Round(Expression expr)
        {
            Expr = expr;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new Round(Expr.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("Math.Round(");
            Expr.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Expr;
        }

        readonly Expression Expr;
    }

    /// <summary>
    /// Class representing expression returning the truncated value
    /// </summary>
    public class Truncate : Expression
    {
        public Truncate(Expression expr)
        {
            Expr = expr;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new Truncate(Expr.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("Math.Truncate(");
            Expr.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Expr;
        }

        readonly Expression Expr;
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

        readonly Expression Expr;
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

        readonly Expression Expr;
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

        readonly Expression Expr;
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

        readonly Expression Expr;
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

        readonly Expression Expr;
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

        readonly Expression Expr;
    }

    /// <summary>
    /// Class representing the square function.
    /// </summary>
    public class Sqr : Expression
    {
        public Sqr(Expression expr)
        {
            Expr = expr;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new Sqr(Expr.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.MathHelper.Sqr(");
            Expr.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Expr;
        }

        public readonly Expression Expr;
    }

    /// <summary>
    /// Class representing the square root function.
    /// </summary>
    public class Sqrt : Expression
    {
        public Sqrt(Expression expr)
        {
            Expr = expr;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new Sqr(Expr.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("Math.Sqrt(");
            Expr.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Expr;
        }

        public readonly Expression Expr;
    }

    /// <summary>
    /// Class representing the to-the-power-of function.
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
            if(Left != null)
                yield return Left;
            yield return Right;
        }

        public readonly Expression Left;
        public readonly Expression Right;
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
            if(Right != null)
                yield return Right;
        }

        public readonly Expression Left;
        public readonly Expression Right;
    }

    /// <summary>
    /// Class representing the scan function
    /// </summary>
    public class Scan : Expression
    {
        public Scan(Expression stringExpr, String attributeType, String type)
        {
            StringExpr = stringExpr;
            AttributeType = attributeType;
            Type = type;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new Scan(StringExpr, AttributeType, Type);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("((" + Type + ")");
            sourceCode.Append("GRGEN_LIBGR.GRSImport.Scan(" + AttributeType + ", ");
            StringExpr.Emit(sourceCode);
            sourceCode.Append(", graph))");
        }

        readonly Expression StringExpr;
        readonly String AttributeType;
        readonly String Type;
    }

    /// <summary>
    /// Class representing the tryscan function
    /// </summary>
    public class TryScan : Expression
    {
        public TryScan(Expression stringExpr, String attributeType)
        {
            StringExpr = stringExpr;
            AttributeType = attributeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new TryScan(StringExpr, AttributeType);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GRSImport.TryScan(" + AttributeType + ", ");
            StringExpr.Emit(sourceCode);
            sourceCode.Append(", graph)");
        }

        readonly Expression StringExpr;
        readonly String AttributeType;
    }
}

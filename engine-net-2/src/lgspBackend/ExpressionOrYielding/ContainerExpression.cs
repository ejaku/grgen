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
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.expression
{
    /// <summary>
    /// Class representing the set/map union operator.
    /// </summary>
    public class DICT_BIT_OR : BinFuncOperator
    {
        public DICT_BIT_OR(Expression left, Expression right)
            : base(left, right)
        {
        }

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
        public DICT_BIT_AND(Expression left, Expression right)
            : base(left, right)
        {
        }

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
        public DICT_EXCEPT(Expression left, Expression right)
            : base(left, right)
        {
        }

        public override Expression Copy(string renameSuffix)
        {
            return new DICT_EXCEPT(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override string GetFuncOperatorAndLParen()
        {
            return "GRGEN_LIBGR.ContainerHelper.Except(";
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
        public DICT_EQ(Expression left, Expression right)
            : base(left, right)
        {
        }

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
        public DICT_NE(Expression left, Expression right)
            : base(left, right)
        {
        }

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
    /// Class representing set/map structural equality comparison.
    /// </summary>
    public class DICT_SE : BinFuncOperator
    {
        public DICT_SE(Expression left, Expression right, bool isSet, bool isAttributeBearer)
            : base(left, right)
        {
            this.isSet = isSet;
            this.isAttributeBearer = isAttributeBearer;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new DICT_SE(Left.Copy(renameSuffix), Right.Copy(renameSuffix), isSet, isAttributeBearer);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            String methodName = isSet ? "DeeplyEqualSet" : "DeeplyEqualMap";
            sourceCode.Append("GRGEN_LIBGR.ContainerHelper." + methodName + "(");
            Left.Emit(sourceCode);
            sourceCode.Append(", ");
            Right.Emit(sourceCode);
            sourceCode.Append(", new Dictionary<object, object>()");
            if(isAttributeBearer) {
                sourceCode.Append(", new Dictionary<GRGEN_LIBGR.IAttributeBearer, object>()");
                sourceCode.Append(", new Dictionary<GRGEN_LIBGR.IAttributeBearer, object>()");
            } else {
                sourceCode.Append(", new Dictionary<object, object>()");
                sourceCode.Append(", new Dictionary<object, object>()");
            }
            sourceCode.Append(")");
        }

        public override string GetFuncOperatorAndLParen()
        {
            throw new NotImplementedException();
        }

        bool isSet;
        bool isAttributeBearer;
    }

    /// <summary>
    /// Class representing proper subset/map comparison.
    /// </summary>
    public class DICT_LT : BinFuncOperator
    {
        public DICT_LT(Expression left, Expression right)
            : base(left, right)
        {
        }

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
        public DICT_LE(Expression left, Expression right)
            : base(left, right)
        {
        }

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
        public DICT_GT(Expression left, Expression right)
            : base(left, right)
        {
        }

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
        public DICT_GE(Expression left, Expression right)
            : base(left, right)
        {
        }

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
        public LIST_EQ(Expression left, Expression right)
            : base(left, right)
        {
        }

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
        public LIST_NE(Expression left, Expression right)
            : base(left, right)
        {
        }

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
    /// Class representing array structural equality comparison.
    /// </summary>
    public class LIST_SE : BinFuncOperator
    {
        public LIST_SE(Expression left, Expression right, bool isAttributeBearer)
            : base(left, right)
        {
            this.isAttributeBearer = isAttributeBearer;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new LIST_SE(Left.Copy(renameSuffix), Right.Copy(renameSuffix), isAttributeBearer);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            String methodName = isAttributeBearer ? "DeeplyEqualArrayAttributeBearer" : "DeeplyEqualArrayObject";
            sourceCode.Append("GRGEN_LIBGR.ContainerHelper." + methodName + "(");
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

        bool isAttributeBearer;
    }

    /// <summary>
    /// Class representing proper subarray comparison.
    /// </summary>
    public class LIST_LT : BinFuncOperator
    {
        public LIST_LT(Expression left, Expression right)
            : base(left, right)
        {
        }

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
        public LIST_LE(Expression left, Expression right)
            : base(left, right)
        {
        }

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
        public LIST_GT(Expression left, Expression right)
            : base(left, right)
        {
        }

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
        public LIST_GE(Expression left, Expression right)
            : base(left, right)
        {
        }

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
        public DEQUE_EQ(Expression left, Expression right)
            : base(left, right)
        {
        }

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
        public DEQUE_NE(Expression left, Expression right)
            : base(left, right)
        {
        }

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
    /// Class representing deque structural equality comparison.
    /// </summary>
    public class DEQUE_SE : BinFuncOperator
    {
        public DEQUE_SE(Expression left, Expression right, bool isAttributeBearer)
            : base(left, right)
        {
            this.isAttributeBearer = isAttributeBearer;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new DEQUE_SE(Left.Copy(renameSuffix), Right.Copy(renameSuffix), isAttributeBearer);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            String methodName = isAttributeBearer ? "DeeplyEqualDequeAttributeBearer" : "DeeplyEqualDequeObject";
            sourceCode.Append("GRGEN_LIBGR.ContainerHelper." + methodName + "(");
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

        bool isAttributeBearer;
    }

    /// <summary>
    /// Class representing proper subdeque comparison.
    /// </summary>
    public class DEQUE_LT : BinFuncOperator
    {
        public DEQUE_LT(Expression left, Expression right) : base(left, right)
        {
        }

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
        public DEQUE_LE(Expression left, Expression right)
            : base(left, right)
        {
        }

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
        public DEQUE_GT(Expression left, Expression right)
            : base(left, right)
        {
        }

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
        public DEQUE_GE(Expression left, Expression right)
            : base(left, right)
        {
        }

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
    /// Class representing an array concatenation.
    /// </summary>
    public class LIST_ADD : BinFuncOperator
    {
        public LIST_ADD(Expression left, Expression right)
            : base(left, right)
        {
        }

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
        public DEQUE_ADD(Expression left, Expression right)
            : base(left, right)
        {
        }

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

        readonly Expression Target;
        readonly Expression KeyExpr;
        readonly String Type;
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

        readonly Expression Target;
        readonly Expression KeyExpr;
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

        readonly Expression Target;
        readonly Expression KeyExpr;
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

        readonly Expression Target;
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

        readonly Expression Target;
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

        readonly Expression Target;
        readonly Expression Number;
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

        readonly Expression Target;
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

        readonly Expression Target;
    }

    /// <summary>
    /// Class representing a map as array expression.
    /// </summary>
    public class MapAsArray : Expression
    {
        public MapAsArray(Expression target)
        {
            Target = target;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new MapAsArray(Target.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.ContainerHelper.MapAsArray(");
            Target.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
        }

        readonly Expression Target;
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

        readonly Expression Target;
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

        readonly Expression Target;
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

        readonly Expression Target;
        readonly Expression Number;
    }

    /// <summary>
    /// Class representing a set as array expression.
    /// </summary>
    public class SetAsArray : Expression
    {
        public SetAsArray(Expression target)
        {
            Target = target;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new SetAsArray(Target.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.ContainerHelper.SetAsArray(");
            Target.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
        }

        readonly Expression Target;
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

        readonly Expression Target;
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

        readonly Expression Target;
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
            sourceCode.Append("GRGEN_LIBGR.ContainerHelper.Peek(");
            Target.Emit(sourceCode);
            if(Number != null)
            {
                sourceCode.Append(", ");
                Number.Emit(sourceCode);
            }
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
            if(Number!=null)
                yield return Number;
            else yield break;
        }

        readonly Expression Target;
        readonly Expression Number;
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

        public ArrayIndexOf(Expression target, Expression value, Expression startIndex)
        {
            Target = target;
            Value = value;
            StartIndex = startIndex;
        }

        public override Expression Copy(string renameSuffix)
        {
            if(StartIndex != null)
                return new ArrayIndexOf(Target.Copy(renameSuffix), Value.Copy(renameSuffix), StartIndex.Copy(renameSuffix));
            else
                return new ArrayIndexOf(Target.Copy(renameSuffix), Value.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.ContainerHelper.IndexOf(");
            Target.Emit(sourceCode);
            sourceCode.Append(", ");
            Value.Emit(sourceCode);
            if(StartIndex != null)
            {
                sourceCode.Append(", ");
                StartIndex.Emit(sourceCode);
            }
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
            yield return Value;
            if(StartIndex != null)
               yield return StartIndex;
        }

        readonly Expression Target;
        readonly Expression Value;
        readonly Expression StartIndex;
    }

    /// <summary>
    /// Class representing an array index of by attribute expression.
    /// </summary>
    public class ArrayIndexOfBy : Expression
    {
        public ArrayIndexOfBy(Expression target, string ownerType, string member, Expression value)
        {
            Target = target;
            OwnerType = ownerType;
            Member = member;
            Value = value;
        }

        public ArrayIndexOfBy(Expression target, string ownerType, string member, Expression value, Expression startIndex)
        {
            Target = target;
            OwnerType = ownerType;
            Member = member;
            Value = value;
            StartIndex = startIndex;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ArrayIndexOfBy(Target.Copy(renameSuffix), OwnerType, Member, Value.Copy(renameSuffix), StartIndex != null ? StartIndex.Copy(renameSuffix) : null);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFormat("GRGEN_MODEL.ArrayHelper_{0}_{1}.ArrayIndexOfBy(", OwnerType, Member);
            Target.Emit(sourceCode);
            sourceCode.Append(", ");
            Value.Emit(sourceCode);
            if(StartIndex != null)
            {
                sourceCode.Append(", ");
                StartIndex.Emit(sourceCode);
            }
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
            yield return Value;
            if(StartIndex != null)
                yield return StartIndex;
        }

        readonly Expression Target;
        readonly String OwnerType;
        readonly String Member;
        readonly Expression Value;
        readonly Expression StartIndex;
    }

    /// <summary>
    /// Class representing an array of action matches index of by member expression.
    /// </summary>
    public class ArrayOfMatchTypeIndexOfBy : Expression
    {
        public ArrayOfMatchTypeIndexOfBy(Expression target, string patternName, string member, string rulePackage, Expression value)
        {
            Target = target;
            PatternName = patternName;
            Member = member;
            RulePackage = rulePackage;
            Value = value;
        }

        public ArrayOfMatchTypeIndexOfBy(Expression target, string patternName, string member, string rulePackage, Expression value, Expression startIndex)
        {
            Target = target;
            PatternName = patternName;
            Member = member;
            RulePackage = rulePackage;
            Value = value;
            StartIndex = startIndex;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ArrayOfMatchTypeIndexOfBy(Target.Copy(renameSuffix), PatternName, Member, RulePackage, Value.Copy(renameSuffix), StartIndex != null ? StartIndex.Copy(renameSuffix) : null);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFrontFormat("{0}ArrayHelper.Array_{1}_{2}_{3}(",
                "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(RulePackage),
                PatternName, "indexOfBy", Member);
            Target.Emit(sourceCode);
            sourceCode.Append(", ");
            Value.Emit(sourceCode);
            if(StartIndex != null)
            {
                sourceCode.Append(", ");
                StartIndex.Emit(sourceCode);
            }
            sourceCode.AppendFrontFormat(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
            yield return Value;
            if(StartIndex != null)
                yield return StartIndex;
        }

        readonly Expression Target;
        readonly String PatternName;
        readonly String Member;
        readonly String RulePackage;
        readonly Expression Value;
        readonly Expression StartIndex;
    }

    /// <summary>
    /// Class representing an array of iterated matches index of by member expression.
    /// </summary>
    public class ArrayOfIteratedMatchTypeIndexOfBy : Expression
    {
        public ArrayOfIteratedMatchTypeIndexOfBy(Expression target, string patternName, string iteratedName, string member, string rulePackage, Expression value)
        {
            Target = target;
            PatternName = patternName;
            IteratedName = iteratedName;
            Member = member;
            RulePackage = rulePackage;
            Value = value;
        }

        public ArrayOfIteratedMatchTypeIndexOfBy(Expression target, string patternName, string iteratedName, string member, string rulePackage, Expression value, Expression startIndex)
        {
            Target = target;
            PatternName = patternName;
            IteratedName = iteratedName;
            Member = member;
            RulePackage = rulePackage;
            Value = value;
            StartIndex = startIndex;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ArrayOfIteratedMatchTypeIndexOfBy(Target.Copy(renameSuffix), PatternName, IteratedName, Member, RulePackage, Value.Copy(renameSuffix), StartIndex != null ? StartIndex.Copy(renameSuffix) : null);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFrontFormat("{0}ArrayHelper.Array_{1}_{2}_{3}_{4}(",
                "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(RulePackage),
                PatternName, IteratedName, "indexOfBy", Member);
            Target.Emit(sourceCode);
            sourceCode.Append(", ");
            Value.Emit(sourceCode);
            if(StartIndex != null)
            {
                sourceCode.Append(", ");
                StartIndex.Emit(sourceCode);
            }
            sourceCode.AppendFrontFormat(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
            yield return Value;
            if(StartIndex != null)
                yield return StartIndex;
        }

        readonly Expression Target;
        readonly String PatternName;
        readonly String IteratedName;
        readonly String Member;
        readonly String RulePackage;
        readonly Expression Value;
        readonly Expression StartIndex;
    }

    /// <summary>
    /// Class representing an array of match class matches index of by member expression.
    /// </summary>
    public class ArrayOfMatchClassTypeIndexOfBy : Expression
    {
        public ArrayOfMatchClassTypeIndexOfBy(Expression target, string matchClassName, string member, string matchClassPackage, Expression value)
        {
            Target = target;
            MatchClassName = matchClassName;
            Member = member;
            MatchClassPackage = matchClassPackage;
            Value = value;
        }

        public ArrayOfMatchClassTypeIndexOfBy(Expression target, string matchClassName, string member, string matchClassPackage, Expression value, Expression startIndex)
        {
            Target = target;
            MatchClassName = matchClassName;
            Member = member;
            MatchClassPackage = matchClassPackage;
            Value = value;
            StartIndex = startIndex;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ArrayOfMatchClassTypeIndexOfBy(Target.Copy(renameSuffix), MatchClassName, Member, MatchClassPackage, Value.Copy(renameSuffix), StartIndex != null ? StartIndex.Copy(renameSuffix) : null);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFrontFormat("{0}ArrayHelper.Array_{1}_{2}_{3}(",
                "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(MatchClassPackage),
                MatchClassName, "indexOfBy", Member);
            Target.Emit(sourceCode);
            sourceCode.Append(", ");
            Value.Emit(sourceCode);
            if(StartIndex != null)
            {
                sourceCode.Append(", ");
                StartIndex.Emit(sourceCode);
            }
            sourceCode.AppendFrontFormat(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
            yield return Value;
            if(StartIndex != null)
                yield return StartIndex;
        }

        readonly Expression Target;
        readonly String MatchClassName;
        readonly String Member;
        readonly String MatchClassPackage;
        readonly Expression Value;
        readonly Expression StartIndex;
    }

    /// <summary>
    /// Class representing an array index of ordered expression.
    /// </summary>
    public class ArrayIndexOfOrdered : Expression
    {
        public ArrayIndexOfOrdered(Expression target, Expression value)
        {
            Target = target;
            Value = value;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ArrayIndexOfOrdered(Target.Copy(renameSuffix), Value.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.ContainerHelper.IndexOfOrdered(");
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

        readonly Expression Target;
        readonly Expression Value;
    }

    /// <summary>
    /// Class representing an array index of ordered by attribute expression.
    /// </summary>
    public class ArrayIndexOfOrderedBy : Expression
    {
        public ArrayIndexOfOrderedBy(Expression target, String ownerType, String member, Expression value)
        {
            Target = target;
            OwnerType = ownerType;
            Member = member;
            Value = value;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ArrayIndexOfOrderedBy(Target.Copy(renameSuffix), OwnerType, Member, Value.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFormat("GRGEN_MODEL.ArrayHelper_{0}_{1}.ArrayIndexOfOrderedBy(", OwnerType, Member);
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

        readonly Expression Target;
        readonly String OwnerType;
        readonly String Member;
        readonly Expression Value;
    }

    /// <summary>
    /// Class representing an array of action matches index of ordered by member expression.
    /// </summary>
    public class ArrayOfMatchTypeIndexOfOrderedBy : Expression
    {
        public ArrayOfMatchTypeIndexOfOrderedBy(Expression target, string patternName, string member, string rulePackage, Expression value)
        {
            Target = target;
            PatternName = patternName;
            Member = member;
            RulePackage = rulePackage;
            Value = value;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ArrayOfMatchTypeIndexOfOrderedBy(Target.Copy(renameSuffix), PatternName, Member, RulePackage, Value.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFrontFormat("{0}ArrayHelper.Array_{1}_{2}_{3}(",
                "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(RulePackage),
                PatternName, "indexOfOrderedBy", Member);
            Target.Emit(sourceCode);
            sourceCode.Append(", ");
            Value.Emit(sourceCode);
            sourceCode.AppendFrontFormat(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
            yield return Value;
        }

        readonly Expression Target;
        readonly String PatternName;
        readonly String Member;
        readonly String RulePackage;
        readonly Expression Value;
    }

    /// <summary>
    /// Class representing an array of iterated matches index of ordered by member expression.
    /// </summary>
    public class ArrayOfIteratedMatchTypeIndexOfOrderedBy : Expression
    {
        public ArrayOfIteratedMatchTypeIndexOfOrderedBy(Expression target, string patternName, string iteratedName, string member, string rulePackage, Expression value)
        {
            Target = target;
            PatternName = patternName;
            IteratedName = iteratedName;
            Member = member;
            RulePackage = rulePackage;
            Value = value;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ArrayOfIteratedMatchTypeIndexOfOrderedBy(Target.Copy(renameSuffix), PatternName, IteratedName, Member, RulePackage, Value.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFrontFormat("{0}ArrayHelper.Array_{1}_{2}_{3}_{4}(",
                "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(RulePackage),
                PatternName, IteratedName, "indexOfOrderedBy", Member);
            Target.Emit(sourceCode);
            sourceCode.Append(", ");
            Value.Emit(sourceCode);
            sourceCode.AppendFrontFormat(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
            yield return Value;
        }

        readonly Expression Target;
        readonly String PatternName;
        readonly String IteratedName;
        readonly String Member;
        readonly String RulePackage;
        readonly Expression Value;
    }

    /// <summary>
    /// Class representing an array of match class matches index of ordered by member expression.
    /// </summary>
    public class ArrayOfMatchClassTypeIndexOfOrderedBy : Expression
    {
        public ArrayOfMatchClassTypeIndexOfOrderedBy(Expression target, string matchClassName, string member, string matchClassPackage, Expression value)
        {
            Target = target;
            MatchClassName = matchClassName;
            Member = member;
            MatchClassPackage = matchClassPackage;
            Value = value;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ArrayOfMatchClassTypeIndexOfOrderedBy(Target.Copy(renameSuffix), MatchClassName, Member, MatchClassPackage, Value.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFrontFormat("{0}ArrayHelper.Array_{1}_{2}_{3}(",
                "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(MatchClassPackage),
                MatchClassName, "indexOfOrderedBy", Member);
            Target.Emit(sourceCode);
            sourceCode.Append(", ");
            Value.Emit(sourceCode);
            sourceCode.AppendFrontFormat(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
            yield return Value;
        }

        readonly Expression Target;
        readonly String MatchClassName;
        readonly String Member;
        readonly String MatchClassPackage;
        readonly Expression Value;
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

        public ArrayLastIndexOf(Expression target, Expression value, Expression startIndex)
        {
            Target = target;
            Value = value;
            StartIndex = startIndex;
        }

        public override Expression Copy(string renameSuffix)
        {
            if(StartIndex != null)
                return new ArrayLastIndexOf(Target.Copy(renameSuffix), Value.Copy(renameSuffix));
            else
                return new ArrayLastIndexOf(Target.Copy(renameSuffix), Value.Copy(renameSuffix), StartIndex);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.ContainerHelper.LastIndexOf(");
            Target.Emit(sourceCode);
            sourceCode.Append(", ");
            Value.Emit(sourceCode);
            if(StartIndex != null)
            {
                sourceCode.Append(", ");
                StartIndex.Emit(sourceCode);
            }
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
            yield return Value;
            if(StartIndex != null)
                yield return StartIndex;
        }

        readonly Expression Target;
        readonly Expression Value;
        readonly Expression StartIndex;
    }

    /// <summary>
    /// Class representing an array last index of by expression.
    /// </summary>
    public class ArrayLastIndexOfBy : Expression
    {
        public ArrayLastIndexOfBy(Expression target, string ownerType, string member, Expression value)
        {
            Target = target;
            OwnerType = ownerType;
            Member = member;
            Value = value;
        }

        public ArrayLastIndexOfBy(Expression target, string ownerType, string member, Expression value, Expression startIndex)
        {
            Target = target;
            OwnerType = ownerType;
            Member = member;
            Value = value;
            StartIndex = startIndex;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ArrayLastIndexOfBy(Target.Copy(renameSuffix), OwnerType, Member, Value.Copy(renameSuffix), StartIndex != null ? StartIndex.Copy(renameSuffix) : null);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFormat("GRGEN_MODEL.ArrayHelper_{0}_{1}.ArrayLastIndexOfBy(", OwnerType, Member);
            Target.Emit(sourceCode);
            sourceCode.Append(", ");
            Value.Emit(sourceCode);
            if(StartIndex != null)
            {
                sourceCode.Append(", ");
                StartIndex.Emit(sourceCode);
            }
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
            yield return Value;
            if(StartIndex != null)
                yield return StartIndex;
        }

        readonly Expression Target;
        readonly String OwnerType;
        readonly String Member;
        readonly Expression Value;
        readonly Expression StartIndex;
    }

    /// <summary>
    /// Class representing an array of action matches last index of by member expression.
    /// </summary>
    public class ArrayOfMatchTypeLastIndexOfBy : Expression
    {
        public ArrayOfMatchTypeLastIndexOfBy(Expression target, string patternName, string member, string rulePackage, Expression value)
        {
            Target = target;
            PatternName = patternName;
            Member = member;
            RulePackage = rulePackage;
            Value = value;
        }

        public ArrayOfMatchTypeLastIndexOfBy(Expression target, string patternName, string member, string rulePackage, Expression value, Expression startIndex)
        {
            Target = target;
            PatternName = patternName;
            Member = member;
            RulePackage = rulePackage;
            Value = value;
            StartIndex = startIndex;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ArrayOfMatchTypeLastIndexOfBy(Target.Copy(renameSuffix), PatternName, Member, RulePackage, Value.Copy(renameSuffix), StartIndex != null ? StartIndex.Copy(renameSuffix) : null);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFrontFormat("{0}ArrayHelper.Array_{1}_{2}_{3}(",
                "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(RulePackage),
                PatternName, "lastIndexOfBy", Member);
            Target.Emit(sourceCode);
            sourceCode.Append(", ");
            Value.Emit(sourceCode);
            if(StartIndex != null)
            {
                sourceCode.Append(", ");
                StartIndex.Emit(sourceCode);
            }
            sourceCode.AppendFrontFormat(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
            yield return Value;
            if(StartIndex != null)
                yield return StartIndex;
        }

        readonly Expression Target;
        readonly String PatternName;
        readonly String Member;
        readonly String RulePackage;
        readonly Expression Value;
        readonly Expression StartIndex;
    }

    /// <summary>
    /// Class representing an array of iterated matches last index of by member expression.
    /// </summary>
    public class ArrayOfIteratedMatchTypeLastIndexOfBy : Expression
    {
        public ArrayOfIteratedMatchTypeLastIndexOfBy(Expression target, string patternName, string iteratedName, string member, string rulePackage, Expression value)
        {
            Target = target;
            PatternName = patternName;
            IteratedName = iteratedName;
            Member = member;
            RulePackage = rulePackage;
            Value = value;
        }

        public ArrayOfIteratedMatchTypeLastIndexOfBy(Expression target, string patternName, string iteratedName, string member, string rulePackage, Expression value, Expression startIndex)
        {
            Target = target;
            PatternName = patternName;
            IteratedName = iteratedName;
            Member = member;
            RulePackage = rulePackage;
            Value = value;
            StartIndex = startIndex;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ArrayOfIteratedMatchTypeLastIndexOfBy(Target.Copy(renameSuffix), PatternName, IteratedName, Member, RulePackage, Value.Copy(renameSuffix), StartIndex != null ? StartIndex.Copy(renameSuffix) : null);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFrontFormat("{0}ArrayHelper.Array_{1}_{2}_{3}_{4}(",
                "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(RulePackage),
                PatternName, IteratedName, "lastIndexOfBy", Member);
            Target.Emit(sourceCode);
            sourceCode.Append(", ");
            Value.Emit(sourceCode);
            if(StartIndex != null)
            {
                sourceCode.Append(", ");
                StartIndex.Emit(sourceCode);
            }
            sourceCode.AppendFrontFormat(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
            yield return Value;
            if(StartIndex != null)
                yield return StartIndex;
        }

        readonly Expression Target;
        readonly String PatternName;
        readonly String IteratedName;
        readonly String Member;
        readonly String RulePackage;
        readonly Expression Value;
        readonly Expression StartIndex;
    }

    /// <summary>
    /// Class representing an array of match class matches last index of by member expression.
    /// </summary>
    public class ArrayOfMatchClassTypeLastIndexOfBy : Expression
    {
        public ArrayOfMatchClassTypeLastIndexOfBy(Expression target, string matchClassName, string member, string matchClassPackage, Expression value)
        {
            Target = target;
            MatchClassName = matchClassName;
            Member = member;
            MatchClassPackage = matchClassPackage;
            Value = value;
        }

        public ArrayOfMatchClassTypeLastIndexOfBy(Expression target, string matchClassName, string member, string matchClassPackage, Expression value, Expression startIndex)
        {
            Target = target;
            MatchClassName = matchClassName;
            Member = member;
            MatchClassPackage = matchClassPackage;
            Value = value;
            StartIndex = startIndex;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ArrayOfMatchClassTypeLastIndexOfBy(Target.Copy(renameSuffix), MatchClassName, Member, MatchClassPackage, Value.Copy(renameSuffix), StartIndex != null ? StartIndex.Copy(renameSuffix) : null);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFrontFormat("{0}ArrayHelper.Array_{1}_{2}_{3}(",
                "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(MatchClassPackage),
                MatchClassName, "lastIndexOfBy", Member);
            Target.Emit(sourceCode);
            sourceCode.Append(", ");
            Value.Emit(sourceCode);
            if(StartIndex != null)
            {
                sourceCode.Append(", ");
                StartIndex.Emit(sourceCode);
            }
            sourceCode.AppendFrontFormat(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
            yield return Value;
            if(StartIndex != null)
                yield return StartIndex;
        }

        readonly Expression Target;
        readonly String MatchClassName;
        readonly String Member;
        readonly String MatchClassPackage;
        readonly Expression Value;
        readonly Expression StartIndex;
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

        readonly Expression Target;
        readonly Expression Start;
        readonly Expression Length;
    }

    /// <summary>
    /// Class representing an array sort expression.
    /// </summary>
    public class ArrayOrder : Expression
    {
        public ArrayOrder(Expression target, bool ascending)
        {
            Target = target;
            Ascending = ascending;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ArrayOrder(Target.Copy(renameSuffix), Ascending);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFormat("GRGEN_LIBGR.ContainerHelper.ArrayOrder{0}(", Ascending ? "Ascending" : "Descending");
            Target.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
        }

        readonly Expression Target;
        readonly bool Ascending;
    }

    /// <summary>
    /// Class representing an array group expression.
    /// </summary>
    public class ArrayGroup : Expression
    {
        public ArrayGroup(Expression target)
        {
            Target = target;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ArrayGroup(Target.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.ContainerHelper.ArrayGroup(");
            Target.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
        }

        readonly Expression Target;
    }

    /// <summary>
    /// Class representing an array keep one for each expression.
    /// </summary>
    public class ArrayKeepOneForEach : Expression
    {
        public ArrayKeepOneForEach(Expression target)
        {
            Target = target;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ArrayKeepOneForEach(Target.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.ContainerHelper.ArrayKeepOneForEach(");
            Target.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
        }

        readonly Expression Target;
    }

    /// <summary>
    /// Class representing an array ordering expression.
    /// </summary>
    public abstract class ArrayOrderByBase : Expression
    {
        public ArrayOrderByBase(Expression target, OrderMethod orderMethod)
        {
            Target = target;
            OrderMethod = orderMethod;
        }

        public string GetOrderMethodString()
        {
            if(OrderMethod == OrderMethod.OrderAscending)
                return "orderAscendingBy";
            else if(OrderMethod == OrderMethod.OrderDescending)
                return "orderDescendingBy";
            else if(OrderMethod == OrderMethod.Group)
                return "groupBy";
            else
                return "keepOneForEachBy";
        }

        protected readonly Expression Target;
        protected readonly OrderMethod OrderMethod;
    }

    /// <summary>
    /// Class representing an array sort by expression.
    /// </summary>
    public class ArrayOrderBy : ArrayOrderByBase
    {
        public ArrayOrderBy(Expression target, string ownerType, string member, string typePackage, OrderMethod orderMethod)
            : base(target, orderMethod)
        {
            OwnerType = ownerType;
            Member = member;
            TypePackage = typePackage;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ArrayOrderBy(Target.Copy(renameSuffix), OwnerType, Member, TypePackage, OrderMethod);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFormat("{0}ArrayHelper_{1}_{2}.Array{3}(",
                "GRGEN_MODEL." + TypesHelper.GetPackagePrefixDot(TypePackage), 
                OwnerType, Member, OrderMethod.ToString() + "By");
            Target.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
        }

        readonly String OwnerType;
        readonly String Member;
        readonly String TypePackage;
    }

    /// <summary>
    /// Class representing an array of action matches sort by expression.
    /// </summary>
    public class ArrayOfMatchTypeOrderBy : ArrayOrderByBase
    {
        public ArrayOfMatchTypeOrderBy(Expression target, string patternName, string member, string rulePackage, OrderMethod orderMethod)
            : base(target, orderMethod)
        {
            PatternName = patternName;
            Member = member;
            RulePackage = rulePackage;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ArrayOfMatchTypeOrderBy(Target.Copy(renameSuffix), PatternName, Member, RulePackage, OrderMethod);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFrontFormat("{0}ArrayHelper.Array_{1}_{2}_{3}(",
                "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(RulePackage),
                PatternName, GetOrderMethodString(), Member);
            Target.Emit(sourceCode);
            sourceCode.AppendFrontFormat(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
        }

        readonly String PatternName;
        readonly String Member;
        readonly String RulePackage;
    }

    /// <summary>
    /// Class representing an array of iterated matches sort by expression.
    /// </summary>
    public class ArrayOfIteratedMatchTypeOrderBy : ArrayOrderByBase
    {
        public ArrayOfIteratedMatchTypeOrderBy(Expression target, string patternName, string iteratedName, string member, string rulePackage, OrderMethod orderMethod)
            : base(target, orderMethod)
        {
            PatternName = patternName;
            IteratedName = iteratedName;
            Member = member;
            RulePackage = rulePackage;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ArrayOfIteratedMatchTypeOrderBy(Target.Copy(renameSuffix), PatternName, IteratedName, Member, RulePackage, OrderMethod);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFrontFormat("{0}ArrayHelper.Array_{1}_{2}_{3}_{4}(",
                "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(RulePackage),
                PatternName, IteratedName, GetOrderMethodString(), Member);
            Target.Emit(sourceCode);
            sourceCode.AppendFrontFormat(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
        }

        readonly String PatternName;
        readonly String IteratedName;
        readonly String Member;
        readonly String RulePackage;
    }

    /// <summary>
    /// Class representing an array of match class matches sort by expression.
    /// </summary>
    public class ArrayOfMatchClassTypeOrderBy : ArrayOrderByBase
    {
        public ArrayOfMatchClassTypeOrderBy(Expression target, string matchClassName, string member, string matchClassPackage, OrderMethod orderMethod)
            : base(target, orderMethod)
        {
            MatchClassName = matchClassName;
            Member = member;
            MatchClassPackage = matchClassPackage;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ArrayOfMatchClassTypeOrderBy(Target.Copy(renameSuffix), MatchClassName, Member, MatchClassPackage, OrderMethod);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFrontFormat("{0}ArrayHelper.Array_{1}_{2}_{3}(",
                "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(MatchClassPackage),
                MatchClassName, GetOrderMethodString(), Member);
            Target.Emit(sourceCode);
            sourceCode.AppendFrontFormat(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
        }

        readonly String MatchClassName;
        readonly String Member;
        readonly String MatchClassPackage;
    }

    /// <summary>
    /// Class representing an array reverse expression.
    /// </summary>
    public class ArrayReverse : Expression
    {
        public ArrayReverse(Expression target)
        {
            Target = target;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ArrayReverse(Target.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.ContainerHelper.ArrayReverse(");
            Target.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
        }

        readonly Expression Target;
    }

    /// <summary>
    /// Class representing an array shuffle expression.
    /// </summary>
    public class ArrayShuffle : Expression
    {
        public ArrayShuffle(Expression target)
        {
            Target = target;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ArrayShuffle(Target.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.ContainerHelper.Shuffle(");
            Target.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
        }

        readonly Expression Target;
    }

    /// <summary>
    /// Class representing an array extract (from match type of rule) expression.
    /// </summary>
    public class ArrayExtract : Expression
    {
        public ArrayExtract(Expression target, string member, string ruleName, string packageName)
        {
            Target = target;
            Member = member;
            RuleName = ruleName;
            PackageName = packageName;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ArrayExtract(Target.Copy(renameSuffix), Member, RuleName, PackageName);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            string ruleClass = NamesOfEntities.RulePatternClassName(RuleName, PackageName, false);
            sourceCode.AppendFormat("GRGEN_ACTIONS.{0}.Extractor.Extract_{1}(", ruleClass, Member);
            Target.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
        }

        readonly Expression Target;
        readonly String Member;
        readonly String RuleName;
        readonly String PackageName;
    }

    /// <summary>
    /// Class representing an array map expression.
    /// </summary>
    public class ArrayMap : Expression
    {
        public ArrayMap(Expression source, String sourceType,
            String arrayAccessVariable, String indexVariable, String elementVariable, Expression mapping, String targetType,
            PatternNode[] patternNodes, PatternEdge[] patternEdges, PatternVariable[] patternVariables)
        {
            Source = source;
            SourceType = sourceType;
            ArrayAccessVariable = arrayAccessVariable;
            IndexVariable = indexVariable;
            ElementVariable = elementVariable;
            Mapping = mapping;
            TargetType = targetType;
            PatternNodes = patternNodes;
            PatternEdges = patternEdges;
            PatternVariables = patternVariables;
        }

        public override Expression Copy(string renameSuffix)
        {
            PatternNode[] newPatternNodes = new PatternNode[PatternNodes.Length];
            for(int i = 0; i < PatternNodes.Length; ++i)
            {
                newPatternNodes[i] = new PatternNode(PatternNodes[i], renameSuffix);
            }
            PatternEdge[] newPatternEdges = new PatternEdge[PatternEdges.Length];
            for(int i = 0; i < PatternEdges.Length; ++i)
            {
                newPatternEdges[i] = new PatternEdge(PatternEdges[i], renameSuffix);
            }
            PatternVariable[] newPatternVariables = new PatternVariable[PatternVariables.Length];
            for(int i = 0; i < PatternVariables.Length; ++i)
            {
                newPatternVariables[i] = new PatternVariable(PatternVariables[i], renameSuffix);
            }
            return new ArrayMap(Source.Copy(renameSuffix), SourceType,
                ArrayAccessVariable, IndexVariable, ElementVariable, Mapping.Copy(renameSuffix), TargetType,
                newPatternNodes, newPatternEdges, newPatternVariables);
        }

        public override void EmitLambdaExpressionImplementationMethods(SourceBuilder sourceCode)
        {
            base.EmitLambdaExpressionImplementationMethods(sourceCode);

            SourceBuilder sb = new SourceBuilder();
            sb.Indent();
            sb.Indent();

            string sourceVar = "source";
            string targetVar = "target";
            string resultVar = "result";

            string SourceArrayType = "List<" + SourceType + ">";
            string TargetArrayType = "List<" + TargetType + ">";
            string ArrayMapMethodName = "ArrayMap_" + "expr" + Id;
            sb.AppendFrontFormat("static {0} {1}(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv", TargetArrayType, ArrayMapMethodName);
            sb.Append(", ");
            sb.AppendFormat("{0} {1}", SourceArrayType, sourceVar);
            foreach(PatternNode patternNode in PatternNodes)
            {
                sb.Append(", ");
                sb.Append(TypesHelper.TypeName(patternNode.type));
                sb.Append(" ");
                sb.Append(NamesOfEntities.CandidateVariable(patternNode.name));
            }
            foreach(PatternEdge patternEdge in PatternEdges)
            {
                sb.Append(", ");
                sb.Append(TypesHelper.TypeName(patternEdge.type));
                sb.Append(" ");
                sb.Append(NamesOfEntities.CandidateVariable(patternEdge.name));
            }
            foreach(PatternVariable patternVariable in PatternVariables)
            {
                sb.Append(", ");
                sb.Append(TypesHelper.TypeName(patternVariable.type));
                sb.Append(" ");
                sb.Append(NamesOfEntities.Variable(patternVariable.name));
            }
            if(Parallel)
                sb.Append(", int threadId");
            sb.Append(")\n");
            sb.AppendFront("{\n");
            sb.Indent();

            sb.AppendFront("GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;\n");
            sb.AppendFrontFormat("{0} {1} = new {0}();\n", TargetArrayType, targetVar);

            if(ArrayAccessVariable != null)
                sb.AppendFrontFormat("{0} {1} = {2};\n", SourceArrayType, NamesOfEntities.Variable(ArrayAccessVariable), sourceVar);

            string indexVar = IndexVariable != null ? NamesOfEntities.Variable(IndexVariable) : "index";
            sb.AppendFrontFormat("for(int {0} = 0; {0} < {1}.Count; ++{0})\n", indexVar, sourceVar);
            sb.AppendFront("{\n");
            sb.Indent();

            sb.AppendFrontFormat("{0} {1} = {2}[{3}];\n", SourceType, NamesOfEntities.Variable(ElementVariable), sourceVar, indexVar);
            sb.AppendFrontFormat("{0} {1} = ", TargetType, resultVar);

            Mapping.Emit(sb);

            sb.Append(";\n");
            sb.AppendFrontFormat("{0}.Add({1});\n", targetVar, resultVar);

            sb.Unindent();
            sb.AppendFront("}\n");

            sb.AppendFrontFormat("return {0};\n", targetVar);

            sb.Unindent();
            sb.AppendFront("}\n");

            sourceCode.Append(sb.ToString());
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            string ArrayMapMethodName = "ArrayMap_" + "expr" + Id;
            sourceCode.AppendFormat("{0}(actionEnv", ArrayMapMethodName);
            sourceCode.Append(", ");
            Source.Emit(sourceCode);
            foreach(PatternNode patternNode in PatternNodes)
            {
                sourceCode.Append(", (");
                sourceCode.Append(TypesHelper.TypeName(patternNode.type));
                sourceCode.Append(")");
                sourceCode.Append(NamesOfEntities.CandidateVariable(patternNode.name));
            }
            foreach(PatternEdge patternEdge in PatternEdges)
            {
                sourceCode.Append(", (");
                sourceCode.Append(TypesHelper.TypeName(patternEdge.type));
                sourceCode.Append(")");
                sourceCode.Append(NamesOfEntities.CandidateVariable(patternEdge.name));
            }
            foreach(PatternVariable patternVariable in PatternVariables)
            {
                sourceCode.Append(", (");
                sourceCode.Append(TypesHelper.TypeName(patternVariable.type));
                sourceCode.Append(")");
                sourceCode.Append(NamesOfEntities.Variable(patternVariable.name));
            }
            if(Parallel)
                sourceCode.Append(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Source;
            yield return Mapping;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        readonly Expression Source;
        readonly String SourceType;
        readonly String ArrayAccessVariable;
        readonly String IndexVariable;
        readonly String ElementVariable;
        readonly Expression Mapping;
        readonly String TargetType;
        readonly PatternNode[] PatternNodes;
        readonly PatternEdge[] PatternEdges;
        readonly PatternVariable[] PatternVariables;
        bool Parallel;
    }

    /// <summary>
    /// Class representing an array removeIf expression.
    /// </summary>
    public class ArrayRemoveIf : Expression
    {
        public ArrayRemoveIf(Expression source, String sourceType,
            String arrayAccessVariable, String indexVariable, String elementVariable, Expression condition, String targetType,
            PatternNode[] patternNodes, PatternEdge[] patternEdges, PatternVariable[] patternVariables)
        {
            Source = source;
            SourceType = sourceType;
            ArrayAccessVariable = arrayAccessVariable;
            IndexVariable = indexVariable;
            ElementVariable = elementVariable;
            Condition = condition;
            TargetType = targetType;
            PatternNodes = patternNodes;
            PatternEdges = patternEdges;
            PatternVariables = patternVariables;
        }

        public override Expression Copy(string renameSuffix)
        {
            PatternNode[] newPatternNodes = new PatternNode[PatternNodes.Length];
            for(int i = 0; i < PatternNodes.Length; ++i)
            {
                newPatternNodes[i] = new PatternNode(PatternNodes[i], renameSuffix);
            }
            PatternEdge[] newPatternEdges = new PatternEdge[PatternEdges.Length];
            for(int i = 0; i < PatternEdges.Length; ++i)
            {
                newPatternEdges[i] = new PatternEdge(PatternEdges[i], renameSuffix);
            }
            PatternVariable[] newPatternVariables = new PatternVariable[PatternVariables.Length];
            for(int i = 0; i < PatternVariables.Length; ++i)
            {
                newPatternVariables[i] = new PatternVariable(PatternVariables[i], renameSuffix);
            }
            return new ArrayRemoveIf(Source.Copy(renameSuffix), SourceType,
                ArrayAccessVariable, IndexVariable, ElementVariable, Condition.Copy(renameSuffix), TargetType,
                newPatternNodes, newPatternEdges, newPatternVariables);
        }

        public override void EmitLambdaExpressionImplementationMethods(SourceBuilder sourceCode)
        {
            base.EmitLambdaExpressionImplementationMethods(sourceCode);

            SourceBuilder sb = new SourceBuilder();
            sb.Indent();
            sb.Indent();

            string sourceVar = "source";
            string targetVar = "target";

            string SourceArrayType = "List<" + SourceType + ">";
            string TargetArrayType = "List<" + TargetType + ">";
            string ArrayRemoveIfMethodName = "ArrayRemoveIf_" + "expr" + Id;
            sb.AppendFrontFormat("static {0} {1}(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv", TargetArrayType, ArrayRemoveIfMethodName);
            sb.Append(", ");
            sb.AppendFormat("{0} {1}", SourceArrayType, sourceVar);
            foreach(PatternNode patternNode in PatternNodes)
            {
                sb.Append(", ");
                sb.Append(TypesHelper.TypeName(patternNode.type));
                sb.Append(" ");
                sb.Append(NamesOfEntities.CandidateVariable(patternNode.name));
            }
            foreach(PatternEdge patternEdge in PatternEdges)
            {
                sb.Append(", ");
                sb.Append(TypesHelper.TypeName(patternEdge.type));
                sb.Append(" ");
                sb.Append(NamesOfEntities.CandidateVariable(patternEdge.name));
            }
            foreach(PatternVariable patternVariable in PatternVariables)
            {
                sb.Append(", ");
                sb.Append(TypesHelper.TypeName(patternVariable.type));
                sb.Append(" ");
                sb.Append(NamesOfEntities.Variable(patternVariable.name));
            }
            if(Parallel)
                sb.Append(", int threadId");
            sb.Append(")\n");
            sb.AppendFront("{\n");
            sb.Indent();

            sb.AppendFront("GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;\n");
            sb.AppendFrontFormat("{0} {1} = new {0}();\n", TargetArrayType, targetVar);

            if(ArrayAccessVariable != null)
                sb.AppendFrontFormat("{0} {1} = {2};\n", SourceArrayType, NamesOfEntities.Variable(ArrayAccessVariable), sourceVar);

            String indexVar = IndexVariable != null ? NamesOfEntities.Variable(IndexVariable) : "index";
            sb.AppendFrontFormat("for(int {0} = 0; {0} < {1}.Count; ++{0})\n", indexVar, sourceVar);
            sb.AppendFront("{\n");
            sb.Indent();

            sb.AppendFrontFormat("{0} {1} = {2}[{3}];\n", SourceType, NamesOfEntities.Variable(ElementVariable), sourceVar, indexVar);
            sb.AppendFront("if(!(bool)(");

            Condition.Emit(sb);

            sb.Append("))\n");
            sb.AppendFrontIndentedFormat("{0}.Add({1}[{2}]);\n", targetVar, sourceVar, indexVar);

            sb.Unindent();
            sb.AppendFront("}\n");

            sb.AppendFrontFormat("return {0};\n", targetVar);

            sb.Unindent();
            sb.AppendFront("}\n");

            sourceCode.Append(sb.ToString());
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            string ArrayRemoveIfMethodName = "ArrayRemoveIf_" + "expr" + Id;
            sourceCode.AppendFormat("{0}(actionEnv", ArrayRemoveIfMethodName);
            sourceCode.Append(", ");
            Source.Emit(sourceCode);
            foreach(PatternNode patternNode in PatternNodes)
            {
                sourceCode.Append(", (");
                sourceCode.Append(TypesHelper.TypeName(patternNode.type));
                sourceCode.Append(")");
                sourceCode.Append(NamesOfEntities.CandidateVariable(patternNode.name));
            }
            foreach(PatternEdge patternEdge in PatternEdges)
            {
                sourceCode.Append(", (");
                sourceCode.Append(TypesHelper.TypeName(patternEdge.type));
                sourceCode.Append(")");
                sourceCode.Append(NamesOfEntities.CandidateVariable(patternEdge.name));
            }
            foreach(PatternVariable patternVariable in PatternVariables)
            {
                sourceCode.Append(", (");
                sourceCode.Append(TypesHelper.TypeName(patternVariable.type));
                sourceCode.Append(")");
                sourceCode.Append(NamesOfEntities.Variable(patternVariable.name));
            }
            if(Parallel)
                sourceCode.Append(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Source;
            yield return Condition;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        readonly Expression Source;
        readonly String SourceType;
        readonly String ArrayAccessVariable;
        readonly String IndexVariable;
        readonly String ElementVariable;
        readonly Expression Condition;
        readonly String TargetType;
        readonly PatternNode[] PatternNodes;
        readonly PatternEdge[] PatternEdges;
        readonly PatternVariable[] PatternVariables;
        public bool Parallel;
    }

    /// <summary>
    /// Class representing an array map start with accumulate by expression.
    /// </summary>
    public class ArrayMapStartWithAccumulateBy : Expression
    {
        public ArrayMapStartWithAccumulateBy(Expression source, String sourceType,
            String initArrayAccessVariable, Expression init,
            String arrayAccessVariable, String previousAccumulationAccessVariable, String indexVariable, String elementVariable,
            Expression mapping, String targetType,
            PatternNode[] patternNodes, PatternEdge[] patternEdges, PatternVariable[] patternVariables)
        {
            Source = source;
            SourceType = sourceType;
            InitArrayAccessVariable = initArrayAccessVariable;
            Init = init;
            ArrayAccessVariable = arrayAccessVariable;
            PreviousAccumulationAccessVariable = previousAccumulationAccessVariable;
            IndexVariable = indexVariable;
            ElementVariable = elementVariable;
            Mapping = mapping;
            TargetType = targetType;
            PatternNodes = patternNodes;
            PatternEdges = patternEdges;
            PatternVariables = patternVariables;
        }

        public override Expression Copy(string renameSuffix)
        {
            PatternNode[] newPatternNodes = new PatternNode[PatternNodes.Length];
            for(int i = 0; i < PatternNodes.Length; ++i)
            {
                newPatternNodes[i] = new PatternNode(PatternNodes[i], renameSuffix);
            }
            PatternEdge[] newPatternEdges = new PatternEdge[PatternEdges.Length];
            for(int i = 0; i < PatternEdges.Length; ++i)
            {
                newPatternEdges[i] = new PatternEdge(PatternEdges[i], renameSuffix);
            }
            PatternVariable[] newPatternVariables = new PatternVariable[PatternVariables.Length];
            for(int i = 0; i < PatternVariables.Length; ++i)
            {
                newPatternVariables[i] = new PatternVariable(PatternVariables[i], renameSuffix);
            }
            return new ArrayMapStartWithAccumulateBy(Source.Copy(renameSuffix), SourceType,
                InitArrayAccessVariable, Init.Copy(renameSuffix),
                ArrayAccessVariable, PreviousAccumulationAccessVariable, IndexVariable, ElementVariable, Mapping.Copy(renameSuffix), TargetType,
                newPatternNodes, newPatternEdges, newPatternVariables);
        }

        public override void EmitLambdaExpressionImplementationMethods(SourceBuilder sourceCode)
        {
            base.EmitLambdaExpressionImplementationMethods(sourceCode);

            SourceBuilder sb = new SourceBuilder();
            sb.Indent();
            sb.Indent();

            string sourceVar = "source";
            string targetVar = "target";
            string resultVar = "result";

            string SourceArrayType = "List<" + SourceType + ">";
            string TargetArrayType = "List<" + TargetType + ">";
            string ArrayMapMethodName = "ArrayMapStartWithAccumulateBy_" + "expr" + Id;
            sb.AppendFrontFormat("static {0} {1}(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv", TargetArrayType, ArrayMapMethodName);
            sb.Append(", ");
            sb.AppendFormat("{0} {1}", SourceArrayType, sourceVar);
            foreach(PatternNode patternNode in PatternNodes)
            {
                sb.Append(", ");
                sb.Append(TypesHelper.TypeName(patternNode.type));
                sb.Append(" ");
                sb.Append(NamesOfEntities.CandidateVariable(patternNode.name));
            }
            foreach(PatternEdge patternEdge in PatternEdges)
            {
                sb.Append(", ");
                sb.Append(TypesHelper.TypeName(patternEdge.type));
                sb.Append(" ");
                sb.Append(NamesOfEntities.CandidateVariable(patternEdge.name));
            }
            foreach(PatternVariable patternVariable in PatternVariables)
            {
                sb.Append(", ");
                sb.Append(TypesHelper.TypeName(patternVariable.type));
                sb.Append(" ");
                sb.Append(NamesOfEntities.Variable(patternVariable.name));
            }
            if(Parallel)
                sb.Append(", int threadId");
            sb.Append(")\n");
            sb.AppendFront("{\n");
            sb.Indent();

            sb.AppendFront("GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;\n");
            sb.AppendFrontFormat("{0} {1} = new {0}();\n", TargetArrayType, targetVar);

            if(InitArrayAccessVariable != null)
                sb.AppendFrontFormat("{0} {1} = {2};\n", SourceArrayType, NamesOfEntities.Variable(InitArrayAccessVariable), sourceVar);

            sb.AppendFront(TargetType + " " + NamesOfEntities.Variable(PreviousAccumulationAccessVariable) + " = ");
            Init.Emit(sb);
            sb.AppendFront(";\n");

            if(ArrayAccessVariable != null)
                sb.AppendFrontFormat("{0} {1} = {2};\n", SourceArrayType, NamesOfEntities.Variable(ArrayAccessVariable), sourceVar);

            String indexVar = IndexVariable != null ? NamesOfEntities.Variable(IndexVariable) : "index";
            sb.AppendFrontFormat("for(int {0} = 0; {0} < {1}.Count; ++{0})\n", indexVar, sourceVar);
            sb.AppendFront("{\n");
            sb.Indent();

            sb.AppendFrontFormat("{0} {1} = {2}[{3}];\n", SourceType, NamesOfEntities.Variable(ElementVariable), sourceVar, indexVar);
            sb.AppendFrontFormat("{0} {1} = ", TargetType, resultVar);

            Mapping.Emit(sb);

            sb.Append(";\n");
            sb.AppendFrontFormat("{0}.Add({1});\n", targetVar, resultVar);

            sb.AppendFrontFormat("{0} = {1};\n", NamesOfEntities.Variable(PreviousAccumulationAccessVariable), resultVar);

            sb.Unindent();
            sb.AppendFront("}\n");

            sb.AppendFrontFormat("return {0};\n", targetVar);

            sb.Unindent();
            sb.AppendFront("}\n");

            sourceCode.Append(sb.ToString());
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            string ArrayMapMethodName = "ArrayMapStartWithAccumulateBy_" + "expr" + Id;
            sourceCode.AppendFormat("{0}(actionEnv", ArrayMapMethodName);
            sourceCode.Append(", ");
            Source.Emit(sourceCode);
            foreach(PatternNode patternNode in PatternNodes)
            {
                sourceCode.Append(", (");
                sourceCode.Append(TypesHelper.TypeName(patternNode.type));
                sourceCode.Append(")");
                sourceCode.Append(NamesOfEntities.CandidateVariable(patternNode.name));
            }
            foreach(PatternEdge patternEdge in PatternEdges)
            {
                sourceCode.Append(", (");
                sourceCode.Append(TypesHelper.TypeName(patternEdge.type));
                sourceCode.Append(")");
                sourceCode.Append(NamesOfEntities.CandidateVariable(patternEdge.name));
            }
            foreach(PatternVariable patternVariable in PatternVariables)
            {
                sourceCode.Append(", (");
                sourceCode.Append(TypesHelper.TypeName(patternVariable.type));
                sourceCode.Append(")");
                sourceCode.Append(NamesOfEntities.Variable(patternVariable.name));
            }
            if(Parallel)
                sourceCode.Append(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Source;
            yield return Init;
            yield return Mapping;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        readonly Expression Source;
        readonly String SourceType;
        readonly String InitArrayAccessVariable;
        readonly Expression Init;
        readonly String ArrayAccessVariable;
        readonly String PreviousAccumulationAccessVariable;
        readonly String IndexVariable;
        readonly String ElementVariable;
        readonly Expression Mapping;
        readonly String TargetType;
        readonly PatternNode[] PatternNodes;
        readonly PatternEdge[] PatternEdges;
        readonly PatternVariable[] PatternVariables;
        bool Parallel;
    }

    /// <summary>
    /// Class representing an array extract (from match type of rule iterated) expression.
    /// </summary>
    public class ArrayExtractIterated : Expression
    {
        public ArrayExtractIterated(Expression target, string member, string ruleName, string iteratedName, string packageName)
        {
            Target = target;
            Member = member;
            RuleName = ruleName;
            IteratedName = iteratedName;
            PackageName = packageName;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ArrayExtractIterated(Target.Copy(renameSuffix), Member, RuleName, IteratedName, PackageName);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            string ruleClass = NamesOfEntities.RulePatternClassName(RuleName, PackageName, false);
            sourceCode.AppendFormat("GRGEN_ACTIONS.{0}.Extractor_{1}.Extract_{2}(", ruleClass, IteratedName, Member);
            Target.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
        }

        readonly Expression Target;
        readonly String Member;
        readonly String RuleName;
        readonly String IteratedName;
        readonly String PackageName;
    }

    /// <summary>
    /// Class representing an array extract (from match class) expression.
    /// </summary>
    public class ArrayExtractMatchClass : Expression
    {
        public ArrayExtractMatchClass(Expression target, string member, string matchClassName, string packageName)
        {
            Target = target;
            Member = member;
            MatchClassName = matchClassName;
            PackageName = packageName;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ArrayExtractMatchClass(Target.Copy(renameSuffix), Member, MatchClassName, PackageName);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            string matchClass = NamesOfEntities.MatchClassName(MatchClassName, PackageName);
            sourceCode.AppendFormat("GRGEN_ACTIONS.{0}.Extractor.Extract_{1}(", matchClass, Member);
            Target.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
        }

        readonly Expression Target;
        readonly String Member;
        readonly String MatchClassName;
        readonly String PackageName;
    }

    /// <summary>
    /// Class representing an array extract from node/edge type expression.
    /// </summary>
    public class ArrayExtractGraphElementType : Expression
    {
        public ArrayExtractGraphElementType(Expression target, string member, string graphElementTypeName, string packageName)
        {
            Target = target;
            Member = member;
            GraphElementTypeName = graphElementTypeName;
            PackageName = packageName;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ArrayExtractGraphElementType(Target.Copy(renameSuffix), Member, GraphElementTypeName, PackageName);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            string arrayHelperClassName = NamesOfEntities.ArrayHelperClassName(GraphElementTypeName, PackageName, Member);
            sourceCode.AppendFormat("GRGEN_MODEL.{0}.Extract(", arrayHelperClassName);
            Target.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
        }

        readonly Expression Target;
        readonly String Member;
        readonly String GraphElementTypeName;
        readonly String PackageName;
    }

    /// <summary>
    /// Class representing an array as set expression.
    /// </summary>
    public class ArrayAsSet : Expression
    {
        public ArrayAsSet(Expression target)
        {
            Target = target;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ArrayAsSet(Target.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.ContainerHelper.ArrayAsSet(");
            Target.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
        }

        readonly Expression Target;
    }

    /// <summary>
    /// Class representing an array as map expression.
    /// </summary>
    public class ArrayAsMap : Expression
    {
        public ArrayAsMap(Expression target)
        {
            Target = target;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ArrayAsMap(Target.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.ContainerHelper.ArrayAsMap(");
            Target.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
        }

        readonly Expression Target;
    }

    /// <summary>
    /// Class representing an array as deque expression.
    /// </summary>
    public class ArrayAsDeque : Expression
    {
        public ArrayAsDeque(Expression target)
        {
            Target = target;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ArrayAsDeque(Target.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.ContainerHelper.ArrayAsDeque(");
            Target.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
        }

        readonly Expression Target;
    }

    /// <summary>
    /// Class representing an array as string expression.
    /// </summary>
    public class ArrayAsString : Expression
    {
        public ArrayAsString(Expression target, Expression value)
        {
            Target = target;
            Value = value;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ArrayAsString(Target.Copy(renameSuffix), Value.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.ContainerHelper.ArrayAsString(");
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

        readonly Expression Target;
        readonly Expression Value;
    }

    /// <summary>
    /// Class representing an array sum expression.
    /// </summary>
    public class ArraySum : Expression
    {
        public ArraySum(Expression target)
        {
            Target = target;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ArraySum(Target.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.ContainerHelper.Sum(");
            Target.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
        }

        readonly Expression Target;
    }

    /// <summary>
    /// Class representing an array prod(uct) expression.
    /// </summary>
    public class ArrayProd : Expression
    {
        public ArrayProd(Expression target)
        {
            Target = target;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ArrayProd(Target.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.ContainerHelper.Prod(");
            Target.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
        }

        readonly Expression Target;
    }

    /// <summary>
    /// Class representing an array min(imum) expression.
    /// </summary>
    public class ArrayMin : Expression
    {
        public ArrayMin(Expression target)
        {
            Target = target;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ArrayMin(Target.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.ContainerHelper.Min(");
            Target.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
        }

        readonly Expression Target;
    }

    /// <summary>
    /// Class representing an array max(imum) expression.
    /// </summary>
    public class ArrayMax : Expression
    {
        public ArrayMax(Expression target)
        {
            Target = target;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ArrayMax(Target.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.ContainerHelper.Max(");
            Target.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
        }

        readonly Expression Target;
    }

    /// <summary>
    /// Class representing an array avg (average/mean) expression.
    /// </summary>
    public class ArrayAvg : Expression
    {
        public ArrayAvg(Expression target)
        {
            Target = target;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ArrayAvg(Target.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.ContainerHelper.Avg(");
            Target.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
        }

        readonly Expression Target;
    }

    /// <summary>
    /// Class representing an array med(ian) (on already sorted array) expression.
    /// </summary>
    public class ArrayMed : Expression
    {
        public ArrayMed(Expression target)
        {
            Target = target;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ArrayMed(Target.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.ContainerHelper.Med(");
            Target.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
        }

        readonly Expression Target;
    }

    /// <summary>
    /// Class representing an array medUnordered (median on array not ordered) expression.
    /// </summary>
    public class ArrayMedUnordered : Expression
    {
        public ArrayMedUnordered(Expression target)
        {
            Target = target;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ArrayMedUnordered(Target.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.ContainerHelper.MedUnordered(");
            Target.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
        }

        readonly Expression Target;
    }

    /// <summary>
    /// Class representing an array var(iance) expression.
    /// </summary>
    public class ArrayVar : Expression
    {
        public ArrayVar(Expression target)
        {
            Target = target;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ArrayVar(Target.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.ContainerHelper.Var(");
            Target.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
        }

        readonly Expression Target;
    }

    /// <summary>
    /// Class representing an array (standard) dev(iation) expression.
    /// </summary>
    public class ArrayDev : Expression
    {
        public ArrayDev(Expression target)
        {
            Target = target;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ArrayDev(Target.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.ContainerHelper.Dev(");
            Target.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
        }

        readonly Expression Target;
    }

    /// <summary>
    /// Class representing an array and expression.
    /// </summary>
    public class ArrayAnd : Expression
    {
        public ArrayAnd(Expression target)
        {
            Target = target;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ArrayAnd(Target.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.ContainerHelper.And(");
            Target.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
        }

        readonly Expression Target;
    }

    /// <summary>
    /// Class representing an array or expression.
    /// </summary>
    public class ArrayOr : Expression
    {
        public ArrayOr(Expression target)
        {
            Target = target;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ArrayOr(Target.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.ContainerHelper.Or(");
            Target.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
        }

        readonly Expression Target;
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

        readonly Expression Target;
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

        readonly Expression Target;
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
            sourceCode.Append("GRGEN_LIBGR.ContainerHelper.Peek(");
            Target.Emit(sourceCode);
            if(Number != null)
            {
                sourceCode.Append(", ");
                Number.Emit(sourceCode);
            }
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
            if(Number!=null)
                yield return Number;
            else yield break;
        }

        readonly Expression Target;
        readonly Expression Number;
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

        public DequeIndexOf(Expression target, Expression value, Expression startIndex)
        {
            Target = target;
            Value = value;
            StartIndex = startIndex;
        }

        public override Expression Copy(string renameSuffix)
        {
            if(StartIndex!=null)
                return new DequeIndexOf(Target.Copy(renameSuffix), Value.Copy(renameSuffix), StartIndex.Copy(renameSuffix));
            else
                return new DequeIndexOf(Target.Copy(renameSuffix), Value.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.ContainerHelper.IndexOf(");
            Target.Emit(sourceCode);
            sourceCode.Append(", ");
            Value.Emit(sourceCode);
            if(StartIndex != null)
            {
                sourceCode.Append(", ");
                StartIndex.Emit(sourceCode);
            }
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
            yield return Value;
            if(StartIndex != null)
                yield return StartIndex;
        }

        readonly Expression Target;
        readonly Expression Value;
        readonly Expression StartIndex;
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

        public DequeLastIndexOf(Expression target, Expression value, Expression startIndex)
        {
            Target = target;
            Value = value;
            StartIndex = startIndex;
        }

        public override Expression Copy(string renameSuffix)
        {
            if(StartIndex != null)
                return new DequeLastIndexOf(Target.Copy(renameSuffix), Value.Copy(renameSuffix), StartIndex.Copy(renameSuffix));
            else
                return new DequeLastIndexOf(Target.Copy(renameSuffix), Value.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.ContainerHelper.LastIndexOf(");
            Target.Emit(sourceCode);
            sourceCode.Append(", ");
            Value.Emit(sourceCode);
            if(StartIndex != null)
            {
                sourceCode.Append(", ");
                StartIndex.Emit(sourceCode);
            }
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
            yield return Value;
            if(StartIndex != null)
                yield return StartIndex;
        }

        readonly Expression Target;
        readonly Expression Value;
        readonly Expression StartIndex;
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

        readonly Expression Target;
        readonly Expression Start;
        readonly Expression Length;
    }

    /// <summary>
    /// Class representing a deque as set expression.
    /// </summary>
    public class DequeAsSet : Expression
    {
        public DequeAsSet(Expression target)
        {
            Target = target;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new DequeAsSet(Target.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.ContainerHelper.DequeAsSet(");
            Target.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
        }

        readonly Expression Target;
    }

    /// <summary>
    /// Class representing a deque as array expression.
    /// </summary>
    public class DequeAsArray : Expression
    {
        public DequeAsArray(Expression target)
        {
            Target = target;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new DequeAsArray(Target.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.ContainerHelper.DequeAsArray(");
            Target.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Target;
        }

        readonly Expression Target;
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

        readonly String ClassName;
        readonly String MapName;
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

        readonly String ClassName;
        readonly String SetName;
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

        readonly String ClassName;
        readonly String ArrayName;
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

        readonly String ClassName;
        readonly String DequeName;
    }

    public class MapCopyConstructor : Expression
    {
        public MapCopyConstructor(String mapType, String mapKeyType, String mapValueType, Expression sourceMap)
        {
            MapType = mapType;
            MapKeyType = mapKeyType;
            MapValueType = mapValueType;
            SourceMap = sourceMap;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new MapCopyConstructor(MapType, MapKeyType, MapValueType, SourceMap.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.ContainerHelper.FillMap(new ");
            sourceCode.Append(MapType);
            sourceCode.Append("(), \"");
            sourceCode.Append(MapKeyType);
            sourceCode.Append("\", \"");
            sourceCode.Append(MapValueType);
            sourceCode.Append("\", ");
            SourceMap.Emit(sourceCode);
            sourceCode.Append(", graph.Model)");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return SourceMap;
        }

        readonly String MapType;
        readonly String MapKeyType;
        readonly String MapValueType;
        readonly Expression SourceMap;
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
            if(First!=null)
                First.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            if(First!=null)
                yield return First;
        }

        readonly String ClassName;
        readonly String MapName;
        readonly MapItem First;
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

        readonly Expression Key;
        readonly String KeyType;
        readonly Expression Value;
        readonly String ValueType;
        readonly MapItem Next;
    }

    public class SetCopyConstructor : Expression
    {
        public SetCopyConstructor(String setType, String setValueType, Expression sourceSet)
        {
            SetType = setType;
            SetValueType = setValueType;
            SourceSet = sourceSet;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new SetCopyConstructor(SetType, SetValueType, SourceSet.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.ContainerHelper.FillSet(new ");
            sourceCode.Append(SetType);
            sourceCode.Append("(), \"");
            sourceCode.Append(SetValueType);
            sourceCode.Append("\", ");
            SourceSet.Emit(sourceCode);
            sourceCode.Append(", graph.Model)");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return SourceSet;
        }

        readonly String SetType;
        readonly String SetValueType;
        readonly Expression SourceSet;
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
            if(First!=null)
                First.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            if(First!=null) yield return First;
        }

        readonly String ClassName;
        readonly String SetName;
        readonly SetItem First;
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

            if(Next != null)
            {
                sourceCode.Append(", ");
                Next.Emit(sourceCode);
            }
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Value;
            if(Next!=null)
                yield return Next;
        }

        readonly Expression Value;
        readonly String ValueType;
        readonly SetItem Next;
    }

    public class ArrayCopyConstructor : Expression
    {
        public ArrayCopyConstructor(String arrayType, String arrayValueType, Expression sourceArray)
        {
            ArrayType = arrayType;
            ArrayValueType = arrayValueType;
            SourceArray = sourceArray;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ArrayCopyConstructor(ArrayType, ArrayValueType, SourceArray.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.ContainerHelper.FillArray(new ");
            sourceCode.Append(ArrayType);
            sourceCode.Append("(), \"");
            sourceCode.Append(ArrayValueType);
            sourceCode.Append("\", ");
            SourceArray.Emit(sourceCode);
            sourceCode.Append(", graph.Model)");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return SourceArray;
        }

        readonly String ArrayType;
        readonly String ArrayValueType;
        readonly Expression SourceArray;
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
            if(First!=null)
                First.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            if(First!=null)
                yield return First;
        }

        readonly String ClassName;
        readonly String ArrayName;
        readonly ArrayItem First;
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
            if(Next!=null)
                yield return Next;
        }

        readonly Expression Value;
        readonly String ValueType;
        readonly ArrayItem Next;
    }

    public class DequeCopyConstructor : Expression
    {
        public DequeCopyConstructor(String dequeType, String dequeValueType, Expression sourceDeque)
        {
            DequeType = dequeType;
            DequeValueType = dequeValueType;
            SourceDeque = sourceDeque;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new DequeCopyConstructor(DequeType, DequeValueType, SourceDeque.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.ContainerHelper.FillDeque(new ");
            sourceCode.Append(DequeType);
            sourceCode.Append("(), \"");
            sourceCode.Append(DequeValueType);
            sourceCode.Append("\", ");
            SourceDeque.Emit(sourceCode);
            sourceCode.Append(", graph.Model)");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return SourceDeque;
        }

        readonly String DequeType;
        readonly String DequeValueType;
        readonly Expression SourceDeque;
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
            if(First!=null)
                First.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            if(First!=null)
                yield return First;
        }

        readonly String ClassName;
        readonly String DequeName;
        readonly DequeItem First;
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
            if(Next!=null)
                yield return Next;
        }

        readonly Expression Value;
        readonly String ValueType;
        readonly DequeItem Next;
    }

    /// <summary>
    /// Class representing a match class constructor.
    /// </summary>
    public class MatchClassConstructor : Expression
    {
        public MatchClassConstructor(String className)
        {
            ClassName = className;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new MatchClassConstructor(ClassName);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("new " + ClassName + "()");
        }

        readonly String ClassName;
    }

    /// <summary>
    /// Class representing a match type constructor.
    /// </summary>
    public class InternalObjectConstructor : Expression
    {
        public InternalObjectConstructor(String className, bool isTransientClass)
        {
            ClassName = className;
            IsTransientClass = isTransientClass;
        }

        public InternalObjectConstructor(String className, String objectName, bool isTransientClass, AttributeInitialization first)
        {
            ClassName = className;
            ObjectName = objectName;
            IsTransientClass = isTransientClass;
            First = first;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new InternalObjectConstructor(ClassName, ObjectName, IsTransientClass, First != null ? (AttributeInitialization)First.Copy(renameSuffix) : null);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(ObjectName == null)
                sourceCode.Append("new " + ClassName + "(" + (IsTransientClass ? "" : "graph.GlobalVariables.FetchObjectUniqueId()") + ")");
            else
            {
                sourceCode.Append(ClassName + ".fill_" + ObjectName + "(" + (IsTransientClass ? "" : "graph.GlobalVariables.FetchObjectUniqueId(), "));
                if(First != null)
                    First.Emit(sourceCode);
                sourceCode.Append(")");
            }
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            if(First != null)
                yield return First;
        }

        readonly String ClassName;
        readonly bool IsTransientClass;

        readonly String ObjectName;
        readonly AttributeInitialization First;
    }

    /// <summary>
    /// Class representing an attribute initialization.
    /// </summary>
    public class AttributeInitialization : Expression
    {
        public AttributeInitialization(Expression value, String valueType, AttributeInitialization next)
        {
            Value = value;
            ValueType = valueType;
            Next = next;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new AttributeInitialization(Value.Copy(renameSuffix), ValueType, Next != null ? (AttributeInitialization)Next.Copy(renameSuffix) : null);
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
            if(Next != null)
                yield return Next;
        }

        readonly Expression Value;
        readonly String ValueType;
        readonly AttributeInitialization Next;
    }
}

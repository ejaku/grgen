/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
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
    }


    /// <summary>
    /// Base class of expressions used in conditions to constrain the pattern
    /// </summary>
    public abstract class Expression : ExpressionOrYielding
    {
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

        Expression Left;
        Expression Right;
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

        public override string GetFuncOperatorAndLParen()
        {
            return "GRGEN_LIBGR.DictionaryListHelper.Union(";
        }
    }

    /// <summary>
    /// Class representing the set/map intersection operator.
    /// </summary>
    public class DICT_BIT_AND : BinFuncOperator
    {
        public DICT_BIT_AND(Expression left, Expression right) : base(left, right) { }

        public override string GetFuncOperatorAndLParen()
        {
            return "GRGEN_LIBGR.DictionaryListHelper.Intersect(";
        }
    }

    /// <summary>
    /// Class representing the set/map except operator.
    /// </summary>
    public class DICT_EXCEPT : BinFuncOperator
    {
        public DICT_EXCEPT(Expression left, Expression right) : base(left, right) { }

        public override string GetFuncOperatorAndLParen()
        {
            return "GRGEN_LIBGR.DictionaryListHelper.Except(";
        }
    }

    /// <summary>
    /// Class representing a bitwise xor.
    /// </summary>
    public class BIT_XOR : BinInfixOperator
    {
        public BIT_XOR(Expression left, Expression right) : base(left, right) { }

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

        public override string GetInfixOperator()
        {
            return " >= ";
        }
    }

    /// <summary>
    /// Class representing set/map equality comparison.
    /// </summary>
    public class DICT_EQ : BinFuncOperator
    {
        public DICT_EQ(Expression left, Expression right) : base(left, right) { }

        public override string GetFuncOperatorAndLParen()
        {
            return "GRGEN_LIBGR.DictionaryListHelper.Equal(";
        }
    }

    /// <summary>
    /// Class representing set/map inequality comparison.
    /// </summary>
    public class DICT_NE : BinFuncOperator
    {
        public DICT_NE(Expression left, Expression right) : base(left, right) { }

        public override string GetFuncOperatorAndLParen()
        {
            return "GRGEN_LIBGR.DictionaryListHelper.NotEqual(";
        }
    }

    /// <summary>
    /// Class representing proper subset/map comparison.
    /// </summary>
    public class DICT_LT : BinFuncOperator
    {
        public DICT_LT(Expression left, Expression right) : base(left, right) { }

        public override string GetFuncOperatorAndLParen()
        {
            return "GRGEN_LIBGR.DictionaryListHelper.LessThan(";
        }
    }

    /// <summary>
    /// Class representing subset/map comparison.
    /// </summary>
    public class DICT_LE : BinFuncOperator
    {
        public DICT_LE(Expression left, Expression right) : base(left, right) { }

        public override string GetFuncOperatorAndLParen()
        {
            return "GRGEN_LIBGR.DictionaryListHelper.LessOrEqual(";
        }
    }

    /// <summary>
    /// Class representing proper superset comparison.
    /// </summary>
    public class DICT_GT : BinFuncOperator
    {
        public DICT_GT(Expression left, Expression right) : base(left, right) { }

        public override string GetFuncOperatorAndLParen()
        {
            return "GRGEN_LIBGR.DictionaryListHelper.GreaterThan(";
        }
    }

    /// <summary>
    /// Class representing superset comparison.
    /// </summary>
    public class DICT_GE : BinFuncOperator
    {
        public DICT_GE(Expression left, Expression right) : base(left, right) { }

        public override string GetFuncOperatorAndLParen()
        {
            return "GRGEN_LIBGR.DictionaryListHelper.GreaterOrEqual(";
        }
    }

    /// <summary>
    /// Class representing set/map equality comparison.
    /// </summary>
    public class LIST_EQ : BinFuncOperator
    {
        public LIST_EQ(Expression left, Expression right) : base(left, right) { }

        public override string GetFuncOperatorAndLParen()
        {
            return "GRGEN_LIBGR.DictionaryListHelper.Equal(";
        }
    }

    /// <summary>
    /// Class representing set/map inequality comparison.
    /// </summary>
    public class LIST_NE : BinFuncOperator
    {
        public LIST_NE(Expression left, Expression right) : base(left, right) { }

        public override string GetFuncOperatorAndLParen()
        {
            return "GRGEN_LIBGR.DictionaryListHelper.NotEqual(";
        }
    }

    /// <summary>
    /// Class representing proper subset/map comparison.
    /// </summary>
    public class LIST_LT : BinFuncOperator
    {
        public LIST_LT(Expression left, Expression right) : base(left, right) { }

        public override string GetFuncOperatorAndLParen()
        {
            return "GRGEN_LIBGR.DictionaryListHelper.LessThan(";
        }
    }

    /// <summary>
    /// Class representing subset/map comparison.
    /// </summary>
    public class LIST_LE : BinFuncOperator
    {
        public LIST_LE(Expression left, Expression right) : base(left, right) { }

        public override string GetFuncOperatorAndLParen()
        {
            return "GRGEN_LIBGR.DictionaryListHelper.LessOrEqual(";
        }
    }

    /// <summary>
    /// Class representing proper superset comparison.
    /// </summary>
    public class LIST_GT : BinFuncOperator
    {
        public LIST_GT(Expression left, Expression right) : base(left, right) { }

        public override string GetFuncOperatorAndLParen()
        {
            return "GRGEN_LIBGR.DictionaryListHelper.GreaterThan(";
        }
    }

    /// <summary>
    /// Class representing superset comparison.
    /// </summary>
    public class LIST_GE : BinFuncOperator
    {
        public LIST_GE(Expression left, Expression right) : base(left, right) { }

        public override string GetFuncOperatorAndLParen()
        {
            return "GRGEN_LIBGR.DictionaryListHelper.GreaterOrEqual(";
        }
    }

    /// <summary>
    /// Class representing a shift left expression.
    /// </summary>
    public class SHL : BinInfixOperator
    {
        public SHL(Expression left, Expression right) : base(left, right) { }

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

        public override string GetInfixOperator()
        {
            return " + ";
        }
    }

    /// <summary>
    /// Class representing a List concatenation.
    /// </summary>
    public class LIST_ADD : BinFuncOperator
    {
        public LIST_ADD(Expression left, Expression right) : base(left, right) { }

        public override string GetFuncOperatorAndLParen()
        {
            return "GRGEN_LIBGR.DictionaryListHelper.Concatenate(";
        }
    }

    /// <summary>
    /// Class representing a subtraction.
    /// </summary>
    public class SUB : BinInfixOperator
    {
        public SUB(Expression left, Expression right) : base(left, right) { }

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
        public Cast(String typeName, Expression nested, bool isDictionary)
        {
            TypeName = typeName;
            Nested = nested;
            IsDictionary = isDictionary;
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if (TypeName == "string")
            {
                if(IsDictionary)
                {
                    sourceCode.Append("GRGEN_LIBGR.DictionaryListHelper.ToString(");
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
        bool IsDictionary;
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

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append(NamesOfEntities.CandidateVariable(Entity));
        }

        String Entity;
    }

    /// <summary>
    /// Class representing nameof expression
    /// </summary>
    public class Nameof : Expression
    {
        public Nameof(String entity)
        {
            Entity = entity;
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if (Entity != null)
            {
                sourceCode.Append("((GRGEN_LGSP.LGSPNamedGraph)graph).GetElementName(" + NamesOfEntities.CandidateVariable(Entity) +")");
            }
            else
            {
                sourceCode.Append("graph.Name");
            }
        }

        String Entity;
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

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("((" + OwnerType + ")"+ NamesOfEntities.CandidateVariable(Owner) + ").@" + Member);
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
    /// Class representing visited expression
    /// </summary>
    public class Visited : Expression
    {
        public Visited(String entity, Expression nested) // nested = expression computing visited-id
        {
            Entity = entity;
            Nested = nested;
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("graph.IsVisited(" + NamesOfEntities.CandidateVariable(Entity) + ", ");
            Nested.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Nested;
        }

        String Entity;
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
            yield return Nested;
        }

        Expression Nested;
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
    /// Class representing a map size expression.
    /// </summary>
    public class MapSize : Expression
    {
        public MapSize(Expression target)
        {
            Target = target;
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
    /// Class representing a map peek expression.
    /// </summary>
    public class MapPeek : Expression
    {
        public MapPeek(Expression target, Expression number)
        {
            Target = target;
            Number = number;
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.DictionaryListHelper.Peek(");
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

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.DictionaryListHelper.Domain(");
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

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.DictionaryListHelper.Range(");
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
    /// Class representing a set peek expression.
    /// </summary>
    public class SetPeek : Expression
    {
        public SetPeek(Expression target, Expression number)
        {
            Target = target;
            Number = number;
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.DictionaryListHelper.Peek(");
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
    /// Class representing an array peek expression.
    /// </summary>
    public class ArrayPeek : Expression
    {
        public ArrayPeek(Expression target, Expression number)
        {
            Target = target;
            Number = number;
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            Target.Emit(sourceCode);
            sourceCode.Append("[");
            Number.Emit(sourceCode);
            sourceCode.Append("]");
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
    /// Class representing an array index of expression.
    /// </summary>
    public class ArrayIndexOf : Expression
    {
        public ArrayIndexOf(Expression target, Expression value)
        {
            Target = target;
            Value = value;
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.DictionaryListHelper.IndexOf(");
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

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.DictionaryListHelper.LastIndexOf(");
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

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.DictionaryListHelper.Subarray(");
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

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append(ClassName + "." + ArrayName);
        }

        String ClassName;
        String ArrayName;
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

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append(ClassName + ".fill_" + MapName + "(");
            First.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return First;
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

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append(ClassName + ".fill_" + SetName + "(");
            First.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return First;
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
            yield return Next;
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

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append(ClassName + ".fill_" + ArrayName + "(");
            First.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return First;
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
            yield return Next;
        }

        Expression Value;
        String ValueType;
        ArrayItem Next;
    }

    /// <summary>
    /// Class representing a function invocation of an external attribute evaluation function.
    /// </summary>
    public class ExternalFunctionInvocation : Expression
    {
        public ExternalFunctionInvocation(String functionName, Expression[] arguments, String[] argumentTypes)
        {
            FunctionName = functionName;
            Arguments = arguments;
            ArgumentTypes = argumentTypes;
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_EXPR.ExternalFunctions." + FunctionName + "(");
            for(int i=0; i<Arguments.Length; ++i)
            {
                Expression argument = Arguments[i];
                if(ArgumentTypes[i]!=null) sourceCode.Append("("+ArgumentTypes[i]+")");
                argument.Emit(sourceCode);
                if(i+1<Arguments.Length) sourceCode.Append(", ");
            }
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            foreach(Expression argument in Arguments)
                yield return argument;
        }

        String FunctionName;
        Expression[] Arguments;
        String[] ArgumentTypes; // for each argument: if node/edge: the interface type, otherwise: null
    }

    /// <summary>
    /// Class representing expression returning the outgoing edges of a node (as set)
    /// </summary>
    public class Outgoing : Expression
    {
        public Outgoing(String node, String incidentEdgeType, String adjacentNodeType)
        {
            Node = node;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.Outgoing("
                + NamesOfEntities.CandidateVariable(Node) + ", "
                + IncidentEdgeType + ", "
                + AdjacentNodeType
                + ")");
        }

        String Node;
        String IncidentEdgeType;
        String AdjacentNodeType;
    }

    /// <summary>
    /// Class representing expression returning the incoming edges of a node (as set)
    /// </summary>
    public class Incoming : Expression
    {
        public Incoming(String node, String incidentEdgeType, String adjacentNodeType)
        {
            Node = node;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.Incoming("
                + NamesOfEntities.CandidateVariable(Node) + ", "
                + IncidentEdgeType + ", "
                + AdjacentNodeType
                + ")");
        }

        String Node;
        String IncidentEdgeType;
        String AdjacentNodeType;
    }

    /// <summary>
    /// Class representing the max operator.
    /// </summary>
    public class Max : BinFuncOperator
    {
        public Max(Expression left, Expression right) : base(left, right) { }

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

        public override string GetFuncOperatorAndLParen()
        {
            return "Math.Min(";
        }
    }

    /// <summary>
    /// Class representing the to-the-power-of operator.
    /// </summary>
    public class Pow : BinFuncOperator
    {
        public Pow(Expression left, Expression right) : base(left, right) { }

        public override string GetFuncOperatorAndLParen()
        {
            return "Math.Pow(";
        }
    }


    /////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////


    /// <summary>
    /// Base class of yielding in assignments and expressions
    /// </summary>
    public abstract class Yielding : ExpressionOrYielding
    {
    }

    /// <summary>
    /// Class representing a yielding assignment executed after the match was found
    /// writing a value computed from the right expression into the left def variable
    /// </summary>
    public class YieldAssignment : Yielding
    {
        public YieldAssignment(String left, bool isVar, Expression right)
        {
            Left = left;
            IsVar = isVar;
            Right = right;
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append(IsVar ? NamesOfEntities.Variable(Left) : NamesOfEntities.CandidateVariable(Left));
            sourceCode.Append(" = ");
            Right.Emit(sourceCode);
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Right;
        }

        String Left;
        bool IsVar;
        Expression Right;
    }

    /// <summary>
    /// Class representing a yielding indexed assignment executed after the match was found
    /// writing a value computed from the right expression
    /// into the position at the given index of the left def variable of type array (TODO: extend to map)
    /// </summary>
    public class YieldAssignmentIndexed : Yielding
    {
        public YieldAssignmentIndexed(String left, Expression right, Expression index)
        {
            Left = left;
            Right = right;
            Index = index;
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append(NamesOfEntities.Variable(Left));
            sourceCode.Append("[");
            Index.Emit(sourceCode);
            sourceCode.Append("] = ");
            Right.Emit(sourceCode);
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Right;
            yield return Index;
        }

        String Left;
        Expression Right;
        Expression Index;
    }

    /// <summary>
    /// Class representing a yielding change assignment executed after the match was found
    /// writing the information whether the right yield method changed the set/map it operates upon
    /// to the left def variable
    /// </summary>
    public class YieldChangeAssignment : Yielding
    {
        public YieldChangeAssignment(String left, YieldMethod right)
        {
            Left = left;
            Right = right;
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append(NamesOfEntities.Variable(Left));
            sourceCode.Append(" = ");
            Right.Emit(sourceCode);
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Right;
        }

        String Left;
        YieldMethod Right;
    }

    /// <summary>
    /// Class representing a yielding change conjunction assignment executed after the match was found
    /// writing the information whether the right yield method changed the set/map it operates upon
    /// and'ed with the left def variable to the left def variable
    /// </summary>
    public class YieldChangeConjunctionAssignment : Yielding
    {
        public YieldChangeConjunctionAssignment(String left, YieldMethod right)
        {
            Left = left;
            Right = right;
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append(NamesOfEntities.Variable(Left));
            sourceCode.Append(" &= ");
            Right.Emit(sourceCode);
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Right;
        }

        String Left;
        YieldMethod Right;
    }

    /// <summary>
    /// Class representing a yielding change disjunction assignment executed after the match was found
    /// writing the information whether the right yield method changed the set/map it operates upon
    /// or'ed with the left def variable to the left def variable
    /// </summary>
    public class YieldChangeDisjunctionAssignment : Yielding
    {
        public YieldChangeDisjunctionAssignment(String left, YieldMethod right)
        {
            Left = left;
            Right = right;
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append(NamesOfEntities.Variable(Left));
            sourceCode.Append(" |= ");
            Right.Emit(sourceCode);
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Right;
        }

        String Left;
        YieldMethod Right;
    }

    /// <summary>
    /// Class representing a yielding method call executed after the match was found
    /// writing a value computed from the right expression into the left def variable
    /// </summary>
    public abstract class YieldMethod : Yielding
    {
        public YieldMethod(String left, Expression right)
        {
            Left = left;
            Right = right;
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Right;
        }

        protected String Left;
        protected Expression Right;
    }

    /// <summary>
    /// Class representing a remove from set or map
    /// </summary>
    public class SetMapRemove : YieldMethod
    {
        public SetMapRemove(String left, Expression right)
            : base(left, right)
        {
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append(NamesOfEntities.Variable(Left));
            sourceCode.Append(".Remove(");
            Right.Emit(sourceCode);
            sourceCode.Append(")");
        }
    }

    /// <summary>
    /// Class representing an add to set
    /// </summary>
    public class SetAdd : YieldMethod
    {
        public SetAdd(String left, Expression value)
            : base(left, value)
        {
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append(NamesOfEntities.Variable(Left));
            sourceCode.Append(".Add(");
            Right.Emit(sourceCode);
            sourceCode.Append(")");
        }
    }

    /// <summary>
    /// Class representing an add to map
    /// </summary>
    public class MapAdd : YieldMethod
    {
        public MapAdd(String left, Expression key, Expression value)
            : base(left, key)
        {
            Value = value;
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append(NamesOfEntities.Variable(Left));
            sourceCode.Append(".Add(");
            Right.Emit(sourceCode);
            sourceCode.Append(", ");
            Value.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Right;
            yield return Value;
        }

        Expression Value;
    }

    /// <summary>
    /// Class representing a change set or map by union with another one
    /// </summary>
    public class SetMapUnion : YieldMethod
    {
        public SetMapUnion(String left, Expression right)
            : base(left, right)
        {
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.DictionaryListHelper.UnionChanged(");
            sourceCode.Append(NamesOfEntities.Variable(Left));
            sourceCode.Append(", ");
            Right.Emit(sourceCode);
            sourceCode.Append(")");
        }
    }

    /// <summary>
    /// Class representing a change set or map by intersection with another one
    /// </summary>
    public class SetMapIntersect : YieldMethod
    {
        public SetMapIntersect(String left, Expression right)
            : base(left, right)
        {
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.DictionaryListHelper.IntersectChanged(");
            sourceCode.Append(NamesOfEntities.Variable(Left));
            sourceCode.Append(", ");
            Right.Emit(sourceCode);
            sourceCode.Append(")");
        }
    }

    /// <summary>
    /// Class representing a change set or map by subtracting another one
    /// </summary>
    public class SetMapExcept : YieldMethod
    {
        public SetMapExcept(String left, Expression right)
            : base(left, right)
        {
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.DictionaryListHelper.ExceptChanged(");
            sourceCode.Append(NamesOfEntities.Variable(Left));
            sourceCode.Append(", ");
            Right.Emit(sourceCode);
            sourceCode.Append(")");
        }
    }

    /// <summary>
    /// Class representing an iterated accumulation yield executed after the match was found
    /// accumulating the values matched by a nested iterated with a chosen operator
    /// writing the accumulated value into the target def variable
    /// </summary>
    public class IteratedAccumulationYield : Yielding
    {
        public IteratedAccumulationYield(String variable, String unprefixedVariable, String iterated, Yielding statement)
        {
            Variable = variable;
            UnprefixedVariable = unprefixedVariable;
            Iterated = iterated;
            Statement = statement;
        }

        public void ReplaceVariableByIterationVariable(ExpressionOrYielding curr)
        {
            // traverses the yielding and expression tree, if it visits a reference to the iteration variable
            // it switches it from a normal variable reference into a iteration variable reference
            foreach(ExpressionOrYielding eoy in curr)
                ReplaceVariableByIterationVariable(eoy);

            if(curr is VariableExpression)
            {
                VariableExpression ve = (VariableExpression)curr;
                if(ve.Entity == Variable)
                {
                    ve.MatchEntity = IteratedMatchVariable;
                }
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            //sourceCode.Append(NamesOfEntities.Variable(Variable) + " ");
            //sourceCode.Append(IteratedMatchVariable);
            Statement.Emit(sourceCode);
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Statement;
        }

        public String Variable;
        public String UnprefixedVariable;
        public String Iterated;
        Yielding Statement;

        public String IteratedMatchVariable;
    }

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

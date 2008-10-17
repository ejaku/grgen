/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.0
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
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
    /// Base class of expressions used in conditions to constrain the pattern
    /// </summary>
    public abstract class Expression
    {
        /// <summary>
        /// emits c# code implementing expression into source builder
        /// to be implemented by concrete subclasses
        /// </summary>
        public abstract void Emit(SourceBuilder sourceCode);
    }

    /// <summary>
    /// Base class of operator expressions
    /// </summary>
    public abstract class Operator : Expression
    {
    }

    /// <summary>
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

        Expression Condition;
        Expression Left;
        Expression Right;
    }

    /// <summary>
    /// </summary>
    public class LOG_OR : Operator
    {
        public LOG_OR(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("(");
            Left.Emit(sourceCode);
            sourceCode.Append(" || ");
            Right.Emit(sourceCode);
            sourceCode.Append(")");
        }

        Expression Left;
        Expression Right;
    }

    /// <summary>
    /// </summary>
    public class LOG_AND : Operator
    {
        public LOG_AND(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("(");
            Left.Emit(sourceCode);
            sourceCode.Append(" && ");
            Right.Emit(sourceCode);
            sourceCode.Append(")");
        }

        Expression Left;
        Expression Right;
    }

    /// <summary>
    /// </summary>
    public class BIT_OR : Operator
    {
        public BIT_OR(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("(");
            Left.Emit(sourceCode);
            sourceCode.Append(" | ");
            Right.Emit(sourceCode);
            sourceCode.Append(")");
        }

        Expression Left;
        Expression Right;
    }

    /// <summary>
    /// </summary>
    public class BIT_XOR : Operator
    {
        public BIT_XOR(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("(");
            Left.Emit(sourceCode);
            sourceCode.Append(" ^ ");
            Right.Emit(sourceCode);
            sourceCode.Append(")");
        }

        Expression Left;
        Expression Right;
    }

    /// <summary>
    /// </summary>
    public class BIT_AND : Operator
    {
        public BIT_AND(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("(");
            Left.Emit(sourceCode);
            sourceCode.Append(" & ");
            Right.Emit(sourceCode);
            sourceCode.Append(")");
        }

        Expression Left;
        Expression Right;
    }

    /// <summary>
    /// </summary>
    public class EQ : Operator
    {
        public EQ(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("(");
            Left.Emit(sourceCode);
            sourceCode.Append(" == ");
            Right.Emit(sourceCode);
            sourceCode.Append(")");
        }

        Expression Left;
        Expression Right;
    }

    /// <summary>
    /// </summary>
    public class NE : Operator
    {
        public NE(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("(");
            Left.Emit(sourceCode);
            sourceCode.Append(" != ");
            Right.Emit(sourceCode);
            sourceCode.Append(")");
        }

        Expression Left;
        Expression Right;
    }

    /// <summary>
    /// </summary>
    public class LT : Operator
    {
        public LT(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("(");
            Left.Emit(sourceCode);
            sourceCode.Append(" < ");
            Right.Emit(sourceCode);
            sourceCode.Append(")");
        }

        Expression Left;
        Expression Right;
    }

    /// <summary>
    /// </summary>
    public class LE : Operator
    {
        public LE(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("(");
            Left.Emit(sourceCode);
            sourceCode.Append(" <= ");
            Right.Emit(sourceCode);
            sourceCode.Append(")");
        }

        Expression Left;
        Expression Right;
    }

    /// <summary>
    /// </summary>
    public class GT : Operator
    {
        public GT(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("(");
            Left.Emit(sourceCode);
            sourceCode.Append(" > ");
            Right.Emit(sourceCode);
            sourceCode.Append(")");
        }

        Expression Left;
        Expression Right;
    }

    /// <summary>
    /// </summary>
    public class GE : Operator
    {
        public GE(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("(");
            Left.Emit(sourceCode);
            sourceCode.Append(" >= ");
            Right.Emit(sourceCode);
            sourceCode.Append(")");
        }

        Expression Left;
        Expression Right;
    }

    /// <summary>
    /// </summary>
    public class SHL : Operator
    {
        public SHL(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("(");
            Left.Emit(sourceCode);
            sourceCode.Append(" << ");
            Right.Emit(sourceCode);
            sourceCode.Append(")");
        }

        Expression Left;
        Expression Right;
    }

    /// <summary>
    /// </summary>
    public class SHR : Operator
    {
        public SHR(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("(");
            Left.Emit(sourceCode);
            sourceCode.Append(" >> ");
            Right.Emit(sourceCode);
            sourceCode.Append(")");
        }

        Expression Left;
        Expression Right;
    }

    /// <summary>
    /// </summary>
    public class BIT_SHR : Operator
    {
        public BIT_SHR(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("(");
            Left.Emit(sourceCode);
            sourceCode.Append(" >> ");
            Right.Emit(sourceCode);
            sourceCode.Append(")");
        }

        Expression Left;
        Expression Right;
    }

    /// <summary>
    /// </summary>
    public class ADD : Operator
    {
        public ADD(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("(");
            Left.Emit(sourceCode);
            sourceCode.Append(" + ");
            Right.Emit(sourceCode);
            sourceCode.Append(")");
        }

        Expression Left;
        Expression Right;
    }

    /// <summary>
    /// </summary>
    public class SUB : Operator
    {
        public SUB(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("(");
            Left.Emit(sourceCode);
            sourceCode.Append(" - ");
            Right.Emit(sourceCode);
            sourceCode.Append(")");
        }

        Expression Left;
        Expression Right;
    }

    /// <summary>
    /// </summary>
    public class MUL : Operator
    {
        public MUL(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("(");
            Left.Emit(sourceCode);
            sourceCode.Append(" * ");
            Right.Emit(sourceCode);
            sourceCode.Append(")");
        }

        Expression Left;
        Expression Right;
    }

    /// <summary>
    /// </summary>
    public class DIV : Operator
    {
        public DIV(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("(");
            Left.Emit(sourceCode);
            sourceCode.Append(" / ");
            Right.Emit(sourceCode);
            sourceCode.Append(")");
        }

        Expression Left;
        Expression Right;
    }

    /// <summary>
    /// </summary>
    public class MOD : Operator
    {
        public MOD(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("(");
            Left.Emit(sourceCode);
            sourceCode.Append(" % ");
            Right.Emit(sourceCode);
            sourceCode.Append(")");
        }

        Expression Left;
        Expression Right;
    }

    /// <summary>
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

        Expression Nested;
    }

    /// <summary>
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

        Expression Nested;
    }

    /// <summary>
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

        Expression Nested;
    }

    /// <summary>
    /// </summary>
    public class IN : Operator
    {
        public IN(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("(");
            Right.Emit(sourceCode);
            sourceCode.Append(").ContainsKey(");
            Left.Emit(sourceCode);
            sourceCode.Append(")");
        }

        Expression Left;
        Expression Right;
    }

    /// <summary>
    /// Class representing cast expression
    /// </summary>
    public class Cast : Expression
    {
        public Cast(String typeName, Expression nested)
        {
            TypeName = typeName;
            Nested = nested;
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if (TypeName == "string")
            {
                Nested.Emit(sourceCode);
                sourceCode.Append(".ToString()");
            }
            else
            {
                sourceCode.Append("(" + TypeName + ")");
                Nested.Emit(sourceCode);
            }
        }

        String TypeName;
        Expression Nested;
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
            sourceCode.Append("ENUM_" + EnumType + ".@" + EnumItem);
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
                sourceCode.Append("graph.GetElementName(" + NamesOfEntities.CandidateVariable(Entity) +")");
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
            sourceCode.Append(NamesOfEntities.CandidateVariable(Entity) +".type");
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
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append(NamesOfEntities.Variable(Entity));
        }

        String Entity;
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

        String Entity;
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

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("(");
            Target.Emit(sourceCode);
            sourceCode.Append("[");
            KeyExpr.Emit(sourceCode);
            sourceCode.Append("])");
        }

        Expression Target;
        Expression KeyExpr;
    }
}

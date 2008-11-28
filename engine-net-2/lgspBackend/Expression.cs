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

        public abstract String GetInfixOperator();

        Expression Left;
        Expression Right;
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
            return "GRGEN_LIBGR.DictionaryHelper.Union(";
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
            return "GRGEN_LIBGR.DictionaryHelper.Intersect(";
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
            return "GRGEN_LIBGR.DictionaryHelper.Except(";
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
    /// Class representing a logical shift right expression.
    /// TODO: Currently same as shift right expression.
    /// </summary>
    public class BIT_SHR : BinInfixOperator
    {
        public BIT_SHR(Expression left, Expression right) : base(left, right) { }

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

        Expression Nested;
    }

    /// <summary>
    /// Class representing a map/set inclusion query.
    /// </summary>
    public class IN : BinInfixOperator
    {
        // Switch operands as "right" is the dictionary
        public IN(Expression left, Expression right) : base(right, left) { }

        public override string GetInfixOperator()
        {
            return ").ContainsKey(";
        }
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

        Expression Target;
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

        String ClassName;
        String MapName;
        MapItem First;
    }

    /// <summary>
    /// Class representing a map item.
    /// </summary>
    public class MapItem : Expression
    {
        public MapItem(Expression key, Expression value, MapItem next)
        {
            Key = key;
            Value = value;
            Next = next;
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            Key.Emit(sourceCode);
            sourceCode.Append(", ");
            Value.Emit(sourceCode);
            if (Next != null)
            {
                sourceCode.Append(", ");
                Next.Emit(sourceCode);
            }
        }

        Expression Key;
        Expression Value;
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

        String ClassName;
        String SetName;
        SetItem First;
    }

    /// <summary>
    /// Class representing a set item.
    /// </summary>
    public class SetItem : Expression
    {
        public SetItem(Expression value, SetItem next)
        {
            Value = value;
            Next = next;
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            Value.Emit(sourceCode);
            if (Next != null)
            {
                sourceCode.Append(", ");
                Next.Emit(sourceCode);
            }
        }

        Expression Value;
        SetItem Next;
    }
}

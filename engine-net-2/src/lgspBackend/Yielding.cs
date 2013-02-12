/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.6
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
    /// Base class of yielding in assignments and expressions
    /// </summary>
    public abstract class Yielding : ExpressionOrYielding
    {
        /// <summary>
        /// copies the yielding, renaming all variables with the given suffix
        /// </summary>
        public abstract Yielding Copy(string renameSuffix);
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

        public override Yielding Copy(string renameSuffix)
        {
            return new YieldAssignment(Left + renameSuffix, IsVar, Right.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append(IsVar ? NamesOfEntities.Variable(Left) : NamesOfEntities.CandidateVariable(Left));
            sourceCode.Append(" = ");
            Right.Emit(sourceCode);
            sourceCode.Append(";\n");
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

        public override Yielding Copy(string renameSuffix)
        {
            return new YieldAssignmentIndexed(Left + renameSuffix, Right.Copy(renameSuffix), Index.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append(NamesOfEntities.Variable(Left));
            sourceCode.Append("[");
            Index.Emit(sourceCode);
            sourceCode.Append("] = ");
            Right.Emit(sourceCode);
            sourceCode.Append(";\n");
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

        public override Yielding Copy(string renameSuffix)
        {
            return new YieldChangeAssignment(Left + renameSuffix, (YieldMethod)Right.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append(NamesOfEntities.Variable(Left));
            sourceCode.Append(" = ");
            Right.Emit(sourceCode);
            sourceCode.Append(";\n");
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

        public override Yielding Copy(string renameSuffix)
        {
            return new YieldChangeConjunctionAssignment(Left + renameSuffix, (YieldMethod)Right.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append(NamesOfEntities.Variable(Left));
            sourceCode.Append(" &= ");
            Right.Emit(sourceCode);
            sourceCode.Append(";\n");
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

        public override Yielding Copy(string renameSuffix)
        {
            return new YieldChangeDisjunctionAssignment(Left + renameSuffix, (YieldMethod)Right.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append(NamesOfEntities.Variable(Left));
            sourceCode.Append(" |= ");
            Right.Emit(sourceCode);
            sourceCode.Append(";\n");
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

        public override Yielding Copy(string renameSuffix)
        {
            return new SetMapRemove(Left + renameSuffix, Right.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append(NamesOfEntities.Variable(Left));
            sourceCode.Append(".Remove(");
            Right.Emit(sourceCode);
            sourceCode.Append(")");
            sourceCode.Append(";\n");
        }
    }

    /// <summary>
    /// Class representing a remove from an array
    /// </summary>
    public class ArrayRemove : YieldMethod
    {
        public ArrayRemove(String left, Expression right)
            : base(left, right)
        {
        }

        public ArrayRemove(String left)
            : base(left, null)
        {
        }

        public override Yielding Copy(string renameSuffix)
        {
            return new ArrayRemove(Left + renameSuffix, null);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append(NamesOfEntities.Variable(Left));
            sourceCode.Append(".RemoveAt(");
            if(Right==null)
                sourceCode.Append(NamesOfEntities.Variable(Left) + ".Count-1");
            else
                Right.Emit(sourceCode);
            sourceCode.Append(")");
            sourceCode.Append(";\n");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            if(Right==null) yield return Right;
            yield break;
        }
    }

    /// <summary>
    /// Class representing a remove from a deque
    /// </summary>
    public class DequeRemove : YieldMethod
    {
        public DequeRemove(String left, Expression right)
            : base(left, right)
        {
        }

        public DequeRemove(String left)
            : base(left, null)
        {
        }

        public override Yielding Copy(string renameSuffix)
        {
            return new DequeRemove(Left + renameSuffix, null);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append(NamesOfEntities.Variable(Left));
            if(Right == null)
                sourceCode.Append(".Dequeue(");
            else
            {
                sourceCode.Append(".DequeueAt(");
                Right.Emit(sourceCode);
            }
            sourceCode.Append(")");
            sourceCode.Append(";\n");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            if(Right == null) yield return Right;
            yield break;
        }
    }

    /// <summary>
    /// Class representing a clearing of a set or a map or an array or a deque
    /// </summary>
    public class Clear : YieldMethod
    {
        public Clear(String left)
            : base(left, null)
        {
        }

        public override Yielding Copy(string renameSuffix)
        {
            return new Clear(Left + renameSuffix);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append(NamesOfEntities.Variable(Left));
            sourceCode.Append(".Clear()");
            sourceCode.Append(";\n");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield break;
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

        public override Yielding Copy(string renameSuffix)
        {
            return new SetAdd(Left + renameSuffix, Right.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append(NamesOfEntities.Variable(Left));
            sourceCode.Append(".Add(");
            Right.Emit(sourceCode);
            sourceCode.Append(")");
            sourceCode.Append(";\n");
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

        public override Yielding Copy(string renameSuffix)
        {
            return new MapAdd(Left + renameSuffix, Right.Copy(renameSuffix), Value.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append(NamesOfEntities.Variable(Left));
            sourceCode.Append(".Add(");
            Right.Emit(sourceCode);
            sourceCode.Append(", ");
            Value.Emit(sourceCode);
            sourceCode.Append(")");
            sourceCode.Append(";\n");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Right;
            yield return Value;
        }

        Expression Value;
    }

    /// <summary>
    /// Class representing an add to an array
    /// </summary>
    public class ArrayAdd : YieldMethod
    {
        public ArrayAdd(String left, Expression value, Expression index)
            : base(left, value)
        {
            Index = index;
        }

        public ArrayAdd(String left, Expression value)
            : base(left, value)
        {
            Index = null;
        }

        public override Yielding Copy(string renameSuffix)
        {
            return new ArrayAdd(Left + renameSuffix, Right.Copy(renameSuffix), Index != null ? Index.Copy(renameSuffix) : Index);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append(NamesOfEntities.Variable(Left));
            if(Index != null)
            {
                sourceCode.Append(".Insert(");
                Index.Emit(sourceCode);
                sourceCode.Append(", ");
                Right.Emit(sourceCode);
                sourceCode.Append(")");
                sourceCode.Append(";\n");
            }
            else
            {
                sourceCode.Append(".Add(");
                Right.Emit(sourceCode);
                sourceCode.Append(")");
                sourceCode.Append(";\n");
            }
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            if(Index != null) yield return Index;
            yield return Right;
        }

        Expression Index;
    }

    /// <summary>
    /// Class representing an add to a deque
    /// </summary>
    public class DequeAdd : YieldMethod
    {
        public DequeAdd(String left, Expression value, Expression index)
            : base(left, value)
        {
            Index = index;
        }

        public DequeAdd(String left, Expression value)
            : base(left, value)
        {
            Index = null;
        }

        public override Yielding Copy(string renameSuffix)
        {
            return new DequeAdd(Left + renameSuffix, Right.Copy(renameSuffix), Index != null ? Index.Copy(renameSuffix) : Index);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append(NamesOfEntities.Variable(Left));
            if(Index != null)
            {
                sourceCode.Append(".EnqueueAt(");
                Index.Emit(sourceCode);
                sourceCode.Append(", ");
                Right.Emit(sourceCode);
                sourceCode.Append(")");
                sourceCode.Append(";\n");
            }
            else
            {
                sourceCode.Append(".Enqueue(");
                Right.Emit(sourceCode);
                sourceCode.Append(")");
                sourceCode.Append(";\n");
            }
        }

        Expression Index;
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

        public override Yielding Copy(string renameSuffix)
        {
            return new SetMapUnion(Left + renameSuffix, Right.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.ContainerHelper.UnionChanged(");
            sourceCode.Append(NamesOfEntities.Variable(Left));
            sourceCode.Append(", ");
            Right.Emit(sourceCode);
            sourceCode.Append(")");
            sourceCode.Append(";\n");
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

        public override Yielding Copy(string renameSuffix)
        {
            return new SetMapIntersect(Left + renameSuffix, Right.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.ContainerHelper.IntersectChanged(");
            sourceCode.Append(NamesOfEntities.Variable(Left));
            sourceCode.Append(", ");
            Right.Emit(sourceCode);
            sourceCode.Append(")");
            sourceCode.Append(";\n");
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

        public override Yielding Copy(string renameSuffix)
        {
            return new SetMapExcept(Left + renameSuffix, Right.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.ContainerHelper.ExceptChanged(");
            sourceCode.Append(NamesOfEntities.Variable(Left));
            sourceCode.Append(", ");
            Right.Emit(sourceCode);
            sourceCode.Append(")");
            sourceCode.Append(";\n");
        }
    }

    /// <summary>
    /// Class representing an iterated accumulation yield executed after the match was found
    /// accumulating the values matched by a nested iterated with chosen statements
    /// </summary>
    public class IteratedAccumulationYield : Yielding
    {
        public IteratedAccumulationYield(String variable, String unprefixedVariable, String iterated, Yielding[] statements)
        {
            Variable = variable;
            UnprefixedVariable = unprefixedVariable;
            Iterated = iterated;
            Statements = statements;
        }

        public override Yielding Copy(string renameSuffix)
        {
            Yielding[] statementsCopy = new Yielding[Statements.Length];
            for(int i = 0; i < Statements.Length; ++i)
                statementsCopy[i] = Statements[i].Copy(renameSuffix);
            return new IteratedAccumulationYield(Variable + renameSuffix, UnprefixedVariable + renameSuffix, Iterated, statementsCopy);
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
            foreach(Yielding statement in Statements)
                statement.Emit(sourceCode);
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            foreach(Yielding statement in Statements)
                yield return statement;
        }

        public String Variable;
        public String UnprefixedVariable;
        public String Iterated;
        Yielding[] Statements;

        public String IteratedMatchVariable;
    }

    /// <summary>
    /// Class representing an container accumulation yield executed after the match was found
    /// accumulating the values in a container with chosen statements
    /// </summary>
    public class ContainerAccumulationYield : Yielding
    {
        // TODO: kopie von iterated, anpassen, spezialmagie für iterated entfernen
        public ContainerAccumulationYield(String variable, String unprefixedVariable, String container, Yielding[] statements)
        {
            Variable = variable;
            UnprefixedVariable = unprefixedVariable;
            Container = container;
            Statements = statements;
        }

        public ContainerAccumulationYield(String variable, String unprefixedVariable, String index, String unprefixedIndex, String container, Yielding[] statement)
        {
            Variable = variable;
            UnprefixedVariable = unprefixedVariable;
            Index = index;
            UnprefixedIndex = unprefixedIndex;
            Container = container;
            Statements = statement;
        }

        public override Yielding Copy(string renameSuffix)
        {
            Yielding[] statementsCopy = new Yielding[Statements.Length];
            for(int i = 0; i < Statements.Length; ++i)
                statementsCopy[i] = Statements[i].Copy(renameSuffix);
            if(Index != null)
                return new ContainerAccumulationYield(Variable + renameSuffix, UnprefixedVariable + renameSuffix, Index + renameSuffix, UnprefixedIndex + renameSuffix, Container, statementsCopy);
            else
                return new ContainerAccumulationYield(Variable + renameSuffix, UnprefixedVariable + renameSuffix, Container, statementsCopy);
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
                if(ve.Entity == Index)
                {
                    ve.MatchEntity = IteratedMatchVariable;
                }
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            //sourceCode.Append(NamesOfEntities.Variable(Variable) + " ");
            //sourceCode.Append(IteratedMatchVariable);
            foreach(Yielding statement in Statements)
                statement.Emit(sourceCode);
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            foreach(Yielding statement in Statements)
                yield return statement;
        }

        public String Variable;
        public String UnprefixedVariable;
        public String Index;
        public String UnprefixedIndex;
        public String Container;
        Yielding[] Statements;

        public String IteratedMatchVariable;
    }

    /// <summary>
    /// Class representing an if statement, maybe with else part
    /// </summary>
    public class ConditionStatement : Yielding
    {
        public ConditionStatement(Expression condition, Yielding[] trueCaseStatements, Yielding[] falseCaseStatements)
        {
            Condition = condition;
            TrueCaseStatements = trueCaseStatements;
            FalseCaseStatements = falseCaseStatements;
        }

        public override Yielding Copy(string renameSuffix)
        {
            Yielding[] trueCaseStatementsCopy = new Yielding[TrueCaseStatements.Length];
            for(int i = 0; i < TrueCaseStatements.Length; ++i)
                trueCaseStatementsCopy[i] = TrueCaseStatements[i].Copy(renameSuffix);
            Yielding[] falseCaseStatementsCopy = null;
            if(FalseCaseStatements != null)
            {
                falseCaseStatementsCopy = new Yielding[FalseCaseStatements.Length];
                for(int i = 0; i < FalseCaseStatements.Length; ++i)
                    falseCaseStatementsCopy[i] = FalseCaseStatements[i].Copy(renameSuffix);
            }
            return new ConditionStatement(Condition.Copy(renameSuffix), trueCaseStatementsCopy, falseCaseStatementsCopy);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("if(");
            Condition.Emit(sourceCode);
            sourceCode.Append(") {\n");
            foreach(Yielding statement in TrueCaseStatements)
                statement.Emit(sourceCode);
            if(FalseCaseStatements != null)
            {
                sourceCode.Append("} else {\n");
                foreach(Yielding statement in FalseCaseStatements)
                    statement.Emit(sourceCode);
            }
            sourceCode.Append("}\n");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            foreach(Yielding statement in TrueCaseStatements)
                yield return statement;
            if(FalseCaseStatements!=null)
                foreach(Yielding statement in FalseCaseStatements)
                    yield return statement;
        }

        Expression Condition;
        Yielding[] TrueCaseStatements;
        Yielding[] FalseCaseStatements;
    }

    /// <summary>
    /// Class representing while statement
    /// </summary>
    public class WhileStatement : Yielding
    {
        public WhileStatement(Expression condition, Yielding[] loopedStatements)
        {
            Condition = condition;
            LoopedStatements = loopedStatements;
        }

        public override Yielding Copy(string renameSuffix)
        {
            Yielding[] loopedStatementsCopy = new Yielding[LoopedStatements.Length];
            for(int i = 0; i < LoopedStatements.Length; ++i)
                loopedStatementsCopy[i] = LoopedStatements[i].Copy(renameSuffix);
            return new WhileStatement(Condition.Copy(renameSuffix), loopedStatementsCopy);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("while(");
            Condition.Emit(sourceCode);
            sourceCode.Append(") {\n");
            foreach(Yielding statement in LoopedStatements)
                statement.Emit(sourceCode);
            sourceCode.Append("}\n");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            foreach(Yielding statement in LoopedStatements)
                yield return statement;
        }

        Expression Condition;
        Yielding[] LoopedStatements;
    }

    /// <summary>
    /// Class representing a def declaration (variable or graph entity), potentially including initialization
    /// </summary>
    public class DefDeclaration : Yielding
    {
        public DefDeclaration(String name, String type, Expression initialization)
        {
            Name = name;
            Type = type;
            Initialization = initialization;
        }

        public override Yielding Copy(string renameSuffix)
        {
            return new DefDeclaration(Name + renameSuffix, Type, Initialization != null ? Initialization.Copy(renameSuffix) : null);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append(Type + " " + NamesOfEntities.Variable(Name));
            if(Initialization != null)
            {
                sourceCode.Append(" = ");
                Initialization.Emit(sourceCode);
            }
            sourceCode.Append(";\n");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Initialization;
        }

        String Name;
        String Type;
        Expression Initialization;
    }
}

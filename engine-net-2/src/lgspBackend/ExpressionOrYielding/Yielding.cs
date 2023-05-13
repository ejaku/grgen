/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Collections.Generic;

using de.unika.ipd.grGen.libGr;
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
        public YieldAssignment(String left, bool isVar, String targetType, Expression right)
        {
            Left = left;
            IsVar = isVar;
            TargetType = targetType;
            Right = right;
        }

        public override Yielding Copy(string renameSuffix)
        {
            return new YieldAssignment(Left + renameSuffix, IsVar, TargetType, Right.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append(IsVar ? NamesOfEntities.Variable(Left) : NamesOfEntities.CandidateVariable(Left));
            sourceCode.Append(" = ");
            sourceCode.Append("(" + TargetType + ")");
            Right.Emit(sourceCode);
            sourceCode.Append(";\n");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Right;
        }

        readonly String Left;
        readonly bool IsVar;
        readonly String TargetType;
        readonly Expression Right;
    }

    /// <summary>
    /// Class representing a yielding indexed assignment executed after the match was found
    /// writing a value computed from the right expression
    /// into the position at the given index of the left def variable
    /// </summary>
    public class YieldAssignmentIndexed : Yielding
    {
        public YieldAssignmentIndexed(String left, Expression right, Expression index, string typeRight, string typeIndex)
        {
            Left = left;
            Right = right;
            Index = index;
            TypeRight = typeRight;
            TypeIndex = typeIndex;
        }

        public override Yielding Copy(string renameSuffix)
        {
            return new YieldAssignmentIndexed(Left + renameSuffix, Right.Copy(renameSuffix), Index.Copy(renameSuffix), TypeRight, TypeIndex);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append(NamesOfEntities.Variable(Left));
            sourceCode.Append("[");
            sourceCode.Append("(");
            sourceCode.Append(TypeIndex);
            sourceCode.Append(")");
            Index.Emit(sourceCode);
            sourceCode.Append("] = ");
            sourceCode.Append("(");
            sourceCode.Append(TypeRight);
            sourceCode.Append(")");
            Right.Emit(sourceCode);
            sourceCode.Append(";\n");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Right;
            yield return Index;
        }

        readonly String Left;
        readonly Expression Right;
        readonly String TypeRight;
        readonly Expression Index;
        readonly String TypeIndex;
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

        readonly String Left;
        readonly YieldMethod Right;
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

        readonly String Left;
        readonly YieldMethod Right;
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

        readonly String Left;
        readonly YieldMethod Right;
    }

    /// <summary>
    /// Class representing a yielding method call executed after the match was found
    /// writing a value computed from the right expression into the left def variable
    /// </summary>
    public abstract class YieldMethod : Yielding
    {
        protected YieldMethod(String left, Expression right)
        {
            Left = left;
            Right = right;
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Right;
        }

        protected readonly String Left;
        protected readonly Expression Right;
    }

    /// <summary>
    /// Class representing a remove from set or map
    /// </summary>
    public class SetMapRemove : YieldMethod
    {
        public SetMapRemove(String left, Expression right, String rightType)
            : base(left, right)
        {
            RightType = rightType;
        }

        public override Yielding Copy(string renameSuffix)
        {
            return new SetMapRemove(Left + renameSuffix, Right.Copy(renameSuffix), RightType);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append(NamesOfEntities.Variable(Left));
            sourceCode.Append(".Remove(");
            sourceCode.Append("(");
            sourceCode.Append(RightType);
            sourceCode.Append(")");
            Right.Emit(sourceCode);
            sourceCode.Append(")");
            sourceCode.Append(";\n");
        }

        readonly String RightType;
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
            return new ArrayRemove(Left + renameSuffix, Right);
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
            if(Right != null)
                yield return Right;
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
            return new DequeRemove(Left + renameSuffix, Right);
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
            if(Right != null)
                yield return Right;
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
        public SetAdd(String left, Expression value, String valueType)
            : base(left, value)
        {
            ValueType = valueType;
        }

        public override Yielding Copy(string renameSuffix)
        {
            return new SetAdd(Left + renameSuffix, Right.Copy(renameSuffix), ValueType);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append(NamesOfEntities.Variable(Left));
            sourceCode.Append("[");
            sourceCode.Append("(");
            sourceCode.Append(ValueType);
            sourceCode.Append(")");
            Right.Emit(sourceCode);
            sourceCode.Append("] = null");
            sourceCode.Append(";\n");
        }

        readonly String ValueType;
    }

    /// <summary>
    /// Class representing an addAll to a set
    /// </summary>
    public class SetAddAll : YieldMethod
    {
        // attention: valueType is the value type of the set type of the value, not of the value itself
        public SetAddAll(String left, Expression value, String valueType)
            : base(left, value)
        {
            ValueType = valueType;
        }

        public override Yielding Copy(string renameSuffix)
        {
            return new SetAddAll(Left + renameSuffix, Right.Copy(renameSuffix), ValueType);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            String setValueName = "value_" + Id;
            sourceCode.Append("foreach(" + ValueType + " " + setValueName + " in (");
            Right.Emit(sourceCode);
            sourceCode.Append(").Keys)\n");
            sourceCode.Append("{\n");
            sourceCode.Indent();
            sourceCode.Append(NamesOfEntities.Variable(Left));
            sourceCode.Append(".Add(" + setValueName + ", null);\n");
            sourceCode.Unindent();
            sourceCode.Append("}\n");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Right;
        }

        readonly String ValueType;
    }

    /// <summary>
    /// Class representing an add to map
    /// </summary>
    public class MapAdd : YieldMethod
    {
        public MapAdd(String left, Expression key, Expression value, String keyType, String valueType)
            : base(left, key)
        {
            Value = value;
            KeyType = keyType;
            ValueType = valueType;
        }

        public override Yielding Copy(string renameSuffix)
        {
            return new MapAdd(Left + renameSuffix, Right.Copy(renameSuffix), Value.Copy(renameSuffix), KeyType, ValueType);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append(NamesOfEntities.Variable(Left));
            sourceCode.Append("[");
            sourceCode.Append("(");
            sourceCode.Append(KeyType);
            sourceCode.Append(")");
            Right.Emit(sourceCode);
            sourceCode.Append("] = ");
            sourceCode.Append("(");
            sourceCode.Append(ValueType);
            sourceCode.Append(")");
            Value.Emit(sourceCode);
            sourceCode.Append(";\n");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Right;
            yield return Value;
        }

        readonly Expression Value;
        readonly String KeyType;
        readonly String ValueType;
    }

    /// <summary>
    /// Class representing an add to an array
    /// </summary>
    public class ArrayAdd : YieldMethod
    {
        public ArrayAdd(String left, Expression value, String valueType, Expression index)
            : base(left, value)
        {
            Index = index;
            ValueType = valueType;
        }

        public ArrayAdd(String left, Expression value, String valueType)
            : base(left, value)
        {
            Index = null;
            ValueType = valueType;
        }

        public override Yielding Copy(string renameSuffix)
        {
            return new ArrayAdd(Left + renameSuffix, Right.Copy(renameSuffix), ValueType, Index != null ? Index.Copy(renameSuffix) : Index);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append(NamesOfEntities.Variable(Left));
            if(Index != null)
            {
                sourceCode.Append(".Insert(");
                Index.Emit(sourceCode);
                sourceCode.Append(", ");
                sourceCode.Append("(");
                sourceCode.Append(ValueType);
                sourceCode.Append(")");
                Right.Emit(sourceCode);
                sourceCode.Append(")");
                sourceCode.Append(";\n");
            }
            else
            {
                sourceCode.Append(".Add(");
                sourceCode.Append("(");
                sourceCode.Append(ValueType);
                sourceCode.Append(")");
                Right.Emit(sourceCode);
                sourceCode.Append(")");
                sourceCode.Append(";\n");
            }
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            if(Index != null)
                yield return Index;
            yield return Right;
        }

        readonly Expression Index;
        readonly String ValueType;
    }

    /// <summary>
    /// Class representing an addAll to an array
    /// </summary>
    public class ArrayAddAll : YieldMethod
    {
        // attention: valueType is the value type of the array type of the value, not of the value itself
        public ArrayAddAll(String left, Expression value, String valueType)
            : base(left, value)
        {
            ValueType = valueType;
        }

        public override Yielding Copy(string renameSuffix)
        {
            return new ArrayAddAll(Left + renameSuffix, Right.Copy(renameSuffix), ValueType);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append(NamesOfEntities.Variable(Left));
            sourceCode.Append(".AddRange(");
            Right.Emit(sourceCode);
            sourceCode.Append(")");
            sourceCode.Append(";\n");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Right;
        }

        readonly String ValueType;
    }

    /// <summary>
    /// Class representing an add to a deque
    /// </summary>
    public class DequeAdd : YieldMethod
    {
        public DequeAdd(String left, Expression value, String valueType, Expression index)
            : base(left, value)
        {
            Index = index;
            ValueType = valueType;
        }

        public DequeAdd(String left, Expression value, String valueType)
            : base(left, value)
        {
            Index = null;
            ValueType = valueType;
        }

        public override Yielding Copy(string renameSuffix)
        {
            return new DequeAdd(Left + renameSuffix, Right.Copy(renameSuffix), ValueType, Index != null ? Index.Copy(renameSuffix) : Index);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append(NamesOfEntities.Variable(Left));
            if(Index != null)
            {
                sourceCode.Append(".EnqueueAt(");
                Index.Emit(sourceCode);
                sourceCode.Append(", ");
                sourceCode.Append("(");
                sourceCode.Append(ValueType);
                sourceCode.Append(")");
                Right.Emit(sourceCode);
                sourceCode.Append(")");
                sourceCode.Append(";\n");
            }
            else
            {
                sourceCode.Append(".Enqueue(");
                sourceCode.Append("(");
                sourceCode.Append(ValueType);
                sourceCode.Append(")");
                Right.Emit(sourceCode);
                sourceCode.Append(")");
                sourceCode.Append(";\n");
            }
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            if(Index != null)
                yield return Index;
            yield return Right;
        }

        readonly Expression Index;
        readonly String ValueType;
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
            {
                statementsCopy[i] = Statements[i].Copy(renameSuffix);
            }
            return new IteratedAccumulationYield(Variable + renameSuffix, UnprefixedVariable + renameSuffix, Iterated, statementsCopy);
        }

        public void ReplaceVariableByIterationVariable(ExpressionOrYielding curr)
        {
            // traverses the yielding and expression tree, if it visits a reference to the iteration variable
            // it switches it from a normal variable reference into a iteration variable reference
            foreach(ExpressionOrYielding eoy in curr)
            {
                ReplaceVariableByIterationVariable(eoy);
            }

            if(curr is VariableExpression)
            {
                VariableExpression ve = (VariableExpression)curr;
                if(ve.Entity == Variable)
                    ve.MatchEntity = IteratedMatchVariable;
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            //sourceCode.Append(NamesOfEntities.Variable(Variable) + " ");
            //sourceCode.Append(IteratedMatchVariable);
            foreach(Yielding statement in Statements)
            {
                statement.Emit(sourceCode);
            }
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            foreach(Yielding statement in Statements)
            {
                yield return statement;
            }
        }

        public readonly String Variable;
        public readonly String UnprefixedVariable;
        public readonly String Iterated;
        readonly Yielding[] Statements;

        public String IteratedMatchVariable;
    }

    /// <summary>
    /// Class representing an container accumulation yield, accumulating the values in a container with chosen statements
    /// </summary>
    public class ContainerAccumulationYield : Yielding
    {
        public ContainerAccumulationYield(String variable, String unprefixedVariable, String variableType, String container, String unprefixedContainer, String containerType, Yielding[] statements)
        {
            Variable = variable;
            UnprefixedVariable = unprefixedVariable;
            VariableType = variableType;
            Container = container;
            UnprefixedContainer = unprefixedContainer;
            ContainerType = containerType;
            Statements = statements;
        }

        public ContainerAccumulationYield(String variable, String unprefixedVariable, String variableType, String index, String unprefixedIndex, String indexType, String container, String unprefixedContainer, String containerType, Yielding[] statement)
        {
            Variable = variable;
            UnprefixedVariable = unprefixedVariable;
            VariableType = variableType;
            Index = index;
            UnprefixedIndex = unprefixedIndex;
            IndexType = indexType;
            Container = container;
            UnprefixedContainer = unprefixedContainer;
            ContainerType = containerType;
            Statements = statement;
        }

        public override Yielding Copy(string renameSuffix)
        {
            Yielding[] statementsCopy = new Yielding[Statements.Length];
            for(int i = 0; i < Statements.Length; ++i)
            {
                statementsCopy[i] = Statements[i].Copy(renameSuffix);
            }
            if(Index != null)
                return new ContainerAccumulationYield(Variable + renameSuffix, UnprefixedVariable + renameSuffix, VariableType, Index + renameSuffix, UnprefixedIndex + renameSuffix, IndexType, Container, UnprefixedContainer + renameSuffix, ContainerType, statementsCopy);
            else
                return new ContainerAccumulationYield(Variable + renameSuffix, UnprefixedVariable + renameSuffix, VariableType, Container, UnprefixedContainer + renameSuffix, ContainerType, statementsCopy);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(ContainerType.StartsWith("List"))
            {
                sourceCode.AppendFrontFormat("{0} entry_{1} = ({0}) " + NamesOfEntities.Variable(Container) + ";\n", ContainerType, Id);
                sourceCode.AppendFrontFormat("for(int index_{0}=0; index_{0}<entry_{0}.Count; ++index_{0})\n", Id);
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                if(Index != null)
                {
                    sourceCode.AppendFront(IndexType + " " + NamesOfEntities.Variable(Index) + " = index_" + Id + ";\n");
                    sourceCode.AppendFront(VariableType + " " + NamesOfEntities.Variable(Variable) + " = " + " entry_" + Id + "[index_" + Id + "];\n");
                }
                else
                {
                    sourceCode.AppendFront(VariableType + " " + NamesOfEntities.Variable(Variable) + " = " + " entry_" + Id + "[index_" + Id + "];\n");
                }
            }
            else if(ContainerType.StartsWith("GRGEN_LIBGR.Deque"))
            {
                sourceCode.AppendFrontFormat("{0} entry_{1} = ({0}) " + NamesOfEntities.Variable(Container) + ";\n", ContainerType, Id);
                sourceCode.AppendFrontFormat("for(int index_{0}=0; index_{0}<entry_{0}.Count; ++index_{0})\n", Id);
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                if(Index != null)
                {
                    sourceCode.AppendFront(IndexType + " " + NamesOfEntities.Variable(Index) + " = index_" + Id + ";\n");
                    sourceCode.AppendFront(VariableType + " " + NamesOfEntities.Variable(Variable) + " = " + " entry_" + Id + "[index_" + Id + "];\n");
                }
                else
                {
                    sourceCode.AppendFront(VariableType + " " + NamesOfEntities.Variable(Variable) + " = " + " entry_" + Id + "[index_" + Id + "];\n");
                }
            }
            else if(ContainerType.StartsWith("Dictionary") && ContainerType.Contains("SetValueType"))
            {
                sourceCode.AppendFrontFormat("foreach(KeyValuePair<{0},GRGEN_LIBGR.SetValueType> entry_{1} in {2})\n", VariableType, Id, NamesOfEntities.Variable(Container));
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                sourceCode.AppendFront(VariableType + " " + NamesOfEntities.Variable(Variable) + " = " + " entry_" + Id + ".Key;\n");
            }
            else
            {
                sourceCode.AppendFrontFormat("foreach(KeyValuePair<{0},{1}> entry_{2} in {3})\n", IndexType, VariableType, Id, NamesOfEntities.Variable(Container));
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                sourceCode.AppendFront(IndexType + " " + NamesOfEntities.Variable(Index) + " = entry_" + Id + ".Key;\n");
                sourceCode.AppendFront(VariableType + " " + NamesOfEntities.Variable(Variable) + " = " + " entry_" + Id + ".Value;\n");
            }

            foreach(Yielding statement in Statements)
            {
                statement.Emit(sourceCode);
            }

            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            foreach(Yielding statement in Statements)
            {
                yield return statement;
            }
        }

        public readonly String Variable;
        public readonly String UnprefixedVariable;
        public readonly String VariableType;
        public readonly String Index;
        public readonly String UnprefixedIndex;
        public readonly String IndexType;
        public readonly String Container;
        public readonly String UnprefixedContainer;
        public readonly String ContainerType;
        readonly Yielding[] Statements;
    }

    /// <summary>
    /// Class representing an integer range iteration yield
    /// </summary>
    public class IntegerRangeIterationYield : Yielding
    {
        public IntegerRangeIterationYield(String variable, String unprefixedVariable, String variableType, 
            Expression left, Expression right, Yielding[] statements)
        {
            Variable = variable;
            UnprefixedVariable = unprefixedVariable;
            VariableType = variableType;
            Left = left;
            Right = right;
            Statements = statements;
        }

        public override Yielding Copy(string renameSuffix)
        {
            Yielding[] statementsCopy = new Yielding[Statements.Length];
            for(int i = 0; i < Statements.Length; ++i)
            {
                statementsCopy[i] = Statements[i].Copy(renameSuffix);
            }
            return new IntegerRangeIterationYield(Variable + renameSuffix, UnprefixedVariable + renameSuffix, VariableType, Left, Right, statementsCopy);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            String ascendingVar = "ascending_" + Id;
            String entryVar = "entry_" + Id;
            String limitVar = "limit_" + Id;
            sourceCode.AppendFront("int " + entryVar + " = (int)(");
            Left.Emit(sourceCode);
            sourceCode.AppendFront(");\n");
            sourceCode.AppendFront("int " + limitVar + " = (int)(");
            Right.Emit(sourceCode);
            sourceCode.AppendFront(");\n");
            sourceCode.AppendFront("bool " + ascendingVar + " = " + entryVar + " <= " + limitVar + ";\n");

            sourceCode.AppendFront("while(" + ascendingVar + " ? " + entryVar + " <= " + limitVar + " : " + entryVar + " >= " + limitVar + ")\n");
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            sourceCode.AppendFront(VariableType + " " + NamesOfEntities.Variable(Variable) + " = " + entryVar + ";\n");

            foreach(Yielding statement in Statements)
            {
                statement.Emit(sourceCode);
            }

            sourceCode.AppendFront("if(" + ascendingVar + ") ++" + entryVar + "; else --" + entryVar + ";\n");

            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Left;
            yield return Right;
            foreach(Yielding statement in Statements)
            {
                yield return statement;
            }
        }

        public readonly String Variable;
        public readonly String UnprefixedVariable;
        public readonly String VariableType;
        public readonly Expression Left;
        public readonly Expression Right;
        readonly Yielding[] Statements;
    }

    /// <summary>
    /// Class representing an iteration over helper function results (nodes/edgse/incident/adjacent/reachable stuff)
    /// </summary>
    public class ForFunction : Yielding
    {
        public ForFunction(String variable, String unprefixedVariable, String variableType, Expression function, Yielding[] statements)
        {
            Variable = variable;
            UnprefixedVariable = unprefixedVariable;
            VariableType = variableType;
            Function = function;
            Statements = statements;
        }

        public override Yielding Copy(string renameSuffix)
        {
            Yielding[] statementsCopy = new Yielding[Statements.Length];
            for(int i = 0; i < Statements.Length; ++i)
            {
                statementsCopy[i] = Statements[i].Copy(renameSuffix);
            }
            return new ForFunction(Variable + renameSuffix, UnprefixedVariable + renameSuffix, VariableType, Function.Copy(renameSuffix), statementsCopy);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(Function is Adjacent)
            {
                Adjacent adjacent = (Adjacent)Function;
                sourceCode.AppendFront("GRGEN_LIBGR.INode node_" + Id + " = ");
                adjacent.Node.Emit(sourceCode);
                sourceCode.Append(";\n");
                if(!Profiling)
                {
                    sourceCode.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge edge_{0} in node_{0}.GetCompatibleIncident(", Id);
                    adjacent.IncidentEdgeType.Emit(sourceCode);
                    sourceCode.Append("))\n");
                }
                else
                {
                    sourceCode.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge edge_{0} in node_{0}.Incident)\n", Id);
                }
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                if(Profiling)
                {
                    if(Parallel)
                        sourceCode.AppendFront("++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];\n");
                    else
                        sourceCode.AppendFront("++actionEnv.PerformanceInfo.SearchSteps;\n");
                    sourceCode.AppendFrontFormat("if(!edge_{0}.InstanceOf(", Id);
                    adjacent.IncidentEdgeType.Emit(sourceCode);
                    sourceCode.Append("))\n");
                    sourceCode.AppendFrontIndented("continue;\n");
                }

                sourceCode.AppendFrontFormat("if(!edge_{0}.Opposite(node_{0}).InstanceOf(", Id);
                adjacent.AdjacentNodeType.Emit(sourceCode);
                sourceCode.Append("))\n");
                sourceCode.AppendFrontIndented("continue;\n");
                sourceCode.AppendFrontFormat("{0} {1} = ({0})edge_{2}.Opposite(node_{2});\n", VariableType, NamesOfEntities.Variable(Variable), Id);
            }
            else if(Function is AdjacentIncoming)
            {
                AdjacentIncoming adjacent = (AdjacentIncoming)Function;
                sourceCode.AppendFront("GRGEN_LIBGR.INode node_" + Id + " = ");
                adjacent.Node.Emit(sourceCode);
                sourceCode.Append(";\n");
                if(!Profiling)
                {
                    sourceCode.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge edge_{0} in node_{0}.GetCompatibleIncoming(", Id);
                    adjacent.IncidentEdgeType.Emit(sourceCode);
                    sourceCode.Append("))\n");
                }
                else
                {
                    sourceCode.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge edge_{0} in node_{0}.Incoming)\n", Id);
                }
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                if(Profiling)
                {
                    if(Parallel)
                        sourceCode.AppendFront("++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];\n");
                    else
                        sourceCode.AppendFront("++actionEnv.PerformanceInfo.SearchSteps;\n");
                    sourceCode.AppendFrontFormat("if(!edge_{0}.InstanceOf(", Id);
                    adjacent.IncidentEdgeType.Emit(sourceCode);
                    sourceCode.Append("))\n");
                    sourceCode.AppendFrontIndented("continue;\n");
                }

                sourceCode.AppendFrontFormat("if(!edge_{0}.Source.InstanceOf(", Id);
                adjacent.AdjacentNodeType.Emit(sourceCode);
                sourceCode.Append("))\n");
                sourceCode.AppendFrontIndented("continue;\n");
                sourceCode.AppendFrontFormat("{0} {1} = ({0})edge_{2}.Source;\n", VariableType, NamesOfEntities.Variable(Variable), Id);
            }
            else if(Function is AdjacentOutgoing)
            {
                AdjacentOutgoing adjacent = (AdjacentOutgoing)Function;
                sourceCode.AppendFront("GRGEN_LIBGR.INode node_" + Id + " = ");
                adjacent.Node.Emit(sourceCode);
                sourceCode.Append(";\n");
                if(!Profiling)
                {
                    sourceCode.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge edge_{0} in node_{0}.GetCompatibleOutgoing(", Id);
                    adjacent.IncidentEdgeType.Emit(sourceCode);
                    sourceCode.Append("))\n");
                }
                else
                {
                    sourceCode.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge edge_{0} in node_{0}.Outgoing)\n", Id);
                }
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                if(Profiling)
                {
                    if(Parallel)
                        sourceCode.AppendFront("++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];\n");
                    else
                        sourceCode.AppendFront("++actionEnv.PerformanceInfo.SearchSteps;\n");
                    sourceCode.AppendFrontFormat("if(!edge_{0}.InstanceOf(", Id);
                    adjacent.IncidentEdgeType.Emit(sourceCode);
                    sourceCode.Append("))\n");
                    sourceCode.AppendFrontIndented("continue;\n");
                }

                sourceCode.AppendFrontFormat("if(!edge_{0}.Target.InstanceOf(", Id);
                adjacent.AdjacentNodeType.Emit(sourceCode);
                sourceCode.Append("))\n");
                sourceCode.AppendFrontIndented("continue;\n");
                sourceCode.AppendFrontFormat("{0} {1} = ({0})edge_{2}.Target;\n", VariableType, NamesOfEntities.Variable(Variable), Id);
            }
            else if(Function is Incident)
            {
                Incident incident = (Incident)Function;
                sourceCode.AppendFront("GRGEN_LIBGR.INode node_" + Id + " = ");
                incident.Node.Emit(sourceCode);
                sourceCode.Append(";\n");
                if(!Profiling)
                {
                    sourceCode.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge edge_{0} in node_{0}.GetCompatibleIncident(", Id);
                    incident.IncidentEdgeType.Emit(sourceCode);
                    sourceCode.Append("))\n");
                }
                else
                {
                    sourceCode.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge edge_{0} in node_{0}.Incident)\n", Id);
                }
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                if(Profiling)
                {
                    if(Parallel)
                        sourceCode.AppendFront("++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];\n");
                    else
                        sourceCode.AppendFront("++actionEnv.PerformanceInfo.SearchSteps;\n");
                    sourceCode.AppendFrontFormat("if(!edge_{0}.InstanceOf(", Id);
                    incident.IncidentEdgeType.Emit(sourceCode);
                    sourceCode.Append("))\n");
                    sourceCode.AppendFrontIndented("continue;\n");
                }

                sourceCode.AppendFrontFormat("if(!edge_{0}.Opposite(node_{0}).InstanceOf(", Id);
                incident.AdjacentNodeType.Emit(sourceCode);
                sourceCode.Append("))\n");
                sourceCode.AppendFrontIndented("continue;\n");
                sourceCode.AppendFrontFormat("{0} {1} = ({0})edge_{2};\n", VariableType, NamesOfEntities.Variable(Variable), Id);
            }
            else if(Function is Incoming)
            {
                Incoming incident = (Incoming)Function;
                sourceCode.AppendFront("GRGEN_LIBGR.INode node_" + Id + " = ");
                incident.Node.Emit(sourceCode);
                sourceCode.Append(";\n");
                if(!Profiling)
                {
                    sourceCode.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge edge_{0} in node_{0}.GetCompatibleIncoming(", Id);
                    incident.IncidentEdgeType.Emit(sourceCode);
                    sourceCode.Append("))\n");
                }
                else
                {
                    sourceCode.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge edge_{0} in node_{0}.Incoming)\n", Id);
                }
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                if(Profiling)
                {
                    if(Parallel)
                        sourceCode.AppendFront("++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];\n");
                    else
                        sourceCode.AppendFront("++actionEnv.PerformanceInfo.SearchSteps;\n");
                    sourceCode.AppendFrontFormat("if(!edge_{0}.InstanceOf(", Id);
                    incident.IncidentEdgeType.Emit(sourceCode);
                    sourceCode.Append("))\n");
                    sourceCode.AppendFrontIndented("continue;\n");
                }

                sourceCode.AppendFrontFormat("if(!edge_{0}.Source.InstanceOf(", Id);
                incident.AdjacentNodeType.Emit(sourceCode);
                sourceCode.Append("))\n");
                sourceCode.AppendFrontIndented("continue;\n");
                sourceCode.AppendFrontFormat("{0} {1} = ({0})edge_{2};\n", VariableType, NamesOfEntities.Variable(Variable), Id);
            }
            else if(Function is Outgoing)
            {
                Outgoing incident = (Outgoing)Function;
                sourceCode.AppendFront("GRGEN_LIBGR.INode node_" + Id + " = ");
                incident.Node.Emit(sourceCode);
                sourceCode.Append(";\n");
                if(!Profiling)
                {
                    sourceCode.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge edge_{0} in node_{0}.GetCompatibleOutgoing(", Id);
                    incident.IncidentEdgeType.Emit(sourceCode);
                    sourceCode.Append("))\n");
                }
                else
                {
                    sourceCode.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge edge_{0} in node_{0}.Outgoing)\n", Id);
                }
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                if(Profiling)
                {
                    if(Parallel)
                        sourceCode.AppendFront("++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];\n");
                    else
                        sourceCode.AppendFront("++actionEnv.PerformanceInfo.SearchSteps;\n");
                    sourceCode.AppendFrontFormat("if(!edge_{0}.InstanceOf(", Id);
                    incident.IncidentEdgeType.Emit(sourceCode);
                    sourceCode.Append("))\n");
                    sourceCode.AppendFrontIndented("continue;\n");
                }

                sourceCode.AppendFrontFormat("if(!edge_{0}.Target.InstanceOf(", Id);
                incident.AdjacentNodeType.Emit(sourceCode);
                sourceCode.Append("))\n");
                sourceCode.AppendFrontIndented("continue;\n");
                sourceCode.AppendFrontFormat("{0} {1} = ({0})edge_{2};\n", VariableType, NamesOfEntities.Variable(Variable), Id);
            }
            else if(Function is Reachable)
            {
                Reachable reachable = (Reachable)Function;
                sourceCode.AppendFront("GRGEN_LIBGR.INode node_" + Id + " = ");
                reachable.Node.Emit(sourceCode);
                sourceCode.Append(";\n");
                sourceCode.AppendFrontFormat("foreach(GRGEN_LIBGR.INode iter_{0} in GRGEN_LIBGR.GraphHelper.Reachable(node_{0}, ", Id);
                reachable.IncidentEdgeType.Emit(sourceCode);
                sourceCode.Append(", ");
                reachable.AdjacentNodeType.Emit(sourceCode);
                sourceCode.Append(", ");
                sourceCode.Append("graph");
                if(Profiling)
                    sourceCode.Append(", actionEnv");
                if(Parallel)
                    sourceCode.Append(", threadId"); 
                sourceCode.Append("))\n");
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                sourceCode.AppendFrontFormat("{0} {1} = ({0})iter_{2};\n", VariableType, NamesOfEntities.Variable(Variable), Id);
            }
            else if(Function is ReachableIncoming)
            {
                ReachableIncoming reachable = (ReachableIncoming)Function;
                sourceCode.AppendFront("GRGEN_LIBGR.INode node_" + Id + " = ");
                reachable.Node.Emit(sourceCode);
                sourceCode.Append(";\n");
                sourceCode.AppendFrontFormat("foreach(GRGEN_LIBGR.INode iter_{0} in GRGEN_LIBGR.GraphHelper.ReachableIncoming(node_{0}, ", Id);
                reachable.IncidentEdgeType.Emit(sourceCode);
                sourceCode.Append(", ");
                reachable.AdjacentNodeType.Emit(sourceCode);
                sourceCode.Append(", ");
                sourceCode.Append("graph");
                if(Profiling)
                    sourceCode.Append(", actionEnv");
                if(Parallel)
                    sourceCode.Append(", threadId");
                sourceCode.Append("))\n");
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                sourceCode.AppendFrontFormat("{0} {1} = ({0})iter_{2};\n", VariableType, NamesOfEntities.Variable(Variable), Id);
            }
            else if(Function is ReachableOutgoing)
            {
                ReachableOutgoing reachable = (ReachableOutgoing)Function;
                sourceCode.AppendFront("GRGEN_LIBGR.INode node_" + Id + " = ");
                reachable.Node.Emit(sourceCode);
                sourceCode.Append(";\n");
                sourceCode.AppendFrontFormat("foreach(GRGEN_LIBGR.INode iter_{0} in GRGEN_LIBGR.GraphHelper.ReachableOutgoing(node_{0}, ", Id);
                reachable.IncidentEdgeType.Emit(sourceCode);
                sourceCode.Append(", ");
                reachable.AdjacentNodeType.Emit(sourceCode);
                sourceCode.Append(", ");
                sourceCode.Append("graph");
                if(Profiling)
                    sourceCode.Append(", actionEnv");
                if(Parallel)
                    sourceCode.Append(", threadId");
                sourceCode.Append("))\n");
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                sourceCode.AppendFrontFormat("{0} {1} = ({0})iter_{2};\n", VariableType, NamesOfEntities.Variable(Variable), Id);
            }
            else if(Function is ReachableEdges)
            {
                ReachableEdges reachable = (ReachableEdges)Function;
                sourceCode.AppendFront("GRGEN_LIBGR.INode node_" + Id + " = ");
                reachable.Node.Emit(sourceCode);
                sourceCode.Append(";\n");
                sourceCode.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge edge_{0} in GRGEN_LIBGR.GraphHelper.ReachableEdges(node_{0}, ", Id);
                reachable.IncidentEdgeType.Emit(sourceCode);
                sourceCode.Append(", ");
                reachable.AdjacentNodeType.Emit(sourceCode);
                sourceCode.Append(", ");
                sourceCode.Append("graph");
                if(Profiling)
                    sourceCode.Append(", actionEnv");
                if(Parallel)
                    sourceCode.Append(", threadId");
                sourceCode.Append("))\n");
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                sourceCode.AppendFrontFormat("{0} {1} = ({0})edge_{2};\n", VariableType, NamesOfEntities.Variable(Variable), Id);
            }
            else if(Function is ReachableEdgesIncoming)
            {
                ReachableEdgesIncoming reachable = (ReachableEdgesIncoming)Function;
                sourceCode.AppendFront("GRGEN_LIBGR.INode node_" + Id + " = ");
                reachable.Node.Emit(sourceCode);
                sourceCode.Append(";\n");
                sourceCode.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge edge_{0} in GRGEN_LIBGR.GraphHelper.ReachableEdgesIncoming(node_{0}, ", Id);
                reachable.IncidentEdgeType.Emit(sourceCode);
                sourceCode.Append(", ");
                reachable.AdjacentNodeType.Emit(sourceCode);
                sourceCode.Append(", ");
                sourceCode.Append("graph");
                if(Profiling)
                    sourceCode.Append(", actionEnv");
                if(Parallel)
                    sourceCode.Append(", threadId");
                sourceCode.Append("))\n");
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                sourceCode.AppendFrontFormat("{0} {1} = ({0})edge_{2};\n", VariableType, NamesOfEntities.Variable(Variable), Id);
            }
            else if(Function is ReachableEdgesOutgoing)
            {
                ReachableEdgesOutgoing reachable = (ReachableEdgesOutgoing)Function;
                sourceCode.AppendFront("GRGEN_LIBGR.INode node_" + Id + " = ");
                reachable.Node.Emit(sourceCode);
                sourceCode.Append(";\n");
                sourceCode.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge edge_{0} in GRGEN_LIBGR.GraphHelper.ReachableEdgesOutgoing(node_{0}, ", Id);
                reachable.IncidentEdgeType.Emit(sourceCode);
                sourceCode.Append(", ");
                reachable.AdjacentNodeType.Emit(sourceCode);
                sourceCode.Append(", ");
                sourceCode.Append("graph");
                if(Profiling)
                    sourceCode.Append(", actionEnv");
                if(Parallel)
                    sourceCode.Append(", threadId");
                sourceCode.Append("))\n");
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                sourceCode.AppendFrontFormat("{0} {1} = ({0})edge_{2};\n", VariableType, NamesOfEntities.Variable(Variable), Id);
            }
            else if(Function is BoundedReachable)
            {
                BoundedReachable reachable = (BoundedReachable)Function;
                sourceCode.AppendFront("GRGEN_LIBGR.INode node_" + Id + " = ");
                reachable.Node.Emit(sourceCode);
                sourceCode.Append(";\n");
                sourceCode.AppendFrontFormat("foreach(GRGEN_LIBGR.INode iter_{0} in GRGEN_LIBGR.GraphHelper.BoundedReachable(node_{0}, ", Id);
                reachable.IncidentEdgeType.Emit(sourceCode);
                sourceCode.Append(", ");
                reachable.AdjacentNodeType.Emit(sourceCode);
                sourceCode.Append(", ");
                sourceCode.Append("graph");
                if(Profiling)
                    sourceCode.Append(", actionEnv");
                if(Parallel)
                    sourceCode.Append(", threadId");
                sourceCode.Append("))\n");
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                sourceCode.AppendFrontFormat("{0} {1} = ({0})iter_{2};\n", VariableType, NamesOfEntities.Variable(Variable), Id);
            }
            else if(Function is BoundedReachableIncoming)
            {
                BoundedReachableIncoming reachable = (BoundedReachableIncoming)Function;
                sourceCode.AppendFront("GRGEN_LIBGR.INode node_" + Id + " = ");
                reachable.Node.Emit(sourceCode);
                sourceCode.Append(";\n");
                sourceCode.AppendFrontFormat("foreach(GRGEN_LIBGR.INode iter_{0} in GRGEN_LIBGR.GraphHelper.BoundedReachableIncoming(node_{0}, ", Id);
                reachable.IncidentEdgeType.Emit(sourceCode);
                sourceCode.Append(", ");
                reachable.AdjacentNodeType.Emit(sourceCode);
                sourceCode.Append(", ");
                sourceCode.Append("graph");
                if(Profiling)
                    sourceCode.Append(", actionEnv");
                if(Parallel)
                    sourceCode.Append(", threadId");
                sourceCode.Append("))\n");
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                sourceCode.AppendFrontFormat("{0} {1} = ({0})iter_{2};\n", VariableType, NamesOfEntities.Variable(Variable), Id);
            }
            else if(Function is BoundedReachableOutgoing)
            {
                BoundedReachableOutgoing reachable = (BoundedReachableOutgoing)Function;
                sourceCode.AppendFront("GRGEN_LIBGR.INode node_" + Id + " = ");
                reachable.Node.Emit(sourceCode);
                sourceCode.Append(";\n");
                sourceCode.AppendFrontFormat("foreach(GRGEN_LIBGR.INode iter_{0} in GRGEN_LIBGR.GraphHelper.BoundedReachableOutgoing(node_{0}, ", Id);
                reachable.IncidentEdgeType.Emit(sourceCode);
                sourceCode.Append(", ");
                reachable.AdjacentNodeType.Emit(sourceCode);
                sourceCode.Append(", ");
                sourceCode.Append("graph");
                if(Profiling)
                    sourceCode.Append(", actionEnv");
                if(Parallel)
                    sourceCode.Append(", threadId");
                sourceCode.Append("))\n");
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                sourceCode.AppendFrontFormat("{0} {1} = ({0})iter_{2};\n", VariableType, NamesOfEntities.Variable(Variable), Id);
            }
            else if(Function is BoundedReachableEdges)
            {
                BoundedReachableEdges reachable = (BoundedReachableEdges)Function;
                sourceCode.AppendFront("GRGEN_LIBGR.INode node_" + Id + " = ");
                reachable.Node.Emit(sourceCode);
                sourceCode.Append(";\n");
                sourceCode.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge edge_{0} in GRGEN_LIBGR.GraphHelper.BoundedReachableEdges(node_{0}, ", Id);
                reachable.IncidentEdgeType.Emit(sourceCode);
                sourceCode.Append(", ");
                reachable.AdjacentNodeType.Emit(sourceCode);
                sourceCode.Append(", ");
                sourceCode.Append("graph");
                if(Profiling)
                    sourceCode.Append(", actionEnv");
                if(Parallel)
                    sourceCode.Append(", threadId");
                sourceCode.Append("))\n");
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                sourceCode.AppendFrontFormat("{0} {1} = ({0})edge_{2};\n", VariableType, NamesOfEntities.Variable(Variable), Id);
            }
            else if(Function is BoundedReachableEdgesIncoming)
            {
                BoundedReachableEdgesIncoming reachable = (BoundedReachableEdgesIncoming)Function;
                sourceCode.AppendFront("GRGEN_LIBGR.INode node_" + Id + " = ");
                reachable.Node.Emit(sourceCode);
                sourceCode.Append(";\n");
                sourceCode.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge edge_{0} in GRGEN_LIBGR.GraphHelper.BoundedReachableEdgesIncoming(node_{0}, ", Id);
                reachable.IncidentEdgeType.Emit(sourceCode);
                sourceCode.Append(", ");
                reachable.AdjacentNodeType.Emit(sourceCode);
                sourceCode.Append(", ");
                sourceCode.Append("graph");
                if(Profiling)
                    sourceCode.Append(", actionEnv");
                if(Parallel)
                    sourceCode.Append(", threadId");
                sourceCode.Append("))\n");
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                sourceCode.AppendFrontFormat("{0} {1} = ({0})edge_{2};\n", VariableType, NamesOfEntities.Variable(Variable), Id);
            }
            else if(Function is BoundedReachableEdgesOutgoing)
            {
                BoundedReachableEdgesOutgoing reachable = (BoundedReachableEdgesOutgoing)Function;
                sourceCode.AppendFront("GRGEN_LIBGR.INode node_" + Id + " = ");
                reachable.Node.Emit(sourceCode);
                sourceCode.Append(";\n");
                sourceCode.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge edge_{0} in GRGEN_LIBGR.GraphHelper.BoundedReachableEdgesOutgoing(node_{0}, ", Id);
                reachable.IncidentEdgeType.Emit(sourceCode);
                sourceCode.Append(", ");
                reachable.AdjacentNodeType.Emit(sourceCode);
                sourceCode.Append(", ");
                sourceCode.Append("graph");
                if(Profiling)
                    sourceCode.Append(", actionEnv");
                if(Parallel)
                    sourceCode.Append(", threadId");
                sourceCode.Append("))\n");
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                sourceCode.AppendFrontFormat("{0} {1} = ({0})edge_{2};\n", VariableType, NamesOfEntities.Variable(Variable), Id);
            }
            else if(Function is Nodes)
            {
                Nodes nodes = (Nodes)Function;
                sourceCode.AppendFrontFormat("foreach(GRGEN_LIBGR.INode node_{0} in graph.GetCompatibleNodes(", Id);
                nodes.NodeType.Emit(sourceCode);
                sourceCode.Append("))\n");
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                if(Profiling)
                {
                    if(Parallel)
                        sourceCode.AppendFront("++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];\n");
                    else
                        sourceCode.AppendFront("++actionEnv.PerformanceInfo.SearchSteps;\n");
                }

                sourceCode.AppendFront(VariableType + " " + NamesOfEntities.Variable(Variable) + " = (" + VariableType + ") node_" + Id + ";\n");
            }
            else if(Function is Edges)
            {
                Edges edges = (Edges)Function;
                sourceCode.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge edge_{0} in graph.GetCompatibleEdges(", Id);
                edges.EdgeType.Emit(sourceCode);
                sourceCode.Append("))\n");
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                if(Profiling)
                {
                    if(Parallel)
                        sourceCode.AppendFront("++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];\n");
                    else
                        sourceCode.AppendFront("++actionEnv.PerformanceInfo.SearchSteps;\n");
                }

                sourceCode.AppendFront(VariableType + " " + NamesOfEntities.Variable(Variable) + " = (" + VariableType + ") edge_" + Id + ";\n");
            }

            foreach(Yielding statement in Statements)
            {
                statement.Emit(sourceCode);
            }

            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            // the Function is not an independent child, it's just simpler/more consistent to reuse some parts of it here
            foreach(Yielding statement in Statements)
            {
                yield return statement;
            }
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        public readonly String Variable;
        public readonly String UnprefixedVariable;
        public readonly String VariableType;
        public readonly Expression Function;
        readonly Yielding[] Statements;
        bool Parallel;
        bool Profiling;
    }

    /// <summary>
    /// Class representing an iteration over index based on equality comparison
    /// </summary>
    public class ForIndexAccessEquality : Yielding
    {
        public ForIndexAccessEquality(String indexSetType, IndexDescription index, String variable, String unprefixedVariable, String variableType, Expression expr, Yielding[] statements)
        {
            IndexSetType = indexSetType;
            Index = index;
            Variable = variable;
            UnprefixedVariable = unprefixedVariable;
            VariableType = variableType;
            Expr = expr;
            Statements = statements;
        }

        public override Yielding Copy(string renameSuffix)
        {
            Yielding[] statementsCopy = new Yielding[Statements.Length];
            for(int i = 0; i < Statements.Length; ++i)
            {
                statementsCopy[i] = Statements[i].Copy(renameSuffix);
            }
            return new ForIndexAccessEquality(IndexSetType, Index, Variable + renameSuffix, UnprefixedVariable + renameSuffix, VariableType, 
                Expr.Copy(renameSuffix), statementsCopy);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFrontFormat("foreach({0} {1} in (({2})graph.Indices).{3}.Lookup(", 
                VariableType, NamesOfEntities.Variable(Variable), IndexSetType, Index.Name);
            Expr.Emit(sourceCode);
            sourceCode.Append("))\n");
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            if(Profiling)
            {
                if(Parallel)
                    sourceCode.AppendFront("++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];\n");
                else
                    sourceCode.AppendFront("++actionEnv.PerformanceInfo.SearchSteps;\n");
            }

            foreach(Yielding statement in Statements)
            {
                statement.Emit(sourceCode);
            }

            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            foreach(Yielding statement in Statements)
            {
                yield return statement;
            }
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        public readonly String IndexSetType;
        public readonly IndexDescription Index;
        public readonly String Variable;
        public readonly String UnprefixedVariable;
        public readonly String VariableType;
        public readonly Expression Expr;
        readonly Yielding[] Statements;
        bool Parallel;
        bool Profiling;
    }

    /// <summary>
    /// Class representing an iteration over index based on ordering comparison
    /// </summary>
    public class ForIndexAccessOrdering : Yielding
    {
        public ForIndexAccessOrdering(String indexSetType, IndexDescription index, String variable, String unprefixedVariable, String variableType, 
            bool ascending, bool includingFrom, bool includingTo,
            Expression from, Expression to, Yielding[] statements)
        {
            IndexSetType = indexSetType;
            Index = index;
            Variable = variable;
            UnprefixedVariable = unprefixedVariable;
            VariableType = variableType;
            Ascending = ascending;
            IncludingFrom = includingFrom;
            IncludingTo = includingTo;
            From = from;
            To = to;
            Statements = statements;
        }

        public override Yielding Copy(string renameSuffix)
        {
            Yielding[] statementsCopy = new Yielding[Statements.Length];
            for(int i = 0; i < Statements.Length; ++i)
            {
                statementsCopy[i] = Statements[i].Copy(renameSuffix);
            }
            return new ForIndexAccessOrdering(IndexSetType, Index, Variable + renameSuffix, UnprefixedVariable + renameSuffix, VariableType, 
                Ascending, IncludingFrom, IncludingTo, From!=null ? From.Copy(renameSuffix) : null, To!=null ? To.Copy(renameSuffix) : null, 
                statementsCopy);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFrontFormat("foreach({0} {1} in (({2})graph.Indices).{3}.Lookup",
                VariableType, NamesOfEntities.Variable(Variable), IndexSetType, Index.Name);

            if(Ascending)
                sourceCode.Append("Ascending");
            else
                sourceCode.Append("Descending");
            if(From != null && To != null)
            {
                sourceCode.Append("From");
                if(IncludingFrom)
                    sourceCode.Append("Inclusive");
                else
                    sourceCode.Append("Exclusive");
                sourceCode.Append("To");
                if(IncludingTo)
                    sourceCode.Append("Inclusive");
                else
                    sourceCode.Append("Exclusive");
                sourceCode.Append("(");
                From.Emit(sourceCode); ;
                sourceCode.Append(", ");
                To.Emit(sourceCode); ;
            }
            else if(From != null)
            {
                sourceCode.Append("From");
                if(IncludingFrom)
                    sourceCode.Append("Inclusive");
                else
                    sourceCode.Append("Exclusive");
                sourceCode.Append("(");
                From.Emit(sourceCode); ;
            }
            else if(To != null)
            {
                sourceCode.Append("To");
                if(IncludingTo)
                    sourceCode.Append("Inclusive");
                else
                    sourceCode.Append("Exclusive");
                sourceCode.Append("(");
                To.Emit(sourceCode); ;
            }
            else
            {
                sourceCode.Append("(");
            }

            sourceCode.Append("))\n");
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            if(Profiling)
            {
                if(Parallel)
                    sourceCode.AppendFront("++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];\n");
                else
                    sourceCode.AppendFront("++actionEnv.PerformanceInfo.SearchSteps;\n");
            }

            foreach(Yielding statement in Statements)
            {
                statement.Emit(sourceCode);
            }

            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            // the Function is not an independent child, it's just simpler/more consistent to reuse some parts of it here
            foreach(Yielding statement in Statements)
            {
                yield return statement;
            }
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        public readonly String IndexSetType;
        public readonly IndexDescription Index;
        public readonly String Variable;
        public readonly String UnprefixedVariable;
        public readonly String VariableType;
        public readonly bool Ascending;
        public readonly bool IncludingFrom;
        public readonly bool IncludingTo;
        public readonly Expression From;
        public readonly Expression To;
        readonly Yielding[] Statements;
        bool Parallel;
        bool Profiling;
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
            {
                trueCaseStatementsCopy[i] = TrueCaseStatements[i].Copy(renameSuffix);
            }
            Yielding[] falseCaseStatementsCopy = null;
            if(FalseCaseStatements != null)
            {
                falseCaseStatementsCopy = new Yielding[FalseCaseStatements.Length];
                for(int i = 0; i < FalseCaseStatements.Length; ++i)
                {
                    falseCaseStatementsCopy[i] = FalseCaseStatements[i].Copy(renameSuffix);
                }
            }
            return new ConditionStatement(Condition.Copy(renameSuffix), trueCaseStatementsCopy, falseCaseStatementsCopy);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFront("if(");
            Condition.Emit(sourceCode);
            sourceCode.Append(") {\n");
            foreach(Yielding statement in TrueCaseStatements)
            {
                statement.Emit(sourceCode);
            }

            if(FalseCaseStatements != null)
            {
                sourceCode.AppendFront("} else {\n");
                foreach(Yielding statement in FalseCaseStatements)
                {
                    statement.Emit(sourceCode);
                }
            }
            sourceCode.AppendFront("}\n");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Condition;
            foreach(Yielding statement in TrueCaseStatements)
            {
                yield return statement;
            }
            if(FalseCaseStatements != null)
            {
                foreach(Yielding statement in FalseCaseStatements)
                {
                    yield return statement;
                }
            }
        }

        readonly Expression Condition;
        readonly Yielding[] TrueCaseStatements;
        readonly Yielding[] FalseCaseStatements;
    }

    /// <summary>
    /// Class representing a switch statement
    /// </summary>
    public class SwitchStatement : Yielding
    {
        public SwitchStatement(Expression switchExpression, CaseStatement[] caseStatements)
        {
            SwitchExpression = switchExpression;
            CaseStatements = caseStatements;
        }

        public override Yielding Copy(string renameSuffix)
        {
            CaseStatement[] caseStatementsCopy = new CaseStatement[CaseStatements.Length];
            for(int i = 0; i < CaseStatements.Length; ++i)
            {
                caseStatementsCopy[i] = (CaseStatement)CaseStatements[i].Copy(renameSuffix);
            }
            return new SwitchStatement(SwitchExpression.Copy(renameSuffix), caseStatementsCopy);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFront("switch(");
            SwitchExpression.Emit(sourceCode);
            sourceCode.Append(") {\n");
            foreach(CaseStatement statement in CaseStatements)
            {
                statement.Emit(sourceCode);
            }
            sourceCode.AppendFront("}\n");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return SwitchExpression;
            foreach(Yielding statement in CaseStatements)
            {
                yield return statement;
            }
        }

        readonly Expression SwitchExpression;
        readonly CaseStatement[] CaseStatements;
    }

    /// <summary>
    /// Class representing a case statement within a switch
    /// </summary>
    public class CaseStatement : Yielding
    {
        public CaseStatement(Expression switchExpression, Yielding[] statements)
        {
            CaseConstExpression = switchExpression;
            Statements = statements;
        }

        public override Yielding Copy(string renameSuffix)
        {
            Yielding[] statementsCopy = new Yielding[Statements.Length];
            for(int i = 0; i < Statements.Length; ++i)
            {
                statementsCopy[i] = Statements[i].Copy(renameSuffix);
            }
            return new CaseStatement(CaseConstExpression!=null ? CaseConstExpression.Copy(renameSuffix) : null, statementsCopy);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(CaseConstExpression != null)
            {
                sourceCode.AppendFront("case ");
                CaseConstExpression.Emit(sourceCode);
                sourceCode.Append(": ");
            }
            else
                sourceCode.AppendFront("default: ");
            sourceCode.Append("{\n");
            foreach(Yielding statement in Statements)
            {
                statement.Emit(sourceCode);
            }
            sourceCode.AppendFront("break;\n");
            sourceCode.AppendFront("}\n");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            if(CaseConstExpression!=null)
                yield return CaseConstExpression;
            foreach(Yielding statement in Statements)
            {
                yield return statement;
            }
        }

        readonly Expression CaseConstExpression;
        readonly Yielding[] Statements;
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
            {
                loopedStatementsCopy[i] = LoopedStatements[i].Copy(renameSuffix);
            }
            return new WhileStatement(Condition.Copy(renameSuffix), loopedStatementsCopy);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFront("while(");
            Condition.Emit(sourceCode);
            sourceCode.Append(") {\n");
            foreach(Yielding statement in LoopedStatements)
            {
                statement.Emit(sourceCode);
            }
            sourceCode.AppendFront("}\n");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Condition;
            foreach(Yielding statement in LoopedStatements)
            {
                yield return statement;
            }
        }

        readonly Expression Condition;
        readonly Yielding[] LoopedStatements;
    }

    /// <summary>
    /// Class representing do while statement
    /// </summary>
    public class DoWhileStatement : Yielding
    {
        public DoWhileStatement(Yielding[] loopedStatements, Expression condition)
        {
            LoopedStatements = loopedStatements;
            Condition = condition;
        }

        public override Yielding Copy(string renameSuffix)
        {
            Yielding[] loopedStatementsCopy = new Yielding[LoopedStatements.Length];
            for(int i = 0; i < LoopedStatements.Length; ++i)
            {
                loopedStatementsCopy[i] = LoopedStatements[i].Copy(renameSuffix);
            }
            return new WhileStatement(Condition.Copy(renameSuffix), loopedStatementsCopy);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFront("do {\n");
            foreach(Yielding statement in LoopedStatements)
            {
                statement.Emit(sourceCode);
            }
            sourceCode.AppendFront("} while(");
            Condition.Emit(sourceCode);
            sourceCode.Append(");\n");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            foreach(Yielding statement in LoopedStatements)
            {
                yield return statement;
            }
            yield return Condition;
        }

        readonly Yielding[] LoopedStatements;
        readonly Expression Condition;
    }

    /// <summary>
    /// Class representing multi statement
    /// </summary>
    public class MultiStatement : Yielding
    {
        public MultiStatement(Yielding[] statements)
        {
            Statements = statements;
        }

        public override Yielding Copy(string renameSuffix)
        {
            Yielding[] statementsCopy = new Yielding[Statements.Length];
            for(int i = 0; i < Statements.Length; ++i)
            {
                statementsCopy[i] = Statements[i].Copy(renameSuffix);
            }
            return new MultiStatement(statementsCopy);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            foreach(Yielding statement in Statements)
            {
                statement.Emit(sourceCode);
            }
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            foreach(Yielding statement in Statements)
            {
                yield return statement;
            }
        }

        public readonly Yielding[] Statements;
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
            sourceCode.AppendFront(Type + " " + NamesOfEntities.Variable(Name));
            if(Initialization != null)
            {
                sourceCode.Append(" = ");
                sourceCode.Append("(");
                sourceCode.Append(Type);
                sourceCode.Append(")");
                Initialization.Emit(sourceCode);
            }
            sourceCode.Append(";\n");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            if(Initialization != null)
                yield return Initialization;
        }

        readonly String Name;
        readonly String Type;
        readonly Expression Initialization;
    }

    /// <summary>
    /// Class representing a break statement
    /// </summary>
    public class BreakStatement : Yielding
    {
        public BreakStatement()
        {
        }

        public override Yielding Copy(string renameSuffix)
        {
            return new BreakStatement();
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFront("break;\n");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator() { yield break; }
    }

    /// <summary>
    /// Class representing a continue statement
    /// </summary>
    public class ContinueStatement : Yielding
    {
        public ContinueStatement()
        {
        }

        public override Yielding Copy(string renameSuffix)
        {
            return new ContinueStatement();
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFront("continue;\n");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator() { yield break; }
    }

    /// <summary>
    /// Class representing an emit statement
    /// </summary>
    public class EmitStatement : Yielding
    {
        public EmitStatement(Expression[] values, bool isDebug)
        {
            Values = values;
            IsDebug = isDebug;
        }

        public override Yielding Copy(string renameSuffix)
        {
            Expression[] valuesCopy = new Expression[Values.Length];
            for(int i = 0; i < Values.Length; ++i)
            {
                valuesCopy[i] = Values[i].Copy(renameSuffix);
            }
            return new EmitStatement(valuesCopy, IsDebug);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            String emitVar = "emit_value_" + Id.ToString();
            sourceCode.AppendFront("object " + emitVar + ";\n");
            foreach(Expression value in Values)
            {
                sourceCode.AppendFront(emitVar + " = ");
                value.Emit(sourceCode);
                sourceCode.Append(";\n");
                sourceCode.AppendFront("if(" + emitVar + " != null)\n");
                String emitWriter = IsDebug ? "EmitWriterDebug" : "EmitWriter";
                sourceCode.AppendFront("((GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv)." + emitWriter + ".Write("
                        + "GRGEN_LIBGR.EmitHelper.ToStringNonNull(" + emitVar + ", graph, false, null, null));\n");
            }
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            foreach(Expression expr in Values)
            {
                yield return expr;
            }
        }

        readonly Expression[] Values;
        readonly bool IsDebug;
    }

    /// <summary>
    /// Class representing a debug add (entry) statement
    /// </summary>
    public class DebugAddStatement : Yielding
    {
        public DebugAddStatement(Expression message, Expression[] values)
        {
            Message = message;
            Values = values;
        }

        public override Yielding Copy(string renameSuffix)
        {
            Expression[] valuesCopy = new Expression[Values.Length];
            for(int i = 0; i < Values.Length; ++i)
            {
                valuesCopy[i] = Values[i].Copy(renameSuffix);
            }
            return new DebugAddStatement(Message.Copy(renameSuffix), valuesCopy);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(!SequenceBase.FireDebugEvents)
                return;

            sourceCode.AppendFront("((GRGEN_LGSP.LGSPSubactionAndOutputAdditionEnvironment)actionEnv).DebugEntering(");
            Message.Emit(sourceCode);
            foreach(Expression value in Values)
            {
                sourceCode.Append(", ");
                value.Emit(sourceCode);
            }
            sourceCode.Append(");\n");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Message;
            foreach(Expression expr in Values)
            {
                yield return expr;
            }
        }

        readonly Expression Message;
        readonly Expression[] Values;
    }

    /// <summary>
    /// Class representing a debug rem (exit) statement
    /// </summary>
    public class DebugRemStatement : Yielding
    {
        public DebugRemStatement(Expression message, Expression[] values)
        {
            Message = message;
            Values = values;
        }

        public override Yielding Copy(string renameSuffix)
        {
            Expression[] valuesCopy = new Expression[Values.Length];
            for(int i = 0; i < Values.Length; ++i)
            {
                valuesCopy[i] = Values[i].Copy(renameSuffix);
            }
            return new DebugRemStatement(Message.Copy(renameSuffix), valuesCopy);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(!SequenceBase.FireDebugEvents)
                return;

            sourceCode.AppendFront("((GRGEN_LGSP.LGSPSubactionAndOutputAdditionEnvironment)actionEnv).DebugExiting(");
            Message.Emit(sourceCode);
            foreach(Expression value in Values)
            {
                sourceCode.Append(", ");
                value.Emit(sourceCode);
            }
            sourceCode.Append(");\n");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Message;
            foreach(Expression expr in Values)
            {
                yield return expr;
            }
        }

        readonly Expression Message;
        readonly Expression[] Values;
    }

    /// <summary>
    /// Class representing a debug emit statement
    /// </summary>
    public class DebugEmitStatement : Yielding
    {
        public DebugEmitStatement(Expression message, Expression[] values)
        {
            Message = message;
            Values = values;
        }

        public override Yielding Copy(string renameSuffix)
        {
            Expression[] valuesCopy = new Expression[Values.Length];
            for(int i = 0; i < Values.Length; ++i)
            {
                valuesCopy[i] = Values[i].Copy(renameSuffix);
            }
            return new DebugEmitStatement(Message.Copy(renameSuffix), valuesCopy);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(!SequenceBase.FireDebugEvents)
                return;

            sourceCode.AppendFront("((GRGEN_LGSP.LGSPSubactionAndOutputAdditionEnvironment)actionEnv).DebugEmitting(");
            Message.Emit(sourceCode);
            foreach(Expression value in Values)
            {
                sourceCode.Append(", ");
                value.Emit(sourceCode);
            }
            sourceCode.Append(");\n");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Message;
            foreach(Expression expr in Values)
            {
                yield return expr;
            }
        }

        readonly Expression Message;
        readonly Expression[] Values;
    }

    /// <summary>
    /// Class representing a debug halt statement
    /// </summary>
    public class DebugHaltStatement : Yielding
    {
        public DebugHaltStatement(Expression message, Expression[] values)
        {
            Message = message;
            Values = values;
        }

        public override Yielding Copy(string renameSuffix)
        {
            Expression[] valuesCopy = new Expression[Values.Length];
            for(int i = 0; i < Values.Length; ++i)
            {
                valuesCopy[i] = Values[i].Copy(renameSuffix);
            }
            return new DebugHaltStatement(Message.Copy(renameSuffix), valuesCopy);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(!SequenceBase.FireDebugEvents)
                return;

            sourceCode.AppendFront("((GRGEN_LGSP.LGSPSubactionAndOutputAdditionEnvironment)actionEnv).DebugHalting(");
            Message.Emit(sourceCode);
            foreach(Expression value in Values)
            {
                sourceCode.Append(", ");
                value.Emit(sourceCode);
            }
            sourceCode.Append(");\n");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Message;
            foreach(Expression expr in Values)
            {
                yield return expr;
            }
        }

        readonly Expression Message;
        readonly Expression[] Values;
    }

    /// <summary>
    /// Class representing a debug highlight statement
    /// </summary>
    public class DebugHighlightStatement : Yielding
    {
        public DebugHighlightStatement(Expression message, Expression[] values, Expression[] sourceNames)
        {
            Message = message;
            Values = values;
            SourceNames = sourceNames;
        }

        public override Yielding Copy(string renameSuffix)
        {
            Expression[] valuesCopy = new Expression[Values.Length];
            for(int i = 0; i < Values.Length; ++i)
            {
                valuesCopy[i] = Values[i].Copy(renameSuffix);
            }
            Expression[] sourceNamesCopy = new Expression[SourceNames.Length];
            for(int i = 0; i < SourceNames.Length; ++i)
            {
                sourceNamesCopy[i] = SourceNames[i].Copy(renameSuffix);
            }
            return new DebugHighlightStatement(Message.Copy(renameSuffix), valuesCopy, sourceNamesCopy);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(!SequenceBase.FireDebugEvents)
                return;

            String highlightValuesArray = "highlight_values_" + Id;
		    sourceCode.AppendFront("List<object> " + highlightValuesArray + " = new List<object>();");
    	    String highlightSourceNamesArray = "highlight_source_names_" + Id.ToString();
		    sourceCode.AppendFront("List<string> " + highlightSourceNamesArray + " = new List<string>();");
            foreach(Expression value in Values)
            {
			    sourceCode.AppendFront(highlightValuesArray + ".Add(");
			    value.Emit(sourceCode);
			    sourceCode.Append(");\n");
            }
            foreach(Expression sourceName in SourceNames)
            {
                sourceCode.AppendFront(highlightSourceNamesArray + ".Add((string)");
                sourceName.Emit(sourceCode);
                sourceCode.Append(");\n");
            }
            sourceCode.AppendFront("((GRGEN_LGSP.LGSPSubactionAndOutputAdditionEnvironment)actionEnv).DebugHighlighting(");
            Message.Emit(sourceCode);
            sourceCode.Append(", " + highlightValuesArray + ", " + highlightSourceNamesArray);
            sourceCode.Append(");\n");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Message;
            foreach(Expression expr in Values)
            {
                yield return expr;
            }
            foreach(Expression sourceName in SourceNames)
            {
                yield return sourceName;
            }
        }

        readonly Expression Message;
        readonly Expression[] Values;
        readonly Expression[] SourceNames;
    }

    /// <summary>
    /// Class representing an assert statement
    /// </summary>
    public class AssertStatement : Yielding
    {
        public AssertStatement(Expression[] values, bool isAlways)
        {
            Values = values;
            IsAlways = isAlways;
        }

        public override Yielding Copy(string renameSuffix)
        {
            Expression[] valuesCopy = new Expression[Values.Length];
            for(int i = 0; i < Values.Length; ++i)
            {
                valuesCopy[i] = Values[i].Copy(renameSuffix);
            }
            return new AssertStatement(valuesCopy, IsAlways);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFront("((GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv).UserProxy.HandleAssert(");
            sourceCode.Append(IsAlways ? "true" : "false");
            foreach(Expression value in Values)
            {
                sourceCode.Append(", () => ");
                value.Emit(sourceCode);
            }
            sourceCode.AppendFront(");\n");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            foreach(Expression expr in Values)
            {
                yield return expr;
            }
        }

        readonly Expression[] Values;
        readonly bool IsAlways;
    }

    /// <summary>
    /// Class representing a record statement
    /// </summary>
    public class RecordStatement : Yielding
    {
        public RecordStatement(Expression toRecord)
        {
            ToRecordExpression = toRecord;
        }

        public override Yielding Copy(string renameSuffix)
        {
            return new RecordStatement(ToRecordExpression.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            String recordVar = "emit_value_" + Id.ToString();
            sourceCode.AppendFront("object " + recordVar + " = ");
            ToRecordExpression.Emit(sourceCode);
            sourceCode.Append(";\n");
            sourceCode.AppendFront("if(" + recordVar + " != null)\n");
            sourceCode.AppendFront("((GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv).Recorder.Write("
                    + "GRGEN_LIBGR.EmitHelper.ToStringNonNull(" + recordVar + ", graph, false, null, null));\n");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return ToRecordExpression;
        }

        readonly Expression ToRecordExpression;
    }

    /// <summary>
    /// Class representing an iterated filtering.
    /// </summary>
    public class IteratedFiltering : Yielding
    {
        public IteratedFiltering(String ruleOrSubpatternName, bool isSubpattern, String iteratedName, FilterInvocationBase[] filterInvocations)
        {
            RuleOrSubpatternName = ruleOrSubpatternName;
            IsSubpattern = isSubpattern;
            IteratedName = iteratedName;
            FilterInvocations = filterInvocations;
        }

        public override Yielding Copy(string renameSuffix)
        {
            FilterInvocationBase[] newFilterInvocations = new FilterInvocationBase[FilterInvocations.Length];
            for(int i = 0; i < FilterInvocations.Length; ++i)
            {
                newFilterInvocations[i] = (FilterInvocationBase)FilterInvocations[i].Copy(renameSuffix);
            }
            return new IteratedFiltering(RuleOrSubpatternName, IsSubpattern, IteratedName, newFilterInvocations);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            String matchesSource = "match." + IteratedName;
            foreach(FilterInvocationBase filterInvocation in FilterInvocations)
            {
                filterInvocation.Emit(sourceCode, matchesSource, RuleOrSubpatternName, IsSubpattern, IteratedName);
            }
        }

        public override void EmitLambdaExpressionImplementationMethods(SourceBuilder sourceCode)
        {
            foreach(FilterInvocationBase filterInvocation in FilterInvocations)
            {
                if(filterInvocation is FilterInvocationLambdaExpression)
                    ((FilterInvocationLambdaExpression)filterInvocation).EmitArrayPerElementMethods(sourceCode, RuleOrSubpatternName, IsSubpattern, IteratedName);
            }
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            foreach(FilterInvocationBase filterInvocation in FilterInvocations)
            {
                yield return filterInvocation;
            }
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        String RuleOrSubpatternName;
        bool IsSubpattern;
        String IteratedName;
        FilterInvocationBase[] FilterInvocations;
        public bool Parallel;
    }

    /// <summary>
    /// Class representing a base class for filter invocations applied to an iterated (of regular filters or lambda expression filters).
    /// </summary>
    public abstract class FilterInvocationBase : Yielding
    {
        public override void Emit(SourceBuilder sourceCode)
        {
            throw new NotImplementedException();
        }

        public abstract void Emit(SourceBuilder sourceCode, String matchesSource, String ruleOrSubpatternName, bool IsSubpattern, String iteratedName);
    }

    /// <summary>
    /// Class representing a filter invocation applied to an iterated.
    /// </summary>
    public class FilterInvocation : FilterInvocationBase
    {
        public FilterInvocation(string filterName, bool isAutoSupplied, Expression[] arguments, String[] argumentTypes)
        {
            FilterName = filterName;
            IsAutoSupplied = isAutoSupplied;
            Arguments = arguments;
            ArgumentTypes = argumentTypes;
        }

        public override Yielding Copy(string renameSuffix)
        {
            Expression[] newArguments = new Expression[Arguments.Length];
            for(int i = 0; i < Arguments.Length; ++i)
            {
                newArguments[i] = (Expression)Arguments[i].Copy(renameSuffix);
            }
            return new FilterInvocation(FilterName, IsAutoSupplied, newArguments, (String[])ArgumentTypes.Clone());
        }

        public override void Emit(SourceBuilder sourceCode, String matchesSource, String ruleOrSubpatternName, bool isSubpattern, String iteratedName)
        {
            if(IsAutoSupplied)
            {
                sourceCode.AppendFrontFormat("({0}).Filter_{1}(", matchesSource, FilterName);
                for(int i = 0; i < Arguments.Length; ++i)
                {
                    if(i != 0)
                        sourceCode.Append(", ");
                    Expression argument = Arguments[i];
                    if(ArgumentTypes[i] != null)
                        sourceCode.Append("(" + ArgumentTypes[i] + ")");
                    argument.Emit(sourceCode);
                }
                sourceCode.Append(")\n;");
            }
            else
            {
                sourceCode.AppendFrontFormat("MatchFilters.Filter_{0}_{1}_{2}((GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv, {3});\n",
                    ruleOrSubpatternName, iteratedName, FilterName, matchesSource);
            }
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

        public readonly string FilterName;
        public readonly bool IsAutoSupplied;
        public readonly Expression[] Arguments;
        public readonly String[] ArgumentTypes; // for each argument: if node/edge: the interface type, otherwise: null
        public bool Parallel;
    }

    /// <summary>
    /// Class representing a lambda expression filter invocation applied to an iterated.
    /// </summary>
    public class FilterInvocationLambdaExpression : FilterInvocationBase
    {
        public FilterInvocationLambdaExpression(string filterName, string entity, string matchElementType,
            string initArrayAccessVariable, Expression initExpression,
            string arrayAccessVariable, string previousAccumulationAccessVariable,
            string indexVariable, string elementVariable, Expression lambdaExpression,
            PatternNode[] patternNodes, PatternEdge[] patternEdges, PatternVariable[] patternVariables)
        {
            FilterName = filterName;
            Entity = entity;
            MatchElementType = matchElementType;
            InitArrayAccessVariable = initArrayAccessVariable;
            InitExpression = initExpression;
            ArrayAccessVariable = arrayAccessVariable;
            PreviousAccumulationAccessVariable = previousAccumulationAccessVariable;
            IndexVariable = indexVariable;
            ElementVariable = elementVariable;
            LambdaExpression = lambdaExpression;
            PatternNodes = patternNodes;
            PatternEdges = patternEdges;
            PatternVariables = patternVariables;
        }

        public override Yielding Copy(string renameSuffix)
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
            return new FilterInvocationLambdaExpression(FilterName, Entity, MatchElementType,
                InitArrayAccessVariable, (Expression)InitExpression.Copy(renameSuffix),
                ArrayAccessVariable, PreviousAccumulationAccessVariable,
                IndexVariable, ElementVariable, (Expression)LambdaExpression.Copy(renameSuffix),
                newPatternNodes, newPatternEdges, newPatternVariables);
        }

        public override void Emit(SourceBuilder sourceCode, String matchesSource, String ruleOrSubpatternName, bool isSubpattern, String iteratedName)
        {
            if(FilterName == "assign")
                EmitFilterCallAssign(sourceCode, matchesSource, ruleOrSubpatternName, isSubpattern, iteratedName);
            else if(FilterName == "assignStartWithAccumulateBy")
                EmitFilterCallAssignStartWithAccumulateBy(sourceCode, matchesSource, ruleOrSubpatternName, isSubpattern, iteratedName);
            else if(FilterName == "removeIf")
                EmitFilterCallRemoveIf(sourceCode, matchesSource, ruleOrSubpatternName, isSubpattern, iteratedName);
            else
                throw new Exception("Unknown lambda expression filter call (available are assign, removeIf, assignStartWithAccumulateBy)");
        }

        public void EmitFilterCallAssign(SourceBuilder source, String matchesSource, String ruleOrSubpatternName, bool isSubpattern, String iteratedName)
        {
            String filterAssignMethodName = "FilterAssign_" + Id;

            source.AppendFrontFormat("{0}((GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv", filterAssignMethodName);

            source.Append(", ");
            source.Append(matchesSource);

            foreach(PatternNode patternNode in PatternNodes)
            {
                source.Append(", (");
                source.Append(TypesHelper.TypeName(patternNode.type));
                source.Append(")");
                source.Append(NamesOfEntities.CandidateVariable(patternNode.name));
            }
            foreach(PatternEdge patternEdge in PatternEdges)
            {
                source.Append(", (");
                source.Append(TypesHelper.TypeName(patternEdge.type));
                source.Append(")");
                source.Append(NamesOfEntities.CandidateVariable(patternEdge.name));
            }
            foreach(PatternVariable patternVariable in PatternVariables)
            {
                source.Append(", (");
                source.Append(TypesHelper.TypeName(patternVariable.type));
                source.Append(")");
                source.Append(NamesOfEntities.Variable(patternVariable.name));
            }

            source.Append(")");

            source.Append(";\n");
        }

        public void EmitFilterCallRemoveIf(SourceBuilder source, String matchesSource, String ruleOrSubpatternName, bool isSubpattern, String iteratedName)
        {
            String filterRemoveIfMethodName = "FilterRemoveIf_" + Id;

            source.AppendFrontFormat("{0}((GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv", filterRemoveIfMethodName);

            source.Append(", ");
            source.Append(matchesSource);

            foreach(PatternNode patternNode in PatternNodes)
            {
                source.Append(", (");
                source.Append(TypesHelper.TypeName(patternNode.type));
                source.Append(")");
                source.Append(NamesOfEntities.CandidateVariable(patternNode.name));
            }
            foreach(PatternEdge patternEdge in PatternEdges)
            {
                source.Append(", (");
                source.Append(TypesHelper.TypeName(patternEdge.type));
                source.Append(")");
                source.Append(NamesOfEntities.CandidateVariable(patternEdge.name));
            }
            foreach(PatternVariable patternVariable in PatternVariables)
            {
                source.Append(", (");
                source.Append(TypesHelper.TypeName(patternVariable.type));
                source.Append(")");
                source.Append(NamesOfEntities.Variable(patternVariable.name));
            }

            source.Append(")");

            source.Append(";\n");
        }

        public void EmitFilterCallAssignStartWithAccumulateBy(SourceBuilder source, String matchesSource, String ruleOrSubpatternName, bool isSubpattern, String iteratedName)
        {
            String filterAssignMethodName = "FilterAssignStartWithAccumulateBy_" + Id;

            source.AppendFrontFormat("{0}((GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv", filterAssignMethodName);

            source.Append(", ");
            source.Append(matchesSource);

            foreach(PatternNode patternNode in PatternNodes)
            {
                source.Append(", (");
                source.Append(TypesHelper.TypeName(patternNode.type));
                source.Append(")");
                source.Append(NamesOfEntities.CandidateVariable(patternNode.name));
            }
            foreach(PatternEdge patternEdge in PatternEdges)
            {
                source.Append(", (");
                source.Append(TypesHelper.TypeName(patternEdge.type));
                source.Append(")");
                source.Append(NamesOfEntities.CandidateVariable(patternEdge.name));
            }
            foreach(PatternVariable patternVariable in PatternVariables)
            {
                source.Append(", (");
                source.Append(TypesHelper.TypeName(patternVariable.type));
                source.Append(")");
                source.Append(NamesOfEntities.Variable(patternVariable.name));
            }

            source.Append(")");

            source.Append(";\n");
        }

        public override void EmitLambdaExpressionImplementationMethods(SourceBuilder sourceCode)
        {
            throw new NotImplementedException();
        }

        public void EmitArrayPerElementMethods(SourceBuilder sourceCode, String ruleOrSubpatternName, bool isSubpattern, String iteratedName)
        {
            base.EmitLambdaExpressionImplementationMethods(sourceCode);

            if(FilterName == "assign")
                EmitFilterAssign(sourceCode, ruleOrSubpatternName, isSubpattern, iteratedName);
            else if(FilterName == "assignStartWithAccumulateBy")
                EmitFilterAssignStartWithAccumulateBy(sourceCode, ruleOrSubpatternName, isSubpattern, iteratedName);
            else if(FilterName == "removeIf")
                EmitFilterRemoveIf(sourceCode, ruleOrSubpatternName, isSubpattern, iteratedName);
            else
                throw new Exception("Unknown lambda expression filter call (available are assign, removeIf, assignStartWithAccumulateBy)");
        }

        public void EmitFilterAssign(SourceBuilder sourceCode, String ruleOrSubpatternName, bool isSubpattern, String iteratedName)
        {
            String filterAssignMethodName = "FilterAssign_" + Id;

            String matchType = NamesOfEntities.RulePatternClassName(ruleOrSubpatternName, null, isSubpattern) + "." + NamesOfEntities.MatchInterfaceName(ruleOrSubpatternName, iteratedName);
            String matchesType = "GRGEN_LIBGR.IMatchesExact<" + matchType + ">";
            String arrayType = "List<" + matchType + ">";
            String elementType = matchType;

            sourceCode.AppendFront("static " + matchesType + " " + filterAssignMethodName + "(");
            sourceCode.Append("GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv");

            sourceCode.AppendFormat(", {0} matches", matchesType);

            foreach(PatternNode patternNode in PatternNodes)
            {
                sourceCode.Append(", ");
                sourceCode.Append(TypesHelper.TypeName(patternNode.type));
                sourceCode.Append(" ");
                sourceCode.Append(NamesOfEntities.CandidateVariable(patternNode.name));
            }
            foreach(PatternEdge patternEdge in PatternEdges)
            {
                sourceCode.Append(", ");
                sourceCode.Append(TypesHelper.TypeName(patternEdge.type));
                sourceCode.Append(" ");
                sourceCode.Append(NamesOfEntities.CandidateVariable(patternEdge.name));
            }
            foreach(PatternVariable patternVariable in PatternVariables)
            {
                sourceCode.Append(", ");
                sourceCode.Append(TypesHelper.TypeName(patternVariable.type));
                sourceCode.Append(" ");
                sourceCode.Append(NamesOfEntities.Variable(patternVariable.name));
            }

            sourceCode.Append(")\n");
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            sourceCode.AppendFront("GRGEN_LGSP.LGSPGraph graph = procEnv.graph;\n");

            if(ArrayAccessVariable != null)
            {
                sourceCode.AppendFrontFormat("{0} matchListCopy = new {0}();\n", arrayType);
                sourceCode.AppendFrontFormat("foreach({0} match in matches)\n", elementType);
                sourceCode.AppendFront("{\n");
                sourceCode.AppendFrontIndentedFormat("matchListCopy.Add(({0})match.Clone());\n", elementType);
                sourceCode.AppendFront("}\n");

                sourceCode.AppendFrontFormat("{0} {1} = matchListCopy;\n", arrayType, NamesOfEntities.Variable(ArrayAccessVariable));
            }

            sourceCode.AppendFront("int index = 0;\n");
            sourceCode.AppendFrontFormat("foreach({0} match in matches)\n", elementType);
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            if(IndexVariable != null)
                sourceCode.AppendFront("int " + NamesOfEntities.Variable(IndexVariable) + " = index;\n");
            sourceCode.AppendFront(elementType + " " + NamesOfEntities.Variable(ElementVariable) + " = match;\n");
            sourceCode.AppendFront(MatchElementType + " result = ");
            LambdaExpression.Emit(sourceCode);
            sourceCode.Append(";\n");
            sourceCode.AppendFrontFormat("match.SetMember(\"{0}\", result);\n", Entity); // TODO: directly access entity in match instead, requires name prefix
            sourceCode.AppendFront("++index;\n");

            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");

            sourceCode.AppendFront("return matches;\n");

            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        public void EmitFilterRemoveIf(SourceBuilder sourceCode, String ruleOrSubpatternName, bool isSubpattern, String iteratedName)
        {
            String filterRemoveIfMethodName = "FilterRemoveIf_" + Id;

            String matchType = NamesOfEntities.RulePatternClassName(ruleOrSubpatternName, null, isSubpattern) + "." + NamesOfEntities.MatchInterfaceName(ruleOrSubpatternName, iteratedName);
            String matchesType = "GRGEN_LIBGR.IMatchesExact<" + matchType + ">";
            String arrayType = "List<" + matchType + ">";
            String elementType = matchType;

            sourceCode.AppendFront("static " + matchesType + " " + filterRemoveIfMethodName + "(");
            sourceCode.Append("GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv");

            sourceCode.AppendFormat(", {0} matches", matchesType);

            foreach(PatternNode patternNode in PatternNodes)
            {
                sourceCode.Append(", ");
                sourceCode.Append(TypesHelper.TypeName(patternNode.type));
                sourceCode.Append(" ");
                sourceCode.Append(NamesOfEntities.CandidateVariable(patternNode.name));
            }
            foreach(PatternEdge patternEdge in PatternEdges)
            {
                sourceCode.Append(", ");
                sourceCode.Append(TypesHelper.TypeName(patternEdge.type));
                sourceCode.Append(" ");
                sourceCode.Append(NamesOfEntities.CandidateVariable(patternEdge.name));
            }
            foreach(PatternVariable patternVariable in PatternVariables)
            {
                sourceCode.Append(", ");
                sourceCode.Append(TypesHelper.TypeName(patternVariable.type));
                sourceCode.Append(" ");
                sourceCode.Append(NamesOfEntities.Variable(patternVariable.name));
            }

            sourceCode.Append(")\n");
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            sourceCode.AppendFront("GRGEN_LGSP.LGSPGraph graph = procEnv.graph;\n");
            sourceCode.AppendFront(arrayType + " matchList = matches.ToListExact();\n");

            if(ArrayAccessVariable != null)
            {
                sourceCode.AppendFrontFormat("{0} matchListCopy = new {0}(matchList);\n", arrayType);

                sourceCode.AppendFrontFormat("{0} {1} = matchListCopy;\n", arrayType, NamesOfEntities.Variable(ArrayAccessVariable));
            }

            sourceCode.AppendFront("for(int index = 0; index < matchList.Count; ++index)\n");
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            if(IndexVariable != null)
                sourceCode.AppendFront("int " + NamesOfEntities.Variable(IndexVariable) + " = index;\n");
            sourceCode.AppendFrontFormat("{0} match = matchList[index];\n", elementType);
            sourceCode.AppendFront(elementType + " " + NamesOfEntities.Variable(ElementVariable) + " = match;\n");
            sourceCode.AppendFront("if((bool)(");
            LambdaExpression.Emit(sourceCode);
            sourceCode.Append("))\n");
            sourceCode.AppendFrontIndented("matchList[index] = null;\n");

            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");

            sourceCode.AppendFront("matches.FromListExact();\n");
            sourceCode.AppendFront("return matches;\n");

            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        public void EmitFilterAssignStartWithAccumulateBy(SourceBuilder sourceCode, String ruleOrSubpatternName, bool isSubpattern, String iteratedName)
        {
            String filterAssignMethodName = "FilterAssignStartWithAccumulateBy_" + Id;

            String matchType = NamesOfEntities.RulePatternClassName(ruleOrSubpatternName, null, isSubpattern) + "." + NamesOfEntities.MatchInterfaceName(ruleOrSubpatternName, iteratedName);
            String matchesType = "GRGEN_LIBGR.IMatchesExact<" + matchType + ">";
            String arrayType = "List<" + matchType + ">";
            String elementType = matchType;

            sourceCode.AppendFront("static " + matchesType + " " + filterAssignMethodName + "(");
            sourceCode.Append("GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv");

            sourceCode.AppendFormat(", {0} matches", matchesType);

            foreach(PatternNode patternNode in PatternNodes)
            {
                sourceCode.Append(", ");
                sourceCode.Append(TypesHelper.TypeName(patternNode.type));
                sourceCode.Append(" ");
                sourceCode.Append(NamesOfEntities.CandidateVariable(patternNode.name));
            }
            foreach(PatternEdge patternEdge in PatternEdges)
            {
                sourceCode.Append(", ");
                sourceCode.Append(TypesHelper.TypeName(patternEdge.type));
                sourceCode.Append(" ");
                sourceCode.Append(NamesOfEntities.CandidateVariable(patternEdge.name));
            }
            foreach(PatternVariable patternVariable in PatternVariables)
            {
                sourceCode.Append(", ");
                sourceCode.Append(TypesHelper.TypeName(patternVariable.type));
                sourceCode.Append(" ");
                sourceCode.Append(NamesOfEntities.Variable(patternVariable.name));
            }

            sourceCode.Append(")\n");
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            sourceCode.AppendFront("GRGEN_LGSP.LGSPGraph graph = procEnv.graph;\n");

            if(InitArrayAccessVariable != null || ArrayAccessVariable != null)
            {
                sourceCode.AppendFrontFormat("{0} matchListCopy = new {0}();\n", arrayType);
                sourceCode.AppendFrontFormat("foreach({0} match in matches)\n", elementType);
                sourceCode.AppendFront("{\n");
                sourceCode.AppendFrontIndentedFormat("matchListCopy.Add(({0})match.Clone());\n", elementType);
                sourceCode.AppendFront("}\n");
            }

            if(InitArrayAccessVariable != null)
            {
                sourceCode.AppendFrontFormat("{0} {1} = matchListCopy;\n", arrayType, NamesOfEntities.Variable(InitArrayAccessVariable));
            }

            sourceCode.AppendFrontFormat("{0} {1} = ", MatchElementType, NamesOfEntities.Variable(PreviousAccumulationAccessVariable));
            InitExpression.Emit(sourceCode);
            sourceCode.Append(";\n");

            if(ArrayAccessVariable != null)
            {
                sourceCode.AppendFrontFormat("{0} {1} = matchListCopy;\n", arrayType, NamesOfEntities.Variable(ArrayAccessVariable));
            }

            sourceCode.AppendFront("int index = 0;\n");
            sourceCode.AppendFrontFormat("foreach({0} match in matches)\n", elementType);
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            if(IndexVariable != null)
                sourceCode.AppendFront("int " + NamesOfEntities.Variable(IndexVariable) + " = index;\n");
            sourceCode.AppendFront(elementType + " " + NamesOfEntities.Variable(ElementVariable) + " = match;\n");
            sourceCode.AppendFront(MatchElementType + " result = ");
            LambdaExpression.Emit(sourceCode);
            sourceCode.Append(";\n");
            sourceCode.AppendFrontFormat("match.SetMember(\"{0}\", result);\n", Entity); // TODO: directly access entity in match instead, requires name prefix
            sourceCode.AppendFront("++index;\n");

            sourceCode.AppendFrontFormat("{0} = result;\n", NamesOfEntities.Variable(PreviousAccumulationAccessVariable));

            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");

            sourceCode.AppendFront("return matches;\n");

            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return LambdaExpression;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public readonly string FilterName;
        public readonly string Entity;
        public readonly String MatchElementType;
        public readonly string InitArrayAccessVariable;
        public readonly Expression InitExpression;
        public readonly string ArrayAccessVariable;
        public readonly string PreviousAccumulationAccessVariable;
        public readonly string IndexVariable;
        public readonly string ElementVariable;
        public readonly Expression LambdaExpression;
        readonly PatternNode[] PatternNodes;
        readonly PatternEdge[] PatternEdges;
        readonly PatternVariable[] PatternVariables;
        public bool Parallel;
    }
}

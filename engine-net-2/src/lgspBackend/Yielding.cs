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
    /// Base class of yielding in assignments and expressions
    /// </summary>
    public abstract class Yielding : ExpressionOrYielding
    {
        /// <summary>
        /// copies the yielding, renaming all variables with the given suffix
        /// </summary>
        public abstract Yielding Copy(string renameSuffix);

        public static int fetchId()
        {
            return idGenerator++;
        }

        private static int idGenerator = 0;
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

        String Left;
        bool IsVar;
        String TargetType;
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
            if(Right != null) yield return Right;
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
            if(Right != null) yield return Right;
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
            sourceCode.Append("[");
            Right.Emit(sourceCode);
            sourceCode.Append("] = null");
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
            sourceCode.Append("[");
            Right.Emit(sourceCode);
            sourceCode.Append("] = ");
            Value.Emit(sourceCode);
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

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            if(Index != null) yield return Index;
            yield return Right;
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
                statementsCopy[i] = Statements[i].Copy(renameSuffix);
            if(Index != null)
                return new ContainerAccumulationYield(Variable + renameSuffix, UnprefixedVariable + renameSuffix, VariableType, Index + renameSuffix, UnprefixedIndex + renameSuffix, IndexType, Container, UnprefixedContainer + renameSuffix, ContainerType, statementsCopy);
            else
                return new ContainerAccumulationYield(Variable + renameSuffix, UnprefixedVariable + renameSuffix, VariableType, Container, UnprefixedContainer + renameSuffix, ContainerType, statementsCopy);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            String id = fetchId().ToString();
            if(ContainerType.StartsWith("List"))
            {
                sourceCode.AppendFrontFormat("{0} entry_{1} = ({0}) " + NamesOfEntities.Variable(Container) + ";\n", ContainerType, id);
                sourceCode.AppendFrontFormat("for(int index_{0}=0; index_{0}<entry_{0}.Count; ++index_{0})\n", id);
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                if(Index != null)
                {
                    sourceCode.AppendFront(IndexType + " " + NamesOfEntities.Variable(Index) + " = index_" + id + ";\n");
                    sourceCode.AppendFront(VariableType + " " + NamesOfEntities.Variable(Variable) + " = " + " entry_" + id + "[index_" + id + "];\n");
                }
                else
                {
                    sourceCode.AppendFront(VariableType + " " + NamesOfEntities.Variable(Variable) + " = " + " entry_" + id + "[index_" + id + "];\n");
                }
            }
            else if(ContainerType.StartsWith("GRGEN_LIBGR.Deque"))
            {
                sourceCode.AppendFrontFormat("{0} entry_{1} = ({0}) " + NamesOfEntities.Variable(Container) + ";\n", ContainerType, id);
                sourceCode.AppendFrontFormat("for(int index_{0}=0; index_{0}<entry_{0}.Count; ++index_{0})\n", id);
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                if(Index != null)
                {
                    sourceCode.AppendFront(IndexType + " " + NamesOfEntities.Variable(Index) + " = index_" + id + ";\n");
                    sourceCode.AppendFront(VariableType + " " + NamesOfEntities.Variable(Variable) + " = " + " entry_" + id + "[index_" + id + "];\n");
                }
                else
                {
                    sourceCode.AppendFront(VariableType + " " + NamesOfEntities.Variable(Variable) + " = " + " entry_" + id + "[index_" + id + "];\n");
                }
            }
            else if(ContainerType.StartsWith("Dictionary") && ContainerType.Contains("SetValueType"))
            {
                sourceCode.AppendFrontFormat("foreach(KeyValuePair<{0},GRGEN_LIBGR.SetValueType> entry_{1} in {2})\n", VariableType, id, NamesOfEntities.Variable(Container));
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                sourceCode.AppendFront(VariableType + " " + NamesOfEntities.Variable(Variable) + " = " + " entry_" + id + ".Key;\n");
            }
            else
            {
                sourceCode.AppendFrontFormat("foreach(KeyValuePair<{0},{1}> entry_{2} in {3})\n", IndexType, VariableType, id, NamesOfEntities.Variable(Container));
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                sourceCode.AppendFront(IndexType + " " + NamesOfEntities.Variable(Index) + " = entry_" + id + ".Key;\n");
                sourceCode.AppendFront(VariableType + " " + NamesOfEntities.Variable(Variable) + " = " + " entry_" + id + ".Value;\n");
            }

            foreach(Yielding statement in Statements)
                statement.Emit(sourceCode);

            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            foreach(Yielding statement in Statements)
                yield return statement;
        }

        public String Variable;
        public String UnprefixedVariable;
        public String VariableType;
        public String Index;
        public String UnprefixedIndex;
        public String IndexType;
        public String Container;
        public String UnprefixedContainer;
        public String ContainerType;
        Yielding[] Statements;
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
                statementsCopy[i] = Statements[i].Copy(renameSuffix);
            return new ForFunction(Variable + renameSuffix, UnprefixedVariable + renameSuffix, VariableType, Function.Copy(renameSuffix), statementsCopy);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            String id = fetchId().ToString();

            if(Function is Adjacent)
            {
                Adjacent adjacent = (Adjacent)Function;
                sourceCode.AppendFront("GRGEN_LIBGR.INode node_" + id + " = ");
                adjacent.Node.Emit(sourceCode);
                sourceCode.Append(";\n");
                if(!Profiling)
                {
                    sourceCode.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge edge_{0} in node_{0}.GetCompatibleIncident(", id);
                    adjacent.IncidentEdgeType.Emit(sourceCode);
                    sourceCode.Append("))\n");
                }
                else
                {
                    sourceCode.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge edge_{0} in node_{0}.Incident)\n", id);
                }
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                if(Profiling)
                {
                    if(Parallel)
                        sourceCode.AppendFront("++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];\n");
                    else
                        sourceCode.AppendFront("++actionEnv.PerformanceInfo.SearchSteps;\n");
                    sourceCode.AppendFrontFormat("if(!edge_{0}.InstanceOf(", id);
                    adjacent.IncidentEdgeType.Emit(sourceCode);
                    sourceCode.Append("))\n");
                    sourceCode.AppendFront("\tcontinue;\n");
                }

                sourceCode.AppendFrontFormat("if(!edge_{0}.Opposite(node_{0}).InstanceOf(", id);
                adjacent.AdjacentNodeType.Emit(sourceCode);
                sourceCode.Append("))\n");
                sourceCode.AppendFront("\tcontinue;\n");
                sourceCode.AppendFrontFormat("{0} {1} = ({0})edge_{2}.Opposite(node_{2});\n", VariableType, NamesOfEntities.Variable(Variable), id);
            }
            else if(Function is AdjacentIncoming)
            {
                AdjacentIncoming adjacent = (AdjacentIncoming)Function;
                sourceCode.AppendFront("GRGEN_LIBGR.INode node_" + id + " = ");
                adjacent.Node.Emit(sourceCode);
                sourceCode.Append(";\n");
                if(!Profiling)
                {
                    sourceCode.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge edge_{0} in node_{0}.GetCompatibleIncoming(", id);
                    adjacent.IncidentEdgeType.Emit(sourceCode);
                    sourceCode.Append("))\n");
                }
                else
                {
                    sourceCode.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge edge_{0} in node_{0}.Incoming)\n", id);
                }
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                if(Profiling)
                {
                    if(Parallel)
                        sourceCode.AppendFront("++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];\n");
                    else
                        sourceCode.AppendFront("++actionEnv.PerformanceInfo.SearchSteps;\n");
                    sourceCode.AppendFrontFormat("if(!edge_{0}.InstanceOf(", id);
                    adjacent.IncidentEdgeType.Emit(sourceCode);
                    sourceCode.Append("))\n");
                    sourceCode.AppendFront("\tcontinue;\n");
                }

                sourceCode.AppendFrontFormat("if(!edge_{0}.Source.InstanceOf(", id);
                adjacent.AdjacentNodeType.Emit(sourceCode);
                sourceCode.Append("))\n");
                sourceCode.AppendFront("\tcontinue;\n");
                sourceCode.AppendFrontFormat("{0} {1} = ({0})edge_{2}.Source;\n", VariableType, NamesOfEntities.Variable(Variable), id);
            }
            else if(Function is AdjacentOutgoing)
            {
                AdjacentOutgoing adjacent = (AdjacentOutgoing)Function;
                sourceCode.AppendFront("GRGEN_LIBGR.INode node_" + id + " = ");
                adjacent.Node.Emit(sourceCode);
                sourceCode.Append(";\n");
                if(!Profiling)
                {
                    sourceCode.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge edge_{0} in node_{0}.GetCompatibleOutgoing(", id);
                    adjacent.IncidentEdgeType.Emit(sourceCode);
                    sourceCode.Append("))\n");
                }
                else
                {
                    sourceCode.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge edge_{0} in node_{0}.Outgoing)\n", id);
                }
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                if(Profiling)
                {
                    if(Parallel)
                        sourceCode.AppendFront("++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];\n");
                    else
                        sourceCode.AppendFront("++actionEnv.PerformanceInfo.SearchSteps;\n");
                    sourceCode.AppendFrontFormat("if(!edge_{0}.InstanceOf(", id);
                    adjacent.IncidentEdgeType.Emit(sourceCode);
                    sourceCode.Append("))\n");
                    sourceCode.AppendFront("\tcontinue;\n");
                }

                sourceCode.AppendFrontFormat("if(!edge_{0}.Target.InstanceOf(", id);
                adjacent.AdjacentNodeType.Emit(sourceCode);
                sourceCode.Append("))\n");
                sourceCode.AppendFront("\tcontinue;\n");
                sourceCode.AppendFrontFormat("{0} {1} = ({0})edge_{2}.Target;\n", VariableType, NamesOfEntities.Variable(Variable), id);
            }
            else if(Function is Incident)
            {
                Incident incident = (Incident)Function;
                sourceCode.AppendFront("GRGEN_LIBGR.INode node_" + id + " = ");
                incident.Node.Emit(sourceCode);
                sourceCode.Append(";\n");
                if(!Profiling)
                {
                    sourceCode.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge edge_{0} in node_{0}.GetCompatibleIncident(", id);
                    incident.IncidentEdgeType.Emit(sourceCode);
                    sourceCode.Append("))\n");
                }
                else
                {
                    sourceCode.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge edge_{0} in node_{0}.Incident)\n", id);
                }
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                if(Profiling)
                {
                    if(Parallel)
                        sourceCode.AppendFront("++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];\n");
                    else
                        sourceCode.AppendFront("++actionEnv.PerformanceInfo.SearchSteps;\n");
                    sourceCode.AppendFrontFormat("if(!edge_{0}.InstanceOf(", id);
                    incident.IncidentEdgeType.Emit(sourceCode);
                    sourceCode.Append("))\n");
                    sourceCode.AppendFront("\tcontinue;\n");
                }

                sourceCode.AppendFrontFormat("if(!edge_{0}.Opposite(node_{0}).InstanceOf(", id);
                incident.AdjacentNodeType.Emit(sourceCode);
                sourceCode.Append("))\n");
                sourceCode.AppendFront("\tcontinue;\n");
                sourceCode.AppendFrontFormat("{0} {1} = ({0})edge_{2};\n", VariableType, NamesOfEntities.Variable(Variable), id);
            }
            else if(Function is Incoming)
            {
                Incoming incident = (Incoming)Function;
                sourceCode.AppendFront("GRGEN_LIBGR.INode node_" + id + " = ");
                incident.Node.Emit(sourceCode);
                sourceCode.Append(";\n");
                if(!Profiling)
                {
                    sourceCode.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge edge_{0} in node_{0}.GetCompatibleIncoming(", id);
                    incident.IncidentEdgeType.Emit(sourceCode);
                    sourceCode.Append("))\n");
                }
                else
                {
                    sourceCode.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge edge_{0} in node_{0}.Incoming)\n", id);
                }
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                if(Profiling)
                {
                    if(Parallel)
                        sourceCode.AppendFront("++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];\n");
                    else
                        sourceCode.AppendFront("++actionEnv.PerformanceInfo.SearchSteps;\n");
                    sourceCode.AppendFrontFormat("if(!edge_{0}.InstanceOf(", id);
                    incident.IncidentEdgeType.Emit(sourceCode);
                    sourceCode.Append("))\n");
                    sourceCode.AppendFront("\tcontinue;\n");
                }

                sourceCode.AppendFrontFormat("if(!edge_{0}.Source.InstanceOf(", id);
                incident.AdjacentNodeType.Emit(sourceCode);
                sourceCode.Append("))\n");
                sourceCode.AppendFront("\tcontinue;\n");
                sourceCode.AppendFrontFormat("{0} {1} = ({0})edge_{2};\n", VariableType, NamesOfEntities.Variable(Variable), id);
            }
            else if(Function is Outgoing)
            {
                Outgoing incident = (Outgoing)Function;
                sourceCode.AppendFront("GRGEN_LIBGR.INode node_" + id + " = ");
                incident.Node.Emit(sourceCode);
                sourceCode.Append(";\n");
                if(!Profiling)
                {
                    sourceCode.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge edge_{0} in node_{0}.GetCompatibleOutgoing(", id);
                    incident.IncidentEdgeType.Emit(sourceCode);
                    sourceCode.Append("))\n");
                }
                else
                {
                    sourceCode.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge edge_{0} in node_{0}.Outgoing)\n", id);
                }
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                if(Profiling)
                {
                    if(Parallel)
                        sourceCode.AppendFront("++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];\n");
                    else
                        sourceCode.AppendFront("++actionEnv.PerformanceInfo.SearchSteps;\n");
                    sourceCode.AppendFrontFormat("if(!edge_{0}.InstanceOf(", id);
                    incident.IncidentEdgeType.Emit(sourceCode);
                    sourceCode.Append("))\n");
                    sourceCode.AppendFront("\tcontinue;\n");
                }

                sourceCode.AppendFrontFormat("if(!edge_{0}.Target.InstanceOf(", id);
                incident.AdjacentNodeType.Emit(sourceCode);
                sourceCode.Append("))\n");
                sourceCode.AppendFront("\tcontinue;\n");
                sourceCode.AppendFrontFormat("{0} {1} = ({0})edge_{2};\n", VariableType, NamesOfEntities.Variable(Variable), id);
            }
            else if(Function is Reachable)
            {
                Reachable reachable = (Reachable)Function;
                sourceCode.AppendFront("GRGEN_LIBGR.INode node_" + id + " = ");
                reachable.Node.Emit(sourceCode);
                sourceCode.Append(";\n");
                sourceCode.AppendFrontFormat("foreach(GRGEN_LIBGR.INode iter_{0} in GRGEN_LIBGR.GraphHelper.Reachable(node_{0}, ", id);
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

                sourceCode.AppendFrontFormat("{0} {1} = ({0})iter_{2};\n", VariableType, NamesOfEntities.Variable(Variable), id);
            }
            else if(Function is ReachableIncoming)
            {
                ReachableIncoming reachable = (ReachableIncoming)Function;
                sourceCode.AppendFront("GRGEN_LIBGR.INode node_" + id + " = ");
                reachable.Node.Emit(sourceCode);
                sourceCode.Append(";\n");
                sourceCode.AppendFrontFormat("foreach(GRGEN_LIBGR.INode iter_{0} in GRGEN_LIBGR.GraphHelper.ReachableIncoming(node_{0}, ", id);
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

                sourceCode.AppendFrontFormat("{0} {1} = ({0})iter_{2};\n", VariableType, NamesOfEntities.Variable(Variable), id);
            }
            else if(Function is ReachableOutgoing)
            {
                ReachableOutgoing reachable = (ReachableOutgoing)Function;
                sourceCode.AppendFront("GRGEN_LIBGR.INode node_" + id + " = ");
                reachable.Node.Emit(sourceCode);
                sourceCode.Append(";\n");
                sourceCode.AppendFrontFormat("foreach(GRGEN_LIBGR.INode iter_{0} in GRGEN_LIBGR.GraphHelper.ReachableOutgoing(node_{0}, ", id);
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

                sourceCode.AppendFrontFormat("{0} {1} = ({0})iter_{2};\n", VariableType, NamesOfEntities.Variable(Variable), id);
            }
            else if(Function is ReachableEdges)
            {
                ReachableEdges reachable = (ReachableEdges)Function;
                sourceCode.AppendFront("GRGEN_LIBGR.INode node_" + id + " = ");
                reachable.Node.Emit(sourceCode);
                sourceCode.Append(";\n");
                sourceCode.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge edge_{0} in GRGEN_LIBGR.GraphHelper.ReachableEdges(node_{0}, ", id);
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

                sourceCode.AppendFrontFormat("{0} {1} = ({0})edge_{2};\n", VariableType, NamesOfEntities.Variable(Variable), id);
            }
            else if(Function is ReachableEdgesIncoming)
            {
                ReachableEdgesIncoming reachable = (ReachableEdgesIncoming)Function;
                sourceCode.AppendFront("GRGEN_LIBGR.INode node_" + id + " = ");
                reachable.Node.Emit(sourceCode);
                sourceCode.Append(";\n");
                sourceCode.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge edge_{0} in GRGEN_LIBGR.GraphHelper.ReachableEdgesIncoming(node_{0}, ", id);
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

                sourceCode.AppendFrontFormat("{0} {1} = ({0})edge_{2};\n", VariableType, NamesOfEntities.Variable(Variable), id);
            }
            else if(Function is ReachableEdgesOutgoing)
            {
                ReachableEdgesOutgoing reachable = (ReachableEdgesOutgoing)Function;
                sourceCode.AppendFront("GRGEN_LIBGR.INode node_" + id + " = ");
                reachable.Node.Emit(sourceCode);
                sourceCode.Append(";\n");
                sourceCode.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge edge_{0} in GRGEN_LIBGR.GraphHelper.ReachableEdgesOutgoing(node_{0}, ", id);
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

                sourceCode.AppendFrontFormat("{0} {1} = ({0})edge_{2};\n", VariableType, NamesOfEntities.Variable(Variable), id);
            }
            else if(Function is BoundedReachable)
            {
                BoundedReachable reachable = (BoundedReachable)Function;
                sourceCode.AppendFront("GRGEN_LIBGR.INode node_" + id + " = ");
                reachable.Node.Emit(sourceCode);
                sourceCode.Append(";\n");
                sourceCode.AppendFrontFormat("foreach(GRGEN_LIBGR.INode iter_{0} in GRGEN_LIBGR.GraphHelper.BoundedReachable(node_{0}, ", id);
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

                sourceCode.AppendFrontFormat("{0} {1} = ({0})iter_{2};\n", VariableType, NamesOfEntities.Variable(Variable), id);
            }
            else if(Function is BoundedReachableIncoming)
            {
                BoundedReachableIncoming reachable = (BoundedReachableIncoming)Function;
                sourceCode.AppendFront("GRGEN_LIBGR.INode node_" + id + " = ");
                reachable.Node.Emit(sourceCode);
                sourceCode.Append(";\n");
                sourceCode.AppendFrontFormat("foreach(GRGEN_LIBGR.INode iter_{0} in GRGEN_LIBGR.GraphHelper.BoundedReachableIncoming(node_{0}, ", id);
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

                sourceCode.AppendFrontFormat("{0} {1} = ({0})iter_{2};\n", VariableType, NamesOfEntities.Variable(Variable), id);
            }
            else if(Function is BoundedReachableOutgoing)
            {
                BoundedReachableOutgoing reachable = (BoundedReachableOutgoing)Function;
                sourceCode.AppendFront("GRGEN_LIBGR.INode node_" + id + " = ");
                reachable.Node.Emit(sourceCode);
                sourceCode.Append(";\n");
                sourceCode.AppendFrontFormat("foreach(GRGEN_LIBGR.INode iter_{0} in GRGEN_LIBGR.GraphHelper.BoundedReachableOutgoing(node_{0}, ", id);
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

                sourceCode.AppendFrontFormat("{0} {1} = ({0})iter_{2};\n", VariableType, NamesOfEntities.Variable(Variable), id);
            }
            else if(Function is BoundedReachableEdges)
            {
                BoundedReachableEdges reachable = (BoundedReachableEdges)Function;
                sourceCode.AppendFront("GRGEN_LIBGR.INode node_" + id + " = ");
                reachable.Node.Emit(sourceCode);
                sourceCode.Append(";\n");
                sourceCode.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge edge_{0} in GRGEN_LIBGR.GraphHelper.BoundedReachableEdges(node_{0}, ", id);
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

                sourceCode.AppendFrontFormat("{0} {1} = ({0})edge_{2};\n", VariableType, NamesOfEntities.Variable(Variable), id);
            }
            else if(Function is BoundedReachableEdgesIncoming)
            {
                BoundedReachableEdgesIncoming reachable = (BoundedReachableEdgesIncoming)Function;
                sourceCode.AppendFront("GRGEN_LIBGR.INode node_" + id + " = ");
                reachable.Node.Emit(sourceCode);
                sourceCode.Append(";\n");
                sourceCode.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge edge_{0} in GRGEN_LIBGR.GraphHelper.BoundedReachableEdgesIncoming(node_{0}, ", id);
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

                sourceCode.AppendFrontFormat("{0} {1} = ({0})edge_{2};\n", VariableType, NamesOfEntities.Variable(Variable), id);
            }
            else if(Function is BoundedReachableEdgesOutgoing)
            {
                BoundedReachableEdgesOutgoing reachable = (BoundedReachableEdgesOutgoing)Function;
                sourceCode.AppendFront("GRGEN_LIBGR.INode node_" + id + " = ");
                reachable.Node.Emit(sourceCode);
                sourceCode.Append(";\n");
                sourceCode.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge edge_{0} in GRGEN_LIBGR.GraphHelper.BoundedReachableEdgesOutgoing(node_{0}, ", id);
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

                sourceCode.AppendFrontFormat("{0} {1} = ({0})edge_{2};\n", VariableType, NamesOfEntities.Variable(Variable), id);
            }
            else if(Function is Nodes)
            {
                Nodes nodes = (Nodes)Function;
                sourceCode.AppendFrontFormat("foreach(GRGEN_LIBGR.INode node_{0} in graph.GetCompatibleNodes(", id);
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

                sourceCode.AppendFront(VariableType + " " + NamesOfEntities.Variable(Variable) + " = (" + VariableType + ") node_" + id + ";\n");
            }
            else if(Function is Edges)
            {
                Edges edges = (Edges)Function;
                sourceCode.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge edge_{0} in graph.GetCompatibleEdges(", id);
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

                sourceCode.AppendFront(VariableType + " " + NamesOfEntities.Variable(Variable) + " = (" + VariableType + ") edge_" + id + ";\n");
            }

            foreach(Yielding statement in Statements)
                statement.Emit(sourceCode);

            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            // the Function is not an independent child, it's just simpler/more consistent to reuse some parts of it here
            foreach(Yielding statement in Statements)
                yield return statement;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        public String Variable;
        public String UnprefixedVariable;
        public String VariableType;
        public Expression Function;
        Yielding[] Statements;
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
            sourceCode.AppendFront("if(");
            Condition.Emit(sourceCode);
            sourceCode.Append(") {\n");
            foreach(Yielding statement in TrueCaseStatements)
                statement.Emit(sourceCode);
            if(FalseCaseStatements != null)
            {
                sourceCode.AppendFront("} else {\n");
                foreach(Yielding statement in FalseCaseStatements)
                    statement.Emit(sourceCode);
            }
            sourceCode.AppendFront("}\n");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Condition;
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
            sourceCode.AppendFront("while(");
            Condition.Emit(sourceCode);
            sourceCode.Append(") {\n");
            foreach(Yielding statement in LoopedStatements)
                statement.Emit(sourceCode);
            sourceCode.AppendFront("}\n");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Condition;
            foreach(Yielding statement in LoopedStatements)
                yield return statement;
        }

        Expression Condition;
        Yielding[] LoopedStatements;
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
                loopedStatementsCopy[i] = LoopedStatements[i].Copy(renameSuffix);
            return new WhileStatement(Condition.Copy(renameSuffix), loopedStatementsCopy);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFront("do {\n");
            foreach(Yielding statement in LoopedStatements)
                statement.Emit(sourceCode);
            sourceCode.AppendFront("} while(");
            Condition.Emit(sourceCode);
            sourceCode.Append(");\n");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            foreach(Yielding statement in LoopedStatements)
                yield return statement;
            yield return Condition;
        }

        Yielding[] LoopedStatements;
        Expression Condition;
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
                statementsCopy[i] = Statements[i].Copy(renameSuffix);
            return new MultiStatement(statementsCopy);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            foreach(Yielding statement in Statements)
                statement.Emit(sourceCode);
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            foreach(Yielding statement in Statements)
                yield return statement;
        }

        Yielding[] Statements;
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
                Initialization.Emit(sourceCode);
            }
            sourceCode.Append(";\n");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            if(Initialization != null)
                yield return Initialization;
        }

        String Name;
        String Type;
        Expression Initialization;
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
        public EmitStatement(Expression toEmit)
        {
            ToEmitExpression = toEmit;
        }

        public override Yielding Copy(string renameSuffix)
        {
            return new EmitStatement(ToEmitExpression.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            String emitVar = "emit_value_" + fetchId().ToString();
            sourceCode.AppendFront("object " + emitVar + " = ");
            ToEmitExpression.Emit(sourceCode);
            sourceCode.Append(";\n");
            sourceCode.AppendFront("if(" + emitVar + " != null)\n");
            sourceCode.AppendFront("((GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv).EmitWriter.Write("
                    + "GRGEN_LIBGR.EmitHelper.ToStringNonNull(" + emitVar + ", graph));\n");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return ToEmitExpression;
        }

        Expression ToEmitExpression;
    }

    /// <summary>
    /// Class representing a highlight statement
    /// </summary>
    public class HighlightStatement : Yielding
    {
        public HighlightStatement(Expression[] values, string[] sourceNames)
        {
            Values = values;
            SourceNames = sourceNames;
        }

        public override Yielding Copy(string renameSuffix)
        {
            Expression[] valuesCopy = new Expression[Values.Length];
            for(int i = 0; i < Values.Length; ++i)
                valuesCopy[i] = Values[i].Copy(renameSuffix);
            return new HighlightStatement(Values, SourceNames);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            String highlightValuesArray = "highlight_values_" + fetchId().ToString();
		    sourceCode.AppendFront("List<object> " + highlightValuesArray + " = new List<object>();");
    	    String highlightSourceNamesArray = "highlight_source_names_" + fetchId().ToString();
		    sourceCode.AppendFront("List<string> " + highlightSourceNamesArray + " = new List<string>();");
            foreach(Expression value in Values)
            {
			    sourceCode.AppendFront(highlightValuesArray + ".Add(");
			    value.Emit(sourceCode);
			    sourceCode.Append(");\n");
            }
            foreach(String sourceName in SourceNames)
            {
                sourceCode.AppendFront(highlightSourceNamesArray + ".Add(");
                if(sourceName != null)
                {
                    sourceCode.Append("\"");
                    sourceCode.Append(sourceName);
                    sourceCode.Append("\"");
                }
                else
                    sourceCode.Append("null");
                sourceCode.Append(");\n");
            }
            sourceCode.AppendFront("((GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv).UserProxy.Highlight"
				    + "(" + highlightValuesArray + ", " + highlightSourceNamesArray + ");\n");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            foreach(Expression expr in Values)
                yield return expr;
        }

        Expression[] Values;
        String[] SourceNames;
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
            return new EmitStatement(ToRecordExpression.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            String recordVar = "emit_value_" + fetchId().ToString();
            sourceCode.AppendFront("object " + recordVar + " = ");
            ToRecordExpression.Emit(sourceCode);
            sourceCode.Append(";\n");
            sourceCode.AppendFront("if(" + recordVar + " != null)\n");
            sourceCode.AppendFront("((GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv).Recorder.Write("
                    + "GRGEN_LIBGR.EmitHelper.ToStringNonNull(" + recordVar + ", graph));\n");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return ToRecordExpression;
        }

        Expression ToRecordExpression;
    }
}

/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.stmt.graph
{
	using de.unika.ipd.grgen.ast;
	using VarDeclNode = de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
	using EdgeTypeNode = de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
	using NodeTypeNode = de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
	using EvalStatementNode = de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
	using NestingStatementNode = de.unika.ipd.grgen.ast.stmt.NestingStatementNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	/// <summary>
	/// AST node representing a for lookup of a neighborhood function.
	/// </summary>
	public abstract class ForGraphQueryNode : NestingStatementNode
	{
		static ForGraphQueryNode()
		{
			SetClassName(typeof(ForGraphQueryNode), "ForGraphQuery");
		}

		internal BaseNode iterationVariableUnresolved;
		internal VarDeclNode iterationVariable;

		protected internal ForGraphQueryNode(Coords coords, BaseNode iterationVariable, CollectNode<EvalStatementNode> loopedStatements)
			: base(coords, loopedStatements)
		{
			this.iterationVariableUnresolved = iterationVariable;
			BecomeParent(this.iterationVariableUnresolved);
		}

		protected internal virtual bool ResolveIterationVariable(string forType)
		{
			bool successfullyResolved = true;

			if(iterationVariableUnresolved is VarDeclNode)
				iterationVariable = (VarDeclNode)iterationVariableUnresolved;
			else
			{
				ReportError("Error in resolving iteration variable of for " + forType + " loop.");
				successfullyResolved = false;
			}

			if(!iterationVariable.Resolve())
				successfullyResolved = false;

			return successfullyResolved;
		}

		protected internal virtual bool CheckIterationVariable(string forType)
		{
			TypeNode iterationVariableType = iterationVariable.DeclType;
			if(!(iterationVariableType is NodeTypeNode)
					&& !(iterationVariableType is EdgeTypeNode))
			{
				ReportError("Iteration variable of for " + forType + " loop must be of type Node or Edge"
						+ " (but is of type " + iterationVariableType.ToStringWithDeclarationCoords() + ").");
				return false;
			}

			return true;
		}
	}

}

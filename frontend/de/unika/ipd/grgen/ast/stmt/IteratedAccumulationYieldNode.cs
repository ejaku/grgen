/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.stmt
{

	using System.Collections.Generic;

	using de.unika.ipd.grgen.ast;
	using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
	using EdgeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.EdgeDeclNode;
	using IteratedDeclNode = de.unika.ipd.grgen.ast.decl.pattern.IteratedDeclNode;
	using NodeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;
	using VarDeclNode = de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
	using de.unika.ipd.grgen.ast.util;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Rule = de.unika.ipd.grgen.ir.executable.Rule;
	using EvalStatement = de.unika.ipd.grgen.ir.stmt.EvalStatement;
	using IteratedAccumulationYield = de.unika.ipd.grgen.ir.stmt.IteratedAccumulationYield;
	using Variable = de.unika.ipd.grgen.ir.pattern.Variable;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	/// <summary>
	/// AST node representing an accumulation yielding of an iterated match def variable.
	/// </summary>
	public class IteratedAccumulationYieldNode : NestingStatementNode
	{
		static IteratedAccumulationYieldNode()
		{
			SetClassName(typeof(IteratedAccumulationYieldNode), "IteratedAccumulationYield");
		}

		internal VarDeclNode iterationVariableUnresolved;
		internal IdentNode iteratedUnresolved;

		internal VarDeclNode iterationVariable;
		internal IteratedDeclNode iterated;

		public IteratedAccumulationYieldNode(Coords coords, VarDeclNode iterationVariable, IdentNode iterated,
				CollectNode<EvalStatementNode> accumulationStatements)
			: base(coords, accumulationStatements)
		{
			this.iterationVariableUnresolved = iterationVariable;
			BecomeParent(this.iterationVariableUnresolved);
			this.iteratedUnresolved = iterated;
			BecomeParent(this.iteratedUnresolved);
		}

		/// <summary>
		/// returns children of this node </summary>
		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(GetValidVersion(iterationVariableUnresolved, iterationVariable));
				children.Add(GetValidVersion(iteratedUnresolved, iterated));
				children.Add(statements);
				return children;
			}
		}

		/// <summary>
		/// returns names of the children, same order as in getChildren </summary>
		public override ICollection<string> ChildrenNames
		{
			get
			{
				IList<string> childrenNames = new List<string>();
				childrenNames.Add("iterationVariable");
				childrenNames.Add("iterated");
				childrenNames.Add("accumulationStatements");
				return childrenNames;
			}
		}

		private static readonly DeclarationResolver<IteratedDeclNode> iteratedResolver =
				new DeclarationResolver<IteratedDeclNode>(typeof(IteratedDeclNode));

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool ResolveLocal()
		{
			bool successfullyResolved = true;

			iterated = iteratedResolver.Resolve(iteratedUnresolved, this);
			if(iterated == null)
				successfullyResolved = false;

			if(iterationVariableUnresolved is VarDeclNode)
				iterationVariable = (VarDeclNode)iterationVariableUnresolved;
			//} else if(accumulationVariableUnresolved instanceof ConstraintDeclNode) {
			//	accumulationGraphElement = (ConstraintDeclNode)accumulationVariableUnresolved;
			else
			{ // defining occurrence, no resolving should be necessary
				ReportError("Error in resolving the iteration variable of the for iterated accumulation loop.");
				successfullyResolved = false;
			}

			if((iterationVariable.context & BaseNode.CONTEXT_LHS_OR_RHS) == CONTEXT_RHS)
			{
				ReportError("A for iterated accumulation loop can only be used within a yield block in the pattern.");
				successfullyResolved = false;
			}

			bool iterationVariableFound = false;
			foreach(VarDeclNode var in iterated.pattern.DefVariablesToBeYieldedTo.ChildrenExact)
			{
				if(iterationVariable.ToString().Equals(var.ToString()))
				{
					iterationVariable.typeUnresolved = var.typeUnresolved;
					iterationVariableFound = true;
				}
			}
			foreach(NodeDeclNode node in iterated.pattern.Nodes)
			{
				if(iterationVariable.ToString().Equals(node.ToString()))
				{
					iterationVariable.typeUnresolved = node.typeUnresolved;
					iterationVariableFound = true;
				}
			}
			foreach(EdgeDeclNode edge in iterated.pattern.Edges)
			{
				if(iterationVariable.ToString().Equals(edge.ToString()))
				{
					iterationVariable.typeUnresolved = edge.typeUnresolved;
					iterationVariableFound = true;
				}
			}

			if(!iterationVariableFound)
			{
				ReportError("Cannot find the iteration variable " + iterationVariable + " in the iterated.");
				successfullyResolved = false;
			}

			if(!iterationVariable.Resolve())
				successfullyResolved = false;

			return successfullyResolved;
		}

		protected internal override bool CheckLocal()
		{
			return true;
		}

		public override bool CheckStatementLocal(bool isLHS, DeclNode root, EvalStatementNode enclosingLoop)
		{
			return true;
		}

		protected internal override IR ConstructIR()
		{
			IteratedAccumulationYield iay = new IteratedAccumulationYield(iterationVariable.CheckIR<Variable>(typeof(Variable)),
					iterated.CheckIR<Rule>(typeof(Rule)));
			foreach(EvalStatementNode accumulationStatement in statements.ChildrenExact)
				iay.AddStatement(accumulationStatement.CheckIR<EvalStatement>(typeof(EvalStatement)));
			return iay;
		}
	}

}

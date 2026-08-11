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
using VarDeclNode = de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
using de.unika.ipd.grgen.ast.util;
using ContainerAccumulationYield = de.unika.ipd.grgen.ir.stmt.ContainerAccumulationYield;
using EvalStatement = de.unika.ipd.grgen.ir.stmt.EvalStatement;
using IR = de.unika.ipd.grgen.ir.IR;
using Variable = de.unika.ipd.grgen.ir.pattern.Variable;
using Coords = de.unika.ipd.grgen.parser.Coords;

/// <summary>
/// AST node representing an accumulation yielding of a container variable.
/// </summary>
public class ContainerAccumulationYieldNode : NestingStatementNode
{
	static ContainerAccumulationYieldNode()
	{
		SetClassName(typeof(ContainerAccumulationYieldNode), "ContainerAccumulationYield");
	}

	internal VarDeclNode iterationVariableUnresolved;
	internal VarDeclNode iterationIndexUnresolved;
	internal IdentNode containerUnresolved;

	internal VarDeclNode iterationVariable;
	internal VarDeclNode iterationIndex;
	internal VarDeclNode container;

	public ContainerAccumulationYieldNode(Coords coords, VarDeclNode iterationVariable, VarDeclNode iterationIndex,
			IdentNode container, CollectNode<EvalStatementNode> accumulationStatements)
		: base(coords, accumulationStatements)
	{
		this.iterationVariableUnresolved = iterationVariable;
		BecomeParent(this.iterationVariableUnresolved);
		this.iterationIndexUnresolved = iterationIndex;
		if(this.iterationIndexUnresolved != null)
			BecomeParent(this.iterationIndexUnresolved);
		this.containerUnresolved = container;
		BecomeParent(this.containerUnresolved);
		this.statements = accumulationStatements;
		BecomeParent(this.statements);
	}

	/// <summary>
	/// returns children of this node </summary>
	public override ICollection<BaseNode> Children
	{
		get
		{
			IList<BaseNode> children = new List<BaseNode>();
			children.Add(GetValidVersion(iterationVariableUnresolved, iterationVariable));
			if(iterationIndexUnresolved != null)
				children.Add(GetValidVersion(iterationIndexUnresolved, iterationIndex));
			children.Add(GetValidVersion(containerUnresolved, container));
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
			if(iterationIndexUnresolved != null)
				childrenNames.Add("iterationIndex");
			childrenNames.Add("container");
			childrenNames.Add("accumulationStatements");
			return childrenNames;
		}
	}

	private static readonly DeclarationResolver<VarDeclNode> containerResolver =
			new DeclarationResolver<VarDeclNode>(typeof(VarDeclNode));

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
	protected internal override bool ResolveLocal()
	{
		bool successfullyResolved = true;

		container = containerResolver.Resolve(containerUnresolved, this);
		if(container == null)
			successfullyResolved = false;

		if(iterationVariableUnresolved is VarDeclNode) // defining occurrence, no resolving should be necessary
			iterationVariable = (VarDeclNode)iterationVariableUnresolved;
		else
		{
			ReportError("Error in resolving the iteration variable of the for loop iterating over a container.");
			successfullyResolved = false;
		}

		if(iterationIndexUnresolved != null)
		{
			if(iterationIndexUnresolved is VarDeclNode) // defining occurrence, no resolving should be necessary
				iterationIndex = (VarDeclNode)iterationIndexUnresolved;
			else
			{
				ReportError("Error in resolving the iteration index variable of the for loop iterating over a container.");
				successfullyResolved = false;
			}
		}

		if(!iterationVariable.Resolve())
			successfullyResolved = false;

		if(iterationIndex != null)
		{
			if(!iterationIndex.Resolve())
				successfullyResolved = false;
		}

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
		ContainerAccumulationYield cay = new ContainerAccumulationYield(iterationVariable.CheckIR(typeof(Variable)),
				iterationIndex != null ? iterationIndex.CheckIR(typeof(Variable)) : null,
				container.CheckIR(typeof(Variable)));
		foreach(EvalStatementNode accumulationStatement in statements.ChildrenExact)
			cay.AddStatement(accumulationStatement.CheckIR(typeof(EvalStatement)));
		return cay;
	}
}

}

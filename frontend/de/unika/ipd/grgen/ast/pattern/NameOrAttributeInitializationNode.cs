/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.pattern
{

using System.Collections.Generic;
using System.Diagnostics;

using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
using ConstraintDeclNode = de.unika.ipd.grgen.ast.decl.pattern.ConstraintDeclNode;
using ConstNode = de.unika.ipd.grgen.ast.expr.ConstNode;
using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
using MemberDeclNode = de.unika.ipd.grgen.ast.model.decl.MemberDeclNode;
using EdgeTypeNode = de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
using NodeTypeNode = de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using StringTypeNode = de.unika.ipd.grgen.ast.type.basic.StringTypeNode;
using de.unika.ipd.grgen.ast.util;
using Entity = de.unika.ipd.grgen.ir.Entity;
using IR = de.unika.ipd.grgen.ir.IR;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using GraphEntity = de.unika.ipd.grgen.ir.pattern.GraphEntity;
using NameOrAttributeInitialization = de.unika.ipd.grgen.ir.pattern.NameOrAttributeInitialization;

public class NameOrAttributeInitializationNode : BaseNode
{
	public ConstraintDeclNode owner;
	public GraphEntity ownerIR;

	public IdentNode attributeUnresolved;
	public MemberDeclNode attribute;
	public ExprNode initialization;

	public NameOrAttributeInitializationNode(ConstraintDeclNode owner, IdentNode attribute, ExprNode initialization)
	{
		this.owner = owner;
		this.attributeUnresolved = attribute;
		this.initialization = initialization;
	}

	public NameOrAttributeInitializationNode(ConstraintDeclNode owner, ExprNode initialization)
	{
		this.owner = owner;
		this.initialization = initialization;
	}

	public override ICollection<BaseNode> Children
	{
		get
		{
			IList<BaseNode> children = new List<BaseNode>();
			if(attributeUnresolved != null)
				children.Add(GetValidVersion(attributeUnresolved, attribute));
			children.Add(initialization);
			return children;
		}
	}

	public override ICollection<string> ChildrenNames
	{
		get
		{
			IList<string> childrenNames = new List<string>();
			if(attributeUnresolved != null)
				childrenNames.Add("attribute");
			childrenNames.Add("initialization");
			return childrenNames;
		}
	}

	private static readonly DeclarationResolver<MemberDeclNode> memberResolver =
			new DeclarationResolver<MemberDeclNode>(typeof(MemberDeclNode));

	protected internal override bool ResolveLocal()
	{
		if(attributeUnresolved != null)
		{
			owner.DeclInhType.FixupDefinition(attributeUnresolved);
			attribute = memberResolver.Resolve(attributeUnresolved, this);
			return attribute != null;
		}
		return true;
	}

	protected internal override bool CheckLocal()
	{
		if(attributeUnresolved == null)
		{
			TypeNode targetTypeNameInit = StringTypeNode.stringType;
			TypeNode exprTypeNameInit = initialization.Type;

			if(exprTypeNameInit.IsEqual(targetTypeNameInit))
				return true;

			initialization = BecomeParent(initialization.AdjustType(targetTypeNameInit, owner.Coords));
			if(initialization == ConstNode.Invalid)
			{
				owner.ReportError("The name of an element must be initialized with a value of type string"
						+ " (but it is initialized with a value of type " + exprTypeNameInit.TypeName + ").");
				return false;
			}

			return true;
		}

		if(attribute.IsConst())
		{
			owner.ReportError("An assignment to a const member is not allowed"
					+ " (but " + attribute.Ident + " is const).");
			return false;
		}

		if(owner.DeclInhType.IsConst())
		{
			owner.ReportError("An assignment to a const type object is not allowed"
					+ " (but " + owner.DeclType.TypeName + " is const).");
			return false;
		}

		TypeNode targetType = attribute.DeclType;
		TypeNode exprType = initialization.Type;

		if(exprType.IsEqual(targetType))
			return true;

		initialization = BecomeParent(initialization.AdjustType(targetType, owner.Coords));
		if(initialization == ConstNode.Invalid)
			return false;

		if(targetType is NodeTypeNode && exprType is NodeTypeNode
				|| targetType is EdgeTypeNode && exprType is EdgeTypeNode)
		{
			ICollection<TypeNode> superTypes = new HashSet<TypeNode>();
			exprType.DoGetCompatibleToTypes(superTypes);
			if(!superTypes.Contains(targetType))
			{
				owner.ReportError("Cannot initialize an attribute of type " + targetType.ToStringWithDeclarationCoords()
						+ " with a value of type " + exprType.ToStringWithDeclarationCoords() + ".");
				return false;
			}
		}
		if(targetType is NodeTypeNode && exprType is EdgeTypeNode
				|| targetType is EdgeTypeNode && exprType is NodeTypeNode)
		{
			owner.ReportError("Cannot initialize an attribute of type " + targetType.ToStringWithDeclarationCoords()
					+ " with a value of type " + exprType.ToStringWithDeclarationCoords() + ".");
			return false;
		}
		return true;
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.constructIR() "/>
	protected internal override IR ConstructIR()
	{
		// return if the IR object was already constructed
		// that may happen in recursive calls
		if(IsIRAlreadySet())
			return IR;

		NameOrAttributeInitialization nai = new NameOrAttributeInitialization();

		// mark this node as already visited
		IR = nai;

		Debug.Assert((ownerIR != null));
		nai.owner = ownerIR;
		if(attribute != null)
			nai.attribute = attribute.CheckIR(typeof(Entity));
		initialization = initialization.Evaluate();
		nai.expr = initialization.CheckIR(typeof(Expression));

		return nai;
	}
}

}

/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.expr
{

using System.Collections.Generic;
using System.Diagnostics;

using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
using MemberDeclNode = de.unika.ipd.grgen.ast.model.decl.MemberDeclNode;
using BaseInternalObjectTypeNode = de.unika.ipd.grgen.ast.model.type.BaseInternalObjectTypeNode;
using EdgeTypeNode = de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
using NodeTypeNode = de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using de.unika.ipd.grgen.ast.util;
using de.unika.ipd.grgen.ast.util;
using Entity = de.unika.ipd.grgen.ir.Entity;
using IR = de.unika.ipd.grgen.ir.IR;
using AttributeInitialization = de.unika.ipd.grgen.ir.expr.AttributeInitialization;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using InternalObjectInit = de.unika.ipd.grgen.ir.expr.InternalObjectInit;
using BaseInternalObjectType = de.unika.ipd.grgen.ir.model.type.BaseInternalObjectType;

public class AttributeInitializationNode : BaseNode
{
	public ObjectInitNode objectInit;
	public InternalObjectInit objectInitIR;

	public IdentNode ownerUnresolved;
	public BaseInternalObjectTypeNode owner;

	public IdentNode attributeUnresolved;
	public MemberDeclNode attribute;
	public ExprNode initialization;

	public AttributeInitializationNode(ObjectInitNode objectInit, IdentNode owner, IdentNode attribute, ExprNode initialization)
	{
		this.objectInit = objectInit;
		this.ownerUnresolved = owner;
		this.attributeUnresolved = attribute;
		this.initialization = initialization;
	}

	public override ICollection<BaseNode> Children
	{
		get
		{
			IList<BaseNode> children = new List<BaseNode>();
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
			childrenNames.Add("attribute");
			childrenNames.Add("initialization");
			return childrenNames;
		}
	}

	private static readonly DeclarationTypeResolver<BaseInternalObjectTypeNode> objectTypeResolver =
			new DeclarationTypeResolver<BaseInternalObjectTypeNode>(typeof(BaseInternalObjectTypeNode));

	private static readonly DeclarationResolver<MemberDeclNode> memberResolver =
			new DeclarationResolver<MemberDeclNode>(typeof(MemberDeclNode));

	protected internal override bool ResolveLocal()
	{
		owner = objectTypeResolver.Resolve(ownerUnresolved, this);
		if(owner == null || !owner.Resolve())
			return false;

		owner.FixupDefinition(attributeUnresolved);
		attribute = memberResolver.Resolve(attributeUnresolved, this);
		return attribute != null;
	}

	protected internal override bool CheckLocal()
	{
		if(attribute.IsConst())
		{
			objectInit.ReportError("An assignment to a const member is not allowed"
					+ " (but occurs for " + attribute + ").");
			return false;
		}

		if(owner.IsConst())
		{
			objectInit.ReportError("An assignment to an object of const type is not allowed"
					+ " (but occurs for " + attribute + " of " + owner.Ident + ").");
			return false;
		}

		TypeNode targetType = attribute.DeclType;
		TypeNode exprType = initialization.Type;

		if(exprType.IsEqual(targetType))
			return true;

		initialization = BecomeParent(initialization.AdjustType(targetType, objectInit.Coords));
		if(initialization == ConstNode.Invalid)
			return false;

		if(targetType is NodeTypeNode && exprType is NodeTypeNode
				|| targetType is EdgeTypeNode && exprType is EdgeTypeNode)
		{
			ICollection<TypeNode> superTypes = new HashSet<TypeNode>();
			exprType.DoGetCompatibleToTypes(superTypes);
			if(!superTypes.Contains(targetType))
			{
				objectInit.ReportError("Cannot initialize-assign a value of " + exprType.ToStringWithDeclarationCoords()
						+ " to an attribute of " + targetType.ToStringWithDeclarationCoords() + " (this occurs for " + attribute + ").");
				return false;
			}
		}
		if(targetType is NodeTypeNode && exprType is EdgeTypeNode
				|| targetType is EdgeTypeNode && exprType is NodeTypeNode)
		{
			objectInit.ReportError("Cannot initialize-assign a value of " + exprType.ToStringWithDeclarationCoords()
					+ " to an attribute of " + targetType.ToStringWithDeclarationCoords() + " (this occurs for " + attribute + ").");
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

		AttributeInitialization ai = new AttributeInitialization();

		// mark this node as already visited
		IR = ai;

		Debug.Assert((objectInitIR != null));
		ai.init = objectInitIR;
		ai.owner = owner.CheckIR(typeof(BaseInternalObjectType));
		ai.attribute = attribute.CheckIR(typeof(Entity));
		initialization = initialization.Evaluate();
		ai.expr = initialization.CheckIR(typeof(Expression));

		return ai;
	}
}

}

/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>
namespace de.unika.ipd.grgen.ast.decl.pattern
{

using System.Collections.Generic;

using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
using IdentExprNode = de.unika.ipd.grgen.ast.expr.IdentExprNode;
using QualIdentNode = de.unika.ipd.grgen.ast.expr.QualIdentNode;
using NodeTypeNode = de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
using PatternGraphLhsNode = de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using MapTypeNode = de.unika.ipd.grgen.ast.type.container.MapTypeNode;
using IR = de.unika.ipd.grgen.ir.IR;
using Qualification = de.unika.ipd.grgen.ir.expr.Qualification;
using GraphEntity = de.unika.ipd.grgen.ir.pattern.GraphEntity;
using Node = de.unika.ipd.grgen.ir.pattern.Node;
using StorageAccess = de.unika.ipd.grgen.ir.pattern.StorageAccess;
using StorageAccessIndex = de.unika.ipd.grgen.ir.pattern.StorageAccessIndex;
using Variable = de.unika.ipd.grgen.ir.pattern.Variable;

public class MatchNodeByStorageAccessDeclNode : MatchNodeFromByStorageDeclNode
{
	static MatchNodeByStorageAccessDeclNode()
	{
		SetClassName(typeof(MatchNodeByStorageAccessDeclNode), "match node by storage access decl");
	}

	private IdentExprNode accessorUnresolved;
	private ConstraintDeclNode accessor = null;

	public MatchNodeByStorageAccessDeclNode(IdentNode id, BaseNode type, int context,
			BaseNode storage, IdentExprNode accessor,
			PatternGraphLhsNode directlyNestingLHSGraph)
		: base(id, type, context, storage, directlyNestingLHSGraph)
	{
		this.accessorUnresolved = accessor;
		BecomeParent(this.accessorUnresolved);
	}

	/// <summary>
	/// returns children of this node </summary>
	public override ICollection<BaseNode> Children
	{
		get
		{
			IList<BaseNode> children = new List<BaseNode>();
			children.Add(ident);
			children.Add(GetValidVersion(typeUnresolved, typeNodeDecl, typeTypeDecl));
			children.Add(constraints);
			children.Add(GetValidVersion(storageUnresolved, storage, storageAttribute, storageGlobalVariable));
			children.Add(GetValidVersion(accessorUnresolved, accessor));
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
			childrenNames.Add("ident");
			childrenNames.Add("type");
			childrenNames.Add("constraints");
			childrenNames.Add("storage");
			childrenNames.Add("accessor");
			return childrenNames;
		}
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
	protected internal override bool ResolveLocal()
	{
		bool successfullyResolved = base.ResolveLocal();
		if(storageUnresolved is IdentExprNode)
		{
			IdentExprNode unresolved = (IdentExprNode)storageUnresolved;
			if(unresolved.Resolve())
			{
				if(unresolved.decl is VarDeclNode)
					storage = (VarDeclNode)unresolved.decl;
				else if(unresolved.decl is NodeDeclNode)
					storageGlobalVariable = (NodeDeclNode)unresolved.decl;
				else
				{
					ReportError("Match node by storage access expects a node storage parameter or a node global variable"
							+ " (but" + EmptyWhenAnonymousPostfix(" ") + " is given neither)" + ".");
					successfullyResolved = false;
				}
			}
			else
			{
				ReportError("Match node by storage access expects a node storage parameter or a node global variable"
						+ " (but" + EmptyWhenAnonymousPostfix(" ") + " is given neither)" + ".");
				successfullyResolved = false;
			}
		}
		else if(storageUnresolved is QualIdentNode)
		{
			QualIdentNode unresolved = (QualIdentNode)storageUnresolved;
			if(unresolved.Resolve())
				storageAttribute = unresolved;
			else
			{
				ReportError("Match node by storage attribute access expects a storage attribute"
						+ " (but" + EmptyWhenAnonymousPostfix(" ") + " is given " + unresolved + ").");
				successfullyResolved = false;
			}
		}
		else
		{
			ReportError("Internal error - invalid match node by storage attribute"
					+ " (for " + Ident + ").");
			successfullyResolved = false;
		}

		if(accessorUnresolved.Resolve() && accessorUnresolved.decl is ConstraintDeclNode)
			accessor = (ConstraintDeclNode)accessorUnresolved.decl;
		else
		{
			ReportError("Match node by storage access expects a pattern element as accessor"
					+ " (but" + EmptyWhenAnonymousPostfix(" ") + " is given " + accessorUnresolved + ").");
			successfullyResolved = false;
		}
		return successfullyResolved;
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
	protected internal override bool CheckLocal()
	{
		bool res = base.CheckLocal();
		if((context & CONTEXT_LHS_OR_RHS) == CONTEXT_RHS)
		{
			ReportError("Cannot employ match node by storage access in the rewrite part"
					+ " (as it occurs in match node" + EmptyWhenAnonymousPostfix(" ") + " by " + StorageName + ")" + ".");
			return false;
		}
		TypeNode storageType = StorageType;
		if(!(storageType is MapTypeNode))
		{
			ReportError("Match node by storage access expects a map type"
					+ " (but" + EmptyWhenAnonymousPostfix(" ") + " is given " + storageType.TypeName + " by " + StorageName + ").");
			return false;
		}
		TypeNode expectedStorageKeyType = ((MapTypeNode)storageType).keyType;
		TypeNode storageKeyType = accessor.DeclType;
		if(!storageKeyType.IsCompatibleTo(expectedStorageKeyType))
		{
			string expTypeName = expectedStorageKeyType.ToStringWithDeclarationCoords();
			string typeName = storageKeyType.ToStringWithDeclarationCoords();
			ident.ReportError("Cannot convert " + typeName
					+ " to the expected map key type " + expTypeName + " in match node by storage access"
					+ " (" + EmptyWhenAnonymous("of " + Ident + " ") + "accessing " + StorageName + ").");
			return false;
		}
		TypeNode storageElementType = ((MapTypeNode)storageType).valueType;
		if(!(storageElementType is NodeTypeNode))
		{
			ReportError("Match node by storage access expects a map mapping to a node type"
					+ " (but" + EmptyWhenAnonymousPostfix(" ") + " is given a map mapping to "
					+ storageElementType.Kind + " " + storageElementType.ToStringWithDeclarationCoords() + ").");
			return false;
		}
		NodeTypeNode storageElemType = (NodeTypeNode)storageElementType;
		NodeTypeNode expectedStorageElemType = DeclNodeType;
		if(!expectedStorageElemType.IsCompatibleTo(storageElemType))
		{
			string expTypeName = expectedStorageElemType.ToStringWithDeclarationCoords();
			string typeName = storageElemType.ToStringWithDeclarationCoords();
			ident.ReportError("Cannot convert map value type " + typeName
					+ " to the expected pattern element type " + expTypeName
					+ " in match node by storage access" + " (" + EmptyWhenAnonymous("of " + Ident + " ") + "accessing " + StorageName + ").");
			return false;
		}
		return res;
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.constructIR() "/>
	protected internal override IR ConstructIR()
	{
		if(IsIRAlreadySet()) // break endless recursion in case of cycle in usage
			return IR;

		Node node = (Node)base.ConstructIR();

		IR = node;

		if(storage != null)
			node.Storage = new StorageAccess(storage.CheckIR(typeof(Variable)));
		else if(storageAttribute != null)
			node.Storage = new StorageAccess(storageAttribute.CheckIR(typeof(Qualification)));
		//else node.setStorage(new StorageAccess(storageGlobalVariable.checkIR(Node.class)));
		node.StorageIndex = new StorageAccessIndex(accessor.CheckIR(typeof(GraphEntity)));
		return node;
	}
}

}

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
	using ContainerTypeNode = de.unika.ipd.grgen.ast.type.container.ContainerTypeNode;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Qualification = de.unika.ipd.grgen.ir.expr.Qualification;
	using Node = de.unika.ipd.grgen.ir.pattern.Node;
	using StorageAccess = de.unika.ipd.grgen.ir.pattern.StorageAccess;
	using Variable = de.unika.ipd.grgen.ir.pattern.Variable;

	public class MatchNodeFromStorageDeclNode : MatchNodeFromByStorageDeclNode
	{
		static MatchNodeFromStorageDeclNode()
		{
			SetClassName(typeof(MatchNodeFromStorageDeclNode), "match node from storage decl");
		}

		public MatchNodeFromStorageDeclNode(IdentNode id, BaseNode type, int context, BaseNode storage,
				PatternGraphLhsNode directlyNestingLHSGraph)
			: base(id, type, context, storage, directlyNestingLHSGraph)
		{
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
						ReportError("Match node from storage expects a node storage parameter or a node global variable"
								+ " (but" + EmptyWhenAnonymousPostfix(" ") + " is given neither).");
						successfullyResolved = false;
					}
				}
				else
				{
					ReportError("Match node from storage expects a node storage parameter or a node global variable"
							+ " (but" + EmptyWhenAnonymousPostfix(" ") + " is given neither).");
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
					ReportError("Match node from storage attribute expects a storage attribute"
							+ " (but" + EmptyWhenAnonymousPostfix(" ") + " is given " + unresolved + ").");
					successfullyResolved = false;
				}
			}
			else
			{
				ReportError("Internal error - invalid match node from storage attribute"
						+ " (for " + Ident + ").");
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
				ReportError("Cannot employ match node from storage in the rewrite part"
						+ " (as it occurs in match node" + EmptyWhenAnonymousPostfix(" ") + " from " + StorageName + ").");
				return false;
			}
			TypeNode storageType = StorageType;
			if(!(storageType is ContainerTypeNode))
			{
				if(storageGlobalVariable == null)
				{
					ReportError("Match node from storage expects a collection type"
							+ " (but" + EmptyWhenAnonymousPostfix(" ") + " is given " + storageType.TypeName + " by " + StorageName + ").");
					return false;
				}
			}
			TypeNode storageElementType = null;
			if(storageType is ContainerTypeNode)
				storageElementType = ((ContainerTypeNode)storageType).ElementType;
			else
				storageElementType = storageGlobalVariable.DeclType;
			if(!(storageElementType is NodeTypeNode))
			{
				if(storageGlobalVariable == null)
				{
					ReportError("Match node from storage expects the element type to be a node type"
							+ " (but" + EmptyWhenAnonymousPostfix(" ") + " is given " + storageElementType.Kind + " "
							+ storageElementType.ToStringWithDeclarationCoords() + " accessing " + StorageName + ").");
					return false;
				}
				else
				{
					ReportError("Match node from storage global variable expects a node type"
							+ " (but" + EmptyWhenAnonymousPostfix(" ") + " is given " + storageElementType.Kind + " "
							+ storageElementType.ToStringWithDeclarationCoords() + " accessing " + StorageName + ").");
					return false;
				}
			}
			NodeTypeNode storageElemType = (NodeTypeNode)storageElementType;
			NodeTypeNode expectedStorageElemType = DeclNodeType;
			if(!expectedStorageElemType.IsCompatibleTo(storageElemType))
			{
				string expTypeName = expectedStorageElemType.ToStringWithDeclarationCoords();
				string typeName = storageElemType.ToStringWithDeclarationCoords();
				ident.ReportError("Cannot convert storage element type from " + typeName
						+ " to the expected " + expTypeName + " in match node from storage"
						+ " (" + EmptyWhenAnonymous("of " + Ident + " ") + "accessing " + StorageName + ").");
				return false;
			}
			return res;
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.constructIR() "/>
		protected internal override IR ConstructIR()
		{
			Node node = (Node)base.ConstructIR();
			if(storage != null)
				node.Storage = new StorageAccess(storage.CheckIR<Variable>(typeof(Variable)));
			else if(storageAttribute != null)
				node.Storage = new StorageAccess(storageAttribute.CheckIR<Qualification>(typeof(Qualification)));
			//else node.setStorage(new StorageAccess(storageGlobalVariable.checkIR(Node.class)));
			return node;
		}
	}

}

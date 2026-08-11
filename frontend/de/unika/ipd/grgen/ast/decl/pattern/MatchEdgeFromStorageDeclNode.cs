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
	using EdgeTypeNode = de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
	using PatternGraphLhsNode = de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using ContainerTypeNode = de.unika.ipd.grgen.ast.type.container.ContainerTypeNode;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Qualification = de.unika.ipd.grgen.ir.expr.Qualification;
	using Edge = de.unika.ipd.grgen.ir.pattern.Edge;
	using StorageAccess = de.unika.ipd.grgen.ir.pattern.StorageAccess;
	using Variable = de.unika.ipd.grgen.ir.pattern.Variable;

	public class MatchEdgeFromStorageDeclNode : MatchEdgeFromByStorageDeclNode
	{
		static MatchEdgeFromStorageDeclNode()
		{
			SetClassName(typeof(MatchEdgeFromStorageDeclNode), "match edge from storage decl");
		}

		public MatchEdgeFromStorageDeclNode(IdentNode id, BaseNode type, int context, BaseNode storage,
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
				children.Add(GetValidVersion(typeUnresolved, typeEdgeDecl, typeTypeDecl));
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
					else if(unresolved.decl is EdgeDeclNode)
						storageGlobalVariable = (EdgeDeclNode)unresolved.decl;
					else
					{
						ReportError("Match edge from storage expects an edge storage parameter or an edge global variable"
								+ " (but" + EmptyWhenAnonymousPostfix(" ") + " is given neither).");
						successfullyResolved = false;
					}
				}
				else
				{
					ReportError("Match edge from storage expects an edge storage parameter or an edge global variable"
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
					ReportError("Match edge from storage attribute expects a storage attribute"
							+ " (but" + EmptyWhenAnonymousPostfix(" ") + " is given " + unresolved + ").");
					successfullyResolved = false;
				}
			}
			else
			{
				ReportError("Internal error - invalid match edge from storage attribute"
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
				ReportError("Cannot employ match edge from storage in the rewrite part"
						+ " (as it occurs in match edge" + EmptyWhenAnonymousPostfix(" ") + " from " + StorageName + ").");
				return false;
			}
			TypeNode storageType = StorageType;
			if(!(storageType is ContainerTypeNode))
			{
				if(storageGlobalVariable == null)
				{
					ReportError("Match edge from storage expects a collection type"
							+ " (but" + EmptyWhenAnonymousPostfix(" ") + " is given " + storageType.TypeName + " by " + StorageName + ").");
					return false;
				}
			}
			TypeNode storageElementType = null;
			if(storageType is ContainerTypeNode)
				storageElementType = ((ContainerTypeNode)storageType).ElementType;
			else
				storageElementType = storageGlobalVariable.DeclType;
			if(!(storageElementType is EdgeTypeNode))
			{
				if(storageGlobalVariable == null)
				{
					ReportError("Match edge from storage expects the element type to be an edge type"
							+ " (but" + EmptyWhenAnonymousPostfix(" ") + " is given " + storageElementType.Kind + " "
							+ storageElementType.ToStringWithDeclarationCoords() + " accessing " + StorageName + ").");
					return false;
				}
				else
				{
					ReportError("Match edge from storage global variable expects an edge type"
							+ " (but" + EmptyWhenAnonymousPostfix(" ") + " is given " + storageElementType.Kind + " "
							+ storageElementType.ToStringWithDeclarationCoords() + " accessing " + StorageName + ").");
					return false;
				}
			}
			EdgeTypeNode storageElemType = (EdgeTypeNode)storageElementType;
			EdgeTypeNode expectedStorageElemType = DeclEdgeType;
			if(!expectedStorageElemType.IsCompatibleTo(storageElemType))
			{
				string expTypeName = expectedStorageElemType.ToStringWithDeclarationCoords();
				string typeName = storageElemType.ToStringWithDeclarationCoords();
				ident.ReportError("Cannot convert storage element type from " + typeName
						+ " to the expected " + expTypeName + " in match edge from storage"
						+ " (" + EmptyWhenAnonymous("of " + Ident + " ") + "accessing " + StorageName + ").");
				return false;
			}
			return res;
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.constructIR() "/>
		protected internal override IR ConstructIR()
		{
			Edge edge = (Edge)base.ConstructIR();
			if(storage != null)
				edge.Storage = new StorageAccess(storage.CheckIR<Variable>(typeof(Variable)));
			else if(storageAttribute != null)
				edge.Storage = new StorageAccess(storageAttribute.CheckIR<Qualification>(typeof(Qualification)));
			//else edge.setStorage(new StorageAccess(storageGlobalVariable.checkIR(Edge.class)));
			return edge;
		}
	}

}

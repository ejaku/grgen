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
using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
using QualIdentNode = de.unika.ipd.grgen.ast.expr.QualIdentNode;
using PatternGraphLhsNode = de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;
using TypeExprNode = de.unika.ipd.grgen.ast.type.TypeExprNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;

public abstract class MatchEdgeFromByStorageDeclNode : EdgeDeclNode
{
	static MatchEdgeFromByStorageDeclNode()
	{
		SetClassName(typeof(MatchEdgeFromByStorageDeclNode), "match edge from by storage decl");
	}

	protected internal BaseNode storageUnresolved;
	protected internal VarDeclNode storage = null;
	protected internal QualIdentNode storageAttribute = null;
	protected internal EdgeDeclNode storageGlobalVariable = null;

	protected internal MatchEdgeFromByStorageDeclNode(IdentNode id, BaseNode type, int context, BaseNode storage,
			PatternGraphLhsNode directlyNestingLHSGraph)
		: base(id, type, CopyKind.None, context, TypeExprNode.Empty, directlyNestingLHSGraph)
	{
		this.storageUnresolved = storage;
		BecomeParent(this.storageUnresolved);
	}

	protected internal virtual TypeNode StorageType
	{
		get
		{
			if(storage != null)
				return storage.DeclType;
			else if(storageGlobalVariable != null)
				return storageGlobalVariable.DeclType;
			else
				return storageAttribute.Decl.DeclType;
		}
	}

	protected internal virtual string StorageName
	{
		get
		{
			if(storage != null)
				return storage.ToString();
			else if(storageGlobalVariable != null)
				return storageGlobalVariable.ToString();
			else
				return storageAttribute.ToString();
		}
	}
}

}

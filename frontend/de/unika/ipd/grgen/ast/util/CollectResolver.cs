/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.util
{
using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
using de.unika.ipd.grgen.ast;

/// <summary>
/// A resolver, that resolves a source AST CollectNode into a target AST CollectNode of type T,
/// by using a given resolver.
/// </summary>
public class CollectResolver<T> where T : de.unika.ipd.grgen.ast.BaseNode
{
	private Resolver<T> resolver;

	public CollectResolver(Resolver<T> resolver)
	{
		this.resolver = resolver;
	}

	/// <summary>
	/// resolves n to node of type R, via declaration if n is an identifier, via simple cast otherwise
	///  returns null if n's declaration or n can't be cast to R 
	/// </summary>
	public virtual CollectNode<T> Resolve<T1>(CollectNode<T1> collect, BaseNode parent) where T1 : de.unika.ipd.grgen.ast.BaseNode
	{
		CollectNode<T> res = new CollectNode<T>();
		res.Coords = collect.Coords;

		foreach(BaseNode child in collect.ChildrenExact)
		{
			T resolved = resolver.Resolve(child, collect);
			if(resolved == null)
				return null;
			res.AddChild(resolved);
		}
		parent.BecomeParent(res);
		return res;
	}
}

}

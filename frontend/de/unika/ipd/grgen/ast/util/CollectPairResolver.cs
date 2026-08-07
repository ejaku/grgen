/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.util
{
using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
using CollectBaseNode = de.unika.ipd.grgen.ast.CollectBaseNode;
using de.unika.ipd.grgen.ast;

/// <summary>
/// A resolver, that resolves a source AST CollectNode into (one of) two target AST CollectNode of types S and T,
/// by using a given resolver.
/// </summary>
public class CollectPairResolver<S, T> where S : de.unika.ipd.grgen.ast.BaseNode where T : de.unika.ipd.grgen.ast.BaseNode
{
	private Resolver<Pair<S, T>> resolver;

	public CollectPairResolver(Resolver<Pair<S, T>> resolver)
	{
		this.resolver = resolver;
	}

	/// <summary>
	/// resolves the collect node to collect nodes of type S, T via the given resolver
	/// </summary>
	public virtual Pair<CollectNode<S>, CollectNode<T>> Resolve(CollectBaseNode collect)
	{
		CollectNode<S> first = null;
		CollectNode<T> second = null;

		foreach(BaseNode child in collect.Children)
		{
			Pair<S, T> pair = resolver.Resolve(child, collect);
			if(pair == null)
				return null;
			if(pair.fst != null)
			{
				if(first == null)
				{
					first = new CollectNode<S>();
					first.Coords = collect.Coords;
				}
				first.AddChild(pair.fst);
			}
			if(pair.snd != null)
			{
				if(second == null)
				{
					second = new CollectNode<T>();
					second.Coords = collect.Coords;
				}
				second.AddChild(pair.snd);
			}
		}

		Pair<CollectNode<S>, CollectNode<T>> res = new Pair<CollectNode<S>, CollectNode<T>>();
		res.fst = first;
		res.snd = second;

		return res;
	}
}

}

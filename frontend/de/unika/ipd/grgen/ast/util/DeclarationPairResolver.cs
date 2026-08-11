/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.util
{
	using System;
	using System.Diagnostics;

	using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
	using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
	using PackageIdentNode = de.unika.ipd.grgen.ast.PackageIdentNode;
	using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
	using Util = de.unika.ipd.grgen.util.Util;

	/// <summary>
	/// A resolver, that resolves a source AST node into a target AST node of type R or S,
	/// by drawing the declaration node out of the source node if it is an identifier node,
	/// or by simply casting source to R/S otherwise
	/// </summary>
	public class DeclarationPairResolver<R, S> : Resolver<Pair<R, S>> where R : de.unika.ipd.grgen.ast.BaseNode where S : de.unika.ipd.grgen.ast.BaseNode
	{
		private Type clsR = typeof(R);
		private Type clsS = typeof(S);
		private Type[] classes;

		public DeclarationPairResolver(Type clsR, Type clsS)
		{
			this.clsR = clsR;
			this.clsS = clsS;

			classes = new Type[] {this.clsR, this.clsS};
		}

		/// <summary>
		/// resolves n to node of type R, via declaration if n is an identifier, via simple cast otherwise
		///  returns null if n's declaration or n can't be cast to R or S 
		/// </summary>
		public override Pair<R, S> Resolve(BaseNode bn, BaseNode parent)
		{
			if(bn is IdentNode)
			{
				Pair<R, S> pair = Resolve((IdentNode)bn);
				if(pair != null)
				{
					Debug.Assert(pair.fst == null || pair.snd == null);
					parent.BecomeParent(pair.fst);
					parent.BecomeParent(pair.snd);
				}
				return pair;
			}
			else
			{
				Pair<R, S> pair = new Pair<R, S>();
				if(clsR.IsInstanceOfType(bn))
					pair.fst = clsR.Cast(bn);
				if(clsS.IsInstanceOfType(bn))
					pair.snd = clsS.Cast(bn);
				if(pair.fst != null || pair.snd != null)
				{
					Debug.Assert(pair.fst == null || pair.snd == null);
					return pair;
				}

				bn.ReportError(bn + " is a " + bn.Kind +
						" but a " + Util.GetStrListWithOr(classes, typeof(BaseNode), "getKindStr") + " is expected.");
				return null;
			}
		}

		/// <summary>
		/// resolves n to node of type R or S, via declaration
		///  returns null if n's declaration can't be cast to R/S 
		/// </summary>
		private Pair<R, S> Resolve(IdentNode n)
		{
			if(n is PackageIdentNode)
			{
				if(!ResolveOwner((PackageIdentNode)n))
					return null;
			}

			Pair<R, S> pair = new Pair<R, S>();
			DeclNode resolved = n.Decl;
			if(clsR.IsInstanceOfType(resolved))
				pair.fst = clsR.Cast(resolved);
			if(clsS.IsInstanceOfType(resolved))
				pair.snd = clsS.Cast(resolved);
			if(pair.fst != null || pair.snd != null)
				return pair;

			n.ReportError(n + " is a " + resolved.Kind +
					" but a " + Util.GetStrListWithOr(classes, typeof(BaseNode), "getKindStr") + " is expected.");
			return null;
		}
	}

}

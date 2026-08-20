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
	/// A resolver, that resolves a source AST node into a target AST node of type R, S or T,
	/// by drawing the declaration node out of the source node if it is an identifier node,
	/// or by simply casting source to R/S/T otherwise
	/// </summary>
	public class DeclarationTripleResolver<R, S, T> : Resolver<Triple<R, S, T>> where R : de.unika.ipd.grgen.ast.BaseNode where S : de.unika.ipd.grgen.ast.BaseNode where T : de.unika.ipd.grgen.ast.BaseNode
	{
		private Type clsR = typeof(R);
		private Type clsS = typeof(S);
		private Type clsT = typeof(T);
		private Type[] classes;

		public DeclarationTripleResolver(Type clsR, Type clsS, Type clsT)
		{
			this.clsR = clsR;
			this.clsS = clsS;
			this.clsT = clsT;

			classes = new Type[] { this.clsR, this.clsS, this.clsT };
		}

		/// <summary>
		/// resolves n to node of type R, S or T, via declaration if n is an identifier, via simple cast otherwise
		///  returns null if n's declaration or n can't be cast to R, S or T 
		/// </summary>
		public override Triple<R, S, T> Resolve(BaseNode bn, BaseNode parent)
		{
			Triple<R, S, T> triple;
			if(bn is IdentNode)
			{
				triple = Resolve((IdentNode)bn);
				if(triple != null)
				{
					Debug.Assert((triple.first == null ? 0 : 1)
							+ (triple.second == null ? 0 : 1)
							+ (triple.third == null ? 0 : 1) == 1);
					parent.BecomeParent(triple.first);
					parent.BecomeParent(triple.second);
					parent.BecomeParent(triple.third);
				}
				return triple;
			}

			triple = new Triple<R, S, T>();
			if(clsR.IsInstanceOfType(bn))
				triple.first = bn as R;
			if(clsS.IsInstanceOfType(bn))
				triple.second = bn as S;
			if(clsT.IsInstanceOfType(bn))
				triple.third = bn as T;
			if(triple.first != null || triple.second != null || triple.third != null)
			{
				Debug.Assert((triple.first == null ? 0 : 1)
						+ (triple.second == null ? 0 : 1)
						+ (triple.third == null ? 0 : 1) == 1);

				return triple;
			}

			bn.ReportError(bn + " is a " + bn.Kind +
					" but a " + Util.GetStrListWithOr(classes, typeof(BaseNode), "KindStr") + " is expected.");
			return null;
		}

		/// <summary>
		/// resolves n to node of type R, S or T, via declaration
		///  returns null if n's declaration can't be cast to R/S/T 
		/// </summary>
		private Triple<R, S, T> Resolve(IdentNode n)
		{
			if(n is PackageIdentNode)
			{
				if(!ResolveOwner((PackageIdentNode)n))
					return null;
			}

			Triple<R, S, T> triple = new Triple<R, S, T>();
			DeclNode resolved = n.Decl;
			if(clsR.IsInstanceOfType(resolved))
				triple.first = resolved as R;
			if(clsS.IsInstanceOfType(resolved))
				triple.second = resolved as S;
			if(clsT.IsInstanceOfType(resolved))
				triple.third = resolved as T;
			if(triple.first != null || triple.second != null || triple.third != null)
				return triple;

			n.ReportError(n + " is a " + resolved.Kind +
					" but a " + Util.GetStrListWithOr(classes, typeof(BaseNode), "KindStr") + " is expected.");
			return null;
		}
	}

}

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
public class DeclarationQuadrupleResolver<R, S, T, U> : Resolver<Quadruple<R, S, T, U>> where R : de.unika.ipd.grgen.ast.BaseNode where S : de.unika.ipd.grgen.ast.BaseNode where T : de.unika.ipd.grgen.ast.BaseNode where U : de.unika.ipd.grgen.ast.BaseNode
{
	private Type clsR = typeof(R);
	private Type clsS = typeof(S);
	private Type clsT = typeof(T);
	private Type clsU = typeof(U);
	private Type[] classes;

	public DeclarationQuadrupleResolver(Type clsR, Type clsS, Type clsT, Type clsU)
	{
		this.clsR = clsR;
		this.clsS = clsS;
		this.clsT = clsT;
		this.clsU = clsU;

		classes = new Type[] {this.clsR, this.clsS, this.clsT};
	}

	/// <summary>
	/// resolves n to node of type R, S, T or U, via declaration if n is an identifier, via simple cast otherwise
	///  returns null if n's declaration or n can't be cast to R, S, T or U 
	/// </summary>
	public override Quadruple<R, S, T, U> Resolve(BaseNode bn, BaseNode parent)
	{
		Quadruple<R, S, T, U> quadruple;
		if(bn is IdentNode)
		{
			quadruple = Resolve((IdentNode)bn);
			if(quadruple != null)
			{
				Debug.Assert((quadruple.first == null ? 0, 1)
						+ (quadruple.second == null ? 0 : 1)
						+ (quadruple.third == null ? 0 : 1)
						+ (quadruple.fourth == null ? 0 : 1) == 1);
				parent.BecomeParent(quadruple.first);
				parent.BecomeParent(quadruple.second);
				parent.BecomeParent(quadruple.third);
				parent.BecomeParent(quadruple.fourth);
			}
			return quadruple;
		}

		quadruple = new Quadruple<R, S, T, U>();
		if(clsR.IsInstanceOfType(bn))
			quadruple.first = clsR.Cast(bn);
		if(clsS.IsInstanceOfType(bn))
			quadruple.second = clsS.Cast(bn);
		if(clsT.IsInstanceOfType(bn))
			quadruple.third = clsT.Cast(bn);
		if(clsU.IsInstanceOfType(bn))
			quadruple.fourth = clsU.Cast(bn);
		if(quadruple.first != null || quadruple.second != null || quadruple.third != null || quadruple.fourth != null)
		{
			Debug.Assert((quadruple.first == null ? 0, 1)
					+ (quadruple.second == null ? 0 : 1)
					+ (quadruple.third == null ? 0 : 1)
					+ (quadruple.fourth == null ? 0 : 1) == 1);

			return quadruple;
		}

		bn.ReportError(bn + " is a " + bn.Kind +
				" but a " + Util.GetStrListWithOr(classes, typeof(BaseNode), "getKindStr") + " is expected.");
		return null;
	}

	/// <summary>
	/// resolves n to node of type R, S, T or U, via declaration
	///  returns null if n's declaration can't be cast to R/S/T/U 
	/// </summary>
	private Quadruple<R, S, T, U> Resolve(IdentNode n)
	{
		if(n is PackageIdentNode)
		{
			if(!ResolveOwner((PackageIdentNode)n))
				return null;
		}

		Quadruple<R, S, T, U> quadruple = new Quadruple<R, S, T, U>();
		DeclNode resolved = n.Decl;
		if(clsR.IsInstanceOfType(resolved))
			quadruple.first = clsR.Cast(resolved);
		if(clsS.IsInstanceOfType(resolved))
			quadruple.second = clsS.Cast(resolved);
		if(clsT.IsInstanceOfType(resolved))
			quadruple.third = clsT.Cast(resolved);
		if(clsU.IsInstanceOfType(resolved))
			quadruple.fourth = clsU.Cast(resolved);
		if(quadruple.first != null || quadruple.second != null || quadruple.third != null || quadruple.fourth != null)
			return quadruple;

		n.ReportError(n + " is a " + resolved.Kind +
				" but a " + Util.GetStrListWithOr(classes, typeof(BaseNode), "getKindStr") + " is expected.");
		return null;
	}
}

}

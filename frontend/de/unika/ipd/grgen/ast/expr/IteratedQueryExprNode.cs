/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.expr
{

	using System.Collections.Generic;

	using de.unika.ipd.grgen.ast;
	using IteratedDeclNode = de.unika.ipd.grgen.ast.decl.pattern.IteratedDeclNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using de.unika.ipd.grgen.ast.util;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Rule = de.unika.ipd.grgen.ir.executable.Rule;
	using IteratedQueryExpr = de.unika.ipd.grgen.ir.expr.IteratedQueryExpr;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	public class IteratedQueryExprNode : ExprNode
	{
		static IteratedQueryExprNode()
		{
			SetClassName(typeof(IteratedQueryExprNode), "iterated query");
		}

		private IdentNode iteratedUnresolved;
		private IteratedDeclNode iterated;

		private TypeNode arrayOfMatchTypeUnresolved;
		private TypeNode arrayOfMatchType;

		public IteratedQueryExprNode(Coords coords, IdentNode iterated, TypeNode arrayOfMatchType)
			: base(coords)
		{

			this.iteratedUnresolved = BecomeParent(iterated);
			this.arrayOfMatchTypeUnresolved = BecomeParent(arrayOfMatchType);
		}

		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(GetValidVersion(iteratedUnresolved, iterated));
				children.Add(GetValidVersion(arrayOfMatchTypeUnresolved, arrayOfMatchType));
				return children;
			}
		}

		public override ICollection<string> ChildrenNames
		{
			get
			{
				IList<string> childrenNames = new List<string>();
				childrenNames.Add("iterated");
				childrenNames.Add("arrayOfMatchType");
				return childrenNames;
			}
		}

		private static readonly DeclarationResolver<IteratedDeclNode> iteratedResolver =
				new DeclarationResolver<IteratedDeclNode>(typeof(IteratedDeclNode));

		protected internal override bool ResolveLocal()
		{
			iterated = iteratedResolver.Resolve(iteratedUnresolved, this);
			if(iterated == null)
				return false;
			if(arrayOfMatchTypeUnresolved.Resolve())
				arrayOfMatchType = arrayOfMatchTypeUnresolved;
			return arrayOfMatchType != null;
		}

		protected internal override bool CheckLocal()
		{
			return true;
		}

		protected internal override IR ConstructIR()
		{
			return new IteratedQueryExpr(iteratedUnresolved.IRIdent, iterated.CheckIR(typeof(Rule)), Type.IRType);
		}

		public override TypeNode Type
		{
			get
			{
				return arrayOfMatchType;
			}
		}

		public override bool NoIteratedReference(string containingConstruct)
		{
			ReportError("The matches of an iterated cannot be accessed with an iterated query [?" + iteratedUnresolved + "]"
					+ " from a " + containingConstruct + ", only from a yield block or yield expression or eval.");
			return false;
		}

		public override bool IteratedNotReferenced(string iterName)
		{
			if(iterated.Ident.ToString().Equals(iterName))
			{
				ReportError("An iterated query cannot access an iterated it is contained in, as it occurs with [?" + iteratedUnresolved + "].");
				return false;
			}
			return true;
		}
	}

}

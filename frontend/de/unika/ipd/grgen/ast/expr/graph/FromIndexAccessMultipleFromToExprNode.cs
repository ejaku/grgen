/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.expr.graph
{

	using System.Collections.Generic;
	using System.Text;

	using de.unika.ipd.grgen.ast;
	using BuiltinFunctionInvocationBaseNode = de.unika.ipd.grgen.ast.expr.BuiltinFunctionInvocationBaseNode;
	using IndexDeclNode = de.unika.ipd.grgen.ast.model.decl.IndexDeclNode;
	using InheritanceTypeNode = de.unika.ipd.grgen.ast.model.type.InheritanceTypeNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using SetTypeNode = de.unika.ipd.grgen.ast.type.container.SetTypeNode;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	/// <summary>
	/// A node yielding the nodes from multiple indices (by accessing a range from a certain value to a certain value, each time).
	/// </summary>
	public abstract class FromIndexAccessMultipleFromToExprNode : BuiltinFunctionInvocationBaseNode
	{
		static FromIndexAccessMultipleFromToExprNode()
		{
			SetClassName(typeof(FromIndexAccessMultipleFromToExprNode), "from index access multiple from to expr");
		}

		protected internal CollectNode<FromIndexAccessFromToPartExprNode> indexAccessExprs = new CollectNode<FromIndexAccessFromToPartExprNode>();
		private SetTypeNode setTypeNode;

		public FromIndexAccessMultipleFromToExprNode(Coords coords)
			: base(coords)
		{

			this.indexAccessExprs = BecomeParent(indexAccessExprs);
		}

		public virtual void AddIndexAccessExpr(FromIndexAccessFromToPartExprNode expr)
		{
			indexAccessExprs.AddChild(expr);
		}

		/// <summary>
		/// returns children of this node </summary>
		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(indexAccessExprs);
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
				childrenNames.Add("indexAccessExprs");
				return childrenNames;
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool ResolveLocal()
		{
			bool successfullyResolved = true;
			setTypeNode = new SetTypeNode(Root);
			successfullyResolved &= setTypeNode.Resolve();
			return successfullyResolved;
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
		protected internal override bool CheckLocal()
		{
			bool successfullyChecked = true;

			TypeNode expectedEntityType = Root.Decl.GetDeclType();
			foreach(FromIndexAccessFromToPartExprNode indexAccessExpr in indexAccessExprs.ChildrenExact)
			{
				TypeNode entityType = indexAccessExpr.index.Type;
				if(!entityType.IsCompatibleTo(expectedEntityType))
					successfullyChecked = false; // the index type is checked with the parts, and an error is emitted there - we just skip the warning messages here in case of an index type mismatch
			}

			if(!successfullyChecked)
				return false;

			for(int i = 0; i < indexAccessExprs.ChildrenExact.Count; ++i)
			{
				FromIndexAccessFromToPartExprNode indexAccessExpr = indexAccessExprs.Get(i);
				InheritanceTypeNode entityType = indexAccessExpr.index.Type;

				for(int j = i + 1; j < indexAccessExprs.ChildrenExact.Count; ++j)
				{
					FromIndexAccessFromToPartExprNode indexAccessExpr2 = indexAccessExprs.Get(j);
					InheritanceTypeNode entityType2 = indexAccessExpr2.index.Type;

					if(!InheritanceTypeNode.HasCommonSubtype(entityType, entityType2))
					{
						ReportWarning("The indexed type " + entityType.ToStringWithDeclarationCoords() + " of the " + (i * 3 + 1) + ". argument (index)"
										+ " and the indexed type " + entityType2.ToStringWithDeclarationCoords() + " of the " + (j * 3 + 1) + ". argument (index)"
										+ " have no common subtype, thus the content of these indices is disjoint, and the index join will always be empty.");
					}
				}
			}

			int indexShift = 0;
			HashSet<IndexDeclNode> indicesUsed = new HashSet<IndexDeclNode>();
			foreach(FromIndexAccessFromToPartExprNode indexAccessExpr in indexAccessExprs.ChildrenExact)
			{
				int indexArgumentNumber = 1 + indexShift;
				if(indicesUsed.Contains(indexAccessExpr.index))
				{
					ReportWarning("The function " + ShortSignature() + " uses as " + indexArgumentNumber + ". argument (index) the index " + indexAccessExpr.index.ToStringWithDeclarationCoords()
							+ " for another time (combine the queried ranges into one).");
				}
				else
					indicesUsed.Add(indexAccessExpr.index);
				indexShift += 3;
			}

			return true;
		}

		protected internal abstract IdentNode Root {get;}

		protected internal abstract string ShortSignature();

		protected internal virtual string ArgumentsPart()
		{
			StringBuilder sb = new StringBuilder();
			bool first = true;
	// JAVA TO C# CONVERTER TASK: Most Java annotations will not have direct .NET equivalent attributes:
	// ORIGINAL LINE: for(@SuppressWarnings("unused") FromIndexAccessFromToExprNode indexAccessExpr : indexAccessExprs.getChildrenExact())
			foreach(FromIndexAccessFromToExprNode indexAccessExpr in indexAccessExprs.ChildrenExact)
			{
				if(first)
					first = false;
				else
					sb.Append(",");
				sb.Append(".,.,.");
			}
			return sb.ToString();
		}

		public override TypeNode Type
		{
			get
			{
				return setTypeNode;
			}
		}
	}

}

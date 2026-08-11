/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast
{

	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using ExprPairNode = de.unika.ipd.grgen.ast.expr.ExprPairNode;

	public abstract class CollectBaseNode : BaseNode
	{
		public override Color NodeColor
		{
			get
			{
				return Color.GRAY;
			}
		}

		public virtual bool NoDefElement(string containingConstruct)
		{
			bool res = true;
			foreach(BaseNode child in Children)
			{
				if(child is ExprNode)
					res &= ((ExprNode)child).NoDefElement(containingConstruct);
				else if(child is ExprPairNode)
					res &= ((ExprPairNode)child).NoDefElement(containingConstruct);
			}
			return res;
		}

		public virtual bool NoIteratedReference(string containingConstruct)
		{
			bool res = true;
			foreach(BaseNode child in Children)
			{
				if(child is ExprNode)
					res &= ((ExprNode)child).NoIteratedReference(containingConstruct);
				else if(child is ExprPairNode)
					res &= ((ExprPairNode)child).NoIteratedReference(containingConstruct);
			}
			return res;
		}

		public virtual bool IteratedNotReferenced(string iterName)
		{
			bool res = true;
			foreach(BaseNode child in Children)
			{
				if(child is ExprNode)
					res &= ((ExprNode)child).IteratedNotReferenced(iterName);
				else if(child is ExprPairNode)
					res &= ((ExprPairNode)child).IteratedNotReferenced(iterName);
			}
			return res;
		}
	}

}

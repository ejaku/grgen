/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

package de.unika.ipd.grgen.ast;

import java.awt.Color;

import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.expr.ExprPairNode;

public abstract class CollectBaseNode extends BaseNode
{
	@Override
	public Color getNodeColor()
	{
		return Color.GRAY;
	}

	public boolean noDefElement(String containingConstruct)
	{
		boolean res = true;
		for(BaseNode child : getChildren()) {
			if(child instanceof ExprNode)
				res &= ((ExprNode)child).noDefElement(containingConstruct);
			else if(child instanceof ExprPairNode)
				res &= ((ExprPairNode)child).noDefElement(containingConstruct);
		}
		return res;
	}

	public boolean noIteratedReference(String containingConstruct)
	{
		boolean res = true;
		for(BaseNode child : getChildren()) {
			if(child instanceof ExprNode)
				res &= ((ExprNode)child).noIteratedReference(containingConstruct);
			else if(child instanceof ExprPairNode)
				res &= ((ExprPairNode)child).noIteratedReference(containingConstruct);
		}
		return res;
	}

	public boolean iteratedNotReferenced(String iterName)
	{
		boolean res = true;
		for(BaseNode child : getChildren()) {
			if(child instanceof ExprNode)
				res &= ((ExprNode)child).iteratedNotReferenced(iterName);
			else if(child instanceof ExprPairNode)
				res &= ((ExprPairNode)child).iteratedNotReferenced(iterName);
		}
		return res;
	}
}

/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.6
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

public class ProcedureCallNode extends EvalStatementNode
{
	static {
		setName(ProcedureCallNode.class, "procedure call eval statement");
	}

	private String procedureName;
	private CollectNode<ExprNode> params;
	private EvalStatementNode result;

	public ProcedureCallNode(Coords coords, String procedureName, CollectNode<ExprNode> params)
	{
		super(coords);
		this.procedureName = procedureName;
		this.params = becomeParent(params);
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(params);
		if(isResolved())
			children.add(result);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("params");
		if(isResolved())
			childrenNames.add("result");
		return childrenNames;
	}

	protected boolean resolveLocal() {
		if(procedureName.equals("rem")) {
			if(params.size() != 1) {
				reportError("rem(value) takes one parameter.");
				return false;
			}
			else {
//				result = new GraphRemoveNode(getCoords(), params.get(0));
			}
		}
		else if(procedureName.equals("clear")) {
			if(params.size() != 0) {
				reportError("clear() takes no parameters.");
				return false;
			}
			else {
//				result = new GraphClearNode(getCoords());
			}
		}
		else {
			reportError("no procedure named \"" + procedureName + "\" known");
			return false;
		}

		return true;
	}

	@Override
	protected boolean checkLocal() {
		return true;
	}

	@Override
	protected IR constructIR() {
		return result.getIR();
	}
}

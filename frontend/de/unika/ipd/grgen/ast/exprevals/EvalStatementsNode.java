/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.1
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.exprevals;

import java.util.Collection;
import java.util.LinkedList;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ir.exprevals.EvalStatement;
import de.unika.ipd.grgen.ir.exprevals.EvalStatements;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

public class EvalStatementsNode extends BaseNode
{
	public String name;
	public CollectNode<EvalStatementNode> evalStatements;
	
	public EvalStatementsNode(Coords coords, String name) {
		super(coords);
		this.name = name;
		evalStatements = new CollectNode<EvalStatementNode>();
	}
	
	public void addChild(EvalStatementNode c) {
		evalStatements.addChild(c);
	}
	
	@Override
	public Collection<EvalStatementNode> getChildren() {
		return evalStatements.getChildren();
	}

	@Override
	protected Collection<String> getChildrenNames() {
		LinkedList<String> res = new LinkedList<String>();
		for(int i=0; i<getChildren().size(); ++i) {
			res.add("eval"+i);
		}
		return res;
	}
	
	@Override
	protected boolean resolveLocal() {
		return true;
	}

	@Override
	protected boolean checkLocal() {
		return true;
	}
	
	@Override
	protected IR constructIR() {
		EvalStatements es = new EvalStatements(name);

		for(EvalStatementNode evalStatement : evalStatements.children) {
			es.evalStatements.add(evalStatement.checkIR(EvalStatement.class));
		}

		return es;
	}
}

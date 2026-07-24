/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ast.decl.executable.FunctionDeclNode;
import de.unika.ipd.grgen.ir.executable.Function;
import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node that represents a function auto node
 */
public abstract class FunctionAutoNode extends BaseNode
{
	static {
		setClassName(FunctionAutoNode.class, "function auto");
	}

	protected String function;
	
	public FunctionAutoNode(Coords coords, String function)
	{
		super(coords);
		this.function = function;
	}
	
	public abstract boolean resolveLocalBypass();
	
	public abstract boolean checkLocalBypass();

	public abstract boolean checkLocal(FunctionDeclNode functionDecl);

	public abstract void getStatements(FunctionDeclNode functionDecl, Function function);
}

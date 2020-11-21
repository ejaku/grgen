/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.2
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.decl.executable.FunctionDeclNode;
import de.unika.ipd.grgen.ir.executable.Function;
import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node that represents a function auto node
 */
public abstract class FunctionAutoNode extends BaseNode
{
	static {
		setName(FunctionAutoNode.class, "function auto");
	}

	protected String function;
	
	public FunctionAutoNode(Coords coords, String function)
	{
		super(coords);
		this.function = function;
	}
	
	@Override
	public abstract boolean resolveLocal();
	
	@Override
	public abstract boolean checkLocal();

	public abstract boolean checkLocal(FunctionDeclNode functionDecl);

	public abstract void getStatements(FunctionDeclNode functionDecl, Function function);
}

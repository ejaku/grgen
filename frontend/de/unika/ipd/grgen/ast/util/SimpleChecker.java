/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author shack
 */

package de.unika.ipd.grgen.ast.util;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.util.report.ErrorReporter;

/**
 * A checker that checks if the AST node is an instance of one of the specified types.
 */
public class SimpleChecker implements Checker
{
	/** The types the node is to be checked against. */
	private Class<?>[] validTypes;

	/** Create checker with one type to check the AST node against */
	public SimpleChecker(Class<?> validType)
	{
		this.validTypes = new Class[] { validType };
	}

	/** Create checker with the types to check the AST node against */
	public SimpleChecker(Class<?>[] validTypes)
	{
		this.validTypes = validTypes;
	}

	/**
	 * Just check whether the node is an instance of one of the valid types
	 * @see de.unika.ipd.grgen.ast.check.Checker#check(de.unika.ipd.grgen.ast.BaseNode, de.unika.ipd.grgen.util.report.ErrorReporter)
	 */
	@Override
	public boolean check(BaseNode bn, ErrorReporter reporter)
	{
		boolean res = false;

		// If the declaration's type is an instance of the desired class
		// everything's fine, else report errors

		for(int i = 0; i < validTypes.length; i++) {
			if(validTypes[i].isInstance(bn)) {
				res = true;
				break;
			}
		}

		if(!res) {
			if(validTypes.length == 1) {
				bn.reportError("AST node " + bn.getName() + " must be an instance of type " + shortClassName(validTypes[0]));
			} else {
				bn.reportError("AST node " + bn.getName() + " - Unknown type");
			}
		}

		return res;
	}

	/**
	 * Strip the package name from the class name.
	 * @param cls The class.
	 * @return stripped class name.
	 */
	protected static String shortClassName(Class<?> cls)
	{
		String s = cls.getName();
		return s.substring(s.lastIndexOf('.') + 1);
	}
}

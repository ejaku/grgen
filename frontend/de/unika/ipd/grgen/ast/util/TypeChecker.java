/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author adam
 */

package de.unika.ipd.grgen.ast.util;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.util.report.ErrorReporter;

/**
 * A checker that checks whether the declared type of the AST declaration node is one of the specified types
 */
public class TypeChecker implements Checker
{
	/** The types the declaration type is to be checked against */
	private Class<?>[] validTypes;

	/** Create checker with one type to check the declared type of the AST declaration node against */
	public TypeChecker(Class<?>[] types)
	{
		this.validTypes = types;
	}

	/** Create checker with the types to check the declared type of the AST declaration node against */
	public TypeChecker(Class<?> type)
	{
		this(new Class[] { type });
	}

	/**
	 * Check if node is an instance of DeclNode
	 * if so check whether the declaration has the right type
	 * @see de.unika.ipd.grgen.ast.check.Checker#check(de.unika.ipd.grgen.ast.BaseNode, de.unika.ipd.grgen.util.report.ErrorReporter)
	 */
	@Override
	public boolean check(BaseNode bn, ErrorReporter reporter)
	{
		boolean res = (bn instanceof DeclNode);

		if(!res) {
			bn.reportError("Not a " + BaseNode.getName(DeclNode.class));
		} else {
			TypeNode type = ((DeclNode)bn).getDeclType();

			res = false;
			for(Class<?> c : this.validTypes) {
				if(c.isInstance(type)) {
					res = true;
					break;
				}
			}

			if(!res)
				((DeclNode)bn).getIdentNode().reportError(getErrorMsg(validTypes, bn));

		}
		
		return res;
	}

	protected static String getExpection(Class<?> cls)
	{
		String res = "";

		try {
			res = (String)cls.getMethod("getKindStr").invoke(null);
		} catch(Exception e) {
			res = "<invalid>";
		}

		return res;
	}

	protected static String getExpectionList(Class<?>[] classes)
	{
		StringBuffer list = new StringBuffer();
		for(int i = 0; i < classes.length; i++) {
			list.append(getExpection(classes[i]));
			if(i < classes.length - 2)
				list.append(", ");
			else if(i == classes.length - 2)
				list.append(" or ");
		}
		return list.toString();
	}

	protected static String getErrorMsg(Class<?>[] classes, BaseNode bn)
	{
		return "expected a " + getExpectionList(classes);
	}
}

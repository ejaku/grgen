/*
 GrGen: graph rewrite generator tool.
 Copyright (C) 2005  IPD Goos, Universit"at Karlsruhe, Germany

 This library is free software; you can redistribute it and/or
 modify it under the terms of the GNU Lesser General Public
 License as published by the Free Software Foundation; either
 version 2.1 of the License, or (at your option) any later version.

 This library is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 Lesser General Public License for more details.

 You should have received a copy of the GNU Lesser General Public
 License along with this library; if not, write to the Free Software
 Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
 */


/**
 * @author adam
 * @version $Id$
 */
package de.unika.ipd.grgen.ast.util;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.DeclNode;
import de.unika.ipd.grgen.ast.TypeNode;
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
	public boolean check(BaseNode node, ErrorReporter reporter)
	{
		boolean res = (node instanceof DeclNode);

		if(!res) {
			node.reportError("Not a " + BaseNode.getName(DeclNode.class));
		}
		else
		{
			TypeNode type = ((DeclNode)node).getDeclType();

			res = false;
			for(Class<?> c : this.validTypes)
			{
				if(c.isInstance(type))
				{
					res = true;
					break;
				}
			}

			if(!res)
				((DeclNode)node).getIdentNode().reportError(getErrorMsg(validTypes, node));

		}
		return res;
	}

	protected String getExpection(Class<?> cls)
	{
		String res = "";

		try {
			res = (String) cls.getMethod("getKindStr").invoke(null);
		}
		catch(Exception e)
		{
			res = "<invalid>";
		}

		return res;
	}

	protected String getExpectionList(Class<?>[] classes)
	{
		StringBuffer list = new StringBuffer();
		for (int i=0; i < classes.length; i++) {
			list.append(getExpection(classes[i]));
			if ( i < classes.length - 2 )
				list.append(", ");
			else if (i == classes.length - 2 )
				list.append(" or ");
		}
		return list.toString();
	}

	protected String getErrorMsg(Class<?>[] classes, BaseNode node)
	{
		return "expected a " + getExpectionList(classes);
	}
}


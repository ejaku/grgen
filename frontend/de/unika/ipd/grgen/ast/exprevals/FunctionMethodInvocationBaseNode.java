/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2016 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.exprevals;

import de.unika.ipd.grgen.ast.CollectNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.InheritanceTypeNode;
import de.unika.ipd.grgen.ast.TypeNode;
import de.unika.ipd.grgen.parser.Coords;

public abstract class FunctionMethodInvocationBaseNode extends ExprNode
{
	static {
		setName(FunctionMethodInvocationBaseNode.class, "function method invocation base");
	}

	protected CollectNode<ExprNode> arguments;

	public FunctionMethodInvocationBaseNode(Coords coords) {
		super(coords);
	}

	protected String getTypeName(TypeNode type) {
		String typeName;
		if(type instanceof InheritanceTypeNode)
			typeName = ((InheritanceTypeNode) type).getIdentNode().toString();
		else
			typeName = type.toString();
		return typeName;
	}
	
	/** Check whether the usage adheres to the signature of the declaration */
	protected boolean checkSignatureAdhered(FunctionBase fb, IdentNode unresolved) {
		// check if the number of parameters are correct
		int expected = fb.getParameterTypes().size();
		int actual = arguments.getChildren().size();
		if (expected != actual) {
			String patternName = fb.ident.toString();
			unresolved.reportError("The function \"" + patternName + "\" needs "
					+ expected + " parameters, given are " + actual);
			return false;
		}

		// check if the types of the parameters are correct
		boolean res = true;
		for (int i = 0; i < arguments.children.size(); ++i) {
			ExprNode actualParameter = arguments.children.get(i);
			TypeNode actualParameterType = actualParameter.getType();
			TypeNode formalParameterType = fb.getParameterTypes().get(i);
			
			if(!actualParameterType.isCompatibleTo(formalParameterType)) {
				res = false;
				String exprTypeName = getTypeName(actualParameterType);
				String paramTypeName = getTypeName(formalParameterType);
				unresolved.reportError("Cannot convert " + (i + 1) + ". function argument from \""
						+ exprTypeName + "\" to \"" + paramTypeName + "\"");
			}
		}

		return res;
	}
}

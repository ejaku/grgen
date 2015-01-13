/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2015 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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

public abstract class ProcedureMethodInvocationBaseNode extends ProcedureInvocationBaseNode
{
	static {
		setName(ProcedureMethodInvocationBaseNode.class, "procedure method invocation base");
	}

	protected CollectNode<ExprNode> arguments;
	protected int context;
	
	public ProcedureMethodInvocationBaseNode(Coords coords)
	{
		super(coords);
	}

	String getTypeName(TypeNode type) {
		String typeName;
		if(type instanceof InheritanceTypeNode)
			typeName = ((InheritanceTypeNode) type).getIdentNode().toString();
		else
			typeName = type.toString();
		return typeName;
	}

	/** Check whether the usage adheres to the signature of the declaration */
	protected boolean checkSignatureAdhered(ProcedureBase pb, IdentNode unresolved) {	
		// check if the number of parameters are correct
		int expected = pb.getParameterTypes().size();
		int actual = arguments.getChildren().size();
		if (expected != actual) {
			String patternName = pb.ident.toString();
			unresolved.reportError("The procedure method \"" + patternName + "\" needs "
					+ expected + " parameters, given are " + actual);
			return false;
		}
	
		// check if the types of the parameters are correct
		boolean res = true;
		for (int i = 0; i < arguments.children.size(); ++i) {
			ExprNode actualParameter = arguments.children.get(i);
			TypeNode actualParameterType = actualParameter.getType();
			TypeNode formalParameterType = pb.getParameterTypes().get(i);
			
			if(!actualParameterType.isCompatibleTo(formalParameterType)) {
				res = false;
				String exprTypeName = getTypeName(actualParameterType);
				String paramTypeName = getTypeName(formalParameterType);
				unresolved.reportError("Cannot convert " + (i + 1) + ". procedure method argument from \""
						+ exprTypeName + "\" to \"" + paramTypeName + "\"");
			}
		}
	
		return res;
	}
}

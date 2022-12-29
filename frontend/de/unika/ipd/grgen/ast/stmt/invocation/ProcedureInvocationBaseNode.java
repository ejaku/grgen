/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.6
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.stmt.invocation;

import de.unika.ipd.grgen.ast.CollectNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.decl.executable.ProcedureDeclBaseNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.parser.Coords;

public abstract class ProcedureInvocationBaseNode extends ProcedureOrBuiltinProcedureInvocationBaseNode
{
	static {
		setName(ProcedureInvocationBaseNode.class, "procedure invocation base");
	}

	protected CollectNode<ExprNode> arguments;
	protected int context;

	protected ProcedureInvocationBaseNode(Coords coords, CollectNode<ExprNode> arguments, int context)
	{
		super(coords);
		this.arguments = becomeParent(arguments);
		this.context = context;
	}

	/** Check whether the usage adheres to the signature of the declaration */
	protected boolean checkSignatureAdhered(ProcedureDeclBaseNode pb, IdentNode unresolved, boolean isMethod)
	{
		String procedureName = pb.ident.toString();

		// check if the number of parameters are correct
		int expected = pb.getParameterTypes().size();
		int actual = arguments.getChildren().size();
		if(expected != actual) {
			unresolved.reportError("The procedure " + (isMethod ? "method " : "") + procedureName
					+ " expects " + expected + " arguments (given are " + actual + " arguments).");
			return false;
		}

		// check if the types of the parameters are correct
		boolean res = true;
		for(int i = 0; i < arguments.size(); ++i) {
			ExprNode actualParameter = arguments.get(i);
			TypeNode actualParameterType = actualParameter.getType();
			TypeNode formalParameterType = pb.getParameterTypes().get(i);

			if(!actualParameterType.isCompatibleTo(formalParameterType)) {
				res = false;
				unresolved.reportError("Cannot convert " + (i + 1) + ". argument"
						+ " from " + actualParameterType.getTypeName()
						+ " to the expected " + formalParameterType.getTypeName()
						+ " (when calling procedure " + (isMethod ? "method " : "") + pb.toStringWithDeclarationCoords() + ")"
						+ actualParameterType.toStringWithDeclarationCoordsIfCoordsAreOfInterest()
						+ formalParameterType.toStringWithDeclarationCoordsIfCoordsAreOfInterest()
						+ ".");
			}
		}

		return res;
	}
}

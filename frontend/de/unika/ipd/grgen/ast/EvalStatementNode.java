/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Moritz Kroll, Edgar Jakumeit
 * @version $Id$
 */

package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.parser.Coords;

public abstract class EvalStatementNode extends OrderedReplacementNode
{
	public EvalStatementNode(Coords coords)
	{
		super(coords);
	}

	protected boolean checkType(ExprNode value, TypeNode targetType, String statement, String parameter) {
		TypeNode givenType = value.getType();
		TypeNode expectedType = targetType;
		if(!givenType.isCompatibleTo(expectedType)) {
			String givenTypeName;
			if(givenType instanceof InheritanceTypeNode)
				givenTypeName = ((InheritanceTypeNode) givenType).getIdentNode().toString();
			else
				givenTypeName = givenType.toString();
			String expectedTypeName;
			if(expectedType instanceof InheritanceTypeNode)
				expectedTypeName = ((InheritanceTypeNode) expectedType).getIdentNode().toString();
			else
				expectedTypeName = expectedType.toString();
			reportError("Cannot convert parameter " + parameter + " of " + statement + " from \""
					+ givenTypeName + "\" to \"" + expectedTypeName + "\"");
			return false;
		}
		return true;
	}
}

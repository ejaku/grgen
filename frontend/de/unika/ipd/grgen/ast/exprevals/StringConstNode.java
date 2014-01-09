/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.2
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ast.exprevals;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A string constant.
 */
public class StringConstNode extends ConstNode
{
    public StringConstNode(Coords coords, String value) {
        super(coords, "string", value);
    }

	@Override
    public TypeNode getType() {
        return BasicTypeNode.stringType;
    }

    /** @see de.unika.ipd.grgen.ast.ConstNode#doCastTo(de.unika.ipd.grgen.ast.TypeNode) */
	@Override
    protected ConstNode doCastTo(TypeNode type) {
		throw new UnsupportedOperationException();
    }
}

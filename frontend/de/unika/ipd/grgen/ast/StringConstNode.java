/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.0
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.parser.Coords;

/**
 * A string constant.
 */
public class StringConstNode extends ConstNode
{
    public StringConstNode(Coords coords, String value) {
        super(coords, "string", value);
    }

    public TypeNode getType() {
        return BasicTypeNode.stringType;
    }

    /** @see de.unika.ipd.grgen.ast.ConstNode#doCastTo(de.unika.ipd.grgen.ast.TypeNode) */
    protected ConstNode doCastTo(TypeNode type) {
		throw new UnsupportedOperationException();
    }
}

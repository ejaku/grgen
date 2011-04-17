/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ir.Rule;

/**
 * Base class for all action type ast nodes
 */
// TODO: a lot of code duplication among the subtypes of ActionDeclNode,
// fix the copy'n'paste programming by extracting the common stuff to action node,
// with parameters giving access to the left/right patterns.
public abstract class ActionDeclNode extends DeclNode
{
	public ActionDeclNode(IdentNode id, TypeNode type) {
        super(id, type);
    }

    /**
     * Get the IR object for this action node.
     * The IR object is instance of Rule.
     * @return The IR object.
     */
    protected Rule getAction() {
        return checkIR(Rule.class);
    }
}

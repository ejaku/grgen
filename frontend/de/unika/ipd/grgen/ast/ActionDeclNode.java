/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.0
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author shack
 */

package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ir.Rule;

import java.util.Collection;
import java.util.LinkedList;
import java.util.Queue;

/**
 * Base class for all action type ast nodes
 */
// TODO: a lot of code duplication among the subtypes of ActionDeclNode,
// fix the copy'n'paste programming by extracting the common stuff to action node,
// with parameters giving access to the left/right patterns.
// First step: remove the ability for multiple right sides, not needed in practice (0 or 1 RHS allowed)
// (maybe a test must be modeled as a rule with 0 RHS in order to be able to unify with the other action decl nodes)
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

    protected PatternGraphNode getParentPatternGraph(BaseNode node) {
        if (node == null) {
            return null;
        }

        Queue<Collection<BaseNode>> queue = new LinkedList<Collection<BaseNode>>();
        for (Collection<BaseNode> parents = node.getParents(); parents != null; parents = queue.poll()) {
            for (BaseNode parent : parents) {
                if (parent instanceof PatternGraphNode) {
                    return (PatternGraphNode)parent;
                }
                Collection<BaseNode> grandParents = parent.getParents();
                if (grandParents != null && !grandParents.isEmpty()) {
                    queue.add(grandParents);
                }
            }
        }

        return null;
    }
}

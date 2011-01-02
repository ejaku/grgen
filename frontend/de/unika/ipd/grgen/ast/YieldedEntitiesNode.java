/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.7
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @file YieldedEntitiesNode.java
 * @version $Id: YieldedEntitiesNode.java 26740 2010-01-02 11:21:07Z eja $
 */
package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ir.GraphEntity;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.YieldedEntities;
import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node that represents a set of graph elements resulting from a yield
 */
public class YieldedEntitiesNode extends BaseNode {
	static {
		setName(YieldedEntitiesNode.class, "yielded entities");
	}

	public BaseNode origin; // exec; todo: subpattern usage and alternative
	private CollectNode<BaseNode> yieldedEntities = null;

	public YieldedEntitiesNode(Coords coords, CollectNode<BaseNode> yieldedEntities, BaseNode origin) {
		super(coords);
		this.yieldedEntities = yieldedEntities;
		this.origin = origin;
	}


	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		return yieldedEntities.getChildren();
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		// nameless children
		return childrenNames;
	}
	
	@Override
	protected boolean resolveLocal() {
		return true;
	}

	protected boolean checkLocal() {
		return true;
	}
	
	@Override
	protected IR constructIR() {
		YieldedEntities ye = new YieldedEntities(origin.getIR());
		
		DeclNode decl;
		for(BaseNode yield : yieldedEntities.getChildren()) {
	        if (yield instanceof ConnectionNode) {
	        	ConnectionNode conn = (ConnectionNode)yield;
	        	decl = conn.getEdge().getDecl();
	        }
	        else if (yield instanceof SingleNodeConnNode) {
	        	NodeDeclNode node = ((SingleNodeConnNode)yield).getNode();
	        	decl = node;
	        }
			else if(yield instanceof VarDeclNode) {
				decl = (VarDeclNode)yield;
			}
			else
				throw new UnsupportedOperationException("Unsupported yield (" + yield + ")");
    
			ye.AddEntity(decl.checkIR(GraphEntity.class));
		}
		
		return ye;
	}
}

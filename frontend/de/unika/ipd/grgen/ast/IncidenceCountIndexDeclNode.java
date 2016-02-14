/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2016 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ast.exprevals.CountIncidentEdgeExprNode;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.ir.EdgeType;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.IncidenceCountIndex;
import de.unika.ipd.grgen.ir.NodeType;
import de.unika.ipd.grgen.parser.ParserEnvironment;

import java.util.Collection;
import java.util.Vector;


/**
 * AST node class representing incidence count index declarations
 */
public class IncidenceCountIndexDeclNode extends IndexDeclNode {
	static {
		setName(IncidenceCountIndexDeclNode.class, "incidence count index declaration");
	}

	private String functionName; // input string, "resolved" to direction
	private int direction; // one of INCIDENT|INCOMING|OUTGOING in CountIncidentEdgeExprNode
	private IdentNode startNodeTypeUnresolved;
	private InheritanceTypeNode startNodeType;
	private IdentNode incidentEdgeTypeUnresolved;
	private InheritanceTypeNode incidentEdgeType;
	private IdentNode adjacentNodeTypeUnresolved;
	private InheritanceTypeNode adjacentNodeType;

	private static final IncidenceCountIndexTypeNode incidenceCountIndexType =
		new IncidenceCountIndexTypeNode();

	private static final DeclarationTypeResolver<InheritanceTypeNode> typeResolver = 
		new DeclarationTypeResolver<InheritanceTypeNode>(InheritanceTypeNode.class);

	public IncidenceCountIndexDeclNode(IdentNode id, String functionName, 
			IdentNode startNodeType, IdentNode incidentEdgeType, IdentNode adjacentNodeType,
			ParserEnvironment env) {
		super(id, incidenceCountIndexType);
		this.functionName = functionName;
		this.startNodeTypeUnresolved = becomeParent(startNodeType);
		this.incidentEdgeTypeUnresolved = becomeParent(incidentEdgeType!=null ? incidentEdgeType : env.getDirectedEdgeRoot());
		this.adjacentNodeTypeUnresolved = becomeParent(adjacentNodeType!=null ? adjacentNodeType : env.getNodeRoot());
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(startNodeTypeUnresolved, startNodeType));
		children.add(getValidVersion(incidentEdgeTypeUnresolved, incidentEdgeType));
		children.add(getValidVersion(adjacentNodeTypeUnresolved, adjacentNodeType));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident");
		childrenNames.add("startNodeType");
		childrenNames.add("incidentEdgeType");
		childrenNames.add("adjacentNodeType");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		if(startNodeTypeUnresolved == null) {
			reportError(functionName + "() takes 1-3 parameters.");
			return false;
		}

		if(startNodeTypeUnresolved instanceof PackageIdentNode)
			Resolver.resolveOwner((PackageIdentNode)startNodeTypeUnresolved);
		else if(startNodeTypeUnresolved instanceof IdentNode)
			fixupDefinition((IdentNode)startNodeTypeUnresolved, startNodeTypeUnresolved.getScope());
		startNodeType = typeResolver.resolve(startNodeTypeUnresolved, this);
		if(startNodeType == null)
			return false;

		if(incidentEdgeTypeUnresolved instanceof PackageIdentNode)
			Resolver.resolveOwner((PackageIdentNode)incidentEdgeTypeUnresolved);
		else if(incidentEdgeTypeUnresolved instanceof IdentNode)
			fixupDefinition((IdentNode)incidentEdgeTypeUnresolved, incidentEdgeTypeUnresolved.getScope());
		incidentEdgeType = typeResolver.resolve(incidentEdgeTypeUnresolved, this);
		if(incidentEdgeType == null)
			return false;

		if(adjacentNodeTypeUnresolved instanceof PackageIdentNode)
			Resolver.resolveOwner((PackageIdentNode)adjacentNodeTypeUnresolved);
		else if(adjacentNodeTypeUnresolved instanceof IdentNode)
			fixupDefinition((IdentNode)adjacentNodeTypeUnresolved, adjacentNodeTypeUnresolved.getScope());
		adjacentNodeType = typeResolver.resolve(adjacentNodeTypeUnresolved, this);
		if(adjacentNodeType == null)
			return false;

		return true;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean checkLocal() {
		if(functionName.equals("countIncoming"))
			direction = CountIncidentEdgeExprNode.INCOMING;
		else if(functionName.equals("countOutgoing"))
			direction = CountIncidentEdgeExprNode.OUTGOING;
		else if(functionName.equals("countIncident"))
			direction = CountIncidentEdgeExprNode.INCIDENT;
		else {
			reportError(functionName + "() is not valid, use countIncoming|countOutgoing|countIncident for defining an incidence count index.");
			return false;
		}
		
		if(!(startNodeType instanceof NodeTypeNode)) {
			reportError("first argument of "+functionName+"(.,.,.) must be a node type");
			return false;
		}
		if(!(incidentEdgeType instanceof EdgeTypeNode)) {
			reportError("second argument of "+functionName+"(.,.,.) must be an edge type");
			return false;
		}
		if(!(adjacentNodeType instanceof NodeTypeNode)) {
			reportError("third argument of "+functionName+"(.,.,.) must be a node type");
			return false;
		}
		return true;
	}
	
	@Override
	public TypeNode getDeclType() {
		assert isResolved();
	
		return incidenceCountIndexType;
	}
	
	public TypeNode getType() {
		assert isResolved();

		return startNodeType;
	}

	@Override
	protected IR constructIR() {
		IncidenceCountIndex incidenceCountIndex = new IncidenceCountIndex(getIdentNode().toString(),
				getIdentNode().getIdent(), startNodeType.checkIR(NodeType.class),
				incidentEdgeType.checkIR(EdgeType.class), direction,
				adjacentNodeType.checkIR(NodeType.class));
		return incidenceCountIndex;
	}	
}



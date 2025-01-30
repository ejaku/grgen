/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.model.decl;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.PackageIdentNode;
import de.unika.ipd.grgen.ast.expr.invocation.FunctionInvocationDecisionNode;
import de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
import de.unika.ipd.grgen.ast.model.type.IncidenceCountIndexTypeNode;
import de.unika.ipd.grgen.ast.model.type.InheritanceTypeNode;
import de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.IntTypeNode;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.model.IncidenceCountIndex;
import de.unika.ipd.grgen.ir.model.type.EdgeType;
import de.unika.ipd.grgen.ir.model.type.NodeType;
import de.unika.ipd.grgen.parser.ParserEnvironment;
import de.unika.ipd.grgen.util.Direction;

import java.util.Collection;
import java.util.Vector;

/**
 * AST node class representing incidence count index declarations
 */
public class IncidenceCountIndexDeclNode extends IndexDeclNode
{
	static {
		setName(IncidenceCountIndexDeclNode.class, "incidence count index declaration");
	}

	private String functionName; // input string, "resolved" to direction
	private Direction direction;
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
			ParserEnvironment env)
	{
		super(id, incidenceCountIndexType);
		this.functionName = functionName;
		this.startNodeTypeUnresolved = becomeParent(startNodeType);
		this.incidentEdgeTypeUnresolved = becomeParent(
				incidentEdgeType != null ? incidentEdgeType : env.getDirectedEdgeRoot());
		this.adjacentNodeTypeUnresolved = becomeParent(adjacentNodeType != null ? adjacentNodeType : env.getNodeRoot());
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(startNodeTypeUnresolved, startNodeType));
		children.add(getValidVersion(incidentEdgeTypeUnresolved, incidentEdgeType));
		children.add(getValidVersion(adjacentNodeTypeUnresolved, adjacentNodeType));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident");
		childrenNames.add("startNodeType");
		childrenNames.add("incidentEdgeType");
		childrenNames.add("adjacentNodeType");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		if(startNodeTypeUnresolved == null) {
			reportError(functionName + "() expects 1-3 parameters (but already the start node type is missing).");
			return false;
		}

		if(startNodeTypeUnresolved instanceof PackageIdentNode)
			Resolver.resolveOwner((PackageIdentNode)startNodeTypeUnresolved);
		else
			fixupDefinition(startNodeTypeUnresolved, startNodeTypeUnresolved.getScope());
		startNodeType = typeResolver.resolve(startNodeTypeUnresolved, this);
		if(startNodeType == null)
			return false;

		if(incidentEdgeTypeUnresolved instanceof PackageIdentNode)
			Resolver.resolveOwner((PackageIdentNode)incidentEdgeTypeUnresolved);
		else
			fixupDefinition(incidentEdgeTypeUnresolved, incidentEdgeTypeUnresolved.getScope());
		incidentEdgeType = typeResolver.resolve(incidentEdgeTypeUnresolved, this);
		if(incidentEdgeType == null)
			return false;

		if(adjacentNodeTypeUnresolved instanceof PackageIdentNode)
			Resolver.resolveOwner((PackageIdentNode)adjacentNodeTypeUnresolved);
		else
			fixupDefinition(adjacentNodeTypeUnresolved, adjacentNodeTypeUnresolved.getScope());
		adjacentNodeType = typeResolver.resolve(adjacentNodeTypeUnresolved, this);
		if(adjacentNodeType == null)
			return false;

		direction = FunctionInvocationDecisionNode.getDirection(functionName);
		if(direction == Direction.INVALID) {
			reportError(functionName
					+ "() is not a valid incidence count index declaration, expected is one of the count incidence function names countIncoming|countOutgoing|countIncident.");
			return false;
		}

		return true;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean checkLocal()
	{
		if(!(startNodeType instanceof NodeTypeNode)) {
			reportError("The incidence count function specification " + functionName + "()"
					+ " in the incidende count index " + ident + " declaration"
					+ " expects as 1. type (start node type) a node type, but is given type " + startNodeType.getTypeName() + ".");
			return false;
		}
		if(!(incidentEdgeType instanceof EdgeTypeNode)) {
			reportError("The incidence count function specification " + functionName + "()"
					+ " in the incidende count index " + ident + " declaration"
					+ " expects as 2. type (incident edge type) an edge type, but is given type " + incidentEdgeType.getTypeName() + ".");
			return false;
		}
		if(!(adjacentNodeType instanceof NodeTypeNode)) {
			reportError("The incidence count function specification " + functionName + "()"
					+ " in the incidende count index " + ident + " declaration"
					+ " expects as 3. type (adjacent node type) a node type, but is given type " + adjacentNodeType.getTypeName() + ".");
			return false;
		}
		return true;
	}

	@Override
	public TypeNode getDeclType()
	{
		assert isResolved();

		return incidenceCountIndexType;
	}

	@Override
	public InheritanceTypeNode getType()
	{
		assert isResolved();

		return startNodeType;
	}

	@Override
	public TypeNode getExpectedAccessType()
	{
		assert isResolved();
		
		return IntTypeNode.intType;
	}

	@Override
	protected IR constructIR()
	{
		IncidenceCountIndex incidenceCountIndex = new IncidenceCountIndex(getIdentNode().toString(),
				getIdentNode().getIdent(), startNodeType.checkIR(NodeType.class),
				incidentEdgeType.checkIR(EdgeType.class), direction,
				adjacentNodeType.checkIR(NodeType.class));
		return incidenceCountIndex;
	}
}

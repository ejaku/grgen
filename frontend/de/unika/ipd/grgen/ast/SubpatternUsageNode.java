package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.LinkedList;
import java.util.List;
import java.util.Vector;

import de.unika.ipd.grgen.ast.util.CollectPairResolver;
import de.unika.ipd.grgen.ast.util.DeclarationPairResolver;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ir.GraphEntity;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.MatchingAction;
import de.unika.ipd.grgen.ir.SubpatternUsage;

public class SubpatternUsageNode extends DeclNode
{
	static {
		setName(SubpatternUsageNode.class, "subpattern node");
	}
	
	CollectNode<ConstraintDeclNode> connections;
	CollectNode<IdentNode> connectionsUnresolved;
	
	protected ActionDeclNode type = null;	
	
	
	public SubpatternUsageNode(IdentNode n, BaseNode t, CollectNode<IdentNode> c) {
		super(n, t);
		this.connectionsUnresolved = c;
		becomeParent(this.connectionsUnresolved);
	}
	
	@Override
	public BaseNode getDeclType() {
		return type.getDeclType();
	}
	
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, type));
		children.add(getValidVersion(connectionsUnresolved, connections));
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident");
		childrenNames.add("type");
		childrenNames.add("connections");
		return childrenNames;
	}

	@Override
	protected boolean resolve() {
		if (isResolved()) {
			return resolutionResult();
		}

		debug.report(NOTE, "resolve in: " + getId() + "(" + getClass() + ")");
		boolean successfullyResolved = true;
		DeclarationResolver<ActionDeclNode> actionResolver = new DeclarationResolver<ActionDeclNode>(ActionDeclNode.class);
		type = actionResolver.resolve(typeUnresolved, this);
		successfullyResolved = type != null && successfullyResolved;
		DeclarationPairResolver<NodeDeclNode, EdgeDeclNode> connectionResolver =
			new DeclarationPairResolver<NodeDeclNode, EdgeDeclNode>(NodeDeclNode.class, EdgeDeclNode.class);
		CollectPairResolver<ConstraintDeclNode> connectionsResolver =
			new CollectPairResolver<ConstraintDeclNode>(connectionResolver);
		connections = connectionsResolver.resolve(connectionsUnresolved);
		successfullyResolved = connections!=null && successfullyResolved;
		nodeResolvedSetResult(successfullyResolved); // local result
		if (!successfullyResolved) {
			debug.report(NOTE, "resolve error");
		}

		successfullyResolved = ident.resolve() && successfullyResolved;
		successfullyResolved = (type!=null ? type.resolve() : false) && successfullyResolved;
		successfullyResolved = (connections!=null ? connections.resolve() : false) && successfullyResolved;
		return successfullyResolved;
	}
	
	@Override
	protected boolean checkLocal() {
		return true;
	}
	
	@Override
	protected IR constructIR() {
		List<GraphEntity> subpatternConnections = new LinkedList<GraphEntity>();
		for (ConstraintDeclNode c : connections.getChildren()) {
			subpatternConnections.add((GraphEntity) c.checkIR(GraphEntity.class));
		}
		return new SubpatternUsage("subpattern", getIdentNode().getIdent(), (MatchingAction)type.getIR(), subpatternConnections);
	}
}

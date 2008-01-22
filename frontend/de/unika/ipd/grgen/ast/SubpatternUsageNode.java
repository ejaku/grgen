package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.SubpatternUsage;
import de.unika.ipd.grgen.ir.MatchingAction;

public class SubpatternUsageNode extends DeclNode
{
	static {
		setName(SubpatternUsageNode.class, "subpattern node");
	}
	
	CollectNode connections;
	
	protected ActionDeclNode type = null;	
	
	
	public SubpatternUsageNode(IdentNode n, BaseNode t, CollectNode c) {
		super(n, t);
		this.connections = c;
		becomeParent(this.connections);
	}
	
	@Override
	public BaseNode getDeclType() {
		return type.getDeclType();
	}
	
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(typeUnresolved);
		children.add(connections);
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
		nodeResolvedSetResult(successfullyResolved); // local result
		if (!successfullyResolved) {
			debug.report(NOTE, "resolve error");
		}

		successfullyResolved = ident.resolve() && successfullyResolved;
		successfullyResolved = (type!=null ? type.resolve() : false) && successfullyResolved;
		return successfullyResolved;
	}
	
	@Override
	protected boolean checkLocal() {
		return true;
	}
	
	@Override
	protected IR constructIR() {
		return new SubpatternUsage("subpattern", getIdentNode().getIdent(), (MatchingAction)type.getIR());
	}
}

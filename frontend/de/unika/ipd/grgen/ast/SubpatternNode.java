package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

public class SubpatternNode extends DeclNode
{
	static {
		setName(SubpatternNode.class, "subpattern node");
	}
	
	CollectNode connections;
	
	public SubpatternNode(IdentNode n, BaseNode t, CollectNode c) {
		super(n, t);
		this.connections = c;
		becomeParent(this.connections);
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
		return true;
	}
	
	@Override
	protected boolean checkLocal() {
		return true;
	}
}

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

public class SubpatternReplNode extends BaseNode
{
	static {
		setName(SubpatternReplNode.class, "subpattern repl node");
	}
	
	IdentNode subpattern;
	CollectNode replConnections;
	
	public SubpatternReplNode(IdentNode n, CollectNode c) {
		this.subpattern = n;
		becomeParent(this.subpattern);
		this.replConnections = c;
		becomeParent(this.replConnections);
	}
	
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(subpattern);
		children.add(replConnections);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("subpattern");
		childrenNames.add("replConnections");
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

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

public class SubpatternReplNode extends BaseNode {
	static {
		setName(SubpatternReplNode.class, "subpattern repl node");
	}

	IdentNode subpattern;
	CollectNode<IdentNode> replConnections;

	public SubpatternReplNode(IdentNode n, CollectNode<IdentNode> c) {
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
		/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
		protected boolean resolveLocal() {
		return true;
	}

	@Override
		protected boolean checkLocal() {
		return true;
	}
}

/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ir.Graph;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Test;

/**
 * A type that represents tests
 */
public class TestDeclNode extends ActionDeclNode {


	private static final int PATTERN = LAST + 1;

	private static final String[] childrenNames = {
		declChildrenNames[0], declChildrenNames[1], "test"
	};

	private static final TypeNode testType = new TypeNode() { };

	static {
		setName(TestDeclNode.class, "test declaration");
		setName(testType.getClass(), "test type");
	}

	public TestDeclNode(IdentNode id, BaseNode pattern) {
		super(id, testType);
		addChild(pattern);
		setChildrenNames(childrenNames);
	}
	
	/**
	 * The children of a test type are
	 * 1) a pattern
	 * @see de.unika.ipd.grgen.ast.BaseNode#check()
	 */
	protected boolean check() {
		return checkChild(PATTERN, PatternNode.class);
	}
	
	protected IR constructIR() {
		Graph gr = ((PatternNode) getChild(PATTERN)).getGraph();
		IR test = new Test(getIdentNode().getIdent(), gr);
		
		return test;	
	}
}

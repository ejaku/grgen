/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.util.Iterator;

import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.CollectChecker;
import de.unika.ipd.grgen.ast.util.SimpleChecker;
import de.unika.ipd.grgen.ir.Graph;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Test;

/**
 * A type that represents tests
 */
public class TestDeclNode extends ActionDeclNode {


	private static final int PATTERN = LAST + 1;
	
	private static final int COND = LAST + 2;

	private static final String[] childrenNames = {
		declChildrenNames[0], declChildrenNames[1], "test", "cond"
	};

	private static final TypeNode testType = new TypeNode() { };
	
	private static final Checker condChecker = 
		new CollectChecker(new SimpleChecker(ExprNode.class));

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
	 * 2) and a cond part. 
	 * @see de.unika.ipd.grgen.ast.BaseNode#check()
	 */
	protected boolean check() {
		boolean childs = checkChild(PATTERN, PatternNode.class)
			&& checkChild(COND, ExprNode.class);
			
		boolean expr = true;
		if(childs) {
			for(Iterator it = getChild(COND).getChildren(); it.hasNext(); ) {
				// Must go right, since it is checked 5 lines above. 
				ExprNode exp = (ExprNode) it.next();
				if(!exp.getType().isEqual(BasicTypeNode.booleanType)) {
					exp.reportError("Expression must be of type boolean");
					expr = false;
				}
			}
		}
			
		return childs && expr;
	}
	
	protected IR constructIR() {
		Graph gr = ((PatternNode) getChild(PATTERN)).getGraph();
		IR test = new Test(getIdentNode().getIdent(), gr);
		
		return test;	
	}
}

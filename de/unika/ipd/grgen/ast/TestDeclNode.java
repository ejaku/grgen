/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.util.Iterator;
import java.util.Set;

import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.CollectChecker;
import de.unika.ipd.grgen.ast.util.SimpleChecker;
import de.unika.ipd.grgen.ir.Expression;
import de.unika.ipd.grgen.ir.Graph;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Test;

/**
 * A type that represents tests
 */
public class TestDeclNode extends ActionDeclNode {
    
    
    private static final int PATTERN = LAST + 1;
    private static final int NEG = LAST + 2;
    private static final int COND = LAST + 3;
    
    private static final String[] childrenNames =
        addChildrenNames(new String[] { "test", "neg" , "cond"});
    
    private static final TypeNode testType = new TypeNode() { };
    
    private static final Checker condChecker =
        new CollectChecker(new SimpleChecker(ExprNode.class));
    
    static {
        setName(TestDeclNode.class, "test declaration");
        setName(testType.getClass(), "test type");
    }
    
    public TestDeclNode(IdentNode id, BaseNode pattern, BaseNode neg, BaseNode cond) {
        super(id, testType);
        addChild(pattern);
        addChild(neg);
        addChild(cond);
        setChildrenNames(childrenNames);
    }
    
    /**
     * The children of a test type are
     * 1) a pattern
     * 2) a NAC
     * 3) and a cond part.
     * @see de.unika.ipd.grgen.ast.BaseNode#check()
     */
    protected boolean check() {
        boolean childs = checkChild(PATTERN, PatternNode.class)
			&& checkChild(NEG, PatternNode.class)
            && checkChild(COND, condChecker);
        
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

        boolean nac=true;
		if(childs) {
			// The NAC part must not contain negated edges.
			PatternNode neg = (PatternNode) getChild(NEG);
			for(Iterator it = neg.getConnections(); it.hasNext();) {
				BaseNode conn = (BaseNode) it.next();
				ConnectionCharacter cc = (ConnectionCharacter) conn;
				if (cc.isNegated()) {
					conn.reportError("Edge may not be negated in the NAC part");
					nac = false;
				}
			}
		}

		boolean homomorphic = true;
		if(childs) {
			//Nodes that occur in the NAC part but not in the left side of a rule
			//may not be mapped non-injectively.
			PatternNode neg  = (PatternNode) getChild(NEG);
			PatternNode left = (PatternNode) getChild(PATTERN);
			Set s = neg.getNodes();
			s.removeAll(left.getNodes());
			for (Iterator it = s.iterator(); it.hasNext();) {
				NodeDeclNode nd = (NodeDeclNode) it.next();
				if (nd.hasHomomorphicNodes()) {
					nd.reportError("Node must not have homomorphic nodes (because it is used in a negative section but not in the pattern)");
					homomorphic = false;
				}
			
			}
		}
        
        
        return childs && expr && nac && homomorphic;
    }
    
    protected IR constructIR() {
        Graph gr = ((PatternNode) getChild(PATTERN)).getGraph();
        Graph neg = ((PatternNode) getChild(NEG)).getGraph();
        Test test = new Test(getIdentNode().getIdent(), gr, neg);

        //Add Cond statments to the IR
        //TODO DG This is the last point i completed Tests with COND-blocks. 
        //No clue if they are used by the backends... 
		for(Iterator it = getChild(COND).getChildren(); it.hasNext();) {
			OpNode op = (OpNode) it.next();
			test.getCondition().add((Expression) op.checkIR(Expression.class));
		}
        
        return test;
    }
}

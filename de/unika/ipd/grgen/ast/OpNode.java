/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.parser.Coords;

/**
 * Expression operators.
 */
public class OpNode extends ExprNode {

	static {
		setName(OpNode.class, "operator");
	}

	/** The ID of the operator. */
	private int opId;

	/** The corresponding operator. */
	private Operator operator;

  /**
   * Make a new operator node.
   * @param coords The source coordinates of that node.
   * @param opId The operator ID.
   */
  public OpNode(Coords coords, int opId) {
    super(coords);
    this.opId = opId;
    
    for(int i = 0; i < Operator.getArity(opId); i++)
    	addResolver(i, identExprResolver);
  }
  
  public String toString() {
  	return Operator.getName(opId);
  }

  /**
   * @see de.unika.ipd.grgen.ast.BaseNode#check()
   */
  protected boolean check() {
  	boolean res = true;
  	if(children() != Operator.getArity(opId)) {
  		reportError("Wrong operator arity: " + children());
  		res = false;
  	}
  	
  	if(!checkAllChildren(ExprNode.class))
  		res = false;
  	
  	
  	return res;
  }
  
  /**
   * Determine the operator that will be used with this operator node.
   * The method gets the operand types of this node and determines the 
   * operator, that will need the least implicit type casts using the
   * operands' types (this is done via 
   * {@link Operator#getNearest(int, TypeNode[])}). If no such operator is
   * found, an error message is reported.
   * @return The proper operator for this node, <code>null</code> otherwise.
   */
  private Operator getOperator() {
  	Operator operator = null;
  	int n = children();
  	TypeNode[] opTypes = new TypeNode[n];
  	
  	for(int i = 0; i < n; i++) {
  		ExprNode op = (ExprNode) getChild(i);
  		opTypes[i] = op.getType();
  	}
  	
		operator = Operator.getNearest(opId, opTypes);
		if(operator == null) {
			StringBuffer params = new StringBuffer();
			
			params.append('(');
			for(int i = 0; i < n; i++) 
				params.append((i > 0 ? ", " : "") + opTypes[i].getName()); 			
			params.append(')');
			
			reportError("No such operator " + Operator.getName(opId) + " " + params);
		}
		
		return operator;
  }
  
  /**
   * Get the type of this expression.
   * @see de.unika.ipd.grgen.ast.ExprNode#getType()
   * If a proper operator for this node can be found, the type of this 
   * node is the result type of the operator, else it's the error type 
   * {@link BasicTypeNode#errorType}. 
   */
  public TypeNode getType() {
  	TypeNode res = BasicTypeNode.errorType;
  	
  	if(operator == null)
  		operator = getOperator();

		if(operator != null)
			res = operator.getResultType();

  	return res;
  }

	
}

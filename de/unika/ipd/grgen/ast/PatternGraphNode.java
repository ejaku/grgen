/*
  GrGen: graph rewrite generator tool.
  Copyright (C) 2005  IPD Goos, Universit"at Karlsruhe, Germany

  This library is free software; you can redistribute it and/or
  modify it under the terms of the GNU Lesser General Public
  License as published by the Free Software Foundation; either
  version 2.1 of the License, or (at your option) any later version.

  This library is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
  Lesser General Public License for more details.

  You should have received a copy of the GNU Lesser General Public
  License along with this library; if not, write to the Free Software
  Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
*/


/**
 * PatternGraphNode.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ast;
import de.unika.ipd.grgen.ast.util.*;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.ConnectionCharacter;
import de.unika.ipd.grgen.ast.ExprNode;
import de.unika.ipd.grgen.ast.GraphNode;
import de.unika.ipd.grgen.ir.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.PatternGraph;
import de.unika.ipd.grgen.parser.Coords;
import java.util.Iterator;

public class PatternGraphNode extends GraphNode {
	
	/** Index of the conditions collect node. */
	private static final int CONDITIONS = 1;
	
	/** Conditions checker. */
	private static final Checker conditionsChecker =
	  new CollectChecker(new SimpleChecker(ExprNode.class));
	
	static {
		setName(PatternGraphNode.class, "pattern_graph");
	}
	
	/**
	 * A new pattern node
	 * @param connections A collection containing connection nodes
	 * @param conditions A collection of conditions.
	 */
	public PatternGraphNode(Coords coords, BaseNode connections, BaseNode conditions) {
		super(coords, connections);
		addChild(conditions);
	}
	
	protected boolean check() {
		boolean childs = super.check() &&
			checkChild(CONDITIONS, conditionsChecker);
		
		boolean expr = true;
		if(childs) {
			for(Iterator<BaseNode> it = getChild(CONDITIONS).getChildren(); it.hasNext(); ) {
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
	
	/**
	 * Get the correctly casted IR object.
	 * @return The IR object.
	 */
	public PatternGraph getPatternGraph() {
		return (PatternGraph) checkIR(PatternGraph.class);
	}
	
	
	protected IR constructIR() {
		PatternGraph gr = new PatternGraph();
		
		for(Iterator<BaseNode> it = getChild(CONNECTIONS).getChildren(); it.hasNext();) {
			ConnectionCharacter conn = (ConnectionCharacter) it.next();
			conn.addToGraph(gr);
		}
		
		for(Iterator<BaseNode> it = getChild(CONDITIONS).getChildren(); it.hasNext();) {
			ExprNode expr = (ExprNode) it.next();
			gr.addCondition((Expression) expr.checkIR(Expression.class));
		}
		
		return gr;
	}
	
}


/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.util.HashSet;
import java.util.Iterator;
import java.util.Set;

import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.CollectChecker;
import de.unika.ipd.grgen.ast.util.SimpleChecker;
import de.unika.ipd.grgen.ir.Graph;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A class which represents a graph pattern
 */
public class PatternNode extends BaseNode {

	/** Index of the connections collect node. */
	private static final int CONNECTIONS = 0;
	
	/** Connections checker. */
	private static final Checker connectionsChecker =
	  new CollectChecker(new SimpleChecker(ConnectionCharacter.class));
	  
	static {
		setName(PatternNode.class, "pattern");
	}

	/**
	 * A new pattern node
	 * @param c A collection containing connection nodes
	 */
	public PatternNode(Coords coords, BaseNode c) {
		super(coords);
		addChild(c);
	}
	
	/**
	 * A pattern node contains just a collect node with connection nodes
	 * as its children.
	 * @see de.unika.ipd.grgen.ast.BaseNode#check()
	 */
	protected boolean check() {
		if (! checkChild(CONNECTIONS, connectionsChecker)) return false;
		
		//check, that each named edge is only used once in a pattern
		boolean edgeUsage = true;
		CollectNode collect = (CollectNode) getChild(CONNECTIONS);
		HashSet edges = new HashSet(); 
		for (Iterator i = collect.getChildren(); i.hasNext(); ) {
			ConnectionCharacter cc = (ConnectionCharacter) i.next();

			EdgeCharacter ec = cc.getEdge();
			if (ec == null) continue; //filter out SingleNodeConnNodes

			if (! edges.add(ec)) { //add returns false iff edges already contains ec
				((EdgeDeclNode) ec).reportError("This (named) edge is used more than once in a graph of this action");
				edgeUsage = false;
			}
		}
		return edgeUsage;
	}
	
	/**
	 * Get an iterator iterating over all connections characters 
	 * in this pattern.
	 * These are the children of the collect node at position 0.
	 * @return The iterator.
	 */
	protected Iterator getConnections() {
		return getChild(CONNECTIONS).getChildren();
	}
	
	/** 
	 * Get a set of all nodes in this pattern.
	 * Use this function after this not has been checked with {@link #check()}
	 * to ensure, that the children have the right type.
	 * @return A set containing the declarations of all nodes occuring
	 * in this graph pattern. 
	 */
	protected Set getNodes() {
		Set res = new HashSet();
		
		for(Iterator it = getChild(CONNECTIONS).getChildren(); it.hasNext();) {
			ConnectionCharacter conn = (ConnectionCharacter) it.next();
			conn.addNodes(res);
		}
		
		return res;
	}
	
	/**
	 * Get the correctly casted IR object.
	 * @return The IR object.
	 */
	public Graph getGraph() {
		return (Graph) checkIR(Graph.class);
	}

	/**
	 * Construct the IR object.
	 * It is a Graph and all the connections (children of the pattern AST node)
	 * are put into it.
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	protected IR constructIR() {
		Graph gr = new Graph();
		
		for(Iterator it = getChild(CONNECTIONS).getChildren(); it.hasNext();) {
			ConnectionCharacter conn = (ConnectionCharacter) it.next();
			conn.addToGraph(gr);
		}
		
		return gr;
	}

}

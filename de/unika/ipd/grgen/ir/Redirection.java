/**
 * @author Rubino Geiss
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;


/** A class representing redirections in rules. */
public class Redirection {
	public final Node from;
	public final Node to;
	public final EdgeType edgeType;
	public final NodeType nodeType;
	public final boolean incoming;
	
	public Redirection(Node from, Node to, EdgeType edgeType,
					   NodeType nodeType, boolean incoming) {
		
		this.from = from;
		this.to = to;
		this.edgeType = edgeType;
		this.nodeType = nodeType;
		this.incoming = incoming;
	}
}

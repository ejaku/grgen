/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

/**
 * Denotes the connections assertions of nodes and edges.
 */
public class ConnAssert extends IR {
	private int srcLower;
	private int srcUpper;
	private int tgtLower;
	private int tgtUpper;
	private NodeType srcType;
	private NodeType tgtType;
	
	/**
	 * Make a new edge type.
	 * @param ident The identifier declaring this type.
	 */
	public ConnAssert(NodeType srcType, int srcLower, int srcUpper,
					  NodeType tgtType, int tgtLower, int tgtUpper) {
		super("conn assert");
		this.srcType = srcType;
		this.srcLower = srcLower;
		this.srcUpper = srcUpper;
		this.tgtType = tgtType;
		this.tgtLower = tgtLower;
		this.tgtUpper = tgtUpper;
	}
	
	public NodeType getSrcType() {
		return srcType;
	}
	
	public NodeType getTgtType() {
		return tgtType;
	}
	
	public int getSrcLower() {
		return srcLower;
	}
	
	public int getSrcUpper() {
		return srcUpper;
	}
	
	public int getTgtLower() {
		return tgtLower;
	}
	
	public int getTgtUpper() {
		return tgtUpper;
	}
}

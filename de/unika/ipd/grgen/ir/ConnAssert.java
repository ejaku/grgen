/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

/**
 * Denotes the connections assertions of nodes and edges.
 */
import de.unika.ipd.grgen.util.SingleIterator;
import java.util.Map;

public class ConnAssert extends IR {
	private final int srcLower;
	private final int srcUpper;
	private final int tgtLower;
	private final int tgtUpper;
	private final NodeType srcType;
	private final NodeType tgtType;
	
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
	
	public void addFields(Map fields) {
		super.addFields(fields);
		fields.put("src_lower", Integer.toString(srcLower));
		fields.put("src_upper", Integer.toString(srcUpper));
		fields.put("tgt_lower", Integer.toString(tgtLower));
		fields.put("tgt_upper", Integer.toString(tgtUpper));
		fields.put("src_type", new SingleIterator(getSrcType()));
		fields.put("tgt_type", new SingleIterator(getTgtType()));
	}
}

package de.unika.ipd.grgen.ir;

import de.unika.ipd.grgen.util.Annotations;
import de.unika.ipd.grgen.util.Retyped;

public class RetypedEdge extends Edge implements Retyped {

	/**  The original edge */
	protected Edge oldEdge = null;
	
	public RetypedEdge(Ident ident, EdgeType type, Annotations annots) {
		super(ident, type, annots);
	}

	public Entity getOldEntity() {
		return oldEdge;
	}
	
	public void setOldEntity(Entity old) {
		this.oldEdge = (Edge)old;
	}

	/**
	 * If this is a retyped edge then this will return
	 * the original edge in the graph.
	 *
	 * @return The retyped edge
	 */
	public Edge getOldEdge() {
		return oldEdge;
	}
	
	/**
	 * Set the edge being retyped to this one
	 *
	 * @param old The edge being retyped to this one
	 */
	public void setOldEdge(Edge old) {
		this.oldEdge = old;
	}
	
	public boolean changesType() {
		return false;
	}
	
	public boolean isRetyped() {
		return true;
	}
}

package de.unika.ipd.grgen.ir;

import de.unika.ipd.grgen.util.Annotations;
import de.unika.ipd.grgen.util.EmptyAnnotations;
import de.unika.ipd.grgen.util.Retyped;

public class RetypedNode extends Node implements Retyped {

	/**  The original entity if this is a retyped entity */
	protected Node oldNode = null;
	
	public RetypedNode(Ident ident, NodeType type, Annotations annots) {
		super(ident, type, annots);
	}

	public RetypedNode(Ident ident, NodeType type, Node old) {
		this(ident, type, EmptyAnnotations.get());
		this.oldNode = old;
	}

	public Entity getOldEntity() {
		return oldNode;
	}
	
	public void setOldEntity(Entity old) {
		this.oldNode = (Node)old;
	}

	/** returns the original node in the graph. */
	public Node getOldNode() {
		return oldNode;
	}
	
	/** Set the old node being retyped to this one */
	public void setOldNode(Node old) {
		this.oldNode = old;
	}
	
	public boolean changesType() {
		return false;
	}
	
	public boolean isRetyped() {
		return true;
	}
}

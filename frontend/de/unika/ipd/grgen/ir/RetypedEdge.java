/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universität Karlsruhe, Institut für Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import de.unika.ipd.grgen.util.Annotations;
import de.unika.ipd.grgen.util.Retyped;

public class RetypedEdge extends Edge implements Retyped {

	/**  The original edge */
	protected Edge oldEdge = null;

	public RetypedEdge(Ident ident, EdgeType type, Annotations annots, boolean maybeDeleted, boolean maybeRetyped) {
		super(ident, type, annots, maybeDeleted, maybeRetyped);
	}

	public Entity getOldEntity() {
		return oldEdge;
	}

	public void setOldEntity(Entity old) {
		this.oldEdge = (Edge)old;
	}

	/** returns the original edge in the graph. */
	public Edge getOldEdge() {
		return oldEdge;
	}

	/** Set the old edge being retyped to this one */
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

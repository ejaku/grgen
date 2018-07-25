/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2018 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir;

import de.unika.ipd.grgen.util.Annotations;
import de.unika.ipd.grgen.util.Retyped;

public class RetypedEdge extends Edge implements Retyped {

	/**  The original edge */
	protected Edge oldEdge = null;

	public RetypedEdge(Ident ident, EdgeType type, Annotations annots,
			boolean maybeDeleted, boolean maybeRetyped, boolean isDefToBeYieldedTo, int context) {
		super(ident, type, annots, null, maybeDeleted, maybeRetyped, isDefToBeYieldedTo, context);
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

	public boolean isRetyped() {
		return true;
	}
}

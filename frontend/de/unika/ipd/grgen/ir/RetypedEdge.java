/*
 GrGen: graph rewrite generator tool.
 Copyright (C) 2008  IPD Goos, Universit"at Karlsruhe, Germany

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

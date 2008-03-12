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
import de.unika.ipd.grgen.util.EmptyAnnotations;
import de.unika.ipd.grgen.util.Retyped;

public class RetypedNode extends Node implements Retyped {

	/**  The original entity if this is a retyped entity */
	protected Node oldNode = null;

	public RetypedNode(Ident ident, NodeType type, Annotations annots, boolean maybeDeleted) {
		super(ident, type, annots, maybeDeleted);
	}

	public RetypedNode(Ident ident, NodeType type, Node old, boolean maybeDeleted) {
		this(ident, type, EmptyAnnotations.get(), maybeDeleted);
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

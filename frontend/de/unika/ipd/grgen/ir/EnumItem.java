/*
  GrGen: graph rewrite generator tool.
  Copyright (C) 2005  IPD Goos, Universit"at Karlsruhe, Germany

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



package de.unika.ipd.grgen.ir;



/**
 * An enumeration value
 */
import java.util.*;

public class EnumItem extends Identifiable {
	private final Ident id;
	
	private final Constant value;
	
	/**
	 * Make a new enumeration value.
	 *
	 * @param id The enumeration item identifier.
	 * @param value The associated value.
	 */
	public EnumItem(Ident id, Constant value) {
		super("enum item", id);
		this.id = id;
		this.value = value;
	}
	
	/**
	 * Returns the enum items identifier.
	 * @return The identifier of the enum item.
	 */
	public Ident getIdent() {
		return id;
	}
	
	/**
	 * The string of an enum item is its identifier's text.
	 * @see java.lang.Object#toString()
	 */
	public String toString() {
		return id.toString();
	}
	
	/**
	 * Get the value of the enum item.
	 * @return The value.
	 */
	public Constant getValue() {
		return value;
	}
	
	
  /**
	 * @see de.unika.ipd.grgen.util.Walkable#getWalkableChildren()
	 */
  public Collection<IR> getWalkableChildren() {
		Set<IR> res = new HashSet<IR>();
		res.add(id);
		res.add(value);
		return res;
  }
	
}

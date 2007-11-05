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


/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import java.util.Collections;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;

/**
 * An enumeration type.
 */
public class EnumType extends PrimitiveType {
	
	private final List<EnumItem> items = new LinkedList<EnumItem>();
	
	/**
	 * Make a new enum type.
	 * @param ident The identifier of this enumeration.
	 */
	public EnumType(Ident ident) {
		super("enum type", ident);
	}
	
	/**
	 * Add an item to a this enum type and autoenumerate it.
	 * @param name The identifier of the enum item.
	 */
	public void addItem(EnumItem item) {
		items.add(item);
	}
	
	/**
	 * Return iterator of all identifiers in the enum type.
	 * @return An iterator with idents.
	 */
	public List<EnumItem> getItems() {
		return Collections.unmodifiableList(items);
	}
	
	/**
	 * @see de.unika.ipd.grgen.ir.Type#classify()
	 */
	public int classify() {
		return IS_INTEGER;
	}
	
	protected void canonicalizeLocal() {
		Collections.sort(items, Identifiable.COMPARATOR);
	}
	
	void addToDigest(StringBuffer sb) {
		sb.append(this);
		sb.append('[');
		
		int i = 0;
		for(Iterator<EnumItem> it = items.iterator(); it.hasNext(); i++) {
			EnumItem ent = (EnumItem) it.next();
			if(i > 0)
				sb.append(',');
			sb.append(ent);
		}
		
		sb.append(']');
	}
	
}

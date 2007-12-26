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
 * DefaultMarkerSource.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.be.sql.stmt;

import de.unika.ipd.grgen.be.sql.meta.DataType;
import de.unika.ipd.grgen.be.sql.meta.MarkerSource;
import de.unika.ipd.grgen.be.sql.meta.Op;
import de.unika.ipd.grgen.be.sql.meta.Term;
import java.io.PrintStream;
import java.util.Collection;
import java.util.Collections;
import java.util.LinkedList;

public abstract class DefaultMarkerSource implements MarkerSource {
	
	private final Collection<Object> types = new LinkedList<Object>();
	
	private static final Op markerOp = new DefaultOp("marker");
	
	private static final class MarkerSourceTerm extends DefaultTerm {
		
		private final String text;
		private final DataType type;
		
		MarkerSourceTerm(String text, DataType type) {
			super(markerOp);
			this.text = text;
			this.type = type;
		}
		
		/**
		 * @see de.unika.ipd.grgen.be.sql.meta.MetaBase#dump(java.lang.StringBuffer)
		 */
		public void dump(PrintStream ps) {
			ps.print(text);
		}
		
		public boolean equals(Object obj) {
			return this == obj;
		}
		
		public DataType getType() {
			return type;
		}
	}
	
	/**
	 * An array containing all types for all markers got up to now.
	 * Index the array using the number of the marker in which's type
	 * you are interested in.
	 * @return An array containing all types of gotten markers.
	 */
	public Collection getTypes() {
		return Collections.unmodifiableCollection(types);
	}
	
	/**
	 * Get a term with a marker for prepared queries.
	 * @param datatype The type of the entity designated by the marker.
	 * @return A new marker term.
	 */
	public Term nextMarker(DataType type) {
		types.add(type);
		return new MarkerSourceTerm(nextMarkerString(type), type);
	}

	/**
	 * Get the next marker string.
	 * @param type The type of this marker.
	 * @return A string representing the next marker.
	 */
	protected abstract String nextMarkerString(DataType type);
	
}


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
import de.unika.ipd.grgen.util.Annotated;
import de.unika.ipd.grgen.util.Annotations;
import java.util.Comparator;
import java.util.Map;

/**
 * Identifier with an identifier.
 * This is a super class for all classes which are associated with
 * an identifier.
 */
public class Identifiable extends IR implements Annotated, Comparable {
	
	static final Comparator<Identifiable> COMPARATOR = new Comparator<Identifiable>() {
		public int compare(Identifiable lt, Identifiable rt) {
			return lt.getIdent().compareTo(rt.getIdent());
		}
	};
	
	/** The identifier */
	private Ident ident;
	
  /**
	 * @param name The name of the IR class
	 * @param ident The identifier associated with this IR object.
	 */
  public Identifiable(String name, Ident ident) {
		super(name);
		this.ident = ident;
  }
	
  /**
	 * @return The identifier that identifies this IR structure.
	 */
  public Ident getIdent() {
		return ident;
  }
	
  /**
	 * Set the identifier for this object.
	 * @param ident The identifier.
	 */
  public void setIdent(Ident ident) {
		this.ident = ident;
  }
	
  /**
	 * @see de.unika.ipd.grgen.util.GraphDumpable#getNodeLabel()
	 */
  public String getNodeLabel() {
		return toString();
  }
	
	public String getNodeInfo() {
		return ident.getNodeInfo();
	}
	
	public String toString() {
		return getName() + " " + ident;
	}
	
	public void addFields(Map<String, Object> fields) {
		fields.put("ident", ident.toString());
	}
	
	public int hashCode() {
		return getIdent().hashCode();
	}
	
	public int compareTo(Object obj) {
		return COMPARATOR.compare(this,(Identifiable) obj);
	}
	
	/**
	 * Get the annotations.
	 * @return The annotations.
	 */
	public Annotations getAnnotations() {
		return getIdent().getAnnotations();
	}
	
}

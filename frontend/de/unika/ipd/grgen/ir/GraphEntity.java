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
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import de.unika.ipd.grgen.util.Annotations;

import java.util.Collection;
import java.util.Collections;
import java.util.Map;

/**
 * An instantiation of a type.
 */
public abstract class GraphEntity extends Entity {

	/** Type of the entity. */
	protected final InheritanceType type;

	/** The annotations of this entity. */
	protected final Annotations annotations;

	/** The retyped version of this entity if any. */
	protected GraphEntity retyped = null;
	
	/** The entity from which this one will inherit its dynamic type */
	protected GraphEntity typeof = null;

	protected Collection<? extends InheritanceType> constraints = Collections.emptySet();
	
	
	/**
	 * Make a new graph entity of a given type
	 * @param name The name of the entity.
	 * @param ident The declaring identifier.
	 * @param type The type used in the declaration.
	 */
	protected GraphEntity(String name, Ident ident, InheritanceType type, Annotations annots) {
		super(name, ident, type);
		setChildrenNames(childrenNames);
		this.type = type;
		this.annotations = annots;
	}
	
	/**
	 * Method getInheritanceType
	 *
	 * @return   an InheritanceType
	 */
	public InheritanceType getInheritanceType() {
		return type;
	}

	/**
	 * Sets the entity this one inherits its dynamic type from
	 * @param typeof The entity this one inherits its dynamic type from
	 */
	public void setTypeof(GraphEntity typeof) {
		this.typeof = typeof;
	}
	
	/**
	 * Sets the type constraints for this entity
	 * @param  expr The type constraints for this entity
	 */
	public void setConstraints(TypeExpr expr) {
		this.constraints = expr.evaluate();
	}
	
	/**
	 * Get the annotations.
	 * @return The annotations.
	 */
	public Annotations getAnnotations() {
		return annotations;
	}
	
	public void addFields(Map<String, Object> fields) {
		super.addFields(fields);
		fields.put("valid_types", constraints.iterator());
		fields.put("retyped", Collections.singleton(retyped));
		fields.put("typeof", Collections.singleton(typeof));
	}
	
	/**
	 * Check, whether this is a retyped entity.
	 * @return true, if this is a retyped entity
	 */
	public boolean isRetyped() {
		return false;
	}
	
	/**
	 * Check, whether this entity changes its type during rewrite.
	 * @return true, if this entity changes its type
	 */
	public boolean changesType() {
		return retyped != null;
	}
	
	/**
	 * Sets the corresponding retyped version of this entity
	 * @param retyped The retyped version
	 */
	public void setRetypedEntity(GraphEntity retyped) {
		this.retyped = retyped;
	}
	
	/**
	 * Returns the corresponding retyped version of this entity
	 * @return The retyped version or <code>null</code>
	 */
	public GraphEntity getRetypedEntity() {
		return this.retyped;
	}
	
	/**
	 * Get the edge from which this entity inherits its dynamic type
	 */
	public GraphEntity getTypeof() {
		return typeof;
	}
	
	/**
	 * Checks whether this entity inherits its type from some other entity
	 * @return true, if this entity inherits its type
	 */
	public boolean inheritsType() {
		return typeof != null;
	}

	public final Collection<InheritanceType> getConstraints() {
		return Collections.unmodifiableCollection(constraints);
	}
	
	public String getNodeInfo() {
		return super.getNodeInfo()
			+ "\nconstraints: " + getConstraints();
	}
}

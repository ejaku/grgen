/**
 * ConstraintEntity.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ir;

import de.unika.ipd.grgen.util.Attributes;
import java.util.Collection;
import java.util.Collections;
import java.util.Map;

/**
 * An entity that has type constraints.
 * The constraint set contains all allowed types.
 */
public class ConstraintEntity extends Entity {

	private Collection<InheritanceType> constraints = Collections.EMPTY_SET;
	
	private final InheritanceType type;
	
	protected ConstraintEntity(String name, Ident ident, InheritanceType type,
														 Attributes attr) {
		super(name, ident, type, attr);
		this.type = type;
  }
	
	public void setConstraints(TypeExpr expr) {
		constraints = expr.evaluate();
	}
	
	public final Collection getConstraints() {
		return Collections.unmodifiableCollection(constraints);
	}
	
	public void addFields(Map<String, Object> fields) {
		super.addFields(fields);
		fields.put("valid_types", constraints.iterator());
	}
	
	public final InheritanceType getInheritanceType() {
		return type;
	}
	
	public String getNodeInfo() {
		return super.getNodeInfo()
			+ "\nconstraints: " + getConstraints();
	}
	
}


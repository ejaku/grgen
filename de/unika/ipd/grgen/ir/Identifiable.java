/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;
import de.unika.ipd.grgen.util.Attributed;
import de.unika.ipd.grgen.util.Attributes;
import java.util.Comparator;
import java.util.Map;

/**
 * Identifier with an identifier.
 * This is a super clas for all classes which are associated with
 * an identifier.
 */
public class Identifiable extends IR implements Attributed, Comparable {
	
	static final Comparator COMPARATOR = new Comparator() {
		public int compare(Object lhs, Object rhs) {
			Identifiable lt = (Identifiable) lhs;
			Identifiable rt = (Identifiable) rhs;
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
	
	public String toString() {
		return getName() + " " + ident;
	}
	
	public void addFields(Map fields) {
		fields.put("ident", ident.toString());
	}
	
	public int hashCode() {
		return getIdent().hashCode();
	}
	
	public int compareTo(Object obj) {
		return COMPARATOR.compare(this, obj);
	}
	
	/**
	 * Get the attributes.
	 * @return The atttributes.
	 */
	public Attributes getAttributes() {
		return getIdent().getAttributes();
	}
	
}

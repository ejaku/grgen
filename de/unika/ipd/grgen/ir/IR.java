/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import de.unika.ipd.grgen.util.*;

import de.unika.ipd.grgen.ir.Identifiable;
import java.awt.Color;
import java.util.Comparator;
import java.util.Iterator;
import java.util.Map;

/**
 * Base class for all IR classes.
 */
public abstract class IR extends Base implements Walkable, GraphDumpable, XMLDumpable {

	protected static final Iterator emptyIterator =
	  new ArrayIterator(new Object[] { });
	
	private static final IR bad = new Bad();

	private static final String[] noChildrenNames = { };
	
	private boolean canonicalValid = false;

	/** Names of the children of this node */
	private String[] childrenNames;

	/** The name of this IR object */
	private String name;

	/**
	 * Make a new IR object with a name.
	 * @param name The name.
	 */
	protected IR(String name) {
		this.name = name;
		childrenNames = noChildrenNames;
	}
	
	/**
	 * Is this ir object bad.
	 * @return true, if the ir object is bad, false otherwise.
	 */
	public boolean isBad() {
		return false;
	}

	/**
	 * Get a bad ir object.
	 * @return A bad ir object.
	 */
	public static IR getBad() {
		return bad;
	}

	/**
	 * Get the name of this IR object.
	 * That is (group, node, edge, test, ...)
	 * @return The name of this IR object.
	 */
	public String getName() {
		return name;
	}
	
	/**
	 * Re-set the name of an IR object.
	 * @param s The new name.
	 */
	protected void setName(String s) {
		name = s;
	}
	
	/**
	 * View an IR object as a string.
	 * The string of an IR object is its name.
	 * @see java.lang.Object#toString()
	 */
	public String toString() {
		return name;
	}
	
	/**
	 * Set the names of the children of this node.
	 * @param names A string array with the names.
	 */
	protected void setChildrenNames(String[] names) {
		this.childrenNames = names;
	}

  /**
   * By default this object has the number of the edge as edge label.
   * @see de.unika.ipd.grgen.util.GraphDumpable#getEdgeLabel(int)
   */
  public String getEdgeLabel(int edge) {
  	return edge < childrenNames.length ? childrenNames[edge] : "" + edge;
  }

  /**
   * @see de.unika.ipd.grgen.util.GraphDumpable#getNodeColor()
   */
  public Color getNodeColor() {
    return Color.WHITE;
  }

  /**
   * @see de.unika.ipd.grgen.util.GraphDumpable#getNodeId()
   */
  public String getNodeId() {
    return getId();
  }

  /**
   * @see de.unika.ipd.grgen.util.GraphDumpable#getNodeInfo()
   */
  public String getNodeInfo() {
    return "ID: " + getId();
  }

  /**
   * @see de.unika.ipd.grgen.util.GraphDumpable#getNodeLabel()
   */
  public String getNodeLabel() {
    return name;
  }

  /**
   * @see de.unika.ipd.grgen.util.GraphDumpable#getNodeShape()
   */
  public int getNodeShape() {
    return GraphDumper.DEFAULT;
  }


  /**
   * @see de.unika.ipd.grgen.util.Walkable#getWalkableChildren()
   */
  public Iterator getWalkableChildren() {
  	return emptyIterator;
  }
	
	/**
	 * Get the name of the tag.
	 * @return The tag string.
	 */
	public String getTagName() {
		return getName().replace(' ', '_');
	}
	
	/**
	 * Name of the tag that expression a reference to
	 * this object.
	 * @return The ref tag name.
	 */
	public String getRefTagName() {
		return getName().replace(' ', '_') + "_ref";
	}
	
	/**
	 * Get a unique ID for this object.
	 * @return A unique ID.
	 */
	public String getXMLId() {
		return getId();
	}
	
	/**
	 * Add the XML fields to a map.
	 * @param fields The map to add the fields to.
	 */
	public void addFields(Map fields) {
	}
	
	/**
	 * Build the canonical form.
	 * Compound types must sort their members alphabetically.
	 */
	protected void canonicalizeLocal() {
	}
	
	public final void canonicalize() {
		if(!canonicalValid) {
			canonicalizeLocal();
			canonicalValid = true;
		}
	}
	
	protected final void invalidateCanonical() {
		canonicalValid = false;
	}
	
	/**
	 * Add this type to the digest.
	 */
	void addToDigest(StringBuffer sb) {
	}
}

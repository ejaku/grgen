/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import de.unika.ipd.grgen.util.Util;
import java.security.MessageDigest;
import java.util.Collections;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;

/**
 * A unit with all declared entities
 */
public class Unit extends Identifiable {

	private final List actions = new LinkedList();
	
	private final List models = new LinkedList();
	
	private String digest = "";
	
	private boolean digestValid = false;
	
	/** The source filename of this unit. */
	private String filename;

	public Unit(Ident ident, String filename) {
		super("unit", ident);
	}
	
	/**
	 * Add entity to the unit.
	 * An entity as a declrared object which has a type,
	 * such as a group, a test, etc.
	 * @param ent The entitiy to add
	 */
	public void addAction(Action action) {
		actions.add(action);
	}
	
	public Iterator getActions() {
		return actions.iterator();
	}

	public void addModel(Model model) {
		models.add(model);
		digestValid = false;
	}
	
	/**
	 * Get the type model of this unit.
	 * @return The type model.
	 */
	public Iterator getModels() {
		return models.iterator();
	}
	
  /**
   * @see de.unika.ipd.grgen.util.Walkable#getWalkableChildren()
   */
  public Iterator getWalkableChildren() {
		return actions.iterator();
  }

  /**
   * Get the source filename corresponding to this unit.
   * @return The source filename.
   */
  public String getFilename() {
    return filename;
  }
	
	public void addFields(Map fields) {
		super.addFields(fields);
		fields.put("models", models.iterator());
	}
	
	protected void canonicalizeLocal() {
		Collections.sort(models, Identifiable.COMPARATOR);
		Collections.sort(actions, Identifiable.COMPARATOR);
		
		for(Iterator it = models.iterator(); it.hasNext();) {
			Model model = (Model) it.next();
			model.canonicalize();
		}
	}

	void addToDigest(StringBuffer sb) {
		for(Iterator it = models.iterator(); it.hasNext();) {
			Model model = (Model) it.next();
			model.addToDigest(sb);
		}
	}
	
	/**
	 * Build the digest string of this type model.
	 */
	private void buildDigest() {
		StringBuffer sb = new StringBuffer();

		addToDigest(sb);
		
		try {
			byte[] serialData = sb.toString().getBytes("US-ASCII");
			MessageDigest md = MessageDigest.getInstance("MD5");
			digest = Util.hexString(md.digest(serialData));
		} catch (Exception e) {
			e.printStackTrace(System.err);
			digest = "<error>";
		}
		
		digestValid = true;
	}
	
	/**
	 * Get the digest of all type models. model's digest.
	 * @return The deigest of this model.
	 */
	public final String getTypeDigest() {
		if(!digestValid)
			buildDigest();
		
		return digest;
	}

}

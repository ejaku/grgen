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

import java.util.*;

import de.unika.ipd.grgen.util.Util;
import java.security.MessageDigest;

/**
 * A unit with all declared entities
 */
public class Unit extends Identifiable {

	private final List<Action> actions = new LinkedList<Action>();
	
	private final List<Model> models = new LinkedList<Model>();
	
	private String digest = "";
	
	private boolean digestValid = false;
	
	/** The source filename of this unit. */
	private String filename;

	public Unit(Ident ident, String filename) {
		super("unit", ident);
		this.filename = filename;
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
	
	public Collection<Action> getActions() {
		return Collections.unmodifiableCollection(actions);
	}

	public void addModel(Model model) {
		models.add(model);
		digestValid = false;
	}
	
	/**
	 * Get the type model of this unit.
	 * @return The type model.
	 */
	public Collection<Model> getModels() {
		return Collections.unmodifiableCollection(models);
	}

  /**
   * Get the source filename corresponding to this unit.
   * @return The source filename.
   */
  public String getFilename() {
    return filename;
  }
	
	public void addFields(Map<String, Object> fields) {
		super.addFields(fields);
		fields.put("models", models.iterator());
	}
	
	protected void canonicalizeLocal() {
		Collections.sort(models, Identifiable.COMPARATOR);
		Collections.sort(actions, Identifiable.COMPARATOR);
		
		for(Iterator<Model> it = models.iterator(); it.hasNext();) {
			Model model = it.next();
			model.canonicalize();
		}
	}

	void addToDigest(StringBuffer sb) {
		for(Iterator<Model> it = models.iterator(); it.hasNext();) {
			Model model = it.next();
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

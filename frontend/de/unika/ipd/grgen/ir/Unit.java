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

import java.security.MessageDigest;
import java.util.Collection;
import java.util.Collections;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;

import de.unika.ipd.grgen.ast.SubpatternNode;
import de.unika.ipd.grgen.util.Util;

/**
 * A unit with all declared entities
 */
public class Unit extends Identifiable {

	private final List<Action> actions = new LinkedList<Action>();

	private final List<Action> subpatterns = new LinkedList<Action>();
	
	private final List<Model> models = new LinkedList<Model>();
	
	private String digest = "";
	
	private boolean digestValid = false;
	
	/** The source filename of this unit. */
	private String filename;

	public Unit(Ident ident, String filename) {
		super("unit", ident);
		this.filename = filename;
	}
	
	/** Add an action to the unit. */
	public void addAction(Action action) {
		actions.add(action);
	}
	
	public Collection<Action> getActions() {
		return Collections.unmodifiableCollection(actions);
	}
	
	/** Add a subpattern to the unit. */
	public void addSubpattern(Action action) {
		subpatterns.add(action);
	}
	
	public Collection<Action> getSubpatterns() {
		return Collections.unmodifiableCollection(subpatterns);
	}

	/** Add a model to the unit. */
	public void addModel(Model model) {
		models.add(model);
		digestValid = false;
	}
	
	/** @return The type model of this unit. */
	public Collection<Model> getModels() {
		return Collections.unmodifiableCollection(models);
	}

	/** @return The source filename corresponding to this unit. */
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
		Collections.sort(subpatterns, Identifiable.COMPARATOR);
		
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
	
	/** Build the digest string of this type model. */
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
	
	/** Get the digest of thia type model. */
	public final String getTypeDigest() {
		if(!digestValid)
			buildDigest();
		
		return digest;
	}
}

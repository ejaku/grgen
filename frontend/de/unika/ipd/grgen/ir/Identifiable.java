/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.5
 * Copyright (C) 2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;
import java.util.Comparator;
import java.util.Map;

import de.unika.ipd.grgen.util.Annotated;
import de.unika.ipd.grgen.util.Annotations;

/**
 * Identifiable with an identifier.
 * This is a super class for all classes which are associated with an identifier.
 */
public abstract class Identifiable extends IR implements Annotated, Comparable<Identifiable> {
	/** helper class for comparing objects of type Identifiable, used in compareTo */
	protected static final Comparator<Identifiable> COMPARATOR = new Comparator<Identifiable>() {
		public int compare(Identifiable lt, Identifiable rt) {
			return lt.getIdent().compareTo(rt.getIdent());
		}
	};

	/** The identifier */
	private Ident ident;

	/** @param name The name of the IR class
	 *  @param ident The identifier associated with this IR object */
	public Identifiable(String name, Ident ident) {
		super(name);
		this.ident = ident;
	}

	/** @return The identifier that identifies this IR structure. */
	public Ident getIdent() {
		return ident;
	}

	/** Set the identifier for this object. */
	public void setIdent(Ident ident) {
		this.ident = ident;
	}

	/** @see de.unika.ipd.grgen.util.GraphDumpable#getNodeLabel() */
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

	public int compareTo(Identifiable id) {
		return COMPARATOR.compare(this, id);
	}

	/** @return The annotations. */
	public Annotations getAnnotations() {
		return getIdent().getAnnotations();
	}
}

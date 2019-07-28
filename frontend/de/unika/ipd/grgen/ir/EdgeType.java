/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2019 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author shack
 */

package de.unika.ipd.grgen.ir;

import java.util.Collection;
import java.util.Collections;
import java.util.Comparator;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;

/**
 * IR class that represents edge types.
 */
public class EdgeType extends InheritanceType implements ContainedInPackage {
	private String packageContainedIn;
	
	/** The connection assertions. */
	private final List<ConnAssert> connectionAsserts = new LinkedList<ConnAssert>();

	public enum Directedness
	{
		Arbitrary, Directed, Undirected
	}

	protected Directedness directedness;

	/**
	 * Make a new edge type.
	 * @param ident The identifier declaring this type.
	 * @param modifiers The modifiers for this type.
	 * @param externalName The name of the external implementation of this type or null.
	 */
	public EdgeType(Ident ident, int modifiers, String externalName) {
		super("edge type", ident, modifiers, externalName);
	}

	public Directedness getDirectedness() { return directedness; }
	public void setDirectedness(Directedness dir) { directedness = dir; }

	/**
	 * Sorts the Connection assertion of this edge type,
	 * so that the computed graph model digest is stable according to semantically equivalent connection assertions.
	 * The order of the sorting is given by the <code>compareTo</code> method.
	 */
	public void canonicalizeConnectionAsserts() {
		Collections.sort(connectionAsserts, new Comparator<ConnAssert>() {
					public int compare(ConnAssert ca1, ConnAssert ca2) {
						return ca1.compareTo(ca2);
					}
				});
	}

	/** Add the given connection assertion to this edge type. */
	public void addConnAssert(ConnAssert ca) {
		connectionAsserts.add(ca);
	}

	/** Get all connection assertions. */
	public Collection<ConnAssert> getConnAsserts() {
		return Collections.unmodifiableCollection(connectionAsserts);
	}

	public void addFields(Map<String, Object> fields) {
		super.addFields(fields);
		fields.put("conn_asserts", connectionAsserts.iterator());
	}

	public void addToDigest(StringBuffer sb) {
		super.addToDigest(sb);

		sb.append('[');
		int i = 0;
		for(Iterator<ConnAssert> it = connectionAsserts.iterator(); it.hasNext(); i++) {
			ConnAssert ca = it.next();
			if(i > 0)
				sb.append(',');
			sb.append(ca.toString());
		}
		sb.append(']');
	}
	
	/** @see de.unika.ipd.grgen.ir.Type#classify() */
	public int classify() {
		return IS_EDGE;
	}
	
	public String getPackageContainedIn() {
		return packageContainedIn;
	}
	
	public void setPackageContainedIn(String packageContainedIn) {
		this.packageContainedIn = packageContainedIn;
	}
}

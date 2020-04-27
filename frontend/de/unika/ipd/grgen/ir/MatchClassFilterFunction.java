/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir;

import java.util.Collections;
import java.util.LinkedList;
import java.util.List;

/**
 * Base type for match class filter functions (internal and external).
 */
public abstract class MatchClassFilterFunction extends Identifiable implements MatchClassFilter, ContainedInPackage {
	private String packageContainedIn;

	/** A list of the parameters */
	protected List<Entity> params = new LinkedList<Entity>();

	/** A list of the parameter types, computed from the parameters */
	protected List<Type> parameterTypes = null;
	
	/** The match class we're a filter for */
	protected DefinedMatchType matchClass;

	public MatchClassFilterFunction(String name, Ident ident) {
		super(name, ident);
	}

	public void setMatchClass(DefinedMatchType matchClass) {
		this.matchClass = matchClass;
	}

	public DefinedMatchType getMatchClass() {
		return matchClass;
	}

	public String getPackageContainedIn() {
		return packageContainedIn;
	}
	
	public void setPackageContainedIn(String packageContainedIn) {
		this.packageContainedIn = packageContainedIn;
	}

	public String getFilterName() {
		return getIdent().toString();
	}

	/** Add a parameter to the match class filter function. */
	public void addParameter(Entity entity) {
		params.add(entity);
	}

	/** Get all parameters of this match class filter function. */
	public List<Entity> getParameters() {
		return Collections.unmodifiableList(params);
	}
	
	/** Get all parameter types of this match class filter function. */
	public List<Type> getParameterTypes() {
		if(parameterTypes==null) {
			parameterTypes = new LinkedList<Type>();
			for(Entity entity : getParameters()) {
				parameterTypes.add(entity.getType());
			}
		}
		return Collections.unmodifiableList(parameterTypes);
	}
}

/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.executable;

import java.util.Collections;
import java.util.LinkedList;
import java.util.List;

import de.unika.ipd.grgen.ir.ContainedInPackage;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.Ident;
import de.unika.ipd.grgen.ir.Identifiable;
import de.unika.ipd.grgen.ir.type.Type;

/**
 * Base type for filter functions (internal and external).
 */
public abstract class FilterFunction extends Identifiable implements Filter, ContainedInPackage
{
	private String packageContainedIn;

	/** A list of the parameters */
	protected List<Entity> params = new LinkedList<Entity>();

	/** A list of the parameter types, computed from the parameters */
	protected List<Type> parameterTypes = null;

	/** The action we're a filter for */
	protected Rule action;

	public FilterFunction(String name, Ident ident)
	{
		super(name, ident);
	}

	public void setAction(Rule action)
	{
		this.action = action;
	}

	@Override
	public Rule getAction()
	{
		return action;
	}

	@Override
	public String getPackageContainedIn()
	{
		return packageContainedIn;
	}

	public void setPackageContainedIn(String packageContainedIn)
	{
		this.packageContainedIn = packageContainedIn;
	}

	public String getFilterName()
	{
		return getIdent().toString();
	}

	/** Add a parameter to the filter function. */
	public void addParameter(Entity entity)
	{
		params.add(entity);
	}

	/** Get all parameters of this filter function. */
	public List<Entity> getParameters()
	{
		return Collections.unmodifiableList(params);
	}

	/** Get all parameter types of this filter function. */
	public List<Type> getParameterTypes()
	{
		if(parameterTypes == null) {
			parameterTypes = new LinkedList<Type>();
			for(Entity entity : getParameters()) {
				parameterTypes.add(entity.getType());
			}
		}
		return Collections.unmodifiableList(parameterTypes);
	}
}

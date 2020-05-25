/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.executable;

import java.util.ArrayList;

import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.type.DefinedMatchType;
import de.unika.ipd.grgen.ir.type.Type;

/**
 * An auto-generated match class filter.
 */
public class MatchClassFilterAutoGenerated extends IR implements MatchClassFilter
{
	protected String name;
	protected ArrayList<String> entities;
	protected ArrayList<Type> entityTypes;

	/** The match class we're a filter for */
	protected DefinedMatchType matchClass;

	public MatchClassFilterAutoGenerated(String name, ArrayList<String> entities)
	{
		super(name);
		this.name = name;
		this.entities = entities;
		if(entities != null)
			this.entityTypes = new ArrayList<Type>();
	}

	public void setMatchClass(DefinedMatchType matchClass)
	{
		this.matchClass = matchClass;
	}

	public void addEntityType(Type entityType)
	{
		this.entityTypes.add(entityType);
	}

	public DefinedMatchType getMatchClass()
	{
		return matchClass;
	}

	public String getFilterName()
	{
		return name;
	}

	public ArrayList<String> getFilterEntities()
	{
		return entities;
	}

	public ArrayList<Type> getFilterEntityTypes()
	{
		return entityTypes;
	}

	public String getSuffix()
	{
		StringBuilder sb = new StringBuilder();
		if(entities != null && entities.size() != 0 && name != "auto") {
			sb.append("<");
			boolean first = true;
			for(String entity : entities) {
				if(first)
					first = false;
				else
					sb.append(",");
				sb.append(entity);
			}
			sb.append(">");
		}
		return sb.toString();
	}
}
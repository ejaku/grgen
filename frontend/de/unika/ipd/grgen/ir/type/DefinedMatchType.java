/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.type;

import java.util.ArrayList;
import java.util.Collection;
import java.util.Collections;
import java.util.List;

import de.unika.ipd.grgen.ir.ContainedInPackage;
import de.unika.ipd.grgen.ir.Ident;
import de.unika.ipd.grgen.ir.executable.MatchClassFilter;
import de.unika.ipd.grgen.ir.pattern.Edge;
import de.unika.ipd.grgen.ir.pattern.Node;
import de.unika.ipd.grgen.ir.pattern.PatternGraph;
import de.unika.ipd.grgen.ir.pattern.Variable;

public class DefinedMatchType extends CompoundType implements ContainedInPackage
{
	private String packageContainedIn;
	private PatternGraph pattern;
	private ArrayList<MatchClassFilter> matchClassFilters;

	public DefinedMatchType(String name, Ident ident, PatternGraph pattern)
	{
		super(name, ident);
		this.pattern = pattern;
		matchClassFilters = new ArrayList<MatchClassFilter>();
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

	public void addMatchClassFilter(MatchClassFilter filter)
	{
		matchClassFilters.add(filter);
	}

	public List<MatchClassFilter> getMatchClassFilters()
	{
		return Collections.unmodifiableList(matchClassFilters);
	}

	public PatternGraph getPatternGraph()
	{
		return pattern;
	}

	public Collection<Node> getNodes()
	{
		return pattern.getNodes();
	}

	public Collection<Edge> getEdges()
	{
		return pattern.getEdges();
	}

	public Collection<Variable> getVars()
	{
		return pattern.getVars();
	}

	/** @see de.unika.ipd.grgen.ir.type.Type#classify() */
	@Override
	public TypeClass classify()
	{
		return TypeClass.IS_DEFINED_MATCH;
	}
}

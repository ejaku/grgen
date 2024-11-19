/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.0
 * Copyright (C) 2003-2024 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ir.NestingStatement;
import de.unika.ipd.grgen.ir.stmt.EvalStatement;
import de.unika.ipd.grgen.ir.type.Type;

/**
 * A procedure (has return types and parameters,
 * is a top-level object that contains nested statements, and may be contained in a package).
 */
public class Procedure extends ProcedureBase implements ContainedInPackage, NestingStatement
{
	private String packageContainedIn;

	/** A list of the parameters */
	private List<Entity> params = new LinkedList<Entity>();

	/** A list of the parameter types, computed from the parameters */
	private List<Type> parameterTypes = null;

	/** The computation statements */
	private List<EvalStatement> procedureStatements = new LinkedList<EvalStatement>();

	public Procedure(String name, Ident ident)
	{
		super(name, ident);
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

	/** Add a parameter to the procedure. */
	public void addParameter(Entity entity)
	{
		params.add(entity);
	}

	/** Get all parameters of this procedure. */
	public List<Entity> getParameters()
	{
		return Collections.unmodifiableList(params);
	}

	/** Add a computation statement to the procedure. */
	@Override
	public void addStatement(EvalStatement eval)
	{
		procedureStatements.add(eval);
	}

	/** Get all computation statements of this procedure. */
	@Override
	public List<EvalStatement> getStatements()
	{
		return Collections.unmodifiableList(procedureStatements);
	}

	/** Get all parameter types of this procedure. */
	@Override
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

/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.model.type;

import java.util.Collection;
import java.util.Collections;
import java.util.LinkedHashMap;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.executable.ExternalFunctionMethod;
import de.unika.ipd.grgen.ir.executable.ExternalProcedureMethod;

/**
 * IR class that represents external object types.
 */
public class ExternalObjectType extends InheritanceType
{
	private List<ExternalFunctionMethod> externalFunctionMethods = new LinkedList<ExternalFunctionMethod>();
	private List<ExternalProcedureMethod> externalProcedureMethods = new LinkedList<ExternalProcedureMethod>();

	private Map<String, ExternalFunctionMethod> allExternalFunctionMethods = null;
	private Map<String, ExternalProcedureMethod> allExternalProcedureMethods = null;

	/**
	 * Make a new external type.
	 * @param ident The identifier that declares this type.
	 */
	public ExternalObjectType(Ident ident)
	{
		super("external object type", ident, 0, null);
	}

	public Collection<ExternalFunctionMethod> getExternalFunctionMethods()
	{
		return Collections.unmodifiableCollection(externalFunctionMethods);
	}

	public void addExternalFunctionMethod(ExternalFunctionMethod method)
	{
		externalFunctionMethods.add(method);
		method.setOwner(this);
	}

	public void addExternalProcedureMethod(ExternalProcedureMethod method)
	{
		externalProcedureMethods.add(method);
		method.setOwner(this);
	}

	public Collection<ExternalProcedureMethod> getExternalProcedureMethods()
	{
		return Collections.unmodifiableCollection(externalProcedureMethods);
	}

	private void addExternalFunctionMethods(ExternalObjectType type)
	{
		for(ExternalFunctionMethod fm : type.getExternalFunctionMethods()) {
			String functionName = fm.getIdent().toString();
			allExternalFunctionMethods.put(functionName, fm);
		}
	}

	private void addExternalProcedureMethods(ExternalObjectType type)
	{
		for(ExternalProcedureMethod pm : type.getExternalProcedureMethods()) {
			String procedureName = pm.getIdent().toString();
			allExternalProcedureMethods.put(procedureName, pm);
		}
	}

	public Collection<ExternalFunctionMethod> getAllExternalFunctionMethods()
	{
		if(allExternalFunctionMethods == null) {
			allExternalFunctionMethods = new LinkedHashMap<String, ExternalFunctionMethod>();

			// add the members of the super types
			for(InheritanceType superType : getAllSuperTypes()) {
				addExternalFunctionMethods((ExternalObjectType)superType);
			}

			// add members of the current type
			addExternalFunctionMethods(this);
		}

		return allExternalFunctionMethods.values();
	}

	public Collection<ExternalProcedureMethod> getAllExternalProcedureMethods()
	{
		if(allExternalProcedureMethods == null) {
			allExternalProcedureMethods = new LinkedHashMap<String, ExternalProcedureMethod>();

			// add the members of the super types
			for(InheritanceType superType : getAllSuperTypes()) {
				addExternalProcedureMethods((ExternalObjectType)superType);
			}

			// add members of the current type
			addExternalProcedureMethods(this);
		}

		return allExternalProcedureMethods.values();
	}

	/** Return a classification of a type for the IR. */
	@Override
	public TypeClass classify()
	{
		return TypeClass.IS_EXTERNAL_CLASS_OBJECT;
	}
}

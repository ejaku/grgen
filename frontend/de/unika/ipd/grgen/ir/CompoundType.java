/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author shack
 */

package de.unika.ipd.grgen.ir;

import java.util.Collection;
import java.util.Collections;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;

import de.unika.ipd.grgen.ir.exprevals.FunctionMethod;
import de.unika.ipd.grgen.ir.exprevals.ProcedureMethod;

/**
 * Abstract base class for compound types containing members.
 */
public abstract class CompoundType extends Type {

	/** Collection containing all members defined in that type. */
	private List<Entity> members = new LinkedList<Entity>();

	private List<FunctionMethod> functionMethods = new LinkedList<FunctionMethod>();
	private List<ProcedureMethod> procedureMethods = new LinkedList<ProcedureMethod>();

	/**
	 * Make a new compound type.
	 * @param name The name of the type.
	 * @param ident The identifier used to declare this type.
	 */
	public CompoundType(String name, Ident ident) {
		super(name, ident);
	}

	/** Get all members of this compound type. */
	public Collection<Entity> getMembers() {
		return Collections.unmodifiableCollection(members);
	}

	/** Add a member entity to the compound type. */
	public void addMember(Entity member) {
		members.add(member);
		member.setOwner(this);
	}

	public Collection<FunctionMethod> getFunctionMethods() {
		return Collections.unmodifiableCollection(functionMethods);
	}

	public void addFunctionMethod(FunctionMethod method) {
		functionMethods.add(method);
		method.setOwner(this);
	}

	public void addProcedureMethod(ProcedureMethod method) {
		procedureMethods.add(method);
		method.setOwner(this);
	}

	public Collection<ProcedureMethod> getProcedureMethods() {
		return Collections.unmodifiableCollection(procedureMethods);
	}

	protected void canonicalizeLocal() {
		Collections.sort(members, Identifiable.COMPARATOR);
	}

	public void addFields(Map<String, Object> fields) {
		super.addFields(fields);
		fields.put("members", members.iterator());
	}

	public void addToDigest(StringBuffer sb) {
		sb.append(this);
		sb.append('[');

		int i = 0;
		for(Iterator<Entity> it = members.iterator(); it.hasNext(); i++) {
			Entity ent = it.next();
			if(i > 0)
				sb.append(',');
			sb.append(ent);
		}

		sb.append(']');
	}
}

/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */
package de.unika.ipd.grgen.ir.stmt;

import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.pattern.Variable;
import de.unika.ipd.grgen.ir.type.container.ArrayType;

public class FunctionAutoKeepOneForEachAccumulateBy extends EvalStatement
{
	protected Variable targetVar;

	private Entity member;
	private Variable accumulationMember;
	private String accumulationMethod;
	
	public FunctionAutoKeepOneForEachAccumulateBy(Variable targetVar, Entity member,
			Variable accumulationMember, String accumulationMethod)
	{
		super("function auto keep one for each accumulate by stmt");
		this.targetVar = targetVar;
		this.member = member;
		this.accumulationMember = accumulationMember;
		this.accumulationMethod = accumulationMethod;
	}

	public Entity getMember()
	{
		return member;
	}
	
	public Variable getAccumulationMember()
	{
		return accumulationMember;
	}
	
	public String getAccumulationMethod()
	{
		return accumulationMethod;
	}
	
	public Variable getTargetVar()
	{
		return targetVar;
	}
	
	public ArrayType getTargetType()
	{
		return (ArrayType)targetVar.getType();
	}
}

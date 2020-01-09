/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.containers;

import java.util.Collection;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.exprevals.*;

public class ArrayInit extends Expression {
	private Collection<ArrayItem> arrayItems;
	private Entity member;
	private ArrayType arrayType;
	private boolean isConst;
	private int anonymousArrayId;
	private static int anonymousArrayCounter;

	public ArrayInit(Collection<ArrayItem> arrayItems, Entity member, ArrayType arrayType, boolean isConst) {
		super("array init", member!=null ? member.getType() : arrayType);
		this.arrayItems = arrayItems;
		this.member = member;
		this.arrayType = arrayType;
		this.isConst = isConst;
		if(member==null) {
			anonymousArrayId = anonymousArrayCounter;
			++anonymousArrayCounter;
		}
	}

	public void collectNeededEntities(NeededEntities needs) {
		needs.add(this);
		for(ArrayItem arrayItem : arrayItems) {
			arrayItem.collectNeededEntities(needs);
		}
	}

	public Collection<ArrayItem> getArrayItems() {
		return arrayItems;
	}

	public void setMember(Entity entity) {
		assert(member==null && entity!=null);
		member = entity;
	}

	public Entity getMember() {
		return member;
	}

	public ArrayType getArrayType() {
		return arrayType;
	}

	public void forceNotConstant() {
		isConst = false;
	}

	public boolean isConstant() {
		return isConst;
	}

	public String getAnonymousArrayName() {
		return "anonymous_array_" + anonymousArrayId;
	}
}

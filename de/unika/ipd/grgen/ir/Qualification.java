package de.unika.ipd.grgen.ir;

import de.unika.ipd.grgen.util.ArrayIterator;
import java.util.Iterator;

public class Qualification extends Expression
{
	private static final String[] childrenNames = {
		"owner", "member"
	};
	
	private Entity owner;
	private Entity member;
	
	public Qualification(Entity owner, Entity member)
	{
		super("qual", member.getType());
		setChildrenNames(childrenNames);
		this.owner = owner;
		this.member = member;
	}
	
	public Iterator getWalkableChildren()
	{
		return new ArrayIterator(new Object [] { owner, member });
	}
	
	public Entity getOwner()
	{
		return owner;
	}
	
	public Entity getMember()
	{
		return member;
	}
	
	
}


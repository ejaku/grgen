/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.model;

/**
 * Denotes the connections assertions of nodes and edges.
 */
import java.util.Collections;
import java.util.Map;

import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.model.type.NodeType;

public class ConnAssert extends IR
{
	private final long srcLower;
	private final long srcUpper;
	private final long tgtLower;
	private final long tgtUpper;
	private final NodeType srcType;
	private final NodeType tgtType;
	private final boolean bothDirections;

	public ConnAssert(NodeType srcType, long srcLower, long srcUpper,
			NodeType tgtType, long tgtLower, long tgtUpper,
			boolean bothDirections)
	{
		super("conn assert");
		this.srcType = srcType;
		this.srcLower = srcLower;
		this.srcUpper = srcUpper;
		this.tgtType = tgtType;
		this.tgtLower = tgtLower;
		this.tgtUpper = tgtUpper;
		this.bothDirections = bothDirections;
	}

	public NodeType getSrcType()
	{
		return srcType;
	}

	public NodeType getTgtType()
	{
		return tgtType;
	}

	public long getSrcLower()
	{
		return srcLower;
	}

	public long getSrcUpper()
	{
		return srcUpper;
	}

	public long getTgtLower()
	{
		return tgtLower;
	}

	public long getTgtUpper()
	{
		return tgtUpper;
	}

	public boolean getBothDirections()
	{
		return bothDirections;
	}

	@Override
	public void addFields(Map<String, Object> fields)
	{
		super.addFields(fields);
		fields.put("src_lower", Long.toString(srcLower));
		fields.put("src_upper", Long.toString(srcUpper));
		fields.put("tgt_lower", Long.toString(tgtLower));
		fields.put("tgt_upper", Long.toString(tgtUpper));
		fields.put("src_type", Collections.singleton(getSrcType()));
		fields.put("tgt_type", Collections.singleton(getTgtType()));
	}

	/**
	 * Compares a given connection assert with <code>this</code> one.
	 * @return a negative integer, zero, or a positive integer as the
	 * 	       argument is less than, equal to, or greater than
	 *	       <code>this</code> connection assertion.
	 */
	public int compareTo(ConnAssert ca)
	{
		if(srcLower == ca.srcLower &&
			srcUpper == ca.srcUpper &&
			tgtLower == ca.tgtLower &&
			tgtUpper == ca.tgtUpper &&
			getSrcType() == ca.getSrcType() &&
			getTgtType() == ca.getTgtType()) {
			return 0;
		}

		if(this.srcLower < ca.srcLower) {
			if(this.srcUpper < ca.srcUpper) {
				if(this.tgtLower < ca.tgtLower) {
					if(this.tgtUpper < ca.tgtUpper)
						return -1;
				}
			}
		}

		return 1;
	}

	@Override
	public String toString()
	{
		return getName() +
				" {" +
				"(" + srcType + " [" + srcLower + ".." + srcUpper + "])," +
				"(" + tgtType + " [" + tgtLower + ".." + tgtUpper + "])" +
				"}";
	}
}

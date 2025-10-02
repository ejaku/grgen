/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.0
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/**
 * @author Veit Batz
 */

package de.unika.ipd.grgen.be.C;

import java.util.Vector;

import de.unika.ipd.grgen.ir.model.EnumItem;

public class EnumDescriptor
{
	public int type_id;
	public String name;
	public int n_items;
	public Vector<EnumItem> items = new Vector<EnumItem>();
}

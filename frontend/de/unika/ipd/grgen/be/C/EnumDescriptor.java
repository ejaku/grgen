/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/**
 * @author Veit Batz
 */

package de.unika.ipd.grgen.be.C;

import java.util.ArrayList;
import java.util.List;

import de.unika.ipd.grgen.ir.model.EnumItem;

public class EnumDescriptor
{
	public int type_id;
	public String name;
	public int n_items;
	public List<EnumItem> items = new ArrayList<EnumItem>();
}

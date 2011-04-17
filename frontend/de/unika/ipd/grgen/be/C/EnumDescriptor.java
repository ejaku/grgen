/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * EnumDescriptor.java
 *
 * @author Veit Batz
 * @version $Id$
 */

package de.unika.ipd.grgen.be.C;
import java.util.Vector;

import de.unika.ipd.grgen.ir.EnumItem;



public class EnumDescriptor
{
	public int  type_id;
	public String name;
	public int n_items;
	public Vector<EnumItem> items = new Vector<EnumItem>();
}


/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universität Karlsruhe, Institut für Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
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


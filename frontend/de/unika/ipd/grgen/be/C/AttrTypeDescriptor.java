/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * AttrTypeDescriptor.java
 *
 * @author Veit Batz
 * @version $Id$
 */

package de.unika.ipd.grgen.be.C;

public class AttrTypeDescriptor
{

	public int kind;				//0: integer, 1: boolean, 2: string, 3: enum
	public int attr_id;			//this attributes id
	public String name;			//the attr identifier used in the '.grg' file
	public int decl_owner_type_id;	//the id of the type owning this attr
	public int enum_id = -1;				//the id of the enum type (if the attr IS of an enum type)

	public static final int INTEGER = 0;
	public static final int BOOLEAN = 1;
	public static final int STRING = 2;
	public static final int ENUM = 3;

	/**
	 * Method kindToStr
	 *
	 * @param    attr_desc      a  FrameBasedBackend.AttrTypeDescriptor
	 *
	 * @return   a  String
	 */
	public static String kindToStr(AttrTypeDescriptor attr_desc)
	{
		String ret = null;

		if (attr_desc.kind == INTEGER)
			ret = "fb_kind_prim_int";
		if (attr_desc.kind == BOOLEAN)
			ret = "fb_kind_prim_boolean";
		if (attr_desc.kind == STRING)
			ret = "fb_kind_prim_string";
		if (attr_desc.kind == ENUM)
			ret = "fb_kind_enum";

		return ret;
	}

}


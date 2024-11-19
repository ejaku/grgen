/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.0
 * Copyright (C) 2003-2024 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.util;

import java.util.prefs.Preferences;
import java.util.prefs.PreferencesFactory;

/**
 * My preferences factory.
 */
public class MyPreferencesFactory implements PreferencesFactory
{
	private MyPreferences systemRoot, userRoot;

	public MyPreferencesFactory()
	{
		systemRoot = new MyPreferences(null, "");
		userRoot = new MyPreferences(null, "");
	}

	/**
	 * @see java.util.prefs.PreferencesFactory#systemRoot()
	 */
	@Override
	public Preferences systemRoot()
	{
		return systemRoot;
	}

	/**
	 * @see java.util.prefs.PreferencesFactory#userRoot()
	 */
	@Override
	public Preferences userRoot()
	{
		return userRoot;
	}
}

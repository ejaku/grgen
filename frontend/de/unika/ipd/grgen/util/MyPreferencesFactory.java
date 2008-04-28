/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.util;

import java.util.prefs.Preferences;
import java.util.prefs.PreferencesFactory;

/**
 * My preferences factory.
 */
public class MyPreferencesFactory implements PreferencesFactory {

	private MyPreferences systemRoot, userRoot;

	public MyPreferencesFactory() {
		systemRoot = new MyPreferences(null, "");
		userRoot = new MyPreferences(null, "");
	}

  /**
   * @see java.util.prefs.PreferencesFactory#systemRoot()
   */
  public Preferences systemRoot() {
    return systemRoot;
  }

  /**
   * @see java.util.prefs.PreferencesFactory#userRoot()
   */
  public Preferences userRoot() {
    return userRoot;
  }

}

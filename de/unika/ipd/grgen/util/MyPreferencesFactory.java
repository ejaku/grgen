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

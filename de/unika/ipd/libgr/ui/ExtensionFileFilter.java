/**
 * Created on Mar 16, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.libgr.ui;

import java.io.File;
import java.util.Collection;
import java.util.HashSet;

import javax.swing.filechooser.FileFilter;


/**
 * A simple file filter.
 */
public class ExtensionFileFilter extends FileFilter {

	private Collection<String> extensions = new HashSet<String>();
	private String description;

	private static final String getExtension(File f) {
		String filename = f.getName();
		int lastDot = filename.lastIndexOf('.');
		
		return lastDot != -1 ? filename.substring(lastDot + 1) : "";
	}

	public ExtensionFileFilter(String extension, String description) {
		this(new String[] { extension }, description);
	}

	public ExtensionFileFilter(String[] extensions, String description) {
		for(int i = 0; i < extensions.length; i++)
			this.extensions.add(extensions[i]);
		this.description = description;
	}
	
	/**
	 * @see javax.swing.filechooser.FileFilter#accept(java.io.File)
	 */
	public boolean accept(File f) {
		return extensions.contains(getExtension(f)) || f.isDirectory();
	}

	/**
	 * @see javax.swing.filechooser.FileFilter#getDescription()
	 */
	public String getDescription() {
		return description;
	}

}

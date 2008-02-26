/*
  GrGen: graph rewrite generator tool.
  Copyright (C) 2005  IPD Goos, Universit"at Karlsruhe, Germany

  This library is free software; you can redistribute it and/or
  modify it under the terms of the GNU Lesser General Public
  License as published by the Free Software Foundation; either
  version 2.1 of the License, or (at your option) any later version.

  This library is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
  Lesser General Public License for more details.

  You should have received a copy of the GNU Lesser General Public
  License along with this library; if not, write to the Free Software
  Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
*/


/**
 * VCGDUmperFactory.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.util;
import java.io.File;
import java.io.OutputStream;
import java.io.PrintStream;

import de.unika.ipd.grgen.Sys;



public class VCGDumperFactory implements GraphDumperFactory {

	private Sys system;

	public VCGDumperFactory(Sys system) {
		this.system = system;
	}

	public GraphDumper get(String fileNamePart) {

		String fileName = fileNamePart + ".vcg";
		OutputStream os = system.createDebugFile(new File(fileName));
		PrintStream ps = new PrintStream(os);
		return new VCGDumper(ps);
	}

}


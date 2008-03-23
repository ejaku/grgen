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
 * A GrGen backend which generates C# code for a searchplan-based implementation
 * @author Rubino Geiss
 * @version $Id$
 */
package de.unika.ipd.grgen.be.Csharp;

import java.io.File;

import de.unika.ipd.grgen.Sys;
import de.unika.ipd.grgen.be.Backend;
import de.unika.ipd.grgen.be.BackendFactory;
import de.unika.ipd.grgen.be.IDBase;
import de.unika.ipd.grgen.ir.Unit;

public class SearchPlanBackend2 extends IDBase implements Backend, BackendFactory {
	/** The unit to generate code for. */
	protected Unit unit;

	/** The output path as handed over by the frontend. */
	public File path;

	/**
	 * Returns this backend.
	 * @return This backend.
	 */
	public Backend getBackend() {
		return this;
	}

	/**
	 * Initializes this backend.
	 * @see de.unika.ipd.grgen.be.Backend#init(de.unika.ipd.grgen.ir.Unit, de.unika.ipd.grgen.util.report.ErrorReporter)
	 */
	public void init(Unit unit, Sys system, File outputPath) {
		this.unit = unit;
		this.path = outputPath;
		path.mkdirs();

		makeTypes(unit);
	}

	/**
	 * Starts the C#-code Generation of the SearchPlanBackend2
	 * @see de.unika.ipd.grgen.be.Backend#generate()
	 */
	public void generate() {
		System.out.println("The " + this.getClass() + " GrGen backend...");

		new ModelGen(this).genModel();
		new ActionsGen(this).genActionsAndSubpatterns();

		System.out.println("done!");
	}

	public void done() {}
}



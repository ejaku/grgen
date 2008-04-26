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
import java.util.HashSet;

import de.unika.ipd.grgen.Sys;
import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.be.Backend;
import de.unika.ipd.grgen.be.BackendFactory;
import de.unika.ipd.grgen.ir.InheritanceType;
import de.unika.ipd.grgen.ir.Model;
import de.unika.ipd.grgen.ir.Type;
import de.unika.ipd.grgen.ir.Unit;

public class SearchPlanBackend2 implements Backend, BackendFactory {
	/** The unit to generate code for. */
	protected Unit unit;

	/** The output path as handed over by the frontend. */
	public File path;
	
	private HashSet<String> reservedWords;

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
		
		// These names are declared as "reserved" as most of them
		// are needed in their original meaning in the generated code.
		reservedWords = new HashSet<String>();
		reservedWords.add("bool");
		reservedWords.add("char");
		reservedWords.add("decimal");
		reservedWords.add("double");
		reservedWords.add("float");
		reservedWords.add("int");
		reservedWords.add("object");
		reservedWords.add("Object");
		reservedWords.add("string");
		reservedWords.add("String");
		reservedWords.add("void");
		
		reservedWords.add("Action");
		reservedWords.add("Graph");
		reservedWords.add("IAction");
		reservedWords.add("IGraph");
		reservedWords.add("IMatch");
		reservedWords.add("IMatches");
		reservedWords.add("Match");
		reservedWords.add("Matches");
	}

	/**
	 * Starts the C#-code Generation of the SearchPlanBackend2
	 * @see de.unika.ipd.grgen.be.Backend#generate()
	 */
	public void generate() {
		System.out.println("The " + this.getClass() + " GrGen backend...");
		
		// Check whether type prefixes are needed because type names
		// use one of the names from reservedWords (results in a warning)
		String nodeTypePrefix = "", edgeTypePrefix = "";
modloop:for(Model model : unit.getModels()) {
			for(Type type : model.getTypes()) {
				if(!(type instanceof InheritanceType)) continue;
				
				String typeName = type.getIdent().toString();
				if(reservedWords.contains(typeName)) {
					BaseNode.error.warning(type.getIdent().getCoords(),
							"The reserved name \"" + typeName
							+ "\" has been used for a type. \"Node_\" and \"Edge_\""
							+ " prefixes are applied to the C# element class names to avoid errors.");
					nodeTypePrefix = "Node_";
					edgeTypePrefix = "Edge_";
					break modloop;					
				}
			}
		}

		// Generate graph models for all top level models
		ModelGen modelGen = new ModelGen(this, nodeTypePrefix, edgeTypePrefix);
		for(Model model : unit.getModels())
			modelGen.genModel(model);

		//if(unit.getActionRules().size() != 0 || unit.getSubpatternRules().size() != 0)
			new ActionsGen(this, nodeTypePrefix, edgeTypePrefix).genActionsAndSubpatterns();

		System.out.println("done!");
	}

	public void done() {}
}

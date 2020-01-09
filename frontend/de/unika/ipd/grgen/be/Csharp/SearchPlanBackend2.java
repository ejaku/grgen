/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * A GrGen backend which generates C# code for a searchplan-based implementation
 * @author Rubino Geiss
 */

package de.unika.ipd.grgen.be.Csharp;

import java.io.File;
import java.util.HashSet;

import de.unika.ipd.grgen.Sys;
import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.be.Backend;
import de.unika.ipd.grgen.be.BackendFactory;
import de.unika.ipd.grgen.ir.ActionsBearer;
import de.unika.ipd.grgen.ir.ComposedActionsBearer;
import de.unika.ipd.grgen.ir.Index;
import de.unika.ipd.grgen.ir.InheritanceType;
import de.unika.ipd.grgen.ir.Model;
import de.unika.ipd.grgen.ir.Rule;
import de.unika.ipd.grgen.ir.Type;
import de.unika.ipd.grgen.ir.Unit;

public class SearchPlanBackend2 implements Backend, BackendFactory {
	/** The unit to generate code for. */
	protected Unit unit;

	protected Sys system;

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
		this.system = system;
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
		reservedWords.add("string");
		reservedWords.add("void");
	}

	/**
	 * Starts the C#-code Generation of the SearchPlanBackend2
	 * @see de.unika.ipd.grgen.be.Backend#generate()
	 */
	public void generate() {
		System.out.println("The " + this.getClass() + " GrGen backend...");

		// Check whether type prefixes are needed because type names
		// use one of the names from reservedWords (results in a warning)
		String nodeTypePrefix = "";
		String edgeTypePrefix = "";
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

		boolean forceUnique = false;
		for(Model model : unit.getModels()) {
			if(model.isUniqueIndexDefined())
				forceUnique = true;
			if(model.isoParallel() > 0)
				forceUnique = true;
			for(@SuppressWarnings("unused") Index index : model.getIndices())
				forceUnique = true;
		}
		ActionsBearer bearer = new ComposedActionsBearer(unit);
		for(Rule actionRule : bearer.getActionRules()) {
			if(actionRule.getAnnotations().containsKey("parallelize")) { // comment out to parallelize everything as possible, for testing
				unit.toBeParallelizedActionIsExisting();
				forceUnique = true;
			}
		}

		// Generate graph models for all top level models
		ModelGen modelGen = new ModelGen(this, nodeTypePrefix, edgeTypePrefix);
		boolean modelGenerated = false;
		for(Model model : unit.getModels()) {
			if(forceUnique)
				model.forceUniqueDefined();
			
			modelGen.genModel(model);
			
			if(modelGenerated)
				throw new UnsupportedOperationException("Internal error: Only one model supported, and that was already generated");
			else
				modelGenerated = true;
		}

		modelGen = null; // throw away model generator (including filled output buffer) not needed any more -> reduce memory requirements

		//if(unit.getActionRules().size() != 0 || unit.getSubpatternRules().size() != 0)
			new ActionsGen(this, nodeTypePrefix, edgeTypePrefix).genActionlike();

		System.out.println("done!");
	}

	public void done() {}
}

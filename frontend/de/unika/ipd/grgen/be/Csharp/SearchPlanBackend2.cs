/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// A GrGen backend which generates C# code for a searchplan-based implementation
/// @author Rubino Geiss
/// </summary>

namespace de.unika.ipd.grgen.be.Csharp
{

	using System;
	using System.Collections.Generic;
	using System.IO;

	using Sys = de.unika.ipd.grgen.Sys;
	using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
	using Backend = de.unika.ipd.grgen.be.Backend;
	using BackendFactory = de.unika.ipd.grgen.be.BackendFactory;
	using Unit = de.unika.ipd.grgen.ir.Unit;
	using Index = de.unika.ipd.grgen.ir.model.Index;
	using Model = de.unika.ipd.grgen.ir.model.Model;
	using InheritanceType = de.unika.ipd.grgen.ir.model.type.InheritanceType;
	using Type = de.unika.ipd.grgen.ir.type.Type;

	public class SearchPlanBackend2 : Backend, BackendFactory
	{
		/// <summary>
		/// The unit to generate code for. </summary>
		protected internal Unit unit;

		protected internal Sys sys;

		/// <summary>
		/// The output path as handed over by the frontend. </summary>
		public DirectoryInfo path;

		private HashSet<string> reservedWords;

		/// <summary>
		/// Returns this backend. </summary>
		/// <returns> This backend. </returns>
		public virtual Backend Backend
		{
			get
			{
				return this;
			}
		}

		/// <summary>
		/// Initializes this backend. </summary>
		/// <seealso cref="de.unika.ipd.grgen.be.Backend.init(de.unika.ipd.grgen.ir.Unit, de.unika.ipd.grgen.util.report.ErrorReporter)"/>
		public virtual void Init(Unit unit, Sys sys, DirectoryInfo outputPath)
		{
			this.unit = unit;
			this.sys = sys;
			this.path = outputPath;
			FileAndDirectoryHelper.Mkdirs(path);

			// These names are declared as "reserved" as most of them
			// are needed in their original meaning in the generated code.
			reservedWords = new HashSet<string>();
			reservedWords.Add("bool");
			reservedWords.Add("char");
			reservedWords.Add("decimal");
			reservedWords.Add("double");
			reservedWords.Add("float");
			reservedWords.Add("int");
			reservedWords.Add("object");
			reservedWords.Add("string");
			reservedWords.Add("void");
		}

		/// <summary>
		/// Starts the C#-code Generation of the SearchPlanBackend2 </summary>
		/// <seealso cref="de.unika.ipd.grgen.be.Backend.generate()"/>
		public virtual void Generate()
		{
			Console.WriteLine("The " + this.GetType() + " GrGen backend...");

			// Check whether type prefixes are needed because type names
			// use one of the names from reservedWords (results in a warning)
			string nodeTypePrefix = "";
			string edgeTypePrefix = "";
			string objectTypePrefix = "";
			string transientObjectTypePrefix = "";
			foreach(Model model in unit.Models)
			{
				foreach(Type type in model.Types)
				{
					if(!(type is InheritanceType))
						continue;

					string typeName = type.Ident.ToString();
					if(reservedWords.Contains(typeName))
					{
						BaseNode.error.Warning(type.Ident.Coords,
								"The reserved name \"" + typeName
										+ "\" has been used for a type. \"Node_\" and \"Edge_\" and \"Object_\" and \"TransientObject_\""
										+ " prefixes are applied to the C# element class names to avoid errors.");
						nodeTypePrefix = "Node_";
						edgeTypePrefix = "Edge_";
						objectTypePrefix = "Object_";
						transientObjectTypePrefix = "TransientObject_";
						goto modloopBreak;
					}
				}
		modloopContinue:;
			}
	modloopBreak:

			bool forceUniqueDefined = false;
			foreach(Model model in unit.Models)
			{
				if(model.IsUniqueIndexDefined())
					forceUniqueDefined = true;
			}

			bool forceUniqueResulting = forceUniqueDefined;
			foreach(Model model in unit.Models)
			{
				if(model.AreFunctionsParallel())
					forceUniqueResulting = true;
				if(model.IsoParallel > 0)
					forceUniqueResulting = true;
	// JAVA TO C# CONVERTER TASK: Most Java annotations will not have direct .NET equivalent attributes:
	// ORIGINAL LINE: for(@SuppressWarnings("unused") de.unika.ipd.grgen.ir.model.Index index : model.getIndices())
				foreach(Index index in model.Indices)
					forceUniqueResulting = true;
			}

			// Generate graph models for all top level models
			ModelGen modelGen = new ModelGen(this, nodeTypePrefix, edgeTypePrefix, objectTypePrefix, transientObjectTypePrefix);
			bool modelGenerated = false;
			foreach(Model model in unit.Models)
			{
				if(forceUniqueDefined)
					model.ForceUniqueDefined();
				if(forceUniqueResulting)
					model.ForceUniqueResulting();

				modelGen.GenModel(model);

				if(modelGenerated)
					throw new System.NotSupportedException("Internal error: Only one model supported, and that was already generated");
				else
					modelGenerated = true;
			}

			modelGen = null; // throw away model generator (including filled output buffer) not needed any more -> reduce memory requirements

			//if(unit.getActionRules().size() != 0 || unit.getSubpatternRules().size() != 0)
			(new ActionsGen(this, nodeTypePrefix, edgeTypePrefix, objectTypePrefix, transientObjectTypePrefix)).GenActionlike();

			Console.WriteLine("done!");
		}

		public virtual void Done()
		{
			// nothing to do
		}
	}

}

/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author shack
/// </summary>

namespace de.unika.ipd.grgen.be
{
	using System.IO;

	using Sys = de.unika.ipd.grgen.Sys;
	using Unit = de.unika.ipd.grgen.ir.Unit;

	/// <summary>
	/// Generic Backend interface.
	/// </summary>
	public interface Backend
	{
		/// <summary>
		/// Initialize the backend with the intermediate representation. </summary>
		/// <param name="unit"> The intermediate representation unit to
		/// generate code for. </param>
		/// <param name="sys"> The sys(tem). </param>
		/// <param name="outputPath"> The output path, where
		/// all generated files should go. </param>
		void Init(Unit unit, Sys sys, DirectoryInfo outputPath);

		/// <summary>
		/// Initiates the generation of code.
		/// It is always called after <seealso cref="init(IR)"/>.
		/// </summary>
		void Generate();

		/// <summary>
		/// Clearup some things, perhaps.
		/// Called after <seealso cref="generate"/>.
		/// </summary>
		void Done();
	}

}

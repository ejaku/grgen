/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Sebastian Hack
/// </summary>

namespace de.unika.ipd.grgen.parser.antlr
{

	using System;
	using System.Collections.Generic;
	using System.IO;

	using Antlr.Runtime;

	using Sys = de.unika.ipd.grgen.Sys;
	using de.unika.ipd.grgen.ast;
	using ModelNode = de.unika.ipd.grgen.ast.model.decl.ModelNode;
	using ParserEnvironment = de.unika.ipd.grgen.parser.ParserEnvironment;

	/// <summary>
	/// Ease the ANTLR parser calling
	/// </summary>
	public class GRParserEnvironment : ParserEnvironment
	{
		private bool hadError_ = false;
		private Stack<SubunitInclude> includes = new Stack<SubunitInclude>();
		private HashSet<string> filesOnStack = new HashSet<string>();
		private HashSet<string> modelsOnStack = new HashSet<string>();
		private Dictionary<string, ModelNode> models = new Dictionary<string, ModelNode>();

		/// <summary>
		/// The base directory of the specification or null for the current directory </summary>
		private DirectoryInfo baseDir = null;

		private string filename;

		public GRParserEnvironment(Sys sys)
			: base(sys)
		{
		}

		public override void PushFile(Lexer lexer, FileInfo file)
		{
			if(baseDir != null && !Path.IsPathRooted(file.FullName))
				file = FileAndDirectoryHelper.GetFileInfo(baseDir, file.Name);

			string filePath = file.FullName;
			if(filesOnStack.Contains(filePath))
			{
				Console.Error.WriteLine("GrGen: [ERROR at " + Filename + ":" + lexer.Line
						+ "," + lexer.CharPositionInLine + "] found circular include with file \""
						+ filePath + "\"");
				Environment.Exit(1);
			}
			filesOnStack.Add(filePath);

			try
			{
				// save current lexer's state
				ICharStream input = lexer.CharStream;
				int marker = input.Mark();
				includes.Push(new SubunitInclude(input, marker));

				// switch on new input stream
				ANTLRFileStream stream = new ANTLRFileStream(file.FullName);
				lexer.CharStream = stream;
				lexer.Reset();
				filename = file.FullName;
			}
			catch(IOException)
			{
				Console.Error.WriteLine("GrGen: [ERROR at " + Filename + ":" + lexer.Line
						+ "," + lexer.CharPositionInLine + "] included file could not be found: \""
						+ filePath + "\"");
				Environment.Exit(1);
			}
		}

		public override bool PopFile(Lexer lexer)
		{
			// We've got EOF on an include (not a model using or the initial parser).
			if(includes.Count > 1 && includes.Peek().charStream != null)
			{
				filesOnStack.Remove(lexer.SourceName);

				SubunitInclude include = includes.Pop();
				lexer.CharStream = include.charStream;
				lexer.CharStream.Rewind(include.marking);
				filename = lexer.CharStream.SourceName;
				return true;
			}

			return false;
		}

		public override string Filename
		{
			get
			{
				return filename;
			}
		}

		public override UnitNode ParseActions(FileInfo inputFile)
		{
			UnitNode root = null;

			baseDir = inputFile.Directory;

			try
			{
				ANTLRFileStream stream = new ANTLRFileStream(inputFile.FullName);
				GrGenLexer lexer = new GrGenLexer(stream);
				lexer.Env = this;
				CommonTokenStream tokenStream = new CommonTokenStream(lexer);
				GrGenParser parser = new GrGenParser(tokenStream);
				includes.Push(new SubunitInclude(parser));
				filename = inputFile.FullName;

				try
				{
					parser.Env = this;
					root = parser.TextActions;
					hadError_ = hadError_ || parser.HadError;
				}
				catch(RecognitionException e)
				{
					Console.Error.WriteLine(e.StackTrace);
					Console.Error.WriteLine("parser exception: " + e.Message);
					Environment.Exit(1);
				}

				includes.Pop();
			}
			catch(IOException e)
			{
				Console.Error.WriteLine("input file not found: " + e.Message);
				Environment.Exit(1);
			}

			return root;
		}

		public override ModelNode ParseModel(FileInfo inputFile)
		{
			ModelNode root = null;

			string filePath = inputFile.FullName;
			if(modelsOnStack.Contains(filePath))
			{
				Console.Error.WriteLine("GrGen: [ERROR at " + Filename + /*":" + curlexer.getLine()
						+ "," + curlexer.getCharPositionInLine() +*/ "] found circular model usage with file \""
						+ filePath + "\"");
				Environment.Exit(1);
			}

			models.TryGetValue(filePath, out root);
			if(root != null)
				return root;

			modelsOnStack.Add(filePath);

			try
			{
				ANTLRFileStream stream = new ANTLRFileStream(inputFile.FullName);
				GrGenLexer lexer = new GrGenLexer(stream);
				lexer.Env = this;
				CommonTokenStream tokenStream = new CommonTokenStream(lexer);
				GrGenParser parser = new GrGenParser(tokenStream);
				includes.Push(new SubunitInclude(parser));
				string oldFilename = filename;
				filename = inputFile.FullName;

				try
				{
					parser.Env = this;
					root = parser.TextTypes;
					hadError_ = hadError_ || parser.HadError;
				}
				catch(RecognitionException e)
				{
					Console.Error.WriteLine(e.StackTrace);
					Console.Error.WriteLine("parser exception: " + e.Message);
					Environment.Exit(1);
				}

				filename = oldFilename;

				includes.Pop();
			}
			catch(IOException e)
			{
				Console.Error.WriteLine("cannot load graph model: " + e.Message);
				Environment.Exit(1);
			}

			modelsOnStack.Remove(filePath);

			models[filePath] = root;

			return root;
		}

		public override bool HadError()
		{
			return hadError_;
		}
	}

}

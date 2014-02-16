/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.2
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.parser.antlr;

import java.io.File;
import java.io.IOException;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Stack;

import org.antlr.runtime.*;

import de.unika.ipd.grgen.Sys;
import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.parser.ParserEnvironment;

/**
 * Ease the ANTLR parser calling
 */
public class GRParserEnvironment extends ParserEnvironment {
	private boolean hadError = false;
	private Stack<SubunitInclude> includes = new Stack<SubunitInclude>();
	private HashSet<String> filesOnStack = new HashSet<String>();
	private HashSet<String> modelsOnStack = new HashSet<String>();
	private HashMap<String, ModelNode> models = new HashMap<String, ModelNode>();

	/** The base directory of the specification or null for the current directory */
	private File baseDir = null;

	private String filename;

	public GRParserEnvironment(Sys system) {
		super(system);
	}

    public void pushFile(Lexer lexer, File file) throws RecognitionException {
		if(baseDir != null && !file.isAbsolute())
			file = new File(baseDir, file.getPath());

		String filePath = file.getPath();
		if(filesOnStack.contains(filePath)) {
			System.err.println("GrGen: [ERROR at " + getFilename() + ":" + lexer.getLine()
					+ "," + lexer.getCharPositionInLine() + "] found circular include with file \""
					+ filePath + "\"");
			System.exit(1);
		}
		filesOnStack.add(filePath);

		try {
			// save current lexer's state
			CharStream input = lexer.getCharStream();
	        int marker = input.mark();
	        includes.push(new SubunitInclude(input, marker));

	        // switch on new input stream
	        ANTLRFileStream stream = new ANTLRFileStream(file.getPath());
	        lexer.setCharStream(stream);
	        lexer.reset();
	        filename = file.getPath();
    	}
    	catch (IOException e) {
			System.err.println("GrGen: [ERROR at " + getFilename() + ":" + lexer.getLine()
					+ "," + lexer.getCharPositionInLine() + "] included file could not be found: \""
					+ filePath + "\"");
			System.exit(1);
	  	}
	}

    public boolean popFile(Lexer lexer) {
    	// We've got EOF on an include (not a model using or the initial parser).
    	if(includes.size() > 1 && includes.peek().charStream != null){
			filesOnStack.remove(lexer.getSourceName());

			SubunitInclude include = includes.pop();
			lexer.setCharStream(include.charStream);
			lexer.getCharStream().rewind(include.marking);
			filename = lexer.getCharStream().getSourceName();
			return true;
    	}

    	return false;
	}

	@Override
	public String getFilename() {
		return filename;
	}

    public UnitNode parseActions(File inputFile) {
		UnitNode root = null;

		baseDir = inputFile.getParentFile();

		try {
			ANTLRFileStream stream = new ANTLRFileStream(inputFile.getPath());
			GrGenLexer lexer = new GrGenLexer(stream);
			lexer.setEnv(this);
			CommonTokenStream tokenStream = new CommonTokenStream(lexer);
			GrGenParser parser = new GrGenParser(tokenStream);
			includes.push(new SubunitInclude(parser));
			filename = inputFile.getPath();

			try {
				parser.setEnv(this);
				root = parser.textActions();
				hadError = hadError || parser.hadError();
			}
			catch(RecognitionException e) {
				e.printStackTrace(System.err);
				System.err.println("parser exception: " + e.getMessage());
				System.exit(1);
			}

			includes.pop();
		}
		catch(IOException e) {
			System.err.println("input file not found: " + e.getMessage());
			System.exit(1);
		}

		return root;
	}

    public ModelNode parseModel(File inputFile) {
		ModelNode root = null;

		String filePath = inputFile.getAbsolutePath();
		if(modelsOnStack.contains(filePath)) {
			System.err.println("GrGen: [ERROR at " + getFilename() + /*":" + curlexer.getLine()
					+ "," + curlexer.getCharPositionInLine() +*/ "] found circular model usage with file \""
					+ filePath + "\"");
			System.exit(1);
		}

		root = models.get(filePath);
		if(root != null) return root;

		modelsOnStack.add(filePath);

		try {
			ANTLRFileStream stream = new ANTLRFileStream(inputFile.getPath());
			GrGenLexer lexer = new GrGenLexer(stream);
			lexer.setEnv(this);
			CommonTokenStream tokenStream = new CommonTokenStream(lexer);
			GrGenParser parser = new GrGenParser(tokenStream);
			includes.push(new SubunitInclude(parser));
			String oldFilename = filename;
			filename = inputFile.getPath();

			try {
				parser.setEnv(this);
				root = parser.textTypes();
				hadError = hadError || parser.hadError();
			}
			catch(RecognitionException e) {
				e.printStackTrace(System.err);
				System.err.println("parser exception: " + e.getMessage());
				System.exit(1);
			}

			filename = oldFilename;

			includes.pop();
		}
		catch(IOException e) {
			System.err.println("cannot load graph model: " + e.getMessage());
			System.exit(1);
		}

		modelsOnStack.remove(filePath);

		models.put(filePath, root);

		return root;
	}

	public boolean hadError() {
		return hadError;
	}
}

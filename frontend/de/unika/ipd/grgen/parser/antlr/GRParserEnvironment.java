/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * GRParserActivator.java
 *
 * @author Sebastian Hack
 * @version $Id$
 */

package de.unika.ipd.grgen.parser.antlr;

import antlr.ANTLRException;
import antlr.ANTLRHashString;
import antlr.Parser;
import antlr.TokenStreamException;
import antlr.TokenStreamSelector;
import de.unika.ipd.grgen.Sys;
import de.unika.ipd.grgen.ast.ModelNode;
import de.unika.ipd.grgen.ast.UnitNode;
import de.unika.ipd.grgen.parser.ParserEnvironment;
import java.io.BufferedInputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Stack;

/**
 * Ease the ANTLR parser calling
 */
public class GRParserEnvironment extends ParserEnvironment {
	private boolean hadError = false;
	private Stack<Parser> parsers = new Stack<Parser>();
	private Stack<TokenStreamSelector> selectors = new Stack<TokenStreamSelector>();
	private HashSet<String> filesOnStack = new HashSet<String>();
	private HashSet<String> modelsOnStack = new HashSet<String>();
	private HashMap<String, ModelNode> models = new HashMap<String, ModelNode>();

	private ANTLRHashString hashString;

	/** The base directory of the specification or null for the current directory */
	private File baseDir = null;

	public GRParserEnvironment(Sys system) {
		super(system);
	}

    public void pushFile(File file) throws TokenStreamException {
		if(baseDir != null)
			file = new File(baseDir, file.getPath());

		String filePath = file.getPath();
		if(filesOnStack.contains(filePath)) {
			GRLexer curlexer = (GRLexer) selectors.peek().getCurrentStream();
			System.err.println("GrGen: [ERROR at " + getFilename() + ":" + curlexer.getLine()
					+ "," + curlexer.getColumn() + "] found circular include with file \""
					+ filePath + "\"");
			System.exit(1);
		}
		filesOnStack.add(filePath);

		try {
    		FileInputStream stream = new FileInputStream(file);
			GRLexer sublexer = new GRLexer(new BufferedInputStream(stream)) {
				public void uponEOF() throws TokenStreamException {
		            env.popFile();
			    }
			};

			sublexer.setTabSize(1);
			sublexer.setEnv(this);
			sublexer.setFilename(file.getPath());
			selectors.peek().push(sublexer);
 			selectors.peek().retry();
    	}
    	catch (FileNotFoundException e) {
			System.out.println("could not find file: " + file);
			System.exit(1);
	  	}
	}

    public void popFile() throws TokenStreamException {
    	GRLexer sublexer = (GRLexer) selectors.peek().pop();
		filesOnStack.remove(sublexer.getFilename());
    	selectors.peek().retry();
	}

	@Override
	public String getFilename() {
		String file = ((GRLexer)selectors.peek().getCurrentStream()).getFilename();
		return file;
	}

    public UnitNode parseActions(File inputFile) {
		UnitNode root = null;

		baseDir = inputFile.getParentFile();

		try {
			TokenStreamSelector selector = new TokenStreamSelector();
			GRLexer mainLexer = new GRLexer(new BufferedInputStream(new FileInputStream(inputFile)));
			mainLexer.setTabSize(1);
			mainLexer.setEnv(this);
			mainLexer.setFilename(inputFile.getPath());
			hashString = mainLexer.getHashString();
			literals = mainLexer.getLiterals();
			selector.select(mainLexer);
			GRActionsParser parser = new GRActionsParser(selector);

			selectors.push(selector);
			parsers.push(parser);

			try {
				parser.setEnv(this);
				root = parser.text();
				hadError = hadError || parser.hadError();
			}
			catch(ANTLRException e) {
				e.printStackTrace(System.err);
				System.err.println("parser exception: " + e.getMessage());
				System.exit(1);
			}

			selectors.pop();
			parsers.pop();
		}
		catch(FileNotFoundException e) {
			System.err.println("input file not found: " + e.getMessage());
			System.exit(1);
		}

		return root;
	}

    public ModelNode parseModel(File inputFile) {
		ModelNode root = null;

		String filePath = inputFile.getAbsolutePath();
		if(modelsOnStack.contains(filePath)) {
			GRLexer curlexer = (GRLexer) selectors.peek().getCurrentStream();
			System.err.println("GrGen: [ERROR at " + getFilename() + ":" + curlexer.getLine()
					+ "," + curlexer.getColumn() + "] found circular model usage with file \""
					+ filePath + "\"");
			System.exit(1);
		}

		root = models.get(filePath);
		if(root != null) return root;

		modelsOnStack.add(filePath);

		try {
			TokenStreamSelector selector = new TokenStreamSelector();
			GRLexer mainLexer = new GRLexer(new BufferedInputStream(new FileInputStream(inputFile)));
			mainLexer.setTabSize(1);
			mainLexer.setEnv(this);
			mainLexer.setFilename(inputFile.getPath());
			selector.select(mainLexer);
			GRTypeParser parser = new GRTypeParser(selector);

			selectors.push(selector);
			parsers.push(parser);

			try {
				parser.setEnv(this);
				root = parser.text();
				hadError = hadError || parser.hadError();
			}
			catch(ANTLRException e) {
				e.printStackTrace(System.err);
				System.err.println("parser exception: " + e.getMessage());
				System.exit(1);
			}

			selectors.pop();
			parsers.pop();
		}
		catch(FileNotFoundException e) {
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

	public boolean isKeyword(String str) {
		hashString.setString(str);
		return literals.containsKey(hashString);
	}
}

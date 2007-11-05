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
 * GRParserActivator.java
 *
 * @author Sebastian Hack
 * @version $Id$
 */

package de.unika.ipd.grgen.parser.antlr;

import antlr.ANTLRException;
import antlr.Parser;
import antlr.TokenStreamException;
import antlr.TokenStreamSelector;
import de.unika.ipd.grgen.Sys;
import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.parser.ParserEnvironment;
import java.io.BufferedInputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.util.HashMap;
import java.util.Stack;

/**
 * Ease the antlr parser calling
 */
public class GRParserEnvironment extends ParserEnvironment {
	private boolean hadError = false;
	private Stack<Parser> parsers = new Stack<Parser>();
	private Stack<TokenStreamSelector> selectors = new Stack<TokenStreamSelector>();
	private HashMap<String, Object> filesOnStack = new HashMap<String, Object>();
	
	/** The base directory of the specification or null for the current directory */
	private File baseDir = null;
	
	public GRParserEnvironment(Sys system) {
		super(system);
	}
	
    public void pushFile(File file) throws TokenStreamException {
		if(baseDir != null)
			file = new File(baseDir, file.getPath());
		
		String filePath = file.getPath();
		if(filesOnStack.containsKey(filePath)) {
			GRLexer curlexer = (GRLexer) selectors.peek().getCurrentStream();
			System.err.println("GrGen: [ERROR at " + getFilename() + ":" + curlexer.getLine()
					+ "," + curlexer.getColumn() + "] found circular include with file \""
					+ filePath + "\"");
			System.exit(1);
		}
		filesOnStack.put(filePath, null);

		try {
    		FileInputStream stream = new FileInputStream(file);
			GRLexer sublexer = new GRLexer(stream) {
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

    public BaseNode parseActions(File inputFile) {
		BaseNode root = null;
		
		baseDir = inputFile.getParentFile();
			
		try {
			TokenStreamSelector selector = new TokenStreamSelector();
			GRLexer mainLexer = new GRLexer(new BufferedInputStream(new FileInputStream(inputFile)));
			mainLexer.setTabSize(1);
			mainLexer.setEnv(this);
			mainLexer.setFilename(inputFile.getPath());
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
	
    public BaseNode parseModel(File inputFile) {
		BaseNode root = null;
			
		try {
			TokenStreamSelector selector = new TokenStreamSelector();
			GRLexer mainLexer = new GRLexer(new FileInputStream(inputFile));
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
		
		return root;
	}
	
	public boolean hadError() {
		return hadError;
	}

}



/**
 * GRParserActivator.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.parser.antlr;

import antlr.ANTLRException;
import antlr.TokenStreamSelector;
import de.unika.ipd.grgen.Sys;
import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.parser.ParserEnvironment;
import de.unika.ipd.grgen.util.report.ErrorReporter;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;

/**
 * Ease the antlr parser calling
 */
public class GRParserEnvironment extends ParserEnvironment {

	private boolean hadError = false;
	
	public GRParserEnvironment(Sys system) {
		super(system);
	}
	
	public BaseNode parse(File inputFile) {
		BaseNode root = null;
			
		try {
			GRLexer mainLexer = new GRLexer(new FileInputStream(inputFile));
			mainLexer.setEnv(this);
			GRActionsParser parser = new GRActionsParser(mainLexer);
			
			try {
				parser.setFilename(inputFile.getName());
				parser.setEnv(this);
				root = parser.text();
				hadError = parser.hadError();
			}
			catch(ANTLRException e) {
				e.printStackTrace(System.err);
				System.err.println("parser exception: " + e.getMessage());
				System.exit(1);
			}
		}
		catch(FileNotFoundException e) {
			System.err.println("input file not found: " + e.getMessage());
			System.exit(1);
		}
		
		return root;
	}
	
	public boolean hadError() {
		return hadError;
	}
	
}


package de.unika.ipd.grgen.parser;

import de.unika.ipd.grgen.util.report.Location;

public class Coords implements Location {

	protected static final Coords INVALID = new Coords();
	
	public static Coords getInvalid() {
		return INVALID;
	}

	/** 
	 * The default filename for the coordinates
	 * It should only be changed, if the lexer is switching to another file.
	 */
	private static String defaultFilename = null;
	
  protected int line, col;
  protected String filename;

	/**
	 * Set the default filename coordinates get, when they are constructed.
	 * @param filename The default filename for coordinates. If null,
	 * the coordinates are meant to have no filename (i.e. The filename 
	 * is not printed in the toString() method.
	 */
	public static void setDefaultFilename(String filename) {
		defaultFilename = filename;
	}

	/**
	 * Create empty coordinates.
	 * Coordinates made with this constructor will return false 
	 * on #hasLocation().
	 */
  public Coords() {
  	this(-1, -1, null);
  }
  
  /**
   * Fully construct new coordinates
   * @param line The line
   * @param col The column
   * @param filename The filename
   */
	public Coords(int line, int col, String filename) {
		this.line = line;
		this.col = col;
		this.filename = filename;
	}
  
  /**
   * Construct coordinates from an antlr token. The filename is set
   * to the default filename.
   * @param tok The antlr token.
   */
  public Coords(antlr.Token tok) {
    this(tok.getLine(), tok.getColumn());
  }
  
  /**
   * Get the coordinates from an antlr recognition exception.
   * @param e The antlr recognition exception.
   */
  public Coords(antlr.RecognitionException e) {
  	this(e.getLine(), e.getColumn(), e.getFilename());
  }
  
  /**
   * Make coordinates just from line and column. The filename is set 
   * to the default filename. 
   * @param line The line
   * @param col The column
   */
  public Coords(int line, int col) {
    this(line, col, null);
  }

	public Coords(antlr.Token tok, antlr.Parser parser) {
		this(tok);
		filename = parser.getFilename();
	}

  /**
   * Checks, wheather the coordinates are valid.
   * @return true, if the coordinates are set and valid, false otherwise
   */
	private boolean valid() {
		return line != -1 && col != -1;
	}

  public String toString() {
    if(valid())
    	return filename + ":" + line + "," + col;
      // return (filename != null ? filename + ":" : "") + line + "," + col;
    else
      return "nowhere";
  }
  
  /**
   * @see de.unika.ipd.grgen.util.report.Location#getLocation()
   */
  public String getLocation() {
  	return toString();
  }

  /**
   * @see de.unika.ipd.grgen.util.report.Location#hasLocation()
   */
  public boolean hasLocation() {
    return valid();
  }
  
  /**
   * Compare coordinates.
   * Coordainates are equal, if they have the same filename (or both none)
   * the same line and column. 
   * @see java.lang.Object#equals(java.lang.Object)
   */
  public boolean equals(Object obj) {
  	boolean res = false;
  	if(obj instanceof Coords) {
  		Coords c = (Coords) obj;
  		res = line == c.line && col == c.col && 
  		  ((filename == null && c.filename == null) 
  		   || (filename.equals(c.filename)));
  	}
  	return res;
  }

	/**
	 * Get the line of the coordinates.
	 * @return The line.
	 */
	public int getLine() {
		return line;
	}
	
	/**
	 * Get the column of the coordinates.
	 * @return The column.
	 */
	public int getColumn() {
		return col;
	}
	
	/**
	 * Get the filename of the coordinates.
	 * @return The filename.
	 */
	public String getFileName() {
		return filename;
	}

}

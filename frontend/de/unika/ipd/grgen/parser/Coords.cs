/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Sebastian Hack
/// </summary>

namespace de.unika.ipd.grgen.parser
{
	using Location = de.unika.ipd.grgen.util.report.Location;

	public class Coords : Location
	{
		protected internal static readonly Coords INVALID = new Coords();

		protected internal static readonly Coords BUILTIN = new Coords(0, 0, "<builtin>");

		public static Coords Invalid
		{
			get
			{
				return INVALID;
			}
		}

		public static Coords Builtin
		{
			get
			{
				return BUILTIN;
			}
		}

		protected internal int line;
		protected internal int column;
		protected internal string filename; // non-null if line!=-1 && column!=-1

		/// <summary>
		/// Create empty coordinates.
		/// Coordinates made with this constructor will return false
		/// on #hasLocation().
		/// </summary>
		public Coords()
			: this(-1, -1, null)
		{
		}

		/// <summary>
		/// Fully construct new coordinates </summary>
		/// <param name="line"> The line </param>
		/// <param name="column"> The column </param>
		/// <param name="filename"> The filename </param>
		public Coords(int line, int column, string filename)
		{
			this.line = line;
			this.column = column;
			this.filename = filename;
		}

		/// <summary>
		/// Make coordinates just from line and column. The filename is set
		/// to the default filename. </summary>
		/// <param name="line"> The line </param>
		/// <param name="column"> The column </param>
		public Coords(int line, int column)
			: this(line, column, null)
		{
		}

		/// <summary>
		/// Checks, wheather the coordinates are valid. </summary>
		/// <returns> true, if the coordinates are set and valid, false otherwise </returns>
		private bool Valid()
		{
			return line != -1 && column != -1;
		}

		public override string ToString()
		{
			if(Valid())
				return filename + ":" + line + "," + column;
			else
				return "nowhere";
		}

		public string AtCoords
		{
			get
			{
				return " [at " + ToString() + "]";
			}
		}

		public string GetDeclarationCoords(bool implicitly)
		{
			if(!HasLocation())
				return "";
			if(this == Coords.Builtin)
				return "";
			return " [declared " + (implicitly ? "implicitly " : "") + "at " + ToString() + "]";
		}

		/// <seealso cref="de.unika.ipd.grgen.util.report.Location.getLocation()"/>
		public virtual string Location
		{
			get
			{
				return ToString();
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.util.report.Location.hasLocation()"/>
		public virtual bool HasLocation()
		{
			return Valid();
		}

		/// <summary>
		/// Compare coordinates.
		/// Coordainates are equal, if they have the same filename (or both none)
		/// the same line and column. </summary>
		/// <seealso cref="java.lang.Object.equals(java.lang.Object)"/>
		public override bool Equals(object obj)
		{
			bool res = false;
			if(obj is Coords)
			{
				Coords c = (Coords)obj;
				res = line == c.line && column == c.column &&
						((string.ReferenceEquals(filename, null) && string.ReferenceEquals(c.filename, null))
								|| (!string.ReferenceEquals(filename, null) && filename.Equals(c.filename)));
			}
			return res;
		}

		public override int GetHashCode()
		{
			return ((!string.ReferenceEquals(filename, null) ? filename.GetHashCode() : 13) * 31 + line) * 31 + column;
		}

		/// <summary>
		/// Get the line of the coordinates. </summary>
		/// <returns> The line. </returns>
		public virtual int Line
		{
			get
			{
				return line;
			}
		}

		/// <summary>
		/// Get the column of the coordinates. </summary>
		/// <returns> The column. </returns>
		public virtual int Column
		{
			get
			{
				return column;
			}
		}

		/// <summary>
		/// Get the filename of the coordinates. </summary>
		/// <returns> The filename. </returns>
		public virtual string FileName
		{
			get
			{
				return filename;
			}
		}

		public virtual bool ComesBefore(Coords that)
		{
			if(!this.Valid())
				return false;
			if(!that.Valid())
				return false;
			if(this.Line < that.Line)
				return true;
			if(this.Line == that.Line)
			{
				if(this.Column < that.Column)
					return true;
			}
			return false;
		}
	}

}

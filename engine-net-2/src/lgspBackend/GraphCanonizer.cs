using System;

// by Maneesh Yadav

namespace de.unika.ipd.grGen.lgsp
{
	//Graph canonizer interface
	public interface GraphCanonizer
	{
		/// <summary>
		/// Canonize a graph
		/// </summary>
		/// <param name="graph">The graph to canonize.</param>
		String Canonize(LGSPGraph graph);
	}
}


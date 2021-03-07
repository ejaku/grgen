using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;
using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;

namespace de.unika.ipd.grGen.Model_ExternalAttributeEvaluationCopyObject1
{
    public partial class Own
    {
        public bool muh()
        {
            return false;
        }
    }
	
    public partial class AttributeTypeObjectCopierComparer
    {
        public static object Copy(object that, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewMap)
        {
            return that; // copy reference, default implementation
        }
        
		public static Own Copy(Own that, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewMap)
        {
            return that; // copy reference, default implementation
        }
    }
}

namespace de.unika.ipd.grGen.expression
{
    using GRGEN_MODEL = de.unika.ipd.grGen.Model_ExternalAttributeEvaluationCopyObject1;

	public partial class ExternalFunctions
	{
        public static GRGEN_MODEL.Own own(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph)
        {
            return new GRGEN_MODEL.Own();
        }
    }
}

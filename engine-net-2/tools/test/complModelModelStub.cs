// This file has been generated automatically by GrGen (www.grgen.net)
// Do not modify this file! Any changes will be lost!
// Rename this file or use a copy!
// Generated from "test.grg" on Sat Apr 19 15:09:25 CEST 2025

using System;
using System.Collections.Generic;
using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;
using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;
using GRGEN_MODEL = de.unika.ipd.grGen.Model_complModel;

namespace test
{
	public class D231_4121_Impl : GRGEN_MODEL.@D231_4121
	{
		public D231_4121_Impl() : base() { }

		public override GRGEN_LIBGR.INode Clone() {
			return new D231_4121_Impl(this, null, null);
		}

		public override GRGEN_LIBGR.INode Copy(GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) {
			return new D231_4121_Impl(this, graph, oldToNewObjectMap);
		}

		private D231_4121_Impl(D231_4121_Impl oldElem, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) : base()
		{
			a2_M0no_suXx_h4rD = oldElem.a2_M0no_suXx_h4rD;
			b23_M0no_suXx_h4rD = oldElem.b23_M0no_suXx_h4rD;
			a4_M0no_suXx_h4rD = oldElem.a4_M0no_suXx_h4rD;
			b41_M0no_suXx_h4rD = oldElem.b41_M0no_suXx_h4rD;
			b42_M0no_suXx_h4rD = oldElem.b42_M0no_suXx_h4rD;
			a5_M0no_suXx_h4rD = oldElem.a5_M0no_suXx_h4rD;
			d231_4121_M0no_suXx_h4rD = oldElem.d231_4121_M0no_suXx_h4rD;
		}
		
		private GRGEN_LIBGR.IBaseObject Copy(GRGEN_LIBGR.IBaseObject oldObj, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap)
		{
			if(oldObj == null)
				return null;
			if(oldToNewObjectMap.ContainsKey(oldObj))
				return (GRGEN_LIBGR.IBaseObject)oldToNewObjectMap[oldObj];
			else {
				if(oldObj is GRGEN_LIBGR.IObject) {
					GRGEN_LIBGR.IObject newObj = ((GRGEN_LIBGR.IObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				} else {
					GRGEN_LIBGR.ITransientObject newObj = ((GRGEN_LIBGR.ITransientObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				}
			}
		}

		public override bool IsDeeplyEqual(GRGEN_LIBGR.IDeepEqualityComparer that, IDictionary<object, object> visitedObjects) {
			if(visitedObjects.ContainsKey(this) || visitedObjects.ContainsKey(that))
				throw new Exception("Multiple appearances (and cycles) forbidden in deep equality comparison (only tree-like structures are supported)!");
			if(this == that)
				return true;
			if(!(that is D231_4121_Impl))
				return false;
			D231_4121_Impl that_ = (D231_4121_Impl)that;
			visitedObjects.Add(this, null);
			if(that != this)
				visitedObjects.Add(that, null);
			bool result = true
				&& a2_M0no_suXx_h4rD == that_.a2_M0no_suXx_h4rD
				&& b23_M0no_suXx_h4rD == that_.b23_M0no_suXx_h4rD
				&& a4_M0no_suXx_h4rD == that_.a4_M0no_suXx_h4rD
				&& b41_M0no_suXx_h4rD == that_.b41_M0no_suXx_h4rD
				&& b42_M0no_suXx_h4rD == that_.b42_M0no_suXx_h4rD
				&& a5_M0no_suXx_h4rD == that_.a5_M0no_suXx_h4rD
				&& d231_4121_M0no_suXx_h4rD == that_.d231_4121_M0no_suXx_h4rD
				;
			visitedObjects.Remove(this);
			visitedObjects.Remove(that);
			return result;
		}


		private int a2_M0no_suXx_h4rD;
		public override int @a2
		{
			get { return a2_M0no_suXx_h4rD; }
			set { a2_M0no_suXx_h4rD = value; }
		}

		private int b23_M0no_suXx_h4rD;
		public override int @b23
		{
			get { return b23_M0no_suXx_h4rD; }
			set { b23_M0no_suXx_h4rD = value; }
		}

		private int a4_M0no_suXx_h4rD;
		public override int @a4
		{
			get { return a4_M0no_suXx_h4rD; }
			set { a4_M0no_suXx_h4rD = value; }
		}

		private int b41_M0no_suXx_h4rD;
		public override int @b41
		{
			get { return b41_M0no_suXx_h4rD; }
			set { b41_M0no_suXx_h4rD = value; }
		}

		private int b42_M0no_suXx_h4rD;
		public override int @b42
		{
			get { return b42_M0no_suXx_h4rD; }
			set { b42_M0no_suXx_h4rD = value; }
		}

		private int a5_M0no_suXx_h4rD;
		public override int @a5
		{
			get { return a5_M0no_suXx_h4rD; }
			set { a5_M0no_suXx_h4rD = value; }
		}

		private int d231_4121_M0no_suXx_h4rD;
		public override int @d231_4121
		{
			get { return d231_4121_M0no_suXx_h4rD; }
			set { d231_4121_M0no_suXx_h4rD = value; }
		}
	}
}

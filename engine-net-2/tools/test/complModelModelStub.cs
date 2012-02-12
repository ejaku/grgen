// This file has been generated automatically by GrGen (www.grgen.net)
// Do not modify this file! Any changes will be lost!
// Rename this file or use a copy!
// Generated from "test.grg" on Sun Feb 05 16:26:05 CET 2012

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

		public override GRGEN_LIBGR.INode Clone() { return new D231_4121_Impl(this); }

		private D231_4121_Impl(D231_4121_Impl oldElem) : base()
		{
			a2_M0no_suXx_h4rD = oldElem.a2_M0no_suXx_h4rD;
			b23_M0no_suXx_h4rD = oldElem.b23_M0no_suXx_h4rD;
			a4_M0no_suXx_h4rD = oldElem.a4_M0no_suXx_h4rD;
			b41_M0no_suXx_h4rD = oldElem.b41_M0no_suXx_h4rD;
			b42_M0no_suXx_h4rD = oldElem.b42_M0no_suXx_h4rD;
			a5_M0no_suXx_h4rD = oldElem.a5_M0no_suXx_h4rD;
			d231_4121_M0no_suXx_h4rD = oldElem.d231_4121_M0no_suXx_h4rD;
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is D231_4121_Impl)) return false;
			D231_4121_Impl that_ = (D231_4121_Impl)that;
			return true
				&& a2_M0no_suXx_h4rD == that_.a2_M0no_suXx_h4rD
				&& b23_M0no_suXx_h4rD == that_.b23_M0no_suXx_h4rD
				&& a4_M0no_suXx_h4rD == that_.a4_M0no_suXx_h4rD
				&& b41_M0no_suXx_h4rD == that_.b41_M0no_suXx_h4rD
				&& b42_M0no_suXx_h4rD == that_.b42_M0no_suXx_h4rD
				&& a5_M0no_suXx_h4rD == that_.a5_M0no_suXx_h4rD
				&& d231_4121_M0no_suXx_h4rD == that_.d231_4121_M0no_suXx_h4rD
			;
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

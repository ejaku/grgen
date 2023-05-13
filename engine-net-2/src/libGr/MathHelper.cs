/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;

namespace de.unika.ipd.grGen.libGr
{
    public static class MathHelper
    {
        public static double Sqr(double argument)
        {
            return argument * argument;
        }

        public static object Min(object left, object right)
        {
            if(left.GetType() != right.GetType())
                throw new Exception("left type and right type must be the same");
            if(left is SByte)
                return Math.Min((SByte)left, (SByte)right);
            else if(left is short)
                return Math.Min((short)left, (short)right);
            else if(left is int)
                return Math.Min((int)left, (int)right);
            else if(left is long)
                return Math.Min((long)left, (long)right);
            else if(left is float)
                return Math.Min((float)left, (float)right);
            else if(left is double)
                return Math.Min((double)left, (double)right);
            else
                throw new Exception("left/right type must be numeric");
        }

        public static object Max(object left, object right)
        {
            if(left.GetType() != right.GetType())
                throw new Exception("left type and right type must be the same");
            if(left is SByte)
                return Math.Max((SByte)left, (SByte)right);
            else if(left is short)
                return Math.Max((short)left, (short)right);
            else if(left is int)
                return Math.Max((int)left, (int)right);
            else if(left is long)
                return Math.Max((long)left, (long)right);
            else if(left is float)
                return Math.Max((float)left, (float)right);
            else if(left is double)
                return Math.Max((double)left, (double)right);
            else
                throw new Exception("left/right type must be numeric");
        }

        public static object Abs(object argument)
        {
            if(argument is SByte)
                return Math.Abs((SByte)argument);
            else if(argument is short)
                return Math.Abs((short)argument);
            else if(argument is int)
                return Math.Abs((int)argument);
            else if(argument is long)
                return Math.Abs((long)argument);
            else if(argument is float)
                return Math.Abs((float)argument);
            else if(argument is double)
                return Math.Abs((double)argument);
            else
                throw new Exception("argument type must be numeric");
        }
    }
}

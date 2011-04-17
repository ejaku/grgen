/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

namespace de.unika.ipd.grGen.lgsp
{
    public class Pair<S, T>
    {
        public Pair()
        {
        }

        public Pair(S fst, T snd)
        {
            this.fst = fst;
            this.snd = snd;
        }

        public S fst;
        public T snd;
    }
}

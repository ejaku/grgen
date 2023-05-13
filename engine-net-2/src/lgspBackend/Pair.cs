/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

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

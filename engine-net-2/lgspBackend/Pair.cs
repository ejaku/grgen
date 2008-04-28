/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
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

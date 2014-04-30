#!/bin/bash

# execute the benchmarker on the IMDB graphs
MovieDatabaseBenchmarker.exe findCouplesOpt imdb-0005000-50176.movies.xmi.grs "[computeAverageRanking] ;> [couplesWithRating\orderDescendingBy<avgRating>\keepFirst(15)] ;> [couplesWithRating\orderDescendingBy<numMovies>\keepFirst(15)]"
MovieDatabaseBenchmarker.exe findCouplesOpt imdb-0010000-98388.movies.xmi.grs "[computeAverageRanking] ;> [couplesWithRating\orderDescendingBy<avgRating>\keepFirst(15)] ;> [couplesWithRating\orderDescendingBy<numMovies>\keepFirst(15)]"
MovieDatabaseBenchmarker.exe findCouplesOpt imdb-0030000-207876.movies.xmi.grs "[computeAverageRanking] ;> [couplesWithRating\orderDescendingBy<avgRating>\keepFirst(15)] ;> [couplesWithRating\orderDescendingBy<numMovies>\keepFirst(15)]"
MovieDatabaseBenchmarker.exe findCouplesOpt imdb-0045000-299195.movies.xmi.grs "[computeAverageRanking] ;> [couplesWithRating\orderDescendingBy<avgRating>\keepFirst(15)] ;> [couplesWithRating\orderDescendingBy<numMovies>\keepFirst(15)]"
MovieDatabaseBenchmarker.exe findCouplesOpt imdb-0065000-405592.movies.xmi.grs "[computeAverageRanking] ;> [couplesWithRating\orderDescendingBy<avgRating>\keepFirst(15)] ;> [couplesWithRating\orderDescendingBy<numMovies>\keepFirst(15)]"
MovieDatabaseBenchmarker.exe findCouplesOpt imdb-0085000-502878.movies.xmi.grs "[computeAverageRanking] ;> [couplesWithRating\orderDescendingBy<avgRating>\keepFirst(15)] ;> [couplesWithRating\orderDescendingBy<numMovies>\keepFirst(15)]"
MovieDatabaseBenchmarker.exe findCouplesOpt imdb-0130000-712130.movies.xmi.grs "[computeAverageRanking] ;> [couplesWithRating\orderDescendingBy<avgRating>\keepFirst(15)] ;> [couplesWithRating\orderDescendingBy<numMovies>\keepFirst(15)]"
MovieDatabaseBenchmarker.exe findCouplesOpt imdb-0200000-1007510.movies.xmi.grs "[computeAverageRanking] ;> [couplesWithRating\orderDescendingBy<avgRating>\keepFirst(15)] ;> [couplesWithRating\orderDescendingBy<numMovies>\keepFirst(15)]"
MovieDatabaseBenchmarker.exe findCouplesOpt imdb-0340000-1511739.movies.xmi.grs "[computeAverageRanking] ;> [couplesWithRating\orderDescendingBy<avgRating>\keepFirst(15)] ;> [couplesWithRating\orderDescendingBy<numMovies>\keepFirst(15)]"
MovieDatabaseBenchmarker.exe findCouplesOpt imdb-0495000-2011048.movies.xmi.grs "[computeAverageRanking] ;> [couplesWithRating\orderDescendingBy<avgRating>\keepFirst(15)] ;> [couplesWithRating\orderDescendingBy<numMovies>\keepFirst(15)]"
MovieDatabaseBenchmarker.exe findCouplesOpt imdb-0660000-2514839.movies.xmi.grs "[computeAverageRanking] ;> [couplesWithRating\orderDescendingBy<avgRating>\keepFirst(15)] ;> [couplesWithRating\orderDescendingBy<numMovies>\keepFirst(15)]"
MovieDatabaseBenchmarker.exe findCouplesOpt imdb-0900000-3210567.movies.xmi.grs "[computeAverageRanking] ;> [couplesWithRating\orderDescendingBy<avgRating>\keepFirst(15)] ;> [couplesWithRating\orderDescendingBy<numMovies>\keepFirst(15)]"
MovieDatabaseBenchmarker.exe findCouplesOpt imdb-all-3335074.movies.xmi.grs "[computeAverageRanking] ;> [couplesWithRating\orderDescendingBy<avgRating>\keepFirst(15)] ;> [couplesWithRating\orderDescendingBy<numMovies>\keepFirst(15)]"

# execute the benchmarker on the synthetic graphs
MovieDatabaseBenchmarker.exe findCouplesOpt 1000 "[computeAverageRanking] ;> [couplesWithRating\orderDescendingBy<avgRating>\keepFirst(15)] ;> [couplesWithRating\orderDescendingBy<numMovies>\keepFirst(15)]"
MovieDatabaseBenchmarker.exe findCouplesOpt 2000 "[computeAverageRanking] ;> [couplesWithRating\orderDescendingBy<avgRating>\keepFirst(15)] ;> [couplesWithRating\orderDescendingBy<numMovies>\keepFirst(15)]"
MovieDatabaseBenchmarker.exe findCouplesOpt 3000 "[computeAverageRanking] ;> [couplesWithRating\orderDescendingBy<avgRating>\keepFirst(15)] ;> [couplesWithRating\orderDescendingBy<numMovies>\keepFirst(15)]"
MovieDatabaseBenchmarker.exe findCouplesOpt 4000 "[computeAverageRanking] ;> [couplesWithRating\orderDescendingBy<avgRating>\keepFirst(15)] ;> [couplesWithRating\orderDescendingBy<numMovies>\keepFirst(15)]"
MovieDatabaseBenchmarker.exe findCouplesOpt 5000 "[computeAverageRanking] ;> [couplesWithRating\orderDescendingBy<avgRating>\keepFirst(15)] ;> [couplesWithRating\orderDescendingBy<numMovies>\keepFirst(15)]"
MovieDatabaseBenchmarker.exe findCouplesOpt 10000 "[computeAverageRanking] ;> [couplesWithRating\orderDescendingBy<avgRating>\keepFirst(15)] ;> [couplesWithRating\orderDescendingBy<numMovies>\keepFirst(15)]"
MovieDatabaseBenchmarker.exe findCouplesOpt 50000 "[computeAverageRanking] ;> [couplesWithRating\orderDescendingBy<avgRating>\keepFirst(15)] ;> [couplesWithRating\orderDescendingBy<numMovies>\keepFirst(15)]"
MovieDatabaseBenchmarker.exe findCouplesOpt 100000 "[computeAverageRanking] ;> [couplesWithRating\orderDescendingBy<avgRating>\keepFirst(15)] ;> [couplesWithRating\orderDescendingBy<numMovies>\keepFirst(15)]"
MovieDatabaseBenchmarker.exe findCouplesOpt 200000 "[computeAverageRanking] ;> [couplesWithRating\orderDescendingBy<avgRating>\keepFirst(15)] ;> [couplesWithRating\orderDescendingBy<numMovies>\keepFirst(15)]"

# execute the benchmarker for cliques of 3
MovieDatabaseBenchmarker.exe findCliquesOf3Opt imdb-0005000-50176.movies.xmi.grs "[computeAverageRankingCliques] ;> [cliques3WithRating\orderDescendingBy<avgRating>\keepFirst(15)] ;> [cliques3WithRating\orderDescendingBy<numMovies>\keepFirst(15)]"
MovieDatabaseBenchmarker.exe findCliquesOf3Opt 1000 "[computeAverageRankingCliques] ;> [cliques3WithRating\orderDescendingBy<avgRating>\keepFirst(15)] ;> [cliques3WithRating\orderDescendingBy<numMovies>\keepFirst(15)]"

# execute the benchmarker for cliques of 4
MovieDatabaseBenchmarker.exe findCliquesOf4Opt imdb-0005000-50176.movies.xmi.grs "[computeAverageRankingCliques] ;> [cliques4WithRating\orderDescendingBy<avgRating>\keepFirst(15)] ;> [cliques4WithRating\orderDescendingBy<numMovies>\keepFirst(15)]"
MovieDatabaseBenchmarker.exe findCliquesOf4Opt 1000 "[computeAverageRankingCliques] ;> [cliques4WithRating\orderDescendingBy<avgRating>\keepFirst(15)] ;> [cliques4WithRating\orderDescendingBy<numMovies>\keepFirst(15)]"

# execute the benchmarker for cliques of 5
MovieDatabaseBenchmarker.exe findCliquesOf5Opt imdb-0005000-50176.movies.xmi.grs "[computeAverageRankingCliques] ;> [cliques5WithRating\orderDescendingBy<avgRating>\keepFirst(15)] ;> [cliques5WithRating\orderDescendingBy<numMovies>\keepFirst(15)]"
MovieDatabaseBenchmarker.exe findCliquesOf5Opt 1000 "[computeAverageRankingCliques] ;> [cliques5WithRating\orderDescendingBy<avgRating>\keepFirst(15)] ;> [cliques5WithRating\orderDescendingBy<numMovies>\keepFirst(15)]"

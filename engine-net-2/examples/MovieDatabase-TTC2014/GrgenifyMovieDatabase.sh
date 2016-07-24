#!/bin/bash

#version piping in input from here document
#for file in *.xmi; do	
#    GrShell -N << HERE
#import movies.ecore $file GrgenifyMovieDatabase.grg
#redirect emit $file.grs
#exec create_MovieDatabaseModel ;> [create_Movie] ;> [create_Actor] ;> [create_Actress] ;> [create_personToMovie]
#redirect emit - 
#validate exitonfailure exec noEdgesLeft
#quit
#HERE
#done

#version with -C command line option and non-newline-terminated grshell-commands
for file in *.xmi; do	
    GrShell -N -C "import movies.ecore $file GrgenifyMovieDatabase.grg ;; redirect emit $file.grs ;; exec create_MovieDatabaseModel ;> [create_Movie] ;> [create_Actor] ;> [create_Actress] ;> [create_personToMovie] #§ ;; redirect emit - ;; validate exitonfailure exec noEdgesLeft #§ ;; quit"
done

#version with -C command line option and verbatim grshell input / newline-terminated grshell-commands
#for file in *.xmi; do	
#    GrShell -N -C "import movies.ecore $file GrgenifyMovieDatabase.grg
#redirect emit $file.grs
#exec create_MovieDatabaseModel ;> [create_Movie] ;> [create_Actor] ;> [create_Actress] ;> [create_personToMovie]
#redirect emit -
#validate exitonfailure exec noEdgesLeft
#quit"
#done

#!/bin/bash

for file in *.xmi; do	
    GrShell -N << HERE
import movies.ecore $file GrgenifyMovieDatabase.grg
redirect emit $file.grs
exec create_MovieDatabaseModel ;> [create_Movie] ;> [create_Actor] ;> [create_Actress] ;> [create_personToMovie]
redirect emit - 
validate exitonfailure exec noEdgesLeft
quit
HERE
done

#above a here document is used to pipe some file content via stdin to GrShell; the script below would be a bit nicer, but I don't get it running
#using cygwin it looks as if the input string is split at the blanks --> the -C "shell command lines" option is primarily of interest for windows cmd
#for file in *.xmi; do	
#    GrShell -N -C "import movies.ecore $file GrgenifyMovieDatabase.grg ;; redirect emit $file.grs ;; exec create_MovieDatabaseModel ;> [create_Movie] ;> [create_Actor] ;> [create_Actress] ;> [create_personToMovie] #§ ;; redirect emit - ;; validate exitonfailure exec noEdgesLeft #§ ;; quit"
#done

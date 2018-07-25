#!/bin/bash

rm *.aux *.bbl *.blg *.idx *.ilg *.ind *.toc *~ *.backup *.cut *.log *.out *.tpt *.nav *.snm *.rai *.rao 2>/dev/null

DRAFT=0

for F in $@ ; do

  if [ $F = --draft ] ; then
    DRAFT=1
  else
    if [ $DRAFT = 0 ] ; then
	  echo Processing $F...
      echo pdfLaTeX [1]...
      pdflatex --interaction=nonstopmode "$F".tex > /dev/null

	  echo Building rail diagrams...
      ./rail.exe -h "$F" > /dev/null

	  echo Building bibliography...
	  bibtex "$F" > /dev/null

	  echo pdfLaTeX [2]...
      pdflatex --interaction=nonstopmode "$F".tex > /dev/null

	  makeindex "$F" > /dev/null
      sed /\\.\\.\\.AAA/c\ '\\\\'paragraph{Keywords} "$F".ind | sed /\\.\\.AAA/c\ '\\\\'paragraph{Non-Terminals} | sed /\\.\\.ZZZ/c\ '\\\\'paragraph{General'\~'Index} > "$F".ind.pimped
      mv "$F".ind.pimped "$F".ind
      echo pdfLaTeX [3]...
	  pdflatex --interaction=nonstopmode "$F".tex > /dev/null

	  echo Building bibliography...
	  bibtex "$F" > /dev/null
      grep Warning "$F".blg || true

      echo pdfLaTex [4]...
      pdflatex --interaction=nonstopmode "$F".tex > /dev/null
      echo pdfLaTex [5]...
      pdflatex --interaction=nonstopmode "$F".tex > /dev/null

      echo Building thumbnails...
      thumbpdf  --gscmd gswin64 "$F".pdf > /dev/null
      echo pdfLaTeX [6]...
      pdflatex --interaction=nonstopmode "$F".tex > /dev/null

#	  echo Optimizing...
#	  mv "$F".pdf "$F".pdf.unopt
#      pdfopt "$F".pdf.unopt "$F".pdf
#      rm "$F".pdf.unopt
      AcroRd32.exe "$F".pdf&
	else
	  echo Processing $F...
      echo pdfLaTeX [1]...
      cat "$F".tex | sed 1s/final/draft/ | pdflatex 1> /dev/null
      egrep '^Underfull|^Overfull' texput.log || true

	  echo Building rail diagrams...
      ./rail.exe -h texput > /dev/null

	  echo Building bibliography...
	  bibtex texput > /dev/null
      grep Warning texput.blg || true

	  echo pdfLaTeX [2]...
      cat "$F".tex | sed 1s/final/draft/ | pdflatex 1> /dev/null

	  makeindex texput > /dev/null
      sed /\\.\\.\\.AAA/c\ '\\\\'paragraph{Keywords} texput.ind | sed /\\.\\.AAA/c\ '\\\\'paragraph{Non-Terminals} | sed /\\.\\.ZZZ/c\ '\\\\'paragraph{General'\~'Index} > texput.ind.pimped
      mv texput.ind.pimped texput.ind
      echo pdfLaTeX [3]...
      cat "$F".tex | sed 1s/final/draft/ | pdflatex 1> /dev/null
      echo Building bibliography
      bibtex texput > /dev/null

      echo pdfLaTeX [4]...
	  cat "$F".tex | sed 1s/final/draft/ | pdflatex 1> /dev/null
      echo pdfLaTeX [5]...
	  cat "$F".tex | sed 1s/final/draft/ | pdflatex 1> /dev/null

      AcroRd32.exe texput.pdf &
	fi
  fi
done

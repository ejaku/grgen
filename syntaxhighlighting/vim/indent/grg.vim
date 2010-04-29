" Vim indent file for the D programming language (version 0.137).
"
" Language:	GrGen Rule
" Maintainer:	Sebastian Buchwald <sebastian.buchwald@kit.edu>
" Last Change:	2010 April 25
" Version:	0.1
"

" Only load this indent file when no other was loaded.
if exists("b:did_indent")
  finish
endif

let b:did_indent = 1

" Disable C indenting.
setlocal nocindent
setlocal autoindent

" vim: ts=8 noet

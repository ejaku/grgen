#!/bin/sh
awk '{ print "\t\t\tkeywords.add(\"" substr($3, 2, length($3) - 3) "\");"; }' < keywords.txt > keywords.out

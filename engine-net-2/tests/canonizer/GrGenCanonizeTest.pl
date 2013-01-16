#!/usr/bin/perl
package GrGenCanonizeTest;

use strict;
use warnings;
use List::Util qw(shuffle);

my $maxNodes=6;
my @nodeTypes = qw/NodeClassA NodeClassB NodeClassC NodeClassD/;
my @edgeTypes = qw/EdgeClassA EdgeClassB EdgeClassC EdgeClassD EdgeClassU/;
my @edgeDirMarksMap=(['-','->'],['<-','-'],['-','-']);
my @nodeNames = qw/a b c d e f g h i j k l m n o p q r s t u v w x y z/;
my $nMaxMultiEdges=3;


sub genSingletNode {
   my $nodeTuples=[[0,0]];
   my $edgeTuples=[];
   return [(caller(0))[3], $nodeTuples, $edgeTuples];

}

sub genSelfLoop {
   my $nodeTuples=[[0,0]];
   my $edgeTuples=[[0,0,0,0]];
   return [(caller(0))[3], $nodeTuples, $edgeTuples];
}

sub gen5SelfLoop {
    my $nodeTuples=[[0,0]];
    my $edgeTuples=[[0,0,0,0],[0,0,0,0],[0,0,0,0],[0,0,0,0],[0,0,0,0]];
    return [(caller(0))[3], $nodeTuples, $edgeTuples];
}

sub genDoubleSingletNode {
    my $nodeTuples=[[0,0],[1,1]];
    my $edgeTuples=[];
    return [(caller(0))[3], $nodeTuples, $edgeTuples];
}

sub genTwoConnected {
    my $nodeTuples=[[0,0],[1,1]];
    my $edgeTuples=[[0,1,0,1]];
    return [(caller(0))[3], $nodeTuples, $edgeTuples];
}

sub genThreeConnected {
    my $nodeTuples=[[0,0],[1,0],[2,1]];
    my $edgeTuples=[[0,0,0,1],[1,1,1,2]];
    return [(caller(0))[3], $nodeTuples, $edgeTuples];
}

sub genNPropyl {
    my $nodeTuples =[[0,0],[1,1],[2,2],[3,3]];
    my $edgeTuples =[[0,0,0,1],[1,1,0,2],[2,1,0,3]];
    return [(caller(0))[3], $nodeTuples, $edgeTuples];
}

sub genIsoPropyl {
    my $nodeTuples =[[0,0],[1,1],[2,2],[3,3]];
    my $edgeTuples =[[0,0,0,1],[1,1,0,2],[1,0,0,3]];
    return [(caller(0))[3], $nodeTuples, $edgeTuples];
}


sub genSquare {
    my $nodeTuples=[[0,0],[1,0],[2,0],[3,0]];
    my $edgeTuples=[[0,0,0,1],[1,0,0,2],[2,0,0,3],[3,0,0,0]];
    return [(caller(0))[3], $nodeTuples, $edgeTuples];
}


sub genCowbell {
    my $nodeTuples=[[0,0],[1,0],[2,0],[3,0]];
    my $edgeTuples=[[0,0,0,1],[1,0,0,2],[2,0,0,3],[3,0,0,1]];
    return [(caller(0))[3], $nodeTuples, $edgeTuples];
}

sub genCube {
    my $nodeTuples=[[0,0],[1,0],[2,0],[3,0],[4,0],[5,0],[6,0],[7,0]];
    my $edgeTuples =[[0,2,4,1],
		     [0,2,4,3],
		     [0,2,4,4],
		     [1,2,4,2],
		     [1,2,4,5],
		     [2,2,4,3],
		     [2,2,4,6],
		     [3,2,4,7],
		     [4,2,4,5],
		     [4,2,4,7],
		     [5,2,4,6],
		     [6,2,4,7]];
    return [(caller(0))[3], $nodeTuples, $edgeTuples];
}

sub randomGraph {
    my $gname = shift;
    my $nNodes = shift;
    my $edgeProb = shift;

    my $nodeTuples=[];
    my $edgeTuples=[];

    for(my $i=0; $i<$nNodes; $i++) {
	my $nodeClass=int(rand(scalar @nodeTypes));
	push @{$nodeTuples}, [$i,$nodeClass];
    }
    
    for(my $i=0; $i<$nNodes; $i++) {
	for(my $j=0; $j<=$i; $j++) {
	    if(rand()<$edgeProb) {
		my $nMultiEdges=int(rand($nMaxMultiEdges))+1;
		for(my $k=0; $k<$nMultiEdges; $k++) {
		    my $edgeDirIndex = int(rand(scalar @edgeDirMarksMap-1));
		    my $edgeClassIndex=int(rand(scalar @edgeTypes));
		    if($edgeClassIndex == 4) {
			$edgeDirIndex=2;
		    }
		    push @{$edgeTuples},[$i,$edgeDirIndex,$edgeClassIndex,$j];
		}
	    }
	}
    }
    return [$gname, $nodeTuples, $edgeTuples];
}


package main;
use Data::Dumper;
use List::Util qw(shuffle);

my $graphNum=0;
my @rules=();

sub randomRenderGraphRule {
    my $g=shift;
    my $name = $g->[0];

    if($name =~ /.*\:\:(.*)/) {
	$name=$1;
    }

    $name = $name.$graphNum++;

    push @rules, $name;

    my @nodeTuples = @{$g->[1]};
    my @edgeTuples = @{$g->[2]};

    my @rNodeNames=shuffle @nodeNames;
    @rNodeNames=splice(@rNodeNames,0,scalar @nodeTuples);
    
    my @nodeOrder=shuffle (0..scalar @nodeTuples-1);
    my @edgeOrder=shuffle (0..scalar @edgeTuples-1);

    print "rule $name {\n";
    print "\tmodify{\n";
    print "\t\tparent:GraphNode;\n";

    for(my $i=0; $i< scalar @nodeOrder; $i++)  {
	my $ti = $nodeOrder[$i];
	print "\t\tparent-:Contains->".$rNodeNames[$nodeTuples[$ti]->[0]].':'.$nodeTypes[$nodeTuples[$ti]->[1]].";\n";
    }

    my @rEdgeTuples = shuffle @edgeTuples;


    for(my $i=0; $i<scalar @edgeOrder; $i++) {
	my $e = $edgeTuples[$edgeOrder[$i]];
	my $edgeMark = $edgeDirMarksMap[$e->[1]];
	print "\t\t";
	print $rNodeNames[$e->[0]].$edgeMark->[0].':'.$edgeTypes[$e->[2]].$edgeMark->[1].$rNodeNames[$e->[3]].";\n";
    }
    
    print "\t\t".'eval{parent.name="'.$name.'";}'."\n";

    print "\t}\n";
    print "}\n";
}

sub getGenSubs {
    my $module = "GrGenCanonizeTest";
    no strict 'refs';
    return grep {/gen/} grep { defined &{"$module\::$_"} } keys %{"$module\::"};
}

my $nIsomorphs=8;
my @genSubs = getGenSubs();

my @graphs=();


foreach my $s (@genSubs) {
    eval 'push @graphs,'."GrGenCanonizeTest::$s();";
}

push @graphs, GrGenCanonizeTest::randomGraph("rA",5,0.6);
push @graphs, GrGenCanonizeTest::randomGraph("rB",10,0.5);
push @graphs, GrGenCanonizeTest::randomGraph("rC",15,0.4);
push @graphs, GrGenCanonizeTest::randomGraph("rD",20,0.3);
push @graphs, GrGenCanonizeTest::randomGraph("rE",25,0.3);


my @cyclegraphs=();

push @cyclegraphs, GrGenCanonizeTest::genSquare();
push @cyclegraphs, GrGenCanonizeTest::genCowbell();
push @cyclegraphs, GrGenCanonizeTest::genCube();

my @testgraphs = @graphs;

print("using CanonizeTestModel;\n\n");
foreach my $g (@testgraphs) {
    for(my $i=0; $i<$nIsomorphs; $i++) {
	randomRenderGraphRule($g);
    }
}

print join(';>',@rules);




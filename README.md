A domain specific language (DSL) for a 2D maze game with a COVID-19 theme.

To Play:
	(Windows): run CoronaRun/CoronaRun.exe
	(Mac):     run CoronaRun(mac).app

Instructions/ DSL Grammar:
	INPUT	    	::= CREATEMAP STATEMENT*
	CREATEMAP	  ::= "createMap" COORD ";" START ";" FINISH ";"
	START       ::= "setStart"  COORD ";"
	FINISH      ::= "setFinish" COORD ";"
	COORD       ::= "(" NUM "," NUM ")"
	NUM         ::= [0-9]+
	STATEMENT   ::= DRAWPATH | PLACEOBJECT
	DRAWPATH    ::= "drawPath" RANGE ";"
	RANGE       ::= "[" COORD ["-" COORD]+ "]"
	PLACEOBJECT ::= PLACEITEM | PLACEENEMY
	PLACEITEM   ::= "placeItem" "(" ITEMTYPE, COORDLIST ")" ";"
	ITEMTYPE    ::= "bomb" | "gold"
	COORDLIST   ::= "[" COORD ("," COORD)* "]"
	PLACEENEMY  ::= "placeEnemy" RANGE ";"

Note: Use zero-based index
	  eg. 3x3 game coords:	(0,2) (1,2) (2,2)
							(0,1) (1,1) (2,1)
							(0,0) (1,0) (2,0)

Example of valid input:

createMap (5,5);
setStart (0,0);
setFinish (4,4);
drawPath [(0,0)-(0,4)-(4,4)];
drawPath [(0,0)-(0,2)-(4,2)-(4,4)];
placeEnemy [(0,2)-(0,4)-(2,4)];
placeItem (bomb, [(2,2),(4,2)]);
placeItem (gold, [(3,4),(4,3)]);

Made with C#, Visual Studio IDE, and Unity.

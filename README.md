# Chess Game

### Description
Chess is one of the most popular games in the world. Each player controls sixteen pieces, which can be white or black, with white always making the first move.

During the game, when a player's king is directly threatened by an opponent's piece, it is said to be in check. In this position, the player must move the king out of danger, capture the threatening piece, or block the attack with one of their own pieces. However, blocking is not possible if the attacking piece is a knight since it can jump over opposing pieces.

The objective of the game is to checkmate the opponent's king, which occurs when the opponent's king is in check, and no move can be made to escape, defend, or counter the check.

### Piece Movement
#### Pawn
Pawns move one square forward and capture by moving one square diagonally forward. On their initial move, each pawn may advance one or two squares.

#### Rook
Rooks move vertically and horizontally.

#### Knight
Knights move one square in one direction and then one square in a perpendicular direction, forming an L-shape. Alternatively, they may move two squares in one direction and one square in a perpendicular direction. Knights are the only pieces that can jump over other pieces.

#### Bishop
Bishops move diagonally. A bishop on a light square moves diagonally on light squares, and a bishop on a dark square moves diagonally on dark squares.

#### King
The king can move only one square in any direction, except during castling, a special move described later. It is not permitted for the two kings to be adjacent to each other; there must be at least one square between them.

The king is the only piece that cannot be captured. When under attack, it is in check and must be moved out of danger.

#### Queen
The queen can move in any direction for any number of squares.

It can be said that the queen combines the movements of the rook and bishop, moving vertically, horizontally (like a rook), and diagonally (like a bishop).

### Special Moves
#### En passant
When a pawn is on its fifth rank and the opponent's pawn moves two squares forward, the first pawn can capture it as if it had only moved one square forward, landing on the square behind the opponent's pawn.

#### Castling (Kingside)
The king moves two squares toward the rook on its original square, and the rook moves to the square next to the king.

#### Castling (Queenside)
The king moves two squares toward the rook on its original square, and the rook moves to the square next to the king on the opposite side.

#### Promotion
When a pawn reaches the eighth rank, it is promoted to a queen.

### End of the Game
#### Checkmate
When the king is unable to escape check, it is checkmated, and the game ends with the opponent's victory.

#### Draw
There are two conditions for a draw. The first occurs when it is a player's turn, but they cannot move any of their pieces, particularly the king, as any move would result in check. The second condition is when only the two kings remain on the board.

### Technical Details
The game was developed in C# with the assistance of Windows Forms for creating the visual interfaces. In its source code, several important technical concepts were handled, including: interfaces, class inheritance, polymorphism, method overriding, exception handling, abstract classes and functions, delegates, structs, among others.

To document the source code to make it more understandable and explanatory, Doxygen was used. Doxygen is a tool capable of generating customized software documentation, making the code self-explanatory.
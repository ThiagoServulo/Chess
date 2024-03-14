namespace Chess
{
    /** ************************************************************************
    * \brief Information about the pawn.
    * \details The Pawn class stores information about the pawn.
    * \author Thiago Sérvulo Guimarães - thiagoservulog@gmail.com
    * \date 14/03/2024
    * \version v1.0.1
    ***************************************************************************/
    class Pawn : Piece
    {
        /** ************************************************************************
        * \brief Constructor of the Pawn class.
        * \param board Board where the piece will be inserted.
        * \param pieceColor Color of the piece.
        ***************************************************************************/
        public Pawn(Board board, Color pieceColor) : base(board, pieceColor)
        {
            Image = GetImage(pieceColor == Color.White ? "white_pawn.png" : "black_pawn.png");
        }

        /** ************************************************************************
        * \brief Check if there is an enemy.
        * \details Function responsible for checking if there is an opponent's piece
        * at the given position.
        * \param position Position to be checked.
        * \return 'true' if there is an opponent's piece at the given position,
        * 'false' otherwise.
        ***************************************************************************/
        private bool ThereIsEnemy(Position position)
        {
            Piece piece = ChessBoard.GetPiece(position);
            return (piece != null && piece.PieceColor != PieceColor);
        }

        /** ************************************************************************
        * \brief Check if a position is free.
        * \details Function responsible for checking if a certain position is free.
        * \param position Position to be checked.
        * \return 'true' if the given position is empty, 'false' otherwise.
        ***************************************************************************/
        private bool IsFree(Position position)
        {
            return (ChessBoard.GetPiece(position) == null);
        }

        /** ************************************************************************
         * \brief List possible moves.
         * \details Abstract function responsible for listing the possible moves of
         * the pawn.
         * \return Boolean matrix indicating the possible positions that the pawn 
         * can assume after its movement.
        ***************************************************************************/
        public override bool[,] PossibleMoves()
        {
            bool[,] matrix = new bool[ChessBoard.Rows, ChessBoard.Columns];

            Position position = new Position(0, 0);

            if (PieceColor == Color.White)
            {
                position.SetPosition(CurrentPosition.Row - 1, CurrentPosition.Column);
                if (ValidPosition(position) && IsFree(position))
                {
                    matrix[position.Row, position.Column] = true;
                }

                position.SetPosition(CurrentPosition.Row - 2, CurrentPosition.Column);
                if (ValidPosition(position) && IsFree(position) && NumberOfMoves == 0 &&
                    IsFree(new Position(CurrentPosition.Row - 1, CurrentPosition.Column)))
                {
                    matrix[position.Row, position.Column] = true;
                }

                position.SetPosition(CurrentPosition.Row - 1, CurrentPosition.Column - 1);
                if (ValidPosition(position) && ThereIsEnemy(position))
                {
                    matrix[position.Row, position.Column] = true;
                }

                position.SetPosition(CurrentPosition.Row - 1, CurrentPosition.Column + 1);
                if (ValidPosition(position) && ThereIsEnemy(position))
                {
                    matrix[position.Row, position.Column] = true;
                }

                // Implementation of the En Passant special move
                if (CurrentPosition.Row == 3)
                {
                    Position left = new Position(CurrentPosition.Row, CurrentPosition.Column - 1);
                    if (ValidPosition(left) && ThereIsEnemy(left))
                    {
                        Piece piece = ChessBoard.GetPiece(left);
                        Position destination = new Position(left.Row - 1, left.Column);
                        if (piece is Pawn && piece.NumberOfMoves == 1 && ChessBoard.GetPiece(destination) == null)
                        {
                            matrix[left.Row - 1, left.Column] = true;
                        }
                    }

                    Position right = new Position(CurrentPosition.Row, CurrentPosition.Column + 1);
                    if (ValidPosition(right) && ThereIsEnemy(right))
                    {
                        Piece piece = ChessBoard.GetPiece(right);
                        Position destination = new Position(left.Row - 1, left.Column);
                        if (piece is Pawn && piece.NumberOfMoves == 1 && ChessBoard.GetPiece(destination) == null)
                        {
                            matrix[right.Row - 1, right.Column] = true;
                        }
                    }
                }
            }
            else // Black pawn
            {
                position.SetPosition(CurrentPosition.Row + 1, CurrentPosition.Column);
                if (ValidPosition(position) && IsFree(position))
                {
                    matrix[position.Row, position.Column] = true;
                }

                position.SetPosition(CurrentPosition.Row + 2, CurrentPosition.Column);
                if (ValidPosition(position) && IsFree(position) && NumberOfMoves == 0 &&
                    IsFree(new Position(CurrentPosition.Row + 1, CurrentPosition.Column)))
                {
                    matrix[position.Row, position.Column] = true;
                }

                position.SetPosition(CurrentPosition.Row + 1, CurrentPosition.Column + 1);
                if (ValidPosition(position) && ThereIsEnemy(position))
                {
                    matrix[position.Row, position.Column] = true;
                }

                position.SetPosition(CurrentPosition.Row + 1, CurrentPosition.Column - 1);
                if (ValidPosition(position) && ThereIsEnemy(position))
                {
                    matrix[position.Row, position.Column] = true;
                }

                // Implementation of the En Passant special move
                if (CurrentPosition.Row == 4)
                {
                    Position left = new Position(CurrentPosition.Row, CurrentPosition.Column - 1);
                    if (ValidPosition(left) && ThereIsEnemy(left))
                    {
                        Piece piece = ChessBoard.GetPiece(left);
                        Position destination = new Position(left.Row + 1, left.Column);
                        if (piece is Pawn && piece.NumberOfMoves == 1 && ChessBoard.GetPiece(destination) == null)
                        {
                            matrix[left.Row + 1, left.Column] = true;
                        }
                    }

                    Position right = new Position(CurrentPosition.Row, CurrentPosition.Column + 1);
                    if (ValidPosition(right) && ThereIsEnemy(right))
                    {
                        Piece piece = ChessBoard.GetPiece(right);
                        Position destination = new Position(right.Row + 1, right.Column);
                        if (piece is Pawn && piece.NumberOfMoves == 1 && ChessBoard.GetPiece(destination) == null)
                        {
                            matrix[right.Row + 1, right.Column] = true;
                        }
                    }
                }
            }

            return matrix;
        }
    }
}
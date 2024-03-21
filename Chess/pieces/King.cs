namespace Chess
{
    /** ************************************************************************
    * \brief Information about the king.
    * \details The King class stores information about the king.
    * \author Thiago Sérvulo Guimarães - thiagoservulog@gmail.com
    * \date 15/03/2024
    * \version v1.0.1
    ***************************************************************************/
    class King : Piece
    {
        /// \brief Indicates if the king has already received check.
        public bool ReceivedCheck;

        /** ************************************************************************
        * \brief Constructor of the King class.
        * \param board Board where the piece will be inserted.
        * \param pieceColor Color of the piece.
        ***************************************************************************/
        public King(Board board, Color pieceColor) : base(board, pieceColor)
        {
            Image = GetImage(pieceColor == Color.White ? "white_king.png" : "black_king.png");
            ReceivedCheck = false;
        }

        /** ************************************************************************
        * \brief Checks the possibility of castling.
        * \details Function responsible for checking if there is a rook available
        * to make the special castling move.
        * \param position Position where the rook is located.
        * \return 'true' if castling is possible, 'false' if it is not possible.
        ***************************************************************************/
        private bool CheckRookAvailableForCastling(Position position)
        {
            Piece piece = ChessBoard.GetPiece(position);
            return piece is Rook && piece.PieceColor == PieceColor && piece.NumberOfMoves == 0;
        }

        /** ************************************************************************
        * \brief List possible moves.
        * \details Abstract function responsible for listing the possible moves of the
        * king.
        * \return Boolean matrix indicating the possible positions that the king 
        * can assume after its movement.
        ***************************************************************************/
        public override bool[,] PossibleMoves()
        {
            bool[,] matrix = new bool[ChessBoard.Rows, ChessBoard.Columns];

            Position position = new Position(0, 0);

            // North direction (up)
            position.SetPosition(CurrentPosition.Row - 1, CurrentPosition.Column);
            if (CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
            }

            // Northeast direction (diagonal up right)
            position.SetPosition(CurrentPosition.Row - 1, CurrentPosition.Column + 1);
            if (CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
            }

            // East direction (right)
            position.SetPosition(CurrentPosition.Row, CurrentPosition.Column + 1);
            if (CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
            }

            // Southeast direction (diagonal down right)
            position.SetPosition(CurrentPosition.Row - 1, CurrentPosition.Column - 1);
            if (CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
            }

            // South direction (down)
            position.SetPosition(CurrentPosition.Row + 1, CurrentPosition.Column);
            if (CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
            }

            // Southwest direction (diagonal down left)
            position.SetPosition(CurrentPosition.Row + 1, CurrentPosition.Column - 1);
            if (CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
            }

            // West direction (left)
            position.SetPosition(CurrentPosition.Row, CurrentPosition.Column - 1);
            if (CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
            }

            // Northwest direction (diagonal up left)
            position.SetPosition(CurrentPosition.Row + 1, CurrentPosition.Column + 1);
            if (CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
            }

            // Castling special move
            if (NumberOfMoves == 0 && !ReceivedCheck)
            {
                // Kingside castling special move
                Position positionRook1 = new Position(CurrentPosition.Row, CurrentPosition.Column + 3);
                if (CheckRookAvailableForCastling(positionRook1))
                {
                    // Check if the positions between the King and the Rook are empty
                    Position position1 = new Position(CurrentPosition.Row, CurrentPosition.Column + 1);
                    Position position2 = new Position(CurrentPosition.Row, CurrentPosition.Column + 2);
                    if (ChessBoard.GetPiece(position1) == null &&
                        ChessBoard.GetPiece(position2) == null)
                    {
                        matrix[CurrentPosition.Row, CurrentPosition.Column + 2] = true;
                    }
                }

                // Queenside castling special move
                Position positionRook2 = new Position(CurrentPosition.Row, CurrentPosition.Column - 4);
                if (CheckRookAvailableForCastling(positionRook2))
                {
                    // Check if the positions between the King and the Rook are empty
                    Position position1 = new Position(CurrentPosition.Row, CurrentPosition.Column - 1);
                    Position position2 = new Position(CurrentPosition.Row, CurrentPosition.Column - 2);
                    Position position3 = new Position(CurrentPosition.Row, CurrentPosition.Column - 3);
                    if (ChessBoard.GetPiece(position1) == null &&
                        ChessBoard.GetPiece(position2) == null &&
                        ChessBoard.GetPiece(position3) == null)
                    {
                        matrix[CurrentPosition.Row, CurrentPosition.Column - 2] = true;
                    }
                }
            }

            return matrix;
        }
    }
}
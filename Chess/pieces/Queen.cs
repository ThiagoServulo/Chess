namespace Chess
{
    /** ************************************************************************
    * \brief Information about the queen.
    * \details The Queen class stores information about the queen.
    * \author Thiago Sérvulo Guimarães - thiagoservulog@gmail.com
    * \date 14/03/2024
    * \version v1.0.1
    ***************************************************************************/
    class Queen : Piece
    {
        /** ************************************************************************
        * \brief Constructor of the Queen class.
        * \param board Board where the piece will be inserted.
        * \param pieceColor Color of the piece.
        ***************************************************************************/
        public Queen(Board board, Color pieceColor) : base(board, pieceColor)
        {
            Image = GetImage(pieceColor == Color.White ? "white_queen.png" : "black_queen.png");
        }

        /** ************************************************************************
        * \brief List possible moves.
        * \details Abstract function responsible for listing the possible moves of
        * the queen.
        * \return Boolean matrix indicating the possible positions that the queen 
        * can assume after its movement.
        ***************************************************************************/
        public override bool[,] PossibleMoves()
        {
            bool[,] matrix = new bool[ChessBoard.Rows, ChessBoard.Columns];

            Position position = new Position(0, 0);

            // North direction (up)
            position.SetPosition(CurrentPosition.Row - 1, CurrentPosition.Column);
            while (CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
                if ((ChessBoard.GetPiece(position) != null) && ChessBoard.GetPiece(position).PieceColor != PieceColor)
                {
                    break;
                }
                position.Row -= 1;
            }

            // South direction (down)
            position.SetPosition(CurrentPosition.Row + 1, CurrentPosition.Column);
            while (CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
                if ((ChessBoard.GetPiece(position) != null) && ChessBoard.GetPiece(position).PieceColor != PieceColor)
                {
                    break;
                }
                position.Row += 1;
            }

            // East direction (right)
            position.SetPosition(CurrentPosition.Row, CurrentPosition.Column + 1);
            while (CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
                if ((ChessBoard.GetPiece(position) != null) && ChessBoard.GetPiece(position).PieceColor != PieceColor)
                {
                    break;
                }
                position.Column += 1;
            }

            // West direction (left)
            position.SetPosition(CurrentPosition.Row, CurrentPosition.Column - 1);
            while (CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
                if ((ChessBoard.GetPiece(position) != null) && ChessBoard.GetPiece(position).PieceColor != PieceColor)
                {
                    break;
                }
                position.Column -= 1;
            }

            // Northwest direction (diagonal up left)
            position.SetPosition(CurrentPosition.Row + 1, CurrentPosition.Column - 1);
            while (CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
                if ((ChessBoard.GetPiece(position) != null) && ChessBoard.GetPiece(position).PieceColor != PieceColor)
                {
                    break;
                }
                position.SetPosition(position.Row + 1, position.Column - 1);
            }

            // Northeast direction (diagonal up right)
            position.SetPosition(CurrentPosition.Row + 1, CurrentPosition.Column + 1);
            while (CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
                if ((ChessBoard.GetPiece(position) != null) && ChessBoard.GetPiece(position).PieceColor != PieceColor)
                {
                    break;
                }
                position.SetPosition(position.Row + 1, position.Column + 1);
            }

            // Southwest direction (diagonal down left)
            position.SetPosition(CurrentPosition.Row - 1, CurrentPosition.Column + 1);
            while (CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
                if ((ChessBoard.GetPiece(position) != null) && ChessBoard.GetPiece(position).PieceColor != PieceColor)
                {
                    break;
                }
                position.SetPosition(position.Row - 1, position.Column + 1);
            }

            // Southeast direction (diagonal down right)
            position.SetPosition(CurrentPosition.Row - 1, CurrentPosition.Column - 1);
            while (CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
                if ((ChessBoard.GetPiece(position) != null) && ChessBoard.GetPiece(position).PieceColor != PieceColor)
                {
                    break;
                }
                position.SetPosition(position.Row - 1, position.Column - 1);
            }

            return matrix;
        }
    }
}
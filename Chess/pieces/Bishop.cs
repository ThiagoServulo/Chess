namespace Chess
{
    /** ************************************************************************
    * \brief Information about the bishop.
    * \details The Bishop class stores information about the bishop
    * \author Thiago Sérvulo Guimarães - thiagoservulog@gmail.com
    * \date 15/03/2024
    * \version v1.0.1
    ***************************************************************************/
    class Bishop : Piece
    {
        /** ************************************************************************
        * \brief Constructor of the Bishop class.
        * \param board Game board.
        * \param color Color of the piece.
        ***************************************************************************/
        public Bishop(Board board, Color pieceColor) : base(board, pieceColor)
        {
            Image = GetImage(pieceColor == Color.White ? "white_bishop.png" : "black_bishop.png");
        }

        /** ************************************************************************
        * \brief List possible moves.
        * \details Abstract function responsible for listing the possible moves of
        * the bishop.
        * \return Boolean matrix indicating the possible positions that the bishop 
        * can assume after its movement.
        ***************************************************************************/
        public override bool[,] PossibleMoves()
        {
            bool[,] matrix = new bool[ChessBoard.Rows, ChessBoard.Columns];

            Position position = new Position(0, 0);

            // Northwest direction (diagonal up left)
            position.SetPosition(CurrentPosition.Row + 1, CurrentPosition.Column - 1);
            while (CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
                if (ChessBoard.GetPiece(position) != null && ChessBoard.GetPiece(position).PieceColor != PieceColor)
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
                if (ChessBoard.GetPiece(position) != null && ChessBoard.GetPiece(position).PieceColor != PieceColor)
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
                if (ChessBoard.GetPiece(position) != null && ChessBoard.GetPiece(position).PieceColor != PieceColor)
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
                if (ChessBoard.GetPiece(position) != null && ChessBoard.GetPiece(position).PieceColor != PieceColor)
                {
                    break;
                }
                position.SetPosition(position.Row - 1, position.Column - 1);
            }

            return matrix;
        }
    }
}
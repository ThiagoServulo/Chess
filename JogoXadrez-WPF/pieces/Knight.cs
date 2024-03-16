namespace Chess
{
    /** ************************************************************************
    * \brief Information about the knight.
    * \details The Knight class stores information about the knight.
    * \author Thiago Sérvulo Guimarães - thiagoservulog@gmail.com
    * \date 15/03/2024
    * \version v1.0.1
    ***************************************************************************/
    class Knight : Piece
    {
        /** ************************************************************************
        * \brief Constructor of the Knight class.
        * \param board Game board.
        * \param color Color of the piece.
        ***************************************************************************/
        public Knight(Board board, Color pieceColor) : base(board, pieceColor)
        {
            Image = GetImage(pieceColor == Color.White ? "white_knight.png" : "black_knight.png");
        }

        /** ************************************************************************
        * \brief List possible moves.
        * \details Abstract function responsible for listing the possible moves of
        * the knight.
        * \return Boolean matrix indicating the possible positions that the knight 
        * can assume after its movement.
        ***************************************************************************/
        public override bool[,] PossibleMoves()
        {
            bool[,] matrix = new bool[ChessBoard.Rows, ChessBoard.Columns];

            Position position = new Position(0, 0);

            position.SetPosition(CurrentPosition.Row - 1, CurrentPosition.Column - 2);
            if (CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
            }

            position.SetPosition(CurrentPosition.Row - 2, CurrentPosition.Column - 1);
            if (CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
            }

            position.SetPosition(CurrentPosition.Row - 2, CurrentPosition.Column + 1);
            if (CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
            }

            position.SetPosition(CurrentPosition.Row - 1, CurrentPosition.Column + 2);
            if (CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
            }

            position.SetPosition(CurrentPosition.Row + 1, CurrentPosition.Column + 2);
            if (CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
            }

            position.SetPosition(CurrentPosition.Row + 2, CurrentPosition.Column + 1);
            if (CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
            }

            position.SetPosition(CurrentPosition.Row + 2, CurrentPosition.Column - 1);
            if (CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
            }

            position.SetPosition(CurrentPosition.Row + 1, CurrentPosition.Column - 2);
            if (CanMove(position))
            {
                matrix[position.Row, position.Column] = true;
            }

            return matrix;
        }
    }
}

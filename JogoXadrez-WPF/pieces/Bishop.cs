namespace Chess
{
    /** ************************************************************************
    * \brief Informações sobre o bispo.
    * \details A classe Bispo armazena as informações referentes ao bispo.
    * \author Thiago Sérvulo Guimarães - thiago.servulo@sga.pucminas.br
    * \date 19/07/2022
    * \version v1.0.0
    ***************************************************************************/
    class Bishop : Piece
    {
        /** ************************************************************************
        * \brief Construtor da classe Bispo.
        * \param board Tabuleiro do jogo.
        * \param color Cor da peça.
        ***************************************************************************/
        public Bishop(Board board, Color pieceColor) : base(board, pieceColor)
        {
            Image = GetImage(pieceColor == Color.White ? "white_bishop.png" : "black_bishop.png");
        }

        /** ************************************************************************
        * \brief Lista movimentos possíveis.
        * \details Função abstrata responsável por listar os movimentos posíveis do
        * bispo.
        * \return Matriz de booleanos indicando as possíveis posições que o bispo 
        * pode assumir após a sua movimentação.
        ***************************************************************************/
        public override bool[,] PossibleMoves()
        {
            bool[,] matrix = new bool[ChessBoard.Rows, ChessBoard.Columns];

            Position position = new Position(0, 0);

            // direção noroeste (diagonal superior left)
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

            // direção nordeste (diagonal superior right)
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

            // direção suldoeste (diagonal inferior left)
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

            // direção suldeste (diagonal inferior right)
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
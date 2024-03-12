namespace Chess
{
    /** ************************************************************************
    * \brief Informações sobre a rainha.
    * \details A classe Rainha armazena as informações referentes a rainha.
    * \author Thiago Sérvulo Guimarães - thiago.servulo@sga.pucminas.br
    * \date 19/07/2022
    * \version v1.0.0
    ***************************************************************************/
    class Queen : Piece
    {
        /** ************************************************************************
        * \brief Construtor da classe Rainha.
        * \param board Tabuleiro em que a peça será inserida.
        * \param pieceColor Cor da peça.
        ***************************************************************************/
        public Queen(Board board, Color pieceColor) : base(board, pieceColor)
        {
            Image = GetImage(pieceColor == Color.White ? "white_queen.png" : "black_queen.png");
        }

        /** ************************************************************************
        * \brief Lista movimentos possíveis.
        * \details Função abstrata responsável por listar os movimentos posíveis da
        * rainha.
        * \return Matriz de booleanos indicando as possíveis posições que a rainha 
        * pode assumir após a sua movimentação.
        ***************************************************************************/
        public override bool[,] PossibleMoves()
        {
            bool[,] matrix = new bool[ChessBoard.Linhas, ChessBoard.Colunas];

            Position position = new Position(0, 0);

            // direção norte (acima)
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

            // direção sul (abaixo)
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

            // direção leste (direita)
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

            // direção oeste (esquerda)
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

            // direção noroeste (diagonal superior esquerda)
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

            // direção nordeste (diagonal superior direita)
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

            // direção suldoeste (diagonal inferior esquerda)
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

            // direção suldeste (diagonal inferior direita)
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
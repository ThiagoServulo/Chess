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
            Imagem = GetImage(pieceColor == Color.White ? "white_queen.png" : "black_queen.png");
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
            position.SetPosition(CurrentPosition.Linha - 1, CurrentPosition.Coluna);
            while (CanMove(position))
            {
                matrix[position.Linha, position.Coluna] = true;
                if ((ChessBoard.AcessarPeca(position) != null) && ChessBoard.AcessarPeca(position).PieceColor != PieceColor)
                {
                    break;
                }
                position.Linha -= 1;
            }

            // direção sul (abaixo)
            position.SetPosition(CurrentPosition.Linha + 1, CurrentPosition.Coluna);
            while (CanMove(position))
            {
                matrix[position.Linha, position.Coluna] = true;
                if ((ChessBoard.AcessarPeca(position) != null) && ChessBoard.AcessarPeca(position).PieceColor != PieceColor)
                {
                    break;
                }
                position.Linha += 1;
            }

            // direção leste (direita)
            position.SetPosition(CurrentPosition.Linha, CurrentPosition.Coluna + 1);
            while (CanMove(position))
            {
                matrix[position.Linha, position.Coluna] = true;
                if ((ChessBoard.AcessarPeca(position) != null) && ChessBoard.AcessarPeca(position).PieceColor != PieceColor)
                {
                    break;
                }
                position.Coluna += 1;
            }

            // direção oeste (esquerda)
            position.SetPosition(CurrentPosition.Linha, CurrentPosition.Coluna - 1);
            while (CanMove(position))
            {
                matrix[position.Linha, position.Coluna] = true;
                if ((ChessBoard.AcessarPeca(position) != null) && ChessBoard.AcessarPeca(position).PieceColor != PieceColor)
                {
                    break;
                }
                position.Coluna -= 1;
            }

            // direção noroeste (diagonal superior esquerda)
            position.SetPosition(CurrentPosition.Linha + 1, CurrentPosition.Coluna - 1);
            while (CanMove(position))
            {
                matrix[position.Linha, position.Coluna] = true;
                if ((ChessBoard.AcessarPeca(position) != null) && ChessBoard.AcessarPeca(position).PieceColor != PieceColor)
                {
                    break;
                }
                position.SetPosition(position.Linha + 1, position.Coluna - 1);
            }

            // direção nordeste (diagonal superior direita)
            position.SetPosition(CurrentPosition.Linha + 1, CurrentPosition.Coluna + 1);
            while (CanMove(position))
            {
                matrix[position.Linha, position.Coluna] = true;
                if ((ChessBoard.AcessarPeca(position) != null) && ChessBoard.AcessarPeca(position).PieceColor != PieceColor)
                {
                    break;
                }
                position.SetPosition(position.Linha + 1, position.Coluna + 1);
            }

            // direção suldoeste (diagonal inferior esquerda)
            position.SetPosition(CurrentPosition.Linha - 1, CurrentPosition.Coluna + 1);
            while (CanMove(position))
            {
                matrix[position.Linha, position.Coluna] = true;
                if ((ChessBoard.AcessarPeca(position) != null) && ChessBoard.AcessarPeca(position).PieceColor != PieceColor)
                {
                    break;
                }
                position.SetPosition(position.Linha - 1, position.Coluna + 1);
            }

            // direção suldeste (diagonal inferior direita)
            position.SetPosition(CurrentPosition.Linha - 1, CurrentPosition.Coluna - 1);
            while (CanMove(position))
            {
                matrix[position.Linha, position.Coluna] = true;
                if ((ChessBoard.AcessarPeca(position) != null) && ChessBoard.AcessarPeca(position).PieceColor != PieceColor)
                {
                    break;
                }
                position.SetPosition(position.Linha - 1, position.Coluna - 1);
            }

            return matrix;
        }
    }
}
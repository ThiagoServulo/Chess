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

            Position posicao = new Position(0, 0);

            // direção norte (acima)
            posicao.SetPosition(CurrentPosition.Linha - 1, CurrentPosition.Coluna);
            while (CanMove(posicao))
            {
                matrix[posicao.Linha, posicao.Coluna] = true;
                if ((ChessBoard.AcessarPeca(posicao) != null) && ChessBoard.AcessarPeca(posicao).PieceColor != PieceColor)
                {
                    break;
                }
                posicao.Linha -= 1;
            }

            // direção sul (abaixo)
            posicao.SetPosition(CurrentPosition.Linha + 1, CurrentPosition.Coluna);
            while (CanMove(posicao))
            {
                matrix[posicao.Linha, posicao.Coluna] = true;
                if ((ChessBoard.AcessarPeca(posicao) != null) && ChessBoard.AcessarPeca(posicao).PieceColor != PieceColor)
                {
                    break;
                }
                posicao.Linha += 1;
            }

            // direção leste (direita)
            posicao.SetPosition(CurrentPosition.Linha, CurrentPosition.Coluna + 1);
            while (CanMove(posicao))
            {
                matrix[posicao.Linha, posicao.Coluna] = true;
                if ((ChessBoard.AcessarPeca(posicao) != null) && ChessBoard.AcessarPeca(posicao).PieceColor != PieceColor)
                {
                    break;
                }
                posicao.Coluna += 1;
            }

            // direção oeste (esquerda)
            posicao.SetPosition(CurrentPosition.Linha, CurrentPosition.Coluna - 1);
            while (CanMove(posicao))
            {
                matrix[posicao.Linha, posicao.Coluna] = true;
                if ((ChessBoard.AcessarPeca(posicao) != null) && ChessBoard.AcessarPeca(posicao).PieceColor != PieceColor)
                {
                    break;
                }
                posicao.Coluna -= 1;
            }

            // direção noroeste (diagonal superior esquerda)
            posicao.SetPosition(CurrentPosition.Linha + 1, CurrentPosition.Coluna - 1);
            while (CanMove(posicao))
            {
                matrix[posicao.Linha, posicao.Coluna] = true;
                if ((ChessBoard.AcessarPeca(posicao) != null) && ChessBoard.AcessarPeca(posicao).PieceColor != PieceColor)
                {
                    break;
                }
                posicao.SetPosition(posicao.Linha + 1, posicao.Coluna - 1);
            }

            // direção nordeste (diagonal superior direita)
            posicao.SetPosition(CurrentPosition.Linha + 1, CurrentPosition.Coluna + 1);
            while (CanMove(posicao))
            {
                matrix[posicao.Linha, posicao.Coluna] = true;
                if ((ChessBoard.AcessarPeca(posicao) != null) && ChessBoard.AcessarPeca(posicao).PieceColor != PieceColor)
                {
                    break;
                }
                posicao.SetPosition(posicao.Linha + 1, posicao.Coluna + 1);
            }

            // direção suldoeste (diagonal inferior esquerda)
            posicao.SetPosition(CurrentPosition.Linha - 1, CurrentPosition.Coluna + 1);
            while (CanMove(posicao))
            {
                matrix[posicao.Linha, posicao.Coluna] = true;
                if ((ChessBoard.AcessarPeca(posicao) != null) && ChessBoard.AcessarPeca(posicao).PieceColor != PieceColor)
                {
                    break;
                }
                posicao.SetPosition(posicao.Linha - 1, posicao.Coluna + 1);
            }

            // direção suldeste (diagonal inferior direita)
            posicao.SetPosition(CurrentPosition.Linha - 1, CurrentPosition.Coluna - 1);
            while (CanMove(posicao))
            {
                matrix[posicao.Linha, posicao.Coluna] = true;
                if ((ChessBoard.AcessarPeca(posicao) != null) && ChessBoard.AcessarPeca(posicao).PieceColor != PieceColor)
                {
                    break;
                }
                posicao.SetPosition(posicao.Linha - 1, posicao.Coluna - 1);
            }

            return matrix;
        }
    }
}
namespace Chess
{
    /** ************************************************************************
    * \brief Informações sobre o cavalo.
    * \details A classe Cavalo armazena as informações referentes ao cavalo.
    * \author Thiago Sérvulo Guimarães - thiago.servulo@sga.pucminas.br
    * \date 19/07/2022
    * \version v1.0.0
    ***************************************************************************/
    class Knight : Piece
    {
        /** ************************************************************************
        * \brief Construtor da classe Cavalo.
        * \param board Tabuleiro do jogo.
        * \param cor Cor da peça.
        ***************************************************************************/
        public Knight(Board board, Color pieceColor) : base(board, pieceColor)
        {
            Imagem = GetImage(pieceColor == Color.White ? "white_knight.png" : "black_knight.png");
        }

        /** ************************************************************************
        * \brief Lista movimentos possíveis.
        * \details Função abstrata responsável por listar os movimentos posíveis do
        * cavalo.
        * \return Matriz de booleanos indicando as possíveis posições que o cavalo 
        * pode assumir após a sua movimentação.
        ***************************************************************************/
        public override bool[,] PossibleMoves()
        {
            bool[,] matrix = new bool[ChessBoard.Linhas, ChessBoard.Colunas];

            Position posicao = new Position(0, 0);

            posicao.SetPosition(CurrentPosition.Linha - 1, CurrentPosition.Coluna - 2);
            if (CanMove(posicao))
            {
                matrix[posicao.Linha, posicao.Coluna] = true;
            }

            posicao.SetPosition(CurrentPosition.Linha - 2, CurrentPosition.Coluna - 1);
            if (CanMove(posicao))
            {
                matrix[posicao.Linha, posicao.Coluna] = true;
            }

            posicao.SetPosition(CurrentPosition.Linha - 2, CurrentPosition.Coluna + 1);
            if (CanMove(posicao))
            {
                matrix[posicao.Linha, posicao.Coluna] = true;
            }

            posicao.SetPosition(CurrentPosition.Linha - 1, CurrentPosition.Coluna + 2);
            if (CanMove(posicao))
            {
                matrix[posicao.Linha, posicao.Coluna] = true;
            }

            posicao.SetPosition(CurrentPosition.Linha + 1, CurrentPosition.Coluna + 2);
            if (CanMove(posicao))
            {
                matrix[posicao.Linha, posicao.Coluna] = true;
            }

            posicao.SetPosition(CurrentPosition.Linha + 2, CurrentPosition.Coluna + 1);
            if (CanMove(posicao))
            {
                matrix[posicao.Linha, posicao.Coluna] = true;
            }

            posicao.SetPosition(CurrentPosition.Linha + 2, CurrentPosition.Coluna - 1);
            if (CanMove(posicao))
            {
                matrix[posicao.Linha, posicao.Coluna] = true;
            }

            posicao.SetPosition(CurrentPosition.Linha + 1, CurrentPosition.Coluna - 2);
            if (CanMove(posicao))
            {
                matrix[posicao.Linha, posicao.Coluna] = true;
            }

            return matrix;
        }
    }
}

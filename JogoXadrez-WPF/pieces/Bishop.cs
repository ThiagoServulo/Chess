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
        * \param cor Cor da peça.
        ***************************************************************************/
        public Bishop(Board board, Color pieceColor) : base(board, pieceColor)
        {
            Imagem = GetImage(pieceColor == Color.White ? "white_bishop.png" : "black_bishop.png");
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
            bool[,] matrix = new bool[ChessBoard.Linhas, ChessBoard.Colunas];

            Position posicao = new Position(0, 0);

            // direção noroeste (diagonal superior esquerda)
            posicao.SetPosition(CurrentPosition.Linha + 1, CurrentPosition.Coluna - 1);
            while (CanMove(posicao))
            {
                matrix[posicao.Linha, posicao.Coluna] = true;
                if (ChessBoard.AcessarPeca(posicao) != null && ChessBoard.AcessarPeca(posicao).PieceColor != PieceColor)
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
                if (ChessBoard.AcessarPeca(posicao) != null && ChessBoard.AcessarPeca(posicao).PieceColor != PieceColor)
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
                if (ChessBoard.AcessarPeca(posicao) != null && ChessBoard.AcessarPeca(posicao).PieceColor != PieceColor)
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
                if (ChessBoard.AcessarPeca(posicao) != null && ChessBoard.AcessarPeca(posicao).PieceColor != PieceColor)
                {
                    break;
                }
                posicao.SetPosition(posicao.Linha - 1, posicao.Coluna - 1);
            }

            return matrix;
        }
    }
}
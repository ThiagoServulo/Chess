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

            Position position = new Position(0, 0);

            // direção noroeste (diagonal superior esquerda)
            position.SetPosition(CurrentPosition.Linha + 1, CurrentPosition.Coluna - 1);
            while (CanMove(position))
            {
                matrix[position.Linha, position.Coluna] = true;
                if (ChessBoard.AcessarPeca(position) != null && ChessBoard.AcessarPeca(position).PieceColor != PieceColor)
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
                if (ChessBoard.AcessarPeca(position) != null && ChessBoard.AcessarPeca(position).PieceColor != PieceColor)
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
                if (ChessBoard.AcessarPeca(position) != null && ChessBoard.AcessarPeca(position).PieceColor != PieceColor)
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
                if (ChessBoard.AcessarPeca(position) != null && ChessBoard.AcessarPeca(position).PieceColor != PieceColor)
                {
                    break;
                }
                position.SetPosition(position.Linha - 1, position.Coluna - 1);
            }

            return matrix;
        }
    }
}
namespace Chess
{
    /** ************************************************************************
    * \brief Informações sobre a torre.
    * \details A classe Torre armazena as informações referentes a torre.
    * \author Thiago Sérvulo Guimarães - thiago.servulo@sga.pucminas.br
    * \date 19/07/2022
    * \version v1.0.0
    ***************************************************************************/
    class Rook : Piece
    {
        /** ************************************************************************
        * \brief Construtor da classe Torre.
        * \param board Tabuleiro em que a peça será inserida.
        * \param pieceColor Cor da peça.
        ***************************************************************************/
        public Rook(Board board, Color pieceColor) : base(board, pieceColor)
        {
            Imagem = GetImage(pieceColor == Color.White ? "white_rook.png" : "black_rook.png");
        }

        /** ************************************************************************
        * \brief Lista movimentos possíveis.
        * \details Função abstrata responsável por listar os movimentos posíveis da
        * torre.
        * \return Matriz de booleanos indicando as possíveis posições que a torre
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

            return matrix;
        }

    }
}
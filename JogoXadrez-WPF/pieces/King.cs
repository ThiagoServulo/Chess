namespace Chess
{
    /** ************************************************************************
    * \brief Informações sobre o rei.
    * \details A classe Rei armazena as informações referentes ao rei.
    * \author Thiago Sérvulo Guimarães - thiago.servulo@sga.pucminas.br
    * \date 19/07/2022
    * \version v1.0.0
    ***************************************************************************/
    class King : Piece
    {
        /// \brief Indica se o rei já recebu xeque.
        public bool ReceivedCheck;

        /** ************************************************************************
        * \brief Construtor da classe Rei.
        * \param board Tabuleiro em que a peça será inserida.
        * \param pieceColor Cor da peça.
        ***************************************************************************/
        public King(Board board, Color pieceColor) : base(board, pieceColor)
        {
            Imagem = GetImage(pieceColor == Color.White ? "white_king.png" : "black_king.png");
            ReceivedCheck = false;
        }

        /** ************************************************************************
        * \brief Verifica a possibilidade de roque.
        * \details Função responsável por verificar se existe uma torre disponível
        * para fazer a jogada especial de roque.
        * \param position Posição em que a torre se encontra.
        * \return 'true' - se a jogada de roque for possível, 'false' se não for
        * possível.
        ***************************************************************************/
        private bool VerificaTorreDisponivelParaRoque(Position position)
        {
            Piece peca = ChessBoard.AcessarPeca(position);
            return peca is Rook && peca.PieceColor == PieceColor && peca.NumberOfMoves == 0;
        }

        /** ************************************************************************
        * \brief Lista movimentos possíveis.
        * \details Função abstrata responsável por listar os movimentos posíveis do
        * rei.
        * \return Matriz de booleanos indicando as possíveis posições que o rei 
        * pode assumir após a sua movimentação.
        ***************************************************************************/
        public override bool[,] PossibleMoves()
        {
            bool[,] matrix = new bool[ChessBoard.Linhas, ChessBoard.Colunas];

            Position position = new Position(0, 0);

            // direção norte (acima)
            position.SetPosition(CurrentPosition.Linha - 1, CurrentPosition.Coluna);
            if (CanMove(position))
            {
                matrix[position.Linha, position.Coluna] = true;
            }

            // direção nordeste (diagonal superior direita)
            position.SetPosition(CurrentPosition.Linha - 1, CurrentPosition.Coluna + 1);
            if (CanMove(position))
            {
                matrix[position.Linha, position.Coluna] = true;
            }

            // direção leste (direita)
            position.SetPosition(CurrentPosition.Linha, CurrentPosition.Coluna + 1);
            if (CanMove(position))
            {
                matrix[position.Linha, position.Coluna] = true;
            }

            // direção suldeste (diagonal inferior direita)
            position.SetPosition(CurrentPosition.Linha - 1, CurrentPosition.Coluna - 1);
            if (CanMove(position))
            {
                matrix[position.Linha, position.Coluna] = true;
            }

            // direção sul (abaixo)
            position.SetPosition(CurrentPosition.Linha + 1, CurrentPosition.Coluna);
            if (CanMove(position))
            {
                matrix[position.Linha, position.Coluna] = true;
            }

            // direção suldoeste (diagonal inferior esquerda)
            position.SetPosition(CurrentPosition.Linha + 1, CurrentPosition.Coluna - 1);
            if (CanMove(position))
            {
                matrix[position.Linha, position.Coluna] = true;
            }

            // direção oeste (esquerda)
            position.SetPosition(CurrentPosition.Linha, CurrentPosition.Coluna - 1);
            if (CanMove(position))
            {
                matrix[position.Linha, position.Coluna] = true;
            }

            // direção noroeste (diagonal superior esquerda)
            position.SetPosition(CurrentPosition.Linha + 1, CurrentPosition.Coluna + 1);
            if (CanMove(position))
            {
                matrix[position.Linha, position.Coluna] = true;
            }

            // #jogadaespecial roque
            if (NumberOfMoves == 0 && !ReceivedCheck)
            {
                // #jogadaespecial roque pequeno
                Position positionRook1 = new Position(CurrentPosition.Linha, CurrentPosition.Coluna + 3);
                if (VerificaTorreDisponivelParaRoque(positionRook1))
                {
                    // Verifica se as posições entre o Rei e a Torre estão vazias
                    Position position1 = new Position(CurrentPosition.Linha, CurrentPosition.Coluna + 1);
                    Position position2 = new Position(CurrentPosition.Linha, CurrentPosition.Coluna + 2);
                    if (ChessBoard.AcessarPeca(position1) == null &&
                        ChessBoard.AcessarPeca(position2) == null)
                    {
                        matrix[CurrentPosition.Linha, CurrentPosition.Coluna + 2] = true;
                    }
                }

                // #jogadaespecial roque grande
                Position positionRook2 = new Position(CurrentPosition.Linha, CurrentPosition.Coluna - 4);
                if (VerificaTorreDisponivelParaRoque(positionRook2))
                {
                    // Verifica se as posições entre o Rei e a Torre estão vazias
                    Position position1 = new Position(CurrentPosition.Linha, CurrentPosition.Coluna - 1);
                    Position position2 = new Position(CurrentPosition.Linha, CurrentPosition.Coluna - 2);
                    Position position3 = new Position(CurrentPosition.Linha, CurrentPosition.Coluna - 3);
                    if (ChessBoard.AcessarPeca(position1) == null &&
                        ChessBoard.AcessarPeca(position2) == null &&
                        ChessBoard.AcessarPeca(position3) == null)
                    {
                        matrix[CurrentPosition.Linha, CurrentPosition.Coluna - 2] = true;
                    }
                }
            }

            return matrix;
        }
    }
}
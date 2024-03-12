namespace Chess
{
    /** ************************************************************************
    * \brief Informações sobre o peão.
    * \details A classe Peao armazena as informações referentes ao peão.
    * \author Thiago Sérvulo Guimarães - thiago.servulo@sga.pucminas.br
    * \date 19/07/2022
    * \version v1.0.0
    ***************************************************************************/
    class Pawn : Piece
    {
        /** ************************************************************************
        * \brief Construtor da classe Peao.
        * \param board Tabuleiro em que a peça será inserida.
        * \param pieceColor Cor da peça.
        ***************************************************************************/
        public Pawn(Board board, Color pieceColor) : base(board, pieceColor)
        {
            Image = GetImage(pieceColor == Color.White ? "white_pawn.png" : "black_pawn.png");
        }

        /** ************************************************************************
        * \brief Verifica se existe inimigo.
        * \details Função responsável por verificar se existe uma peça adversária na
        * posição informada.
        * \param position Posição a ser verificada.
        * \param 'true' se existir uma peça adversária na posição informada, 'false'
        * se não.
        ***************************************************************************/
        private bool ThereIsEnemy(Position position)
        {
            Piece piece = ChessBoard.GetPiece(position);
            return (piece != null && piece.PieceColor != PieceColor);
        }

        /** ************************************************************************
        * \brief Verifica se uma posição está livre.
        * \details Função responsável por verificar se uma determinada posição está 
        * livre.
        * \param position Posição a ser verificada.
        * \param 'true' se a posição iformada estiver vazia, 'false' se não.
        ***************************************************************************/
        private bool IsFree(Position position)
        {
            return (ChessBoard.GetPiece(position) == null);
        }

        /** ************************************************************************
        * \brief Lista movimentos possíveis.
        * \details Função abstrata responsável por listar os movimentos posíveis do
        * peão.
        * \return Matriz de booleanos indicando as possíveis posições que o peão 
        * pode assumir após a sua movimentação.
        ***************************************************************************/
        public override bool[,] PossibleMoves()
        {
            bool[,] matrix = new bool[ChessBoard.Rows, ChessBoard.Columns];

            Position position = new Position(0, 0);

            if (PieceColor == Color.White)
            {
                position.SetPosition(CurrentPosition.Row - 1, CurrentPosition.Column);
                if (ValidPosition(position) && IsFree(position))
                {
                    matrix[position.Row, position.Column] = true;
                }

                position.SetPosition(CurrentPosition.Row - 2, CurrentPosition.Column);
                if (ValidPosition(position) && IsFree(position) && NumberOfMoves == 0 &&
                    IsFree(new Position(CurrentPosition.Row - 1, CurrentPosition.Column)))
                {
                    matrix[position.Row, position.Column] = true;
                }

                position.SetPosition(CurrentPosition.Row - 1, CurrentPosition.Column - 1);
                if (ValidPosition(position) && ThereIsEnemy(position))
                {
                    matrix[position.Row, position.Column] = true;
                }

                position.SetPosition(CurrentPosition.Row - 1, CurrentPosition.Column + 1);
                if (ValidPosition(position) && ThereIsEnemy(position))
                {
                    matrix[position.Row, position.Column] = true;
                }

                // Implementação da jogada especial En Passant
                if (CurrentPosition.Row == 3)
                {
                    Position left = new Position(CurrentPosition.Row, CurrentPosition.Column - 1);
                    if (ValidPosition(left) && ThereIsEnemy(left))
                    {
                        Piece piece = ChessBoard.GetPiece(left);
                        Position destination = new Position(left.Row - 1, left.Column);
                        if (piece is Pawn && piece.NumberOfMoves == 1 && ChessBoard.GetPiece(destination) == null)
                        {
                            matrix[left.Row - 1, left.Column] = true;
                        }
                    }

                    Position right = new Position(CurrentPosition.Row, CurrentPosition.Column + 1);
                    if (ValidPosition(right) && ThereIsEnemy(right))
                    {
                        Piece piece = ChessBoard.GetPiece(right);
                        Position destination = new Position(left.Row - 1, left.Column);
                        if (piece is Pawn && piece.NumberOfMoves == 1 && ChessBoard.GetPiece(destination) == null)
                        {
                            matrix[right.Row - 1, right.Column] = true;
                        }
                    }
                }
            }
            else // Peão de cor preta
            {
                position.SetPosition(CurrentPosition.Row + 1, CurrentPosition.Column);
                if (ValidPosition(position) && IsFree(position))
                {
                    matrix[position.Row, position.Column] = true;
                }

                position.SetPosition(CurrentPosition.Row + 2, CurrentPosition.Column);
                if (ValidPosition(position) && IsFree(position) && NumberOfMoves == 0 &&
                    IsFree(new Position(CurrentPosition.Row + 1, CurrentPosition.Column)))
                {
                    matrix[position.Row, position.Column] = true;
                }

                position.SetPosition(CurrentPosition.Row + 1, CurrentPosition.Column + 1);
                if (ValidPosition(position) && ThereIsEnemy(position))
                {
                    matrix[position.Row, position.Column] = true;
                }

                position.SetPosition(CurrentPosition.Row + 1, CurrentPosition.Column - 1);
                if (ValidPosition(position) && ThereIsEnemy(position))
                {
                    matrix[position.Row, position.Column] = true;
                }

                // Implementação da jogada especial En Passant
                if (CurrentPosition.Row == 4)
                {
                    Position left = new Position(CurrentPosition.Row, CurrentPosition.Column - 1);
                    if (ValidPosition(left) && ThereIsEnemy(left))
                    {
                        Piece piece = ChessBoard.GetPiece(left);
                        Position destination = new Position(left.Row + 1, left.Column);
                        if (piece is Pawn && piece.NumberOfMoves == 1 && ChessBoard.GetPiece(destination) == null)
                        {
                            matrix[left.Row + 1, left.Column] = true;
                        }
                    }

                    Position right = new Position(CurrentPosition.Row, CurrentPosition.Column + 1);
                    if (ValidPosition(right) && ThereIsEnemy(right))
                    {
                        Piece piece = ChessBoard.GetPiece(right);
                        Position destination = new Position(right.Row + 1, right.Column);
                        if (piece is Pawn && piece.NumberOfMoves == 1 && ChessBoard.GetPiece(destination) == null)
                        {
                            matrix[right.Row + 1, right.Column] = true;
                        }
                    }
                }
            }

            return matrix;
        }
    }
}
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
        * \param tabuleiro Tabuleiro em que a peça será inserida.
        * \param corDaPeca Cor da peça.
        ***************************************************************************/
        public Pawn(Tabuleiro tabuleiro, Color corDaPeca) : base(tabuleiro, corDaPeca)
        {
            Imagem = BuscarImagem(corDaPeca == Color.White ? "white_pawn.png" : "black_pawn.png");
        }

        /** ************************************************************************
        * \brief Verifica se existe inimigo.
        * \details Função responsável por verificar se existe uma peça adversária na
        * posição informada.
        * \param posicao Posição a ser verificada.
        * \param 'true' se existir uma peça adversária na posição informada, 'false'
        * se não.
        ***************************************************************************/
        private bool ExisteInimigo(Position posicao)
        {
            Piece peca = TabuleiroXadrez.AcessarPeca(posicao);
            return (peca != null && peca.PieceColor != PieceColor);
        }

        /** ************************************************************************
        * \brief Verifica se uma posição está livre.
        * \details Função responsável por verificar se uma determinada posição está 
        * livre.
        * \param posicao Posição a ser verificada.
        * \param 'true' se a posição iformada estiver vazia, 'false' se não.
        ***************************************************************************/
        private bool Livre(Position posicao)
        {
            return (TabuleiroXadrez.AcessarPeca(posicao) == null);
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
            bool[,] matriz = new bool[TabuleiroXadrez.Linhas, TabuleiroXadrez.Colunas];

            Position posicao = new Position(0, 0);

            if (PieceColor == Color.White)
            {
                posicao.DefinirPosicao(CurrentPosition.Linha - 1, CurrentPosition.Coluna);
                if (ValidPosition(posicao) && Livre(posicao))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                posicao.DefinirPosicao(CurrentPosition.Linha - 2, CurrentPosition.Coluna);
                if (ValidPosition(posicao) && Livre(posicao) && NumberOfMoves == 0 &&
                    Livre(new Position(CurrentPosition.Linha - 1, CurrentPosition.Coluna)))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                posicao.DefinirPosicao(CurrentPosition.Linha - 1, CurrentPosition.Coluna - 1);
                if (ValidPosition(posicao) && ExisteInimigo(posicao))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                posicao.DefinirPosicao(CurrentPosition.Linha - 1, CurrentPosition.Coluna + 1);
                if (ValidPosition(posicao) && ExisteInimigo(posicao))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                // Implementação da jogada especial En Passant
                if (CurrentPosition.Linha == 3)
                {
                    Position esquerda = new Position(CurrentPosition.Linha, CurrentPosition.Coluna - 1);
                    if (ValidPosition(esquerda) && ExisteInimigo(esquerda))
                    {
                        Piece peca = TabuleiroXadrez.AcessarPeca(esquerda);
                        Position destino = new Position(esquerda.Linha - 1, esquerda.Coluna);
                        if (peca is Pawn && peca.NumberOfMoves == 1 && TabuleiroXadrez.AcessarPeca(destino) == null)
                        {
                            matriz[esquerda.Linha - 1, esquerda.Coluna] = true;
                        }
                    }

                    Position direita = new Position(CurrentPosition.Linha, CurrentPosition.Coluna + 1);
                    if (ValidPosition(direita) && ExisteInimigo(direita))
                    {
                        Piece peca = TabuleiroXadrez.AcessarPeca(direita);
                        Position destino = new Position(esquerda.Linha - 1, esquerda.Coluna);
                        if (peca is Pawn && peca.NumberOfMoves == 1 && TabuleiroXadrez.AcessarPeca(destino) == null)
                        {
                            matriz[direita.Linha - 1, direita.Coluna] = true;
                        }
                    }
                }
            }
            else // Peão de cor preta
            {
                posicao.DefinirPosicao(CurrentPosition.Linha + 1, CurrentPosition.Coluna);
                if (ValidPosition(posicao) && Livre(posicao))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                posicao.DefinirPosicao(CurrentPosition.Linha + 2, CurrentPosition.Coluna);
                if (ValidPosition(posicao) && Livre(posicao) && NumberOfMoves == 0 &&
                    Livre(new Position(CurrentPosition.Linha + 1, CurrentPosition.Coluna)))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                posicao.DefinirPosicao(CurrentPosition.Linha + 1, CurrentPosition.Coluna + 1);
                if (ValidPosition(posicao) && ExisteInimigo(posicao))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                posicao.DefinirPosicao(CurrentPosition.Linha + 1, CurrentPosition.Coluna - 1);
                if (ValidPosition(posicao) && ExisteInimigo(posicao))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                // Implementação da jogada especial En Passant
                if (CurrentPosition.Linha == 4)
                {
                    Position esquerda = new Position(CurrentPosition.Linha, CurrentPosition.Coluna - 1);
                    if (ValidPosition(esquerda) && ExisteInimigo(esquerda))
                    {
                        Piece peca = TabuleiroXadrez.AcessarPeca(esquerda);
                        Position destino = new Position(esquerda.Linha + 1, esquerda.Coluna);
                        if (peca is Pawn && peca.NumberOfMoves == 1 && TabuleiroXadrez.AcessarPeca(destino) == null)
                        {
                            matriz[esquerda.Linha + 1, esquerda.Coluna] = true;
                        }
                    }

                    Position direita = new Position(CurrentPosition.Linha, CurrentPosition.Coluna + 1);
                    if (ValidPosition(direita) && ExisteInimigo(direita))
                    {
                        Piece peca = TabuleiroXadrez.AcessarPeca(direita);
                        Position destino = new Position(direita.Linha + 1, direita.Coluna);
                        if (peca is Pawn && peca.NumberOfMoves == 1 && TabuleiroXadrez.AcessarPeca(destino) == null)
                        {
                            matriz[direita.Linha + 1, direita.Coluna] = true;
                        }
                    }
                }
            }

            return matriz;
        }
    }
}
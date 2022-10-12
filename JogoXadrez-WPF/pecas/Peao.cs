namespace JogoXadrez_WPF
{
    /** ************************************************************************
    * \brief Informações sobre o Peão.
    * \details A classe Peao armazena as informações referentes ao peão, que é 
    * uma das peças do jogo.
    * \author Thiago Sérvulo Guimarães - thiago.servulo@sga.pucminas.br
    * \date 20/09/2022
    * \version v1.0.0
    ***************************************************************************/
    class Peao : Peca
    {
        /** ************************************************************************
        * \brief Construtor.
        * \details Construtor da classe Peao.
        * \param tabuleiro Tabuleiro em que a peça será inserida.
        * \param corDaPeca Cor da peça.
        ***************************************************************************/
        public Peao(Tabuleiro tabuleiro, Cor corDaPeca) : base(tabuleiro, corDaPeca)
        {
            Imagem = BuscarImagem(corDaPeca == Cor.Branco ? "peao_branco.png" : "peao_preto.png");
        }

        /** ************************************************************************
        * \brief Verifica se existe inimigo.
        * \details Função responsável por verificar se existe uma peça adversária na
        * posição informada.
        * \param posicao Posição a ser verificada.
        * \param 'true' se existir uma peça adversária na posição informada, 'false'
        * se não.
        ***************************************************************************/
        private bool ExisteInimigo(Posicao posicao)
        {
            Peca peca = TabuleiroXadrez.AcessarPeca(posicao);
            return (peca != null && peca.CorDaPeca != CorDaPeca);
        }

        /** ************************************************************************
        * \brief Verifica se uma posição está livre.
        * \details Função responsável por verificar se uma determinada posição está 
        * livre.
        * \param posicao Posição a ser verificada.
        * \param 'true' se a posição iformada estiver vazia, 'false' se não.
        ***************************************************************************/
        private bool Livre(Posicao posicao)
        {
            return (TabuleiroXadrez.AcessarPeca(posicao) == null);
        }

        /** ************************************************************************
        * \brief Movimentos possíveis.
        * \details Função responsável por informar as possíveis movimentações que o
        * peão pode assumir.
        * \return Matriz de possibilidades com posições que um peão pode assumir em 
        * uma jogada.
        ***************************************************************************/
        public override bool[,] MovimentosPossiveis()
        {
            bool[,] matriz = new bool[TabuleiroXadrez.Linhas, TabuleiroXadrez.Colunas];

            Posicao posicao = new Posicao(0, 0);

            if (CorDaPeca == Cor.Branco)
            {
                posicao.DefinirPosicao(PosicaoAtual.Linha - 1, PosicaoAtual.Coluna);
                if (PosicaoValida(posicao) && Livre(posicao))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                posicao.DefinirPosicao(PosicaoAtual.Linha - 2, PosicaoAtual.Coluna);
                if (PosicaoValida(posicao) && Livre(posicao) && QuantidadeDeMovimentos == 0 &&
                    Livre(new Posicao(PosicaoAtual.Linha - 1, PosicaoAtual.Coluna)))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                posicao.DefinirPosicao(PosicaoAtual.Linha - 1, PosicaoAtual.Coluna - 1);
                if (PosicaoValida(posicao) && ExisteInimigo(posicao))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                posicao.DefinirPosicao(PosicaoAtual.Linha - 1, PosicaoAtual.Coluna + 1);
                if (PosicaoValida(posicao) && ExisteInimigo(posicao))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                // Implementação da jogada especial En Passant
                if (PosicaoAtual.Linha == 3)
                {
                    Posicao esquerda = new Posicao(PosicaoAtual.Linha, PosicaoAtual.Coluna - 1);
                    if (PosicaoValida(esquerda) && ExisteInimigo(esquerda))
                    {
                        Peca peca = TabuleiroXadrez.AcessarPeca(esquerda);
                        Posicao destino = new Posicao(esquerda.Linha - 1, esquerda.Coluna);
                        if (peca is Peao && peca.QuantidadeDeMovimentos == 1 && TabuleiroXadrez.AcessarPeca(destino) == null)
                        {
                            matriz[esquerda.Linha - 1, esquerda.Coluna] = true;
                        }
                    }

                    Posicao direita = new Posicao(PosicaoAtual.Linha, PosicaoAtual.Coluna + 1);
                    if (PosicaoValida(direita) && ExisteInimigo(direita))
                    {
                        Peca peca = TabuleiroXadrez.AcessarPeca(direita);
                        Posicao destino = new Posicao(esquerda.Linha - 1, esquerda.Coluna);
                        if (peca is Peao && peca.QuantidadeDeMovimentos == 1 && TabuleiroXadrez.AcessarPeca(destino) == null)
                        {
                            matriz[direita.Linha - 1, direita.Coluna] = true;
                        }
                    }
                }
            }
            else // Peão de cor preta
            {
                posicao.DefinirPosicao(PosicaoAtual.Linha + 1, PosicaoAtual.Coluna);
                if (PosicaoValida(posicao) && Livre(posicao))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                posicao.DefinirPosicao(PosicaoAtual.Linha + 2, PosicaoAtual.Coluna);
                if (PosicaoValida(posicao) && Livre(posicao) && QuantidadeDeMovimentos == 0 &&
                    Livre(new Posicao(PosicaoAtual.Linha + 1, PosicaoAtual.Coluna)))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                posicao.DefinirPosicao(PosicaoAtual.Linha + 1, PosicaoAtual.Coluna + 1);
                if (PosicaoValida(posicao) && ExisteInimigo(posicao))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                posicao.DefinirPosicao(PosicaoAtual.Linha + 1, PosicaoAtual.Coluna - 1);
                if (PosicaoValida(posicao) && ExisteInimigo(posicao))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                // Implementação da jogada especial En Passant
                if (PosicaoAtual.Linha == 4)
                {
                    Posicao esquerda = new Posicao(PosicaoAtual.Linha, PosicaoAtual.Coluna - 1);
                    if (PosicaoValida(esquerda) && ExisteInimigo(esquerda))
                    {
                        Peca peca = TabuleiroXadrez.AcessarPeca(esquerda);
                        Posicao destino = new Posicao(esquerda.Linha + 1, esquerda.Coluna);
                        if (peca is Peao && peca.QuantidadeDeMovimentos == 1 && TabuleiroXadrez.AcessarPeca(destino) == null)
                        {
                            matriz[esquerda.Linha + 1, esquerda.Coluna] = true;
                        }
                    }

                    Posicao direita = new Posicao(PosicaoAtual.Linha, PosicaoAtual.Coluna + 1);
                    if (PosicaoValida(direita) && ExisteInimigo(direita))
                    {
                        Peca peca = TabuleiroXadrez.AcessarPeca(direita);
                        Posicao destino = new Posicao(direita.Linha + 1, direita.Coluna);
                        if (peca is Peao && peca.QuantidadeDeMovimentos == 1 && TabuleiroXadrez.AcessarPeca(destino) == null)
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
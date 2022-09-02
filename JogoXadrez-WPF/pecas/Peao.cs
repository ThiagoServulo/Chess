namespace JogoXadrez_WPF
{
    class Peao : Peca
    {
        public Peao(Tabuleiro tabuleiro, Cor corDaPeca) : base(tabuleiro, corDaPeca)
        {
            Imagem = BuscarImagem(corDaPeca == Cor.Branco ? "peao_branco.png" : "peao_preto.png");
        }

        private bool ExisteInimigo(Posicao posicao)
        {
            Peca peca = TabuleiroXadrez.AcessarPeca(posicao);
            return (peca != null && peca.CorDaPeca != CorDaPeca);
        }
        
        private bool Livre(Posicao posicao)
        {
            return (TabuleiroXadrez.AcessarPeca(posicao) == null);
        }
        
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
                if (PosicaoValida(posicao) && Livre(posicao) && QuantidadeDeMovimentos == 0)
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

                /*
                // #jogadaespecial en passant
                if (PosicaoAtual.Linha == 3)
                {
                    Posicao esquerda = new Posicao(PosicaoAtual.Linha, PosicaoAtual.Coluna - 1);
                    if (Tab.PosicaoValida(esquerda) && ExisteInimigo(esquerda) && (Tab.AcessarPeca(esquerda) == partida.VulneravelEnPassant))
                    {
                        matriz[esquerda.Linha - 1, esquerda.Coluna] = true;
                    }

                    Posicao direita = new Posicao(PosicaoAtual.Linha, PosicaoAtual.Coluna + 1);
                    if (Tab.PosicaoValida(direita) && ExisteInimigo(direita) && (Tab.AcessarPeca(direita) == partida.VulneravelEnPassant))
                    {
                        matriz[direita.Linha - 1, direita.Coluna] = true;
                    }
                }
                */
            }
            else
            {
                posicao.DefinirPosicao(PosicaoAtual.Linha + 1, PosicaoAtual.Coluna);
                if (PosicaoValida(posicao) && Livre(posicao))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                posicao.DefinirPosicao(PosicaoAtual.Linha + 2, PosicaoAtual.Coluna);
                if (PosicaoValida(posicao) && Livre(posicao) && QuantidadeDeMovimentos == 0)
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

                /*
                // #jogadaespecial en passant
                if (PosicaoAtual.Linha == 4)
                {
                    Posicao esquerda = new Posicao(PosicaoAtual.Linha, PosicaoAtual.Coluna - 1);
                    if (Tab.PosicaoValida(esquerda) && ExisteInimigo(esquerda) && (Tab.AcessarPeca(esquerda) == partida.VulneravelEnPassant))
                    {
                        matriz[esquerda.Linha + 1, esquerda.Coluna] = true;
                    }

                    Posicao direita = new Posicao(PosicaoAtual.Linha, PosicaoAtual.Coluna + 1);
                    if (Tab.PosicaoValida(direita) && ExisteInimigo(direita) && (Tab.AcessarPeca(direita) == partida.VulneravelEnPassant))
                    {
                        matriz[direita.Linha + 1, direita.Coluna] = true;
                    }
                }
                */
            }

            return matriz;
        }
    }
}
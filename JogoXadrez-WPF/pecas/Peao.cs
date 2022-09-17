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
                
                // #jogadaespecial en passant
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
            else
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

                // #jogadaespecial en passant
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
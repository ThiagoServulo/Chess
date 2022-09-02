namespace JogoXadrez_WPF
{
    class Cavalo : Peca
    {
        public Cavalo(Tabuleiro tabuleiro, Cor corDaPeca) : base(tabuleiro, corDaPeca)
        {
            Imagem = BuscarImagem(corDaPeca == Cor.Branco ? "cavalo_branco.png" : "cavalo_preto.png");
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] matriz = new bool[TabuleiroXadrez.Linhas, TabuleiroXadrez.Colunas];

            Posicao posicao = new Posicao(0, 0);

            posicao.DefinirPosicao(PosicaoAtual.Linha - 1, PosicaoAtual.Coluna - 2);
            if (PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            posicao.DefinirPosicao(PosicaoAtual.Linha - 2, PosicaoAtual.Coluna - 1);
            if (PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            posicao.DefinirPosicao(PosicaoAtual.Linha - 2, PosicaoAtual.Coluna + 1);
            if (PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            posicao.DefinirPosicao(PosicaoAtual.Linha - 1, PosicaoAtual.Coluna + 2);
            if (PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            posicao.DefinirPosicao(PosicaoAtual.Linha + 1, PosicaoAtual.Coluna + 2);
            if (PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            posicao.DefinirPosicao(PosicaoAtual.Linha + 2, PosicaoAtual.Coluna + 1);
            if (PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            posicao.DefinirPosicao(PosicaoAtual.Linha + 2, PosicaoAtual.Coluna - 1);
            if (PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            posicao.DefinirPosicao(PosicaoAtual.Linha + 1, PosicaoAtual.Coluna - 2);
            if (PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            return matriz;
        }
    }
}

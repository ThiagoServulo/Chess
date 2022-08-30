using System.Drawing;

namespace JogoXadrez_WPF
{
    class Bispo : Peca
    {
        public Bispo(Tabuleiro tabuleiro, Cor corDaPeca) : base(tabuleiro, corDaPeca)
        {
            // TODO: Arrumar a imagem do bispo preto em relação ao branco
            Imagem = Image.FromFile(corDaPeca == Cor.Branco ? "D:/Projetos/JogoXadrez-WPF/JogoXadrez-WPF/pecas/imagens/bispo_branco.png" : "D:/Projetos/JogoXadrez-WPF/JogoXadrez-WPF/pecas/imagens/bispo_preto.png");
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] matriz = new bool[TabuleiroXadrez.Linhas, TabuleiroXadrez.Colunas];

            Posicao posicao = new Posicao(0, 0);

            // direção noroeste (diagonal superior esquerda)
            posicao.DefinirPosicao(PosicaoAtual.Linha + 1, PosicaoAtual.Coluna - 1);
            while (PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
                if (TabuleiroXadrez.AcessarPeca(posicao) != null && TabuleiroXadrez.AcessarPeca(posicao).CorDaPeca != CorDaPeca)
                {
                    break;
                }
                posicao.DefinirPosicao(posicao.Linha + 1, posicao.Coluna - 1);
            }

            // direção nordeste (diagonal superior direita)
            posicao.DefinirPosicao(PosicaoAtual.Linha + 1, PosicaoAtual.Coluna + 1);
            while (PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
                if (TabuleiroXadrez.AcessarPeca(posicao) != null && TabuleiroXadrez.AcessarPeca(posicao).CorDaPeca != CorDaPeca)
                {
                    break;
                }
                posicao.DefinirPosicao(posicao.Linha + 1, posicao.Coluna + 1);
            }

            // direção suldoeste (diagonal inferior esquerda)
            posicao.DefinirPosicao(PosicaoAtual.Linha - 1, PosicaoAtual.Coluna + 1);
            while (PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
                if (TabuleiroXadrez.AcessarPeca(posicao) != null && TabuleiroXadrez.AcessarPeca(posicao).CorDaPeca != CorDaPeca)
                {
                    break;
                }
                posicao.DefinirPosicao(posicao.Linha - 1, posicao.Coluna + 1);
            }

            // direção suldeste (diagonal inferior direita)
            posicao.DefinirPosicao(PosicaoAtual.Linha - 1, PosicaoAtual.Coluna - 1);
            while (PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
                if (TabuleiroXadrez.AcessarPeca(posicao) != null && TabuleiroXadrez.AcessarPeca(posicao).CorDaPeca != CorDaPeca)
                {
                    break;
                }
                posicao.DefinirPosicao(posicao.Linha - 1, posicao.Coluna - 1);
            }

            return matriz;
        }
    }
}
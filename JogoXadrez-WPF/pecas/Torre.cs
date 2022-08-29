using System.Drawing;
using System.Windows.Forms;

namespace JogoXadrez_WPF
{
    class Torre : Peca
    {
        public Image Imagem;

        public Torre(Tabuleiro tabuleiro, Cor corDaPeca) : base(tabuleiro, corDaPeca)
        {
            Imagem = Image.FromFile(corDaPeca == Cor.Branco ? "D:/Projetos/JogoXadrez-WPF/JogoXadrez-WPF/pecas/imagens/torre_branco.png" : "D:/Projetos/JogoXadrez-WPF/JogoXadrez-WPF/pecas/imagens/torre_preto.png");
        }

        public override string ToString()
        {
            return "T";
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] matriz = new bool[TabuleiroXadrez.Linhas, TabuleiroXadrez.Colunas];

            Posicao posicao = new Posicao(0, 0);

            // direção norte (acima)
            posicao.DefinirPosicao(PosicaoAtual.Linha - 1, PosicaoAtual.Coluna);
            while (PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
                if ((TabuleiroXadrez.AcessarPeca(posicao) != null) && TabuleiroXadrez.AcessarPeca(posicao).CorDaPeca != CorDaPeca)
                {
                    break;
                }
                posicao.Linha -= 1;
            }

            // direção sul (abaixo)
            posicao.DefinirPosicao(PosicaoAtual.Linha + 1, PosicaoAtual.Coluna);
            while (PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
                if ((TabuleiroXadrez.AcessarPeca(posicao) != null) && TabuleiroXadrez.AcessarPeca(posicao).CorDaPeca != CorDaPeca)
                {
                    break;
                }
                posicao.Linha += 1;
            }

            // direção leste (direita)
            posicao.DefinirPosicao(PosicaoAtual.Linha, PosicaoAtual.Coluna + 1);
            while (PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
                if ((TabuleiroXadrez.AcessarPeca(posicao) != null) && TabuleiroXadrez.AcessarPeca(posicao).CorDaPeca != CorDaPeca)
                {
                    break;
                }
                posicao.Coluna += 1;
            }

            // direção oeste (esquerda)
            posicao.DefinirPosicao(PosicaoAtual.Linha, PosicaoAtual.Coluna - 1);
            while (PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
                if ((TabuleiroXadrez.AcessarPeca(posicao) != null) && TabuleiroXadrez.AcessarPeca(posicao).CorDaPeca != CorDaPeca)
                {
                    break;
                }
                posicao.Coluna -= 1;
            }

            return matriz;
        }

        public override Image MostrarImagem()
        {
            return Imagem;
        }
    }
}
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace JogoXadrez_WPF
{
    abstract class Peca
    {
        public Image Imagem;
        public Posicao PosicaoAtual { get; set; }
        public Cor CorDaPeca { get; protected set; }
        public int QuantidadeDeMovimentos { get; protected set; }
        public Tabuleiro TabuleiroXadrez { get; protected set; }
        public string FileImagesPath;

        public Peca(Tabuleiro tabuleiro, Cor cor)
        {
            FileImagesPath = Path.GetFullPath("./imagens");
            PosicaoAtual = null;
            TabuleiroXadrez = tabuleiro;
            CorDaPeca = cor;
            QuantidadeDeMovimentos = 0;
        }

        public Image MostrarImagem()
        {
            return Imagem;
        }

        public Image BuscarImagem(string ImagemPeca)
        {
            try
            {
                return Image.FromFile($"{FileImagesPath}/{ImagemPeca}");
            }
            catch
            {
                // TODO: criar a exceção personalizada
                return null; 
            }
        }

        public void IncrementarQuantidadeDeMovimentos()
        {
            QuantidadeDeMovimentos++;
        }

        public void DecrementarQuantidadeDeMovimentos()
        {
            QuantidadeDeMovimentos--;
        }
        
        public bool PosicaoValida(Posicao posicao)
        {
            return posicao.Linha <= 7 && posicao.Linha >= 0 && posicao.Coluna <= 7 && posicao.Coluna >= 0;
        }

        public bool PodeMover(Posicao posicao)
        {
            if (PosicaoValida(posicao))
            {
                Peca peca = TabuleiroXadrez.AcessarPeca(posicao);
                return (peca == null || peca.CorDaPeca != CorDaPeca);
            }
            return false;
        }

        public bool ExisteMovimentosPossiveis()
        {
            bool[,] matriz = MovimentosPossiveis();
            for (int i = 0; i < TabuleiroXadrez.Linhas; i++)
            {
                for (int j = 0; j < TabuleiroXadrez.Colunas; j++)
                {
                    if (matriz[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool MovimentoPossivel(Posicao posicao)
        {
            return MovimentosPossiveis()[posicao.Linha, posicao.Coluna];
        }

        public abstract bool[,] MovimentosPossiveis();

    }
}
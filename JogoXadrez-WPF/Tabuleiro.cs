using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace JogoXadrez_WPF
{
    partial class Tabuleiro : Form
    {
        public int Linhas = 8;
        public int Colunas = 8;
        private Peca[,] _pecasEmJogo;
        private PictureBox[,] _pictureBoxes;
        private HashSet<Peca> _pecasCapturadas;
        private Posicao _origem;
        private Cor _jogadorAtual;

        public Tabuleiro()
        {
            _pecasCapturadas = new HashSet<Peca>();
            _jogadorAtual = Cor.Branco;
            _origem = null;
            InitializeComponent();
            _pecasEmJogo = new Peca[Linhas, Colunas];
            _pictureBoxes = new PictureBox[8, 8] {
                { pictureBoxA1, pictureBoxA2, pictureBoxA3, pictureBoxA4, pictureBoxA5, pictureBoxA6, pictureBoxA7, pictureBoxA8 },
                { pictureBoxB1, pictureBoxB2, pictureBoxB3, pictureBoxB4, pictureBoxB5, pictureBoxB6, pictureBoxB7, pictureBoxB8 },
                { pictureBoxC1, pictureBoxC2, pictureBoxC3, pictureBoxC4, pictureBoxC5, pictureBoxC6, pictureBoxC7, pictureBoxC8 },
                { pictureBoxD1, pictureBoxD2, pictureBoxD3, pictureBoxD4, pictureBoxD5, pictureBoxD6, pictureBoxD7, pictureBoxD8 },
                { pictureBoxE1, pictureBoxE2, pictureBoxE3, pictureBoxE4, pictureBoxE5, pictureBoxE6, pictureBoxE7, pictureBoxE8 },
                { pictureBoxF1, pictureBoxF2, pictureBoxF3, pictureBoxF4, pictureBoxF5, pictureBoxF6, pictureBoxF7, pictureBoxF8 },
                { pictureBoxG1, pictureBoxG2, pictureBoxG3, pictureBoxG4, pictureBoxG5, pictureBoxG6, pictureBoxG7, pictureBoxG8 },
                { pictureBoxH1, pictureBoxH2, pictureBoxH3, pictureBoxH4, pictureBoxH5, pictureBoxH6, pictureBoxH7, pictureBoxH8 }
            } ;
            InicializaTabuleiro();
        }

        private void MudaJogador()
        {
            _jogadorAtual = _jogadorAtual == Cor.Branco ? Cor.Preto : Cor.Branco;
        }

        public Peca AcessarPeca(int linha, int coluna)
        {
            return _pecasEmJogo[linha, coluna];
        }

        public Peca AcessarPeca(Posicao posicao)
        {
            return _pecasEmJogo[posicao.Linha, posicao.Coluna];
        }

        public void ColocarPeca(Peca peca, Posicao posicao)
        {
            _pecasEmJogo[posicao.Linha, posicao.Coluna] = peca;
            peca.PosicaoAtual = posicao;
            _pictureBoxes[posicao.Linha, posicao.Coluna].Image = peca.MostrarImagem();
        }

        private void InicializaTabuleiro()
        {
            MostrarTabuleiro();
            ColocarPeca(new Cavalo(this, Cor.Preto), new Posicao(0, 0));
            ColocarPeca(new Cavalo(this, Cor.Branco), new Posicao(0, 1));
        }

        private void MostrarTabuleiro()
        {
            Color color;
            for (int linha = 0; linha < Linhas; linha++)
            {
                color = (linha % 2 == 0) ? Color.Gray : Color.White;
                for (int coluna = 0; coluna < Colunas; coluna++)
                {
                    _pictureBoxes[linha, coluna].BackColor = color;
                    color = (color == Color.White) ? Color.Gray : Color.White;
                }
            }
        }

        private void MostrarTabuleiro(bool[,] posicoesPossiveis)
        {
            Color color;
            for (int linha = 0; linha < Linhas; linha++)
            {
                color = (linha % 2 == 0) ? Color.Gray : Color.White;
                for (int coluna = 0; coluna < Colunas; coluna++)
                {
                    _pictureBoxes[linha, coluna].BackColor = posicoesPossiveis[linha, coluna] ? 
                        (_pictureBoxes[linha, coluna].BackColor == Color.White ? Color.LightCyan : Color.LightBlue) : color;
                    color = color == Color.White ? Color.Gray : Color.White;
                }
            }
        }

        public Peca RetirarPeca(Posicao posicao)
        {
            if (!ExistePeca(posicao))
            {
                return null;
            }
            Peca aux = AcessarPeca(posicao);
            aux.PosicaoAtual = null;
            _pecasEmJogo[posicao.Linha, posicao.Coluna] = null;
            return aux;
        }

        public bool ExistePeca(Posicao posicao)
        {
            return AcessarPeca(posicao) != null;
        }

        private void ProcessaPictureBoxClick(Posicao posicao)
        {
            bool mover = false;
            if (_origem != null && _origem.CompareTo(posicao) == 0)
            {
                MostrarTabuleiro();
                _origem = null;
                return;
            }

            if (ExistePeca(posicao))
            {
                Peca peca = AcessarPeca(posicao);
                if(peca.CorDaPeca == _jogadorAtual)
                {
                    _origem = posicao;
                    bool[,] posicoesPossiveis = peca.MovimentosPossiveis();
                    MostrarTabuleiro(posicoesPossiveis);
                }
                else if(_origem != null)
                {
                    mover = true;
                }
            }
            else
            {
                if ((_pictureBoxes[posicao.Linha, posicao.Coluna].BackColor == Color.LightBlue ||
                    _pictureBoxes[posicao.Linha, posicao.Coluna].BackColor == Color.LightCyan) &&
                    _origem != null)
                {
                    mover = true;
                }
            }

            if(mover)
            {
                MostrarTabuleiro();
                ExecutaMovimento(_origem, posicao);
                MudaJogador();
                _origem = null;
            }
        }

        public Peca ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca peca = RetirarPeca(origem);
            peca.IncrementarQuantidadeDeMovimentos();
            Peca pecaCapturada = RetirarPeca(destino);
            ColocarPeca(peca, destino);
            if (pecaCapturada != null)
            {
                _pecasCapturadas.Add(pecaCapturada);
            }
            _pictureBoxes[origem.Linha, origem.Coluna].Image = null;
            return pecaCapturada;
        }

        private void PictureBoxA1Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(0, 0));
        }

        private void PictureBoxA2Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(0, 1));
        }

        private void PictureBoxA3Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(0, 2));
        }

        private void PictureBoxA4Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(0, 3));
        }

        private void PictureBoxA5Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(0, 4));
        }

        private void PictureBoxA6Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(0, 5));
        }

        private void PictureBoxA7Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(0, 6));
        }

        private void PictureBoxA8Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(0, 7));
        }

        private void PictureBoxB1Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(1, 0));
        }

        private void PictureBoxB2Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(1, 1));
        }

        private void PictureBoxB3Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(1, 2));
        }

        private void PictureBoxB4Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(1, 3));
        }

        private void PictureBoxB5Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(1, 4));
        }

        private void PictureBoxB6Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(1, 5));
        }

        private void PictureBoxB7Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(1, 6));
        }

        private void PictureBoxB8Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(1, 7));
        }
    }
}

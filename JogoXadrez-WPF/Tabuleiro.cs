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
            ColocarPeca(new Rei(this, Cor.Branco), new Posicao(6, 0));
            ColocarPeca(new Rei(this, Cor.Preto), new Posicao(7, 0));
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

        private void PictureBoxC1Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(2, 0));
        }

        private void PictureBoxC2Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(2, 1));
        }

        private void PictureBoxC3Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(2, 2));
        }

        private void PictureBoxC4Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(2, 3));
        }

        private void PictureBoxC5Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(2, 4));
        }

        private void PictureBoxC6Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(2, 5));
        }

        private void PictureBoxC7Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(2, 6));
        }

        private void PictureBoxC8Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(2, 7));
        }

        private void PictureBoxD1Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(3, 0));
        }

        private void PictureBoxD2Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(3, 1));
        }

        private void PictureBoxD3Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(3, 2));
        }

        private void PictureBoxD4Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(3, 3));
        }

        private void PictureBoxD5Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(3, 4));
        }

        private void PictureBoxD6Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(3, 5));
        }

        private void PictureBoxD7Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(3, 6));
        }

        private void PictureBoxD8Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(3, 7));
        }

        private void PictureBoxE1Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(4, 0));
        }

        private void PictureBoxE2Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(4, 1));
        }

        private void PictureBoxE3Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(4, 2));
        }

        private void PictureBoxE4Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(4, 3));
        }

        private void PictureBoxE5Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(4, 4));
        }

        private void PictureBoxE6Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(4, 5));
        }

        private void PictureBoxE7Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(4, 6));
        }

        private void PictureBoxE8Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(4, 7));
        }

        private void PictureBoxF1Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(5, 0));
        }

        private void PictureBoxF2Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(5, 1));
        }

        private void PictureBoxF3Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(5, 2));
        }

        private void PictureBoxF4Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(5, 3));
        }

        private void PictureBoxF5Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(5, 4));
        }

        private void PictureBoxF6Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(5, 5));
        }

        private void PictureBoxF7Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(5, 6));
        }

        private void PictureBoxF8Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(5, 7));
        }

        private void PictureBoxG1Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(6, 0));
        }

        private void PictureBoxG2Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(6, 1));
        }

        private void PictureBoxG3Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(6, 2));
        }

        private void PictureBoxG4Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(6, 3));
        }

        private void PictureBoxG5Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(6, 4));
        }

        private void PictureBoxG6Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(6, 5));
        }

        private void PictureBoxG7Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(6, 6));
        }

        private void PictureBoxG8Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(6, 7));
        }

        private void PictureBoxH1Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(7, 0));
        }

        private void PictureBoxH2Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(7, 1));
        }

        private void PictureBoxH3Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(7, 2));
        }

        private void PictureBoxH4Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(7, 3));
        }

        private void PictureBoxH5Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(7, 4));
        }

        private void PictureBoxH6Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(7, 5));
        }

        private void PictureBoxH7Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(7, 6));
        }

        private void PictureBoxH8Click(object sender, System.EventArgs e)
        {
            ProcessaPictureBoxClick(new Posicao(7, 7));
        }
    }
}

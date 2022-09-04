﻿using System.Windows.Forms;
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
        private Posicao _origem;
        private Cor _jogadorAtual;
        private int _turno;
        private int _quantidadeBrancasCapturadas;
        private int _quantidadePretasCapturadas;

        public Tabuleiro()
        {
            _quantidadeBrancasCapturadas = 0;
            _quantidadePretasCapturadas = 0;
            _turno = 1;
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
            AtualizaLabels();
            InicializaTabuleiro();
        }

        private void MudaJogador()
        {
            _jogadorAtual = CorAdversaria(_jogadorAtual);
        }

        private Cor CorAdversaria(Cor cor)
        {
            return cor == Cor.Branco ? Cor.Preto : Cor.Branco;
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
            ColocarPeca(new Rei(this, Cor.Preto), new Posicao(0, 0));
            ColocarPeca(new Rei(this, Cor.Branco), new Posicao(7 , 7));
            ColocarPeca(new Rainha(this, Cor.Branco), new Posicao(6, 1));
            ColocarPeca(new Rainha(this, Cor.Preto), new Posicao(0, 1));
            //ColocarPeca(new Rei(this, Cor.Branco), new Posicao(6, 0));
            //ColocarPeca(new Peao(this, Cor.Preto), new Posicao(7, 0));
        }

        private void AtualizaLabelJogador()
        {
            labelJogadorAtual.Text = "Jogador atual: " + (_jogadorAtual == Cor.Branco ? "Branco" : "Preto");
        }

        private void AtualizaLabelTurno()
        {
            labelTurno.Text = $"Turno: {_turno}";
        }

        private void AtualizaLabelsPecasCapturadas()
        {
            labelPecasCapturadasBranco.Text = $"Peças capturadas: {_quantidadePretasCapturadas}";
            labelPecasCapturadasPreto.Text = $"Peças capturadas: {_quantidadeBrancasCapturadas}";
        }

        private void AtualizaLabels()
        {
            AtualizaLabelJogador();
            AtualizaLabelTurno();
            AtualizaLabelsPecasCapturadas();
        }
        private void IncrementaTurno()
        {
            _turno++;
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
                    bool[,] posicoesPossiveis = ChecarMovimentosPossiveis(peca);
                    MostrarTabuleiro(posicoesPossiveis);
                }
                else if(_origem != null && !(AcessarPeca(_origem) is Peao))
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
                IncrementaTurno();
                AtualizaLabels();
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
                if(pecaCapturada.CorDaPeca == Cor.Branco)
                {
                    _quantidadeBrancasCapturadas++;
                }
                else
                {
                    _quantidadePretasCapturadas++;
                }
                PecasEmJogo(Cor.Branco);
                PecasEmJogo(Cor.Preto);
            }
            _pictureBoxes[origem.Linha, origem.Coluna].Image = null;
            return pecaCapturada;
        }

        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca peca in _pecasEmJogo)
            {
                if (peca != null && peca.CorDaPeca == cor)
                {
                    aux.Add(peca);
                }
            }
            return aux;
        }

        
        private Posicao PegarPosicaoRei(Cor cor)
        {
            for (int coluna = 0; coluna < Colunas; coluna++)
            {
                for (int linha = 0; linha < Linhas; linha++)
                {
                    if(_pecasEmJogo[linha, coluna] != null && _pecasEmJogo[linha, coluna] is Rei && _pecasEmJogo[linha, coluna].CorDaPeca == cor)
                    {
                        return new Posicao(linha, coluna);
                    }
                }
            }
            return null;
        }
        
        public bool VerificaXeque(Cor cor)
        {
            Posicao posicaoRei = PegarPosicaoRei(cor);

            //MessageBox.Show($"Posição rei da cor {rei.CorDaPeca} - {rei.PosicaoAtual}");

            if (posicaoRei == null)
            {
                throw new System.Exception($"Não tem rei da cor: {cor} no tabuleiro");
            }

            foreach (Peca peca in PecasEmJogo(CorAdversaria(cor)))
            {
                bool[,] matriz = peca.MovimentosPossiveis();
                if (matriz[posicaoRei.Linha, posicaoRei.Coluna])
                {
                    return true;
                }
            }

            return false;
        }

        public bool[,] ChecarMovimentosPossiveis(Peca peca)
        {
            bool[,] posicoesPossiveis = peca.MovimentosPossiveis();
            for(int coluna = 0; coluna < Colunas; coluna++)
            {
                for (int linha = 0; linha < Linhas; linha++)
                {
                    if (posicoesPossiveis[linha, coluna])
                    {
                        _pecasEmJogo[peca.PosicaoAtual.Linha, peca.PosicaoAtual.Coluna] = null;
                        Peca pecaCapturada = AcessarPeca(linha, coluna);
                        _pecasEmJogo[linha, coluna] = peca;
                        if(VerificaXeque(peca.CorDaPeca))
                        {
                            posicoesPossiveis[linha, coluna] = false;
                        }
                        _pecasEmJogo[linha, coluna] = pecaCapturada;
                        _pecasEmJogo[peca.PosicaoAtual.Linha, peca.PosicaoAtual.Coluna] = peca;
                    }
                }
            }
            return posicoesPossiveis;
        }

    }
}

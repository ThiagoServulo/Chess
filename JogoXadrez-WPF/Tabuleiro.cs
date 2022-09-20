using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;

namespace JogoXadrez_WPF
{
    /** ************************************************************************
    * \brief Informações sobre o tabuleiro.
    * \details A classe Tabuleiro armazena as informações referentes ao tabuleiro 
    * do jogo, ou seja, onde as peças serão colocadas.
    * \author Thiago Sérvulo Guimarães - thiago.servulo@sga.pucminas.br
    * \date 20/09/2022
    * \version v1.0.0
    ***************************************************************************/

    partial class Tabuleiro : Form
    {
        /// \brief Quantidade de linhas existentes no tabuleiro.
        public int Linhas = 8;

        /// \brief Quantidade de colunas existentes no tabuleiro.
        public int Colunas = 8;

        /// \brief Indica se a partida está em xeque.
        public bool _xeque;

        /// \brief Matriz contendo todas as peças que estão em jogo.
        private Peca[,] _pecasEmJogo;

        /// \brief Matriz contendo todas os campos do tabuleiro.
        private PictureBox[,] _pictureBoxes;

        /// \brief Posição de origem da jogada atual.
        private Posicao _origem;

        /// \brief Cor do jogador atual.
        private Cor _jogadorAtual;

        /// \brief Número do turno atual.
        private int _turno;

        /// \brief Quantidade de peças brancas capturadas.
        private int _quantidadeBrancasCapturadas;

        /// \brief Quantidade de peças pretas capturadas.
        private int _quantidadePretasCapturadas;

        public Tabuleiro()
        {
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
            };

            InicializarNovoJogo();
        }

        private void InicializarNovoJogo()
        {
            _xeque = false;
            _quantidadeBrancasCapturadas = 0;
            _quantidadePretasCapturadas = 0;
            _turno = 1;
            _jogadorAtual = Cor.Branco;
            _origem = null;
            AtualizaLabels();
            InicializaTabuleiro();
        }

        private void MudaJogador()
        {
            // Muda o jogador atual
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
            // Inicializa layout tabuleiro
            MostrarTabuleiro();

            // Resetando a imagem de todos os Picture Boxes
            for (int linha = 0; linha < Linhas; linha++)
            {
                for (int coluna = 0; coluna < Colunas; coluna++)
                {
                    _pecasEmJogo[linha, coluna] = null;
                    _pictureBoxes[linha, coluna].Image = null;
                }
            }

            // Coloca as peças brancas
            ColocarPeca(new Torre(this, Cor.Branco), new Posicao(7, 0));
            ColocarPeca(new Cavalo(this, Cor.Branco), new Posicao(7, 1));
            ColocarPeca(new Bispo(this, Cor.Branco), new Posicao(7, 2));
            ColocarPeca(new Rainha(this, Cor.Branco), new Posicao(7, 3));
            ColocarPeca(new Rei(this, Cor.Branco), new Posicao(7, 4));
            ColocarPeca(new Bispo(this, Cor.Branco), new Posicao(7, 5));
            ColocarPeca(new Cavalo(this, Cor.Branco), new Posicao(7, 6));
            ColocarPeca(new Torre(this, Cor.Branco), new Posicao(7, 7));
            ColocarPeca(new Peao(this, Cor.Branco), new Posicao(6, 7));
            ColocarPeca(new Peao(this, Cor.Branco), new Posicao(6, 6));
            ColocarPeca(new Peao(this, Cor.Branco), new Posicao(6, 5));
            ColocarPeca(new Peao(this, Cor.Branco), new Posicao(6, 4));
            ColocarPeca(new Peao(this, Cor.Branco), new Posicao(6, 3));
            ColocarPeca(new Peao(this, Cor.Branco), new Posicao(6, 2));
            ColocarPeca(new Peao(this, Cor.Branco), new Posicao(6, 1));
            ColocarPeca(new Peao(this, Cor.Branco), new Posicao(6, 0));

            // Coloca as peças pretas
            ColocarPeca(new Torre(this, Cor.Preto), new Posicao(0, 0));
            ColocarPeca(new Cavalo(this, Cor.Preto), new Posicao(0, 1));
            ColocarPeca(new Bispo(this, Cor.Preto), new Posicao(0, 2));
            ColocarPeca(new Rainha(this, Cor.Preto), new Posicao(0, 3));
            ColocarPeca(new Rei(this, Cor.Preto), new Posicao(0, 4));
            ColocarPeca(new Bispo(this, Cor.Preto), new Posicao(0, 5));
            ColocarPeca(new Cavalo(this, Cor.Preto), new Posicao(0, 6));
            ColocarPeca(new Torre(this, Cor.Preto), new Posicao(0, 7));
            ColocarPeca(new Peao(this, Cor.Preto), new Posicao(1, 7));
            ColocarPeca(new Peao(this, Cor.Preto), new Posicao(1, 6));
            ColocarPeca(new Peao(this, Cor.Preto), new Posicao(1, 5));
            ColocarPeca(new Peao(this, Cor.Preto), new Posicao(1, 4));
            ColocarPeca(new Peao(this, Cor.Preto), new Posicao(1, 3));
            ColocarPeca(new Peao(this, Cor.Preto), new Posicao(1, 2));
            ColocarPeca(new Peao(this, Cor.Preto), new Posicao(1, 1));
            ColocarPeca(new Peao(this, Cor.Preto), new Posicao(1, 0));
        }

        private void AtualizaLabelXeque()
        {
            labelXeque.Text = _xeque ? "Você está em Xeque" : " ";
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
            AtualizaLabelXeque();
        }
        private void IncrementaTurno()
        {
            _turno++;
        }

        private void MostrarTabuleiro()
        {
            Color color;

            // Imprime o tabuleiro com as cores iniciais
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

            // Imprimir o tabuleiro nas cores iniciais
            MostrarTabuleiro();

            // Imprimir tabuleiro destacando as possíveis jogadas
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
            // Verifica se existe peça na posição informada
            if (!ExistePeca(posicao))
            {
                return null;
            }

            // Retira a peça da posição informada
            Peca aux = AcessarPeca(posicao);
            aux.PosicaoAtual = null;

            // Limpa as informações referentes ao campo localizado nesta posição
            _pecasEmJogo[posicao.Linha, posicao.Coluna] = null;
            _pictureBoxes[posicao.Linha, posicao.Coluna].Image = null;

            // Retorna a peça retirada
            return aux;
        }

        public bool ExistePeca(Posicao posicao)
        {
            return AcessarPeca(posicao) != null;
        }

        private void ProcessaPictureBoxClick(Posicao posicao)
        {
            if (_origem != null && _origem.CompareTo(posicao) == 0)
            {
                MostrarTabuleiro();
                _origem = null;
                return;
            }

            if (ExistePeca(posicao))
            {
                Peca peca = AcessarPeca(posicao);
                if (peca.CorDaPeca == _jogadorAtual)
                {
                    _origem = posicao;
                    bool[,] posicoesPossiveis = ChecarMovimentosPossiveis(peca);
                    MostrarTabuleiro(posicoesPossiveis);
                    return;
                }
            }

            if ((_pictureBoxes[posicao.Linha, posicao.Coluna].BackColor == Color.LightBlue ||
                _pictureBoxes[posicao.Linha, posicao.Coluna].BackColor == Color.LightCyan) &&
                _origem != null)
            {
                MostrarTabuleiro();
                ExecutaMovimento(_origem, posicao);
                MudaJogador();
                IncrementaTurno();
                _origem = null;
                if (_xeque = VerificaXeque(_jogadorAtual))
                {
                    VerificaXequeMate(_jogadorAtual);
                }
                AtualizaLabels();
            }
        }

        public Peca ExecutaMovimento(Posicao origem, Posicao destino)
        {
            // Movimenta a peça da origem para o destino, armazenando a peça capturada
            Peca peca = RetirarPeca(origem);
            peca.IncrementarQuantidadeDeMovimentos();
            Peca pecaCapturada = RetirarPeca(destino);
            ColocarPeca(peca, destino);

            // Implementação da jogada especial de Promoção
            if (peca is Peao &&
                ((peca.CorDaPeca == Cor.Branco && destino.Linha == 0) ||
                (peca.CorDaPeca == Cor.Preto && destino.Linha == 7)))
            {
                RetirarPeca(destino);
                ColocarPeca(new Rainha(this, peca.CorDaPeca), destino);
            }
            
            // Implementação da jogada especial Roque Pequeno
            if (peca is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Peca torre = RetirarPeca(new Posicao(origem.Linha, origem.Coluna + 3));
                torre.IncrementarQuantidadeDeMovimentos();
                ColocarPeca(torre, new Posicao(origem.Linha, origem.Coluna + 1));
            }

            // Implementação da jogada especial Roque Grande
            if (peca is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Peca torre = RetirarPeca(new Posicao(origem.Linha, origem.Coluna - 4));
                torre.IncrementarQuantidadeDeMovimentos();
                ColocarPeca(torre, new Posicao(origem.Linha, origem.Coluna - 1));
            }

            // Implementação da jogada especial En Passant
            if (peca is Peao && origem.Coluna != destino.Coluna && pecaCapturada == null)
            {
                Posicao posicaoPeao = new Posicao(peca.CorDaPeca == Cor.Branco ? destino.Linha + 1 : destino.Linha - 1, destino.Coluna);
                pecaCapturada = RetirarPeca(posicaoPeao);
            }

            // Incrementa contador de peças capturadas
            if (pecaCapturada != null)
            {
                _ = pecaCapturada.CorDaPeca == Cor.Branco ? _quantidadeBrancasCapturadas++ : _quantidadePretasCapturadas++;
            }

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
                    if (_pecasEmJogo[linha, coluna] is Rei && _pecasEmJogo[linha, coluna].CorDaPeca == cor)
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

            if (posicaoRei == null)
            {
                throw new System.Exception($"Não tem rei da cor: {cor} no tabuleiro");
            }

            foreach (Peca peca in PecasEmJogo(CorAdversaria(cor)))
            {
                bool[,] matriz = peca.MovimentosPossiveis();
                if (matriz[posicaoRei.Linha, posicaoRei.Coluna])
                {
                    ((Rei)_pecasEmJogo[posicaoRei.Linha, posicaoRei.Coluna]).RecebeuXeque = true;
                    return true;
                }
            }

            return false;
        }

        public bool[,] ChecarMovimentosPossiveis(Peca peca)
        {
            bool[,] posicoesPossiveis = peca.MovimentosPossiveis();
            for (int coluna = 0; coluna < Colunas; coluna++)
            {
                for (int linha = 0; linha < Linhas; linha++)
                {
                    if (posicoesPossiveis[linha, coluna])
                    {
                        _pecasEmJogo[peca.PosicaoAtual.Linha, peca.PosicaoAtual.Coluna] = null;
                        Peca pecaCapturada = AcessarPeca(linha, coluna);
                        _pecasEmJogo[linha, coluna] = peca;
                        if (VerificaXeque(peca.CorDaPeca))
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

        public bool VerificaXequeMate(Cor cor)
        {
            foreach (Peca peca in PecasEmJogo(cor))
            {
                bool[,] matriz = ChecarMovimentosPossiveis(peca);
                for (int i = 0; i < Linhas; i++)
                {
                    for (int j = 0; j < Colunas; j++)
                    {
                        if (matriz[i, j])
                        {
                            return false;
                        }
                    }
                }
            }

            string mensagem = _xeque ? $"Xeque Mate\nVencedor: {CorAdversaria(_jogadorAtual)}" : "Empate";
            MessageBox.Show(mensagem);

            if (MessageBox.Show("Deseja jogar outra partida?", "Fim de jogo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                InicializarNovoJogo();
            }
            else
            {
                // Fechar a aplicação caso o usuário não queira dispultar uma nova partida
                Application.Exit();
            }

            return true;
        }

    }
}

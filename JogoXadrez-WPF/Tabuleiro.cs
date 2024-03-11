using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace Chess
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
        private Piece[,] _pecasEmJogo;

        /// \brief Matriz contendo todas os campos do tabuleiro.
        private PictureBox[,] _pictureBoxes;

        /// \brief Posição de origem da jogada atual.
        private Posicao _origem;

        /// \brief Cor do jogador atual.
        private Color _jogadorAtual;

        /// \brief Número do turno atual.
        private int _turno;

        /// \brief Quantidade de peças brancas capturadas.
        private int _quantidadeBrancasCapturadas;

        /// \brief Quantidade de peças pretas capturadas.
        private int _quantidadePretasCapturadas;

        /** ************************************************************************
        * \brief Construtor.
        * \details Construtor da classe Tabuleiro.
        ***************************************************************************/
        public Tabuleiro()
        {
            // Inicializa os componentes
            InitializeComponent();

            // Inicializa a matriz de peças do tabuleiro
            _pecasEmJogo = new Piece[Linhas, Colunas];
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

            // Inicializa as variáveis e o layout do tabuleiro
            InicializarNovoJogo();
        }

        /** ************************************************************************
        * \brief Inicializa um novo jogo.
        * \details Função responsável por inicializar as variáveis e o layout do
        * tabuleiro para um novo jogo.
        ***************************************************************************/
        private void InicializarNovoJogo()
        {
            _xeque = false;
            _quantidadeBrancasCapturadas = 0;
            _quantidadePretasCapturadas = 0;
            _turno = 1;
            _jogadorAtual = Color.White;
            _origem = null;
            AtualizaLabels();
            InicializaTabuleiro();
        }

        /** ************************************************************************
        * \brief Muda jogador.
        * \details Função responsável por mudar o jogador atual.
        ***************************************************************************/
        private void MudaJogador()
        {
            _jogadorAtual = CorAdversaria(_jogadorAtual);
        }

        /** ************************************************************************
        * \brief Busca cor adversária.
        * \details Função responsável por retornar a cor adversária de um determinado
        * jogador.
        * \param cor Cor do jogador em que se deseja descobrir o adversário.
        * \return Cor do jogador adversário.
        ***************************************************************************/
        private Color CorAdversaria(Color cor)
        {
            return cor == Color.White ? Color.Black : Color.White;
        }

        /** ************************************************************************
        * \brief Acessa peça.
        * \details Função responsável por acessar uma peça que se encontra na posição
        * informada.
        * \param linha Linha em que a peça se encontra.
        * \param coluna Coluna em que a peça se encontra.
        * \return Peça que está na posição informada.
        ***************************************************************************/
        public Piece AcessarPeca(int linha, int coluna)
        {
            return _pecasEmJogo[linha, coluna];
        }

        /** ************************************************************************
        * \brief Acessa peça.
        * \details Função responsável por acessar uma peça que se encontra na posição
        * informada.
        * \param posicao Posição em que a queremos acessar.
        * \return Peça que está na posição informada.
        ***************************************************************************/
        public Piece AcessarPeca(Posicao posicao)
        {
            return _pecasEmJogo[posicao.Linha, posicao.Coluna];
        }

        /** ************************************************************************
        * \brief Colocar peça.
        * \details Função responsável por colocar uma peça em uma determinada 
        * posição.
        * \param peca Peça a ser colocada.
        * \param posicao Posição onde a peça será colocada.
        ***************************************************************************/
        public void ColocarPeca(Piece peca, Posicao posicao)
        {
            _pecasEmJogo[posicao.Linha, posicao.Coluna] = peca;
            peca.PosicaoAtual = posicao;
            _pictureBoxes[posicao.Linha, posicao.Coluna].Image = peca.MostrarImagem();
        }

        /** ************************************************************************
        * \brief Inicializa tabuleiro.
        * \details Função responsável por inicializar o tabuleiro, colocando as peças
        * pretas e brancas em suas posições iniciais.
        ***************************************************************************/
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
            ColocarPeca(new Rook(this, Color.White), new Posicao(7, 0));
            ColocarPeca(new Knight(this, Color.White), new Posicao(7, 1));
            ColocarPeca(new Bishop(this, Color.White), new Posicao(7, 2));
            ColocarPeca(new Queen(this, Color.White), new Posicao(7, 3));
            ColocarPeca(new King(this, Color.White), new Posicao(7, 4));
            ColocarPeca(new Bishop(this, Color.White), new Posicao(7, 5));
            ColocarPeca(new Knight(this, Color.White), new Posicao(7, 6));
            ColocarPeca(new Rook(this, Color.White), new Posicao(7, 7));
            ColocarPeca(new Pawn(this, Color.White), new Posicao(6, 7));
            ColocarPeca(new Pawn(this, Color.White), new Posicao(6, 6));
            ColocarPeca(new Pawn(this, Color.White), new Posicao(6, 5));
            ColocarPeca(new Pawn(this, Color.White), new Posicao(6, 4));
            ColocarPeca(new Pawn(this, Color.White), new Posicao(6, 3));
            ColocarPeca(new Pawn(this, Color.White), new Posicao(6, 2));
            ColocarPeca(new Pawn(this, Color.White), new Posicao(6, 1));
            ColocarPeca(new Pawn(this, Color.White), new Posicao(6, 0));

            // Coloca as peças pretas
            ColocarPeca(new Rook(this, Color.Black), new Posicao(0, 0));
            ColocarPeca(new Knight(this, Color.Black), new Posicao(0, 1));
            ColocarPeca(new Bishop(this, Color.Black), new Posicao(0, 2));
            ColocarPeca(new Queen(this, Color.Black), new Posicao(0, 3));
            ColocarPeca(new King(this, Color.Black), new Posicao(0, 4));
            ColocarPeca(new Bishop(this, Color.Black), new Posicao(0, 5));
            ColocarPeca(new Knight(this, Color.Black), new Posicao(0, 6));
            ColocarPeca(new Rook(this, Color.Black), new Posicao(0, 7));
            ColocarPeca(new Pawn(this, Color.Black), new Posicao(1, 7));
            ColocarPeca(new Pawn(this, Color.Black), new Posicao(1, 6));
            ColocarPeca(new Pawn(this, Color.Black), new Posicao(1, 5));
            ColocarPeca(new Pawn(this, Color.Black), new Posicao(1, 4));
            ColocarPeca(new Pawn(this, Color.Black), new Posicao(1, 3));
            ColocarPeca(new Pawn(this, Color.Black), new Posicao(1, 2));
            ColocarPeca(new Pawn(this, Color.Black), new Posicao(1, 1));
            ColocarPeca(new Pawn(this, Color.Black), new Posicao(1, 0));
        }

        /** ************************************************************************
        * \brief Atualiza o label de xeque.
        * \details Função responsável por atualizar o label de xeque, indicando se o 
        * jogador atual está ou não em xeque.
        ***************************************************************************/
        private void AtualizaLabelXeque()
        {
            labelXeque.Text = _xeque ? "Você está em Xeque" : " ";
        }

        /** ************************************************************************
        * \brief Atualiza o label do jogador.
        * \details Função responsável por atualizar o label do jogador, indicando a
        * cor do jogador atual.
        ***************************************************************************/
        private void AtualizaLabelJogador()
        {
            labelJogadorAtual.Text = "Jogador atual: " + (_jogadorAtual == Color.White ? "Branco" : "Preto");
        }

        /** ************************************************************************
        * \brief Atualiza o label de turno.
        * \details Função responsável por atualizar o label de turno, indicando o 
        * número do turno atual.
        ***************************************************************************/
        private void AtualizaLabelTurno()
        {
            labelTurno.Text = $"Turno: {_turno}";
        }

        /** ************************************************************************
        * \brief Atualiza o label de peças capturadas.
        * \details Função responsável por atualizar o label de peças capturadas, 
        * indicando o número de peças capturadas por cada jogador.
        ***************************************************************************/
        private void AtualizaLabelsPecasCapturadas()
        {
            labelPecasCapturadasBranco.Text = $"Peças capturadas: {_quantidadePretasCapturadas}";
            labelPecasCapturadasPreto.Text = $"Peças capturadas: {_quantidadeBrancasCapturadas}";
        }

        /** ************************************************************************
        * \brief Atualiza o labels.
        * \details Função responsável por atualizar todos os labels do tabuleiro.
        ***************************************************************************/
        private void AtualizaLabels()
        {
            AtualizaLabelJogador();
            AtualizaLabelTurno();
            AtualizaLabelsPecasCapturadas();
            AtualizaLabelXeque();
        }

        /** ************************************************************************
        * \brief Incrementa turno.
        * \details Função responsável por incrementar o número do turno atual.
        ***************************************************************************/
        private void IncrementaTurno()
        {
            _turno++;
        }


        /** ************************************************************************
        * \brief Mostra tabuleiro.
        * \details Função responsável por imprimir o tabuleiro de jogo.
        ***************************************************************************/
        private void MostrarTabuleiro()
        {
            System.Drawing.Color color;

            // Imprime o tabuleiro com as cores iniciais
            for (int linha = 0; linha < Linhas; linha++)
            {
                color = (linha % 2 == 0) ? System.Drawing.Color.Gray : System.Drawing.Color.White;
                for (int coluna = 0; coluna < Colunas; coluna++)
                {
                    _pictureBoxes[linha, coluna].BackColor = color;
                    color = (color == System.Drawing.Color.White) ? System.Drawing.Color.Gray : System.Drawing.Color.White;
                }
            }
        }

        /** ************************************************************************
        * \brief Mostra tabuleiro.
        * \details Função responsável por imprimir o tabuleiro de jogo, destacando
        * as posíveis movimentações de uma peça.
        * \param posicoesPossiveis Posições possíveis que a peça pode assumir e, 
        * portanto, serão destacadas.
        ***************************************************************************/
        private void MostrarTabuleiro(bool[,] posicoesPossiveis)
        {
            System.Drawing.Color color;

            // Imprimir o tabuleiro nas cores iniciais
            MostrarTabuleiro();

            // Imprimir tabuleiro destacando as possíveis jogadas
            for (int linha = 0; linha < Linhas; linha++)
            {
                color = (linha % 2 == 0) ? System.Drawing.Color.Gray : System.Drawing.Color.White;
                for (int coluna = 0; coluna < Colunas; coluna++)
                {
                    _pictureBoxes[linha, coluna].BackColor = posicoesPossiveis[linha, coluna] ?
                        (_pictureBoxes[linha, coluna].BackColor == System.Drawing.Color.White ? System.Drawing.Color.LightCyan : System.Drawing.Color.LightBlue) : color;
                    color = color == System.Drawing.Color.White ? System.Drawing.Color.Gray : System.Drawing.Color.White;
                }
            }
        }

        /** ************************************************************************
        * \brief Retira peça.
        * \details Função responsável por retirar uma peça de uma determinada posição.
        * \param posicao Posição na qual a peça será retirada.
        * \return Peça retirada da posição informada.
        ***************************************************************************/
        public Piece RetirarPeca(Posicao posicao)
        {
            // Verifica se existe peça na posição informada
            if (!ExistePeca(posicao))
            {
                return null;
            }

            // Retira a peça da posição informada
            Piece aux = AcessarPeca(posicao);
            aux.PosicaoAtual = null;

            // Limpa as informações referentes ao campo localizado nesta posição
            _pecasEmJogo[posicao.Linha, posicao.Coluna] = null;
            _pictureBoxes[posicao.Linha, posicao.Coluna].Image = null;

            // Retorna a peça retirada
            return aux;
        }

        /** ************************************************************************
        * \brief Veridica se existe peça.
        * \details Função responsável por verificar se existe peça em uma determinada
        * posição.
        * \param posicao Posição na qual será verificado se existe uma peça.
        * \return 'true' caso exista uma peça na posição informada ou 'false' caso 
        * não exista.
        ***************************************************************************/
        public bool ExistePeca(Posicao posicao)
        {
            return AcessarPeca(posicao) != null;
        }

        /** ************************************************************************
        * \brief Processa o click sobre um campo.
        * \details Função responsável por processar um click sobre um determinado
        * 'PictureBox'.
        * \param posicao Posição do 'PictureBox' que foi clicado.
        ***************************************************************************/
        private void ProcessaPictureBoxClick(Posicao posicao)
        {
            // Checa se a origem não é nula e a posição de destino é igual a de origem
            if (_origem != null && _origem.CompareTo(posicao) == 0)
            {
                MostrarTabuleiro();
                _origem = null;
                return;
            }

            // Verifica se existe peça na posição informada
            if (ExistePeca(posicao))
            {
                Piece peca = AcessarPeca(posicao);

                // Se a peça for do jogado atual, essa será a origem da jogada
                if (peca.CorDaPeca == _jogadorAtual)
                {
                    _origem = posicao;
                    MostrarTabuleiro(ChecarMovimentosPossiveis(peca));
                    return;
                }
            }

            // Se o campo for uma posição posível de uma peça e a origem não for nula, a jogada pode ser realizada
            if ((_pictureBoxes[posicao.Linha, posicao.Coluna].BackColor == System.Drawing.Color.LightBlue ||
                _pictureBoxes[posicao.Linha, posicao.Coluna].BackColor == System.Drawing.Color.LightCyan) &&
                _origem != null)
            {
                MostrarTabuleiro();
                ExecutaMovimento(_origem, posicao);
                MudaJogador();
                IncrementaTurno();
                _origem = null;
                VerificaFimDeJogo();
                AtualizaLabels();
            }
        }

        /** ************************************************************************
        * \brief Verifica fim de jogo.
        * \details Função responsável por verificar se ocorreu algum evento de fim
        * de jogo.
        * \return 'true' se o jogo estiver terminado, 'false' se não.
        ***************************************************************************/
        public bool VerificaFimDeJogo()
        {
            bool ret;

            // Verifica xeque mate
            _xeque = VerificaXeque(_jogadorAtual);
            ret = _xeque ? VerificaXequeMate(_jogadorAtual) : false;

            // Verifica empate
            ret = ret ? true : !ret && (!CorTemOndeMover() || (QuantidadeDePecasEmJogo() <= 2));

            // Chama a função de fim de jogo caso nescessário
            if(ret)
            {
                FimDoJogo();
            }

            return ret;
        }

        /** ************************************************************************
        * \brief Executa o movimento da peça.
        * \details Função responsável por executar a movimentação de uma peça.
        * \param origem Posição de origem da peça.
        * \param destino Posição de destino da peça.
        * \return Peça que se encontrava na posição de destino, que foi capturada
        * pelo jogador.
        ***************************************************************************/
        public Piece ExecutaMovimento(Posicao origem, Posicao destino)
        {
            // Movimenta a peça da origem para o destino, armazenando a peça capturada
            Piece peca = RetirarPeca(origem);
            peca.IncrementarQuantidadeDeMovimentos();
            Piece pecaCapturada = RetirarPeca(destino);
            ColocarPeca(peca, destino);

            // Implementação da jogada especial de Promoção
            if (peca is Pawn &&
                ((peca.CorDaPeca == Color.White && destino.Linha == 0) ||
                (peca.CorDaPeca == Color.Black && destino.Linha == 7)))
            {
                RetirarPeca(destino);
                ColocarPeca(new Queen(this, peca.CorDaPeca), destino);
            }
            
            // Implementação da jogada especial Roque Pequeno
            if (peca is King && destino.Coluna == origem.Coluna + 2)
            {
                Piece torre = RetirarPeca(new Posicao(origem.Linha, origem.Coluna + 3));
                torre.IncrementarQuantidadeDeMovimentos();
                ColocarPeca(torre, new Posicao(origem.Linha, origem.Coluna + 1));
            }

            // Implementação da jogada especial Roque Grande
            if (peca is King && destino.Coluna == origem.Coluna - 2)
            {
                Piece torre = RetirarPeca(new Posicao(origem.Linha, origem.Coluna - 4));
                torre.IncrementarQuantidadeDeMovimentos();
                ColocarPeca(torre, new Posicao(origem.Linha, origem.Coluna - 1));
            }

            // Implementação da jogada especial En Passant
            if (peca is Pawn && origem.Coluna != destino.Coluna && pecaCapturada == null)
            {
                Posicao posicaoPeao = new Posicao(peca.CorDaPeca == Color.White ? destino.Linha + 1 : destino.Linha - 1, destino.Coluna);
                pecaCapturada = RetirarPeca(posicaoPeao);
            }

            // Incrementa contador de peças capturadas
            if (pecaCapturada != null)
            {
                _ = pecaCapturada.CorDaPeca == Color.White ? _quantidadeBrancasCapturadas++ : _quantidadePretasCapturadas++;
            }

            return pecaCapturada;
        }

        /** ************************************************************************
        * \brief Lista as peças que estão em jogo.
        * \details Função responsável por informar quais são as peças em jogo de um
        * determinado jogador.
        * \param cor Cor do jogador em que se deseja saber as peças que estão em jogo.
        * \return Um conjunto de peças do jogador informado.
        ***************************************************************************/
        public HashSet<Piece> PecasEmJogo(Color cor)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece peca in _pecasEmJogo)
            {
                if (peca != null && peca.CorDaPeca == cor)
                {
                    aux.Add(peca);
                }
            }
            return aux;
        }

        /** ************************************************************************
        * \brief Informa a quantidade de peças em jogo.
        * \details Função responsável por informar a quantidade de peças em jogo.
        * \return Quantidade de peças em jogo.
        ***************************************************************************/
        public int QuantidadeDePecasEmJogo()
        {
            return PecasEmJogo(Color.White).Count + PecasEmJogo(Color.Black).Count;
        }

        /** ************************************************************************
        * \brief Pega a posição do rei.
        * \details Função responsável por informar qual a posição do rei de uma 
        * determinada cor.
        * \param cor Cor do jogador no qual se deseja saber em que posição está o rei.
        * \return Posição do rei do jogador informado.
        ***************************************************************************/
        private Posicao PegarPosicaoRei(Color cor)
        {
            for (int coluna = 0; coluna < Colunas; coluna++)
            {
                for (int linha = 0; linha < Linhas; linha++)
                {
                    if (_pecasEmJogo[linha, coluna] is King && _pecasEmJogo[linha, coluna].CorDaPeca == cor)
                    {
                        return new Posicao(linha, coluna);
                    }
                }
            }
            return null; // Nunca deve acontecer
        }

        /** ************************************************************************
        * \brief Verifica se o jogador está em xeque.
        * \details Função responsável por verificar se o jogador de uma determinada 
        * cor está em xeque.
        * \param cor Cor do jogador no qual se deseja saber se está em xeque.
        * \return 'true' se o jogador estiver em xeque, 'false' se não estiver.
        * \exception System.Exception Lançada quando o rei não estiver no tabuleiro.
        ***************************************************************************/
        public bool VerificaXeque(Color cor)
        {
            Posicao posicaoRei = PegarPosicaoRei(cor);

            if (posicaoRei == null)
            {
                throw new System.Exception($"Não tem rei da cor: {cor} no tabuleiro");
            }

            foreach (Piece peca in PecasEmJogo(CorAdversaria(cor)))
            {
                bool[,] matriz = peca.MovimentosPossiveis();
                if (matriz[posicaoRei.Linha, posicaoRei.Coluna])
                {
                    ((King)_pecasEmJogo[posicaoRei.Linha, posicaoRei.Coluna]).RecebeuXeque = true;
                    return true;
                }
            }

            return false;
        }

        /** ************************************************************************
        * \brief Checa as posíveis movimentações de uma peça.
        * \details Função responsável por checar e listar as posíveis movimentações
        * de uma determinada peça.
        * \param peca Peça a ser analisada.
        * \return Matriz de booleanos indicando quais posições uma peça pode assumir.
        ***************************************************************************/
        public bool[,] ChecarMovimentosPossiveis(Piece peca)
        {
            bool[,] posicoesPossiveis = peca.MovimentosPossiveis();
            for (int coluna = 0; coluna < Colunas; coluna++)
            {
                for (int linha = 0; linha < Linhas; linha++)
                {
                    if (posicoesPossiveis[linha, coluna])
                    {
                        _pecasEmJogo[peca.PosicaoAtual.Linha, peca.PosicaoAtual.Coluna] = null;
                        Piece pecaCapturada = AcessarPeca(linha, coluna);
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

        /** ************************************************************************
        * \brief Verifica xeque mate.
        * \details Função responsável por verificar se um determinado jogador está 
        * em xeque mate.
        * \param cor Cor do jogador a ser verificado.
        * \return 'true' se o jogador estiver em xeque mate, 'false' se não estiver.
        ***************************************************************************/
        public bool VerificaXequeMate(Color cor)
        {
            foreach (Piece peca in PecasEmJogo(cor))
            {
                bool[,] matriz = ChecarMovimentosPossiveis(peca);
                for (int linha = 0; linha < Linhas; linha++)
                {
                    for (int coluna = 0; coluna < Colunas; coluna++)
                    {
                        if (matriz[linha, coluna])
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        /** ************************************************************************
        * \brief Fim de jogo.
        * \details Função responsável por processar o fim de jogo, apresentando o 
        * vencedor ou o empate, além de perguntar se o jogador deseja iniciar um
        * novo jogo.
        ***************************************************************************/
        public void FimDoJogo()
        {
            // Mostra o vencedor
            string mensagem = _xeque ? $"Xeque Mate\nVencedor: {CorAdversaria(_jogadorAtual)}" : "Empate";
            MessageBox.Show(mensagem);

            if (MessageBox.Show("Deseja jogar outra partida?", "Fim de jogo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) 
                == DialogResult.Yes)
            {
                InicializarNovoJogo();
            }
            else
            {
                // Fechar a aplicação caso o usuário não queira dispultar uma nova partida
                Application.Exit();
            }
        }

        /** ************************************************************************
        * \brief Verifica se um jogador tem onde mover.
        * \details Função responsável por verificar se um jogador tem algum movimento
        * possível.
        * \return 'true' se o jogador possuir movimentos possíveis, 'false' se não
        * possuir.
        ***************************************************************************/
        public bool CorTemOndeMover()
        {
            foreach(Piece peca in PecasEmJogo(_jogadorAtual))
            {
                foreach (bool posicao in ChecarMovimentosPossiveis(peca))
                {
                    if(posicao)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}

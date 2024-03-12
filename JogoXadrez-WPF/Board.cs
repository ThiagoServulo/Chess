using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace Chess
{
    /** ************************************************************************
    * \brief Informações sobre o board.
    * \details A classe Tabuleiro armazena as informações referentes ao board 
    * do jogo, ou seja, onde as peças serão colocadas.
    * \author Thiago Sérvulo Guimarães - thiago.servulo@sga.pucminas.br
    * \date 20/09/2022
    * \version v1.0.0
    ***************************************************************************/
    partial class Board : Form
    {
        /// \brief Quantidade de linhas existentes no board.
        public int Rows = 8;

        /// \brief Quantidade de colunas existentes no board.
        public int Columns = 8;

        /// \brief Indica se a partida está em xeque.
        public bool _check;

        /// \brief Matriz contendo todas as peças que estão em jogo.
        private Piece[,] _piecesInPlay;

        /// \brief Matriz contendo todas os campos do board.
        private PictureBox[,] _pictureBoxes;

        /// \brief Posição de origem da jogada atual.
        private Position _origin;

        /// \brief Cor do jogador atual.
        private Color _currentPlayer;

        /// \brief Número do turno atual.
        private int _turn;

        /// \brief Quantidade de peças brancas capturadas.
        private int _whiteCapturedQuantity;

        /// \brief Quantidade de peças pretas capturadas.
        private int _blackCapturedQuantity;

        /** ************************************************************************
        * \brief Construtor.
        * \details Construtor da classe Tabuleiro.
        ***************************************************************************/
        public Board()
        {
            // Inicializa os componentes
            InitializeComponent();

            // Inicializa a matrix de peças do board
            _piecesInPlay = new Piece[Rows, Columns];
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

            // Inicializa as variáveis e o layout do board
            InicializarNovoJogo();
        }

        /** ************************************************************************
        * \brief Inicializa um novo jogo.
        * \details Função responsável por inicializar as variáveis e o layout do
        * board para um novo jogo.
        ***************************************************************************/
        private void InicializarNovoJogo()
        {
            _check = false;
            _whiteCapturedQuantity = 0;
            _blackCapturedQuantity = 0;
            _turn = 1;
            _currentPlayer = Color.White;
            _origin = null;
            AtualizaLabels();
            InicializaTabuleiro();
        }

        /** ************************************************************************
        * \brief Muda jogador.
        * \details Função responsável por mudar o jogador atual.
        ***************************************************************************/
        private void MudaJogador()
        {
            _currentPlayer = CorAdversaria(_currentPlayer);
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
        * \param linha Row em que a peça se encontra.
        * \param coluna Column em que a peça se encontra.
        * \return Peça que está na posição informada.
        ***************************************************************************/
        public Piece GetPiece(int linha, int coluna)
        {
            return _piecesInPlay[linha, coluna];
        }

        /** ************************************************************************
        * \brief Acessa peça.
        * \details Função responsável por acessar uma peça que se encontra na posição
        * informada.
        * \param position Posição em que a queremos acessar.
        * \return Peça que está na posição informada.
        ***************************************************************************/
        public Piece GetPiece(Position position)
        {
            return _piecesInPlay[position.Row, position.Column];
        }

        /** ************************************************************************
        * \brief Colocar peça.
        * \details Função responsável por colocar uma peça em uma determinada 
        * posição.
        * \param peca Peça a ser colocada.
        * \param position Posição onde a peça será colocada.
        ***************************************************************************/
        public void ColocarPeca(Piece peca, Position position)
        {
            _piecesInPlay[position.Row, position.Column] = peca;
            peca.CurrentPosition = position;
            _pictureBoxes[position.Row, position.Column].Image = peca.MostrarImagem();
        }

        /** ************************************************************************
        * \brief Inicializa board.
        * \details Função responsável por inicializar o board, colocando as peças
        * pretas e brancas em suas posições iniciais.
        ***************************************************************************/
        private void InicializaTabuleiro()
        {
            // Inicializa layout board
            MostrarTabuleiro();

            // Resetando a imagem de todos os Picture Boxes
            for (int linha = 0; linha < Rows; linha++)
            {
                for (int coluna = 0; coluna < Columns; coluna++)
                {
                    _piecesInPlay[linha, coluna] = null;
                    _pictureBoxes[linha, coluna].Image = null;
                }
            }

            // Coloca as peças brancas
            ColocarPeca(new Rook(this, Color.White), new Position(7, 0));
            ColocarPeca(new Knight(this, Color.White), new Position(7, 1));
            ColocarPeca(new Bishop(this, Color.White), new Position(7, 2));
            ColocarPeca(new Queen(this, Color.White), new Position(7, 3));
            ColocarPeca(new King(this, Color.White), new Position(7, 4));
            ColocarPeca(new Bishop(this, Color.White), new Position(7, 5));
            ColocarPeca(new Knight(this, Color.White), new Position(7, 6));
            ColocarPeca(new Rook(this, Color.White), new Position(7, 7));
            ColocarPeca(new Pawn(this, Color.White), new Position(6, 7));
            ColocarPeca(new Pawn(this, Color.White), new Position(6, 6));
            ColocarPeca(new Pawn(this, Color.White), new Position(6, 5));
            ColocarPeca(new Pawn(this, Color.White), new Position(6, 4));
            ColocarPeca(new Pawn(this, Color.White), new Position(6, 3));
            ColocarPeca(new Pawn(this, Color.White), new Position(6, 2));
            ColocarPeca(new Pawn(this, Color.White), new Position(6, 1));
            ColocarPeca(new Pawn(this, Color.White), new Position(6, 0));

            // Coloca as peças pretas
            ColocarPeca(new Rook(this, Color.Black), new Position(0, 0));
            ColocarPeca(new Knight(this, Color.Black), new Position(0, 1));
            ColocarPeca(new Bishop(this, Color.Black), new Position(0, 2));
            ColocarPeca(new Queen(this, Color.Black), new Position(0, 3));
            ColocarPeca(new King(this, Color.Black), new Position(0, 4));
            ColocarPeca(new Bishop(this, Color.Black), new Position(0, 5));
            ColocarPeca(new Knight(this, Color.Black), new Position(0, 6));
            ColocarPeca(new Rook(this, Color.Black), new Position(0, 7));
            ColocarPeca(new Pawn(this, Color.Black), new Position(1, 7));
            ColocarPeca(new Pawn(this, Color.Black), new Position(1, 6));
            ColocarPeca(new Pawn(this, Color.Black), new Position(1, 5));
            ColocarPeca(new Pawn(this, Color.Black), new Position(1, 4));
            ColocarPeca(new Pawn(this, Color.Black), new Position(1, 3));
            ColocarPeca(new Pawn(this, Color.Black), new Position(1, 2));
            ColocarPeca(new Pawn(this, Color.Black), new Position(1, 1));
            ColocarPeca(new Pawn(this, Color.Black), new Position(1, 0));
        }

        /** ************************************************************************
        * \brief Atualiza o label de xeque.
        * \details Função responsável por atualizar o label de xeque, indicando se o 
        * jogador atual está ou não em xeque.
        ***************************************************************************/
        private void AtualizaLabelXeque()
        {
            labelXeque.Text = _check ? "Você está em Xeque" : " ";
        }

        /** ************************************************************************
        * \brief Atualiza o label do jogador.
        * \details Função responsável por atualizar o label do jogador, indicando a
        * cor do jogador atual.
        ***************************************************************************/
        private void AtualizaLabelJogador()
        {
            labelJogadorAtual.Text = "Jogador atual: " + (_currentPlayer == Color.White ? "Branco" : "Preto");
        }

        /** ************************************************************************
        * \brief Atualiza o label de turno.
        * \details Função responsável por atualizar o label de turno, indicando o 
        * número do turno atual.
        ***************************************************************************/
        private void AtualizaLabelTurno()
        {
            labelTurno.Text = $"Turno: {_turn}";
        }

        /** ************************************************************************
        * \brief Atualiza o label de peças capturadas.
        * \details Função responsável por atualizar o label de peças capturadas, 
        * indicando o número de peças capturadas por cada jogador.
        ***************************************************************************/
        private void AtualizaLabelsPecasCapturadas()
        {
            labelPecasCapturadasBranco.Text = $"Peças capturadas: {_blackCapturedQuantity}";
            labelPecasCapturadasPreto.Text = $"Peças capturadas: {_whiteCapturedQuantity}";
        }

        /** ************************************************************************
        * \brief Atualiza o labels.
        * \details Função responsável por atualizar todos os labels do board.
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
            _turn++;
        }


        /** ************************************************************************
        * \brief Mostra board.
        * \details Função responsável por imprimir o board de jogo.
        ***************************************************************************/
        private void MostrarTabuleiro()
        {
            System.Drawing.Color color;

            // Imprime o board com as cores iniciais
            for (int linha = 0; linha < Rows; linha++)
            {
                color = (linha % 2 == 0) ? System.Drawing.Color.Gray : System.Drawing.Color.White;
                for (int coluna = 0; coluna < Columns; coluna++)
                {
                    _pictureBoxes[linha, coluna].BackColor = color;
                    color = (color == System.Drawing.Color.White) ? System.Drawing.Color.Gray : System.Drawing.Color.White;
                }
            }
        }

        /** ************************************************************************
        * \brief Mostra board.
        * \details Função responsável por imprimir o board de jogo, destacando
        * as posíveis movimentações de uma peça.
        * \param posicoesPossiveis Posições possíveis que a peça pode assumir e, 
        * portanto, serão destacadas.
        ***************************************************************************/
        private void MostrarTabuleiro(bool[,] posicoesPossiveis)
        {
            System.Drawing.Color color;

            // Imprimir o board nas cores iniciais
            MostrarTabuleiro();

            // Imprimir board destacando as possíveis jogadas
            for (int linha = 0; linha < Rows; linha++)
            {
                color = (linha % 2 == 0) ? System.Drawing.Color.Gray : System.Drawing.Color.White;
                for (int coluna = 0; coluna < Columns; coluna++)
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
        * \param position Posição na qual a peça será retirada.
        * \return Peça retirada da posição informada.
        ***************************************************************************/
        public Piece RetirarPeca(Position position)
        {
            // Verifica se existe peça na posição informada
            if (!ExistePeca(position))
            {
                return null;
            }

            // Retira a peça da posição informada
            Piece aux = GetPiece(position);
            aux.CurrentPosition = null;

            // Limpa as informações referentes ao campo localizado nesta posição
            _piecesInPlay[position.Row, position.Column] = null;
            _pictureBoxes[position.Row, position.Column].Image = null;

            // Retorna a peça retirada
            return aux;
        }

        /** ************************************************************************
        * \brief Veridica se existe peça.
        * \details Função responsável por verificar se existe peça em uma determinada
        * posição.
        * \param position Posição na qual será verificado se existe uma peça.
        * \return 'true' caso exista uma peça na posição informada ou 'false' caso 
        * não exista.
        ***************************************************************************/
        public bool ExistePeca(Position position)
        {
            return GetPiece(position) != null;
        }

        /** ************************************************************************
        * \brief Processa o click sobre um campo.
        * \details Função responsável por processar um click sobre um determinado
        * 'PictureBox'.
        * \param position Posição do 'PictureBox' que foi clicado.
        ***************************************************************************/
        private void ProcessaPictureBoxClick(Position position)
        {
            // Checa se a origem não é nula e a posição de destino é igual a de origem
            if (_origin != null && _origin.CompareTo(position) == 0)
            {
                MostrarTabuleiro();
                _origin = null;
                return;
            }

            // Verifica se existe peça na posição informada
            if (ExistePeca(position))
            {
                Piece peca = GetPiece(position);

                // Se a peça for do jogado atual, essa será a origem da jogada
                if (peca.PieceColor == _currentPlayer)
                {
                    _origin = position;
                    MostrarTabuleiro(ChecarMovimentosPossiveis(peca));
                    return;
                }
            }

            // Se o campo for uma posição posível de uma peça e a origem não for nula, a jogada pode ser realizada
            if ((_pictureBoxes[position.Row, position.Column].BackColor == System.Drawing.Color.LightBlue ||
                _pictureBoxes[position.Row, position.Column].BackColor == System.Drawing.Color.LightCyan) &&
                _origin != null)
            {
                MostrarTabuleiro();
                ExecutaMovimento(_origin, position);
                MudaJogador();
                IncrementaTurno();
                _origin = null;
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
            _check = VerificaXeque(_currentPlayer);
            ret = _check ? VerificaXequeMate(_currentPlayer) : false;

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
        public Piece ExecutaMovimento(Position origem, Position destino)
        {
            // Movimenta a peça da origem para o destino, armazenando a peça capturada
            Piece peca = RetirarPeca(origem);
            peca.IncrementNumberOfMoves();
            Piece pecaCapturada = RetirarPeca(destino);
            ColocarPeca(peca, destino);

            // Implementação da jogada especial de Promoção
            if (peca is Pawn &&
                ((peca.PieceColor == Color.White && destino.Row == 0) ||
                (peca.PieceColor == Color.Black && destino.Row == 7)))
            {
                RetirarPeca(destino);
                ColocarPeca(new Queen(this, peca.PieceColor), destino);
            }
            
            // Implementação da jogada especial Roque Pequeno
            if (peca is King && destino.Column == origem.Column + 2)
            {
                Piece torre = RetirarPeca(new Position(origem.Row, origem.Column + 3));
                torre.IncrementNumberOfMoves();
                ColocarPeca(torre, new Position(origem.Row, origem.Column + 1));
            }

            // Implementação da jogada especial Roque Grande
            if (peca is King && destino.Column == origem.Column - 2)
            {
                Piece torre = RetirarPeca(new Position(origem.Row, origem.Column - 4));
                torre.IncrementNumberOfMoves();
                ColocarPeca(torre, new Position(origem.Row, origem.Column - 1));
            }

            // Implementação da jogada especial En Passant
            if (peca is Pawn && origem.Column != destino.Column && pecaCapturada == null)
            {
                Position posicaoPeao = new Position(peca.PieceColor == Color.White ? destino.Row + 1 : destino.Row - 1, destino.Column);
                pecaCapturada = RetirarPeca(posicaoPeao);
            }

            // Incrementa contador de peças capturadas
            if (pecaCapturada != null)
            {
                _ = pecaCapturada.PieceColor == Color.White ? _whiteCapturedQuantity++ : _blackCapturedQuantity++;
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
            foreach (Piece peca in _piecesInPlay)
            {
                if (peca != null && peca.PieceColor == cor)
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
        private Position PegarPosicaoRei(Color cor)
        {
            for (int coluna = 0; coluna < Columns; coluna++)
            {
                for (int linha = 0; linha < Rows; linha++)
                {
                    if (_piecesInPlay[linha, coluna] is King && _piecesInPlay[linha, coluna].PieceColor == cor)
                    {
                        return new Position(linha, coluna);
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
        * \exception System.Exception Lançada quando o rei não estiver no board.
        ***************************************************************************/
        public bool VerificaXeque(Color cor)
        {
            Position posicaoRei = PegarPosicaoRei(cor);

            if (posicaoRei == null)
            {
                throw new System.Exception($"Não tem rei da cor: {cor} no board");
            }

            foreach (Piece peca in PecasEmJogo(CorAdversaria(cor)))
            {
                bool[,] matrix = peca.PossibleMoves();
                if (matrix[posicaoRei.Row, posicaoRei.Column])
                {
                    ((King)_piecesInPlay[posicaoRei.Row, posicaoRei.Column]).ReceivedCheck = true;
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
            bool[,] posicoesPossiveis = peca.PossibleMoves();
            for (int coluna = 0; coluna < Columns; coluna++)
            {
                for (int linha = 0; linha < Rows; linha++)
                {
                    if (posicoesPossiveis[linha, coluna])
                    {
                        _piecesInPlay[peca.CurrentPosition.Row, peca.CurrentPosition.Column] = null;
                        Piece pecaCapturada = GetPiece(linha, coluna);
                        _piecesInPlay[linha, coluna] = peca;
                        if (VerificaXeque(peca.PieceColor))
                        {
                            posicoesPossiveis[linha, coluna] = false;
                        }
                        _piecesInPlay[linha, coluna] = pecaCapturada;
                        _piecesInPlay[peca.CurrentPosition.Row, peca.CurrentPosition.Column] = peca;
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
                bool[,] matrix = ChecarMovimentosPossiveis(peca);
                for (int linha = 0; linha < Rows; linha++)
                {
                    for (int coluna = 0; coluna < Columns; coluna++)
                    {
                        if (matrix[linha, coluna])
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
            string mensagem = _check ? $"Xeque Mate\nVencedor: {CorAdversaria(_currentPlayer)}" : "Empate";
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
            foreach(Piece peca in PecasEmJogo(_currentPlayer))
            {
                foreach (bool position in ChecarMovimentosPossiveis(peca))
                {
                    if(position)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}

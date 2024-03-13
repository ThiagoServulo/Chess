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
            InitializeNewGame();
        }

        /** ************************************************************************
        * \brief Inicializa um novo jogo.
        * \details Função responsável por inicializar as variáveis e o layout do
        * board para um novo jogo.
        ***************************************************************************/
        private void InitializeNewGame()
        {
            _check = false;
            _whiteCapturedQuantity = 0;
            _blackCapturedQuantity = 0;
            _turn = 1;
            _currentPlayer = Color.White;
            _origin = null;
            UpdateLabels();
            InitializeBoard();
        }

        /** ************************************************************************
        * \brief Muda jogador.
        * \details Função responsável por mudar o jogador atual.
        ***************************************************************************/
        private void SwitchPlayer()
        {
            _currentPlayer = OpponentColor(_currentPlayer);
        }

        /** ************************************************************************
        * \brief Busca color adversária.
        * \details Função responsável por retornar a color adversária de um determinado
        * jogador.
        * \param color Cor do jogador em que se deseja descobrir o adversário.
        * \return Cor do jogador adversário.
        ***************************************************************************/
        private Color OpponentColor(Color color)
        {
            return color == Color.White ? Color.Black : Color.White;
        }

        /** ************************************************************************
        * \brief Acessa peça.
        * \details Função responsável por acessar uma peça que se encontra na posição
        * informada.
        * \param row Row em que a peça se encontra.
        * \param column Column em que a peça se encontra.
        * \return Peça que está na posição informada.
        ***************************************************************************/
        public Piece GetPiece(int row, int column)
        {
            return _piecesInPlay[row, column];
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
        * \param piece Peça a ser colocada.
        * \param position Posição onde a peça será colocada.
        ***************************************************************************/
        public void PlacePiece(Piece piece, Position position)
        {
            _piecesInPlay[position.Row, position.Column] = piece;
            piece.CurrentPosition = position;
            _pictureBoxes[position.Row, position.Column].Image = piece.ShowImage();
        }

        /** ************************************************************************
        * \brief Inicializa board.
        * \details Função responsável por inicializar o board, colocando as peças
        * pretas e brancas em suas posições iniciais.
        ***************************************************************************/
        private void InitializeBoard()
        {
            // Inicializa layout board
            ShowBoard();

            // Resetando a imagem de todos os Picture Boxes
            for (int row = 0; row < Rows; row++)
            {
                for (int column = 0; column < Columns; column++)
                {
                    _piecesInPlay[row, column] = null;
                    _pictureBoxes[row, column].Image = null;
                }
            }

            // Coloca as peças brancas
            PlacePiece(new Rook(this, Color.White), new Position(7, 0));
            PlacePiece(new Knight(this, Color.White), new Position(7, 1));
            PlacePiece(new Bishop(this, Color.White), new Position(7, 2));
            PlacePiece(new Queen(this, Color.White), new Position(7, 3));
            PlacePiece(new King(this, Color.White), new Position(7, 4));
            PlacePiece(new Bishop(this, Color.White), new Position(7, 5));
            PlacePiece(new Knight(this, Color.White), new Position(7, 6));
            PlacePiece(new Rook(this, Color.White), new Position(7, 7));
            PlacePiece(new Pawn(this, Color.White), new Position(6, 7));
            PlacePiece(new Pawn(this, Color.White), new Position(6, 6));
            PlacePiece(new Pawn(this, Color.White), new Position(6, 5));
            PlacePiece(new Pawn(this, Color.White), new Position(6, 4));
            PlacePiece(new Pawn(this, Color.White), new Position(6, 3));
            PlacePiece(new Pawn(this, Color.White), new Position(6, 2));
            PlacePiece(new Pawn(this, Color.White), new Position(6, 1));
            PlacePiece(new Pawn(this, Color.White), new Position(6, 0));

            // Coloca as peças pretas
            PlacePiece(new Rook(this, Color.Black), new Position(0, 0));
            PlacePiece(new Knight(this, Color.Black), new Position(0, 1));
            PlacePiece(new Bishop(this, Color.Black), new Position(0, 2));
            PlacePiece(new Queen(this, Color.Black), new Position(0, 3));
            PlacePiece(new King(this, Color.Black), new Position(0, 4));
            PlacePiece(new Bishop(this, Color.Black), new Position(0, 5));
            PlacePiece(new Knight(this, Color.Black), new Position(0, 6));
            PlacePiece(new Rook(this, Color.Black), new Position(0, 7));
            PlacePiece(new Pawn(this, Color.Black), new Position(1, 7));
            PlacePiece(new Pawn(this, Color.Black), new Position(1, 6));
            PlacePiece(new Pawn(this, Color.Black), new Position(1, 5));
            PlacePiece(new Pawn(this, Color.Black), new Position(1, 4));
            PlacePiece(new Pawn(this, Color.Black), new Position(1, 3));
            PlacePiece(new Pawn(this, Color.Black), new Position(1, 2));
            PlacePiece(new Pawn(this, Color.Black), new Position(1, 1));
            PlacePiece(new Pawn(this, Color.Black), new Position(1, 0));
        }

        /** ************************************************************************
        * \brief Atualiza o label de xeque.
        * \details Função responsável por atualizar o label de xeque, indicando se o 
        * jogador atual está ou não em xeque.
        ***************************************************************************/
        private void UpdateCheckLabel()
        {
            labelCheck.Text = _check ? "You are in check" : " ";
        }

        /** ************************************************************************
        * \brief Atualiza o label do jogador.
        * \details Função responsável por atualizar o label do jogador, indicando a
        * color do jogador atual.
        ***************************************************************************/
        private void UpdatePlayerLabel()
        {
            labelCurrentPlayer.Text = "Current player: " + (_currentPlayer == Color.White ? "White" : "Black");
        }

        /** ************************************************************************
        * \brief Atualiza o label de turno.
        * \details Função responsável por atualizar o label de turno, indicando o 
        * número do turno atual.
        ***************************************************************************/
        private void UpdateTurnLabel()
        {
            labelTurn.Text = $"Turn: {_turn}";
        }

        /** ************************************************************************
        * \brief Atualiza o label de peças capturadas.
        * \details Função responsável por atualizar o label de peças capturadas, 
        * indicando o número de peças capturadas por cada jogador.
        ***************************************************************************/
        private void UpdateCapturedPiecesLabels()
        {
            labelWhiteCapturedPieces.Text = $"Captured pieces: {_blackCapturedQuantity}";
            labelBlackCapturedPieces.Text = $"Captured pieces: {_whiteCapturedQuantity}";
        }

        /** ************************************************************************
        * \brief Atualiza o labels.
        * \details Função responsável por atualizar todos os labels do board.
        ***************************************************************************/
        private void UpdateLabels()
        {
            UpdatePlayerLabel();
            UpdateTurnLabel();
            UpdateCapturedPiecesLabels();
            UpdateCheckLabel();
        }

        /** ************************************************************************
        * \brief Incrementa turno.
        * \details Função responsável por incrementar o número do turno atual.
        ***************************************************************************/
        private void IncrementTurn()
        {
            _turn++;
        }


        /** ************************************************************************
        * \brief Mostra board.
        * \details Função responsável por imprimir o board de jogo.
        ***************************************************************************/
        private void ShowBoard()
        {
            System.Drawing.Color color;

            // Imprime o board com as cores iniciais
            for (int row = 0; row < Rows; row++)
            {
                color = (row % 2 == 0) ? System.Drawing.Color.Gray : System.Drawing.Color.White;
                for (int column = 0; column < Columns; column++)
                {
                    _pictureBoxes[row, column].BackColor = color;
                    color = (color == System.Drawing.Color.White) ? System.Drawing.Color.Gray : System.Drawing.Color.White;
                }
            }
        }

        /** ************************************************************************
        * \brief Mostra board.
        * \details Função responsável por imprimir o board de jogo, destacando
        * as posíveis movimentações de uma peça.
        * \param possiblePositions Posições possíveis que a peça pode assumir e, 
        * portanto, serão destacadas.
        ***************************************************************************/
        private void ShowBoard(bool[,] possiblePositions)
        {
            System.Drawing.Color color;

            // Imprimir o board nas cores iniciais
            ShowBoard();

            // Imprimir board destacando as possíveis jogadas
            for (int row = 0; row < Rows; row++)
            {
                color = (row % 2 == 0) ? System.Drawing.Color.Gray : System.Drawing.Color.White;
                for (int column = 0; column < Columns; column++)
                {
                    _pictureBoxes[row, column].BackColor = possiblePositions[row, column] ?
                        (_pictureBoxes[row, column].BackColor == System.Drawing.Color.White ? System.Drawing.Color.LightCyan : System.Drawing.Color.LightBlue) : color;
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
        public Piece RemovePiece(Position position)
        {
            // Verifica se existe peça na posição informada
            if (!PieceExists(position))
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
        public bool PieceExists(Position position)
        {
            return GetPiece(position) != null;
        }

        /** ************************************************************************
        * \brief Processa o click sobre um campo.
        * \details Função responsável por processar um click sobre um determinado
        * 'PictureBox'.
        * \param position Posição do 'PictureBox' que foi clicado.
        ***************************************************************************/
        private void ProcessPictureBoxClick(Position position)
        {
            // Checa se a origem não é nula e a posição de destination é igual a de origem
            if (_origin != null && _origin.CompareTo(position) == 0)
            {
                ShowBoard();
                _origin = null;
                return;
            }

            // Verifica se existe peça na posição informada
            if (PieceExists(position))
            {
                Piece piece = GetPiece(position);

                // Se a peça for do jogado atual, essa será a origem da jogada
                if (piece.PieceColor == _currentPlayer)
                {
                    _origin = position;
                    ShowBoard(CheckPossibleMoves(piece));
                    return;
                }
            }

            // Se o campo for uma posição posível de uma peça e a origem não for nula, a jogada pode ser realizada
            if ((_pictureBoxes[position.Row, position.Column].BackColor == System.Drawing.Color.LightBlue ||
                _pictureBoxes[position.Row, position.Column].BackColor == System.Drawing.Color.LightCyan) &&
                _origin != null)
            {
                ShowBoard();
                ExecutaMovimento(_origin, position);
                SwitchPlayer();
                IncrementTurn();
                _origin = null;
                VerificaFimDeJogo();
                UpdateLabels();
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
            _check = CheckCheck(_currentPlayer);
            ret = _check ? CheckCheckmate(_currentPlayer) : false;

            // Verifica empate
            ret = ret ? true : !ret && (!ColorHasWhereToMove() || (NumberOfPiecesInPlay() <= 2));

            // Chama a função de fim de jogo caso nescessário
            if(ret)
            {
                EndOfGame();
            }

            return ret;
        }

        /** ************************************************************************
        * \brief Executa o movimento da peça.
        * \details Função responsável por executar a movimentação de uma peça.
        * \param origem Posição de origem da peça.
        * \param destination Posição de destination da peça.
        * \return Peça que se encontrava na posição de destination, que foi capturada
        * pelo jogador.
        ***************************************************************************/
        public Piece ExecutaMovimento(Position origem, Position destination)
        {
            // Movimenta a peça da origem para o destination, armazenando a peça capturada
            Piece piece = RemovePiece(origem);
            piece.IncrementNumberOfMoves();
            Piece capturedPiece = RemovePiece(destination);
            PlacePiece(piece, destination);

            // Implementação da jogada especial de Promoção
            if (piece is Pawn &&
                ((piece.PieceColor == Color.White && destination.Row == 0) ||
                (piece.PieceColor == Color.Black && destination.Row == 7)))
            {
                RemovePiece(destination);
                PlacePiece(new Queen(this, piece.PieceColor), destination);
            }
            
            // Implementação da jogada especial Roque Pequeno
            if (piece is King && destination.Column == origem.Column + 2)
            {
                Piece torre = RemovePiece(new Position(origem.Row, origem.Column + 3));
                torre.IncrementNumberOfMoves();
                PlacePiece(torre, new Position(origem.Row, origem.Column + 1));
            }

            // Implementação da jogada especial Roque Grande
            if (piece is King && destination.Column == origem.Column - 2)
            {
                Piece torre = RemovePiece(new Position(origem.Row, origem.Column - 4));
                torre.IncrementNumberOfMoves();
                PlacePiece(torre, new Position(origem.Row, origem.Column - 1));
            }

            // Implementação da jogada especial En Passant
            if (piece is Pawn && origem.Column != destination.Column && capturedPiece == null)
            {
                Position posicaoPeao = new Position(piece.PieceColor == Color.White ? destination.Row + 1 : destination.Row - 1, destination.Column);
                capturedPiece = RemovePiece(posicaoPeao);
            }

            // Incrementa contador de peças capturadas
            if (capturedPiece != null)
            {
                _ = capturedPiece.PieceColor == Color.White ? _whiteCapturedQuantity++ : _blackCapturedQuantity++;
            }

            return capturedPiece;
        }

        /** ************************************************************************
        * \brief Lista as peças que estão em jogo.
        * \details Função responsável por informar quais são as peças em jogo de um
        * determinado jogador.
        * \param color Cor do jogador em que se deseja saber as peças que estão em jogo.
        * \return Um conjunto de peças do jogador informado.
        ***************************************************************************/
        public HashSet<Piece> PiecesInPlay(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece piece in _piecesInPlay)
            {
                if (piece != null && piece.PieceColor == color)
                {
                    aux.Add(piece);
                }
            }
            return aux;
        }

        /** ************************************************************************
        * \brief Informa a quantidade de peças em jogo.
        * \details Função responsável por informar a quantidade de peças em jogo.
        * \return Quantidade de peças em jogo.
        ***************************************************************************/
        public int NumberOfPiecesInPlay()
        {
            return PiecesInPlay(Color.White).Count + PiecesInPlay(Color.Black).Count;
        }

        /** ************************************************************************
        * \brief Pega a posição do rei.
        * \details Função responsável por informar qual a posição do rei de uma 
        * determinada color.
        * \param color Cor do jogador no qual se deseja saber em que posição está o rei.
        * \return Posição do rei do jogador informado.
        ***************************************************************************/
        private Position PegarPosicaoRei(Color color)
        {
            for (int column = 0; column < Columns; column++)
            {
                for (int row = 0; row < Rows; row++)
                {
                    if (_piecesInPlay[row, column] is King && _piecesInPlay[row, column].PieceColor == color)
                    {
                        return new Position(row, column);
                    }
                }
            }
            return null; // Nunca deve acontecer
        }

        /** ************************************************************************
        * \brief Verifica se o jogador está em xeque.
        * \details Função responsável por verificar se o jogador de uma determinada 
        * color está em xeque.
        * \param color Cor do jogador no qual se deseja saber se está em xeque.
        * \return 'true' se o jogador estiver em xeque, 'false' se não estiver.
        * \exception System.Exception Lançada quando o rei não estiver no board.
        ***************************************************************************/
        public bool CheckCheck(Color color)
        {
            Position kingPosition = PegarPosicaoRei(color);

            if (kingPosition == null)
            {
                throw new System.Exception($"There is no {color} king on the board.");
            }

            foreach (Piece piece in PiecesInPlay(OpponentColor(color)))
            {
                bool[,] matrix = piece.PossibleMoves();
                if (matrix[kingPosition.Row, kingPosition.Column])
                {
                    ((King)_piecesInPlay[kingPosition.Row, kingPosition.Column]).ReceivedCheck = true;
                    return true;
                }
            }

            return false;
        }

        /** ************************************************************************
        * \brief Checa as posíveis movimentações de uma peça.
        * \details Função responsável por checar e listar as posíveis movimentações
        * de uma determinada peça.
        * \param piece Peça a ser analisada.
        * \return Matriz de booleanos indicando quais posições uma peça pode assumir.
        ***************************************************************************/
        public bool[,] CheckPossibleMoves(Piece piece)
        {
            bool[,] possiblePositions = piece.PossibleMoves();
            for (int column = 0; column < Columns; column++)
            {
                for (int row = 0; row < Rows; row++)
                {
                    if (possiblePositions[row, column])
                    {
                        _piecesInPlay[piece.CurrentPosition.Row, piece.CurrentPosition.Column] = null;
                        Piece capturedPiece = GetPiece(row, column);
                        _piecesInPlay[row, column] = piece;
                        if (CheckCheck(piece.PieceColor))
                        {
                            possiblePositions[row, column] = false;
                        }
                        _piecesInPlay[row, column] = capturedPiece;
                        _piecesInPlay[piece.CurrentPosition.Row, piece.CurrentPosition.Column] = piece;
                    }
                }
            }
            return possiblePositions;
        }

        /** ************************************************************************
        * \brief Verifica xeque mate.
        * \details Função responsável por verificar se um determinado jogador está 
        * em xeque mate.
        * \param color Cor do jogador a ser verificado.
        * \return 'true' se o jogador estiver em xeque mate, 'false' se não estiver.
        ***************************************************************************/
        public bool CheckCheckmate(Color color)
        {
            foreach (Piece piece in PiecesInPlay(color))
            {
                bool[,] matrix = CheckPossibleMoves(piece);
                for (int row = 0; row < Rows; row++)
                {
                    for (int column = 0; column < Columns; column++)
                    {
                        if (matrix[row, column])
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
        public void EndOfGame()
        {
            // Mostra o vencedor
            string message = _check ? $"Checkmate\nWinner: {OpponentColor(_currentPlayer)}" : "Draw";
            MessageBox.Show(message);

            if (MessageBox.Show("Do you want to play another game?", "End of game", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                InitializeNewGame();
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
        public bool ColorHasWhereToMove()
        {
            foreach(Piece piece in PiecesInPlay(_currentPlayer))
            {
                foreach (bool position in CheckPossibleMoves(piece))
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

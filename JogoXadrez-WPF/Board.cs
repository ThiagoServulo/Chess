using System.Windows.Forms;
using System.Collections.Generic;

namespace Chess
{
    /** ************************************************************************
    * \brief Information about the board.
    * \details The Board class stores information about the game board, where the 
    * pieces will be placed.
    * \author Thiago Sérvulo Guimarães - thiagoservulog@gmail.com
    * \date 16/03/2024
    * \version v1.0.1
    ***************************************************************************/
    partial class Board : Form
    {
        /// \brief Number of rows existing on the board.
        public int Rows = 8;

        /// \brief Number of columns existing on the board.
        public int Columns = 8;

        /// \brief Indicates if the game is in check.
        public bool _check;

        /// \brief Matrix containing all the pieces that are in play.
        private Piece[,] _piecesInPlay;

        /// \brief Matrix containing all the squares of the board.
        private PictureBox[,] _pictureBoxes;

        /// \brief Origin position of the current move.
        private Position _origin;

        /// \brief Color of the current player.
        private Color _currentPlayer;

        /// \brief Number of the current turn.
        private int _turn;

        /// \brief Number of white pieces captured.
        private int _whiteCapturedQuantity;

        /// \brief Number of black pieces captured.
        private int _blackCapturedQuantity;

        /** ************************************************************************
        * \brief Constructor.
        * \details Constructor of the Board class.
        ***************************************************************************/
        public Board()
        {
            // Inicialize the components
            InitializeComponent();

            // Initialize the matrix of pieces on the board
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

            // Initialize variables and board layout
            InitializeNewGame();
        }

        /** ************************************************************************
        * \brief Initialize a new game.
        * \details Initialize variables and board layout for a new game.
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
        * \brief Change player.
        * \details Function responsible for changing the current player.
        ***************************************************************************/
        private void SwitchPlayer()
        {
            _currentPlayer = OpponentColor(_currentPlayer);
        }

        /** ************************************************************************
        * \brief Find opponent's color.
        * \details Function responsible for returning the opponent's color of a given
        * player.
        * \param color Color of the player to find the opponent for.
        * \return Color of the opponent player.
        ***************************************************************************/
        private Color OpponentColor(Color color)
        {
            return color == Color.White ? Color.Black : Color.White;
        }

        /** ************************************************************************
        * \brief Access piece.
        * \details Function responsible for accessing a piece that is at the given
        * position.
        * \param row Row where the piece is located.
        * \param column Column where the piece is located.
        * \return Piece at the given position.
        ***************************************************************************/
        public Piece GetPiece(int row, int column)
        {
            return _piecesInPlay[row, column];
        }

        /** ************************************************************************
        * \brief Access piece.
        * \details Function responsible for accessing a piece that is at the given
        * position.
        * \param position Position where we want to access the piece.
        * \return Piece at the given position.
        ***************************************************************************/
        public Piece GetPiece(Position position)
        {
            return _piecesInPlay[position.Row, position.Column];
        }

        /** ************************************************************************
        * \brief Place piece.
        * \details Function responsible for placing a piece at a certain position.
        * \param piece Piece to be placed.
        * \param position Position where the piece will be placed.
        ***************************************************************************/
        public void PlacePiece(Piece piece, Position position)
        {
            _piecesInPlay[position.Row, position.Column] = piece;
            piece.CurrentPosition = position;
            _pictureBoxes[position.Row, position.Column].Image = piece.ShowImage();
        }

        /** ************************************************************************
        * \brief Initialize board.
        * \details Function responsible for initializing the board, placing the
        * black and white pieces in their initial positions.
        ***************************************************************************/
        private void InitializeBoard()
        {
            // Initialize board layout
            ShowBoard();

            // Resetting the image of all Picture Boxes
            for (int row = 0; row < Rows; row++)
            {
                for (int column = 0; column < Columns; column++)
                {
                    _piecesInPlay[row, column] = null;
                    _pictureBoxes[row, column].Image = null;
                }
            }

            // Place the white pieces
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

            // Place the black pieces
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
        * \brief Update the check label.
        * \details Function responsible for updating the check label, indicating
        * whether the current player is in check or not.
        ***************************************************************************/
        private void UpdateCheckLabel()
        {
            labelCheck.Text = _check ? "You are in check" : " ";
        }

        /** ************************************************************************
        * \brief Update the player label.
        * \details Function responsible for updating the player label, indicating the
        * color of the current player.
        ***************************************************************************/
        private void UpdatePlayerLabel()
        {
            labelCurrentPlayer.Text = "Current player: " + (_currentPlayer == Color.White ? "White" : "Black");
        }

        /** ************************************************************************
        * \brief Update the turn label.
        * \details Function responsible for updating the turn label, indicating the
        * number of the current turn.
        ***************************************************************************/
        private void UpdateTurnLabel()
        {
            labelTurn.Text = $"Turn: {_turn}";
        }

        /** ************************************************************************
        * \brief Update the captured pieces label.
        * \details Function responsible for updating the captured pieces label,
        * indicating the number of pieces captured by each player.
        ***************************************************************************/
        private void UpdateCapturedPiecesLabels()
        {
            labelWhiteCapturedPieces.Text = $"Captured pieces: {_blackCapturedQuantity}";
            labelBlackCapturedPieces.Text = $"Captured pieces: {_whiteCapturedQuantity}";
        }

        /** ************************************************************************
        * \brief Update the labels.
        * \details Function responsible for updating all labels on the board.
        ***************************************************************************/
        private void UpdateLabels()
        {
            UpdatePlayerLabel();
            UpdateTurnLabel();
            UpdateCapturedPiecesLabels();
            UpdateCheckLabel();
        }

        /** ************************************************************************
        * \brief Increment turn.
        * \details Function responsible for incrementing the number of the current
        * turn.
        ***************************************************************************/
        private void IncrementTurn()
        {
            _turn++;
        }


        /** ************************************************************************
        * \brief Display board.
        * \details Function responsible for printing the game board.
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
        * \brief Display board.
        * \details Function responsible for printing the game board, highlighting
        * the possible movements of a piece.
        * \param possiblePositions Possible positions that the piece can assume and,
        * therefore, will be highlighted.
        ***************************************************************************/
        private void ShowBoard(bool[,] possiblePositions)
        {
            System.Drawing.Color color;

            // Print the board in the initial colors
            ShowBoard();

            // Print the board highlighting the possible moves
            for (int row = 0; row < Rows; row++)
            {
                color = (row % 2 == 0) ? System.Drawing.Color.Gray : System.Drawing.Color.White;
                for (int column = 0; column < Columns; column++)
                {
                    _pictureBoxes[row, column].BackColor = possiblePositions[row, column] ?
                        (_pictureBoxes[row, column].BackColor == System.Drawing.Color.White ? 
                        System.Drawing.Color.LightCyan : System.Drawing.Color.LightBlue) : color;
                    color = color == System.Drawing.Color.White ? System.Drawing.Color.Gray : System.Drawing.Color.White;
                }
            }
        }

        /** ************************************************************************
        * \brief Remove piece.
        * \details Function responsible for removing a piece from a certain position.
        * \param position Position from which the piece will be removed.
        * \return Piece removed from the given position.
        ***************************************************************************/
        public Piece RemovePiece(Position position)
        {
            // Check if there is a piece at the given position
            if (!PieceExists(position))
            {
                return null;
            }

            // Remove the piece from the given position
            Piece aux = GetPiece(position);
            aux.CurrentPosition = null;

            // Clears the information related to the square located at this position
            _piecesInPlay[position.Row, position.Column] = null;
            _pictureBoxes[position.Row, position.Column].Image = null;

            // Returns the piece that was removed
            return aux;
        }

        /** ************************************************************************
        * \brief Check if there is a piece.
        * \details Function responsible for checking if there is a piece at a certain
        * position.
        * \param position Position to check if there is a piece.
        * \return 'true' if there is a piece at the given position, or 'false' if not.
        ***************************************************************************/
        public bool PieceExists(Position position)
        {
            return GetPiece(position) != null;
        }

        /** ************************************************************************
        * \brief Process click on a square.
        * \details Function responsible for processing a click on a certain 
        * 'PictureBox'.
        * \param position Position of the 'PictureBox' that was clicked.
        ***************************************************************************/
        private void ProcessPictureBoxClick(Position position)
        {
            // Check if the origin is not null and the destination position is equal to the origin position
            if (_origin != null && _origin.CompareTo(position) == 0)
            {
                ShowBoard();
                _origin = null;
                return;
            }

            // Check if there is a piece at the given position
            if (PieceExists(position))
            {
                Piece piece = GetPiece(position);

                // If the piece belongs to the current player, it will be the origin of the move
                if (piece.PieceColor == _currentPlayer)
                {
                    _origin = position;
                    ShowBoard(CheckPossibleMoves(piece));
                    return;
                }
            }

            // If the square is a possible position for a piece and the origin is not null, the move can be made
            if ((_pictureBoxes[position.Row, position.Column].BackColor == System.Drawing.Color.LightBlue ||
                _pictureBoxes[position.Row, position.Column].BackColor == System.Drawing.Color.LightCyan) &&
                _origin != null)
            {
                ShowBoard();
                ExecuteMove(_origin, position);
                SwitchPlayer();
                IncrementTurn();
                _origin = null;
                CheckEndOfGame();
                UpdateLabels();
            }
        }

        /** ************************************************************************
        * \brief Check end of game.
        * \details Function responsible for checking if an end of game event has
        * occurred.
        * \return 'true' if the game is over, 'false' if not.
        ***************************************************************************/
        public bool CheckEndOfGame()
        {
            bool ret;

            // Checkmatec check
            _check = CheckCheck(_currentPlayer);
            ret = _check ? CheckCheckmate(_currentPlayer) : false;

            // Check draw
            ret = ret ? true : !ret && (!ColorHasWhereToMove() || (NumberOfPiecesInPlay() <= 2));

            // Chama a função de fim de jogo caso nescessário
            if(ret)
            {
                EndOfGame();
            }

            return ret;
        }

        /** ************************************************************************
        * \brief Execute the piece movement.
        * \details Function responsible for executing the movement of a piece.
        * \param origin Origin position of the piece.
        * \param destination Destination position of the piece.
        * \return Piece that was in the destination position, which was captured
        * by the player.
        ***************************************************************************/
        public Piece ExecuteMove(Position origin, Position destination)
        {
            // Move the piece from the origin to the destination, storing the captured piece.
            Piece piece = RemovePiece(origin);
            piece.IncrementNumberOfMoves();
            Piece capturedPiece = RemovePiece(destination);
            PlacePiece(piece, destination);

            // Implementation of the special move Promotion
            if (piece is Pawn &&
                ((piece.PieceColor == Color.White && destination.Row == 0) ||
                (piece.PieceColor == Color.Black && destination.Row == 7)))
            {
                RemovePiece(destination);
                PlacePiece(new Queen(this, piece.PieceColor), destination);
            }

            // Implementation of the special move Kingside Castling
            if (piece is King && destination.Column == origin.Column + 2)
            {
                Piece rook = RemovePiece(new Position(origin.Row, origin.Column + 3));
                rook.IncrementNumberOfMoves();
                PlacePiece(rook, new Position(origin.Row, origin.Column + 1));
            }

            // Implementation of the special move Queenside Castling
            if (piece is King && destination.Column == origin.Column - 2)
            {
                Piece rook = RemovePiece(new Position(origin.Row, origin.Column - 4));
                rook.IncrementNumberOfMoves();
                PlacePiece(rook, new Position(origin.Row, origin.Column - 1));
            }

            // Implementation of the special move En Passant
            if (piece is Pawn && origin.Column != destination.Column && capturedPiece == null)
            {
                Position pawnPosition = new Position(piece.PieceColor == Color.White ? destination.Row + 1 : destination.Row - 1, destination.Column);
                capturedPiece = RemovePiece(pawnPosition);
            }

            // Increment captured pieces counter
            if (capturedPiece != null)
            {
                _ = capturedPiece.PieceColor == Color.White ? _whiteCapturedQuantity++ : _blackCapturedQuantity++;
            }

            return capturedPiece;
        }

        /** ************************************************************************
        * \brief Lists the pieces that are in play.
        * \details Function responsible for informing which pieces are in play for a
        * given player.
        * \param color Color of the player for which we want to know the pieces that
        * are in play.
        * \return A set of pieces for the given player.
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
        * \brief Inform the quantity of pieces in play.
        * \details Function responsible for informing the quantity of pieces in play.
        * \return Quantity of pieces in play.
        ***************************************************************************/
        public int NumberOfPiecesInPlay()
        {
            return PiecesInPlay(Color.White).Count + PiecesInPlay(Color.Black).Count;
        }

        /** ************************************************************************
        * \brief Get the king position.
        * \details Function responsible for informing the position of the king of a
        * given color.
        * \param color Color of the player for which we want to know the king's
        * position.
        * \return Position of the king of the given player.
        ***************************************************************************/
        private Position GetKingPosition(Color color)
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
            return null; // This should never happen
        }

        /** ************************************************************************
        * \brief Checks if the player is in check.
        * \details Function responsible for checking if the player of a certain
        * color is in check.
        * \param color Color of the player to check for being in check.
        * \return 'true' if the player is in check, 'false' if not.
        * \exception System.Exception Thrown when the king is not on the board.
        ***************************************************************************/
        public bool CheckCheck(Color color)
        {
            Position kingPosition = GetKingPosition(color);

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
        * \brief Checks the possible movements of a piece.
        * \details Function responsible for checking and listing the possible
        * movements of a given piece.
        * \param piece Piece to be analyzed.
        * \return Matrix of booleans indicating which positions a piece can assume.
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
        * \brief Checks for checkmate.
        * \details Function responsible for checking if a certain player is in
        * checkmate.
        * \param color Color of the player to be checked.
        * \return 'true' if the player is in checkmate, 'false' if not.
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
        * \brief Game Over.
        * \details Function responsible for processing the end of the game,
        * presenting the winner or draw, and asking if the player wants to start a
        * new game.
        ***************************************************************************/
        public void EndOfGame()
        {
            // Show the winner
            string message = _check ? $"Checkmate\nWinner: {OpponentColor(_currentPlayer)}" : "Draw";
            MessageBox.Show(message);

            if (MessageBox.Show("Do you want to play another game?", "End of game", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                InitializeNewGame();
            }
            else
            {
                // Close the application if the user does not want to play another match.
                Application.Exit();
            }
        }

        /** ************************************************************************
        * \brief Checks if a player has any valid moves.
        * \details Function responsible for verifying if a player has any possible
        * moves.
        * \return 'true' if the player has possible moves, 'false' if not.
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

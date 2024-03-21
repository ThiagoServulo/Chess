using System.Drawing;
using System.IO;

namespace Chess
{
    /** ************************************************************************
    * \brief Information about the piece.
    * \details The Piece class stores information about the piece, which will be
    * placed on the game board.
    * \author Thiago Sérvulo Guimarães - thiagoservulog@gmail.com
    * \date 15/03/2024
    * \version v1.0.1
    ***************************************************************************/
    abstract class Piece
    {
        /// \brief Image related to the piece.
        public Image Image;

        /// \brief Current position of the piece.
        public Position CurrentPosition { get; set; }

        /// \brief Piece color.
        public Color PieceColor { get; protected set; }

        /// \brief Number of movements of the piece.
        public int NumberOfMoves { get; protected set; }

        /// \brief Game board.
        public Board ChessBoard { get; protected set; }

        /// \brief Path to the piece image.
        public string FileImagesPath;

        /** ************************************************************************
        * \brief Constructor of the Piece class.
        * \param board Game board.
        * \param color Color of the piece.
        ***************************************************************************/
        public Piece(Board board, Color color)
        {
            FileImagesPath = Path.GetFullPath("./images");
            CurrentPosition = null;
            ChessBoard = board;
            PieceColor = color;
            NumberOfMoves = 0;
        }

        /** ************************************************************************
        * \brief Display image.
        * \details Function responsible for displaying the image of the piece.
        * \return Image related to the piece.
        ***************************************************************************/
        public Image ShowImage()
        {
            return Image;
        }

        /** ************************************************************************
        * \brief Display image.
        * \details Function responsible for displaying the image of the piece.
        * \param image Name of the image related to the piece.
        * \return Image related to the piece.
        * \exception System.Exception Thrown if the image path is not found.
        ***************************************************************************/
        public Image GetImage(string image)
        {
            try
            {
                return Image.FromFile($"{FileImagesPath}/{image}");
            }
            catch
            {
                throw new System.Exception("Error while fetching piece images");
            }
        }

        /** ************************************************************************
        * \brief Increment the number of movements.
        * \details Function responsible for incrementing the number of movements of
        * the piece.
        ***************************************************************************/
        public void IncrementNumberOfMoves()
        {
            NumberOfMoves++;
        }

        /** ************************************************************************
        * \brief Check valid position.
        * \details Function responsible for checking if a certain position is valid.
        * \param position Position to be checked.
        * \return 'true' if the position is valid, 'false' if it is invalid.
        ***************************************************************************/
        public bool ValidPosition(Position position)
        {
            return position.Row <= 7 && position.Row >= 0 && position.Column <= 7 && position.Column >= 0;
        }

        /** ************************************************************************
        * \brief Check if it can move.
        * \details Function responsible for checking if the piece can be moved to
        * a certain position.
        * \param position Position to be checked.
        * \return 'true' if the piece can be moved to the given position, 'false' 
        * otherwise.
        ***************************************************************************/
        public bool CanMove(Position position)
        {
            if (ValidPosition(position))
            {
                Piece piece = ChessBoard.GetPiece(position);
                return (piece == null || piece.PieceColor != PieceColor);
            }
            return false;
        }

        /** ************************************************************************
        * \brief List possible moves.
        * \details Abstract function responsible for listing the possible moves of
        * a piece.
        * \return Boolean matrix indicating the possible positions that the piece 
        * can assume after its movement.
        ***************************************************************************/
        public abstract bool[,] PossibleMoves();

    }
}
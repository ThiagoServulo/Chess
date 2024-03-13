using System.Drawing;
using System.IO;

namespace Chess
{
    /** ************************************************************************
    * \brief Informações sobre a peça.
    * \details A classe Peca armazena as informações referentes a peça, que será
    * colocada no board do jogo.
    * \author Thiago Sérvulo Guimarães - thiago.servulo@sga.pucminas.br
    * \date 19/07/2022
    * \version v1.0.0
    ***************************************************************************/
    abstract class Piece
    {
        /// \brief Image referente a peça.
        public Image Image;

        /// \brief Posição atual da peça.
        public Position CurrentPosition { get; set; }

        /// \brief Cor da peça.
        public Color PieceColor { get; protected set; }

        /// \brief Quantidade de movimentações da peça.
        public int NumberOfMoves { get; protected set; }

        /// \brief Tabuleiro do jogo.
        public Board ChessBoard { get; protected set; }

        /// \brief Caminho para a imagem da peça.
        public string FileImagesPath;

        /** ************************************************************************
        * \brief Construtor da classe Peca.
        * \param board Tabuleiro do jogo.
        * \param color Cor da peça.
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
        * \brief Mostra imagem.
        * \details Função responsável por mostrar a imagem da peça.
        * \return Image refrente a peça.
        ***************************************************************************/
        public Image ShowImage()
        {
            return Image;
        }

        /** ************************************************************************
        * \brief Mostra imagem.
        * \details Função responsável por mostrar a imagem da peça.
        * \param imagem Nome da imagem refrente a peça.
        * \return Image refrente a peça.
        * \exception System.Exception Lançada o path de imagens não for encontrado.
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
        * \brief Incrementa a quantidade de movimentos.
        * \details Função responsável por incrementar a  quantidade de movimentos da
        * peça.
        ***************************************************************************/
        public void IncrementNumberOfMoves()
        {
            NumberOfMoves++;
        }

        /** ************************************************************************
        * \brief Verifica posição válida.
        * \details Função responsável por verificar se uma determinada posição é 
        * válida.
        * \param position Posição a ser verificada.
        * \return 'true' se a posição for válida, 'false' se for inválida.
        ***************************************************************************/
        public bool ValidPosition(Position position)
        {
            return position.Row <= 7 && position.Row >= 0 && position.Column <= 7 && position.Column >= 0;
        }

        /** ************************************************************************
        * \brief Verifica se pode mover.
        * \details Função responsável por verificar se a peça pode ser movida para
        * uma determinada posição.
        * \param position Posição a ser verificada.
        * \return 'true' se a peça puder ser movida para a posição informada, 'false' 
        * se não.
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
        * \brief Lista movimentos possíveis.
        * \details Função abstrata responsável por listar os movimentos posíveis de
        * uma peça.
        * \return Matriz de booleanos indicando as possíveis posições que a peça 
        * pode assumir após a sua movimentação.
        ***************************************************************************/
        public abstract bool[,] PossibleMoves();

    }
}
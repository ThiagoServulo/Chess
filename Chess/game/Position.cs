namespace Chess
{
    /** ************************************************************************
    * \brief Information about the position.
    * \details The Position class stores information about the position of
    * a piece on the board.
    * \author Thiago Sérvulo Guimarães - thiagoservulog@gmail.com
    * \date 16/03/2024
    * \version v1.0.1
    ***************************************************************************/
    class Position : System.IComparable
    {
        /// \brief Row number corresponding to the position.
        public int Row { get; set; }

        /// \brief Column number corresponding to the position.
        public int Column { get; set; }

        /** ************************************************************************
        * \brief Constructor of the Position class.
        * \param row Row number.
        * \param column Column number.
        ***************************************************************************/
        public Position(int row, int column)
        {
            SetPosition(row, column);
        }

        /** ************************************************************************
        * \brief Set position.
        * \details Function responsible for setting a position.
        * \param row Row number.
        * \param column Column number.
        ***************************************************************************/
        public void SetPosition(int row, int column)
        {
            Row = row;
            Column = column;
        }

        /** ************************************************************************
        * \brief Compare positions.
        * \details Function responsible for comparing positions.
        * \param obj Object of type Position to be compared.
        * \return 0 if the compared positions are equal, 1 if the compared positions 
        * are different.
        ***************************************************************************/
        public int CompareTo(object obj)
        {
            Position other = obj as Position;
            return other.Row == Row && other.Column == Column ? 0 : 1;
        }

    }
}

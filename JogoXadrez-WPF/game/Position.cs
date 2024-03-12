namespace Chess
{
    /** ************************************************************************
    * \brief Informações sobre a posição.
    * \details A classe Posicao armazena as informações referentes a posição de
    * uma peça no board.
    * \author Thiago Sérvulo Guimarães - thiago.servulo@sga.pucminas.br
    * \date 19/07/2022
    * \version v1.0.0
    ***************************************************************************/
    class Position : System.IComparable
    {
        /// \brief Número da row refrente a posição.
        public int Row { get; set; }

        /// \brief Número da column refrente a posição.
        public int Column { get; set; }

        /** ************************************************************************
        * \brief Construtor da classe Posicao.
        * \param row Número da row.
        * \param column Número da column.
        ***************************************************************************/
        public Position(int row, int column)
        {
            SetPosition(row, column);
        }

        /** ************************************************************************
        * \brief Define posição.
        * \details Função responsável por definir uma posição.
        * \param row Número da row.
        * \param column Número da column.
        ***************************************************************************/
        public void SetPosition(int row, int column)
        {
            Row = row;
            Column = column;
        }

        /** ************************************************************************
        * \brief Compara posições.
        * \details Função responsável por comparar posições.
        * \param obj Objeto do tipo Posicao para ser comparado.
        * \param 0 - se as posições comparadas forem iguais, 1 - se as posições 
        * comparadas forem diferentes.
        ***************************************************************************/
        public int CompareTo(object obj)
        {
            Position other = obj as Position;
            return other.Row == Row && other.Column == Column ? 0 : 1;
        }

    }
}

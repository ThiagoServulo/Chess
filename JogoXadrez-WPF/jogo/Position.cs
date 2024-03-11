namespace Chess
{
    /** ************************************************************************
    * \brief Informações sobre a posição.
    * \details A classe Posicao armazena as informações referentes a posição de
    * uma peça no tabuleiro.
    * \author Thiago Sérvulo Guimarães - thiago.servulo@sga.pucminas.br
    * \date 19/07/2022
    * \version v1.0.0
    ***************************************************************************/
    class Position : System.IComparable
    {
        /// \brief Número da linha refrente a posição.
        public int Linha { get; set; }

        /// \brief Número da coluna refrente a posição.
        public int Coluna { get; set; }

        /** ************************************************************************
        * \brief Construtor da classe Posicao.
        * \param linha Número da linha.
        * \param coluna Número da coluna.
        ***************************************************************************/
        public Position(int linha, int coluna)
        {
            DefinirPosicao(linha, coluna);
        }

        /** ************************************************************************
        * \brief Define posição.
        * \details Função responsável por definir uma posição.
        * \param linha Número da linha.
        * \param coluna Número da coluna.
        ***************************************************************************/
        public void DefinirPosicao(int linha, int coluna)
        {
            Linha = linha;
            Coluna = coluna;
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
            return other.Linha == Linha && other.Coluna == Coluna ? 0 : 1;
        }

    }
}

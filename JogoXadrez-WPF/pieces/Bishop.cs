namespace Chess
{
    /** ************************************************************************
    * \brief Informações sobre o bispo.
    * \details A classe Bispo armazena as informações referentes ao bispo.
    * \author Thiago Sérvulo Guimarães - thiago.servulo@sga.pucminas.br
    * \date 19/07/2022
    * \version v1.0.0
    ***************************************************************************/
    class Bishop : Piece
    {
        /** ************************************************************************
        * \brief Construtor da classe Bispo.
        * \param tabuleiro Tabuleiro do jogo.
        * \param cor Cor da peça.
        ***************************************************************************/
        public Bishop(Tabuleiro tabuleiro, Color corDaPeca) : base(tabuleiro, corDaPeca)
        {
            Imagem = BuscarImagem(corDaPeca == Color.White ? "white_bishop.png" : "black_bishop.png");
        }

        /** ************************************************************************
        * \brief Lista movimentos possíveis.
        * \details Função abstrata responsável por listar os movimentos posíveis do
        * bispo.
        * \return Matriz de booleanos indicando as possíveis posições que o bispo 
        * pode assumir após a sua movimentação.
        ***************************************************************************/
        public override bool[,] MovimentosPossiveis()
        {
            bool[,] matriz = new bool[TabuleiroXadrez.Linhas, TabuleiroXadrez.Colunas];

            Position posicao = new Position(0, 0);

            // direção noroeste (diagonal superior esquerda)
            posicao.DefinirPosicao(PosicaoAtual.Linha + 1, PosicaoAtual.Coluna - 1);
            while (PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
                if (TabuleiroXadrez.AcessarPeca(posicao) != null && TabuleiroXadrez.AcessarPeca(posicao).CorDaPeca != CorDaPeca)
                {
                    break;
                }
                posicao.DefinirPosicao(posicao.Linha + 1, posicao.Coluna - 1);
            }

            // direção nordeste (diagonal superior direita)
            posicao.DefinirPosicao(PosicaoAtual.Linha + 1, PosicaoAtual.Coluna + 1);
            while (PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
                if (TabuleiroXadrez.AcessarPeca(posicao) != null && TabuleiroXadrez.AcessarPeca(posicao).CorDaPeca != CorDaPeca)
                {
                    break;
                }
                posicao.DefinirPosicao(posicao.Linha + 1, posicao.Coluna + 1);
            }

            // direção suldoeste (diagonal inferior esquerda)
            posicao.DefinirPosicao(PosicaoAtual.Linha - 1, PosicaoAtual.Coluna + 1);
            while (PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
                if (TabuleiroXadrez.AcessarPeca(posicao) != null && TabuleiroXadrez.AcessarPeca(posicao).CorDaPeca != CorDaPeca)
                {
                    break;
                }
                posicao.DefinirPosicao(posicao.Linha - 1, posicao.Coluna + 1);
            }

            // direção suldeste (diagonal inferior direita)
            posicao.DefinirPosicao(PosicaoAtual.Linha - 1, PosicaoAtual.Coluna - 1);
            while (PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
                if (TabuleiroXadrez.AcessarPeca(posicao) != null && TabuleiroXadrez.AcessarPeca(posicao).CorDaPeca != CorDaPeca)
                {
                    break;
                }
                posicao.DefinirPosicao(posicao.Linha - 1, posicao.Coluna - 1);
            }

            return matriz;
        }
    }
}
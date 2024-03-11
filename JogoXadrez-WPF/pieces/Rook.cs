namespace Chess
{
    /** ************************************************************************
    * \brief Informações sobre a torre.
    * \details A classe Torre armazena as informações referentes a torre.
    * \author Thiago Sérvulo Guimarães - thiago.servulo@sga.pucminas.br
    * \date 19/07/2022
    * \version v1.0.0
    ***************************************************************************/
    class Rook : Piece
    {
        /** ************************************************************************
        * \brief Construtor da classe Torre.
        * \param tabuleiro Tabuleiro em que a peça será inserida.
        * \param corDaPeca Cor da peça.
        ***************************************************************************/
        public Rook(Tabuleiro tabuleiro, Color corDaPeca) : base(tabuleiro, corDaPeca)
        {
            Imagem = BuscarImagem(corDaPeca == Color.White ? "white_rook.png" : "black_rook.png");
        }

        /** ************************************************************************
        * \brief Lista movimentos possíveis.
        * \details Função abstrata responsável por listar os movimentos posíveis da
        * torre.
        * \return Matriz de booleanos indicando as possíveis posições que a torre
        * pode assumir após a sua movimentação.
        ***************************************************************************/
        public override bool[,] MovimentosPossiveis()
        {
            bool[,] matriz = new bool[TabuleiroXadrez.Linhas, TabuleiroXadrez.Colunas];

            Position posicao = new Position(0, 0);

            // direção norte (acima)
            posicao.DefinirPosicao(PosicaoAtual.Linha - 1, PosicaoAtual.Coluna);
            while (PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
                if ((TabuleiroXadrez.AcessarPeca(posicao) != null) && TabuleiroXadrez.AcessarPeca(posicao).CorDaPeca != CorDaPeca)
                {
                    break;
                }
                posicao.Linha -= 1;
            }

            // direção sul (abaixo)
            posicao.DefinirPosicao(PosicaoAtual.Linha + 1, PosicaoAtual.Coluna);
            while (PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
                if ((TabuleiroXadrez.AcessarPeca(posicao) != null) && TabuleiroXadrez.AcessarPeca(posicao).CorDaPeca != CorDaPeca)
                {
                    break;
                }
                posicao.Linha += 1;
            }

            // direção leste (direita)
            posicao.DefinirPosicao(PosicaoAtual.Linha, PosicaoAtual.Coluna + 1);
            while (PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
                if ((TabuleiroXadrez.AcessarPeca(posicao) != null) && TabuleiroXadrez.AcessarPeca(posicao).CorDaPeca != CorDaPeca)
                {
                    break;
                }
                posicao.Coluna += 1;
            }

            // direção oeste (esquerda)
            posicao.DefinirPosicao(PosicaoAtual.Linha, PosicaoAtual.Coluna - 1);
            while (PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
                if ((TabuleiroXadrez.AcessarPeca(posicao) != null) && TabuleiroXadrez.AcessarPeca(posicao).CorDaPeca != CorDaPeca)
                {
                    break;
                }
                posicao.Coluna -= 1;
            }

            return matriz;
        }

    }
}
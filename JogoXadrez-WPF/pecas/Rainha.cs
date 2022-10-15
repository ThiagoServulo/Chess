namespace JogoXadrez_WPF
{
    /** ************************************************************************
    * \brief Informações sobre a rainha.
    * \details A classe Rainha armazena as informações referentes a rainha.
    * \author Thiago Sérvulo Guimarães - thiago.servulo@sga.pucminas.br
    * \date 19/07/2022
    * \version v1.0.0
    ***************************************************************************/
    class Rainha : Peca
    {
        /** ************************************************************************
        * \brief Construtor da classe Rainha.
        * \param tabuleiro Tabuleiro em que a peça será inserida.
        * \param corDaPeca Cor da peça.
        ***************************************************************************/
        public Rainha(Tabuleiro tabuleiro, Cor corDaPeca) : base(tabuleiro, corDaPeca)
        {
            Imagem = BuscarImagem(corDaPeca == Cor.Branco ? "rainha_branco.png" : "rainha_preto.png");
        }

        /** ************************************************************************
        * \brief Lista movimentos possíveis.
        * \details Função abstrata responsável por listar os movimentos posíveis da
        * rainha.
        * \return Matriz de booleanos indicando as possíveis posições que a rainha 
        * pode assumir após a sua movimentação.
        ***************************************************************************/
        public override bool[,] MovimentosPossiveis()
        {
            bool[,] matriz = new bool[TabuleiroXadrez.Linhas, TabuleiroXadrez.Colunas];

            Posicao posicao = new Posicao(0, 0);

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

            // direção noroeste (diagonal superior esquerda)
            posicao.DefinirPosicao(PosicaoAtual.Linha + 1, PosicaoAtual.Coluna - 1);
            while (PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
                if ((TabuleiroXadrez.AcessarPeca(posicao) != null) && TabuleiroXadrez.AcessarPeca(posicao).CorDaPeca != CorDaPeca)
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
                if ((TabuleiroXadrez.AcessarPeca(posicao) != null) && TabuleiroXadrez.AcessarPeca(posicao).CorDaPeca != CorDaPeca)
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
                if ((TabuleiroXadrez.AcessarPeca(posicao) != null) && TabuleiroXadrez.AcessarPeca(posicao).CorDaPeca != CorDaPeca)
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
                if ((TabuleiroXadrez.AcessarPeca(posicao) != null) && TabuleiroXadrez.AcessarPeca(posicao).CorDaPeca != CorDaPeca)
                {
                    break;
                }
                posicao.DefinirPosicao(posicao.Linha - 1, posicao.Coluna - 1);
            }

            return matriz;
        }
    }
}
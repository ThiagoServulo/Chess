using System.Drawing;
using System.IO;

namespace Chess
{
    /** ************************************************************************
    * \brief Informações sobre a peça.
    * \details A classe Peca armazena as informações referentes a peça, que será
    * colocada no tabuleiro do jogo.
    * \author Thiago Sérvulo Guimarães - thiago.servulo@sga.pucminas.br
    * \date 19/07/2022
    * \version v1.0.0
    ***************************************************************************/
    abstract class Peca
    {
        /// \brief Imagem referente a peça.
        public Image Imagem;

        /// \brief Posição atual da peça.
        public Posicao PosicaoAtual { get; set; }

        /// \brief Cor da peça.
        public Cor CorDaPeca { get; protected set; }

        /// \brief Quantidade de movimentações da peça.
        public int QuantidadeDeMovimentos { get; protected set; }

        /// \brief Tabuleiro do jogo.
        public Tabuleiro TabuleiroXadrez { get; protected set; }

        /// \brief Caminho para a imagem da peça.
        public string FileImagesPath;

        /** ************************************************************************
        * \brief Construtor da classe Peca.
        * \param tabuleiro Tabuleiro do jogo.
        * \param cor Cor da peça.
        ***************************************************************************/
        public Peca(Tabuleiro tabuleiro, Cor cor)
        {
            FileImagesPath = Path.GetFullPath("./imagens");
            PosicaoAtual = null;
            TabuleiroXadrez = tabuleiro;
            CorDaPeca = cor;
            QuantidadeDeMovimentos = 0;
        }

        /** ************************************************************************
        * \brief Mostra imagem.
        * \details Função responsável por mostrar a imagem da peça.
        * \return Imagem refrente a peça.
        ***************************************************************************/
        public Image MostrarImagem()
        {
            return Imagem;
        }

        /** ************************************************************************
        * \brief Mostra imagem.
        * \details Função responsável por mostrar a imagem da peça.
        * \param imagem Nome da imagem refrente a peça.
        * \return Imagem refrente a peça.
        * \exception System.Exception Lançada o path de imagens não for encontrado.
        ***************************************************************************/
        public Image BuscarImagem(string imagem)
        {
            try
            {
                return Image.FromFile($"{FileImagesPath}/{imagem}");
            }
            catch
            {
                throw new System.Exception("Erro ao buscar imagens das peças");
            }
        }

        /** ************************************************************************
        * \brief Incrementa a quantidade de movimentos.
        * \details Função responsável por incrementar a  quantidade de movimentos da
        * peça.
        ***************************************************************************/
        public void IncrementarQuantidadeDeMovimentos()
        {
            QuantidadeDeMovimentos++;
        }

        /** ************************************************************************
        * \brief Verifica posição válida.
        * \details Função responsável por verificar se uma determinada posição é 
        * válida.
        * \param posicao Posição a ser verificada.
        * \return 'true' se a posição for válida, 'false' se for inválida.
        ***************************************************************************/
        public bool PosicaoValida(Posicao posicao)
        {
            return posicao.Linha <= 7 && posicao.Linha >= 0 && posicao.Coluna <= 7 && posicao.Coluna >= 0;
        }

        /** ************************************************************************
        * \brief Verifica se pode mover.
        * \details Função responsável por verificar se a peça pode ser movida para
        * uma determinada posição.
        * \param posicao Posição a ser verificada.
        * \return 'true' se a peça puder ser movida para a posição informada, 'false' 
        * se não.
        ***************************************************************************/
        public bool PodeMover(Posicao posicao)
        {
            if (PosicaoValida(posicao))
            {
                Peca peca = TabuleiroXadrez.AcessarPeca(posicao);
                return (peca == null || peca.CorDaPeca != CorDaPeca);
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
        public abstract bool[,] MovimentosPossiveis();

    }
}
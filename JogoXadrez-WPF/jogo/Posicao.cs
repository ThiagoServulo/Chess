namespace JogoXadrez_WPF
{
    class Posicao
    {
        public int Linha { get; set; }
        public int Coluna { get; set; }

        public Posicao(int linha, int coluna)
        {
            DefinirPosicao(linha, coluna);
        }

        public void DefinirPosicao(int linha, int coluna)
        {
            Linha = linha;
            Coluna = coluna;
        }

        public override string ToString()
        {
            return $"[{Linha},{Coluna}]";
        }
    }
}

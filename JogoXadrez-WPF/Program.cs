using System;
using System.Windows.Forms;

namespace JogoXadrez_WPF
{
    static class Program
    {
        [STAThread]

        /** ************************************************************************
        * \brief Função Main.
        * \details Entry point da aplicação.
        ***************************************************************************/
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Tabuleiro());
        }

    }
}

using System;
using System.Windows.Forms;

namespace JogoXadrez_WPF
{
    static class Program
    {
        [STAThread]

        /** ************************************************************************
        * \brief Fun��o Main.
        * \details Entry point da aplica��o.
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

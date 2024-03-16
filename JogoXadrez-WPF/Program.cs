using System;
using System.Windows.Forms;

namespace Chess
{
    static class Program
    {
        [STAThread]

        /** ************************************************************************
        * \brief Main function.
        * \details Entry point of the application.
        ***************************************************************************/
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Board());
        }

    }
}

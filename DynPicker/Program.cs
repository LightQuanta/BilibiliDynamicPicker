using System;
using System.Windows.Forms;

namespace DynPicker
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //检查个锤子跨线程调用，冲突是不可能冲突的，这辈子都不可能冲突的
            Control.CheckForIllegalCrossThreadCalls = false;
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());
        }
    }
}

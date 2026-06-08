using System;
using System.Windows.Forms;

namespace IDK2
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Db.EnsureDatabase();
            if (args != null && args.Length > 0 && args[0].Equals("smoketest", StringComparison.OrdinalIgnoreCase))
            {
                AdminService.SmokeTest();
                return;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}

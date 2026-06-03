using Microsoft.EntityFrameworkCore;
using TaskBoard.Data;

namespace TaskBoard
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Veritabanını oluştur / Migration'ları uygula
            using (var db = new AppDbContext())
            {
                db.Database.Migrate();
            }

            Application.Run(new LoginForm());
        }
    }
}
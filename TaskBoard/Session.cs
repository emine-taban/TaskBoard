using TaskBoard.Data.Models;

namespace TaskBoard
{
    /// <summary>
    /// Oturum açan kullanıcı bilgisini uygulama genelinde tutar.
    /// </summary>
    public static class Session
    {
        public static User? CurrentUser { get; set; }

        public static bool IsAdmin => CurrentUser?.Role == UserRole.Admin;

        public static void Clear() => CurrentUser = null;
    }
}

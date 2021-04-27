using Steamworks;

namespace Injected
{
    public static class SteamChecker
    {
        public static bool IsInitialized => SteamManager.Initialized;
        public static CSteamID SteamId => SteamUser.GetSteamID();
        public static uint AccountId => SteamId.GetAccountID().m_AccountID;

        public static HAuthTicket StartAuthSession()
        {
            byte[] array = new byte[1024];
            SteamUser.GetAuthSessionTicket(array, 1024, out uint num);
            SteamUser.BeginAuthSession(array, (int)num, SteamId);
            return new HAuthTicket(num);
        }

        public static void EndAuthSession(HAuthTicket ticket)
        {
            SteamUser.EndAuthSession(SteamId);
            SteamUser.CancelAuthTicket(ticket);
        }

        public static int IsAuthorisedGameInstance()
        {
            if (!IsInitialized) return -1;

            HAuthTicket t = StartAuthSession();
            uint vt = 1079260U;
            AppId_t ai_t = new AppId_t(vt);
            var e = EUserHasLicenseForAppResult.k_EUserHasLicenseResultHasLicense;
            uint ret = 1;

            if (SteamUser.UserHasLicenseForApp(SteamId, ai_t) == e) ret = 0;
            EndAuthSession(t);
            return (int)ret;
        }
    }
}

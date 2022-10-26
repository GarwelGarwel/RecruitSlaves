using RimWorld;
using Verse;

namespace RecruitSlaves
{
    enum LogLevel
    {
        Message = 0,
        Warning,
        Error
    };

    public static class Utility
    {
        public static void TryRecruit(Pawn recruiter, Pawn slave)
        {
            Log($"TryRecruit({recruiter}, {slave})");
            Log($"Slave's resistance: {slave.guest.Resistance}; will: {slave.guest.will}");
            slave.guest.SetGuestStatus(null);
            GenGuest.SlaveRelease(slave);
        }

        internal static void Log(string message, LogLevel logLevel = LogLevel.Message)
        {
            message = $"[RecruitSlaves] {message}";
            switch (logLevel)
            {
                case LogLevel.Message:
                    if (Prefs.DevMode || Prefs.LogVerbose)
                        Verse.Log.Message(message);
                    break;

                case LogLevel.Warning:
                    Verse.Log.Warning(message);
                    break;

                case LogLevel.Error:
                    Verse.Log.Error(message);
                    break;
            }
        }
    }
}

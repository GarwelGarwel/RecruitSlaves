using RimWorld;
using UnityEngine;
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
        public static Ideo NationPrimaryIdeo => Find.FactionManager.OfPlayer.ideos.PrimaryIdeo;

        public static float SuccessChance(Pawn recruiter, Pawn slave)
        {
            float effort = recruiter.GetStatValue(StatDefOf.NegotiationAbility);
            effort *= slave.needs.mood.thoughts.TotalOpinionOffset(recruiter) / 200 + 1;
            float difficulty = slave.GetStatValue(DefOf.RecruitmentDifficulty);
            Log($"Effort: {effort.ToStringPercent()}. Difficulty: {difficulty.ToStringPercent()}. Recruitment chance: {(effort / difficulty).ToStringPercent()}");
            return effort / difficulty;
        }

        public static void Recruit(Pawn recruiter, Pawn slave)
        {
#if DEBUG
            Log($"The recruitment was successful, but not enforcing it due to being in DEBUG mode.");
            return;
#else
            slave.guest.SetGuestStatus(null);
            GenGuest.SlaveRelease(slave);
            if (slave.Faction.IsPlayer)
                Messages.Message($"{slave} joined the colony as a free colonist.", slave, MessageTypeDefOf.PositiveEvent);
#endif
        }

        public static void TryRecruit(Pawn recruiter, Pawn slave)
        {
            Log($"TryRecruit({recruiter}, {slave})");
            Log($"Slave's resistance: {slave.guest.Resistance}; will: {slave.guest.will}");
            if (Rand.Chance(SuccessChance(recruiter, slave)))
                Recruit(recruiter, slave);
            else Log($"Failed to recruit {slave}.");
        }

        internal static void Log(string message, LogLevel logLevel = LogLevel.Message)
        {
            message = $"[RecruitSlaves] {message}";
            switch (logLevel)
            {
                case LogLevel.Message:
#if !DEBUG
                    if (Prefs.DevMode || Prefs.LogVerbose)
#endif
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

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
            float difficulty = slave.GetStatValue(DefOf.RecruitmentDifficulty) * Settings.RecruitmentDifficulty * Settings.RecruitmentDifficultyMultiplier_Base;
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
            recruiter.records.Increment(DefOf.SlavesRecruited);
            if (slave.Faction.IsPlayer)
                Find.LetterStack.ReceiveLetter($"{slave} recruited", $"{recruiter.NameFullColored} persuaded {slave.NameFullColored} to join {Faction.OfPlayer.NameColored} as a free colonist.", LetterDefOf.PositiveEvent, slave);
#endif
        }

        public static void TryRecruit(Pawn recruiter, Pawn slave)
        {
            Log($"TryRecruit({recruiter}, {slave})");
            Log($"Last suppression tick: {slave.mindState.lastSlaveSuppressedTick}; current tick: {Find.TickManager.TicksGame}");
            slave.mindState.lastSlaveSuppressedTick = Find.TickManager.TicksGame;
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
                    if (Settings.DebugLogging)
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

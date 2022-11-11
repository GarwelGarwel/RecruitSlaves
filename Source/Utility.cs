using RimWorld;
using System.Collections.Generic;
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
            TaleRecorder.RecordTale(TaleDefOf.Recruited, recruiter, slave);
            recruiter.records.Increment(DefOf.SlavesRecruited);
            slave.needs.mood.thoughts.memories.TryGainMemory(ThoughtDefOf.RecruitedMe, recruiter);
            if (slave.Faction.IsPlayer)
                Find.LetterStack.ReceiveLetter($"{slave} recruited", $"{recruiter.NameShortColored} persuaded {slave.NameShortColored} to join {Faction.OfPlayer.NameColored} as a free colonist.", LetterDefOf.PositiveEvent, slave);
            if (recruiter.InspirationDef == InspirationDefOf.Inspired_Recruitment)
                recruiter.mindState.inspirationHandler.EndInspiration(InspirationDefOf.Inspired_Recruitment);
#endif
        }

        public static void TryRecruit(Pawn recruiter, Pawn slave)
        {
            Log($"TryRecruit({recruiter}, {slave})");
            Log($"Last suppression tick: {slave.mindState.lastSlaveSuppressedTick}; current tick: {Find.TickManager.TicksGame}");
            slave.mindState.lastSlaveSuppressedTick = Find.TickManager.TicksGame;
            List<RulePackDef> extraPacks = new List<RulePackDef>();
            float chance = Mathf.Clamp(SuccessChance(recruiter, slave), 0.01f, 0.9f);
            if (recruiter.InspirationDef == InspirationDefOf.Inspired_Recruitment || Rand.Chance(chance))
            {
                Recruit(recruiter, slave);
                extraPacks.Add(RulePackDefOf.Sentence_RecruitAttemptAccepted);
            }
            else
            {
                Log($"Failed to recruit {slave}.");
                MoteMaker.ThrowText(slave.DrawPos, slave.Map, $"Recruitment failed ({chance.ToStringPercent()} success chance)", 8);
                extraPacks.Add(RulePackDefOf.Sentence_RecruitAttemptRejected);
            }
            Find.PlayLog.Add(new PlayLogEntry_Interaction(InteractionDefOf.RecruitAttempt, recruiter, slave, extraPacks));
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

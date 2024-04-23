using RimWorld;
using Verse;

using static RecruitSlaves.Utility;

namespace RecruitSlaves
{
    public class Settings : ModSettings
    {
        public static float RecruitmentDifficulty;
        public static int RecruitmentAttemptCooldown;
        public static float KeepMinSuppression;
        public static bool DebugLogging = Prefs.DevMode;

        public const float RecruitmentDifficultyMultiplier_Base = 10;
        public const int RecruitmentAttemptCooldown_Default = 24;
        public const float KeepMinSuppression_Default = 0.70f;

        public static int RecruitmentAttemptCooldownTicks => RecruitmentAttemptCooldown * GenDate.TicksPerHour;

        public Settings() => Reset();

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref RecruitmentDifficulty, "RecruitmentDifficulty", 1);
            Scribe_Values.Look(ref RecruitmentAttemptCooldown, "RecruitmentAttemptCooldown", RecruitmentAttemptCooldown_Default);
            Scribe_Values.Look(ref KeepMinSuppression, "KeepMinSuppression", KeepMinSuppression_Default);
            Scribe_Values.Look(ref DebugLogging, "DebugLogging");
            if (Scribe.mode == LoadSaveMode.LoadingVars)
                Print();
        }

        public static void Reset()
        {
            Log($"Resetting settings.");
            RecruitmentDifficulty = 1;
            RecruitmentAttemptCooldown = RecruitmentAttemptCooldown_Default;
            KeepMinSuppression = KeepMinSuppression_Default;
            Print();
        }

        public static void Print()
        {
            if (!DebugLogging)
                return;
            Log($"RecruitmentDifficulty: {RecruitmentDifficulty.ToStringPercent()}");
            Log($"RecruitmentAttemptCooldown: {RecruitmentAttemptCooldown.ToStringCached()} h");
            Log($"KeepMinSuppression: {KeepMinSuppression.ToStringPercent()}");
        }
    }
}

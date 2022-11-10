using RimWorld;
using Verse;

using static RecruitSlaves.Utility;

namespace RecruitSlaves
{
    public class Settings : ModSettings
    {
        public static float RecruitmentDifficulty = 1;
        public static int RecruitmentAttemptCooldown = RecruitmentAttemptCooldown_Default;
        public static bool DebugLogging = Prefs.DevMode;

        public const float RecruitmentDifficultyMultiplier_Base = 10;
        public const int RecruitmentAttemptCooldown_Default = 24;

        public static int RecruitmentAttemptCooldownTicks => RecruitmentAttemptCooldown * GenDate.TicksPerHour;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref RecruitmentDifficulty, "RecruitmentDifficulty", 1);
            Scribe_Values.Look(ref RecruitmentAttemptCooldown, "RecruitmentAttemptCooldown", RecruitmentAttemptCooldown_Default);
            Scribe_Values.Look(ref DebugLogging, "DebugLogging");
            if (Scribe.mode == LoadSaveMode.LoadingVars)
                Print();
        }

        public static void Reset()
        {
            Log($"Resetting settings.");
            RecruitmentDifficulty = 1;
            RecruitmentAttemptCooldown = RecruitmentAttemptCooldown_Default;
            Print();
        }

        static void Print()
        {
            if (!DebugLogging)
                return;
            Log($"RecruitmentDifficulty: {RecruitmentDifficulty}");
            Log($"RecruitmentAttemptCooldown: {RecruitmentAttemptCooldown}");
        }
    }
}

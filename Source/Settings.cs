using Verse;

using static RecruitSlaves.Utility;

namespace RecruitSlaves
{
    public class Settings : ModSettings
    {
        public static float RecruitmentDifficultyMultiplier = RecruitmentDifficultyMultiplier_Default;
        public static bool DebugLogging = Prefs.DevMode;

        internal const float RecruitmentDifficultyMultiplier_Default = 5;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref RecruitmentDifficultyMultiplier, "RecruitmentDifficultyMultiplier", RecruitmentDifficultyMultiplier_Default);
            Scribe_Values.Look(ref DebugLogging, "DebugLogging");
            if (Scribe.mode == LoadSaveMode.LoadingVars)
                Print();
        }

        public static void Reset()
        {
            Log($"Resetting settings.");
            RecruitmentDifficultyMultiplier = RecruitmentDifficultyMultiplier_Default;
            Print();
        }

        static void Print()
        {
            if (!DebugLogging)
                return;
            Log($"RecruitmentDifficultyMultiplier: {RecruitmentDifficultyMultiplier}");
        }
    }
}

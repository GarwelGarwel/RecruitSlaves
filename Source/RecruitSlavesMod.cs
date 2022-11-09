using UnityEngine;
using Verse;

namespace RecruitSlaves
{
    public class RecruitSlavesMod : Mod
    {
        public RecruitSlavesMod(ModContentPack content) : base(content) => GetSettings<Settings>();

        public override string SettingsCategory() => "Recruit Slaves";

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard content = new Listing_Standard();
            content.Begin(inRect);

            Settings.RecruitmentDifficultyMultiplier = Mathf.Round(content.SliderLabeled($"Recruitment Difficulty Multiplier: {Settings.RecruitmentDifficultyMultiplier}", Settings.RecruitmentDifficultyMultiplier, 0.1f, 10, tooltip: $"Relative difficulty of successfully recruiting a slave. Default: {Settings.RecruitmentDifficultyMultiplier_Default}") * 10) / 10;
            content.CheckboxLabeled("Debug Logging", ref Settings.DebugLogging, "Check to enable verbose logging; it is necessary to report bugs");

            if (content.ButtonText("Reset to default"))
                Settings.Reset();

            content.End();
        }
    }
}

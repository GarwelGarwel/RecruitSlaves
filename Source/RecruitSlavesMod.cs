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

            Settings.RecruitmentDifficulty = Mathf.Round(Mathf.Pow(10, content.SliderLabeled(
                $"Recruitment difficulty multiplier: x{(Settings.RecruitmentDifficulty * Settings.RecruitmentDifficultyMultiplier_Base).ToStringDecimalIfSmall()}",
                Mathf.Log10(Settings.RecruitmentDifficulty),
                -1,
                1,
                0.3f,
                $"The chance to recruit a slave is divided by this number.\n\nDefault: {Settings.RecruitmentDifficultyMultiplier_Base}.")) * Settings.RecruitmentDifficultyMultiplier_Base) / Settings.RecruitmentDifficultyMultiplier_Base;

            content.CheckboxLabeled("Storyteller affects recruitment difficulty", ref Settings.UsePopulationIntent, "Recruitment difficulty takes into account storyteller's population intent (whether it wants to increase or decrease population).\nDefault: On.");

            Settings.RecruitmentAttemptCooldown = Mathf.RoundToInt(content.SliderLabeled(
                $"Recruitment attempt cooldown: {Settings.RecruitmentAttemptCooldown.ToStringCached()} h",
                Settings.RecruitmentAttemptCooldown,
                0,
                120,
                0.3f,
                $"How many hours should pass from the last interaction with the slave to try to recruit them.\n\nDefault: {Settings.RecruitmentAttemptCooldown_Default.ToStringCached()} h.") / 6) * 6;

            Settings.KeepMinSuppression = Mathf.Round(content.SliderLabeled($"Keep minimum suppression: {(Settings.KeepMinSuppression > 0 ? Settings.KeepMinSuppression.ToStringPercent() : "off")}",
                Settings.KeepMinSuppression,
                0,
                1,
                0.3f,
                $"Slave interaction mode will automatically switch to \"Suppress\" after an unsuccessful recruit attempt if the slave's Suppression is below this value.\n\nDefault: {Settings.KeepMinSuppression_Default.ToStringPercent()}.") * 20) / 20;

            content.CheckboxLabeled("Debug logging", ref Settings.DebugLogging, "Check to enable verbose logging; it is necessary to report bugs.");

            if (content.ButtonText("Reset to default"))
                Settings.Reset();

            content.End();
        }

        public override void WriteSettings()
        {
            base.WriteSettings();
            Settings.Print();
        }
    }
}

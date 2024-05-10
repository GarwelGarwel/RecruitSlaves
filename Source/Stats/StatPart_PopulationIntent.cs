using RimWorld;
using Verse;

namespace RecruitSlaves
{
    public class StatPart_PopulationIntent : StatPart_Curve
    {
        protected override bool AppliesTo(StatRequest req) => Settings.UsePopulationIntent;

        protected override float CurveXGetter(StatRequest req) => StorytellerUtilityPopulation.PopulationIntent;

        protected override string ExplanationLabel(StatRequest req) => Prefs.DevMode ? $"Storyteller intent ({CurveXGetter(req).ToStringWithSign()})" : "Storyteller";
    }
}

using RimWorld;
using Verse;

namespace RecruitSlaves
{
    public class StatPart_PopulationIntent : StatPart_Curve
    {
        protected override bool AppliesTo(StatRequest req) => true;

        protected override float CurveXGetter(StatRequest req) => StorytellerUtilityPopulation.PopulationIntent;//PawnsFinder.AllMapsCaravansAndTravelingTransportPods_Alive_Colonists_NoSlaves.Count;

        protected override string ExplanationLabel(StatRequest req) => $"Storyteller intent ({CurveXGetter(req).ToStringWithSign()})";
    }
}

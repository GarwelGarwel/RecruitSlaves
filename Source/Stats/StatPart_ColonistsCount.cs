using RimWorld;
using Verse;

namespace RecruitSlaves
{
    public class StatPart_ColonistsCount : StatPart_Curve
    {
        protected override bool AppliesTo(StatRequest req) => true;

        protected override float CurveXGetter(StatRequest req) => PawnsFinder.AllMapsCaravansAndTravelingTransportPods_Alive_Colonists_NoSlaves.Count;

        protected override string ExplanationLabel(StatRequest req) => $"{((int)CurveXGetter(req)).ToStringCached()} free colonists";
    }
}

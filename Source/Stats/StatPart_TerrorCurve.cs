using RimWorld;
using Verse;

namespace RecruitSlaves
{
    public class StatPart_TerrorCurve : StatPart_Curve
    {
        protected override bool AppliesTo(StatRequest req) => req.Thing is Pawn pawn && pawn.IsSlave;

        protected override float CurveXGetter(StatRequest req) => ((Pawn)req.Thing).GetTerrorLevel();

        protected override string ExplanationLabel(StatRequest req) => $"Terror ({CurveXGetter(req).ToStringPercent()})";
    }
}

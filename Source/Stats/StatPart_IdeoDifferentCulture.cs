using RimWorld;
using Verse;

namespace RecruitSlaves
{
    public class StatPart_IdeoDifferentCulture : StatPart_FactorOffset
    {
        protected override bool AppliesTo(StatRequest req) => req.Thing is Pawn pawn && pawn.Ideo != null && pawn.Ideo.culture != Utility.NationPrimaryIdeo.culture;

        protected override string ExplanationLabel(StatRequest req) => "Different culture";
    }
}

using RimWorld;
using Verse;

namespace RecruitSlaves
{
    public class StatPart_IdeoPrecept : StatPart_FactorOffset
    {
        public PreceptDef precept;
        public bool onlyIfDifferentIdeo;

        protected override bool AppliesTo(StatRequest req) =>
            req.Thing is Pawn pawn && pawn.Ideo != null && (!onlyIfDifferentIdeo || pawn.Ideo != Utility.NationPrimaryIdeo) && pawn.Ideo.HasPrecept(precept);

        protected override string ExplanationLabel(StatRequest req) => $"{precept.issue.LabelCap}: {precept.LabelCap}";
    }
}

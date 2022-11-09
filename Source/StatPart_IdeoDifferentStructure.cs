using RimWorld;
using Verse;

namespace RecruitSlaves
{
    public class StatPart_IdeoDifferentStructure : StatPart_FactorOffset
    {
        protected override bool AppliesTo(StatRequest req) => req.Thing is Pawn pawn && pawn.Ideo != null && pawn.Ideo.StructureMeme != Utility.NationPrimaryIdeo.StructureMeme;

        protected override string ExplanationLabel(StatRequest req) => "Different ideoligion structure";
    }
}

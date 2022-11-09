using RimWorld;
using Verse;

namespace RecruitSlaves
{
    public class StatPart_IdeoMeme : StatPart_FactorOffset
    {
        public MemeDef meme;
        public bool onlyIfDifferentIdeo;

        protected override bool AppliesTo(StatRequest req) => req.Thing is Pawn pawn && pawn.Ideo != null && pawn.Ideo.HasMeme(meme) && (!onlyIfDifferentIdeo || pawn.Ideo != Utility.NationPrimaryIdeo);

        protected override string ExplanationLabel(StatRequest req) => meme.LabelCap;
    }
}

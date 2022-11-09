using RimWorld;
using Verse;

namespace RecruitSlaves
{
    public class StatPart_ColonistRelations : StatPart_FactorOffset
    {
        public PawnRelationDef relation;

        Pawn GetFirstRelationPawn(Pawn pawn) => pawn.relations.GetFirstDirectRelationPawn(relation, p => p.IsFreeNonSlaveColonist);

        protected override bool AppliesTo(StatRequest req) => req.Thing is Pawn pawn && pawn.relations != null && GetFirstRelationPawn(pawn) != null;

        protected override string ExplanationLabel(StatRequest req) => relation != null ? $"{relation.GetGenderSpecificLabelCap(GetFirstRelationPawn((Pawn)req.Thing))} is a colonist" : null;
    }
}

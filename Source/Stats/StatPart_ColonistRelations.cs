using RimWorld;
using Verse;

namespace RecruitSlaves
{
    public class StatPart_ColonistRelations : StatPart_FactorOffset
    {
        public PawnRelationDef relation;

        Pawn GetFirstRelationPawn(Pawn pawn) =>
            PawnsFinder.AllMapsCaravansAndTravelingTransportPods_Alive_Colonists_NoSlaves.Find(colonist => relation.Worker.InRelation(pawn, colonist));

        protected override bool AppliesTo(StatRequest req) => req.Thing is Pawn pawn && pawn.relations != null && relation != null && GetFirstRelationPawn(pawn) != null;

        protected override string ExplanationLabel(StatRequest req) => $"{relation.GetGenderSpecificLabelCap(GetFirstRelationPawn((Pawn)req.Thing))} is a colonist";
    }
}

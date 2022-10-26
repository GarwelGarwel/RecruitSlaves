using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using Verse.AI;

namespace RecruitSlaves
{
    public class WorkGiver_RecruitSlave : WorkGiver_Warden
    {
        public override Job JobOnThing(Pawn pawn, Thing t, bool forced = false)
        {
            if (!ModLister.IdeologyInstalled)
                return null;
            if (!ShouldTakeCareOfSlave(pawn, t))
                return null;
            Pawn target = t as Pawn;
            return null;
        }
    }
}

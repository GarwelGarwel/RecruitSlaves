using RimWorld;
using Verse;
using Verse.AI;

namespace RecruitSlaves
{
    public class WorkGiver_RecruitSlave : WorkGiver_Warden
    {
        public override Job JobOnThing(Pawn pawn, Thing t, bool forced = false)
        {
            if (!ShouldTakeCareOfSlave(pawn, t))
                return null;
            if (pawn.IsSlave)
                return null;
            Pawn target = t as Pawn;
            if (target == null || target.guest.slaveInteractionMode != DefOf.Recruit || target.Downed || !target.Awake() || !pawn.CanReserve(target))
                return null;
            if (!target.guest.Recruitable)
            {
                Utility.Log($"{target} is not recruitable. Disabling Recruit slave interaction mode.");
                target.guest.slaveInteractionMode = SlaveInteractionModeDefOf.Suppress;
                Messages.Message($"{target} is not recruitable.", target, MessageTypeDefOf.RejectInput, false);
                return null;
            }
            Need_Suppression suppression = target.needs.TryGetNeed<Need_Suppression>();
            if (suppression != null && suppression.CurLevel < Settings.KeepMinSuppression && target.guest.ScheduledForSlaveSuppression)
            {
                Utility.Log($"{target}'s suppression is {suppression.CurLevel.ToStringPercent()} < {Settings.KeepMinSuppression.ToStringPercent()}. Assigning a suppression job instead of recruitment.");
                return JobMaker.MakeJob(JobDefOf.SlaveSuppress, target);
            }
            if (target.mindState.lastSlaveSuppressedTick >= Find.TickManager.TicksGame - Settings.RecruitmentAttemptCooldownTicks)
                return null;
            return JobMaker.MakeJob(DefOf.Job_RecruitSlave, target);
        }
    }
}

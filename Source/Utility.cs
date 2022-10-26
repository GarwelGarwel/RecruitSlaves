﻿using RimWorld;
using Verse;

namespace RecruitSlaves
{
    enum LogLevel
    {
        Message = 0,
        Warning,
        Error
    };

    public static class Utility
    {
        public static float SuccessChance(Pawn recruiter, Pawn slave)
        {
            float res = recruiter.GetStatValueForPawn(StatDefOf.NegotiationAbility, slave);
            res *= slave.needs.mood.thoughts.TotalOpinionOffset(recruiter) / 200 + 1;
            Log($"Success chance: {res.ToStringPercent()}");
            return res;
        }

        public static void Recruit(Pawn recruiter, Pawn slave)
        {
            slave.guest.SetGuestStatus(null);
            GenGuest.SlaveRelease(slave);
            if (slave.Faction.IsPlayer)
                Messages.Message($"{slave} joined the colony as a free colonist.", slave, MessageTypeDefOf.PositiveEvent);
        }

        public static void TryRecruit(Pawn recruiter, Pawn slave)
        {
            Log($"TryRecruit({recruiter}, {slave})");
            Log($"Slave's resistance: {slave.guest.Resistance}; will: {slave.guest.will}");
            if (Rand.Chance(SuccessChance(recruiter, slave)))
                Recruit(recruiter, slave);
        }

        internal static void Log(string message, LogLevel logLevel = LogLevel.Message)
        {
            message = $"[RecruitSlaves] {message}";
            switch (logLevel)
            {
                case LogLevel.Message:
                    if (Prefs.DevMode || Prefs.LogVerbose)
                        Verse.Log.Message(message);
                    break;

                case LogLevel.Warning:
                    Verse.Log.Warning(message);
                    break;

                case LogLevel.Error:
                    Verse.Log.Error(message);
                    break;
            }
        }
    }
}
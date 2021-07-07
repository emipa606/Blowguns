using Verse;

namespace Blowguns
{
    // Token: 0x02000002 RID: 2
    internal class HealthUtility_ProgessiveBodySizeDependant
    {
        // Token: 0x06000002 RID: 2 RVA: 0x0000205C File Offset: 0x0000025C
        public static void AdjustSeverityPerSeverity(Pawn pawn, HediffDef hdDef, float sevOffset, string partToAffect)
        {
            if (sevOffset == 0f)
            {
                return;
            }

            var hediff = pawn.health.hediffSet.GetFirstHediffOfDef(hdDef);
            if (hediff != null)
            {
                var hediff2 = hediff;
                hediff2.Severity += sevOffset;
            }
            else
            {
                if (!(sevOffset > 0f))
                {
                    return;
                }

                hediff = HediffMaker.MakeHediff(hdDef, pawn);
                hediff.Severity = sevOffset;
                pawn.health.AddHediff(hediff,
                    pawn.RaceProps.body.AllParts.Find(x =>
                        x.def == DefDatabase<BodyPartDef>.GetNamed(partToAffect)));
            }
        }
    }
}
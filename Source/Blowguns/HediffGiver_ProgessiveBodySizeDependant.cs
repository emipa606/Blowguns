using Verse;

namespace Blowguns;

public class HediffGiver_ProgessiveBodySizeDependant : HediffGiver
{
    public string dependantHediffDef;

    public float maxSeverityFactor = 1f;

    public float minSeverityFactor = 0f;

    public string partToAffect;

    public float severityPerDayRecovering = -1f;

    public float severityPerSeverityPerDay = 1f;

    public override void OnIntervalPassed(Pawn pawn, Hediff cause)
    {
        var hediffSet = pawn.health.hediffSet;
        var firstHediffOfDef = hediffSet.GetFirstHediffOfDef(HediffDef.Named(dependantHediffDef));
        var sevOffset = severityPerDayRecovering * 0.00333333341f;
        if (hediffSet.HasHediff(HediffDef.Named(dependantHediffDef)))
        {
            var num = severityPerSeverityPerDay * 0.00333333341f;
            var severity = firstHediffOfDef.Severity;
            var bodySize = pawn.BodySize;
            var sevOffset2 = severity * num / bodySize;
            var num2 = minSeverityFactor * bodySize;
            var num3 = maxSeverityFactor * firstHediffOfDef.Severity / bodySize;
            if (!pawn.Dead && hediffSet.HasHediff(HediffDef.Named(dependantHediffDef)) &&
                !hediffSet.HasHediff(hediff))
            {
                HealthUtility_ProgessiveBodySizeDependant.AdjustSeverityPerSeverity(pawn, hediff, sevOffset2,
                    partToAffect);
            }
            else
            {
                if (pawn.Dead || !hediffSet.HasHediff(HediffDef.Named(dependantHediffDef)))
                {
                    return;
                }

                var firstHediffOfDef2 = hediffSet.GetFirstHediffOfDef(hediff);
                if (firstHediffOfDef.Severity > num2 && firstHediffOfDef2.Severity <= num3)
                {
                    HealthUtility_ProgessiveBodySizeDependant.AdjustSeverityPerSeverity(pawn, hediff,
                        sevOffset2, partToAffect);
                }
            }
        }
        else
        {
            HealthUtility_ProgessiveBodySizeDependant.AdjustSeverityPerSeverity(pawn, hediff, sevOffset,
                partToAffect);
        }
    }
}
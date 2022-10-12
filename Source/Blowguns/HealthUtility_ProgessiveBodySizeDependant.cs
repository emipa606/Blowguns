using Verse;

namespace Blowguns;

internal class HealthUtility_ProgessiveBodySizeDependant
{
    public static void AdjustSeverityPerSeverity(Pawn pawn, HediffDef hdDef, float sevOffset, string partToAffect)
    {
        if (sevOffset == 0f)
        {
            return;
        }

        var hediff = pawn.health.hediffSet.GetFirstHediffOfDef(hdDef);
        if (hediff != null)
        {
            hediff.Severity += sevOffset;
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
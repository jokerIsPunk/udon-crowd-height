
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

// inherit from RequireCondition
public class ExtensionExample : jokerispunk.CrowdHeight.RequireCondition
{
    // implement the required method and define whatever you want
    public override bool _GetLimiterApplyCondition()
    {
        System.DayOfWeek day = System.DateTime.Now.DayOfWeek;

        // e.g. this condition is only true on Saturdays https://www.youtube.com/watch?v=qFSJ6gyiffE
        return (day == System.DayOfWeek.Saturday);
    }
}

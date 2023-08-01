
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace jokerispunk.CrowdHeight
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class RequireConditionOr : RequireCondition
    {
        [SerializeField] bool negative = false;
        [SerializeField] RequireCondition[] conditions;

        // return true if any of the condition scripts return true, otherwise return false
        public override bool _GetLimiterApplyCondition()
        {
            foreach (RequireCondition cond in conditions)
            {
                if (!cond) continue;

                if (cond._GetLimiterApplyCondition())
                    return !negative;
            }

            return negative;
        }
    }
}

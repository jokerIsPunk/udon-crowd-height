
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;


namespace jokerispunk.CrowdHeight
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class RequireConditionAnd : RequireCondition
    {
        [SerializeField] bool negative = false;
        [SerializeField] RequireCondition[] conditions;

        // return false if any of the condition scripts return false, otherwise return true
        public override bool _GetLimiterApplyCondition()
        {
            foreach (RequireCondition cond in conditions)
            {
                if (!cond) continue;

                if (!cond._GetLimiterApplyCondition())
                    return negative;
            }

            return !negative;
        }
    }
}

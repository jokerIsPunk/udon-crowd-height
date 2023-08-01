
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace jokerispunk.CrowdHeight
{
    public abstract class RequireCondition : UdonSharpBehaviour
    {
        public abstract bool _GetLimiterApplyCondition();
    }
}

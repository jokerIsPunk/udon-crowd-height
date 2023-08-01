
using System.Reflection;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace jokerispunk.CrowdHeight
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class CrowdHeight : UdonSharpBehaviour
    {
        [SerializeField, Tooltip("Hard limits apply regardless of conditions and regardless of exemptions.")]
        float maxHeightHardLimit = 5f, minHeightHardLimit = 0.2f;
        [Tooltip("Soft scaling limits will never apply to these users. Copy and paste name strings from the VRC website.")]
        [SerializeField] string[] exemptList;
        private float minHeight = 0.2f, maxHeight = 5f;
        [Tooltip("Logic extensions. Height limiter will not apply if any of these scripts return false. See readme.")]
        [SerializeField] RequireCondition[] requiredConditions;
        [SerializeField] PoseFigure minHeightFigure, maxHeightFigure;
        private bool exempt = false;
        private VRCPlayerApi lp;
        private float pollPeriod = 3f;

        void Start()
        {
            // cache values
            lp = Networking.LocalPlayer;
            exempt = FindExempt();
            minHeight = minHeightFigure ? minHeightFigure.FigureHeight : minHeightHardLimit;
            maxHeight = maxHeightFigure ? maxHeightFigure.FigureHeight : maxHeightHardLimit;

            // initialize
            CheckHeightLimit();
            SendCustomEventDelayedSeconds(nameof(SlowLoop), pollPeriod);
        }

        private float MinHeight
        {
            get => minHeight;
        }

        private float MaxHeight
        {
            get => maxHeight;
        }

        public void SlowLoop()
        {
            // restart
            SendCustomEventDelayedSeconds(nameof(SlowLoop), pollPeriod);

            // code
            CheckHeightLimit();
        }

        public override void OnAvatarChanged(VRCPlayerApi player)
        {
            if (player.isLocal)
                CheckHeightLimit();
        }

        public override void OnAvatarEyeHeightChanged(VRCPlayerApi player, float newEyeHeightAsMeters)
        {
            if (player.isLocal)
                CheckHeightLimit();
        }

        private void CheckHeightLimit()
        {
            bool scalingNotSoftLimited = exempt || GetConditions();
            float instantMin = scalingNotSoftLimited ? minHeightHardLimit : MinHeight;
            float instantMax = scalingNotSoftLimited ? maxHeightHardLimit : MaxHeight;

            // check and set values
            if (lp.GetAvatarEyeHeightMinimumAsMeters() != instantMin)
                lp.SetAvatarEyeHeightMinimumByMeters(instantMin);
            //
            if (lp.GetAvatarEyeHeightMaximumAsMeters() != instantMax)
                lp.SetAvatarEyeHeightMaximumByMeters(instantMax);

            // check if height is out of range and adjust if needed
            float height = lp.GetAvatarEyeHeightAsMeters();
            if (height > instantMax)
                lp.SetAvatarEyeHeightByMeters(instantMax);
            else if (height < instantMin)
                lp.SetAvatarEyeHeightByMeters(instantMin);
            
            // check scaling permissions and re-enable if needed (it auto-disables if height is set)
            if (!lp.GetManualAvatarScalingAllowed())
                lp.SetManualAvatarScalingAllowed(true);
        }

        private bool GetConditions()
        {
            // scaling is limited if any of the required conditions return false, and only hard-limited otherwise
            foreach (RequireCondition cond in requiredConditions)
            {
                if (!cond) continue;

                if (!cond._GetLimiterApplyCondition())
                    return false;
            }

            return true;
        }

        private bool FindExempt()
        {
            string userName = Networking.LocalPlayer.displayName;
            int index = System.Array.IndexOf(exemptList, userName);
            return index >= 0;
        }
    }
}

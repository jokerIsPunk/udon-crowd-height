
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using jokerispunk.CrowdHeight;
using jokerispunk.Utilities;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class InBoxValue : RequireCondition
{
    [SerializeField] BoxCollider box;
    [Tooltip("Check to return true only if player's head is outside of the box."), SerializeField]
    bool negative = false;
    [Tooltip("Treat the player as if they're outside of the box when they move faster than this while inside the box. Allows players to pass briefly through the box without being affected."), SerializeField]
    float walkSpeedExemptionThreshold = 100f;
    private VRCPlayerApi lp;
    private bool initialized = false;

    private void _Initialize()
    {
        if (initialized) return;
        lp = Networking.LocalPlayer;
    }

    public override bool _GetLimiterApplyCondition()
    {
        if (!box)
            return negative;

        _Initialize();

        Vector3 headPos = lp.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).position;
        bool isInBox = jokerispunk.Utilities.Utilities._IsPointInBox(box, headPos);
        bool isWalking = lp.GetVelocity().sqrMagnitude > Mathf.Pow(walkSpeedExemptionThreshold, 2f);

        if (isInBox && !isWalking)
            return !negative;
        else
            return negative;
    }
}

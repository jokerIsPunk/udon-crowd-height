
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace jokerispunk
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class PoseFigure : UdonSharpBehaviour
    {
        [SerializeField] Transform eyePos;
        private float fallbackHeight = 2f;

        public float FigureHeight
        {
            get
            {
                if (eyePos)
                {
                    Vector3 body = eyePos.position - transform.position;
                    return body.magnitude;
                }

                return fallbackHeight;
            }
        }
    }
}

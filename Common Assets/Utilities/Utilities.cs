
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace jokerispunk.Utilities
{
    public class Utilities : UdonSharpBehaviour
    {
        public static bool _IsPointInBox(BoxCollider box, Vector3 worldPoint)
        {
            Vector3 localPoint = box.transform.InverseTransformPoint(worldPoint);
            float xHalf = box.size.x / 2f;
            float yHalf = box.size.y / 2f;
            float zHalf = box.size.z / 2f;

            if (localPoint.x > (box.center.x + xHalf))
                return false;
            else if (localPoint.x < (box.center.x - xHalf))
                return false;
            else if (localPoint.z > (box.center.z + zHalf))
                return false;
            else if (localPoint.z < (box.center.z - zHalf))
                return false;
            else if (localPoint.y > (box.center.y + yHalf))
                return false;
            else if (localPoint.y < (box.center.y - yHalf))
                return false;

            return true;
        }
    }
}

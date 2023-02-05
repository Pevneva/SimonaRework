using UnityEngine;

namespace CodeBase.CameraLogic
{
    public class CameraFollow : MonoBehaviour
    {
        private Transform _following;

        private void LateUpdate()
        {
            if (_following == null)
                return;
            
            transform.position = FollowingPosition();
        }

        public void Follow(GameObject following) => 
            _following = following.transform;

        private Vector3 FollowingPosition()
        {
            return new Vector3(_following.position.x, _following.position.y, -10);
        }
    }
}
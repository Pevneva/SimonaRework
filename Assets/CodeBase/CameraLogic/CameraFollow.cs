using System;
using UnityEngine;

namespace CodeBase.CameraLogic
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _following;

        private void LateUpdate()
        {
            transform.position = new Vector3(_following.position.x, _following.position.y, -10);
        }
    }
}
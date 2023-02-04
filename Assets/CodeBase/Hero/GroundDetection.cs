using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.Hero
{
    public class GroundDetection : MonoBehaviour
    {
        private const string Ground = "Ground";
        
        [SerializeField] private GameObject _checkingObject;
        [SerializeField] private bool _isGrounded;

        private float distanceX = 0.25f;
        private float distanceY = 0.2f;
        private float distanceYUp = 0.3f;

        public bool IsGrounded => _isGrounded;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("AAA");
            _isGrounded = true;
        }

        private void OnTriggerExit2D(Collider2D other) => 
            _isGrounded = false;

        public bool IsHeroGrounded()
        {
            var position = _checkingObject.transform.position; 
            var raycastHits = new List<RaycastHit2D>(Physics2D.RaycastAll(new Vector2(position.x - distanceX, position.y), Vector2.down, distanceY));
            var raycastHitsRight = new List<RaycastHit2D>(Physics2D.RaycastAll(new Vector2(position.x + distanceX, position.y), Vector2.down, distanceY));
            
            raycastHits.AddRange(raycastHitsRight);

            var raycastHitsUp = Physics2D.RaycastAll(position, Vector2.up, distanceYUp);
            
            if (raycastHits != null)
            {
                foreach (var rh in raycastHits)
                {
                    if (rh.transform.CompareTag(Ground))
                    {
                        if (raycastHitsUp != null)
                        {
                            foreach (var rhu in raycastHitsUp)
                            {
                                if (rhu.transform.CompareTag(Ground))
                                {
                                    _isGrounded = false;
                                    return _isGrounded;
                                }
                            }
                        }

                        _isGrounded = true;
                        return _isGrounded;
                    }
                }
            }

            return _isGrounded = false;
        }
    }
}
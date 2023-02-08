using UnityEngine;

public class GroundDetecter : MonoBehaviour
{
    [SerializeField] private float _detectionRange = 1.5f;
    [SerializeField] private LayerMask _layer;

    public bool IsGrounded() => 
        Physics2D.Raycast(transform.position, Vector3.down, _detectionRange, _layer);
}

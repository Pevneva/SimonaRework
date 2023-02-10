using UnityEngine;

namespace CodeBase.Enemy
{
    public class AnimationAlongHero : MonoBehaviour
    {
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private Mover _mover;
        [SerializeField] private ChaseHero _chase;

        private void Update()
        {
            if (ShouldMove())
                _animator.Move(_mover.Speed);
            else
                _animator.StopMoving();
        }

        private bool ShouldMove() => 
            _mover.Direction.magnitude > 0 && _chase.HeroInitialized() && _chase.HeroNotReached();
    }
}
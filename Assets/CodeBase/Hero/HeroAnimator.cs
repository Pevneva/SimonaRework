using System;
using CodeBase.Hero;
using CodeBase.Logic;
using UnityEngine;

public class HeroAnimator : MonoBehaviour, IAnimationStateReader
{
    private static readonly int MoveHash = Animator.StringToHash("Walk");
    private static readonly int AttackHash = Animator.StringToHash("Attack");
    private static readonly int HitHash = Animator.StringToHash("Hit");
    private static readonly int DieHash = Animator.StringToHash("Die");
    private static readonly int StartJumpHash = Animator.StringToHash("StartJump");
    private static readonly int EndJumpHash = Animator.StringToHash("IsGrounded");
    private static readonly int FallHash = Animator.StringToHash("StartFall");

    private readonly int _idleStateHash = Animator.StringToHash("Idle");
    private readonly int _idleStateFullHash = Animator.StringToHash("Base Layer.Idle");
    private readonly int _attackStateHash = Animator.StringToHash("Attack Normal");
    private readonly int _walkingStateHash = Animator.StringToHash("Run");
    private readonly int _deathStateHash = Animator.StringToHash("Die");

    public event Action<AnimatorState> StateEntered;
    public event Action<AnimatorState> StateExited;

    public AnimatorState State { get; private set; }

    public Animator Animator;
    public HeroMover HeroMover;


    private void Update() =>
        Animator.SetBool(MoveHash, HeroMover.MovementVector.magnitude > 0);

    public bool IsAttacking => State == AnimatorState.Attack;

    public void PlayHit() => Animator.SetTrigger(HitHash);

    public void PlayAttack() => Animator.SetTrigger(AttackHash);

    public void PlayDeath() => Animator.SetTrigger(DieHash);

    public void ResetToIdle() => Animator.Play(_idleStateHash, -1);

    public void PlayStartJump() => Animator.Play(StartJumpHash);


    public void PlayEndJump(bool isGrounded) => 
        Animator.SetBool(EndJumpHash, isGrounded);

    public void PlayFall() => Animator.SetTrigger(FallHash);

    public void EnteredState(int stateHash)
    {
        State = StateFor(stateHash);
        StateEntered?.Invoke(State);
    }

    public void ExitedState(int stateHash) =>
        StateExited?.Invoke(StateFor(stateHash));

    private AnimatorState StateFor(int stateHash)
    {
        AnimatorState state;
        if (stateHash == _idleStateHash)
            state = AnimatorState.Idle;
        else if (stateHash == _attackStateHash)
            state = AnimatorState.Attack;
        else if (stateHash == _walkingStateHash)
            state = AnimatorState.Walking;
        else if (stateHash == _deathStateHash)
            state = AnimatorState.Died;
        else
            state = AnimatorState.Unknown;

        return state;
    }
}
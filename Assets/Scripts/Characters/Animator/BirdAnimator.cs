using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BirdAnimator : MonoBehaviour
{
    private readonly int Fly = Animator.StringToHash("IsFly");

    private Animator _animator;

    private void Awake() =>
        _animator = GetComponent<Animator>();

    public void PlayFlyAnimation() =>
        _animator.SetBool(Fly, true);

    public void StopFlyAnimation() =>
        _animator.SetBool(Fly, false);
}

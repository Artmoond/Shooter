using UnityEngine;
[RequireComponent(typeof(Animator))]
public class PlayerAnimationController : MonoBehaviour
{
    private Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    public void SetSpeedInAnimator(float speed)
    {
        _animator.SetFloat("Speed", speed);
    }
}

using UnityEngine;

public class HeroAnimation : MonoBehaviour
{
    [SerializeField] Animator animator;

    public static int Idle = Animator.StringToHash("Idle");
    public static int Run = Animator.StringToHash("Run");
    public static int Jump = Animator.StringToHash("Jump");
    public static int Fall = Animator.StringToHash("Fall");
    public static int Dash = Animator.StringToHash("Dash");
    public static int Death = Animator.StringToHash("Death");
    public static int Win = Animator.StringToHash("Win");



    public void SetState(int state)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).shortNameHash == state) return;
        animator.CrossFade(state, 0, 0);
    }
}

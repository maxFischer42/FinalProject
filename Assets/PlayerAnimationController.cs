using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [Tooltip("0 = Idle, 1 = Sprint, 2 = Roll, 3 = Dash, 4 = Attack1, 5 = Walk, 6 = Slide")]
    public AnimationObject[] animations = new AnimationObject[5];
    private Animator animator;
    private SpriteRenderer renderer;

    private void Start()
    {
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
    }

    public void ChangeAnimation(int id)
    {
        AnimationObject input = animations[id];
        RuntimeAnimatorController anim = input.animation;
        Material mat = input.material;
        animator.runtimeAnimatorController = anim;
        renderer.material = mat;
    }
}

using System;
using UnityEngine;

public class OnAnimationCompleteNode : Node
{
    Animator a;
    string n;

    public OnAnimationCompleteNode(Animator animator, string animationName)
    {
        this.a = animator;
        this.n = animationName;
    }

    public override NodeState Evaluate()
    {
        if(a.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 & a.GetCurrentAnimatorClipInfo(0)[0].clip.name.Equals(n))
            return SetState(NodeState.SUCCESS);
        return SetState(NodeState.RUNNING);
    }

    public override void Reset()
    {
        base.Reset();
    }
}
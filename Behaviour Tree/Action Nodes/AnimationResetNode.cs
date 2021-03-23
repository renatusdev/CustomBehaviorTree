using UnityEngine;

public class AnimationResetNode : Node
{
    Animator a;
    string[] list;

    public AnimationResetNode(Animator animator, params string[] list)
    {
        this.a = animator;
        this.list = list;
    }

    public override NodeState Evaluate()
    {
        foreach(string trigger in list)
            a.ResetTrigger(trigger);
        return SetState(NodeState.SUCCESS);
    }
}
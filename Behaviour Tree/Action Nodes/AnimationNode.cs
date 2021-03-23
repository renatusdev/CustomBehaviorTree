using UnityEngine;

public class AnimationNode : Node
{
    Animator anim;
    string trigger;
    
    public AnimationNode(Animator animator, string trigger)
    {
        this.anim = animator;
        this.trigger = trigger;
    }
    
    public override NodeState Evaluate()
    {
        anim.SetTrigger(trigger);
        return SetState(NodeState.SUCCESS);
    }
}   
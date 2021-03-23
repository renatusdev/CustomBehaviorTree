using UnityEngine;

public class FlagActivator : Node
{
    FlagNode flag;

    public FlagActivator(FlagNode flag)
    {
        this.flag = flag;
    }

    public override NodeState Evaluate()
    {
        flag.Flag();
        return NodeState.SUCCESS;
    }
}
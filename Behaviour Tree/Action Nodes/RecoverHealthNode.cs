using UnityEngine;

public class RecoverHealthNode : Node
{
    Entity e;
    int multiplier;

    public RecoverHealthNode(Entity e, int multiplier)
    {
        this.e = e; 
        this.multiplier = multiplier;
    }

    public override NodeState Evaluate()
    {
        e.SetHP(Time.deltaTime * multiplier);
        return NodeState.SUCCESS;
    } 
}
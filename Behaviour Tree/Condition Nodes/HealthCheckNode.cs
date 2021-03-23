public class HealthCheckNode : Node
{
    Entity e;
    int threshold;
    bool below;

    public HealthCheckNode(Entity e, int threshold, bool below)
    {
        this.e = e; this.threshold = threshold; this.below = below; 
    }

    public override NodeState Evaluate() 
    {
        if(below)
            return e.GetHP() <= threshold ? NodeState.SUCCESS : NodeState.FAILURE;
        else
            return e.GetHP() >= threshold ? NodeState.SUCCESS : NodeState.FAILURE; 
    }
}
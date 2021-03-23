using UnityEngine;

public class TimerNode : Node
{
    float ogTime;
    float timer;

    public TimerNode(float time)
    {
        this.ogTime = time;
        this.timer = ogTime;
    }

    public override NodeState Evaluate()
    {
        if(timer <= 0)
        {
            return SetState(NodeState.SUCCESS);
        }
        else
            timer -= Time.deltaTime;
            return SetState(NodeState.RUNNING);
    }

    public override void Reset()
    {
        base.Reset();
        this.timer = ogTime;
    }
}
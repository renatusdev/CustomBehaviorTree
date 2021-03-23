using UnityEngine;

public class DestroyNode : Node
{
    GameObject obj;
    float timer;

    public DestroyNode(GameObject gameObject)
    {
        this.obj = gameObject;
        this.timer = 0;
    }

    public DestroyNode(GameObject gameObject, float timer)
    {
        this.obj = gameObject;
        this.timer = timer;
    }

    public override NodeState Evaluate()
    {
        if(timer.Equals(0))
            MonoBehaviour.Destroy(obj);
        else
            MonoBehaviour.Destroy(obj, timer);

        return SetState(NodeState.SUCCESS); 
    }    
}
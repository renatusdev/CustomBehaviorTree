using UnityEngine;

public class FindCover : PhysicsNode
{
    Entity e;
    Transform target;
    int distance;
    float submersion;

    public FindCover(Entity  e, Transform target, int distance) : base()
    {
        this.e = e; this.target = target; 
        this.distance = distance; this.submersion = 0;
    }

    public FindCover(Entity  e, Transform target, int distance, float submersion)
    {
        this.e = e; this.target = target;
        this.distance = distance; this.submersion = submersion;
    }

    public override NodeState Evaluate()
    {
        Vector3 pos = e.GetPosition();
        Vector3 cover = pos - target.position;

        cover.Normalize();
        cover *= distance;
        cover.y = pos.y - submersion;
        cover += pos;

        if(Physics.CheckSphere(cover, 2, LayerMask.GetMask("Obstacle")))
            Debug.Log("Entity found cover in a blocked position. Work around has not been implement.");
        
        e.target.position = cover;
        return NodeState.SUCCESS;
    }
}
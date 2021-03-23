using UnityEngine;
using System.Collections.Generic;

public class PatrolNode : PhysicsNode
{
    public Queue<Vector3> path;

    Rigidbody r;
    int speed;
    float stopRange;
    public Vector3 currPoint;

    public PatrolNode(Rigidbody r, Queue<Vector3> path, int speed, float stopRange) : base()
    {
        this.r = r;
        this.path = path;
        this.speed = speed;
        this.stopRange = stopRange;

        currPoint = path.Dequeue();
        path.Enqueue(currPoint);
    }
    
    public PatrolNode(Rigidbody r, int speed, float stopRange, int waypoints, int pathSize, float entityRadius, float maxSubmersion)
    {
        this.r = r;
        this.speed = speed;
        this.stopRange = stopRange;

        if(waypoints < 1)
        {
            Debug.LogError("Path must have at least one waypoint. Recalculating.");
            waypoints = 5;
        }
        
        if(-maxSubmersion <= Settings.OCEAN_MAX_DEPTH + entityRadius)
        {
            Debug.LogError("Submersion for entity path will hit sand. Recalculating.");
            maxSubmersion = Settings.OCEAN_MAX_DEPTH + entityRadius + 4;
        }

        path = PathGenerator(waypoints, pathSize, entityRadius, maxSubmersion); 

        currPoint = path.Dequeue();
        path.Enqueue(currPoint);
    }

    public override NodeState Evaluate()
    {   
        // If a target is reached
        if(Vector3.Distance(r.position, currPoint) <= stopRange)
        {
            // If more targets are to be reached
            if(path.Count != 0)
            {
                // Get new target
                currPoint = path.Dequeue();
                path.Enqueue(currPoint);
                return SetState(NodeState.RUNNING);
            }
            // Else there are no more targets
            else
            {
                return SetState(NodeState.SUCCESS);
                // TODO: Reset path.
            }
        }
        else
        {
            Vector3 dir = currPoint - r.position;
            Quaternion rot = Quaternion.LookRotation(dir);

            r.MoveRotation(Quaternion.Slerp(r.rotation, rot, Time.fixedDeltaTime * speed * 1.5f));
            r.MovePosition(r.position + r.transform.forward * Time.fixedDeltaTime * speed);

            return SetState(NodeState.RUNNING);
        }
    }

    Queue<Vector3> PathGenerator(int i, int size, float entityRadius, float maxSubmersion)
    {
        Queue<Vector3> gPath = new Queue<Vector3>();
        int error_threshold = 0;

        while(i > 0)
        {
            Vector3 xz = r.position + Random.onUnitSphere * size;
            float y = Mathf.PerlinNoise(xz.x, xz.z) * -maxSubmersion;

            Vector3 point = new Vector3(xz.x, y, xz.z);
            
            // If area is blocked
            if(!Physics.CheckSphere(point, entityRadius * 1.5f, LayerMask.GetMask("Obstacle")))
            {
                i--;
                gPath.Enqueue(point);
            }
            else
            {
                if(error_threshold++ > 100)
                {
                    Debug.LogError("Path reached overflow error, Stopping Path Generation.");
                    i = 0;
                }
            }
        }

        return gPath;
    }
}
using UnityEngine;

public class ColorFlashNode : Node
{
    private readonly static int maxSpeed = 10;

    Material mat;
    Color color;
    float originalTime;
    float speed;    

    Color originalColor;
    float t;
    int i;
    
    public ColorFlashNode(Material mat, Color color, float time, float speed)
    {
        this.mat = mat; 
        this.color = color;
        this.speed = speed;
        this.originalTime = time;

        this.originalColor = mat.color;
        this.t = 0;
        this.i = 0;
    }

    public override NodeState Evaluate()
    {
        if(GetState().Equals(NodeState.SUCCESS))
            return NodeState.SUCCESS;

        t += Time.deltaTime;

        if(t*i >= originalTime)
        {
            mat.color = Color.Lerp(mat.color, originalColor, t*maxSpeed*speed*4);

            if(mat.color.b == originalColor.b)
            {
                i = 0;
                t = 0;
                return SetState(NodeState.SUCCESS);
            }

            return SetState(NodeState.RUNNING);
        }
        else
        {
            mat.color = Color.Lerp(originalColor, color, t*maxSpeed*speed);
            
            if(mat.color.b == color.b)
            {
                i++;
                t = 0;
                mat.color = originalColor;
            }

            return  SetState(NodeState.RUNNING);
        }
    }
}

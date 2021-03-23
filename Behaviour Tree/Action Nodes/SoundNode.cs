public class SoundNode : Node
{
    Sound s;

    public SoundNode(Sound s) { this.s = s; }
    
    public override NodeState Evaluate()
    {
        SoundManager.Play(s);
        return NodeState.SUCCESS;
    }
}
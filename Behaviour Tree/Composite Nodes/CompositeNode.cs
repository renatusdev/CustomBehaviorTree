using UnityEngine;
using System.Collections.Generic;

public class CompositeNode : Node
{
    public List<Node> nodes { get; set;}
    protected int i;

    public override void Reset()
    {
        foreach(Node n in nodes)
            n.Reset();
    }
}
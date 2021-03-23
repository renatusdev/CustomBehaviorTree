using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeState {RUNNING, SUCCESS, FAILURE}

[System.Serializable]
public abstract class Node
{
    protected NodeState state;
    public bool isPhysicsBased;

    public virtual NodeState Evaluate() { return NodeState.RUNNING; }

    public virtual void Reset() { this.state  = NodeState.RUNNING; }

    public NodeState GetState() { return state; }
    public NodeState SetState(NodeState state) {this.state = state; return state; }
}
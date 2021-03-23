using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaMonster : Entity
{
    public readonly static int consideredLowHP = 40;
    public readonly static int hpRecoveryMultiplier = 2;
    public readonly static int atkRange = 16;
    
    [Range(0,1)] public float accuracy;
    [Range(0,30)] public int speed;
    
    public bool debug;

    Rigidbody rB;

    Queue<Vector3> path;
    Vector3[] debugPath;
    Rigidbody r;
    Vector3 test;

    Tree topNode;

    void Start()
    {
        rB = GetComponent<Rigidbody>();

        GenerateBehaviors();
    }

    void GenerateBehaviors()
    {   
        Animator a = GetAnimator();

        ////////////////////////////////// Movement //////////////////////////////////

        EvadeNode evade = new EvadeNode(this.rB, speed, 0.4f, 25);
        IsBlockedNode isBlocked = new IsBlockedNode(transform, evade, 1, 1);
        MoveNode move = new MoveNode(rB, target, speed, atkRange);

        SequenceNode evading = new SequenceNode(new List<Node>(){isBlocked, evade});
        SelectorNode movement = new SelectorNode(new List<Node>(){evading, move}); 

        ////////////////////////////////// Death //////////////////////////////////

        HealthCheckNode isDead = new HealthCheckNode(this, 0, true);
        ClampSuccessNode isDeadClamped = new ClampSuccessNode(isDead);
        AnimationNode deathAnim = new AnimationNode(a, "Die");
        OnAnimationCompleteNode onDeathAnimComplete = new OnAnimationCompleteNode(a, "Die");
        DestroyNode destroy = new DestroyNode(this.gameObject);
        
        SequenceNode death = new SequenceNode(new List<Node>{isDeadClamped, deathAnim, onDeathAnimComplete, destroy});

        ////////////////////////////////// Cover //////////////////////////////////

        HealthCheckNode isLow = new HealthCheckNode(this, consideredLowHP, true);
        SoundNode hurtSFX = new SoundNode(Sound.SeaMonsterHurt);
        RandomNode chanceHurtSFX = new RandomNode(hurtSFX, 0.4f);
        ColorFlashNode hurtFlash = new ColorFlashNode(GetMaterial(), Color.red, 3, 1);
        RecoverHealthNode hpRecover = new RecoverHealthNode(this, hpRecoveryMultiplier);
        FindCover findCover = new FindCover(this, target, 10);

        SequenceNode hurting = new SequenceNode(new List<Node>() { chanceHurtSFX, hurtFlash});
        SequenceNode recovering =new SequenceNode(new List<Node>(){hpRecover, hurting});
        SequenceNode covering = new SequenceNode(new List<Node>(){findCover, movement});
        ParallelNode recoveringAndCovering = new ParallelNode(new List<Node>(){recovering, covering}, 2);

        SequenceNode cover = new SequenceNode(new List<Node>(){isLow, recoveringAndCovering});

        ////////////////////////////////// Attack //////////////////////////////////

    // Condition To Start Attack Sequence
        IsInRangeNode isInATKRange = new IsInRangeNode(atkRange, transform, target);

        // Attack Sequence
        AnimationNode readyUpAnim = new AnimationNode(a, "Aim");
        LookNode aim = new LookNode(rB, target, 1, 12, 7);
        AnimationNode strikeAnim = new AnimationNode(a, "Fire");
        TimerNode atkTimer = new TimerNode(1);

        // Wrapping Nodes
        SequenceNode attackPattern = new SequenceNode(new List<Node>(){readyUpAnim, aim, strikeAnim, atkTimer });
        ResetOnStateNode resetATK = new ResetOnStateNode(attackPattern, NodeState.SUCCESS);
        SequenceNode attacking = new SequenceNode(new List<Node>(){isInATKRange, resetATK});
        SelectorNode attack = new SelectorNode(new List<Node>(){attacking, movement});


        ////////////////////////////////// Patrol //////////////////////////////////

        PatrolNode patrolMove = new PatrolNode(rB, Mathf.RoundToInt(speed * 0.7f), 0.5f, 6, 20, 1.5f, 4);
        SelectorNode evadeAndPatrol = new SelectorNode(new List<Node>(){evading, patrolMove});

        SpotTargetNode spotTarget = new SpotTargetNode(transform, 40, 80, LayerMask.NameToLayer("Target"), LayerMask.NameToLayer("Enemy"));

        FlagNode flag = new FlagNode(NodeState.SUCCESS, NodeState.FAILURE);
        FlagActivator flagActivator = new FlagActivator(flag);
        
        SequenceNode spotAndStop = new SequenceNode(new List<Node>(){spotTarget, flagActivator});
        
        SelectorNode patroling = new SelectorNode (new List<Node>(){spotAndStop, evadeAndPatrol});
        SequenceNode patrol = new SequenceNode(new List<Node>(){flag, patroling});

        ////////////////////////////////// Top Node //////////////////////////////////

        topNode = new Tree(new SelectorNode(new List<Node>(){death, cover, patrol, attack}));
        
        BehaviorTreeManager.AddTree(topNode);
    }
}
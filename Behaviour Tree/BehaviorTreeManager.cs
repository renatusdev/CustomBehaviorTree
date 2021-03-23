using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorTreeManager : MonoBehaviour
{
    static bool isFixedUpdateable;
    static List<Tree> trees = new List<Tree>();

    float gameTime = 0.0f;
    float fixedTime = 0.0f;

    void Update()
    {
        CheckFixedTime();

        // Running every tree.
        foreach(Tree tree in trees)
            tree.node.Evaluate();
    }
    
    // Verifies if the current update tick is in alignment with the fixed update.
    void CheckFixedTime()
    {
        isFixedUpdateable = false;
        gameTime += Time.deltaTime;
        while(gameTime - fixedTime > Time.fixedDeltaTime)
        {
            isFixedUpdateable = true;
            fixedTime += Time.fixedDeltaTime;
        }
    }

    public static bool IsFixedTime()
    {
        return BehaviorTreeManager.isFixedUpdateable;
    }

    public static void AddTree(Tree tree) { trees.Add(tree); }
}

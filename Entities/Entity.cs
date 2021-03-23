using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [Range(0,100)] public float hp;
    public Transform target;

    public Animator animator;
    public Material material;

    
    public Material GetMaterial()   { return material;              }
    public Animator GetAnimator()   { return animator;              }
    public Vector3 GetPosition()    { return transform.position;    }
    public float GetHP()            { return hp;                    }
    
    public void SetColor(Color c)   { material.color = c;       }
    public void SetHP(float value)  { this.hp = value;         }
}
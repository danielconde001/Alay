using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCharacterCollision : MonoBehaviour
{
    [SerializeField] private Collider2D charCollider;
    [SerializeField] private Collider2D charBlockCollider;

    void Start()
    {
        Physics2D.IgnoreCollision(charCollider, charBlockCollider, true);
    }
}

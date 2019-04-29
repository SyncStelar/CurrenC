using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildFollowParent : MonoBehaviour
{
    public float velocity = 99980f;

    void FixedUpdate()
    {
        if (transform.parent == null) {
            transform.position = Vector3.MoveTowards(transform.position, CoinController.cc.player.transform.position, velocity * Time.fixedDeltaTime);
        }
    }
}

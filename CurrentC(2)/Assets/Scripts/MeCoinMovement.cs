using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeCoinMovement : MonoBehaviour
{
    public float velocity = 3.0f;

    private float originalVelocity = 0f;

    private Rigidbody rb;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        originalVelocity = velocity;
    }

    private float dropTrailt = 0f;

    void FixedUpdate()
    {
        dropTrailt += Time.fixedDeltaTime;
        float x = Input.GetAxis("Horizontal") * velocity;
        float y = Input.GetAxis("Vertical") * velocity;

        rb.velocity = new Vector2(x, y) * Time.fixedDeltaTime;

        if (dropTrailt >= 1f) {
            dropTrailt = 0f;
            CoinController.cc.dropTrail.transform.position = transform.position;
        }
    }

    public void StopPlayerMovement() {
        velocity = 0f;
    }

    public void ContinuePlayerMovement() {
        velocity = originalVelocity;
    }
}

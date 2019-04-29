using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeChangerMinus : MonoBehaviour
{

    private CoinController cc;

    private bool cooldownStart = false;

    private float t = 0f;

    private void Start() {
        cc = CoinController.cc;
    }

    private void Update() {
        if (cooldownStart) {
            t += Time.deltaTime;
        }
        if (t >= 0.5f) {
            cooldownStart = false;
            t = 0f;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            if (!cooldownStart) {
                cc.SplitValue();
                Debug.Log("Touched the LifeChangingMinus");
                cooldownStart = true;
            }
        }
    }
}

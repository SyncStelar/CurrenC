using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeChangerPlus : MonoBehaviour
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
        if (!cooldownStart) {
            Debug.Log(other);
            if (other.tag == "Player") {
                cc.CombineValue();
                Debug.Log("Player touched LifeChangerPlus");
                cooldownStart = true;
            }
        }

    }
}
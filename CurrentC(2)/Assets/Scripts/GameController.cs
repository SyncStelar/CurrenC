using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController gc;

    public KeyCode leftButton = KeyCode.Q;
    public KeyCode rightButton = KeyCode.E;
    public KeyCode interact = KeyCode.F;
    public KeyCode pause = KeyCode.P;

    private void Awake() {
        if (gc != null) {
            Destroy(gameObject);
        } else {
            gc = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
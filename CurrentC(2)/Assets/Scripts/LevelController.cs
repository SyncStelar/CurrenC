using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{

    public static LevelController lc;

    private void Awake() {
        lc = this;
    }

    public float goal;
    public int lastDay;

    public GameObject walletThief;
    public float percentageOfSpawningWalletThief = 5f;

    public int economyState = 0;

    [System.NonSerialized] public bool allSpawnedIn = false;

    private void Start() {
        CoinController.cc.SpawnMoney(CoinController.cc.fiveDollars, 5f);
        allSpawnedIn = true;
    }
}
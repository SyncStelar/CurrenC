using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public int dayCount = 1;
    public float dayLength = 15f;
    public float nightLength = 5f;
    public bool canPressButtons = true;

    public SceneController sc;

    public GameObject nightFilter;

    private float currentTime = 0f;
    private float economyTime = 0f;
    private bool isItDay = true;

    private bool bankUsed = false;
    private bool beggarUsed = false;

    private MeCoinMovement mcm;

    private LevelController lc;
    private CoinController cc;

    private void Start() {
        lc = LevelController.lc;
        cc = CoinController.cc;
        mcm = GetComponent<MeCoinMovement>();
        lc.economyState = Random.Range(-1, 2);
        EconomyUpdate();
    }

    private bool runSpawnWalletSnatcherOnce = true;

    private void Update() {

        //if (Input.GetKeyDown(KeyCode.B)) {
        //    cc.SpawnMoney(cc.fiveDollars, 5f);
        //}
        //if (Input.GetKeyDown(KeyCode.C)) {
        //    cc.DeSpawnMoney(cc.fiveDollars, 5f);
        //}
        //if (Input.GetKeyDown(KeyCode.T)) {
        //    EconomyUpdate();
        //}


        //if (cc.totalMoneyValue == 0f) {
        //    if (dayCount == lc.winDate) {
        //        sc.YouWin();
        //    } else if (lc.allSpawnedIn) {
        //        sc.GameOver();
        //    }
        //}

        if (isItDay) {
            //Economy behaviour update
            economyTime += Time.deltaTime;
            CanvasController.cac.EconomySecondsChange(economyTime);
            if (economyTime >= 6f) {
                EconomyUpdate();
                economyTime = 0f;
            }
        }

        currentTime += Time.deltaTime;

        if (!isItDay && currentTime >= nightLength) {
            //Becomes Day
            runSpawnWalletSnatcherOnce = false;
            currentTime = 0;
            dayCount += 1;
            CanvasController.cac.ChangeDay(dayCount);
            isItDay = true;
            beggarUsed = false;
            bankUsed = false;
            CanvasController.cac.DayNightIconChange(true);
            nightFilter.GetComponent<Animator>().SetBool("IsItDay", true);

            if (dayCount > lc.lastDay) {
                sc.GameOver();
            }

            Debug.Log("It is Day!");
        }
        else if (isItDay && currentTime >= dayLength) {
            //Becomes Night
            GivePlayerMonies();
            SpawnWalletSnatcher();
            currentTime = 0;
            isItDay = false;
            CanvasController.cac.DayNightIconChange(false);
            nightFilter.GetComponent<Animator>().SetBool("IsItDay", false);
            Debug.Log("It is Night!");
        }

        if (mcm.velocity > 0f) {
            if (Input.GetKeyDown(GameController.gc.leftButton)) {
                GameObject temp = CoinController.cc.allCoins[0];
                CoinController.cc.allCoins.Remove(temp);
                CoinController.cc.allCoins.Add(temp);
                CoinController.cc.IsThereLeader();
            } else if (Input.GetKeyDown(GameController.gc.rightButton)) {
                List<GameObject> temp = new List<GameObject>();
                temp.Add(CoinController.cc.allCoins[CoinController.cc.allCoins.Count - 1]);
                for (int i = 0; i < CoinController.cc.allCoins.Count - 1; i++) {
                    temp.Add(CoinController.cc.allCoins[i]);
                }

                CoinController.cc.allCoins = new List<GameObject>(temp);
                CoinController.cc.IsThereLeader();
            }
        }
    }

    private void EconomyUpdate() {
        List<int> bag = new List<int>();
        bag.Add(lc.economyState);

        if (lc.economyState == -1) {
            bag.Add(0);
            bag.Add(0);
            bag.Add(1);
            bag.Add(1);
        }
        if (lc.economyState == 0) {
            bag.Add(-1);
            bag.Add(-1);
            bag.Add(1);
            bag.Add(1);
        }
        if (lc.economyState == 1) {
            bag.Add(0);
            bag.Add(0);
            bag.Add(-1);
            bag.Add(-1);
        }
        lc.economyState = bag[Random.Range(0, bag.Count)];
        //if (lc.economyState == -1) { CanvasController.cac.EconomyStateChange(CanvasController.cac.economyStateDown); }
        //if (lc.economyState == 0) { CanvasController.cac.EconomyStateChange(CanvasController.cac.economyStateDash); }
        //if (lc.economyState == 1) { CanvasController.cac.EconomyStateChange(CanvasController.cac.economyStateUp); }
        if (lc.economyState == -1) { CanvasController.cac.EconomyStateChange(CanvasController.cac.economyDown); }
        else if (lc.economyState == 0) { CanvasController.cac.EconomyStateChange(CanvasController.cac.economyDash); }
        else if (lc.economyState == 1) { CanvasController.cac.EconomyStateChange(CanvasController.cac.economyUp); }

    }

    private void GivePlayerMonies() {
        
        if (forexInvestedValue > 0f) {
            if (lc.economyState == -1) { forexInvestedValue = 0f; }
            if (lc.economyState == 0) { }
            if (lc.economyState == 1) { forexInvestedValue *= 2f; }
            while (forexInvestedValue >= 5f) {
                cc.SpawnMoney(cc.fiveDollars, 5f);
                forexInvestedValue -= 5f;
            }
            while (forexInvestedValue >= 1f) {
                cc.SpawnMoney(cc.oneDollar, 1f);
                forexInvestedValue -= 1f;
            }
            while (forexInvestedValue >= 0.5f) {
                cc.SpawnMoney(cc.fiftyCents, 0.5f);
                forexInvestedValue -= 0.5f;
            }
            forexInvestedValue = 0f;
        }

        if (bankReturnValue > 0f) {
            while (bankReturnValue >= 5f) {
                cc.SpawnMoney(cc.fiveDollars, 5f);
                bankReturnValue -= 5f;
            }
            while (bankReturnValue >= 1f) {
                cc.SpawnMoney(cc.oneDollar, 1f);
                bankReturnValue -= 1f;
            }
            while (bankReturnValue >= 0.5f) {
                cc.SpawnMoney(cc.fiftyCents, 0.5f);
                bankReturnValue -= 0.5f;
            }
            bankReturnValue = 0;
        }

        cc.IsThereLeader();
        CanvasController.cac.UpdateCashText();
    }

    private void SpawnWalletSnatcher() {
        if (!runSpawnWalletSnatcherOnce && (Random.Range(0f, 100f) <= (cc.allCoins.Count * lc.percentageOfSpawningWalletThief) || lc.percentageOfSpawningWalletThief >= 100)) {
            int staticNumber = Random.Range(1, cc.allCoins.Count);
            GameObject walletThief = Instantiate(lc.walletThief, cc.allCoins[Random.Range(1, cc.allCoins.Count)].transform);
            walletThief.GetComponent<WalletThief>().moneyTargeted = staticNumber;
            mcm.StopPlayerMovement();
        }
        runSpawnWalletSnatcherOnce = true;
    }

    public float forexInvestedValue = 0f;
    public float bankReturnValue = 0f;
    public float vendingMachineCost = 0f;

    private float plusCooldown = 0f;
    private float minusCooldown = 0f;

    private void OnTriggerStay(Collider other) {
        plusCooldown += Time.deltaTime;
        minusCooldown += Time.deltaTime;

        if (Input.GetKeyDown(GameController.gc.interact) && canPressButtons) {
            if (other.tag == "Bank" && !bankUsed && cc.allCoins.Count > 1) {
                //run Bank code
                //if (cc.ReturnLeader().tag == "5 Dollars" && bankReturnValue == 0 && !bankUsed && cc.allCoins.Count > 1) {
                //    cc.DeSpawnMoney(cc.ReturnLeader(), 5f);
                //    bankReturnValue = 6f;
                //    bankUsed = true;

                //    SoundController.sc.PlaySound(SoundController.sc.chaChing);
                //}
                if (cc.ReturnLeader().tag == "5 Dollars") {
                    bankReturnValue += 7.5f;
                    cc.DeSpawnMoney(cc.allCoins[0], 5f);
                    bankUsed = true;
                } else if (cc.ReturnLeader().tag == "1 Dollar") {
                    bankReturnValue += 1.5f;
                    cc.DeSpawnMoney(cc.allCoins[0], 1f);
                    bankUsed = true;
                } else if (cc.ReturnLeader().tag == "50 Cents") {
                    return;
                }

                SoundController.sc.PlaySound(SoundController.sc.chaChing);
            } 
            else if (other.tag == "Forex" && forexInvestedValue == 0 && cc.allCoins.Count > 1) {
                //run Forex code
                if (cc.ReturnLeader().tag == "5 Dollars") {
                    forexInvestedValue += 5f;
                    cc.DeSpawnMoney(cc.allCoins[0], 5f);
                } else if (cc.ReturnLeader().tag == "1 Dollar") {
                    forexInvestedValue += 1f;
                    cc.DeSpawnMoney(cc.allCoins[0], 1f);
                } else if (cc.ReturnLeader().tag == "50 Cents") {
                    forexInvestedValue += 0.5f;
                    cc.DeSpawnMoney(cc.allCoins[0], 0.5f);
                }

                SoundController.sc.PlaySound(SoundController.sc.chaChing);

            } 
            else if (other.tag == "Beggar" && !beggarUsed && cc.allCoins.Count > 1) {
                //run Beggar code
                float value = 0f;
                if (cc.ReturnLeader().tag == "5 Dollars") { value = 5f; }
                else if (cc.ReturnLeader().tag == "1 Dollar") { value = 1f; }
                else if (cc.ReturnLeader().tag == "50 Cents") { value = 0.5f; }
                cc.DeSpawnMoney(cc.ReturnLeader(), value);
                beggarUsed = true;
                Destroy(other.gameObject);

                SoundController.sc.PlaySound(SoundController.sc.chaChing);
            }
            // else if (other.tag == "Vending Machine") {
            //    //run Vending Machine Code
            //}
            //else if (other.tag == "Goal") {
            //    //run Goal code
            //    List<GameObject> temp = new List<GameObject>(lc.goal);
            //    for (int i = 0; i < cc.allCoins.Count; i++) {
            //        for (int j = 0; j < temp.Count; j++) {
            //            if (cc.allCoins[i] == temp[j]) {
            //                temp.Remove(temp[j]);
            //                j -= 1;
            //            }
            //        }
            //    }
            //    if (temp.Count == 0) {
            //        //You win
            //        //
            //    }
            //}
            else if (other.tag == "Life Changer Combine" && plusCooldown >= 0.25f) {
                cc.CombineValue();
                plusCooldown = 0f;
            }
            else if (other.tag == "Life Changer Split" && minusCooldown >= 0.25f) {
                cc.SplitValue();
                minusCooldown = 0f;
            }
            else if (other.tag == "Goal" && cc.totalMoneyValue == lc.goal /*&& dayCount >= lc.lastDay*/) {
                sc.YouWin();
            }
            CanvasController.cac.UpdateCashText();
        }
    }
}

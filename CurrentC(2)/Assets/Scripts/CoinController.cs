using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public static CoinController cc;

    private void Awake() {
        cc = this;
    }

    public float scaleSize = 1.3f;

    public float totalMoneyValue = 0f;

    public GameObject player;
    public GameObject dropTrail;
    public GameObject fiveDollars;
    public GameObject oneDollar;
    public GameObject fiftyCents;

    [System.NonSerialized] public List<GameObject> allCoins = new List<GameObject>();

    private BoxCollider[] colliders;

    private void Start() {
        colliders = player.GetComponents<BoxCollider>();
    }

    public void SplitValue() {

        GameObject currentLeader = allCoins[0];

        if (currentLeader.tag == "5 Dollars") {
            //create 5 one dollar
            for (int i = 0; i < 5; i++) {
                SpawnMoney(oneDollar, 0);
            }
            ReplaceLeader();
            SoundController.sc.PlaySound(SoundController.sc.chaChing);
            Debug.Log("Split 5 dollars");
        } else if (currentLeader.tag == "1 Dollar") {
            //create 2 fifty cents
            for (int i = 0; i < 2; i++) {
                SpawnMoney(fiftyCents, 0);
            }
            SoundController.sc.PlaySound(SoundController.sc.chaChing);
            ReplaceLeader();
            Debug.Log("Split 1 Dollar");
        } else if (currentLeader.tag == "50 Cents") {
            //reject
            Debug.Log("Split 50 cents");
        }

        CanvasController.cac.UpdateCashText();

        IsThereLeader();
    }

    public void CombineValue () {
        GameObject currentLeader = allCoins[0];

        if (currentLeader.tag == "5 Dollars") {
            //reject
            Debug.Log("Combine 5 Dollars");
        } 
        else if (currentLeader.tag == "1 Dollar") {
            //use 5 1 dollars for 5 dollars
            int totalOneDollar = 0;
            List<GameObject> temp = new List<GameObject>();
            for (int i = 0; i < allCoins.Count; i++) {
                if (allCoins[i].tag == "1 Dollar" && totalOneDollar < 5) {
                    temp.Add(allCoins[i]);
                    totalOneDollar += 1;
                }
            }

            if (totalOneDollar < 5) {
                return;
            }

            for (int j = 0; j < 5; j++) {
                allCoins.Remove(temp[j]);
                Destroy(temp[j]);
            }
            SpawnMoney(fiveDollars, 0);
            SoundController.sc.PlaySound(SoundController.sc.chaChing);
            Debug.Log("Combine 1 Dollar");
        } 
        else if (currentLeader.tag == "50 Cents") {
            //use 2 50 cents for 1 dollar
            int totalFiftyCents = 0;
            List<GameObject> temp = new List<GameObject>();
            for (int i = 0; i < allCoins.Count; i++) {
                if (allCoins[i].tag == "50 Cents" && totalFiftyCents < 2) {
                    temp.Add(allCoins[i]);
                    totalFiftyCents += 1;
                }
            }

            if (totalFiftyCents < 2) {
                return;
            }

            for (int j = 0; j < 2; j++) {
                allCoins.Remove(temp[j]);
                Destroy(temp[j]);
            }
            SpawnMoney(oneDollar, 0);
            SoundController.sc.PlaySound(SoundController.sc.chaChing);
            Debug.Log("Combine 50 Cents");
        }

        CanvasController.cac.UpdateCashText();
        IsThereLeader();
    }

    public void ReplaceLeader() {
        GameObject remember = allCoins[0];
        allCoins.Remove(remember);
        Destroy(remember);

        CanvasController.cac.UpdateCashText();
        IsThereLeader();
    }

    private GameObject wasLeader;

    public void IsThereLeader() {
        if (allCoins.Count > 0) {
            if (wasLeader == null) {
                allCoins[0].transform.SetParent(player.transform);
                allCoins[0].transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -1f);
                allCoins[0].GetComponent<Rigidbody>().isKinematic = true;
                allCoins[0].GetComponent<BoxCollider>().enabled = false;
                allCoins[0].transform.localScale *= scaleSize;
                allCoins[0].GetComponent<Rigidbody>().mass = 1f;
                BoxCollider leaderCollider = allCoins[0].GetComponent<BoxCollider>();
                foreach(BoxCollider bc in colliders) {
                    bc.size = new Vector3(leaderCollider.size.x * leaderCollider.transform.localScale.x, leaderCollider.size.y * leaderCollider.transform.localScale.y, leaderCollider.size.z * leaderCollider.transform.localScale.z);
                }
                wasLeader = allCoins[0];
            }
            if (allCoins[0] != wasLeader) {
                for (int i = 0; i < allCoins.Count; i++) {
                    allCoins[i].transform.parent = null;
                    Vector2 curPos = allCoins[i].transform.position;
                    allCoins[i].transform.position = new Vector3(curPos.x, curPos.y, 0);
                    allCoins[i].GetComponent<Rigidbody>().isKinematic = false;
                    allCoins[i].GetComponent<BoxCollider>().enabled = true;
                    allCoins[i].GetComponent<Rigidbody>().mass = 0f;
                    if (allCoins[i] == wasLeader) {
                        allCoins[i].transform.localScale /= scaleSize;
                    }
                }
                allCoins[0].transform.SetParent(player.transform);
                allCoins[0].GetComponent<Rigidbody>().isKinematic = true;
                allCoins[0].GetComponent<BoxCollider>().enabled = false;
                allCoins[0].transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -1f);
                allCoins[0].transform.localScale *= scaleSize;
                BoxCollider leaderCollider = allCoins[0].GetComponent<BoxCollider>();
                foreach (BoxCollider bc in colliders) {
                    bc.size = new Vector3(leaderCollider.size.x * leaderCollider.transform.localScale.x, leaderCollider.size.y * leaderCollider.transform.localScale.y, leaderCollider.size.z * leaderCollider.transform.localScale.z);
                }

                wasLeader = allCoins[0];
            }
        }
        CanvasController.cac.UpdateCashText();
    }

    public GameObject ReturnLeader() {
        return allCoins[0];
    }

    public void SpawnMoney(GameObject value, float moneyValue) {
        GameObject spawned = Instantiate(value, new Vector2 (player.transform.position.x * Random.Range(-1.0001f, 1.0001f), player.transform.position.y * Random.Range(-1.0001f, 1.0001f)), player.transform.rotation);
        allCoins.Add(spawned);

        totalMoneyValue += moneyValue;
        CanvasController.cac._addmoneytextAppear(moneyValue);
        CanvasController.cac.UpdateCashText();

        IsThereLeader();
    }

    public void DeSpawnMoney(GameObject value, float moneyValue) {
        ReplaceLeader();

        totalMoneyValue -= moneyValue;
        CanvasController.cac._minusmoneytextAppear(moneyValue);
        CanvasController.cac.UpdateCashText();

        IsThereLeader();
    }
}
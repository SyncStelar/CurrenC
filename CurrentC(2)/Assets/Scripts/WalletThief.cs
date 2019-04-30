using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalletThief : MonoBehaviour
{

    public int moneyTargeted;

    public float velocity = 20f;

    private CoinController cc;

    private float repeatProtection = 0f;

    private void Start() {
        cc = CoinController.cc;
    }

    private void Update() {
        repeatProtection += Time.deltaTime;

        if (transform.position.y >= -600) {
            transform.position = new Vector2(transform.position.x, transform.position.y - velocity);
        }
        else {
            Destroy(gameObject);
        }

        if (repeatProtection >= 0.1f && transform.parent != null && transform.position.y <= (transform.parent.position.y + 50f) && transform.position.y >= (transform.parent.position.y - 50f)) {
            //cc.allCoins.Remove(transform.parent.gameObject);
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<MeCoinMovement>().ContinuePlayerMovement();
            player.GetComponent<PlayerController>().canPressButtons = true;
            Transform wasParent = transform.parent;
            transform.parent = null;
            float value = 0f;
            if (wasParent.tag == "5 Dollars") { value = 5f; }
            else if (wasParent.tag == "1 Dollar") { value = 1f; }
            else if (wasParent.tag == "50 Cents") { value = 0.5f; }
            cc.DeSpawnMoney(wasParent.gameObject, value);
            repeatProtection = 0f;
            CanvasController.cac.UpdateCashText();
        }
    }
}

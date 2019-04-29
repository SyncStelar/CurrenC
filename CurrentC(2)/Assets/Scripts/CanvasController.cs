using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour {

    public static CanvasController cac;

    public Text totalCashText;

    public Text moneydisplayStatus;

    public Image DayNightIcon;
    public Text ecoCountDown;

    public Text whatDayText;

    private void Awake() {
        cac = this;
    }

    public Sprite day;
    public Sprite night;

    //public Image economyStateUp;
    //public Image economyStateDash;
    //public Image economyStateDown;
    public Image economyStateImg;
    public Sprite economyUp;
    public Sprite economyDash;
    public Sprite economyDown;

    //public void EconomyStateChange(Image img) {
    //    if (img == economyStateUp) {
    //        economyStateUp.gameObject.SetActive(true);
    //        economyStateDash.gameObject.SetActive(false);
    //        economyStateDown.gameObject.SetActive(false);
    //    } else if (img == economyStateUp) {
    //        economyStateUp.gameObject.SetActive(false);
    //        economyStateDash.gameObject.SetActive(true);
    //        economyStateDown.gameObject.SetActive(false);
    //    } else if (img == economyStateUp) {
    //        economyStateUp.gameObject.SetActive(false);
    //        economyStateDash.gameObject.SetActive(false);
    //        economyStateDown.gameObject.SetActive(true);
    //    }
    //}

    public void EconomyStateChange (Sprite img) {
        economyStateImg.sprite = img;
        //economyStateImg.preserveAspect = true;
    }

    public void EconomySecondsChange(float time) {
        float secondsUntilChange = 6f - time;
        ecoCountDown.text = string.Format("{0}\nseconds", secondsUntilChange.ToString("F0"));
    }

    public void DayNightIconChange(bool isItDay) {
        if (isItDay) {
            DayNightIcon.sprite = day;
        } else {
            DayNightIcon.sprite = night;
        }
    }

    public void ChangeDay(int day) {
        whatDayText.text = string.Format("Day\n{0}", day);
    }

    public void UpdateCashText () {
        totalCashText.text = string.Format("${0}", CoinController.cc.totalMoneyValue.ToString("F2"));
    }

    public void _addmoneytextAppear(float moneyValue) {
        //StartCoroutine(addmoneyTextAppear(moneyValue));
    }
    public void _minusmoneytextAppear(float moneyValue) {
        //StartCoroutine(minusmoneyTextAppear(moneyValue));
    }
    //IEnumerator addmoneyTextAppear(float moneyValue) {
    //    byte red = 94;
    //    byte green = 225;
    //    byte blue = 30;
    //    moneydisplayStatus.color = new Color32(red, green, blue, 255);
    //    moneydisplayStatus.text = "+" + moneyValue.ToString("F2");
    //    yield return new WaitForSeconds(1.0f);
    //    moneydisplayStatus.color = new Color32(red, green, blue, 0);
    //}
    //IEnumerator minusmoneyTextAppear(float moneyValue) {
    //    byte red = 219;
    //    byte green = 0;
    //    byte blue = 0;
    //    moneydisplayStatus.color = new Color32(red, green, blue, 255);
    //    moneydisplayStatus.text = "-" + moneyValue.ToString("F2");
    //    yield return new WaitForSeconds(1.0f);
    //}
}

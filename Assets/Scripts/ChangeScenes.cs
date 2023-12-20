using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScenes : MonoBehaviour {
    public GameObject panel1;
    public GameObject panel2;
    public Text zombiePurchased;
    public Text mariaPurchased;
    public Text amyPurchased;
    public Text robotSoldierPurchased;
    public Text lolaPurchased;
    public Text toCoinsText;
    private string money;

    private void Awake() {
        toCoinsText.text = $"Coins: {GameManager.GetTotalCoins()}";
        mariaPurchased.text = GameManager.MariaBoughtText();
        zombiePurchased.text = GameManager.ZombieBoughtText();
        amyPurchased.text = GameManager.AmyBoughtText();
        robotSoldierPurchased.text = GameManager.RobotSoldierBoughtText();
        lolaPurchased.text = GameManager.LolaBoughtText();
    }

    public void ChangeScene(int scene) {
        SceneManager.LoadScene(scene);
    }

    public void SelectCharacter(int i) {
        money = Regex.Replace(toCoinsText.text, @"\D", "");
        if (i == 0) {
            GameManager.charNumber = i;
        }
        if (i == 1) {
            if (int.Parse(money) < 500 && GameManager.ZombieBought() != 1) {
                panel1.gameObject.SetActive(true);
            }
            else if (GameManager.ZombieBought() != 1) {
                PlayerPrefs.SetInt("TotalCoins", GameManager.GetTotalCoins() - 500);
                PlayerPrefs.SetString("Char2Purchased", "Purchased");
                toCoinsText.text = $"Coins: {GameManager.GetTotalCoins()}";
                zombiePurchased.text = GameManager.ZombieBoughtText();
                PlayerPrefs.SetInt("Char2", 1);
                GameManager.charNumber = i;
            }
            else {
                GameManager.charNumber = i;
            }
        }
        if (i == 2) {
            if (int.Parse(money) < 1000 && GameManager.MariaBought() != 1) {
                panel1.gameObject.SetActive(true);
            }
            else if (GameManager.MariaBought() != 1) {
                PlayerPrefs.SetInt("TotalCoins", GameManager.GetTotalCoins() - 1000);
                PlayerPrefs.SetString("Char3Purchased", "Purchased");
                toCoinsText.text = $"Coins: {GameManager.GetTotalCoins()}";
                mariaPurchased.text = GameManager.MariaBoughtText();
                PlayerPrefs.SetInt("Char3", 1);
                GameManager.charNumber = i;
            }
            else {
                GameManager.charNumber = i;
            }
        }
        if (i == 3) {
            if (int.Parse(money) < 2000 && GameManager.AmyBought() != 1) {
                panel1.gameObject.SetActive(true);
            }
            else if (GameManager.AmyBought() != 1) {
                PlayerPrefs.SetInt("TotalCoins", GameManager.GetTotalCoins() - 2000);
                PlayerPrefs.SetString("Char4Purchased", "Purchased");
                toCoinsText.text = $"Coins: {GameManager.GetTotalCoins()}";
                amyPurchased.text = GameManager.AmyBoughtText();
                PlayerPrefs.SetInt("Char4", 1);
                GameManager.charNumber = i;
            }
            else {
                GameManager.charNumber = i;
            }
        }
        if (i == 4) {
            if (int.Parse(money) < 3000 && GameManager.RobotSoldierBought() != 1) {
                panel1.gameObject.SetActive(true);
            }
            else if (GameManager.RobotSoldierBought() != 1) {
                PlayerPrefs.SetInt("TotalCoins", GameManager.GetTotalCoins() - 3000);
                PlayerPrefs.SetString("Char5Purchased", "Purchased");
                toCoinsText.text = $"Coins: {GameManager.GetTotalCoins()}";
                robotSoldierPurchased.text = GameManager.RobotSoldierBoughtText();
                PlayerPrefs.SetInt("Char5", 1);
                GameManager.charNumber = i;
            }
            else {
                GameManager.charNumber = i;
            }
        }
        if (i == 5) {
            if (int.Parse(money) < 4000 && GameManager.LolaBought() != 1) {
                panel1.gameObject.SetActive(true);
            }
            else if (GameManager.LolaBought() != 1) {
                PlayerPrefs.SetInt("TotalCoins", GameManager.GetTotalCoins() - 4000);
                PlayerPrefs.SetString("Char6Purchased", "Purchased");
                toCoinsText.text = $"Coins: {GameManager.GetTotalCoins()}";
                lolaPurchased.text = GameManager.LolaBoughtText();
                PlayerPrefs.SetInt("Char6", 1);
                GameManager.charNumber = i;
            }
            else {
                GameManager.charNumber = i;
            }
        }
    }
    public void ClosePrompt() {
        panel1.gameObject.SetActive(false);
    }

    public void ResetAllStatsButton() {
        panel2.gameObject.SetActive(true);
    }

    public void ResetYesOrNo(int num) {
        if (num == 0) {
            panel2.gameObject.SetActive(false);
        }
        else if (num == 1) {
            panel2.gameObject.SetActive(false);
            GameManager.ResetGame();
        }

    }
}

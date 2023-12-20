using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public AudioClip crystalSound;
    public AudioClip coinSound;
    public static int charNumber = 0;
    public bool gameStarted;
    public int score;
    private int coinGathered;
    public Text scoreText;
    public Text highscoreText;
    public Text coinsText;
    public Canvas canvas;
    public GameObject road;

    public static int gameCount;
    public static int gameCountForFirstAd = 0;
    AdsInitializer ads;

    private void Awake() {
        highscoreText.text = $"Highscore: {GetHighscore()}";
        coinsText.text = $"Coins: {GetTotalCoins()}";
        for (int i = 0; i < GameObject.Find("Characters").transform.childCount; i++) {
            if (i == charNumber) {
                continue;
            }
            else {
                GameObject.Find("Characters").transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        GameObject.Find("Characters").transform.GetChild(charNumber).gameObject.SetActive(true);
        ads = FindObjectOfType<AdsInitializer>();
        transform.LeanScale(Vector2.zero, 1);
    }

    public void StartGame() {
        gameStarted = true;
    }

    // instead of changing the color to a random value every 15 seconds, now we're changing the color value to what we want at a specific score gap in charcontroller Trigger method. this method is not being used atm. if you want to use it you can start a coroutine in the StartGameButton().
    public IEnumerator ChangeRoadColor() {
        while (true) {
            yield return new WaitForSeconds(15f);
            road.LeanColor(new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), 255), 1);
            //Camera.main.backgroundColor = new Color32(255, 210, 201, 255);
        }
    }

    public void StartGameButton() {
        if (!gameStarted) {
            StartGame();
            FindObjectOfType<Road>().StartBuilding();

            for (int i = 0; i < GameObject.Find("Characters").transform.childCount; i++) {
                if (GameObject.Find("Characters").transform.GetChild(i).gameObject.activeSelf) {
                    GameObject.Find("Characters").transform.GetChild(i).gameObject.transform.rotation = Quaternion.Euler(0, 45, 0);
                }
            }
            canvas.transform.GetChild(2).gameObject.SetActive(false);
            canvas.transform.GetChild(3).gameObject.SetActive(false);
            canvas.transform.GetChild(5).gameObject.SetActive(false);
            canvas.transform.GetChild(6).gameObject.SetActive(false);
            canvas.transform.GetChild(7).gameObject.SetActive(false);
            canvas.transform.GetChild(12).gameObject.SetActive(false);

            //StartCoroutine(ChangeRoadColor());
        }
    }

    //private void Update() {
    //    if (Input.GetKeyDown(KeyCode.Return)) {
    //        if (!gameStarted) {
    //            StartGame();
    //            FindObjectOfType<Road>().StartBuilding();

    //            for (int i = 0; i < GameObject.Find("Characters").transform.childCount; i++) {
    //                if (GameObject.Find("Characters").transform.GetChild(i).gameObject.activeSelf) {
    //                    GameObject.Find("Characters").transform.GetChild(i).gameObject.transform.rotation = Quaternion.Euler(0, 45, 0);
    //                }
    //            }
    //            canvas.transform.GetChild(2).gameObject.SetActive(false);
    //            canvas.transform.GetChild(3).gameObject.SetActive(false);
    //            canvas.transform.GetChild(5).gameObject.SetActive(false);
    //            canvas.transform.GetChild(6).gameObject.SetActive(false);
    //        }
    //    }
    //}

    public void EndGame() {
        //SceneManager.LoadScene(0);

        road.GetComponent<Renderer>().sharedMaterial.SetColor("_Color", new Color32(6, 192, 255, 255));
        if(Application.internetReachability != NetworkReachability.NotReachable) {
            gameCountForFirstAd++;

            //ads initialization after first game, and than initializing ads every 3 games. 
            if (gameCountForFirstAd == 1) {
                ads.LoadInterstitialAd();
            }

            if (gameCountForFirstAd != 1) {
                gameCount++;
            }

            if (gameCount % 3 == 0 && gameCount != 0) {
                ads.LoadInterstitialAd();
            }
            else if (gameCount % 3 != 0 && gameCount != 0) {
                SceneManager.LoadScene(0);
            }
        }
        else {
            SceneManager.LoadScene(0);
        }
        
    }

    public void IncreaseScore() {
        score++;
        scoreText.text = score.ToString();
        if (score > GetHighscore()) {
            PlayerPrefs.SetInt("Highscore", score);
            highscoreText.text = $"Highscore: {score}";
        }
        else {
            highscoreText.text = $"Highscore: {GetHighscore()}";
        }
    }

    public void IncreaseCoin() {
        coinGathered = 10;
        coinsText.text = $"Coins: {coinGathered + GetTotalCoins()}";
        PlayerPrefs.SetInt("TotalCoins", coinGathered + GetTotalCoins());
    }


    int GetHighscore() {
        int highscore = PlayerPrefs.GetInt("Highscore", 0);
        return highscore;
    }

    public static int GetTotalCoins() {
        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        return totalCoins;
    }

    public void ChangeScene(int scene) {
        SceneManager.LoadScene(scene);
    }

    public static int ZombieBought() {
        int b = PlayerPrefs.GetInt("Char2", 0);
        return b;
    }
    public static string ZombieBoughtText() {
        string x = PlayerPrefs.GetString("Char2Purchased", "500 Coins");
        return x;
    }

    public static int MariaBought() {
        int b = PlayerPrefs.GetInt("Char3", 0);
        return b;
    }

    public static string MariaBoughtText() {
        string x = PlayerPrefs.GetString("Char3Purchased", "1000 Coins");
        return x;
    }

    public static int AmyBought() {
        int b = PlayerPrefs.GetInt("Char4", 0);
        return b;
    }

    public static string AmyBoughtText() {
        string x = PlayerPrefs.GetString("Char4Purchased", "2000 Coins");
        return x;
    }

    public static int RobotSoldierBought() {
        int b = PlayerPrefs.GetInt("Char5", 0);
        return b;
    }

    public static string RobotSoldierBoughtText() {
        string x = PlayerPrefs.GetString("Char5Purchased", "3000 Coins");
        return x;
    }

    public static int LolaBought() {
        int b = PlayerPrefs.GetInt("Char6", 0);
        return b;
    }

    public static string LolaBoughtText() {
        string x = PlayerPrefs.GetString("Char6Purchased", "4000 Coins");
        return x;
    }

    public static void ResetGame() {
        PlayerPrefs.SetInt("Highscore", 0);
        PlayerPrefs.SetInt("TotalCoins", 0);
        PlayerPrefs.SetInt("Char2", 0);
        PlayerPrefs.SetInt("Char3", 0);
        PlayerPrefs.SetInt("Char4", 0);
        PlayerPrefs.SetInt("Char5", 0);
        PlayerPrefs.SetInt("Char6", 0);
        PlayerPrefs.SetString("Char2Purchased", "500 Coins");
        PlayerPrefs.SetString("Char3Purchased", "1000 Coins");
        PlayerPrefs.SetString("Char4Purchased", "2000 Coins");
        PlayerPrefs.SetString("Char5Purchased", "3000 Coins");
        PlayerPrefs.SetString("Char6Purchased", "4000 Coins");
    }
}

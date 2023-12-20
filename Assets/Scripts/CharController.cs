using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour {
    public Transform rayStart;
    private Animator anim;
    private Rigidbody rb;
    private bool walkingRight = true;
    private GameManager gameManager;
    public GameObject crystalEffect;
    private float speed = 2;
    private float destroySec = 1f;

    private bool isBelowEndPoint = true;

    void Awake() {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void FixedUpdate() {
        if (!gameManager.gameStarted) {
            return;
        }
        else {
            anim.SetTrigger("gameStarted");
        }
        //ne kadar zaman gectigine bagli olarak gameobject i hareket ettiriyor, fixedupdate de time hep ayni olur. burda gameobject i adim adim ilerletiyoruz. transform.forward gameobject in baktigi direction olur(rotation).

        rb.transform.position = transform.position + transform.forward * speed * Time.deltaTime;
    }

    // Update is called once per frame
    void Update() {
        if (/*Input.GetKeyDown(KeyCode.Space) ||*/ Input.GetMouseButtonDown(0)) {
            Switch();
        }
        RaycastHit hit;

        if (!Physics.Raycast(rayStart.position, -transform.up, out hit, Mathf.Infinity)) {
            anim.SetTrigger("isFalling");
        }
        else if (gameManager.gameStarted) {
            Destroy(hit.collider.gameObject, destroySec);
            anim.SetTrigger("isNotFallingAnymore");
        }

        if (transform.position.y < -2 && !isBelowEndPoint) {
            gameManager.EndGame();
            isBelowEndPoint = true;
            //FindObjectOfType<BackgroundLoop>().GetComponent<AudioSource>().mute = false;
        }
        else if(transform.position.y > -2) {
            isBelowEndPoint = false;
        }
    }

    private void Switch() {
        if (!gameManager.gameStarted) {
            return;
        }

        walkingRight = !walkingRight;

        if (walkingRight) {
            transform.rotation = Quaternion.Euler(0, 45, 0);
        }
        else {
            transform.rotation = Quaternion.Euler(0, -45, 0);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Crystal") {
            gameManager.IncreaseScore();
            //crystaleffect i player in position inda instantiate yapip iki saniye sonra bu gameobject i destroy yapiyoruz. particle effect gameobject i destroy olmadigi surece
            //particle effect surekli calisip durucak. her bir effect i g gameobject olarak alip her birini 2 saniye sonra destroy yapiyoruz ki hem yer kaplamasin hem de surekli
            //olarak calisip durmasin.
            GameObject g = Instantiate(crystalEffect, rayStart.transform.position, Quaternion.identity);
            Destroy(g, 2);
            Destroy(other.gameObject);
            FindObjectOfType<GameManager>().GetComponent<AudioSource>().clip = FindObjectOfType<GameManager>().crystalSound;
            FindObjectOfType<GameManager>().GetComponent<AudioSource>().Play();
        }

        if (other.gameObject.name == "GoldCoins") {
            gameManager.IncreaseCoin();
            Destroy(other.gameObject);
            FindObjectOfType<GameManager>().GetComponent<AudioSource>().clip = FindObjectOfType<GameManager>().coinSound;
            FindObjectOfType<GameManager>().GetComponent<AudioSource>().Play();
        }

        if (FindObjectOfType<GameManager>().score > 10 && FindObjectOfType<GameManager>().score < 20) {
            FindObjectOfType<GameManager>().road.LeanColor(Color.yellow, 1);
        }
        else if (FindObjectOfType<GameManager>().score > 20 && FindObjectOfType<GameManager>().score < 30) {
            FindObjectOfType<GameManager>().road.LeanColor(Color.green, 1);
        }
        else if (FindObjectOfType<GameManager>().score > 30 && FindObjectOfType<GameManager>().score < 40) {
            FindObjectOfType<GameManager>().road.LeanColor(new Color32(255, 0, 216, 255), 1);
        }
        else if (FindObjectOfType<GameManager>().score > 40 && FindObjectOfType<GameManager>().score < 60) {
            FindObjectOfType<GameManager>().road.LeanColor(Color.blue, 1);
        }
        else if (FindObjectOfType<GameManager>().score > 60 && FindObjectOfType<GameManager>().score < 80) {
            FindObjectOfType<GameManager>().road.LeanColor(Color.cyan, 1);
        }
        else if (FindObjectOfType<GameManager>().score > 80 && FindObjectOfType<GameManager>().score < 100) {
            FindObjectOfType<GameManager>().road.LeanColor(Color.gray, 1);
        }
        else if (FindObjectOfType<GameManager>().score > 100 && FindObjectOfType<GameManager>().score < 120) {
            FindObjectOfType<GameManager>().road.LeanColor(Color.magenta, 1);
        }
        else if (FindObjectOfType<GameManager>().score > 120 && FindObjectOfType<GameManager>().score < 140) {
            FindObjectOfType<GameManager>().road.LeanColor(new Color32(48, 42, 126, 255), 1);
        }

        if (FindObjectOfType<GameManager>().score > 50) {
            return;
        } else if(FindObjectOfType<GameManager>().score % 5 == 0 && FindObjectOfType<GameManager>().score != 0) {
            speed += 0.2f; // 0.2
            FindObjectOfType<Road>().spawnTempo /= 1.090f; // 1.090
            destroySec /= 1.05f; // 1.05
        }
    }
}

//if (other.tag == "Crystal") {
//    gameManager.IncreaseScore();
//    //crystaleffect i player in position inda instantiate yapip iki saniye sonra bu gameobject i destroy yapiyoruz. particle effect gameobject i destroy olmadigi surece
//    //particle effect surekli calisip durucak. her bir effect i g gameobject olarak alip her birini 2 saniye sonra destroy yapiyoruz ki hem yer kaplamasin hem de surekli
//    //olarak calisip durmasin.
//    GameObject g = Instantiate(crystalEffect, rayStart.transform.position, Quaternion.identity);
//    Destroy(g, 2);
//    Destroy(other.gameObject);
//    FindObjectOfType<GameManager>().GetComponent<AudioSource>().clip = FindObjectOfType<GameManager>().crystalSound;
//    FindObjectOfType<GameManager>().GetComponent<AudioSource>().Play();
//}
//if (other.gameObject.name == "GoldCoins") {
//    gameManager.IncreaseCoin();
//    Destroy(other.gameObject);
//    FindObjectOfType<GameManager>().GetComponent<AudioSource>().clip = FindObjectOfType<GameManager>().coinSound;
//    FindObjectOfType<GameManager>().GetComponent<AudioSource>().Play();
//}
//if (FindObjectOfType<GameManager>().score > 50) {
//    return;
//}
//else if (FindObjectOfType<GameManager>().score % 5 == 0 && FindObjectOfType<GameManager>().score != 0) {
//    speed += 0.2f; // 0.2
//    FindObjectOfType<Road>().spawnTempo /= 1.090f; // 1.090
//    destroySec /= 1.05f; // 1.05
//}

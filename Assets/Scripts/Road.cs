using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour {
    public GameObject roadPrefab;
    private float offset = 0.7071068f;
    public Vector3 lastPos;
    private int roadCount;
    public float spawnTempo = 0.5f;

    public void StartBuilding() {
        //InvokeRepeating("CreateNewRoadPart", 1f, spawnTempo);
        StartCoroutine(CreateNewRoadPart());
    }

    public IEnumerator CreateNewRoadPart() {
        Vector3 spawnPos = Vector3.zero;
        while (true) {
            float tempo = spawnTempo;
            float chance = Random.Range(0, 100);

            if (chance < 50) {
                spawnPos = new Vector3(lastPos.x + offset, lastPos.y, lastPos.z + offset);
            }
            else {
                spawnPos = new Vector3(lastPos.x - offset, lastPos.y, lastPos.z + offset);
            }

            GameObject g = Instantiate(roadPrefab, spawnPos, Quaternion.Euler(0, 45, 0));
            lastPos = g.transform.position;

            roadCount++;

            if (roadCount % 5 == 0) {
                g.transform.GetChild(0).gameObject.SetActive(true);
            }

            if(roadCount % 11 == 0) {
                g.transform.GetChild(2).gameObject.SetActive(true);
            }

            //belli bir sure bekleyip loop a sonsuza kadar devam ediliyor.
            yield return new WaitForSeconds(tempo);
        }


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour {

    private Transform target;
    private Vector3 offset;

    // Update is called once per frame
    void Awake() {
        int myChildCount = GameObject.Find("Characters").transform.childCount;
        for (int i = 0; i < myChildCount; i++) {
            if (GameObject.Find("Characters").transform.GetChild(i).gameObject.activeSelf) {
                target = GameObject.Find("Characters").transform.GetChild(i).gameObject.transform;
            }
        }
        offset = transform.position - target.position;
    }

    private void LateUpdate() {
        transform.position = target.position + offset;
    }
}

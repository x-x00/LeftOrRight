using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationRewarded : MonoBehaviour
{
    private void Awake() {
        transform.LeanScale(new Vector2(0.8f, 0.8f), 1).setLoopPingPong();
    }
}

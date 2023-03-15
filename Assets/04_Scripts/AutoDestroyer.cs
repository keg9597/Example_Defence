using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyer : MonoBehaviour
{
    public float deadTime;
    void Start()
    {
        Destroy(gameObject, deadTime);
    }
}

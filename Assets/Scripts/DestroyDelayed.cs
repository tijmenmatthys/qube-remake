using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDelayed : MonoBehaviour
{
    [SerializeField] private float DestroyTime = 1;

    private float _timer = 0;

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > DestroyTime)
            Destroy(gameObject);
    }
}

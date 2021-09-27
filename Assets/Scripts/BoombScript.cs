using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoombScript : MonoBehaviour
{
    [SerializeField] float destroyTime;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        time = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > destroyTime)
        {
            Destroy(this.gameObject);
        }
    }
}

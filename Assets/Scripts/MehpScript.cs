using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MehpScript : MonoBehaviour
{
    public static float hp;
    Slider hpslider;

    // Start is called before the first frame update
    void Start()
    {
        hpslider = GetComponent<Slider>();
        hp = hpslider.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        hpslider.value = hp ;
        if (hp < hpslider.minValue)
        {
            hp = hpslider.minValue;
            ResultScript.result = false;
            SceneManager.LoadScene("Result");
        }
        else if (hp > hpslider.maxValue) hp = hpslider.maxValue;
    }
}

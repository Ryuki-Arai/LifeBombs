using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnhpScript : MonoBehaviour
{
    public static float hp;
    Slider hpSlider;

    // Start is called before the first frame update
    void Start()
    {
        hpSlider = GetComponent<Slider>();
        hp = hpSlider.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        hpSlider.value = hp;
        if (hp <= hpSlider.minValue)
        {
            hp = hpSlider.minValue;
            ResultScript.result = true;
            PlayerPrefs.Save();
            SceneManager.LoadScene("Result");
        }
        else if (hp > hpSlider.maxValue) hp = hpSlider.maxValue;
        else hp += Time.deltaTime * 3;
    }
}

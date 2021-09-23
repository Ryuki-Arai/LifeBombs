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
<<<<<<< HEAD
        if (hp <= hpSlider.minValue)
=======
        if (hp < hpSlider.minValue)
>>>>>>> 587be0ad4d3eb91993a0dad533c0c2c6deecd0b9
        {
            hp = hpSlider.minValue;
            ResultScript.result = true;
            SceneManager.LoadScene("Result");
        }
        else if (hp > hpSlider.maxValue) hp = hpSlider.maxValue;
<<<<<<< HEAD
        else hp += Time.deltaTime * 3;
=======
        else hp += Time.deltaTime*3;
>>>>>>> 587be0ad4d3eb91993a0dad533c0c2c6deecd0b9
    }
}

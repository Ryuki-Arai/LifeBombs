using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultScript : MonoBehaviour
{
    public static bool result;

    TextMeshProUGUI gmrsl;
    // Start is called before the first frame update
    void Start()
    {
        gmrsl = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (result)
        {
            gmrsl.text = "Congratulation!!!";
        }
        else
        {
            gmrsl.text = "That's too bad...";
        }
    }
}

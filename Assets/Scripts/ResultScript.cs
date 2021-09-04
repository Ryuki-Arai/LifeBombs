using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultScript : MonoBehaviour
{
    public static bool result;

    TextMeshProUGUI gmRsl;
    // Start is called before the first frame update
    void Start()
    {
        gmRsl = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (result)
        {
            gmRsl.text = "Congratulation!!!";
        }
        else
        {
            gmRsl.text = "That's too bad...";
        }
    }
}

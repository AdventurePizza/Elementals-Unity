using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitInfo : MonoBehaviour
{
    public GameObject text;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void setText(string hit){
        text.GetComponent<TMPro.TextMeshProUGUI>().text = hit;
        Object.Destroy(gameObject, 2.0f);
    }


}

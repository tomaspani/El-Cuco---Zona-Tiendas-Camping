using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIKidCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txt;
    [SerializeField] GameManager gm;

    private void Start()
    {
        UpdateTXT(0);
    }
    public void UpdateTXT (int consumedKids)
    {
        txt.text = consumedKids + "/" + gm.KidCount;
    }
}

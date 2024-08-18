using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Level3 : MonoBehaviour
{
    public TextMeshProUGUI textMeshLevel;
    private float score;
    private string level = "3";
    void Start()
    {
        textMeshLevel.text = "Nivel: " + level;
    }
}

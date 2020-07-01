using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Disc : MonoBehaviour
{
    int size;
    public int Size
    {
        get { return size; }
        set {
            size = value;
            text.text = size.ToString();
        }
    }
    public Text text;
}

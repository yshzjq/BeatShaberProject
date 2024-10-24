using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnNameScene : MonoBehaviour
{
    public string name;

    public void SelectNameScene()
    {
        PlayerPrefs.SetString("SelectedScene",name);
    }
}

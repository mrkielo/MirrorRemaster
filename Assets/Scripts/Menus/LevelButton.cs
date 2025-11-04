using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelButton : MonoBehaviour
{
    public Button button;
    public TMP_Text text;
    int number;
    public int Number
    {
        get
        {
            return number;
        }
        set
        {
            number = value;
            text.text = value.ToString();
        }
    }

}

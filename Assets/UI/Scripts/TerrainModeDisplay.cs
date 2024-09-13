using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TerrainModeDisplay : MonoBehaviour
{
    [SerializeField] private HeightSetter heightSetter;

    private TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
        heightSetter.OnParameterChanged += UpdateMode;
        UpdateMode();
    }

    private void UpdateMode()
    {
        text.text = heightSetter.TerrainPaintMode.ToString();
    }
}

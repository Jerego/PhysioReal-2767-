using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class ShowValues : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] private string path;

    void Start()
    {
        string json = File.ReadAllText(path);
        Values values = JsonUtility.FromJson<Values>(json);

        textMeshProUGUI.text = $"Min: {values.Min}\nMax: {values.Max}\nAvg: {values.Avg}";
    }

    public class Values
    {
        public float Min;
        public float Max;
        public float Avg;
    }
}

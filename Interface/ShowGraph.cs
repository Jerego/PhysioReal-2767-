using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowGraph : MonoBehaviour
{
    Texture2D graph;
    [SerializeField] private RawImage graphImage;
    [SerializeField] private string path;

    void Start () 
    {
        graph = Resources.Load(path) as Texture2D;
        graphImage.texture = graph;
    }
}

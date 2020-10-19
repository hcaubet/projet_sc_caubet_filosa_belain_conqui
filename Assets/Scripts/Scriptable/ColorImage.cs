using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ColorImage : ScriptableObject
{
    public Color32 color;
    public Texture2D[] image;
}

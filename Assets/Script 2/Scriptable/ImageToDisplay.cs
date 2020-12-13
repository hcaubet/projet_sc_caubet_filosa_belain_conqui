using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ImageToDisplay : ScriptableObject
{
    public Texture2D[] image;
    public Color32[] colorOfImage;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D tipaCursor; // ← сюда подставь свой курсор

    void Start()
    {
        Cursor.SetCursor(tipaCursor, Vector2.zero, CursorMode.Auto);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoMoth : MonoBehaviour
{
    public GameObject popupPanel;
    public Image popupImage;
    public Sprite[] randomSprites;

    public void ShowRandomImage()
    {
        int index = Random.Range(0, randomSprites.Length);
        popupImage.sprite = randomSprites[index];
        popupPanel.SetActive(true);
    }

    public void ClosePopup()
    {
        popupPanel.SetActive(false);
    }
}
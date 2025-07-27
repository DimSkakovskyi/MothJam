using UnityEngine;

public class MothClick : MonoBehaviour
{
    public InfoMoth infoMoth;

    private void OnMouseDown()
    {
        infoMoth.ShowRandomImage();
    }
}


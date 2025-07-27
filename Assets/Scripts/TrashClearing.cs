using System.Collections;
using UnityEngine;

public class TrashClearing : MonoBehaviour
{
    private bool cleaned = false;
    
    void Update()
    {
        Moth moth = GetComponent<Moth>();
        if (moth != null && moth.stage == 4 && !cleaned)
        {
            string mothTag = gameObject.tag;

            string trashTag = mothTag switch
            {
                "GMoth" => "GTrash",
                "BMoth" => "BTrash",
                "YMoth" => "YTrash",
                _ => ""
            };

            if (!string.IsNullOrEmpty(trashTag))
            {
                GameObject[] trash = GameObject.FindGameObjectsWithTag(trashTag);
                foreach (GameObject obj in trash)
                    Destroy(obj);

                cleaned = true;
            }
        }
    }
}

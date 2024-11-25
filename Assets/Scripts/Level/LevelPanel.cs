using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelPanel : MonoBehaviour
{
    public string idLevel;
    public int isLoad = 0; ////  0 не загруже // 1 загружен
    public GameObject imageBlock;

    private void Awake()
    {
        idLevel = gameObject.name;
    }

    public void IsLock()
    {
        if (isLoad == 0)
        {
            imageBlock.SetActive(true);
            Debug.Log(gameObject.name + "             true");
        }
        if (isLoad == 1)
        {
            imageBlock.SetActive(false);
            Debug.Log(gameObject.name + "             false");
        }
    }
}

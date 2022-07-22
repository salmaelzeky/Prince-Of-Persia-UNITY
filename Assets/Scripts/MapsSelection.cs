using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapsSelection : MonoBehaviour
{
    public bool isUnlock = false;
    public GameObject lockGo;
    public GameObject unlockGo;

    public int mapIndex;
    public int questNum;
    public int startLevel;
    public int endLevel;



    private void Update()
    {
        UpdateMapStatus();
        UnlockMap();

    }

    private void UpdateMapStatus()
    {
        if (isUnlock) //we can play this map
        {
            unlockGo.gameObject.SetActive(true);
            lockGo.gameObject.SetActive(false);
        }
        else //this map is still locked 
        {
            unlockGo.gameObject.SetActive(false);
            lockGo.gameObject.SetActive(true);
        }
    }

    private void UnlockMap()
    {
        if(UIManager.instance.stars >= questNum)
        {
            isUnlock = true;
        }
        else
        {
            isUnlock = false;
        }
    }

}



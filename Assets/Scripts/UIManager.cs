using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 


public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject mapSelectionPanel;
    public GameObject[] levelSelectionPanels;
    public MapsSelection[] mapSelections;
    public Text[] questStarsText;
    public Text[] lockedStarsText;
    public Text[] unlockedStarsText;
    public Text starText;

    public int stars ;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            if(instance != this)
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        UpdateLockedStarUI();
        UpdateUnlockedStarUI();
        UpdateStarUI();
    }
    private void UpdateLockedStarUI()
    {
        for (int i = 0; i < mapSelections.Length; i++)
        {
            if(mapSelections[i].isUnlock == false)
            {
                lockedStarsText[i].text = String.Concat(stars.ToString() , "/" , (mapSelections[i].endLevel* 3).ToString());
                Debug.Log(lockedStarsText[i].text);
            }
            questStarsText[i].text = mapSelections[i].questNum.ToString();
        }
       

    }

    private void UpdateUnlockedStarUI()
    {
        for (int i = 0; i < mapSelections.Length; i++)
        {
            if (mapSelections[i].isUnlock == true)
            {
                unlockedStarsText[i].text = String.Concat(stars.ToString(), "/", (mapSelections[i].endLevel * 3).ToString());
            }
        }
    }

    private void UpdateStarUI()
    {
        starText.text = stars.ToString();
    }

    public void PressMapButton(int _mapIndex)
    {
        if(mapSelections[_mapIndex].isUnlock == true)
        {
            levelSelectionPanels[_mapIndex].gameObject.SetActive(true);
            mapSelectionPanel.gameObject.SetActive(false);
        }
        else{
            Debug.Log("You cannot open this map now.");

        }
    }
    public void BackMapButton()
    {
        mapSelectionPanel.gameObject.SetActive(true);
        for(int i =0; i< mapSelections.Length; i++)
        {
            levelSelectionPanels[i].gameObject.SetActive(false);
        }
    }

    public void SceneTransition(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }
}

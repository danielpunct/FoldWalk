using Gamelogic.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// should to be refactored, so that the event would go through game manager - for clarity
public class Menu : Singleton<Menu> 
{
    public GameObject startUI;
    public GameObject restartUI;
    public GameObject gameUI;

    private void Start()
    {
        ShowStartUI();
    }

    public void OnPlayClassicalClick()
    {
        GameManager.Instance.StartCurrentLevel(true, false);

        ShowGameUI();
    }
  public void OnPlayRandomsClick()
    {
        GameManager.Instance.StartCurrentLevel(true, true);

        ShowGameUI();
    }

    public void OnRestartClick()
    {
        GameManager.Instance.RestartLevel();

        ShowGameUI();
    }

    public void OnShowStartUIClick()
    {
        Game.Instance.Suspend();

        ShowStartUI();
    }


    public void ShowStartUI()
    {
        startUI.SetActive(true);
        HideGameUI();
        HideRestartUI();
    }

    public void HideStartUI()
    {
        startUI.SetActive(false);
    }




    public void ShowRestartUI()
    {
        restartUI.SetActive(true);
        HideGameUI();
        HideStartUI();
    }
    public void HideRestartUI()
    {
        restartUI.SetActive(false);
    }

    public void ShowGameUI()
    {
        gameUI.SetActive(true);
        HideRestartUI();
        HideStartUI();
    }
    public void HideGameUI()
    {
        gameUI.SetActive(false);
    }

}

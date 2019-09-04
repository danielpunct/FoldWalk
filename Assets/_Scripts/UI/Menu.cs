﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject playButton;


    public void OnPlayClick()
    {
        GameManager.Instance.StartCurrentLevel();

        HideStartUI();
    }


    void HideStartUI()
    {
        playButton.SetActive(false);
    }
}
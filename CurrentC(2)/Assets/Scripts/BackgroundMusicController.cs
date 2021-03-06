﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicController : MonoBehaviour
{
    public static BackgroundMusicController bgmc;

    private void Awake() {
        if (bgmc != null) {
            Destroy(gameObject);
        } else {
            bgmc = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}

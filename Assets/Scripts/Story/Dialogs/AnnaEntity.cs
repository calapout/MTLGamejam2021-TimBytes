﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Bytes;

public class AnnaEntity : DialogPlayer
{

    float pitch;

    private void Awake()
    {
        pitch = source.pitch;
    }

    private void Update()
    {
        //source.pitch = pitch + Mathf.Sin(Time.time) * .05f;
    }

}

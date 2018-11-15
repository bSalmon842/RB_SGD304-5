/*
Project: Land of Dreams (SGD305)
File: gameStats.cs
Author: Brock Salmon
Notice: (C) Copyright 2018 by Brock Salmon. All Rights Reserved.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class gameStats
{
    private static int progress = 1;
	
    public static int hubProgress
    {
        get
        {
            return progress;
        }
        set
        {
            progress = value;
        }
    }
}

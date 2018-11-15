/*
Project: Land of Dreams (SGD305)
File: unpause.cs
Author: Brock Salmon
Notice: (C) Copyright 2018 by Brock Salmon. All Rights Reserved.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unpause : MonoBehaviour
{
    public void TaskOnClick()
    {
		Time.timeScale = 1;
	}
}

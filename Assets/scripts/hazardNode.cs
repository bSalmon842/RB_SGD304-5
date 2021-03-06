/*
Project: Land of Dreams (SGD305)
File: hazardNode.cs
Author: Brock Salmon
Notice: (C) Copyright 2018 by Brock Salmon. All Rights Reserved.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hazardNode : MonoBehaviour
{
	void Awake()
	{
		levelHandler.nodesRemaining += 1;
	}
	
	void OnDestroy()
	{
		levelHandler.nodesRemaining -= 1;
	}
}

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

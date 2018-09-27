using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Holds information about the current level
public static class levelHandler
{
	public static bool isCompanionActive = false;
	public static bool isCompanionUnavailable = false;
	public static int currentEnemies = 0;
	public static int levelStatus;
	public static int nodesRemaining;
	public static bool playerIsCrouching;
	public static bool playerIsFacingRight = true;
}
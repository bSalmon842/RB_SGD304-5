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

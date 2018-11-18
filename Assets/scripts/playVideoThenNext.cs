/*
Project: Land of Dreams (SGD305)
File: playVideoThenNext.cs
Author: Brock Salmon
Notice: (C) Copyright 2018 by Brock Salmon. All Rights Reserved.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class playVideoThenNext : MonoBehaviour {
    
    public string sceneName;
    public VideoPlayer vidPlayer;
    
	void Awake()
    {
		vidPlayer = GetComponent<VideoPlayer>();
        vidPlayer.loopPointReached += EndReached;
    }
    
    void FixedUpdate()
    {
        if (Input.GetButtonDown("Skip"))
        {
            EndReached(vidPlayer);
        }
    }
	
    void EndReached(VideoPlayer videoPlayer)
    {
        StartCoroutine(LoadNextScene());
    }
    
    IEnumerator LoadNextScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}

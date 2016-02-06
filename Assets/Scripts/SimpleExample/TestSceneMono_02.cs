﻿using System;
using UnityEngine;
using SceneUtils;

public class TestSceneMono_02 : SceneMonoBehaviour
{
	protected override void InitializeScene()
	{
		Debug.Log("TestScene_02 Init");
		
		SceneManager.Instance.StartTimer(3000);
	}
	
	protected override void FinalizeScene()
	{
		Debug.Log("TestScene_02 Fin");
	}
	
	protected override void OnTimerComplete(EventArgs e)
	{
		SceneManager.Instance.GotoNextScene();
	}
	protected override void OnStageClicked(EventArgs e)
	{
		SceneManager.Instance.GotoSceneById(1);
	}
}
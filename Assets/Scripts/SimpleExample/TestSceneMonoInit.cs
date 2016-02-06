using System;
using UnityEngine;
using SceneUtils;

public class TestSceneMonoInit : SceneMonoBehaviour
{
	protected override void InitializeScene()
	{
		Debug.Log("TestSceneInit Init");
		
		manager.StartTimer(1000);
	}
	
	protected override void FinalizeScene()
	{
		Debug.Log("TestSceneInit Fin");
	}
	
	protected override void OnTimerComplete(EventArgs e)
	{
		manager.GotoNextScene();
	}
}
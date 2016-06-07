using System;
using UnityEngine;
using SceneUtils;

public class TestSceneMono_02 : SceneMonoBehaviour
{
	protected override void InitializeScene()
	{
		Debug.Log("TestScene_02 Init");
		
		Manager.StartTimer(3000);
	}
	
	protected override void FinalizeScene()
	{
		Debug.Log("TestScene_02 Fin");
	}
	
	protected override void OnTimerComplete(EventArgs e)
	{
		Manager.GotoNextScene();
	}
	protected override void OnStageClicked(EventArgs e)
	{
		Manager.GotoSceneById(1);
	}
}
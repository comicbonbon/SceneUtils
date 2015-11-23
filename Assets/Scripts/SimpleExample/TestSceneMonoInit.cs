/*
 * SharpDevelopによって生成
 * ユーザ: sugawara
 * 日付: 2014/06/25
 * 時刻: 20:54
 * 
 * このテンプレートを変更する場合「ツール→オプション→コーディング→標準ヘッダの編集」
 */
using System;
using UnityEngine;
using SceneUtils;

public class TestSceneMonoInit : SceneMonoBehaviour
{
	protected override void InitializeScene()
	{
		Debug.Log("TestSceneInit Init");
		
		SceneManager.GetInstance().StartTimer(1000);
	}
	
	protected override void FinalizeScene()
	{
		Debug.Log("TestSceneInit Fin");
	}
	
	protected override void OnTimerComplete(EventArgs e)
	{
		SceneManager.GetInstance().GotoNextScene();
	}
}
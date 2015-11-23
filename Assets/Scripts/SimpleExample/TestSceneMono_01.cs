/*
 * SharpDevelopによって生成
 * ユーザ: sugawara
 * 日付: 2014/06/25
 * 時刻: 20:14
 * 
 * このテンプレートを変更する場合「ツール→オプション→コーディング→標準ヘッダの編集」
 */
using System;
using UnityEngine;
using SceneUtils;

public class TestSceneMono_01 : SceneMonoBehaviour
{
	protected override void InitializeScene()
	{
		Debug.Log("TestScene_01 Init");
		
		SceneManager.GetInstance().StartTimer(2000);
	}
	
	protected override void FinalizeScene()
	{
		Debug.Log("TestScene_01 Fin");
	}
	
	protected override void OnTimerComplete(EventArgs e)
	{
		SceneManager.GetInstance().GotoNextScene();
	}
}
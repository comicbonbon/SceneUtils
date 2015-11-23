/*
 * SharpDevelopによって生成
 * ユーザ: sugawara
 * 日付: 2014/06/26
 * 時刻: 11:23
 * 
 * このテンプレートを変更する場合「ツール→オプション→コーディング→標準ヘッダの編集」
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SceneUtils
{
	/// <summary>
	/// Description of SceneEvent.
	/// </summary>
	
	public delegate void ClickHandler(EventArgs e);
	public delegate void TimerCompleteHandler(EventArgs e);
	public delegate void ChangeSceneHandler(EventArgs e);
	public delegate void EndSceneHandler(EventArgs e);
	
	public delegate void EndFinalizedHandler(EventArgs e);
	
	public enum SceneEvent
	{
		CLICK_STAGE,
		TIMER_COMPLETE,
		CHANGE_SCENE,
		END_SCENE
	}
}

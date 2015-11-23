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

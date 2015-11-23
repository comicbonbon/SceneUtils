/*
 * SharpDevelopによって生成
 * ユーザ: sugawara
 * 日付: 2014/06/25
 * 時刻: 14:33
 * 
 * このテンプレートを変更する場合「ツール→オプション→コーディング→標準ヘッダの編集」
 */
using System;
using System.Timers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using UnityEngine.UI;

namespace SceneUtils
{
    /// <summary>
    /// SceneMonoBehaviourを管理するスクリプト
    /// InspectorからSceneMonoBehaviourを登録して実行順番を管理する
    /// initialize→イベント発生→シーン遷移→finalize→initialize→・・・
    /// </summary>

    public class SceneManager : MonoBehaviour
	{
		public event ClickHandler stageClicked;
		public event TimerCompleteHandler timerComplete;
		public event ChangeSceneHandler changeScene;
		public event EndSceneHandler endScene;
		
		private static SceneManager instance = null;
		
		// Inspectorで設定するのでPublic
		public int initialSceneId = 0;
		public int finalSceneId= 0; // 強制終了用
		public List<SceneMonoBehaviour> scenes = new List<SceneMonoBehaviour>();
		
		private Timer timer = new Timer();
		private int currentSceneId = 0;
		private int inputGotoId = 0;
		
		private bool isTimerComplete = false;

        void Start()
		{
			currentSceneId = initialSceneId;
			
			foreach(var scene in scenes)
			{
				scene.enabled = false;
			}
			
			if(!SceneManager.instance)	{SceneManager.instance = this;}
			scenes[currentSceneId].enabled = true;
            scenes[currentSceneId].InitializeEvent();
        }

        void Update()
		{
			if(Input.GetMouseButtonDown(0))	// 左クリック
			{
				OnClicked();
			}
			
			if(isTimerComplete)
			{
				if(timerComplete != null)	{timerComplete(EventArgs.Empty);}
				isTimerComplete = false;
			}
		}
		private void OnClicked()
		{
			if(stageClicked != null)
			{
				stageClicked(EventArgs.Empty);
			}
		}
		
		public void StartTimer(double msec)
		{
			timer.Interval = msec;
			timer.Elapsed += LaunchTimerEvent;
			timer.AutoReset = false;
			timer.Start();
		}
		public void StopTimer()
		{
			timer.Elapsed -= LaunchTimerEvent;
			timer.Stop();
		}
		private void LaunchTimerEvent(object sender, EventArgs e)
		{
			timer.Elapsed -= LaunchTimerEvent;
			timer.Stop();
			
			// ここで実行
			//if(timerComplete != null)	{timerComplete(EventArgs.Empty);}
			
			// Update内で実行(Mainスレッド内で実行)
			isTimerComplete = true;
		}
			
		public void GotoNextScene()
		{
			// 終了時にTimerが作動していた場合は終了する
			timer.Elapsed -= LaunchTimerEvent;
			timer.Stop();
			
			if(changeScene != null)		{changeScene(EventArgs.Empty);}

            StartCoroutine(GotoNextSceneProcess(EventArgs.Empty));
		}
		private IEnumerator GotoNextSceneProcess(EventArgs e)
		{
            scenes[currentSceneId % scenes.Count].FinalizeEvent();
            scenes[currentSceneId % scenes.Count].enabled = false;

            currentSceneId += 1;
            scenes[currentSceneId % scenes.Count].enabled = true;
            scenes[currentSceneId % scenes.Count].InitializeEvent();
            yield break;
		}
		
		public void GotoPrevScene()
		{
			// 終了時にTimerが作動していた場合は終了する
			timer.Elapsed -= LaunchTimerEvent;
			timer.Stop();
			
			if(changeScene != null)		{changeScene(EventArgs.Empty);}

            StartCoroutine(GotoPrevSceneProcess(EventArgs.Empty));
		}
		private IEnumerator GotoPrevSceneProcess(EventArgs e)
		{
			scenes[currentSceneId % scenes.Count].FinalizeEvent();
			scenes[currentSceneId % scenes.Count].enabled = false;
			
			currentSceneId -= 1;
			scenes[currentSceneId % scenes.Count].enabled = true;
			scenes[currentSceneId % scenes.Count].InitializeEvent();

            yield break;
		}
		
		public void GotoSceneById(int id)
		{
			// 終了時にTimerが作動していた場合は終了する
			timer.Elapsed -= LaunchTimerEvent;
			timer.Stop();
			
			inputGotoId = id;
			
			if(changeScene != null)		{changeScene(EventArgs.Empty);}

            StartCoroutine(GotoSceneByIdProcess(EventArgs.Empty));
		}
		private IEnumerator GotoSceneByIdProcess(EventArgs e)
		{
			scenes[currentSceneId % scenes.Count].FinalizeEvent();
			scenes[currentSceneId % scenes.Count].enabled = false;
			
			currentSceneId = inputGotoId;
			scenes[currentSceneId % scenes.Count].enabled = true;
			scenes[currentSceneId % scenes.Count].InitializeEvent();

            yield break;
		}
		
		public void GotoEnd()
		{
			// 終了時にTimerが作動していた場合は終了する
			timer.Elapsed -= LaunchTimerEvent;
			timer.Stop();

            StartCoroutine(GotoEndProcess(EventArgs.Empty));
		}
		private IEnumerator GotoEndProcess(EventArgs e)
		{
			scenes[currentSceneId % scenes.Count].FinalizeEvent();
			scenes[currentSceneId % scenes.Count].enabled = false;
			if(endScene != null)		{endScene(EventArgs.Empty);}

            yield break;
		}
		
		public void GotoFinalScene()
		{
			// 終了時にTimerが作動していた場合は終了する
			timer.Elapsed -= LaunchTimerEvent;
			timer.Stop();
			
			inputGotoId = finalSceneId;
			
			if(changeScene != null)		{changeScene(EventArgs.Empty);}

            StartCoroutine(GotoFinalSceneProcess(EventArgs.Empty));			
		}
		private IEnumerator GotoFinalSceneProcess(EventArgs e)
		{
			scenes[currentSceneId % scenes.Count].FinalizeEvent();
			scenes[currentSceneId % scenes.Count].enabled = false;
			
			currentSceneId = inputGotoId;
			scenes[currentSceneId % scenes.Count].enabled = true;
			scenes[currentSceneId % scenes.Count].InitializeEvent();

            yield break;
		}
		
		public static SceneManager GetInstance()
		{
			return SceneManager.instance;
		}
	}
}

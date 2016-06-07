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

	using SceneList = List<SceneMonoBehaviour>;
    using SceneTable = Dictionary<string, SceneMonoBehaviour>;

	public class SceneManager : MonoBehaviour
	{
		public event ClickHandler stageClicked;
		public event TimerCompleteHandler timerComplete;
		public event ChangeSceneHandler changeScene;
		public event EndSceneHandler endScene;

		[SerializeField]
		private int initialSceneId = 0;
		[SerializeField]
		private int finalSceneId= 0; // 強制終了用

		[Space(10)]
		[SerializeField]
		private SceneList scenes = new SceneList();
		private SceneTable sceneTable = new SceneTable(); 
		
		private Timer timer = new Timer();
		private bool isTimerComplete = false;

		private int currentSceneId = 0;
		private int tempId = 0;

        void Start()
		{
			currentSceneId = initialSceneId;

			foreach (var scene in scenes)
			{
				try
				{
					sceneTable.Add(scene.UniqueName, scene);
				}
				catch (ArgumentException)
				{
					throw new ArgumentException(string.Format("{0}[{1}].UniqueName is not unique.", scene.GetType().ToString(), scene.UniqueName));
				}
			}

			foreach (var scene in scenes)
			{
				scene.enabled = false;
			}
			
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
			
			tempId = id;
			
			if(changeScene != null)		{changeScene(EventArgs.Empty);}

            StartCoroutine(GotoSceneByIdProcess(EventArgs.Empty));
		}
		private IEnumerator GotoSceneByIdProcess(EventArgs e)
		{
			scenes[currentSceneId % scenes.Count].FinalizeEvent();
			scenes[currentSceneId % scenes.Count].enabled = false;
			
			currentSceneId = tempId;
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
			
			tempId = finalSceneId;
			
			if(changeScene != null)		{changeScene(EventArgs.Empty);}

            StartCoroutine(GotoFinalSceneProcess(EventArgs.Empty));			
		}
		private IEnumerator GotoFinalSceneProcess(EventArgs e)
		{
			scenes[currentSceneId % scenes.Count].FinalizeEvent();
			scenes[currentSceneId % scenes.Count].enabled = false;
			
			currentSceneId = tempId;
			scenes[currentSceneId % scenes.Count].enabled = true;
			scenes[currentSceneId % scenes.Count].InitializeEvent();

            yield break;
		}

		public void GotoSceneByName(string key)
		{
			// 終了時にTimerが作動していた場合は終了する
			timer.Elapsed -= LaunchTimerEvent;
			timer.Stop();

			if (changeScene != null) { changeScene(EventArgs.Empty); }

			StartCoroutine(GotoSceneByNameProcess(key, EventArgs.Empty));
		}
		private IEnumerator GotoSceneByNameProcess(string key, EventArgs e)
		{
			scenes[currentSceneId % scenes.Count].FinalizeEvent();
			scenes[currentSceneId % scenes.Count].enabled = false;

			// 移動先のIDを取得
			currentSceneId = scenes.IndexOf(sceneTable[key]);

			if (currentSceneId == -1)
				throw new ArgumentException(string.Format("[{0}] key is not found.", key));

			sceneTable[key].enabled = true;
			sceneTable[key].InitializeEvent();

			yield break;
		}
	}
}

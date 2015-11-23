using System;
using System.Collections.Generic;
using UnityEngine;

namespace SceneUtils
{
	/// <summary>
	/// Description of SceneMonoBehaviour.
	/// </summary>
	
	public class SceneMonoBehaviour : MonoBehaviour
	{
		// StartまたはUpdateが無いとInspectorにenabledのチェックボックスが表示されなくなる
		// あったほうがDebugしやすいので追加
		void Start()	{}
		
		public void InitializeEvent()
		{
			SceneManager.GetInstance().stageClicked += OnStageClicked;
			SceneManager.GetInstance().timerComplete += OnTimerComplete;
			SceneManager.GetInstance().changeScene += OnChangeScene;
			SceneManager.GetInstance().endScene += OnEndScene;

            InitializeScene();
		}
		protected virtual void InitializeScene()
		{
			throw new NotImplementedException();
		}
		
		public void FinalizeEvent()
		{
			SceneManager.GetInstance().stageClicked -= OnStageClicked;
			SceneManager.GetInstance().timerComplete -= OnTimerComplete;
			SceneManager.GetInstance().changeScene -= OnChangeScene;
			SceneManager.GetInstance().endScene -= OnEndScene;

            FinalizeScene();
		}
		protected virtual void FinalizeScene()
		{
			throw new NotImplementedException();
		}
		
		protected virtual void OnStageClicked(EventArgs e)	{}
		protected virtual void OnTimerComplete(EventArgs e)	{}
		protected virtual void OnChangeScene(EventArgs e)	{}
		protected virtual void OnEndScene(EventArgs e)		{}
	}
}

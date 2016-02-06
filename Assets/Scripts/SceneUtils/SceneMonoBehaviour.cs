using System;
using System.Collections.Generic;
using UnityEngine;

namespace SceneUtils
{
	/// <summary>
	/// Description of SceneMonoBehaviour.
	/// </summary>

	[RequireComponent(typeof(SceneManager))]
	public class SceneMonoBehaviour : MonoBehaviour
	{
		private SceneManager _manager = null;
		protected SceneManager manager
		{
			get
			{
				if(_manager == null)
				{
					_manager = GetComponent<SceneManager>();
				}

				return _manager;
			}
		}

		// StartまたはUpdateが無いとInspectorにenabledのチェックボックスが表示されなくなる
		// あったほうがDebugしやすいので追加
		void Start()	{}
		
		public void InitializeEvent()
		{
			manager.stageClicked += OnStageClicked;
			manager.timerComplete += OnTimerComplete;
			manager.changeScene += OnChangeScene;
			manager.endScene += OnEndScene;

            InitializeScene();
		}
		protected virtual void InitializeScene()
		{
			throw new NotImplementedException();
		}
		
		public void FinalizeEvent()
		{
			manager.stageClicked -= OnStageClicked;
			manager.timerComplete -= OnTimerComplete;
			manager.changeScene -= OnChangeScene;
			manager.endScene -= OnEndScene;

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

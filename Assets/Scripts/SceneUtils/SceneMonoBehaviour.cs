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
		private SceneManager manager = null;
		protected SceneManager Manager
		{
			get
			{
				if(manager == null)
				{
					manager = GetComponent<SceneManager>();
				}

				return manager;
			}
		}

		// Script名をUniqueな名前として使用するかどうか
		[SerializeField]
		private bool usingScriptName = false;
		// Uniqueな名前を持たせる
		[SerializeField]
		private string uniqueName = "";
		public string UniqueName
		{
			get
			{
				return uniqueName;
			}
		}

		void Awake()
		{
			if (uniqueName == "")
				uniqueName = System.Guid.NewGuid().ToString();

			if (usingScriptName)
				uniqueName = this.GetType().ToString();

			OnAwake();
		}

		// StartまたはUpdateが無いとInspectorにenabledのチェックボックスが表示されなくなる
		// あったほうがDebugしやすいので追加
		void Start()	{}
		
		public void InitializeEvent()
		{
			Manager.stageClicked += OnStageClicked;
			Manager.timerComplete += OnTimerComplete;
			Manager.changeScene += OnChangeScene;
			Manager.endScene += OnEndScene;

            InitializeScene();
		}
		protected virtual void InitializeScene()
		{
			throw new NotImplementedException();
		}
		
		public void FinalizeEvent()
		{
			Manager.stageClicked -= OnStageClicked;
			Manager.timerComplete -= OnTimerComplete;
			Manager.changeScene -= OnChangeScene;
			Manager.endScene -= OnEndScene;

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

		protected virtual void OnAwake() {}
	}
}

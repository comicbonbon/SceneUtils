using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using SceneUtils;
using UnityEngine.UI;
using System;

namespace ButtonSample
{
	public class ButtonExampleBase : SceneMonoBehaviour
	{
		[SerializeField]
		protected Canvas canvas;

		protected List<Button> btns = new List<Button>();

		void Awake()
		{
			// GameObjectが非アクティブ状態だとComponentの取得ができないのでAwakeでButtonをあらかじめ取得しておく
			btns = new List<Button>(canvas.GetComponentsInChildren<Button>());
		}

		protected override void InitializeScene()
		{
			Debug.Log("Base Init");

			// タイムアウトの設定
			SceneManager.Instance.StartTimer(5000.0f);

			InitialProcess();
		}

		protected virtual void InitialProcess()
		{
			throw new NotImplementedException();
		}

		protected override void OnTimerComplete(EventArgs e)
		{
			// Initial画面へ
			SceneManager.Instance.GotoSceneById(0);
		}

		protected override void FinalizeScene()
		{
			foreach (var btn in btns)
			{
				btn.gameObject.SetActive(false);
				btn.onClick.RemoveAllListeners();
			}
		}

		protected virtual void OnClickToNext()
		{
			SceneManager.Instance.GotoNextScene();
		}
	}
}

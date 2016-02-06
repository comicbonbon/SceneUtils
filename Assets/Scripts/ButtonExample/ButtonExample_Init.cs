using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using SceneUtils;
using UnityEngine.UI;

namespace ButtonSample
{
	public class ButtonExample_Init : ButtonExampleBase
	{
		protected override void InitialProcess()
		{
			Debug.Log("Initial");

			foreach (var btn in btns)
			{
				btn.gameObject.SetActive(true);
			}
			btns[0].onClick.AddListener(OnClick_01);
			btns[1].onClick.AddListener(OnClick_02);
			btns[2].onClick.AddListener(OnClick_03);
		}

		private void OnClick_01()
		{
			Debug.Log("1");
			SceneManager.Instance.GotoSceneById(1);
		}
		private void OnClick_02()
		{
			Debug.Log("2");
			SceneManager.Instance.GotoSceneById(2);
		}
		private void OnClick_03()
		{
			Debug.Log("3");
			SceneManager.Instance.GotoSceneById(3);
		}
	}
}
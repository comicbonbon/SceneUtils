using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using SceneUtils;
using UnityEngine.UI;

namespace ButtonSample
{
	public class ButtonExample : ButtonExampleBase
	{
		[SerializeField]
		private Button btn;

		protected override void InitialProcess()
		{
			btn.gameObject.SetActive(true);
			btn.onClick.AddListener(OnClickToNext);
		}
	}
}
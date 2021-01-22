using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DG.Tweening.Plugins.Core.PathCore;
using UnityEditor;

namespace Assets.Scrips.Editor
{
	public class AutoBuilder
	{
		[MenuItem("Build/Android")]
		static void BuildAndroid()
		{
			BuildPipeline.BuildPlayer(new [] { "Assets/Scenes/MainScene.unity" }, "Deploy/Android/Tabletopia.apk", BuildTarget.Android, BuildOptions.None);
		}

		[MenuItem("Build/Windows")]
		static void BuildWindows()
		{
			BuildPipeline.BuildPlayer(new [] { "Assets/Scenes/MainScene.unity" }, "Deploy/Windows/Tabletopia.exe", BuildTarget.StandaloneWindows, BuildOptions.None);
		}
	}
}

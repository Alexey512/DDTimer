using System;
using UnityEngine;

namespace Assets.Scrips.Game.Config
{
    public class GameConfig: IGameConfig
    {
        private class Config
        {
            public int CubesCount;

            public float RespRadius;

            public float RespHeight;
        }

        private Config _config;

        public int CubesCount => _config?.CubesCount ?? 0;

        public float RespRadius => _config?.RespRadius ?? 0;

        public float RespHeight => _config?.RespHeight ?? 0;

        public GameConfig(TextAsset data)
        {
            if (data == null)
            {
                return;
            }
            
            Load(data.text);
        }

        private void Load(string data)
        {
            try
            {
	            _config = JsonUtility.FromJson<Config>(data);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }
}

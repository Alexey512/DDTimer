using System;
using UnityEngine;

namespace Assets.Scrips.Game.Config
{
    public class GameConfig: IGameConfig
    {
	    private class Config
	    {
		    public int TimersCount;

		    public float StartDuration;
	    }

	    private Config _config;

        public int TimersCount => _config?.TimersCount ?? 0;

        public float StartDuration => _config?.StartDuration ?? 0;
        
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

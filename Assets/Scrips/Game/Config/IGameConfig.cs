using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scrips.Game.Config
{
    public interface IGameConfig
    {
        int TimersCount { get; }

        float StartDuration { get; }
    }
}

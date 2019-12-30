using DemoMinifier.Models;
using ProtoBuf;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace DemoMinifier
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            Minifier minifier = new Minifier();
            //MinifiedDemo demo = await minifier.MinifyDemoAsync("g2-vs-fnatic-m3-overpass.dem", new System.Threading.CancellationToken());

            //demo.SaveCompressed("g2-vs-fnatic-m3-overpass.minidem");

            MinifiedDemo demo = await minifier.MinifyDemoAsync("vitality-vs-mousesports-m2-mirage.dem", new System.Threading.CancellationToken());

            int baseCount = 0;
            int positionCount = 0;
            int fullCount = 0;

            foreach (var tick in demo.Ticks)
            {
                baseCount += tick.PlayerStates.Count((s) => s.Type == PlayerStateType.Base);
                positionCount += tick.PlayerStates.Count((s) => s.Type == PlayerStateType.Position);
                fullCount += tick.PlayerStates.Count((s) => s.Type == PlayerStateType.Full);
            }

            int entityBaseCount = 0;
            int entityFullCount = 0;

            foreach (var tick in demo.Ticks)
            {
                entityBaseCount += tick.EntityStates.Count((s) => s.Type == EntityStateType.Base);
                entityFullCount += tick.EntityStates.Count((s) => s.Type == EntityStateType.Full);
            }

            demo.SaveCompressed("vitality-vs-mousesports-m2-mirage.minidem");

            //MinifiedDemo demo = await minifier.MinifyDemoAsync("test2.dem", new System.Threading.CancellationToken());

            //demo.SaveCompressed("test2.minidem");
        }

    }
}

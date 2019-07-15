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
            MinifiedDemo demo = await minifier.MinifyDemoAsync("g2-vs-fnatic-m3-overpass.dem", new System.Threading.CancellationToken());

            demo.SaveCompressed("g2-vs-fnatic-m3-overpass.minidem");
        }
    }
}

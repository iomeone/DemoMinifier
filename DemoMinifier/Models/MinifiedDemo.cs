using K4os.Compression.LZ4.Streams;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DemoMinifier.Models
{
    [ProtoContract]
    public class MinifiedDemo
    {
        [ProtoMember(1)]
        public string ServerName { get; set; }
        [ProtoMember(2)]
        public string ClientName { get; set; }
        [ProtoMember(3)]
        public string MapName { get; set; }
        [ProtoMember(4)]
        public float PlaybackTime { get; set; }
        [ProtoMember(5)]
        public int PlaybackTicks { get; set; }
        [ProtoMember(6)]
        public int PlaybackFrames { get; set; }

        [ProtoMember(7)]
        public Dictionary<byte, long> SteamIDMappings { get; set; }
        [ProtoMember(8)]
        public Dictionary<byte, string> NameMappings { get; set; }

        [ProtoMember(9)]
        public List<Tick> Ticks { get; set; }

        public MinifiedDemo()
        {
            SteamIDMappings = new Dictionary<byte, long>();
            NameMappings = new Dictionary<byte, string>();
            Ticks = new List<Tick>();
        }

        public void Save(string fileName)
        {
            using (var file = File.Create(fileName))
            {
                Serializer.Serialize(file, this);
            }
        }

        public void SaveCompressed(string fileName)
        {
            using (var file = LZ4Stream.Encode(File.Create(fileName)))
            {
                Serializer.Serialize(file, this);
            }
        }

        public static MinifiedDemo Load(string fileName)
        {
            MinifiedDemo deserialisedDemo;
            using (var file = File.OpenRead(fileName))
            {
                deserialisedDemo = Serializer.Deserialize<MinifiedDemo>(file);
            }

            return deserialisedDemo;
        }

        public static MinifiedDemo LoadCompressed(string fileName)
        {
            MinifiedDemo deserialisedDemo;
            using (var file = LZ4Stream.Decode(File.OpenRead(fileName)))
            {
                deserialisedDemo = Serializer.Deserialize<MinifiedDemo>(file);
            }

            return deserialisedDemo;
        }
    }
}

using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoMinifier.Models
{
    [ProtoContract]
    public class Entity
    {
        [ProtoMember(1)]
        public int ID { get; set; }

        [ProtoMember(2)]
        public EntityType Type { get; set; }

        [ProtoMember(3)]
        public Vector Position { get; set; }

        [ProtoMember(4)]
        public Vector Rotation { get; set; }

        [ProtoMember(5)]
        public int ModelIndex { get; set; }

        [ProtoMember(6)]
        public string ModelLocation { get; set; }
    }

    public enum EntityType
    {
        Base = 0,
        Dynamic = 1,
        PhysicsPropMultiplayer = 2,
        PropDoorRotating = 3,
        Chicken = 4,
    }
}

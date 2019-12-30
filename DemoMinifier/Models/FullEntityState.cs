using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoMinifier.Models
{
    [ProtoContract]
    public class FullEntityState : BaseEntityState
    {
        [ProtoMember(3)]
        public EntityType EntityType { get; set; }
        [ProtoMember(4)]
        public Vector Position { get; set; }
        [ProtoMember(5)]
        public Vector Rotation { get; set; }
        [ProtoMember(6)]
        public int ModelIndex { get; set; }
        [ProtoMember(7)]
        public string ModelLocation { get; set; }

        public FullEntityState()
        {
            Type = EntityStateType.Full;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            FullEntityState other = (FullEntityState)obj;

            if (EntityType != other.EntityType)
                return false;

            if (!Position.Equals(other.Position))
                return false;

            if (!Rotation.Equals(other.Rotation))
                return false;

            if (ModelIndex != other.ModelIndex)
                return false;

            if (ModelLocation != other.ModelLocation)
                return false;

            return true;
        }
    }
}

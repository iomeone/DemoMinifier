using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoMinifier.Models
{
    [ProtoContract]
    [ProtoInclude(30, typeof(FullEntityState))]
    public class BaseEntityState
    {
        [ProtoMember(1)]
        public EntityStateType Type { get; set; }

        [ProtoMember(2)]
        public short ID { get; set; }


        public BaseEntityState()
        {
            Type = EntityStateType.Base;
        }

        public BaseEntityState(FullEntityState fullState)
        {
            Type = EntityStateType.Base;
            ID = fullState.ID;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            BaseEntityState other = (BaseEntityState)obj;

            if (Type != other.Type)
                return false;

            if (ID != other.ID)
                return false;

            return true;
        }
    }

    public enum EntityStateType : byte
    {
        Base = 0,
        Full = 1
    }
}

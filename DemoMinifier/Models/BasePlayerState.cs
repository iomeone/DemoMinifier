using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoMinifier.Models
{
    [ProtoContract]
    [ProtoInclude(30, typeof(PositionPlayerState))]
    public class BasePlayerState
    {
        [ProtoMember(1)]
        public PlayerStateType Type { get; set; }
        [ProtoMember(2)]
        public byte SteamIDIndex { get; set; }

        public BasePlayerState()
        {
            Type = PlayerStateType.Base;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            BasePlayerState other = (BasePlayerState)obj;

            if (Type != other.Type)
                return false;

            if (SteamIDIndex != other.SteamIDIndex)
                return false;

            return true;
        }
    }

    public enum PlayerStateType : byte
    {
        Base = 0,
        Position = 1,
        Full = 2
    }
}

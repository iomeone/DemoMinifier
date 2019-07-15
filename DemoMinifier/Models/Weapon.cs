using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoMinifier.Models
{
    [ProtoContract]
    public class Weapon
    {
        [ProtoMember(1)]
        public EquipmentElement Equipment { get; set; }
        public EquipmentClass Class
        {
            get
            {
                return (EquipmentClass)(((int)Equipment / 100) + 1);
            }
        }

        [ProtoMember(2)]
        public string OriginalString { get; set; }
        [ProtoMember(3)]
        public short AmmoInMagazine { get; set; }
        [ProtoMember(4)]
        public short AmmoType { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            Weapon other = (Weapon)obj;

            if (Equipment != other.Equipment)
                return false;

            if (OriginalString != other.OriginalString)
                return false;

            if (AmmoInMagazine != other.AmmoInMagazine)
                return false;

            if (AmmoType != other.AmmoType)
                return false;

            return true;
        }
    }
}

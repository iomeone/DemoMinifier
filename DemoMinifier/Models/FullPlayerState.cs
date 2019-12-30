using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemoMinifier.Models
{
    [ProtoContract]
    public class FullPlayerState : PositionPlayerState
    {
        [ProtoMember(8)]
        public byte NameIndex { get; set; }
        [ProtoMember(9)]
        public byte HP { get; set; }
        [ProtoMember(10)]
        public byte Armor { get; set; }
        [ProtoMember(11)]
        public float FlashDuration { get; set; }
        [ProtoMember(12)]
        public short Money { get; set; }
        [ProtoMember(13)]
        public short CurrentEquipmentValue { get; set; }
        [ProtoMember(14)]
        public short FreezetimeEndEquipmentValue { get; set; }
        [ProtoMember(15)]
        public short RoundStartEquipmentValue { get; set; }
        [ProtoMember(16)]
        public bool IsDucking { get; set; }
        [ProtoMember(17)]
        public byte ActiveWeaponID { get; set; }
        [ProtoMember(18)]
        public Team Team { get; set; }
        [ProtoMember(19)]
        public bool HasDefuseKit { get; set; }
        [ProtoMember(20)]
        public bool HasHelmet { get; set; }
        [ProtoMember(21)]
        public Dictionary<byte, byte> AmmoLeft { get; set; }
        [ProtoMember(22)]
        public Dictionary<short, Weapon> Weapons { get; set; }
        [ProtoMember(23)]
        public bool IsScoped { get; set; }
        [ProtoMember(24)]
        public int ShotsFired { get; set; }
        [ProtoMember(25)]
        public Vector AimPunchAngle { get; set; }

        public FullPlayerState()
        {
            Type = PlayerStateType.Full;
            AmmoLeft = new Dictionary<byte, byte>();
            Weapons = new Dictionary<short, Weapon>();
        }


        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            FullPlayerState other = (FullPlayerState)obj;

            if (NameIndex != other.NameIndex)
                return false;

            if (HP != other.HP)
                return false;

            if (Armor != other.Armor)
                return false;

            if (FlashDuration != other.FlashDuration)
                return false;

            if (Money != other.Money)
                return false;

            if (CurrentEquipmentValue != other.CurrentEquipmentValue)
                return false;

            if (FreezetimeEndEquipmentValue != other.FreezetimeEndEquipmentValue)
                return false;

            if (RoundStartEquipmentValue != other.RoundStartEquipmentValue)
                return false;

            if (IsDucking != other.IsDucking)
                return false;

            if (ActiveWeaponID != other.ActiveWeaponID)
                return false;

            if (Team != other.Team)
                return false;

            if (HasDefuseKit != other.HasDefuseKit)
                return false;

            if (HasHelmet != other.HasHelmet)
                return false;

            if (AmmoLeft.Count != other.AmmoLeft.Count)
                return false;

            if (IsScoped != other.IsScoped)
                return false;

            if (ShotsFired != other.ShotsFired)
                return false;

            if (!AimPunchAngle.Equals(other.AimPunchAngle))
                return false;


            var ammoList = AmmoLeft.ToList();
            var otherAmmoList = other.AmmoLeft.ToList();

            for (int i = 0; i < ammoList.Count; i++)
            {
                if (ammoList[i].Key != otherAmmoList[i].Key)
                    return false;

                if (ammoList[i].Value != otherAmmoList[i].Value)
                    return false;
            }

            if (Weapons.Count != other.Weapons.Count)
                return false;

            var weaponsList = Weapons.ToList();
            var otherWeaponsList = other.Weapons.ToList();

            for (int i = 0; i < weaponsList.Count; i++)
            {
                if (weaponsList[i].Key != otherWeaponsList[i].Key)
                    return false;

                if (!weaponsList[i].Value.Equals(otherWeaponsList[i].Value))
                    return false;
            }

            return true;
        }
    }
}

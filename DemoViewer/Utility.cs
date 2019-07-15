using DemoMinifier.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;

namespace DemoViewer
{
    public static class Utility
    {
        public static BitmapImage GetWeaponIcon(Weapon weapon)
        {
            if (weapon == null)
                return new BitmapImage();

            switch (weapon.Equipment)
            {
                case EquipmentElement.P2000:
                    return new BitmapImage(new Uri(@"./killicons/p2000.png", UriKind.Relative));
                case EquipmentElement.Glock:
                    return new BitmapImage(new Uri(@"./killicons/glock.png", UriKind.Relative));
                case EquipmentElement.P250:
                    return new BitmapImage(new Uri(@"./killicons/p250.png", UriKind.Relative));
                case EquipmentElement.Deagle:
                    return new BitmapImage(new Uri(@"./killicons/deagle.png", UriKind.Relative));
                case EquipmentElement.FiveSeven:
                    return new BitmapImage(new Uri(@"./killicons/fiveseven.png", UriKind.Relative));
                case EquipmentElement.DualBarettas:
                    return new BitmapImage(new Uri(@"./killicons/dualies.png", UriKind.Relative));
                case EquipmentElement.Tec9:
                    return new BitmapImage(new Uri(@"./killicons/tec9.png", UriKind.Relative));
                case EquipmentElement.CZ:
                    return new BitmapImage(new Uri(@"./killicons/cz75.png", UriKind.Relative));
                case EquipmentElement.USP:
                    return new BitmapImage(new Uri(@"./killicons/usp_silencer.png", UriKind.Relative));
                case EquipmentElement.Revolver:
                    return new BitmapImage(new Uri(@"./killicons/suicide.png", UriKind.Relative));
                case EquipmentElement.MP7:
                    return new BitmapImage(new Uri(@"./killicons/mp7.png", UriKind.Relative));
                case EquipmentElement.MP9:
                    return new BitmapImage(new Uri(@"./killicons/mp9.png", UriKind.Relative));
                case EquipmentElement.Bizon:
                    return new BitmapImage(new Uri(@"./killicons/bizon.png", UriKind.Relative));
                case EquipmentElement.Mac10:
                    return new BitmapImage(new Uri(@"./killicons/mac10.png", UriKind.Relative));
                case EquipmentElement.UMP:
                    return new BitmapImage(new Uri(@"./killicons/ump45.png", UriKind.Relative));
                case EquipmentElement.P90:
                    return new BitmapImage(new Uri(@"./killicons/p90.png", UriKind.Relative));
                case EquipmentElement.MP5SD:
                    return new BitmapImage(new Uri(@"./killicons/mp7.png", UriKind.Relative));
                case EquipmentElement.SawedOff:
                    return new BitmapImage(new Uri(@"./killicons/sawedoff.png", UriKind.Relative));
                case EquipmentElement.Nova:
                    return new BitmapImage(new Uri(@"./killicons/nova.png", UriKind.Relative));
                case EquipmentElement.Swag7:
                    return new BitmapImage(new Uri(@"./killicons/mag7.png", UriKind.Relative));
                case EquipmentElement.XM1014:
                    return new BitmapImage(new Uri(@"./killicons/xm1014.png", UriKind.Relative));
                case EquipmentElement.M249:
                    return new BitmapImage(new Uri(@"./killicons/m249.png", UriKind.Relative));
                case EquipmentElement.Negev:
                    return new BitmapImage(new Uri(@"./killicons/negev.png", UriKind.Relative));
                case EquipmentElement.Gallil:
                    return new BitmapImage(new Uri(@"./killicons/galilar.png", UriKind.Relative));
                case EquipmentElement.Famas:
                    return new BitmapImage(new Uri(@"./killicons/famas.png", UriKind.Relative));
                case EquipmentElement.AK47:
                    return new BitmapImage(new Uri(@"./killicons/ak47.png", UriKind.Relative));
                case EquipmentElement.M4A4:
                    return new BitmapImage(new Uri(@"./killicons/m4a1.png", UriKind.Relative));
                case EquipmentElement.M4A1:
                    return new BitmapImage(new Uri(@"./killicons/m4a1_silencer.png", UriKind.Relative));
                case EquipmentElement.Scout:
                    return new BitmapImage(new Uri(@"./killicons/ssg08.png", UriKind.Relative));
                case EquipmentElement.SG556:
                    return new BitmapImage(new Uri(@"./killicons/sg556.png", UriKind.Relative));
                case EquipmentElement.AUG:
                    return new BitmapImage(new Uri(@"./killicons/aug.png", UriKind.Relative));
                case EquipmentElement.AWP:
                    return new BitmapImage(new Uri(@"./killicons/awp.png", UriKind.Relative));
                case EquipmentElement.Scar20:
                    return new BitmapImage(new Uri(@"./killicons/scar20.png", UriKind.Relative));
                case EquipmentElement.G3SG1:
                    return new BitmapImage(new Uri(@"./killicons/g3sg1.png", UriKind.Relative));
                case EquipmentElement.Zeus:
                    return new BitmapImage(new Uri(@"./killicons/taser.png", UriKind.Relative));
                case EquipmentElement.Kevlar:
                    return new BitmapImage(new Uri(@"./killicons/suicide.png", UriKind.Relative));
                case EquipmentElement.Helmet:
                    return new BitmapImage(new Uri(@"./killicons/suicide.png", UriKind.Relative));
                case EquipmentElement.Bomb:
                    return new BitmapImage(new Uri(@"./killicons/suicide.png", UriKind.Relative));
                case EquipmentElement.Knife:
                    return new BitmapImage(new Uri(@"./killicons/knife_bayonet.png", UriKind.Relative));
                case EquipmentElement.DefuseKit:
                    return new BitmapImage(new Uri(@"./killicons/suicide.png", UriKind.Relative));
                case EquipmentElement.World:
                    return new BitmapImage(new Uri(@"./killicons/suicide.png", UriKind.Relative));
                case EquipmentElement.Decoy:
                    return new BitmapImage(new Uri(@"./killicons/decoy.png", UriKind.Relative));
                case EquipmentElement.Molotov:
                    return new BitmapImage(new Uri(@"./killicons/inferno.png", UriKind.Relative));
                case EquipmentElement.Incendiary:
                    return new BitmapImage(new Uri(@"./killicons/inferno.png", UriKind.Relative));
                case EquipmentElement.Flash:
                    return new BitmapImage(new Uri(@"./killicons/flashbang.png", UriKind.Relative));
                case EquipmentElement.Smoke:
                    return new BitmapImage(new Uri(@"./killicons/smokegrenade.png", UriKind.Relative));
                case EquipmentElement.HE:
                    return new BitmapImage(new Uri(@"./killicons/hegrenade.png", UriKind.Relative));
                default:
                    return new BitmapImage(new Uri(@"./killicons/suicide.png", UriKind.Relative)); ;
            }
        }
    }
}

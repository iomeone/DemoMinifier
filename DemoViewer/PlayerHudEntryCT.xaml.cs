using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DemoMinifier.Models;

namespace DemoViewer
{
    /// <summary>
    /// Interaction logic for PlayerHudEntry.xaml
    /// </summary>
    public partial class PlayerHudEntryCT : UserControl
    {
        public PlayerHudEntryCT()
        {
            InitializeComponent();
        }

        string lastName = "";
        public void SetName(string name)
        {
            if (lastName == name)
                return;

            textName.Text = name;
            lastName = name;
        }

        int lastHp = -1;
        public void SetHP(int hp)
        {
            if (lastHp == hp)
                return;

            textHp.Text = "" + hp;
            barHp.Value = hp;
            lastHp = hp;
        }

        int lastMoney = -1;
        public void SetMoney(int money)
        {
            if (lastMoney == money)
                return;

            textMoney.Text = "$" + money;
            lastMoney = money;
        }

        int lastArmor = -1;
        public void SetArmor(int armor, bool helmet)
        {
            if (lastArmor == armor)
                return;

            if (armor > 0)
            {
                if (helmet)
                {
                    imageHelmet.Height = 20;
                    imageArmour.Height = 0;
                }
                else
                {
                    imageHelmet.Height = 0;
                    imageArmour.Height = 20;
                }
            }
            else
            {
                imageHelmet.Height = 0;
                imageArmour.Height = 0;
            }
            lastArmor = armor;
        }

        bool lastDefuse = true;
        public void SetDefuseKit(bool hasKit)
        {
            if (lastDefuse == hasKit)
                return;

            imageDefuse.Height = hasKit ? 20 : 0;
            lastDefuse = hasKit;
        }

        Weapon lastPrimary;
        public void SetPrimary(Weapon weapon)
        {
            if (lastPrimary == weapon)
                return;

            imageWeapon.Source = Utility.GetWeaponIcon(weapon);
            lastPrimary = weapon;
        }

        Weapon lastSecondary;
        public void SetSecondary(Weapon weapon)
        {
            if (lastSecondary == weapon)
                return;

            imagePistol.Source = Utility.GetWeaponIcon(weapon);
            lastSecondary = weapon;
        }

        bool lastSmoke = true;
        public void SetSmokeGrenade(bool hasNade)
        {
            if (lastSmoke == hasNade)
                return;

            imageNadeSmoke.Height = hasNade ? 20 : 0;
            lastSmoke = hasNade;
        }

        bool lastHE = true;
        public void SetHEGrenade(bool hasNade)
        {
            if (lastHE == hasNade)
                return;

            imageNadeExplosive.Height = hasNade ? 20 : 0;
            lastHE = hasNade;
        }

        int lastFlashCount = -1;
        public void SetFlashGrenadeCount(int count)
        {
            if (lastFlashCount == count)
                return;

            if (count == 0)
            {
                imageNadeFlash1.Height = 0;
                imageNadeFlash2.Height = 0;
            }
            else if (count == 1)
            {
                imageNadeFlash1.Height = 20;
                imageNadeFlash2.Height = 0;
            }
            else if (count == 2)
            {
                imageNadeFlash1.Height = 20;
                imageNadeFlash2.Height = 20;
            }

            lastFlashCount = count;
        }

        bool lastMolly = true;
        public void SetMolotov(bool hasNade)
        {
            if (lastMolly == hasNade)
                return;

            imageNadeMolotov.Height = hasNade ? 20 : 0;
            lastMolly = hasNade;
        }

        bool lastIncendiary = true;
        public void SetIncendiary(bool hasNade)
        {
            if (lastIncendiary == hasNade)
                return;

            imageNadeIncendiary.Height = hasNade ? 20 : 0;
            lastIncendiary = hasNade;
        }
    }
}

using Helen.Core;
using System;
using System.IO;

namespace Helen.Term
{
    class Program
    {
        static Actor A1 = new Actor(new Guid(1, 1, 1, new byte[8]), "Helen", 50);
        static Actor A2 = new Actor(new Guid(2, 2, 2, new byte[8]), "Mob", 25);
        static Actor[] PartyA = new Actor[Battle.MaxPartySize] { A1, null, null, null };
        static Actor[] PartyB = new Actor[Battle.MaxPartySize] { A2, null, null, null };

        static void Main(string[] args)
        {
            Console.Clear();
            Console.CursorVisible = false;

            // Add Equipment.
            A1.Add(new Weapon(Guid.NewGuid(), "Sword", 15, 10, 12));
            A1.Add(new Weapon(Guid.NewGuid(), "Bow", 7, 2, 5));
            A1.Add(new Weapon(Guid.NewGuid(), "Heal", 25, 0, 20, WeaponProperties.TrueHealing));
            A1.Add(new Weapon(Guid.NewGuid(), "Thunder", 25, 0, 20, WeaponProperties.Piercing));
            A2.Add(new Weapon(Guid.NewGuid(), "Claw", 5, 5, 7, 2));

            // Initialize Battle.
            new BattleDisplay(new Battle(PartyA, PartyB)).Commence();
        }
    }
}

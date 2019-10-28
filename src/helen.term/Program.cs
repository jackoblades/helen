using System.Threading.Tasks;
using Helen.Core;
using System.IO;
using System;

namespace Helen.Term
{
    class Program
    {
        static int PaddingLength = 75;
        static int MaxPartySize = 4;

        static Actor A1 = new Actor(new Guid(1, 1, 1, new byte[8]), "Helen", 50);
        static Actor A2 = new Actor(new Guid(2, 2, 2, new byte[8]), "Mob", 25);

        static BattleActor[] PartyA = new BattleActor[MaxPartySize];
        static BattleActor[] PartyB = new BattleActor[MaxPartySize];

        static void Main(string[] args)
        {
            // Add Equipment.
            A1.Add(new Weapon(Guid.NewGuid(), "Sword", 15, 10, 12));
            A1.Add(new Weapon(Guid.NewGuid(), "Bow", 7, 2, 5));
            A1.Add(new Weapon(Guid.NewGuid(), "Heal", 25, 0, 20, WeaponProperties.TrueHealing));
            A1.Add(new Weapon(Guid.NewGuid(), "Thunder", 25, 0, 20, WeaponProperties.Piercing));
            A2.Add(new Weapon(Guid.NewGuid(), "Claw", 5, 5, 7));

            // Initialize BattleActors.
            PartyA[0] = A1.Battle();
            PartyB[0] = A2.Battle();

            // Battle loop.
            while (PartyA[0].IsAlive && PartyB[0].IsAlive)
            {
                // Setup BA2.
                if (PartyB[0].Wait == 0) PartyB[0].Select(A2.Weapons[0]);
                if (PartyB[0].CurrentTarget == null) PartyB[0].Select(PartyA[0]);

                Render(PartyA[0], PartyB[0]);
                
                // Setup BA1.
                if (PartyA[0].Wait == 0) PromptWeapon(PartyA[0]);
                else if (PartyA[0].CurrentTarget == null) PromptTarget(PartyA[0]);
                else Tick(PartyA[0], PartyB[0]);
                Task.Delay(500).Wait();
            }

            // Battle is over.
            Console.Clear();

            // Conclusion message.
            if (!PartyA[0].IsAlive && !PartyB[0].IsAlive) Console.WriteLine("Somehow it's a draw.");
            else if (!PartyA[0].IsAlive) Console.WriteLine("You lose, you suck.");
            else if (!PartyB[0].IsAlive) Console.WriteLine("You win, gg.");
        }

        static void Render(BattleActor BA1, BattleActor BA2)
        {
            Console.Clear();
            Console.WriteLine($"|".PadRight(PaddingLength, '-'));

            Console.WriteLine($"| {BA1.Name.PadRight(15, ' ')}{BA1.Health}/{BA1.HealthMax}");
            if (BA1.CurrentWeapon != null)
            Console.WriteLine($"| {BA1.CurrentWeapon.Name.PadRight(15, ' ')}Eff:{BA1.CurrentWeapon.Effectiveness} Def:{BA1.CurrentWeapon.Defense} - Wait:{BA1.Wait}");
            else // Pad out.
            Console.WriteLine($"|".PadRight(PaddingLength, ' '));

            Console.WriteLine($"|");

            Console.WriteLine($"| {BA2.Name.PadRight(15, ' ')}{BA2.Health}/{BA2.HealthMax}");
            if (BA2.CurrentWeapon != null)
            Console.WriteLine($"| {BA2.CurrentWeapon.Name.PadRight(15, ' ')}Eff:{BA2.CurrentWeapon.Effectiveness} Def:{BA2.CurrentWeapon.Defense} - Wait:{BA2.Wait}");
            else // Pad out.
            Console.WriteLine($"|".PadRight(PaddingLength, ' '));

            Console.WriteLine($"|".PadRight(PaddingLength, '-'));
        }

        static void PromptWeapon(BattleActor actor)
        {
            char response = ' ';
            Weapon weapon = null;

            Console.WriteLine($"| Select Weapon: ".PadRight(PaddingLength, '-'));

            // Display offering of weapons.
            for (int i = 0; i < Actor.MaxWeaponCount; ++i)
            {
                weapon = A1.Weapons[i];
                if (weapon != null)
                {
                    Console.Write($"| {i} - {weapon.Name.PadRight(11, ' ')}");
                    Console.Write($" Eff:{$"{weapon.Effectiveness}".PadRight(4, ' ')}");
                    Console.Write($" Def:{$"{weapon.Defense}".PadRight(4, ' ')}");
                    Console.Write($" Speed:{$"{weapon.Speed}".PadRight(4, ' ')}");
                    Console.Write($" {weapon.Properties.Name()}");
                    Console.WriteLine();
                }
            }

            do // Input loop.
            {
                response = Console.ReadKey().KeyChar;
                response = (response >= '0' || response <= '7') ? response : ' ';
                weapon = (int.TryParse(response.ToString(), out int index)) ? A1.Weapons[index] : null;
            } while (weapon == null);

            actor.Select(weapon);
        }

        static void PromptTarget(BattleActor actor)
        {
            char response = ' ';
            BattleActor target = null;

            Console.WriteLine($"| Select Target: ".PadRight(PaddingLength, '-'));

            // Display offering of Party A members. (Allies).
            for (int i = 0; i < MaxPartySize; ++i)
            {
                target = PartyA[i];
                if (target != null)
                {
                    Console.WriteLine($"| {i} - {target.Name.PadRight(PaddingLength, ' ')}");
                }
            }

            // Display offering of Party B members. (Enemies).
            for (int i = MaxPartySize; i < MaxPartySize * 2; ++i)
            {
                target = PartyB[i - MaxPartySize];
                if (target != null)
                {
                    Console.WriteLine($"| {i} - {target.Name.PadRight(PaddingLength, ' ')}");
                }
            }

            do // Input loop.
            {
                response = Console.ReadKey().KeyChar;
                response = (response >= '0' || response <= '7') ? response : ' ';
                target = !(int.TryParse(response.ToString(), out int index)) ? null
                       : (index >= 0 && index <= 3) ? PartyA[index] 
                       : (index >= 4 && index <= 7) ? PartyB[index - MaxPartySize]
                       : null;
            } while (target == null);

            actor.Select(target);
        }

        static void Tick(BattleActor BA1, BattleActor BA2)
        {
            BA1.Act();
            BA2.Act();
        }
    }
}

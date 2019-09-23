using System.Threading.Tasks;
using Helen.Core;
using System.IO;
using System;

namespace Helen.Term
{
    class Program
    {
        static int PaddingLength = 75;

        static Actor A1 = new Actor(new Guid(1, 1, 1, new byte[8]), "Helen", 50);
        static Actor A2 = new Actor(new Guid(2, 2, 2, new byte[8]), "Mob", 25);

        static void Main(string[] args)
        {
            A1.Add(new Weapon(Guid.NewGuid(), "Sword", 15, 10, 12));
            A1.Add(new Weapon(Guid.NewGuid(), "Bow", 7, 2, 5));
            A1.Add(new Weapon(Guid.NewGuid(), "Heal", 25, 0, 20));
            A1.Add(new Weapon(Guid.NewGuid(), "Thunder", 25, 0, 20, WeaponProperties.Piercing));
            A2.Add(new Weapon(Guid.NewGuid(), "Claw", 5, 5, 7));

            var BA1 = A1.Battle();
            var BA2 = A2.Battle();

            // Battle loop.
            while (BA1.IsAlive && BA2.IsAlive)
            {
                Console.Clear();
                if (BA2.Wait == 0) BA2.Select(A2.Weapons[0]);
                Render(BA1, BA2);
                if (BA1.Wait == 0) Prompt(BA1);
                else Tick(BA1, BA2);
                Task.Delay(500).Wait();
            }

            // Battle is over.
            Console.Clear();

            if (!BA1.IsAlive && !BA2.IsAlive) Console.WriteLine("Somehow it's a draw.");
            else if (!BA1.IsAlive) Console.WriteLine("You lose, you suck.");
            else if (!BA2.IsAlive) Console.WriteLine("You win, gg.");
        }

        static void Render(BattleActor BA1, BattleActor BA2)
        {
            Console.WriteLine($"|".PadRight(PaddingLength, '-'));
            Console.WriteLine($"| {BA1.Name.PadRight(15, ' ')}{BA1.Health}/{BA1.HealthMax}");
            if (BA1.CurrentWeapon != null)
            Console.WriteLine($"| {BA1.CurrentWeapon.Name.PadRight(15, ' ')}Eff:{BA1.CurrentWeapon.Effectiveness} Def:{BA1.CurrentWeapon.Defense} - Wait:{BA1.Wait}");
            Console.WriteLine($"|");
            Console.WriteLine($"| {BA2.Name.PadRight(15, ' ')}{BA2.Health}/{BA2.HealthMax}");
            if (BA2.CurrentWeapon != null)
            Console.WriteLine($"| {BA2.CurrentWeapon.Name.PadRight(15, ' ')}Eff:{BA2.CurrentWeapon.Effectiveness} Def:{BA2.CurrentWeapon.Defense} - Wait:{BA2.Wait}");
            Console.WriteLine($"|".PadRight(PaddingLength, '-'));
        }

        static void Prompt(BattleActor BA1)
        {
            char response = ' ';
            Weapon weapon = null;

            Console.WriteLine($"| Select Weapon: ".PadRight(PaddingLength, '-'));

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

            do
            {
                response = Console.ReadKey().KeyChar;
                response = (response >= '0' || response <= '7') ? response : ' ';
                weapon = (int.TryParse(response.ToString(), out int index)) ? A1.Weapons[index] : null;
            } while (weapon == null);

            BA1.Select(weapon);
        }

        static void Tick(BattleActor BA1, BattleActor BA2)
        {
            BA1.Wait--;
            if (BA1.Wait == 0)
            {
                BA2.Receive(BA1.CurrentWeapon);
            }
            BA2.Wait--;
            if (BA2.Wait == 0)
            {
                BA1.Receive(BA2.CurrentWeapon);
            }
        }
    }
}

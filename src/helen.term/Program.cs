using System.Threading.Tasks;
using Helen.Core;
using System.IO;
using System;

namespace Helen.Term
{
    class Program
    {
        static BattleActor BA1 = new BattleActor("Helen", 50, 50);
        static BattleActor BA2 = new BattleActor("Mob", 25, 25);

        static Weapon W1 = new Weapon(Guid.NewGuid(), "Sword", 10, 10, 10);
        static Weapon W2 = new Weapon(Guid.NewGuid(), "Claw", 5, 5, 5);
        static Weapon[] Weapons = new Weapon[] { W1, W2 };

        static void Main(string[] args)
        {
            while (BA1.IsAlive && BA2.IsAlive)
            {
                Console.Clear();
                if (BA2.Wait == 0) BA2.Select(W2);
                Render();
                if (BA1.Wait == 0) Prompt();
                else Tick();
                Task.Delay(500).Wait();
            }

            Console.Clear();

            if (!BA1.IsAlive && !BA2.IsAlive) Console.WriteLine("Somehow it's a draw.");
            else if (!BA1.IsAlive) Console.WriteLine("You lose, you suck.");
            else if (!BA2.IsAlive) Console.WriteLine("You win, gg.");
        }

        static void Render()
        {
            Console.WriteLine($"------------------------------------");
            Console.WriteLine($"- {BA1.Name.PadRight(10, ' ')}{BA1.Health}/{BA1.HealthMax}");
            if (BA1.CurrentWeapon != null)
            Console.WriteLine($"- {BA1.CurrentWeapon.Name.PadRight(10, ' ')}{BA1.CurrentWeapon.Power}/{BA1.CurrentWeapon.Defense} - Wait:{BA1.Wait}");
            Console.WriteLine($"-");
            Console.WriteLine($"- {BA2.Name.PadRight(10, ' ')}{BA2.Health}/{BA2.HealthMax}");
            if (BA2.CurrentWeapon != null)
            Console.WriteLine($"- {BA2.CurrentWeapon.Name.PadRight(10, ' ')}{BA2.CurrentWeapon.Power}/{BA2.CurrentWeapon.Defense} - Wait:{BA2.Wait}");
            Console.WriteLine($"------------------------------------");
        }

        static void Prompt()
        {
            char response = ' ';

            Console.WriteLine($"- Select Weapon:");
            Console.WriteLine($"- 0 - {W1.Name}");
            Console.WriteLine($"- 1 - {W2.Name}");

            do
            {
                response = Console.ReadKey().KeyChar;
                response = (response == '0' || response == '1') ? response : ' ';
            } while (response == ' ');

            BA1.Select(Weapons[int.Parse(response.ToString())]);
        }

        static void Tick()
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

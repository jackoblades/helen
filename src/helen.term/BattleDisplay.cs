using Helen.Core;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace Helen.Term
{
    public class BattleDisplay
    {
        #region Static Members

        public const int TickDelay = 500;

        #endregion

        #region Properties

        public Battle Battle { get; set; }

        public Actor[] PartyA => Battle.PartyA;
        public Actor[] PartyB => Battle.PartyB;

        public BattleActor[] BattlePartyA => Battle.BattlePartyA;
        public BattleActor[] BattlePartyB => Battle.BattlePartyB;

        #endregion

        #region Constructors

        public BattleDisplay()
        {
        }

        public BattleDisplay(Battle battle) : this()
        {
            Battle = battle;
        }

        #endregion

        #region Methods

        public void Commence()
        {
            // Battle loop.
            while (Battle.State() == BattleState.Ongoing)
            {
                // Setup BA2.
                if (BattlePartyB[0].Wait == 0)             BattlePartyB[0].Select(PartyB[0].Weapons[0]);
                if (BattlePartyB[0].CurrentTarget == null) BattlePartyB[0].Select(BattlePartyA[0]);

                Render(BattlePartyA[0], BattlePartyB[0]);

                // Setup BA1.
                if      (BattlePartyA[0].Wait == 0)             PromptWeapon(BattlePartyA[0]);
                else if (BattlePartyA[0].CurrentTarget == null) PromptTarget(BattlePartyA[0]);
                else                                            Tick(BattlePartyA[0], BattlePartyB[0]);
                Task.Delay(TickDelay).Wait();
            }

            Conclude();
        }

        private void Conclude()
        {
            Console.Clear();
            BattleState state = Battle.State();
            if      (state == BattleState.Draw)    Console.WriteLine("Somehow, it's a draw.");
            else if (state == BattleState.Defeat)  Console.WriteLine("You lose, you suck.");
            else if (state == BattleState.Victory) Console.WriteLine("You win, gg.");
        }

        private void Render(BattleActor BA1, BattleActor BA2)
        {
            Console.Clear();
            Terminal.WriteLine($"|", '-');

            Terminal.WriteLine($"| {BA1.Name.PadRight(15, ' ')}{BA1.Health}/{BA1.HealthMax}");
            if (BA1.CurrentWeapon != null)
                Terminal.WriteLine($"| {BA1.CurrentWeapon.Name.PadRight(15, ' ')}Eff:{BA1.CurrentWeapon.Effectiveness} Def:{BA1.CurrentWeapon.Defense} - Wait:{BA1.Wait} - {BA1.CurrentWeapon.PropertySummary}");
            else // Pad out.
                Terminal.WriteLine($"|");

            Terminal.WriteLine($"|");

            Terminal.WriteLine($"| {BA2.Name.PadRight(15, ' ')}{BA2.Health}/{BA2.HealthMax}");
            if (BA2.CurrentWeapon != null)
                Terminal.WriteLine($"| {BA2.CurrentWeapon.Name.PadRight(15, ' ')}Eff:{BA2.CurrentWeapon.Effectiveness} Def:{BA2.CurrentWeapon.Defense} - Wait:{BA2.Wait} - {BA2.CurrentWeapon.PropertySummary}");
            else // Pad out.
                Terminal.WriteLine($"|");

            Terminal.WriteLine($"|", '-');
        }

        private void PromptWeapon(BattleActor actor)
        {
            char response = ' ';
            Weapon weapon = null;

            Terminal.WriteLine($"| Select Weapon: ", '-');

            // Display offering of weapons.
            for (int i = 0; i < Actor.MaxWeaponCount; ++i)
            {
                weapon = PartyA[0].Weapons[i];
                if (weapon != null)
                {
                    Console.Write($"| {i} - {weapon.Name.PadRight(11, ' ')}");
                    Console.Write($" Eff:{$"{weapon.Effectiveness}".PadRight(4, ' ')}");
                    Console.Write($" Def:{$"{weapon.Defense}".PadRight(4, ' ')}");
                    Console.Write($" Speed:{$"{weapon.Speed}".PadRight(4, ' ')}");
                    Console.Write($" {weapon.PropertySummary}");
                    Terminal.WriteLine();
                }
            }

            do // Input loop.
            {
                var cursor = new Point(Console.CursorLeft, Console.CursorTop);
                response = Console.ReadKey().KeyChar;
                Console.SetCursorPosition(cursor.X, cursor.Y);

                response = (response >= '0' && response <= '7') ? response : ' ';
                weapon = (int.TryParse(response.ToString(), out int index)) ? PartyA[0].Weapons[index] : null;
            } while (weapon == null);

            actor.Select(weapon);
        }

        private void PromptTarget(BattleActor actor)
        {
            char response = ' ';
            BattleActor target = null;

            Terminal.WriteLine($"| Select Target: ", '-');

            // Display offering of Party A members. (Allies).
            for (int i = 0; i < Battle.MaxPartySize; ++i)
            {
                target = BattlePartyA[i];
                if (target != null)
                {
                    Terminal.WriteLine($"| {i} - {target.Name}");
                }
            }

            // Display offering of Party B members. (Enemies).
            for (int i = Battle.MaxPartySize; i < Battle.MaxPartySize * 2; ++i)
            {
                target = BattlePartyB[i - Battle.MaxPartySize];
                if (target != null)
                {
                    Terminal.WriteLine($"| {i} - {target.Name}");
                }
            }

            do // Input loop.
            {
                var cursor = new Point(Console.CursorLeft, Console.CursorTop);
                response = Console.ReadKey().KeyChar;
                Console.SetCursorPosition(cursor.X, cursor.Y);

                response = (response >= '0' || response <= '7') ? response : ' ';
                target = !(int.TryParse(response.ToString(), out int index)) ? null
                       : (index >= 0 && index <= 3) ? BattlePartyA[index] 
                       : (index >= 4 && index <= 7) ? BattlePartyB[index - Battle.MaxPartySize]
                       : null;
            } while (target == null);

            actor.Select(target);
        }

        private void Tick(BattleActor BA1, BattleActor BA2)
        {
            BA1.Act();
            BA2.Act();
        }

        #endregion
    }
}

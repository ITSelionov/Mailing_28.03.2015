using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace Mailing
{
    class Program
    {
        public static int Menu()
        {
            int nSelect = 0;

            string[] menu =
            {
                "Twenty-first Century",
                "Alternatives",
                "Vox",
                "Index",
                "YourHealth"
            };

            while (true)
            {
                Console.Clear();
                for (int i = 0; i < menu.Length; i++)
                {
                    if (i == nSelect)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write(" •► ");
                        Console.ResetColor();
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.Write(menu[i]);
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine(" ◄");
                        Console.ResetColor();
                    }
                    else
                        Console.WriteLine("   " + menu[i]);
                }

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.UpArrow:
                        if (nSelect == 0) nSelect = menu.Length - 1;
                        else nSelect--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (nSelect >= menu.Length - 1) nSelect = 0;
                        else nSelect++;
                        break;
                    case ConsoleKey.Enter:
                        return nSelect;
                }
            }
        }

        static void Main(string[] args)
        {
            Subscriber[] mas = {
                new Subscriber("Pavel"),
                new Subscriber("Oleg"),
                new Subscriber("Marina")
            };

            mas[0].Subscribe_twentyFirstCenturyHandler(new Mailing.TwentyFirstCentury(View));
            mas[0].Subscribe_indexHandler(new Mailing.Index(View));
            mas[0].Subscribe_yourHealthHandler(new Mailing.YourHealth(new Mailing.YourHealth(View)));

            mas[1].Subscribe_alternativesHandler(new Mailing.Alternatives(View));
            mas[1].Subscribe_twentyFirstCenturyHandler(new Mailing.TwentyFirstCentury(View));
            mas[1].Subscribe_voxHandler(new Mailing.Vox(View));

            mas[2].Subscribe_twentyFirstCenturyHandler(new Mailing.TwentyFirstCentury(View));
            mas[2].Subscribe_yourHealthHandler(new Mailing.YourHealth(new Mailing.YourHealth(View)));
            mas[2].Subscribe_voxHandler(new Mailing.Vox(View));

            int click = Menu();

            while (true)
            {
                for (int i = 5; i >= 1; i--)
                {
                    Console.Clear();

                    foreach (Subscriber t in mas)
                    {
                        t.Accelerate(t, click);
                    }

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nPlease wait " + i + "s...");
                    Console.ResetColor();

                    Thread.Sleep(999);
                }

                click = Menu();
            }
        }

        public static void View(string msg)
        {
            Console.WriteLine(msg);
        }
    }

    class Mailing
    {
        public delegate void TwentyFirstCentury(string msg);
        public delegate void Alternatives(string msg);
        public delegate void Vox(string msg);
        public delegate void Index(string msg);
        public delegate void YourHealth(string msg);

        private TwentyFirstCentury _twentyFirstCenturyHandler;
        private Alternatives _alternativesHandler;
        private Vox _voxHandler;
        private Index _indexHandler;
        private YourHealth _yourHealthHandler;

        public void Subscribe_twentyFirstCenturyHandler(TwentyFirstCentury method)
        {
            _twentyFirstCenturyHandler = method;
        }

        public void Subscribe_alternativesHandler(Alternatives method)
        {
            _alternativesHandler = method;
        }

        public void Subscribe_voxHandler(Vox method)
        {
            _voxHandler = method;
        }

        public void Subscribe_indexHandler(Index method)
        {
            _indexHandler = method;
        }

        public void Subscribe_yourHealthHandler(YourHealth method)
        {
            _yourHealthHandler = method;
        }

        public void Accelerate(Subscriber mas, int nClick)
        {
            if (nClick == 0)
                if (_twentyFirstCenturyHandler != null)
                    _twentyFirstCenturyHandler(mas.name + " subscribed to the magazine \"Twenty-first Century\"");

            if (nClick == 1)
                if (_alternativesHandler != null)
                    _alternativesHandler(mas.name + " subscribed to the magazine \"Alternatives\"");

            if (nClick == 2)
                if (_voxHandler != null)
                    _voxHandler(mas.name + " subscribed to the magazine \"Vox\"");

            if (nClick == 3)
                if (_indexHandler != null)
                    _indexHandler(mas.name + " subscribed to the magazine \"Index\"");

            if (nClick == 4)
                if (_yourHealthHandler != null)
                    _yourHealthHandler(mas.name + " subscribed to the magazine \"Your Healt\"");
        }
    }

    internal class Subscriber : Mailing
    {
        public string name;

        public Subscriber(string name)
        {
            this.name = name;
        }
    }
}

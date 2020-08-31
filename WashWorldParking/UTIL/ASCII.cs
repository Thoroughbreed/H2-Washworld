using System;
using System.Drawing;
using System.Text;
using System.Threading;

namespace WashWorldParking.UTIL
{
    public class ASCII
    {
        public ASCII()
        {
        }
        public static int HorisontalWash(int x, int y, int key)
        {
            if (key > 9) key = 1;
            if (key == 1)
            {
                Console.SetCursorPosition(x, y);
                Console.WriteLine("-. .-.   .-. .-.   .-. .-.   .");
                Console.WriteLine("||\\|||\\ /|||\\|||\\ /|||\\|||\\ /|");
                Console.WriteLine("|/ \\|||\\|||/ \\|||\\|||/ \\|||\\||");
                Console.WriteLine("~   `-~ `-`   `-~ `-`   `-~ `-");
            }
            if (key == 2)
            {
                Console.SetCursorPosition(x, y);
                Console.WriteLine(". .-.   .-. .-.   .-. .-.   .-");
                Console.WriteLine("|\\|||\\ /|||\\|||\\ /|||\\|||\\ /||");
                Console.WriteLine("/ \\|||\\|||/ \\|||\\|||/ \\|||\\|||");
                Console.WriteLine("   `-~ `-`   `-~ `-`   `-~ `-~");
            }
            if (key == 3)
            {
                Console.SetCursorPosition(x, y);
                Console.WriteLine(" .-.   .-. .-.   .-. .-.   .-.");
                Console.WriteLine("\\|||\\ /|||\\|||\\ /|||\\|||\\ /|||");
                Console.WriteLine(" \\|||\\|||/ \\|||\\|||/ \\|||\\|||/");
                Console.WriteLine("  `-~ `-`   `-~ `-`   `-~ `-~ ");
            }
            if (key == 4)
            {
                Console.SetCursorPosition(x, y);
                Console.WriteLine(".-.   .-. .-.   .-. .-.   .-. ");
                Console.WriteLine("|||\\ /|||\\|||\\ /|||\\|||\\ /|||\\");
                Console.WriteLine("\\|||\\|||/ \\|||\\|||/ \\|||\\|||/ ");
                Console.WriteLine(" `-~ `-`   `-~ `-`   `-~ `-~  ");
            }
            if (key == 4)
            {
                Console.SetCursorPosition(x, y);
                Console.WriteLine("-.   .-. .-.   .-. .-.   .-. .");
                Console.WriteLine("||\\ /|||\\|||\\ /|||\\|||\\ /|||\\|");
                Console.WriteLine("|||\\|||/ \\|||\\|||/ \\|||\\|||/ \\");
                Console.WriteLine("`-~ `-`   `-~ `-`   `-~ `-~   ");
            }
            if (key == 5)
            {
                Console.SetCursorPosition(x, y);
                Console.WriteLine(".   .-. .-.   .-. .-.   .-. .-");
                Console.WriteLine("|\\ /|||\\|||\\ /|||\\|||\\ /|||\\||");
                Console.WriteLine("||\\|||/ \\|||\\|||/ \\|||\\|||/ \\|");
                Console.WriteLine("-~ `-`   `-~ `-`   `-~ `-~   `");
            }
            if (key == 6)
            {
                Console.SetCursorPosition(x, y);
                Console.WriteLine("   .-. .-.   .-. .-.   .-. .-.");
                Console.WriteLine("\\ /|||\\|||\\ /|||\\|||\\ /|||\\|||");
                Console.WriteLine("|\\|||/ \\|||\\|||/ \\|||\\|||/ \\||");
                Console.WriteLine("~ `-`   `-~ `-`   `-~ `-~   `-");
            }
            if (key == 7)
            {
                Console.SetCursorPosition(x, y);
                Console.WriteLine("  .-. .-.   .-. .-.   .-. .-. ");
                Console.WriteLine(" /|||\\|||\\ /|||\\|||\\ /|||\\|||\\");
                Console.WriteLine("\\|||/ \\|||\\|||/ \\|||\\|||/ \\|||");
                Console.WriteLine(" `-`   `-~ `-`   `-~ `-~   `-~");
            }
            if (key == 8)
            {
                Console.SetCursorPosition(x, y);
                Console.WriteLine(" .-. .-.   .-. .-.   .-. .-.  ");
                Console.WriteLine("/|||\\|||\\ /|||\\|||\\ /|||\\|||\\ ");
                Console.WriteLine("|||/ \\|||\\|||/ \\|||\\|||/ \\|||\\");
                Console.WriteLine("`-`   `-~ `-`   `-~ `-~   `-~ ");
            }
            if (key == 9)
            {
                Console.SetCursorPosition(x, y);
                Console.WriteLine(".-. .-.   .-. .-.   .-. .-.   ");
                Console.WriteLine("|||\\|||\\ /|||\\|||\\ /|||\\|||\\ /");
                Console.WriteLine("||/ \\|||\\|||/ \\|||\\|||/ \\|||\\|");
                Console.WriteLine("-`   `-~ `-`   `-~ `-~   `-~ `");
            }
            key++;
            return key;
        }

        public static void VerticalWash(int x, int y)
        {
            int x1;
            int y1;
            while (1 == 1)
            {
                x1 = x; y1 = y;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("6098)o %:::% o(860                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("098)o %:::% o(8609                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine(" 6o %:% o(86098)                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("  (86098)o                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("6098)o %::% o9                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("098)o %::::::% o9                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine(" 6o %::::::% o(860                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("    6o %::% o(8609                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("       o(86098)                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("  (86098)o %:% o9                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("6098)o %:::% o(860                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("098)o %:::% o(8609                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine(" 6o %:% o(86098)                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("  (86098)o                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("6098)o %::% o9                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("098)o %::::::% o9                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine(" 6o %::::::% o(860                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("    6o %::% o(8609                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("       o(86098)                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("  (86098)o %:% o9                ");
                Thread.Sleep(150);

                x1 = x; y1 = y;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("098)o %:::% o(8609                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine(" 6o %:% o(86098)                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("  (86098)o                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("6098)o %::% o9                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("098)o %::::::% o9                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine(" 6o %::::::% o(860                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("    6o %::% o(8609                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("       o(86098)                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("  (86098)o %:% o9                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("6098)o %:::% o(860                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("098)o %:::% o(8609                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine(" 6o %:% o(86098)                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("  (86098)o                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("6098)o %::% o9                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("098)o %::::::% o9                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine(" 6o %::::::% o(860                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("    6o %::% o(8609                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("       o(86098)                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("  (86098)o %:% o9                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("6098)o %:::% o(860                ");
                Thread.Sleep(150);

                x1 = x; y1 = y;
                Console.SetCursorPosition(x, y);
                Console.WriteLine(" 6o %:% o(86098)                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("  (86098)o                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("6098)o %::% o9                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("098)o %::::::% o9                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine(" 6o %::::::% o(860                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("    6o %::% o(8609                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("       o(86098)                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("  (86098)o %:% o9                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("6098)o %:::% o(860                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("098)o %:::% o(8609                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine(" 6o %:% o(86098)                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("  (86098)o                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("6098)o %::% o9                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("098)o %::::::% o9                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine(" 6o %::::::% o(860                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("    6o %::% o(8609                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("       o(86098)                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("  (86098)o %:% o9                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("6098)o %:::% o(860                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("098)o %:::% o(8609                ");
                Thread.Sleep(150);

                x1 = x; y1 = y;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("  (86098)o                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("6098)o %::% o9                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("098)o %::::::% o9                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine(" 6o %::::::% o(860                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("    6o %::% o(8609                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("       o(86098)                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("  (86098)o %:% o9                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("6098)o %:::% o(860                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("098)o %:::% o(8609                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine(" 6o %:% o(86098)                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("  (86098)o                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("6098)o %::% o9                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("098)o %::::::% o9                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine(" 6o %::::::% o(860                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("    6o %::% o(8609                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("       o(86098)                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("  (86098)o %:% o9                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("6098)o %:::% o(860                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("098)o %:::% o(8609                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine(" 6o %:% o(86098)                ");
                Thread.Sleep(150);

                x1 = x; y1 = y;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("6098)o %::% o9                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("098)o %::::::% o9                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine(" 6o %::::::% o(860                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("    6o %::% o(8609                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("       o(86098)                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("  (86098)o %:% o9                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("6098)o %:::% o(860                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("098)o %:::% o(8609                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine(" 6o %:% o(86098)                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("  (86098)o                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("6098)o %::% o9                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("098)o %::::::% o9                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine(" 6o %::::::% o(860                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("    6o %::% o(8609                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("       o(86098)                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("  (86098)o %:% o9                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("6098)o %:::% o(860                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("098)o %:::% o(8609                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine(" 6o %:% o(86098)                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("  (86098)o                ");
                Thread.Sleep(150);

                x1 = x; y1 = y;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("098)o %::::::% o9                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine(" 6o %::::::% o(860                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("    6o %::% o(8609                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("       o(86098)                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("  (86098)o %:% o9                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("6098)o %:::% o(860                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("098)o %:::% o(8609                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine(" 6o %:% o(86098)                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("  (86098)o                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("6098)o %::% o9                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("098)o %::::::% o9                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine(" 6o %::::::% o(860                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("    6o %::% o(8609                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("       o(86098)                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("  (86098)o %:% o9                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("6098)o %:::% o(860                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("098)o %:::% o(8609                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine(" 6o %:% o(86098)                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("  (86098)o                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("6098)o %::% o9                ");
                Thread.Sleep(150);

                x1 = x; y1 = y;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine(" 6o %::::::% o(860                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("    6o %::% o(8609                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("       o(86098)                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("  (86098)o %:% o9                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("6098)o %:::% o(860                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("098)o %:::% o(8609                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine(" 6o %:% o(86098)                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("  (86098)o                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("6098)o %::% o9                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("098)o %::::::% o9                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine(" 6o %::::::% o(860                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("    6o %::% o(8609                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("       o(86098)                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("  (86098)o %:% o9                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("6098)o %:::% o(860                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("098)o %:::% o(8609                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine(" 6o %:% o(86098)                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("  (86098)o                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("6098)o %::% o9                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("098)o %::::::% o9                ");
                Thread.Sleep(150);

                x1 = x; y1 = y;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("    6o %::% o(8609                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("       o(86098)                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("  (86098)o %:% o9                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("6098)o %:::% o(860                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("098)o %:::% o(8609                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine(" 6o %:% o(86098)                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("  (86098)o                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("6098)o %::% o9                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("098)o %::::::% o9                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine(" 6o %::::::% o(860                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("    6o %::% o(8609                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("       o(86098)                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("  (86098)o %:% o9                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("6098)o %:::% o(860                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("098)o %:::% o(8609                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine(" 6o %:% o(86098)                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("  (86098)o                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("6098)o %::% o9                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("098)o %::::::% o9                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine(" 6o %::::::% o(860                ");
                Thread.Sleep(150);

                x1 = x; y1 = y;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("       o(86098)                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("  (86098)o %:% o9                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("6098)o %:::% o(860                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("098)o %:::% o(8609                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine(" 6o %:% o(86098)                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("  (86098)o                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("6098)o %::% o9                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("098)o %::::::% o9                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine(" 6o %::::::% o(860                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("    6o %::% o(8609                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("       o(86098)                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("  (86098)o %:% o9                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("6098)o %:::% o(860                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("098)o %:::% o(8609                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine(" 6o %:% o(86098)                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("  (86098)o                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("6098)o %::% o9                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("098)o %::::::% o9                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine(" 6o %::::::% o(860                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("    6o %::% o(8609                ");
                Thread.Sleep(150);

                x1 = x; y1 = y;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("  (86098)o %:% o9                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("6098)o %:::% o(860                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("098)o %:::% o(8609                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine(" 6o %:% o(86098)                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("  (86098)o                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("6098)o %::% o9                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("098)o %::::::% o9                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine(" 6o %::::::% o(860                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("    6o %::% o(8609                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("       o(86098)                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("  (86098)o %:% o9                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("6098)o %:::% o(860                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("098)o %:::% o(8609                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine(" 6o %:% o(86098)                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("  (86098)o                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("6098)o %::% o9                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("098)o %::::::% o9                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine(" 6o %::::::% o(860                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("    6o %::% o(8609                ");
                y1++;
                Console.SetCursorPosition(x1, y1);
                Console.WriteLine("       o(86098)                ");
                Thread.Sleep(150);
            }
        }

        public static void Spinner(int z, int y)
        {
            int x = 0;
            for (int i = 0; i < 1000; i++)
            {
                Console.SetCursorPosition(z, y);
                if (x == 0) { Console.Write("/"); x = 1; }
                else if (x == 1) { Console.Write("-"); x = 2; }
                else if (x == 2) { Console.Write("\\"); x = 3; }
                else if (x == 3) { Console.Write("|"); x = 0; }
                Thread.Sleep(150);
            }
        }

        public static void SpinnerBool(int z, int y, bool x)
        {
            int i = 0;
            while (x)
            {
                Console.SetCursorPosition(z, y);
                if (i == 0) { Console.Write("/"); i = 1; }
                else if (i == 1) { Console.Write("-"); i = 2; }
                else if (i == 2) { Console.Write("\\"); i = 3; }
                else if (i == 3) { Console.Write("|"); i = 0; }
                Thread.Sleep(150);
            }
        }

        public static bool AdminMenu()
        {
            Console.Clear();
            Console.WriteLine(@"8888888888888888888888888888888888888888888888888888888888888");
            Console.WriteLine(@"8888888888888888888888888888888888888888888888888888888888888");
            Console.WriteLine(@"8888888888888888888888888P""""  """"98888888888888888888888888888");
            Console.WriteLine(@"8888888888888888P""88888P          988888""98888888888888888888");
            Console.WriteLine(@"8888888888888888  ""9888            888P""  8888888888888888888");
            Console.WriteLine(@"888888888888888888bo ""9  d8o  o8b  P"" od888888888888888888888");
            Console.WriteLine(@"888888888888888888888bob 98""  ""8P dod888888888888888888888888");
            Console.WriteLine(@"888888888888888888888888    db    888888888888888888888888888");
            Console.WriteLine(@"88888888888888888888888888      88888888888888888888888888888");
            Console.WriteLine(@"88888888888888888888888P""9bo  odP""988888888888888888888888888");
            Console.WriteLine(@"88888888888888888888P"" od88888888bo ""988888888888888888888888");
            Console.WriteLine(@"888888888888888888   d88888888888888b   888888888888888888888");
            Console.WriteLine(@"8888888888888888888oo8888888888888888oo8888888888888888888888");
            Console.WriteLine(@"8888888888888888888888888888888888888888888888888888888888888");
            Console.WriteLine("╔═══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║             WELCOME TO THE SECRET ADMIN MODE!             ║");
            Console.WriteLine("╠═══════════════════════════════════════════════════════════╣");
            Console.WriteLine("║                      Please select:                       ║");
            Console.WriteLine("║     _.........._                         _.........._     ║");
            Console.WriteLine("║    | |  DOOM  | |    [I]nspect          | |  DOOM  | |    ║");
            Console.WriteLine("║    | | DISK 1 | |    [D]issect          | | DISK 2 | |    ║");
            Console.WriteLine("║    | |  OF 4  | |    [K]ill init        | |  OF 4  | |    ║");
            Console.WriteLine("║    | |________| |    [F]ind user        | |________| |    ║");
            Console.WriteLine("║    |   ______   |    [A]ll information  |   ______   |    ║");
            Console.WriteLine("║    |  |    | |  |                       |  |    | |  |    ║");
            Console.WriteLine("║    |__|____|_|__|    [X] Exit           |__|____|_|__|    ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════╝");
            return true;
        }

        /* Virker ikke...???
        public static void ConvertGif()
        {
            Image image = Image.FromFile(@"C:\some_animated_gif.gif");
            FrameDimension dimension = new FrameDimension(
                                image.FrameDimensionsList[0]);
            int frameCount = image.GetFrameCount(dimension);
            StringBuilder sb;

            // Remember cursor position
            int left = Console.WindowLeft, top = Console.WindowTop;

            char[] chars = { '#', '#', '@', '%', '=', '+',
                        '*', ':', '-', '.', ' ' };
            for (int i = 0; ; i = (i + 1) % frameCount)
            {
                sb = new StringBuilder();
                image.SelectActiveFrame(dimension, i);

                for (int h = 0; h < image.Height; h++)
                {
                    for (int w = 0; w < image.Width; w++)
                    {
                        Color cl = ((Bitmap)image).GetPixel(w, h);
                        int gray = (cl.R + cl.G + cl.B) / 3;
                        int index = (gray * (chars.Length - 1)) / 255;

                        sb.Append(chars[index]);
                    }
                    sb.Append('\n');
                }

                Console.SetCursorPosition(left, top);
                Console.Write(sb.ToString());

                Thread.Sleep(100);
            }
        }
        */
    }
}

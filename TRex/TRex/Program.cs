﻿using System;

namespace TRex
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new ChromeTRex())
                game.Run();
        }
    }
}

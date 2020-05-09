using System;

namespace Helen.App.Models
{
    [Flags]
    public enum Preferences
    {
        None  = 0x0000,
        Vsync = 0x0001,
    }
}

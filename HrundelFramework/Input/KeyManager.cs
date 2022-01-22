using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace HrundelFramework.Input
{
  public  enum Key:byte
    {
        F1=112,
        F2=113,
        F3=114,
        F4=115,
        F5=116,
        F6=117,
        F7=118,
        F8=119,
        F9=120,
        F10=121,
        F11=122,
        F12=123,
        Space=32,
        BackSpace=8,
        Tab=9,
        Enter=13,
        Shift=16,
        Ctrl=17,
        Alt=18,
        CapsLock=20,
        Esc=27,
        Insert=45,
        PageUp=33,
        PageDown=34,
        End=35,
        Home=36,
        LeftArrow=37,
        UpArrow= 38,
        RightArrow= 39,
        DownArrow= 40,
        Delete=46,
        PrintScreen=44,
        ScrollLock=145,
        Number0=48,
        Number1 = 49,
        Number2 = 50,
        Number3 = 51,
        Number4 = 52,
        Number5 = 53,
        Number6 = 54,
        Number7 = 55,
        Number8 = 56,
        Number9 = 57,
        Apostrophe=222,
        Minus=189,
        Equals=187,
        OpenBracket=219,
        CloseBracket=221,
        Semicolon=186,
        ReverseApostrophe = 126,
        BackSlash=220,
        Comma=188,
        Dot=190,
        Slash=191,
        A=65,
        B=66,
        C=67,
        D=68,
        E=69,
        F=70,
        G=71,
        H=72,
        I=73,
        J=74,
        K=75,
        L=76,
        M=77,
        N=78,
        O=79,
        P=80,
        Q=81,
        R=82,
        S=83,
        T=84,
        U=85,
        V=86,
        W=87,
        X=88,
        Y=89,
        Z=90,
        LWin=91,
        RWin=92,
        Numpad0=96,
        Numpad1 = 97,
        Numpad2 = 98,
        Numpad3 = 99,
        Numpad4 = 100,
        Numpad5 = 101,
        Numpad6 = 102,
        Numpad7 = 103,
        Numpad8 = 104,
        Numpad9 = 105,
        Multiply=106,
        Add=107,
        Subtract=108,
        Decimal=109,
        Divide=110,
      
    }
  public static class KeyManager
    {
        [DllImportAttribute("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern short GetAsyncKeyState(int keyCode);
        [DllImportAttribute("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
        [DllImportAttribute("user32.dll",SetLastError =true)]
        private static extern ushort GetKeyboardLayout([In] int idThread);
        [DllImportAttribute("user32.dll")]
        public static extern UInt32 GetWindowThreadProcessId(IntPtr hwnd, ref Int32 pid);
        [DllImportAttribute("user32.dll")]
        public static extern UInt32 GetWindowThreadProcessId([In] IntPtr hwnd, [Out,Optional] IntPtr ipdwProcessId);
        public static bool KeyPressed(Key key)
        {
            int v = GetAsyncKeyState((int)key);
            if (v != 0 && GameOpen())
                return true;
            return false;
        }
        private static bool GameOpen()
        {
            IntPtr h = GetForegroundWindow();
            int pid = 0;
            GetWindowThreadProcessId(h, ref pid);
            Process p = Process.GetProcessById(pid);
        
            return p.ProcessName == "ITBOX_GAME";
        }
    }
}

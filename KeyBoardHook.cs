using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace KeyboardStatistics
{
    public class KeyBoardHook
    {
        // 键盘结构
        [StructLayout(LayoutKind.Sequential)]
        public class KeyBoardStruct
        {
            public int kCode;
            public int scanCode;
            public int flag;
            public int time;
            public int dwExtraInfo;
        }

        // 挂钩
        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto)]
        public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);

        // 取消挂钩
        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto)]
        public static extern bool UnhookWindowsHookEx(int idHook);

        // 调用下一个钩子
        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto)]
        public static extern int CallNextHookEx(int idHook, int nCode, IntPtr wParam, IntPtr LParam);

        // 委托
        public delegate int HookProc(int nCode, IntPtr wParam, IntPtr LParam);

        private static int hHook = 0;
        private const int WH_KEYBOARD_LL = 13;
        private static HookProc KeyBoardHookProcedure;

        // 钩子处理函数
        public static int KeyBoardHookProc(int nCode, IntPtr wParam, IntPtr LParam)
        {
            if (nCode >= 0)
            {
                KeyBoardStruct kbs = (KeyBoardStruct)Marshal.PtrToStructure(LParam, typeof(KeyBoardStruct));

                switch (kbs.kCode)
                {
                    case 13:
                        if (wParam.ToInt32() == 0x100)
                        {
                            Console.WriteLine("<CR>");
                        }
                        break;
                }
            }

            return CallNextHookEx(hHook, nCode, wParam, LParam);
        }

        // 取消钩子事件
        public static bool Hook_Clear()
        {
            bool retKeyboard = true;
            if (hHook != 0)
            {
                retKeyboard = UnhookWindowsHookEx(hHook);
                hHook = 0;
            }
            return retKeyboard;
        }

        // 开始钩子
        public static bool Hook_Start()
        {
            bool retKeyboard = true;
            if (hHook == 0)
            {
                KeyBoardHookProcedure = new HookProc(KeyBoardHookProc);
                hHook = SetWindowsHookEx(WH_KEYBOARD_LL, KeyBoardHookProcedure, Process.GetCurrentProcess().MainModule.BaseAddress, 0);
                // 设置钩子失败
                if (hHook == 0)
                {
                    retKeyboard = false;
                    Hook_Clear();
                }
            }

            return retKeyboard;
        }
    }
}

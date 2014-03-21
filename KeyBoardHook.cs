using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

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
                            // Do Somethings.
                        }
                        break;
                }
            }

            return CallNextHookEx(hHook, nCode, wParam, LParam);
        }
    }
}

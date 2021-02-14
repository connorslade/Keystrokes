using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Keystrokes
{
    /// <summary>
    ///     A class that manages a global low level keyboard hook
    /// </summary>
    internal class GlobalKeyboardHook
    {
        #region Constant, Structure and Delegate Definitions

        /// <summary>
        ///     defines the callback type for the hook
        /// </summary>
        private delegate int KeyboardHookProc(int code, int wParam, ref KeyboardHookStruct lParam);

        private readonly struct KeyboardHookStruct
        {
            public readonly int VkCode;

            public KeyboardHookStruct(int vkCode)
            {
                VkCode = vkCode;
            }
        }

        private const int WhKeyboardLl = 13;
        private const int WmKeydown = 0x100;
        private const int WmKeyup = 0x101;
        private const int WmSysKeyDown = 0x104;
        private const int WmSysKeyUp = 0x105;

        #endregion

        #region Instance Variables

        /// <summary>
        ///     The collections of keys to watch for
        /// </summary>
        public readonly List<Keys> HookedKeys = new List<Keys>();

        /// <summary>
        ///     Handle to the hook, need this to unhook and call the next hook
        /// </summary>
        private IntPtr HHook = IntPtr.Zero;

        #endregion

        #region Events

        /// <summary>
        ///     Occurs when one of the hooked keys is pressed
        /// </summary>
        public event KeyEventHandler KeyDown;

        /// <summary>
        ///     Occurs when one of the hooked keys is released
        /// </summary>
        public event KeyEventHandler KeyUp;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="GlobalKeyboardHook" /> class and installs the keyboard hook.
        /// </summary>
        public GlobalKeyboardHook()
        {
            Hook();
        }

        /// <summary>
        ///     Releases unmanaged resources and performs other cleanup operations before the
        ///     <see cref="GlobalKeyboardHook" /> is reclaimed by garbage collection and uninstalls the keyboard hook.
        /// </summary>
        ~GlobalKeyboardHook()
        {
            Unhook();
        }

        #endregion

        #region Public Methods

        private static KeyboardHookProc _callbackDelegate;

        private void Hook()
        {
            if (_callbackDelegate != null) throw new InvalidOperationException("Can't hook more than once");
            var HInstance = LoadLibrary("User32");
            _callbackDelegate = HookProc;
            HHook = SetWindowsHookEx(WhKeyboardLl, _callbackDelegate, HInstance, 0);
            if (HHook == IntPtr.Zero) throw new Win32Exception();
        }

        private void Unhook()
        {
            if (_callbackDelegate == null) return;
            var Ok = UnhookWindowsHookEx(HHook);
            if (!Ok) throw new Win32Exception();
            _callbackDelegate = null;
        }

        /// <summary>
        ///     The callback for the keyboard hook
        /// </summary>
        /// <param name="code">The hook code, if it isn't >= 0, the function shouldn't do anything</param>
        /// <param name="wParam">The event type</param>
        /// <param name="lParam">The key hook event information</param>
        /// <returns></returns>
        private int HookProc(int code, int wParam, ref KeyboardHookStruct lParam)
        {
            if (code < 0)
                return CallNextHookEx(HHook, code, wParam, ref lParam);
            var Key = (Keys) lParam.VkCode;
            if (!HookedKeys.Contains(Key))
                return CallNextHookEx(HHook, code, wParam, ref lParam);
            var Kea = new KeyEventArgs(Key);
            if ((wParam == WmKeydown || wParam == WmSysKeyDown) && KeyDown != null)
                KeyDown(this, Kea);
            else if (wParam == WmKeyup || wParam == WmSysKeyUp)
                KeyUp?.Invoke(this, Kea);
            return Kea.Handled ? 1 : CallNextHookEx(HHook, code, wParam, ref lParam);
        }

        #endregion

        #region DLL imports

        /// <summary>
        ///     Sets the windows hook, do the desired event, one of hInstance or threadId must be non-null
        /// </summary>
        /// <param name="idHook">The id of the event you want to hook</param>
        /// <param name="callback">The callback.</param>
        /// <param name="hInstance">The handle you want to attach the event to, can be null</param>
        /// <param name="threadId">The thread you want to attach the event to, can be null</param>
        /// <returns>a handle to the desired hook</returns>
        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowsHookEx(int idHook, KeyboardHookProc callback, IntPtr hInstance, uint threadId);

        /// <summary>
        ///     Unhooks the windows hook.
        /// </summary>
        /// <param name="hInstance">The hook handle that was returned from SetWindowsHookEx</param>
        /// <returns>True if successful, false otherwise</returns>
        [DllImport("user32.dll")]
        private static extern bool UnhookWindowsHookEx(IntPtr hInstance);

        /// <summary>
        ///     Calls the next hook.
        /// </summary>
        /// <param name="idHook">The hook id</param>
        /// <param name="nCode">The hook code</param>
        /// <param name="wParam">The wParam.</param>
        /// <param name="lParam">The lParam.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        private static extern int CallNextHookEx(IntPtr idHook, int nCode, int wParam, ref KeyboardHookStruct lParam);

        /// <summary>
        ///     Loads the library.
        /// </summary>
        /// <param name="lpFileName">Name of the library</param>
        /// <returns>A handle to the library</returns>
        [DllImport("kernel32.dll")]
        private static extern IntPtr LoadLibrary(string lpFileName);

        #endregion
    }
}
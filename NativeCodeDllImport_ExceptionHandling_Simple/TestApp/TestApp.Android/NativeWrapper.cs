using System;
using System.Runtime.InteropServices;
namespace TestApp.Droid
{
    class NativeWrapper
    {
        [DllImport("libSharedObject.so")]
        public static extern int ReturnInt();

        [DllImport("libSharedObject.so", CharSet = CharSet.Unicode, EntryPoint = "ThrowAnException")]
        public static extern void ThrowAnException();
    }
}
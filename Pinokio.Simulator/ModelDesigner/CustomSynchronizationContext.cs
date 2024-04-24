using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;
using System.Windows.Forms;

/// <summary>
/// This class helps to avoid some synchornization issues between WinForms UI and Eyeshot parallel execution. More details here: https://stackoverflow.com/questions/35535580/why-does-parallel-for-execute-the-winforms-message-pump-and-how-to-prevent-it
/// The problem (huge delay) was reproducible switching between two tabs of the EyeshotDemo that build a BRep (for example Bracket and Flange) and moving the mouse cursor very fast on the control surface just after tab switch.
/// A similar problem (flickering) was present in the MarchingCubes sample.
/// </summary>
public class CustomSynchronizationContext : SynchronizationContext
{
    public static void Install()
    {
        var currentContext = Current;
        if (currentContext is CustomSynchronizationContext) return;
        WindowsFormsSynchronizationContext.AutoInstall = false;
        SetSynchronizationContext(new CustomSynchronizationContext(currentContext));
    }

    public static void Uninstall()
    {
        var currentContext = Current as CustomSynchronizationContext;
        if (currentContext == null) return;
        SetSynchronizationContext(currentContext.baseContext);
    }

    private WindowsFormsSynchronizationContext baseContext;

    private CustomSynchronizationContext(SynchronizationContext currentContext)
    {
        baseContext = currentContext as WindowsFormsSynchronizationContext  ?? new WindowsFormsSynchronizationContext();
        SetWaitNotificationRequired();
    }

    public override SynchronizationContext CreateCopy() { return this; }
    public override void Post(SendOrPostCallback d, object state) { baseContext.Post(d, state); }
    public override void Send(SendOrPostCallback d, object state) { baseContext.Send(d, state); }
    public override void OperationStarted() { baseContext.OperationStarted(); }
    public override void OperationCompleted() { baseContext.OperationCompleted(); }

    public override int Wait(IntPtr[] waitHandles, bool waitAll, int millisecondsTimeout)
    {
        int result = WaitForMultipleObjectsEx(waitHandles.Length, waitHandles, waitAll, millisecondsTimeout, false);
        if (result == -1) throw new Win32Exception();
        return result;
    }

    [SuppressUnmanagedCodeSecurity]
    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern int WaitForMultipleObjectsEx(int nCount, IntPtr[] pHandles, bool bWaitAll, int dwMilliseconds, bool bAlertable);
}
using System.Runtime.InteropServices;
using UnityEngine;

public class WebGLBridge : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void RequestGyroPermission();

    public void CallGyroPermission()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
            RequestGyroPermission();
#endif
    }
}
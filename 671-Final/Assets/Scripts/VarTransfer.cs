using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VarTransfer{

    private static bool kmode = false;
    private static bool paused = false;
    private static int sVol = 100;
    public static bool Kmode { get { return kmode; } set { kmode = value; } }
    public static bool Paused { get { return paused; } set { paused = value; } }
    public static int SVol { get { return sVol; } set { sVol = value; } }
}

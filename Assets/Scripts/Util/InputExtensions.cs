using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputExt
{
    public static KeyCode GetModifier()
    {
        if (Input.GetKey(KeyCode.LeftShift)) return KeyCode.LeftShift;
        if (Input.GetKey(KeyCode.LeftControl)) return KeyCode.LeftControl;
        if (Input.GetKey(KeyCode.LeftCommand)) return KeyCode.LeftCommand;

        return KeyCode.None;
    }

    public static string ToPrettyString(KeyCode key, KeyCode modifier = KeyCode.None)
    {
        string str = "";
        if (modifier != KeyCode.None)
        {
            str = modifier.ToString()
                .Replace("Left", "").Replace("Right", "")
                .Replace("Shift", "S").Replace("Control", "C").Replace("Command", "M");
        }
        str += key.ToString().Replace("Alpha", "");

        return str;
    }
}

using System;
using UnityEditor;

// HKCU\Software\Unity Technologies\UnityEditor N.x

internal abstract class EditorPrefVar<T>
{
    protected string variable;

    protected EditorPrefVar(string variable)
    {
        this.variable = variable;
    }

    internal abstract T Get();
    internal abstract void Set(T value);
}

internal class EditorPrefVarBool : EditorPrefVar<bool>
{
    internal EditorPrefVarBool(string variable) : base(variable) { }

    internal override bool Get() => EditorPrefs.GetBool(variable);
    internal override void Set(bool value) => EditorPrefs.SetBool(variable, value);
}

internal class EditorPrefVarInt : EditorPrefVar<int>
{
    internal EditorPrefVarInt(string variable) : base(variable) { }

    internal override int Get() => EditorPrefs.GetInt(variable);
    internal override void Set(int value) => EditorPrefs.SetInt(variable, value);
}
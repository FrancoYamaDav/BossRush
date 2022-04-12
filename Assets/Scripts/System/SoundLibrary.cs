using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundLibrary 
{
    private static Dictionary<SoundName, string> _sounds;

    public SoundLibrary()
    {
        MakeList();
    }

    public enum SoundName
    {

    }

    void SoundPath()
    {

    }

    void MakeList()
    {
        _sounds = new Dictionary<SoundName, string>();

    }
}

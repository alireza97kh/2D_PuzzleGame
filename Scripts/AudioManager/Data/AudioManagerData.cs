using Dobeil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioManagerData", menuName = "Puzzles/AudioManagerData")]
public class AudioManagerData : ScriptableObject
{
    public List<AudioClipDataClass> audioClips = new List<AudioClipDataClass>();
}

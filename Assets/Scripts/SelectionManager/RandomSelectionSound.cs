using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSelectionSound : MonoBehaviour
{
    [SerializeField] private List<AudioClip> selectAudioClips;
    [SerializeField] private List<AudioClip> unselectAudioClips;
    [SerializeField] private List<AudioClip> teleportAudioClips;

    public AudioClip RandomObjectSelectionAudioClip()
    {
        int random = Random.Range(0, selectAudioClips.Count);
        return selectAudioClips.ToArray()[random];
    }
    public AudioClip RandomObjectUnselectionAudioClip()
    {
        int random = Random.Range(0, unselectAudioClips.Count);
        return unselectAudioClips.ToArray()[random];
    }

    public AudioClip RandomTeleportationAudioClip()
    {
        int random = Random.Range(0, teleportAudioClips.Count);
        return teleportAudioClips.ToArray()[random];
    }
}

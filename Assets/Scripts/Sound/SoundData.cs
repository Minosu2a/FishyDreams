using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundData", menuName = "Database/Engine/SoundData")]
public class SoundData : ScriptableObject
{

    [SerializeField] private string _key = string.Empty;

    [SerializeField] private AudioClip _clip = null;

    [Range(0.0f, 1.0f)]
    [SerializeField] private float _volume = 1;

    [Range(-3.0f, 3.0f)]
    [SerializeField] private float _pitch = 1;

    [SerializeField] private bool _loop = false;

    [SerializeField] private bool _subtitleOn = false;

    [SerializeField] private float _subtitleTime = 2;

    [SerializeField] private string _subtitleText = string.Empty;


    public string Key => _key;

    public AudioClip Clip => _clip;

    public float Volume  => _volume;

    public float Pitch => _pitch;

    public bool Loop => _loop;

    public bool SubtitleOn => _subtitleOn;
    public float SubtitleTime => _subtitleTime;
    public string SubtitleText => _subtitleText;



}

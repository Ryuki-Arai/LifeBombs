using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
	//　SoundOptionキャンバスを設定
	[SerializeField]
	private GameObject soundOptionCanvas;
	//　GameSoundShot
	[SerializeField]
	private AudioMixerSnapshot gameSoundShot;
	//　OptionSoundShot
	[SerializeField]
	private AudioMixerSnapshot optionSoundShot;

	[SerializeField]
	private AudioMixer audioMixer;

    public void SetMaster(float volume)
	{
		audioMixer.SetFloat("MasterVol", volume);
	}

	public void SetBGM(float volume)
	{
		audioMixer.SetFloat("BGMVol", volume);
	}

	public void SetSE(float volume)
	{
		audioMixer.SetFloat("SEVol", volume);
	}
}

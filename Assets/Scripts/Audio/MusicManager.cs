using FMODUnity;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public FMODUnity.EventReference musicEventPath = new FMODUnity.EventReference();  // Use EventReference directly

    private FMOD.Studio.EventInstance musicEventInstance;

    private void Start()
    {
        RuntimeManager.PlayOneShot("event:/GameplayMusic");
    }
}

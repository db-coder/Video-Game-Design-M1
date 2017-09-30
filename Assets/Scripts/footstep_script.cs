using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class footstep_script : MonoBehaviour
{

    private Animator anim;
    private AudioSource mySound;
    public static GameObject floor;

    private float currentFrameFootstepLeft;
    private float currentFrameFootstepRight;
    private float lastFrameFootstepLeft;
    private float lastFrameFootstepRight;

    [Space(5.0f)]
    private float currentVolume;
    [Range(0.0f, 1.0f)]
    public float volume = 1.0f;
    [Space(5.0f)]
    public GameObject leftFoot ;         //Drag your player's RIG/MESH/BIP/BONE for the left foot here, in the inspector.
    public GameObject rightFoot;        //Drag your player's RIG/MESH/BIP/BONE for the right foot here, in the inspector.
    [Space(5.0f)]
    public AudioClip defaults = new AudioClip();

    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>();
        mySound = gameObject.GetComponent<AudioSource>();
        //leftFoot = anim.transform.Find("mixamorig:Hips/mixamorig:LeftUpLeg/mixamorig:LeftLeg/mixamorig:LeftFoot");
    }

	// Update is called once per frame
	void Update ()
    {
        currentFrameFootstepLeft = anim.GetFloat("FootstepLeft");
        if(currentFrameFootstepLeft > 0 && lastFrameFootstepLeft < 0)
        {
            RaycastHit surfaceHitLeft;
            Ray aboveLeftFoot = new Ray(leftFoot.transform.position + new Vector3(0, 1.5f, 0), Vector3.down);
            if(Physics.Raycast(aboveLeftFoot, out surfaceHitLeft, 2f))
            {
                floor = surfaceHitLeft.transform.gameObject;
                if (floor != null)
                    Invoke("PlaySound", 0);
            }
        }
        lastFrameFootstepLeft = anim.GetFloat("FootstepLeft");

        currentFrameFootstepRight = anim.GetFloat("FootstepRight");
        if (currentFrameFootstepRight > 0 && lastFrameFootstepRight < 0)
        {
            RaycastHit surfaceHitRight;
            Ray aboveRightFoot = new Ray(rightFoot.transform.position + new Vector3(0, 1.5f, 0), Vector3.down);
            if (Physics.Raycast(aboveRightFoot, out surfaceHitRight, 2f))
            {
                floor = surfaceHitRight.transform.gameObject;
                if (floor != null)
                    Invoke("PlaySound", 0);
            }
        }
        lastFrameFootstepRight = anim.GetFloat("FootstepRight");
    }

    void PlaySound()
    {
        currentVolume = Random.Range(0.3f, 1.0f);
        mySound.pitch = 0.1f;
        mySound.PlayOneShot(defaults, currentVolume);
    }
}

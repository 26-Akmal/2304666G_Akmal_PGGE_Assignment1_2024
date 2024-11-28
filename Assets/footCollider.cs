using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footCollider : MonoBehaviour
{
    public PlayerMovement playerScript; // Reference to the Player script
    

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Concrete"))
        {
            playerScript.PlayFootstepSoundConcrete();
        }
        if (collision.gameObject.CompareTag("Dirt"))
        {
            playerScript.PlayFootstepSoundDirt();
        }
        if (collision.gameObject.CompareTag("Metal"))
        {
            playerScript.PlayFootstepSoundMetal();
        }
        if (collision.gameObject.CompareTag("Sand"))
        {
            playerScript.PlayFootstepSoundSand();
        }
        if (collision.gameObject.CompareTag("Wood"))
        {
            playerScript.PlayFootstepSoundWood();
        }
    }
}

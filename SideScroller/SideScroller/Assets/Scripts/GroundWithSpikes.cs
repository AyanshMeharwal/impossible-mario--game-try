using System.Collections;
using UnityEngine;

/*
 *  Baasic behaviour of gruond with added skipes.
 */

public class GroundWithSpikes : Ground
{
    public Transform spikeYPosition;

    public Transform spikeResetPosition;

    public Spikes[]  spikes;

    void OnTriggerEnter2D(Collider2D collider)
    {
        for(int i = 0; i < spikes.Length; i++)
        {
            spikes[i].EnableSpikes(spikeYPosition.position.y);
        }
    } 

    public void ResetSpikes()
    {

        StartCoroutine(ResetWithDelay());
       
    }

    IEnumerator ResetWithDelay()
    {
        yield return new WaitForSeconds(1);
        for (int i = 0; i < spikes.Length; i++)
        {
            spikes[i].EnableSpikes(spikeResetPosition.position.y);
        }
    }
}

        
    
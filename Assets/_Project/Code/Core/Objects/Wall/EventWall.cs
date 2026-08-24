using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class EventWall : MonoBehaviour, IEvent, IPauseFeatures
{
    [SerializeField] private float speedOpen = 3f;

    private Coroutine coroutine;
    private bool featureIsPaused;
    private Vector3 openPosition;
    public void FeatureIsPaused(bool value)
    {
        featureIsPaused = value;
    }
    public void OnTriggerEvent()
    {
        if (coroutine != null) StopCoroutine(coroutine);
        coroutine = StartCoroutine(CoroutineMoveWall());
    }

    private void Awake()
    {
        Collider collider = this.GetComponent<Collider>();
        openPosition = transform.position + transform.up * collider.bounds.size.y;
    }

    private IEnumerator CoroutineMoveWall()
    {
        // moveTowards make the object do not go out of openPosition
        while (Vector3.Distance(this.transform.position, openPosition) > 0.01f)
        {
            if (featureIsPaused)
            {
                yield return null;
                continue;
            }
            transform.position = Vector3.MoveTowards( transform.position, openPosition, speedOpen * Time.deltaTime);
            // wait for next frame
            yield return null;
        }

        transform.position = openPosition;
    }


}

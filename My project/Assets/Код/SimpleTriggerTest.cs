using UnityEngine;

public class SimpleTriggerTest : MonoBehaviour
{
    void Start()
    {
        Debug.Log("✅ SimpleTriggerTest script is running!");

        // Auto-add collider if missing
        if (GetComponent<Collider>() == null)
        {
            BoxCollider col = gameObject.AddComponent<BoxCollider>();
            col.isTrigger = true;
            Debug.Log("Added BoxCollider to floor");
        }

        // Check if collider is trigger
        Collider myCollider = GetComponent<Collider>();
        if (myCollider != null)
        {
            Debug.Log($"Collider found: {myCollider.GetType().Name}, IsTrigger: {myCollider.isTrigger}");
        }
        else
        {
            Debug.LogError("No collider found!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"🔥🔥🔥 ONTRIGGERENTER FIRED! Object: {other.name} 🔥🔥🔥");

        // Change floor color to prove it worked
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = Color.red;
            Debug.Log("Floor turned RED!");
        }
    }

    void OnTriggerStay(Collider other)
    {
        Debug.Log($"Still touching: {other.name}");
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"💥 OnCollisionEnter (not trigger) with: {collision.gameObject.name}");
    }
}
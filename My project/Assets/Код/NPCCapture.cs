using UnityEngine;

public class NPCCapture : MonoBehaviour
{
    [SerializeField] private KeyCode captureKey = KeyCode.E;

    private bool isPlayerNearby = false;
    private bool isCaptured = false;

    void Start()
    {
        // Add Rigidbody if missing (important for triggers)
        if (GetComponent<Rigidbody>() == null)
        {
            Rigidbody rb = gameObject.AddComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.useGravity = false;
            Debug.Log("✅ Added Rigidbody to NPC");
        }

        // Ensure collider is trigger
        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.isTrigger = true;
            Debug.Log($"✅ Collider set as trigger. Size: {col.bounds.size}");
        }
        else
        {
            Debug.LogError("❌ NPC has no Collider! Add a Box Collider.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"🎯 TRIGGER ENTERED by: {other.gameObject.name} (Tag: {other.tag})");

        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            Debug.Log("✅ isPlayerNearby = TRUE - Press E to capture!");
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Continuously show that player is inside
            Debug.Log("👤 Player is inside trigger zone");
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log($"🚪 TRIGGER EXITED by: {other.gameObject.name}");

        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            Debug.Log("❌ isPlayerNearby = FALSE");
        }
    }

    void Update()
    {
        if (isCaptured) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log($"🔑 E pressed - isPlayerNearby = {isPlayerNearby}");

            if (isPlayerNearby)
            {
                Debug.Log("🎉 CAPTURE!");
                isCaptured = true;

                if (RiddleCounter.Instance != null)
                {
                    RiddleCounter.Instance.SolveRiddle();
                }

                Destroy(gameObject);
            }
        }
    }
}
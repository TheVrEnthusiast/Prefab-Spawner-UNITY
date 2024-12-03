using UnityEngine;
using UnityEditor;

[ExecuteInEditMode] 
public class PrefabSpawner : MonoBehaviour
{
    public GameObject prefabToSpawn; 
    private GameObject previewObject;
    private bool previewUpdated = false; 

    [Header("Scaling Settings")]
    public bool applyScaleOnSpawn = false; // Toggle whether to scale the spawned prefab
    public float scaleFactor = 1f; // Scale factor to apply when spawning the prefab

   
    void Start()
    {
        if (prefabToSpawn != null)
        {
           
            GameObject spawnedPrefab = Instantiate(prefabToSpawn, transform.position, transform.rotation);

         
            if (applyScaleOnSpawn)
            {
                spawnedPrefab.transform.localScale *= scaleFactor;
            }

        
            Destroy(gameObject);
        }
    }

    // Function that removes non-visual components like scripts, colliders, and rigidbodies from the preview
    private void RemoveNonVisualComponents(GameObject preview)
    {
        // Remove all MonoBehaviour components (scripts)
        MonoBehaviour[] behaviours = preview.GetComponentsInChildren<MonoBehaviour>();
        foreach (var behaviour in behaviours)
        {
            if (behaviour.GetType() != typeof(PrefabSpawner)) 
            {
                DestroyImmediate(behaviour); // Remove script components immediately
            }
        }

        // Remove all Collider components (BoxCollider, SphereCollider, etc.)
        Collider[] colliders = preview.GetComponentsInChildren<Collider>();
        foreach (var collider in colliders)
        {
            DestroyImmediate(collider); // Remove colliders
        }

        // Remove any Rigidbody components if they exist
        Rigidbody[] rigidbodies = preview.GetComponentsInChildren<Rigidbody>();
        foreach (var rb in rigidbodies)
        {
            DestroyImmediate(rb); // Remove rigidbody 
        }
    }

    // This function updates the prefab preview in the editor
    private void UpdatePrefabPreview()
    {
        if (prefabToSpawn != null && !Application.isPlaying)
        {
            // Ensure only one preview exists
            if (previewObject == null)
            {
                // Instantiate a preview object in the editor (not at runtime)
                previewObject = Instantiate(prefabToSpawn, transform.position, transform.rotation, transform);
                // Remove non-visual components like scripts and colliders from the preview
                RemoveNonVisualComponents(previewObject);
            }

        
            previewObject.transform.position = transform.position;
            previewObject.transform.rotation = transform.rotation;

            previewUpdated = true;
        }
        else
        {
          
            if (previewObject != null)
            {
                DestroyImmediate(previewObject);
                previewUpdated = false;
            }
        }
    }


    void Update()
    {
       
        if (!Application.isPlaying)
        {
            UpdatePrefabPreview();
        }
    }

 
    private void OnDestroy()
    {
        if (previewObject != null)
        {
            DestroyImmediate(previewObject);
        }
    }

  
    [CustomEditor(typeof(PrefabSpawner))]
    public class PrefabSpawnerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            PrefabSpawner spawner = (PrefabSpawner)target;

            if (!Application.isPlaying && spawner.prefabToSpawn != null)
            {
                // Show the preview of the prefab in the Inspector
                EditorGUILayout.LabelField("Prefab Preview", EditorStyles.boldLabel);
                Texture2D previewTexture = AssetPreview.GetAssetPreview(spawner.prefabToSpawn);
                if (previewTexture != null)
                {
                    GUILayout.Label(previewTexture);
                }
            }

        
       
        }
    }
}

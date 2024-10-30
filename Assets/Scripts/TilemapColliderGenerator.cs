using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapColliderGenerator : MonoBehaviour
{
    [SerializeField] private Tilemap _world;
    [SerializeField] private GameObject _colliderPrefab;

    void Start()
    {
        GenerateBoxColliders();
    }

    private void GenerateBoxColliders()
    {
        BoundsInt bounds = _world.cellBounds;

        // Loop through all positions within the bounds
        foreach (var position in bounds.allPositionsWithin)
        {
            // Fetch the tile at the current position
            TileBase tile = _world.GetTile(position);

            // Verify the tile's validity in a more robust way
            if (tile != null && !IsTileEmpty(tile))
            {
                // Get the world position of the cell center
                Vector3 worldPosition = _world.GetCellCenterWorld(position);

                // Debugging: Log the tile position and collider placement
                Debug.Log($"Placing collider at {worldPosition} for tile at {position}");

                // Instantiate the collider prefab at the cell center position
                GameObject colliderInstance = Instantiate(_colliderPrefab, worldPosition, Quaternion.identity);
                colliderInstance.transform.parent = transform;
            }
            else
            {
                // Debugging: Log positions where no valid tile is found
                Debug.Log($"No valid tile found at position {position}");
            }
        }
    }

    // Example method to check if a tile is considered empty or invalid
    private bool IsTileEmpty(TileBase tile)
    {
        // Custom logic to determine if a tile should be considered empty
        // For example, checking tile type or other properties
        // This is just an example; adjust it based on your tile requirements
        
        return tile.GetType() == typeof(Tile); // Replace EmptyTileType with your specific empty tile class
    }
}

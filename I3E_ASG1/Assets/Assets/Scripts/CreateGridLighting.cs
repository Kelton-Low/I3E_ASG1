using UnityEngine;

public class CreateGridLighting : MonoBehaviour
{
    [SerializeField] private int resolution = 10;
    [SerializeField] private float lightIntensity = 1f;
    [SerializeField] private Color lightColor = Color.white;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnGrid();
    }
    void SpawnGrid()
    {
        Vector3 size = GetComponent<Collider>().bounds.size;
        Vector3 cornerCoordinate = new Vector3(
            transform.position.x-(size.x/2)+(size.x/resolution/2),
            transform.position.y,
            transform.position.z-(size.z/2)+(size.z/resolution/2)
            );
        for (int row = 0; row < resolution-1; row++)
        {
            for (int col = 0; col < resolution-1; col++)
            {
                Vector3 position = new Vector3(
                    cornerCoordinate.x + (size.x/(resolution-1)*row),
                    cornerCoordinate.y,
                    cornerCoordinate.z + (size.z/(resolution-1)*col)
                );
                GameObject lightObj = new GameObject($"Light_{row}_{col}");
                lightObj.transform.position = position;
                lightObj.transform.parent = transform;

                Light light = lightObj.AddComponent<Light>();
                light.type = LightType.Point;
                light.intensity = lightIntensity;
                light.color = lightColor;
            }
        }
    }

}

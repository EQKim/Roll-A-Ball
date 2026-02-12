using UnityEngine;

public class Rotator : MonoBehaviour
{
 
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime); // Rotate the object around its local axes at a rate of 15 degrees per second on the X-axis, 30 degrees per second on the Y-axis, and 45 degrees per second on the Z-axis
    }
}

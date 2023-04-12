using UnityEngine;

public class SpriteBillboard : MonoBehaviour
{
    //values that will be set in the Inspector
    public Transform target;
    public float RotationSpeed;

    //values for internal use
    private Quaternion _lookRotation;
    private Vector3 _direction;

    // Update is called once per frame
    void LateUpdate()
    {
        // Look at including x and z leaning
        transform.LookAt(target);

        // Euler angles are easier to deal with. You could use Quaternions here also
        // C# requires you to set the entire rotation variable. You can't set the individual x and z (UnityScript can), so you make a temp Vec3 and set it back
        Vector3 eulerAngles = transform.rotation.eulerAngles;
        eulerAngles.x = 0;
        eulerAngles.z = 0;

        // Set the altered rotation back
        transform.rotation = Quaternion.Euler(eulerAngles);


        //lookPos.y = 0;
        //var rotation = Quaternion.LookRotation(lookPos);
        //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
        //transform.rotation = Quaternion.Euler(0f, Camera.main.transform.rotation.eulerAngles.y, 0f);
    }
}

using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    public float vAngle;
    public float minAngle;
    public float maxAngle;
    
    public float mouseSensitivity;

    private void LateUpdate()
    {
        if (GameManager.Instance.CurrentGameState == GameManager.GameState.PAUSED ||
            GameManager.Instance.CurrentGameState == GameManager.GameState.END) return;
        
        transform.Rotate(-Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity, 0 , 0);
        
        vAngle -= Input.GetAxis("Mouse Y");
        vAngle = Mathf.Clamp(vAngle, minAngle, maxAngle);
        transform.rotation = Quaternion.Euler(vAngle, transform.eulerAngles.y, transform.eulerAngles.z);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            other.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            other.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}

using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterMenuAnim : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
       Invoke("EnableAnimation", Random.Range(0, 1.5f));
    }

    void EnableAnimation()
    {
        _animator.enabled = true;
        _animator.speed += Random.Range(-0.25f, 0.25f);
    }
    
    private void OnDisable()
    {
        StopAllCoroutines();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class AgentBehaviour : MonoBehaviour
{
    public Events.EventEPressed OnEPressed;
    public Events.EventLMBPressed OnLMBPressed;
    
    [SerializeField] private List<Transform> possibleSections = new List<Transform>();
    [SerializeField] private Transform exit;
    [SerializeField] private float probability;
    
    private Vector3 _dest;
    private int _itemToCollect;
    private NavMeshAgent _navMeshAgent;
    private QuestGenerator _questGenerator;
    private PickUp _pickUp;
    private Slapper _slapper;

    private void Start()
    {
        _slapper = GetComponent<Slapper>();
        _slapper.OnCharacterEndSlap.AddListener(CheckForItem);
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _questGenerator = GetComponent<QuestGenerator>();
        _pickUp = GetComponent<PickUp>();
        _itemToCollect = _questGenerator.itemsToCollect[0];
        
        InitSections();
    }

    void InitSections()
    {
        TypeSize goalSectionType = ItemsDatabase.Instance.database[_itemToCollect].typeSize;

        foreach (Section section in MapFactory.spawnnedSections)
        {
            if (section.typeSize == goalSectionType)
            {
                possibleSections.Add(section.transform);
            }
        }
    }

    void ChooseNewSection()
    {
        if (possibleSections.Count > 0)
        {
            int random = Random.Range(0, possibleSections.Count);
            _dest = possibleSections[random].position;
            possibleSections.RemoveAt(random);
        }
        else
        {
            _dest = exit.position;
        }
    }

    private void Update()
    {
        if (_navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete)
        {
            CheckForItem();
        }
    }
    
    private void OnTriggerEnter(Collider other)
    { 
        if( other.GetComponent<Slapper>())
        {
            if (Random.value <= probability)
            {
                OnLMBPressed.Invoke();

                StartCoroutine(WaitForAction());
            }
        }
    }

    private IEnumerator WaitForAction()
    {
        yield return new WaitUntil(() => _pickUp.canDoAction == true);

        CheckForItem();
    }

    private void CheckForItem()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 5);

        foreach (Collider collider in colliders)
        {
            if (collider.GetComponent<ItemId>() && collider.GetComponent<ItemId>().itemId == _itemToCollect)
            {
                OnEPressed.Invoke();

                _dest = _pickUp.hasItem ? exit.position : collider.transform.position;
                break;
            }
            else
            {
                if (_navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete)
                {
                    ChooseNewSection();
                }
                break;
            }
            
            UpdateDestination();
        }
    }
    
    void UpdateDestination()
    {
        _navMeshAgent.SetDestination(_dest);
    }
}

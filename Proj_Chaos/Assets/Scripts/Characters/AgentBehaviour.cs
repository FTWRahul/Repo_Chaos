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

    private void Awake()
    {
        _slapper = GetComponent<Slapper>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _questGenerator = GetComponent<QuestGenerator>();
        _pickUp = GetComponent<PickUp>();
    }

    private void OnEnable()
    {
        _slapper.OnCharacterEndSlap.AddListener(CheckForItem);
    }

    private void OnDisable()
    {
        _slapper.OnCharacterEndSlap.RemoveListener(CheckForItem);
        NPCSpawner.spawnedNPCS.Remove(this.gameObject);
    }

    private void Start()
    {
        _itemToCollect = _questGenerator.itemsToCollect[0];
        exit = GameObject.FindGameObjectWithTag("Exit").transform;
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
        
        ChooseNewSection();
        UpdateDestination();
    }

    void ChooseNewSection()
    {
        if (possibleSections.Count > 0)
        {
            int random = Random.Range(0, possibleSections.Count);
            _dest = possibleSections[random].position;
            possibleSections.RemoveAt(random);
        }
        else if(exit != null)
        {
            _dest = exit.position;
        }
        else
        {
            exit = GameObject.FindGameObjectWithTag("Exit").transform;
            _dest = exit.position;
        }
    }

    private void Update()
    {
        if (IsAtDestination())
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
        bool itemFound = false;
        foreach (Collider collider in colliders)
        {
            if (collider.GetComponent<ItemId>() != null  && collider.GetComponent<ItemId>().itemId == _itemToCollect)
            {
                itemFound = true;

                OnEPressed.Invoke();

                _dest = _pickUp.hasItem ? exit.position : collider.transform.position;
                UpdateDestination();
                break;
            }
           
        }
        if (!itemFound)
        {
            ChooseNewSection();
            //UpdateDestination();
        }
        UpdateDestination();
    }
    
    void UpdateDestination()
    {
        if (_navMeshAgent.isOnNavMesh)
        {
            _navMeshAgent.SetDestination(_dest);
        }
    }

    bool IsAtDestination()
    {
        return Vector3.Distance (_dest, transform.position) < 2.0f;
    }
}

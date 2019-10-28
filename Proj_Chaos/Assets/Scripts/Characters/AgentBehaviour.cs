using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public enum AgentState
{
    START,
    RUNNING_TO_SECTION,
    RUNNING_TO_ITEM,
    RUNNING_TO_EXIT,
}

public class AgentBehaviour : MonoBehaviour
{
        public bool canMove;
        
        [HideInInspector] public Events.EventItemSelection onItemSelection;
        [HideInInspector] public Events.EventPickupCall onPickupCall;
        [HideInInspector] public Events.EventSlapCall onSlapCall;
        [HideInInspector] public Events.EventUpdateMovement onUpdateMovement;
    
        [SerializeField] private AgentState currentState = AgentState.START;
        
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private List<Transform> sections = new List<Transform>();
        [SerializeField] private GameObject[] exits;
        [SerializeField] private float probability;
    
        private SpawnedItem _desireItem;
        private Vector3 _distance;
        private int _range;
    
        private NavMeshAgent _navMeshAgent;
        private QuestGenerator _questGenerator;
        private PickupSystem _pickupSystem;

        private void Awake()
        {
            exits = GameObject.FindGameObjectsWithTag("Exit");
            
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _questGenerator = GetComponent<QuestGenerator>();
            _pickupSystem = GetComponent<PickupSystem>();
        }

        private void Start()
        {
            UpdateState(AgentState.START);
        }

        private void InitSections()
        {
            foreach (Section section in MapFactory.spawnedSections)
            {
                sections.Add(section.transform);
            }
            UpdateState(AgentState.RUNNING_TO_SECTION);
        }

        public void EnableNavMesh()
        {
            _navMeshAgent.enabled = true;
            UpdateDestination();
        }

        public void DisableNavMesh()
        {
            _navMeshAgent.enabled = false;
        }

        private void Update()
        {
            if (_navMeshAgent.enabled)
            {
                onUpdateMovement.Invoke(0, Math.Abs(_navMeshAgent.velocity.magnitude) > 0 ? 1 : 0);

                if (IsAtDestination())
                {
                    switch (currentState)
                    {
                        case AgentState.RUNNING_TO_SECTION:
                            LookForItem();
                            break;
                        case AgentState.RUNNING_TO_ITEM:
                            if (_desireItem != null)
                            {
                                FaceTarget(_desireItem.transform.position); 
                                onPickupCall.Invoke();
                            }
                            else
                            {
                                UpdateState(AgentState.RUNNING_TO_SECTION);
                            }
                            break;
                    }
                }
            }
            else
            {
                onUpdateMovement.Invoke(0, 0);
            }

            if (_pickupSystem.GetItemHold())
            {
                UpdateState(AgentState.RUNNING_TO_EXIT);
            }
        }
    
        private void OnTriggerEnter(Collider other)
        {
            if (!other.GetComponent<SlapSystem>()) return;
            if (!(Random.value <= probability)) return;
        
            onSlapCall.Invoke();
            LookForItem();
        }
    
        private void UpdateState(AgentState newState)
        {
            currentState = newState;

            switch (currentState)
            {
                case AgentState.START:
                    InitSections();
                    break;
            
                case AgentState.RUNNING_TO_SECTION:

                    if (sections.Count > 0)
                    {
                        Transform randomSection = sections[Random.Range(0, sections.Count)];
                        _distance = randomSection.position;
                        NavMeshPath sectionPath = new NavMeshPath();
                
                        if (!NavMesh.CalculatePath(transform.position, _distance, NavMesh.AllAreas, sectionPath))
                        {
                            Vector3 randomPoint = _distance + Random.insideUnitSphere * _range;
                    
                            if (NavMesh.SamplePosition(randomPoint, out var hit, 2.0f, NavMesh.AllAreas)) {
                                _distance = hit.position;
                            }
                        }
                
                        sections.Remove(randomSection);
                    
                    }
                    else
                    {
                        UpdateState(AgentState.RUNNING_TO_EXIT);
                    }
                    break;
            
                case AgentState.RUNNING_TO_ITEM:

                    _distance = _desireItem.transform.position;
                    NavMeshPath itemPath = new NavMeshPath();
                
                    if (!NavMesh.CalculatePath(transform.position, _distance, NavMesh.AllAreas, itemPath))
                    {
                        Vector3 randomPoint = _distance + Random.insideUnitSphere * 2;
                    
                        if (NavMesh.SamplePosition(randomPoint, out var hit, 2.0f, NavMesh.AllAreas)) {
                            _distance = hit.position;
                        }
                    }
                
                    break;
            
                case AgentState.RUNNING_TO_EXIT:
                    _distance = exits[Random.Range(0, exits.Length - 1)].transform.position;
                
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        
            UpdateDestination();
        }

        private void UpdateDestination()
        {
            if (_navMeshAgent.enabled)
            {
                if (_navMeshAgent.isOnNavMesh)
                {
                    _navMeshAgent.SetDestination(_distance);
                }
                else
                {
                    Destroy(gameObject);
                    NPCSpawner.spawnedAmount--;
                }
            }
        }

        public void LookForItem()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 5, layerMask);
        
            foreach (Collider collider in colliders)
            {
                SpawnedItem item = collider.GetComponent<SpawnedItem>();
                if (item.itemId == _questGenerator.itemsToCollect[0])
                {
                    _desireItem = item;
                    _pickupSystem.SelectItem(item.gameObject);
                    UpdateState(AgentState.RUNNING_TO_ITEM);
                    break;
                }
            }

            if (_desireItem == null)
            {
                UpdateState(AgentState.RUNNING_TO_SECTION);
            }
        }
    
        private void FaceTarget(Vector3 destination)
        {
            Vector3 lookPos = destination - transform.position;
            lookPos.y = 0;
            Quaternion rot = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, 1);  
        }
    
        private bool IsAtDestination()
        {
            if(_distance != Vector3.zero)
                return Vector3.Distance(transform.position, _distance) < _navMeshAgent.stoppingDistance;
            else
            {
                return false;
            }
        }
    }
    

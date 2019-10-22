using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour
{
   public List<GameObject> shelfPrefabs;

   public Transform lowerBound, upperBound;

   public Transform startPoint;

   public float aileSize;

   public LayerMask myLayerMask;
   
   [ContextMenu("Generate")]
   public void GenerateShelves()
   {
      //going through the list of items
      for (int i = 0; i < shelfPrefabs.Count; i++)
      {
         //getting the box collider
         BoxCollider myCollider =  shelfPrefabs[i].AddComponent<BoxCollider>();
         Vector3 checkRotation = Vector3.zero;
         for (int j = 0; j < 4; j++)
         {
            Collider[] colArry = Physics.OverlapBox(startPoint.position + new Vector3(0, .5f),
               myCollider.size / 2,
               Quaternion.Euler(checkRotation), myLayerMask);
            Debug.Log(colArry.Length);
           if(!Physics.CheckBox(startPoint.position + new Vector3(0, .5f),
               myCollider.size,
               Quaternion.Euler(checkRotation), myLayerMask))
           {
              GameObject tempGo = Instantiate(shelfPrefabs[i], startPoint.position + new Vector3(0, 0.5f), Quaternion.Euler(checkRotation));
              //DestroyImmediate(myCollider, true);
              break;
           }
            /*if (colArry.Length < 1)
            {
               GameObject tempGo = Instantiate(shelfPrefabs[i], startPoint.position + new Vector3(0, 0.5f), Quaternion.Euler(checkRotation));
               break;
            }*/
            checkRotation.y += 90;
         }

         startPoint.position += new Vector3(0,0,myCollider.size.z + aileSize);
         DestroyImmediate(myCollider, true);

      }
      //Function that takes game object and uses its collider to do a cast at hit point.
      //see if case is overlapping, rotate by 90. check again.
      //Pass = place down object, Fail = try the next object.
      //Offset the start point by collider width + Aile size.
      //Repeat.
   }
   
   public void GenerateNew()
   {
      for (int i = 0; i < shelfPrefabs.Count; i++)
      {
         int rand = Random.Range(0, shelfPrefabs.Count);
         Instantiate(shelfPrefabs[rand], startPoint.position + new Vector3(0, .5f, 0), Quaternion.identity);
      }
   }

   public void OnDrawGizmos()
   {
      //Gizmos.DrawCube(Vector3.zero, shelfPrefabs[0].GetComponent<BoxCollider>().size);
   }
}

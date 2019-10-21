using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
   public List<GameObject> shelfPrefabs;

   public Transform lowerBound, upperBound;

   public Transform startPoint;
   

   public void GenerateShelves()
   {
      for (int i = 0; i < shelfPrefabs.Count; i++)
      {
         //Physics.OverlapBox(startPoint.position + )
      }
      //Function that takes game object and uses its collider to do a cast at hit point.
      //see if case is overlapping, rotate by 90. check again.
      //Pass = place down object, Fail = try the next object.
      //Offset the start point by collider width + Aile size.
      //Repeat.
   }
   
   
   
}

using System;
using UnityEngine;

public class SpriteBillboard : MonoBehaviour
{
   public bool freezeXZAxis = true;

   private void LateUpdate()
   {
      if (freezeXZAxis)
      {
         if (Camera.main != null)
            transform.rotation = Quaternion.Euler(0f, Camera.main.transform.rotation.eulerAngles.y, 0f);
      }
      else
      {
         if (Camera.main != null) transform.rotation = Camera.main.transform.rotation;
      }
   }
}

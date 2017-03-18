using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets._scripts.Weapons
{
    /// <summary>
    ///     Pickupitem type dictionary like struct.
    /// </summary>
    [Serializable]
    public struct PickUpItemKeyValue
    {
        public PickUpItemType Type;
        public GameObject GameObject;

        public PickUpItemKeyValue(PickUpItemType key, GameObject val)
        {
            Type = key;
            GameObject = val;
        }
    }
}

using UnityEngine;

using System.Collections.Generic;

namespace PawnOfKings.Unity.Values {

    /// <summary>
    /// Provides a container of Units that can be populated in the Unity editor with UnitProperties.
    /// This container is used by the UnitLoader to find the settings and properties that define  
    /// the different Units.  Unit Properties placed in this container, in the Unity Editor, 
    /// will be the units available during gameplay in that scene.
    /// </summary>
    class UnitBank : MonoBehaviour {

        [Header( "Game Object Resource" )]
        public List<UnitProperties> Units;
    }
}
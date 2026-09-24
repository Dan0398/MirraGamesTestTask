using UnityEngine;
using System;

namespace Dan398.UI
{
    [Serializable]
    public struct Trs
    {
        public Vector3 position;
        public Vector3 rotation;
        public Vector3 scale;

        public Trs(Vector3 position, Vector3 rotation, Vector3 scale)
        {
            this.position = position;
            this.rotation = rotation;
            this.scale = scale;
        }
    }
}
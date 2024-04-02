using System.Collections.Generic;
using UnityEngine;

namespace Computer
{
    /// <summary>
    /// Holds the state of computer. each computer has its own state
    /// </summary>
    [CreateAssetMenu(fileName = "ComputerState", menuName = "Computer/ComputerState", order = 0)]
    public class ComputerState : ScriptableObject
    {
        /// <summary>
        /// All the files that the computer has
        /// </summary>
        public List<File> files = new List<File>();
        /// <summary>
        /// All the softwares that the computer has
        /// </summary>
        public List<Software> softwares = new List<Software>();
    }
}
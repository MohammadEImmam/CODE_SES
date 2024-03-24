using UnityEngine;

namespace Computer
{
    /// <summary>
    /// Software is base class for all the programs that the computer can run 
    /// </summary>
    [CreateAssetMenu(fileName = "ComputerState", menuName = "Computer/Software", order = 0)]
    public class Software : MonoBehaviour
    {
        /// <summary>
        /// if true, the software will not have a file (and wont appear in desktop)
        /// </summary>
        public bool hidden = false;
        /// <summary>
        /// Icon to appear in desktop
        /// </summary>
        public Sprite icon;


        /// <summary>
        /// is the software running right now?
        /// </summary>
        public bool isRunning { get; set; }
        /// <summary>
        /// the computer that owns this software 
        /// </summary>
        public ComputerInstance computer { get; set; }

        /// <summary>
        /// called once when the software is running
        /// </summary>
        public virtual void OnStart() { }
        /// <summary>
        /// called once when the software is closed
        /// </summary>
        public virtual void OnEnd() { }
        /// <summary>
        /// called every frame while the software is running
        /// </summary>
        /// <param name="deltaTime">similar to Time.deltaTime</param>
        public virtual void OnWhileRunning(float deltaTime) { }
    }
}
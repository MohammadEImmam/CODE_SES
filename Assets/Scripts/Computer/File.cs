namespace Computer
{
    using System;
    using System.Data.SqlTypes;
    using CrossConnections;
    using UnityEngine;
    /// <summary>
    /// Anything can appear in desktop and file explorer.
    /// like executables (to run software when clicked), text files, code files, etc..
    /// files can used to store the data
    /// </summary>
    [CreateAssetMenu(fileName = "File", menuName = "Computer/File", order = 0)]
    public class File : ScriptableObject
    {
        /// <summary>
        /// icon that represent the file in the desktop
        /// </summary>
        public Sprite icon;
        /// <summary>
        /// extensions of the file (*.cs *.txt *.exe etc..). also can tell us about the file type
        /// </summary>
        public FileExtension extension;
        /// <summary>
        /// data of the file that store
        /// </summary>
        [TextArea]
        public string data;


        /// <summary>
        /// computer which file belongs to
        /// </summary>
        public ComputerInstance computer { get; set; }
        /// <summary>
        /// the job that associated with file. when it is set the JobManager will use the data of this file to validate that job
        /// </summary>
        public ManagedJob associatedJob { get; set; }

        /// <summary>
        /// file name +  its extensions
        /// </summary>
        public string fullName
        {
            get => name + "." + extension;
        }
    }
    public enum FileExtension
    {
        /// <summary>
        /// Executable, to run a software
        /// </summary>
        exe,
        /// <summary>
        /// Text file
        /// </summary>
        txt,
        /// <summary>
        /// C# file
        /// </summary>
        cs,
    }
}
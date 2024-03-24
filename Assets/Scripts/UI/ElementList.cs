using UnityEngine;
using System.Collections.Generic;
using System;

/// <summary>
/// Utility class to easy mange list of UI elements
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class ElementList<T> : MonoBehaviour where T : Component
{
    [SerializeField]
    GameObject template;
    [SerializeField]
    Transform _container;
    List<T> elements = new List<T>();

    public Transform container => _container ? _container : transform;

    public int count => elements.Count;

    protected virtual void Reset()
    {
        template = GetComponentInChildren<T>(true)?.gameObject;
        if (template)
            _container = template.transform.parent;
        if (template && template.gameObject.activeSelf)
            Debug.Log("It is recommended to set template inactive.", template);
    }



    protected virtual void OnValidate()
    {
        if (template == null) return;
        if (!template.GetComponent<T>())
        {
            Debug.LogError("Template must has component " + typeof(T).Name);
            template = null;
            return;
        }
    }
    /// <summary>
    /// called once an element created. useuful to register your events
    /// </summary>
    /// <param name="element"></param>
    protected virtual void OnElementCreated(T element)
    {

    }
    /// <summary>
    /// called once an element removed. useful to unregister your events
    /// </summary>
    /// <param name="element"></param>
    protected virtual void OnElementRemoved(T element)
    {

    }

    /// <summary>
    /// fundamental function.
    /// Gets an element at given index. 
    /// if the element is not there it will instantiate it from the template
    /// if the element is exist but not active, it will activate it.
    /// </summary>
    public T Aquire(int index)
    {
        while (index >= elements.Count)
        {
            var go = Instantiate(template, container);
            go.SetActive(true);
            var element = go.GetComponent<T>();
            elements.Add(element);
            OnElementCreated(element);
        }
        return elements[index];
    }
    /// <summary>
    /// fundamental function.
    /// resisze the number of elements.
    /// if the newSize  > current size, then it will create new elements
    /// if the newSize < current size, then it will remove elements
    /// </summary>
    /// <param name="newSize"></param>
    public void Resize(int newSize)
    {
        while (elements.Count > newSize)
        {
            var element = elements[elements.Count - 1];
            OnElementRemoved(element);
            elements.RemoveAt(elements.Count - 1);

#pragma warning disable 0618
            Destroy(element.gameObject);
#pragma warning restore 0618
        }
        while (elements.Count < newSize)
        {
            var element = Instantiate(template, container).GetComponent<T>();

#pragma warning disable 0618
            element.gameObject.SetActive(true);
#pragma warning restore 0618
            elements.Add(element);
            OnElementCreated(element);
        }
    }

    /// <summary>
    /// Iterate over number of elements
    /// </summary>

    public void Iterate(int count, Action<T, int> action)
    {
        for (int i = 0; i < count; i++)
        {
            action(Aquire(i), i);
        }
    }

    /// <summary>
    /// Iterate and resize
    /// </summary>
    public void IterateResized(int count, Action<T, int> action)
    {
        Iterate(count, action);
        Resize(count);
    }
}
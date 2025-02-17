using System;
using System.Collections.Generic;

public class GameEventSystem
{
    private static readonly Dictionary<string, List<Action<string, object>>> listeners = new();

    public static void EmitEvent(string type, object payload)
    {
        if (listeners.ContainsKey(type))
        {
            foreach (var action in listeners[type])
            {
                action(type, payload);
            }
        }
    }

    public static void AddListener(Action<string, object> listener, string type)
    {
        if (!listeners.ContainsKey(type))
        {
            listeners[type] = new();
        }
        listeners[type].Add(listener);
    }

    public static void RemoveListener(Action<string, object> listener, string type)
    {
        if (listeners.ContainsKey(type))
        {
            listeners[type].Remove(listener);
        }
    }
}

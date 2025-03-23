using System;
using System.Collections.Generic;

public class GameEventSystem
{
    public static readonly string broadcastEventName = "broadcastEventName";
    private static readonly Dictionary<string, List<Action<string, object>>> listeners = new();
    private static readonly string[] broadcastEventNames = {broadcastEventName};

    public static void EmitEvent(string type, object payload)
    {
        if (listeners.ContainsKey(type))
        {
            foreach (var action in listeners[type])
            {
                action(type, payload);
            }
        }
        if (listeners.ContainsKey(broadcastEventName))
        {
            foreach (var action in listeners[broadcastEventName])
            {
                action(type, payload);
            }
        }
    }

    public static void AddListener(Action<string, object> listener, params string[] types)
    {
        if (types.Length == 0)
        {
            types = broadcastEventNames;
        }

        foreach (string type in types)
        {
            if (!listeners.ContainsKey(type))
            {
                listeners[type] = new();
            }
            listeners[type].Add(listener);
        }
    }

    public static void RemoveListener(Action<string, object> listener, params string[] types)
    {
        if (types.Length == 0)
        {
            types = broadcastEventNames;
        }

        foreach (string type in types)
        {
            if (listeners.ContainsKey(type))
            {
                listeners[type].Remove(listener);
            }
        }
    }
}

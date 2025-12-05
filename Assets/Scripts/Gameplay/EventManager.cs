using System;
using UnityEngine;

public static class EventManager
{
    public static Action<int, int, int> onDayPassed;
    public static void OnDayPassed(int day, int month, int year) => onDayPassed?.Invoke(day, month, year);

    public static Action onWorkerNumberChanged;
    public static void OnWorkerNumberChanged() => onWorkerNumberChanged?.Invoke();

    public static Action<ResourceType> onResourceNumberChanged;
    public static void OnResourceNumberChanged(ResourceType resourceType) => onResourceNumberChanged?.Invoke(resourceType);
}
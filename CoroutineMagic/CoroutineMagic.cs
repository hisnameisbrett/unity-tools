/******************************************************************************
Filename:   CoroutineMagic.cs
Author(s):  Brett Cunningham
Date:       8/6/2018
Brief:      Static helpers for "fancy" coroutines. Can pass any Monobehaviour 
            to run the coroutines on.

Example Usage:

public void Foo()
{
    int count = 0;

    // runs "count++" on this Monobehaviour after at the end of the frame.
    CoroutineMagic.DelayActionByFrames(this, () => { count++; });

    // runs "count++" on this Monobehaviour after 3 frames.
    CoroutineMagic.DelayActionByFrames(this, () => { count++; }, 3);

    // runs "count++" on this Monobehaviour after 5 seconds.
    CoroutineMagic.DelayActionBySeconds(this, 5, () => { count++; });

    // runs "count++" on this Monobehaviour each frame for 7 seconds.
    CoroutineMagic.RepeatingAction(this, 7, () => { count++; });
}
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public static class CoroutineMagic
{
    // call a function after seconds
    public static void DelayActionBySeconds(MonoBehaviour context, float s, System.Action action)
    {
        context.StartCoroutine(delayActionBySeconds(s, action));
    }

    private static IEnumerator delayActionBySeconds(float s, System.Action action)
    {
        yield return new WaitForSeconds(s);
        action();
    }

    // call function after x amount of frames (default 1)
    public static void DelayActionByFrames(MonoBehaviour context, System.Action action, int frames = 1)
    {
        context.StartCoroutine(delayActionByFrames(action, frames));
    }

    private static IEnumerator delayActionByFrames(System.Action action, int frames)
    {
        while (frames-- > 0) { yield return new WaitForEndOfFrame(); }
        action();
    }

    // repeat a function every frame for a duration
    public static void RepeatingAction(MonoBehaviour context, float totalDuration, System.Action action)
    {
        context.StartCoroutine(repeatingAction(totalDuration, action));
    }

    // repeat a function every frame for a duration
    public static void RepeatingAction(MonoBehaviour context, float totalDuration, System.Action<float> action)
    {
        context.StartCoroutine(repeatingAction(totalDuration, action));
    }

    private static IEnumerator repeatingAction(float totalDuration, System.Action action)
    {
        float elapsedTime = 0;
        while (elapsedTime <= totalDuration)
        {
            action();
            yield return new WaitForEndOfFrame();
            elapsedTime += Time.deltaTime;
        }
    }

    private static IEnumerator repeatingAction(float totalDuration, System.Action<float> action)
    {
        float elapsedTime = 0;
        while (elapsedTime <= totalDuration)
        {
            action((elapsedTime / totalDuration));
            yield return new WaitForEndOfFrame();
            elapsedTime += Time.deltaTime;
        }
    }
    
    // keep calling a function until predicate return true
    public static void TryActionUntilSuccess(MonoBehaviour context, System.Func<bool> predicate, System.Action action)
    {
        context.StartCoroutine(tryActionUntilSuccess(predicate, action));
    }

    private static IEnumerator tryActionUntilSuccess(System.Func<bool> predicate, System.Action action)
    {
        do
        {
            action();
            yield return new WaitForEndOfFrame();
        }
        while (!predicate());
    }

    // call function predicate every frame until it returns, then call the next function
    public static void PerformActionOnSuccess(MonoBehaviour context, System.Func<bool> predicate, System.Action action)
    {
        context.StartCoroutine(performActionOnSuccess(predicate, action));
    }

    private static IEnumerator performActionOnSuccess(System.Func<bool> predicate, System.Action action)
    {
        yield return new WaitUntil(predicate);
        action();
    }
}


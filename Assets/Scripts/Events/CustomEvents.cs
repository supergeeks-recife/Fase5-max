using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class IntEvent : UnityEvent<int> { }

[Serializable]
public class FloatEvent : UnityEvent<float> { }

[Serializable]
public class BoolEvent : UnityEvent<bool> { }

[Serializable]
public class StringEvent : UnityEvent<string> { }

[Serializable]
public class GameObjectEvent : UnityEvent<GameObject> { }

[Serializable]
public class Vector3Event : UnityEvent<Vector3> { }


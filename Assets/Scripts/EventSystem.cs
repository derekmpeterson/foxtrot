using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class EventSystem : MonoBehaviour {
	public interface EventData {};

	public class SimpleGameEvent : UnityEvent<GameObject> {};
	public class GameEvent : UnityEvent<GameObject, EventData> {};
}

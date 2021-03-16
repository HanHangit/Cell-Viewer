using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface Controller
{
	void AddClickEventListener(UnityAction<ControllerArgs> action);
	void AddReleaseEventListener(UnityAction<ControllerArgs> action);
	void AddDragStartEventListener(UnityAction<ControllerArgs> action);
	void AddDragEndEventListener(UnityAction<ControllerArgs> action);
}

public class ControllerArgs
{
	public Entity Entity;
}

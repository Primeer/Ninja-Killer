using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITouchable
{
	bool Solid { get; }

	void Touch();
}

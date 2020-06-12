using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

public class RopeCutter : MonoBehaviour
{
	public void Cut()
	{
		GameObject secondPart = Instantiate(gameObject, transform.parent);
		RopeResize(gameObject, true);
		RopeResize(secondPart, false);
	}

	private void RopeResize(GameObject rope, bool upper)
	{
		ObiRope obiRope = rope.GetComponent<ObiRope>();
		ObiRopeCursor obiCursor = rope.GetComponent<ObiRopeCursor>();

		foreach(ObiParticleAttachment attachment in rope.GetComponents<ObiParticleAttachment>())
		{
			if (upper)
			{
				if (attachment.attachmentType == ObiParticleAttachment.AttachmentType.Dynamic)
					attachment.enabled = false;
			}
			else
			{
				if (attachment.attachmentType == ObiParticleAttachment.AttachmentType.Static)
					attachment.enabled = false;
			}
		}
		
		obiCursor.cursorMu = upper ? 1f : 0f;
		obiCursor.ChangeLength(obiRope.restLength / 2f);
	}
}

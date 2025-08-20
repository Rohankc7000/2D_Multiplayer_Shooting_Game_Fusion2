using System.Collections;
using UnityEngine;

public static class Utilis
{
	public static IEnumerator PlayAnimationSetStateWhenFinished(GameObject parent,Animator animator, string clipName, bool activeStateAtTheEnd)
	{
		animator.Play(clipName);
		var animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
		yield return new WaitForSeconds(animationLength);
		parent.SetActive(activeStateAtTheEnd);
	}
}

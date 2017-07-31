using UnityEngine;

/// <summary>
/// Ends the level if both players are in the trigger.
/// </summary>
public class LevelEndTrigger : MonoBehaviour
{
	private int m_playerCount = 0;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			m_playerCount++;
			if (m_playerCount >= 2)
			{
				SceneLoader.Instance.NextScene();
			}
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			m_playerCount--;
		}
	}
}

using UnityEngine;

public class AudioMeter : MonoBehaviour
{
	public AudioSource audioSource;
	public float updateStep = 0.01f;
	public int sampleDataLength = 1024;

	private float currentUpdateTime = 0f;

	public float clipLoudness;
	private float[] clipSampleData;

	public float sizeFactor = 1;

	public SpriteRenderer spriteRenderer;

	private void Awake()
	{
		clipSampleData = new float[sampleDataLength];
		spriteRenderer.drawMode = SpriteDrawMode.Tiled;
	}

	private void Update()
	{
		currentUpdateTime += Time.deltaTime;
		if (currentUpdateTime >= updateStep)
		{
			currentUpdateTime = 0f;
			audioSource.clip.GetData(clipSampleData, audioSource.timeSamples);
			clipLoudness = 0f;
			foreach (var sample in clipSampleData)
			{
				clipLoudness += Mathf.Abs(sample);
			}
			clipLoudness /= sampleDataLength;

			clipLoudness *= sizeFactor;

			spriteRenderer.size = new Vector2(spriteRenderer.size.x, clipLoudness);

		}
	}
}



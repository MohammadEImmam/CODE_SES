using UnityEngine;

public class NightDayCycle : MonoBehaviour
{
    public bool isNight;
    public bool isDay;
    public float minutesPerCycle = 5f; // Time in minutes for each cycle (day-night transition)
    public Transform sunTransform; // Reference to the directional light's transform
    private float elapsedTime = 0f; // Time elapsed since the start of the cycle

    void Update()
    {
        // Increment the elapsed time by the time passed since the last frame
        elapsedTime += Time.deltaTime;

        // If the elapsed time exceeds the duration of one full cycle, reset it
        if (elapsedTime >= minutesPerCycle * 60f)
        {
            elapsedTime = 0f;
        }

        // Calculate the current cycle phase (0 to 1)
        float cycleProgress = elapsedTime / (minutesPerCycle * 60f);

        // Update the cycle (transition between day and night)
        UpdateCycle(cycleProgress);

        // Check if it's night or day
        DetectDayNight(cycleProgress);
    }

    void UpdateCycle(float cycleProgress)
    {
        // Rotate the directional light (sun/moon) based on the cycle progress
        if (sunTransform != null)
        {
            // Calculate the angle of rotation (0 to 360 degrees)
            float angle = Mathf.Lerp(0f, 360f, cycleProgress);

            // Apply the rotation around the y-axis (up direction)
            sunTransform.rotation = Quaternion.Euler(new Vector3(angle, 0f, 0f));
        }

        // You can further customize the cycle update logic here.
        // For example, you can adjust other visual effects based on the cycle progress.
    }

    void DetectDayNight(float cycleProgress)
    {
        // Determine if it's night or day based on the cycle progress
        if (cycleProgress < 0.5f)
        {
            isDay = true;
            isNight = false;
        }
        else
        {
            isDay = false;
            isNight = true;
        }
    }
}

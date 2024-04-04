using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace PointsManager
{


    public class Points : MonoBehaviour
    {
        // Points can go from 0-100
        // Points define player's performence in the game
        // Player starts with 50 points


        // Start is called before the first frame update

        float points = 50.0f;

        void Start()
        {
            //Call some getter to get presistent point data and set to points
            if ((PlayerPrefs.HasKey("points")))
            {
                points = PlayerPrefs.GetFloat("points");
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public float getPoints()
        {
            return points;
        }

        void SavePoints()
        {
            PlayerPrefs.SetFloat("points", (float)points);
            Debug.Log("User Points has updated to: " + points);
        }

        private void subtractPoints(float negateVal)
        {
            points = points - negateVal;
            SavePoints();
        }

        private void addPoints(float addVal)
        {
            points = points + addVal;
            SavePoints();
        }

        // To introduce some difficulty we can call invalid runs on failed test cases

        // Run this for each invalid test case?
        public void RunTestCases(int treshold, bool valid)
        {
            if (treshold > 3 || treshold < 0)
            {
                Debug.LogWarning("This is a warning message");
                if (!valid)
                {
                    subtractPoints(0.1f);
                    return;
                }
                else
                {
                    addPoints(0.5f);
                }

                return;
            }

            //Easier the question, Higher the threshold
            //meaning you loose more points getting easy questions wrong
            if (!valid)
            {
                if (treshold == 0)
                {
                    subtractPoints(0.2f);
                    return;
                }

                if (treshold == 1)
                {
                    subtractPoints(0.4f);
                    return;
                }

                if (treshold == 2)
                {
                    subtractPoints(0.6f);
                    return;
                }

                if (treshold == 3)
                {
                    subtractPoints(0.8f);
                }

                return;
            }

            //gain more points getting hard questions right
            if (treshold == 0)
            {
                addPoints(4.2f);
                return;
            }

            if (treshold == 1)
            {
                addPoints(7.9f);
                return;
            }

            if (treshold == 2)
            {
                addPoints(9.8f);
                return;
            }

            if (treshold == 3)
            {
                addPoints(13.6f);
            }
        }

        public void Scenario(float value, bool valid)
        {
            if (value > 10)
            {
                //too much?
                Debug.LogWarning("Scenario Point Negate Value is too high");
                return;
            }

            // Use this carefully, implement dynamic system like above later
            if (valid)
            {
                addPoints(value);
                return;
            }

            subtractPoints(value);
        }


    }

}
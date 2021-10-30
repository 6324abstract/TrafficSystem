using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GleyTrafficSystem
{
    public class CarHazard : MonoBehaviour
    {
        public GameObject Car;
        public Transform turning_point;
        public float speed;
        public float acce;
        public float TimeToArrival;//time for maneuver 
        private float steering;
        VehicleLightsComponent lightsComponent;

        public void Start()
        {
            lightsComponent = gameObject.GetComponent<VehicleLightsComponent>();
            lightsComponent.Initialize();

        }
        public void go_straight()// stage 1 acclerating to the intersection
        {
            Transform hazardcar = Car.transform;
            speed += Time.deltaTime * acce;
            hazardcar.Translate(transform.forward * Time.deltaTime * speed);
            lightsComponent.SetBlinker(BlinkType.BlinkRight);

        }
        public void left_turn()
        {
            lightsComponent.SetBlinker(BlinkType.BlinkRight);
            Transform hazardcar = Car.transform;
            hazardcar.Translate(hazardcar.forward * Time.deltaTime * speed);
            hazardcar.Rotate(hazardcar.up * Time.deltaTime * speed);
        }

        public bool WithinTTA()
        {
            return true;
        }
        /*private float CalculateTTA()
        {
            return;
        }*/
    }
}
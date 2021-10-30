using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GleyTrafficSystem
{
    public class hazardcontrol : MonoBehaviour
{
        private enum StateOfHazard
        {
            idle,
            pedestrian_triggered,
            vehicle_acce,
            vehicle_turn,
            vehicle_to_vanish
}
        public GameObject hazardCar;
        public GameObject Mycar;
        private Transform waypoint;
        private float cur;
        public float accel;
        private float max;
        public float time_to_arrival;// time it takes to collisde given the current speed
        public float InitialSpeed = 2f;
        public int Speed_rot = 60;
        public float steer_angle;
        public float turning;
       

        public Animator animator;
        public GameObject pedestrian;
        private  pedestriancontroller ped;
        private StateOfHazard curstage;

        private float TTAofleftturn;
        private Rigidbody rb;//get the speed by rb.velocity

        VehicleLightsComponent lightsComponent;

        void Awake()
        {
            lightsComponent = hazardCar.GetComponent<VehicleLightsComponent>();
            lightsComponent.Initialize();
            animator.GetComponent<Animator>();
           
            curstage = StateOfHazard.idle;
            hazardCar.SetActive(false);
            rb = Mycar.GetComponent<Rigidbody>();

           
        }

        // Update is called once per frame
        void Update()
        {
            //Debug.Log(curstage);
            float localVelocity =Mathf.Abs(rb.velocity.x);
           lightsComponent.SetBlinker(BlinkType.BlinkRight);
         

            switch (curstage)
            {
                case (StateOfHazard.idle): 
                    {
                       bool totrigger = pedestrian.GetComponent<pedestriancontroller>().isWithinTTA(Mycar.transform, localVelocity);
                        if(totrigger)
                        curstage = StateOfHazard.pedestrian_triggered;
                        break;
                    }
                case (StateOfHazard.pedestrian_triggered):
                    {
                       
                        pedestrian.GetComponent<pedestriancontroller>().Wakling();
                        if (Input.GetKeyDown(KeyCode.X))
                        {
                          curstage = StateOfHazard.vehicle_acce;
                        }
                        break;
                    }
                case (StateOfHazard.vehicle_acce):
                    {
                       hazardCar.SetActive(true);
                        InitialSpeed += Time.deltaTime * accel;
                        hazardCar.transform.Translate(transform.forward * Time.deltaTime * InitialSpeed);
                        if (hazardCar.transform.position.x > turning)
                            curstage = StateOfHazard.vehicle_turn;
                      //  lightsComponent.SetBlinker(BlinkType.BlinkRight);
                        break;
                    }
                case (StateOfHazard.vehicle_turn):
                    {
                        lightsComponent.SetBlinker(BlinkType.BlinkRight);
                       hazardCar.transform.Translate(Vector3.forward * Time.deltaTime * InitialSpeed);
                        hazardCar.transform.Rotate(Vector3.up * Time.deltaTime * Speed_rot);
                        
                        if (hazardCar.transform.eulerAngles.y >= steer_angle)
                            curstage = StateOfHazard.vehicle_to_vanish;
                        break;
                    }
                case (StateOfHazard.vehicle_to_vanish):
                    {
                        hazardCar.transform.Translate(transform.forward * Time.deltaTime * 3f);
                        break;
                    }
                    

            }
            lightsComponent.UpdateLights();





        }
    }
}
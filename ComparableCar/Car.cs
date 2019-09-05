using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComparableCar
{
    class Car : IComparable
    {
        
        // 1. FIELDS.
        public const int MaxSpeed = 100;
        private bool carIsDead;
        private Radio theMusicBox = new Radio();

        // 2. PROPERTIES.
        public int CurrentSpeed { get; set; } = 0;
        public string PetName { get; set; } = "";
        public int CarID { get; set; }
        public Car(String name, int currSp, int id)
        {
            CurrentSpeed = currSp;
            PetName = name;
            CarID = id;
        }

        // 3. CONSTRUCTORS.
        public Car() { }
        public Car(string name, int speed)
        {
            CurrentSpeed = speed;
            PetName = name;
        }


        // 4. VOID METHODS.
        public void CrankTunes(bool state)
        {
            // Delegate request to inner object.
            theMusicBox.TurnOn(state);
        }

            // Checks if Car is overheated.
        public void Accelerate(int delta)
        {
            if (carIsDead)
                Console.WriteLine("{0} is out of order...", PetName);
            else
            {
                CurrentSpeed += delta;
                if (CurrentSpeed > MaxSpeed)
                {
                    carIsDead = true;
                    CurrentSpeed = 0;

                    //We need to call the HelpLink property, thus we need to create a local variable before throwing the Exception object.
                    Exception ex = new Exception($"{PetName} has overheated!");
                    ex.HelpLink = "http://www.CarsRUs.com";

                    // Stuff in custom data regarding the error.
                    ex.Data.Add("TimeStamp", $"The car exploded at {DateTime.Now}");
                    ex.Data.Add("Cause", "You have a lead foot");
                    throw ex;
                }
                else
                    Console.WriteLine("=> CurrentSpeed = {0}", CurrentSpeed);
            }
        }

        // 5. INTERFACE IMPLEMENTATIONS.
            // IComparable
        int IComparable.CompareTo(object obj)
        {
            Car temp = obj as Car;
            if (temp != null)
                return this.CarID.CompareTo(temp.CarID);
            else
                throw new ArgumentException("Parameter is not a Car!");
        }

            // IComparer
        public static IComparer SortByPetName { get { return (IComparer)new PetNameComparer(); } }
    }
}

using System;
using GenerticTest.Model;
using GenerticTest.Generics;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;

namespace GenerticTest
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Dynamisk model
            int count = 0;
            string sqlQuery = string.Empty;

            ADOConnection.Open();

            List<object> Person = new List<object>();
            List<object> Company = new List<object>();

            while (count < 2)
            {
                // SQL-forespørgsel til at hente data fra databasen.
                if (count == 0)
                    sqlQuery = "SELECT FirstName, Phone FROM person";
                else
                    sqlQuery = "SELECT CompanyName, Phone FROM company";


                using (MySqlCommand cmd = new MySqlCommand(sqlQuery, ADOConnection.conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (count == 0)
                        {
                            while (reader.Read())
                            {

                                dynamic dynamicObject = new DynamicObjectModel();

                                // Fyld dynamisk objekt med data fra databasen.
                                dynamicObject.Name = reader["FirstName"].ToString();
                                dynamicObject.Age = reader["Phone"];

                                // Nu har du et dynamisk objekt, der indeholder data fra databasen.
                                // Du kan arbejde med dette objekt dynamisk.
                                Console.WriteLine($"Name: {dynamicObject.Name}, Age: {dynamicObject.Age}");
                                Person.Add(dynamicObject);
                            }
                        }
                        else
                        {
                            while (reader.Read())
                            {
                                dynamic dynamicObject = new DynamicObjectModel();

                                // Fyld dynamisk objekt med data fra databasen.
                                dynamicObject.Name = reader["CompanyName"].ToString();
                                dynamicObject.Age = reader["Phone"];

                                // Nu har du et dynamisk objekt, der indeholder data fra databasen.
                                // Du kan arbejde med dette objekt dynamisk.
                                Console.WriteLine($"Name: {dynamicObject.Name}, Age: {dynamicObject.Age}");
                                Company.Add(dynamicObject);
                            }
                        }
                        Console.WriteLine("-------------------------------------------------------------");
                        count++;

                    }
                }
            }


            Console.WriteLine("Leder efter gentagelser:");

            var gentagelser = Company.Except(Person);

            foreach (var item in gentagelser)
            {
                Console.WriteLine($"{item}");
            }

            Console.ReadKey();
            #endregion

            #region HentFraCSV&IndsætDatabase
            //Generic generic = new Generic();

            //string pathCompany = @"C:\Users\shk165\Data\Company.csv";
            //string pathPeople = @"C:\Users\shk165\Data\PeopleCSV.csv";

            ////T bliver sat herude. Dvs. når vi er i metoden LoadFromTextFile, er T = PersonModel
            //var newPeople = generic.LoadFromCSVFile<PersonModel>(pathPeople);
            //var newCompany = generic.LoadFromCSVFile<CompanyModel>(pathCompany);

            //Console.WriteLine("Færdig med at loade data.");
            //Console.ReadKey();

            //var dataSuccesP = generic.InsertIntoDatabase<PersonModel>(newPeople);

            //if (!dataSuccesP) 
            //    throw new Exception("Noget gik galt med at indsætte People.");

            //var dataSuccesC = generic.InsertIntoDatabase<CompanyModel>(newCompany);

            //if (!dataSuccesC) 
            //    throw new Exception("Noget gik galt med at indsætte Company.");

            #endregion

            #region førsteGeneric
            //var car = new CarModel();
            //var owner = new OwnerModel();

            //var carList = new List<CarModel>();
            //var ownerList = new List<OwnerModel>();

            //var car1 = car.CreateCar(1, "Ford", "CA 63 626");
            //var car2 = car.CreateCar(2, "Renault", "AB 12 345");

            //carList.Add(car1);
            //carList.Add(car2);

            //Generic<CarModel> generic = new Generic<CarModel>();

            //var resultCar = generic.ShowData<CarModel>(carList);

            //foreach (var item in resultCar)
            //{
            //    Console.WriteLine($"Id: {item.Id}. Car name: {item.Name}. Car licenseplate {item.LicensePlate}");
            //}

            //Console.WriteLine("Klar til næste?");
            //Console.ReadKey();

            //var owner1 = owner.CreateOwner(1, "Mathias", 22);
            //var owner2 = owner.CreateOwner(2, "Amalie", 21);

            //ownerList.Add(owner1);
            //ownerList.Add(owner2);

            //Generic<OwnerModel> generic2 = new Generic<OwnerModel>();

            //var resultOwner = generic2.ShowData<OwnerModel>(ownerList);

            //foreach (var item in resultOwner)
            //{
            //    Console.WriteLine($"Id: {item.Id}. Owner name: {item.Name}. Owner age {item.Age}");
            //}

            //Console.ReadKey();

            #endregion
        }
    }


}

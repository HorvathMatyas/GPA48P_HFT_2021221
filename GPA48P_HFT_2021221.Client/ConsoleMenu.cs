using System;
using System.Collections.Generic;
using GPA48P_HFT_2021221.Models;

namespace GPA48P_HFT_2021221.Client
{
    public class ConsoleMenu
    {
        public static void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("|---------------------------------------------------|");
            Console.WriteLine("| Choose an option:                                 |");
            Console.WriteLine("| 1) Create a new animal shelter                    |");
            Console.WriteLine("| 2) Create a new owner                             |");
            Console.WriteLine("| 3) Create a new pet                               |");
            Console.WriteLine("| 4) Read an existing animal shelter by ID          |");
            Console.WriteLine("| 5) Read an existing owner by ID                   |");
            Console.WriteLine("| 6) Read an existing pet by ID                     |");
            Console.WriteLine("| 7) Update an existing animal shelter              |");
            Console.WriteLine("| 8) Update an existing owner                       |");
            Console.WriteLine("| 9) Update an existing pet                         |");
            Console.WriteLine("| 10) Delete an existing animal shelter             |");
            Console.WriteLine("| 11) Delete an existing owner                      |");
            Console.WriteLine("| 12) Delete an existing pet                        |");
            Console.WriteLine("| 13) View the pets avarage age in a shelter by ID  |");
            Console.WriteLine("| 14) View the dogs avarage age at all shelters     |");
            Console.WriteLine("| 15) View how much dogs have an owner by ID        |");
            Console.WriteLine("| 16) View which owner adopted the most cats        |");
            Console.WriteLine("| 17) View which owners adopted pet before 2015     |");
            Console.WriteLine("| 18) View how much the avarage age of all pets     |");
            Console.WriteLine("| X) Exit                                           |");
            Console.WriteLine("|---------------------------------------------------|");
            Console.Write("\r\nSelect an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    CreateAnimalShelter();
                    break;
                case "2":
                    CreateOwner();
                    break;
                case "3":
                    CreatePet();
                    break;
                case "4":
                    ReadAnimalShelter();
                    break;
                case "5":
                    ReadOwner();
                    break;
                case "6":
                    ReadPet();
                    break;
                case "7":
                    UpdateAnimalShelter();
                    break;
                case "8":
                    UpdateOwner();
                    break;
                case "9":
                    UpdatePet();
                    break;
                case "10":
                    DeleteAnimalShelter();
                    break;
                case "11":
                    DeleteOwner();
                    break;
                case "12":
                    DeletePet();
                    break;
                case "13":
                    AvarageAgeByPetsAtOneShelter();
                    break;
                case "14":
                    AvarageAgeOfDogsAtAllShelters();
                    break;
                case "15":
                    DogsOfOwner();
                    break;
                case "16":
                    MostCatsAdoptedBy();
                    break;
                case "17":
                    WhichOwnersAdoptedPetBefore2015();
                    break;
                case "18":
                    AvarageAgeOfPets();
                    break;
                case "X":
                    Exit();
                    break;
                default:
                    break;
            }
        }

        public static void CreateAnimalShelter()
        {
            Console.Clear();
            RestService rest = new RestService("http://localhost:62480");

            Console.Write("New animal shelter name: ");
            string shelterName = Console.ReadLine();
            Console.Write("New animal shelter address: ");
            string address = Console.ReadLine();
            Console.Write("New animal shelter phone number: ");
            string phoneNumber = Console.ReadLine();
            Console.Write("New animal shelter tax number: ");
            string taxNumber = Console.ReadLine();

            rest.Post(new AnimalShelter
            {
                ShelterName = shelterName,
                Address = address,
                PhoneNumber = phoneNumber,
                TaxNumber = taxNumber
            }, "animalshelter");

            Console.WriteLine();
            Console.WriteLine("Animal shelter created successfully");
            Console.WriteLine("Press enter to jump back to main menu");
            Console.ReadLine();

            MainMenu();
        }

        public static void CreateOwner()
        {
            Console.Clear();
            RestService rest = new RestService("http://localhost:62480");

            Console.Write("New owner first name: ");
            string firstName = Console.ReadLine();
            Console.Write("New owner last name: ");
            string lastName = Console.ReadLine();
            Console.Write("New owner address: ");
            string address = Console.ReadLine();
            Console.Write("New owner phone number: ");
            string phoneNumber = Console.ReadLine();
            Console.Write("New owner age: ");
            int age = int.Parse(Console.ReadLine());

            rest.Post(new Owner
            {
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                PhoneNumber = phoneNumber,
                Age = age
            }, "owner");

            Console.WriteLine();
            Console.WriteLine("Owner created successfully");
            Console.WriteLine("Press enter to jump back to main menu");
            Console.ReadLine();

            MainMenu();
        }

        public static void CreatePet()
        {
            Console.Clear();
            RestService rest = new RestService("http://localhost:62480");

            Console.Write("New pet class: ");
            string cLass = Console.ReadLine();
            Console.Write("New pet type: ");
            string type = Console.ReadLine();
            Console.Write("New pet age: ");
            int age = int.Parse(Console.ReadLine());
            Console.Write("New pet reception date: ");
            int adoptionYear = int.Parse(Console.ReadLine());
            Console.Write("New pet's owner ID: ");
            int ownerId = int.Parse(Console.ReadLine());
            Console.Write("New pet's animal shelter ID: ");
            int shelterId = int.Parse(Console.ReadLine());

            rest.Post(new Pet
            {
                Class = cLass,
                Type = type,
                Age = age,
                AdoptionYear = adoptionYear,
                OwnerId = ownerId,
                ShelterId = shelterId
            }, "pet");

            Console.WriteLine();
            Console.WriteLine("Pet created successfully");
            Console.WriteLine("Press enter to jump back to main menu");
            Console.ReadLine();

            MainMenu();
        }

        public static void ReadAnimalShelter()
        {
            Console.Clear();
            RestService rest = new RestService("http://localhost:62480");

            Console.Write("ID of the animal shelter: ");
            int shelterId = int.Parse(Console.ReadLine());

            AnimalShelter animalShelter = rest.Get<AnimalShelter>(shelterId, "animalshelter");

            Console.WriteLine();
            Console.WriteLine("Name: " + animalShelter.ShelterName);
            Console.WriteLine("Address: " + animalShelter.Address);
            Console.WriteLine("Phone number: " + animalShelter.PhoneNumber);
            Console.WriteLine("Tax number: " + animalShelter.TaxNumber);

            Console.WriteLine();
            Console.WriteLine("Press enter to jump back to main menu");
            Console.ReadLine();

            MainMenu();
        }

        public static void ReadOwner()
        {
            Console.Clear();
            RestService rest = new RestService("http://localhost:62480");

            Console.Write("ID of the owner: ");
            int ownerId = int.Parse(Console.ReadLine());

            Owner owner = rest.Get<Owner>(ownerId, "owner");

            Console.WriteLine();
            Console.WriteLine("First name: " + owner.FirstName);
            Console.WriteLine("Last name: " + owner.LastName);
            Console.WriteLine("Address: " + owner.Address);
            Console.WriteLine("Phone number: " + owner.PhoneNumber);
            Console.WriteLine("Age: " + owner.Age);

            Console.WriteLine();
            Console.WriteLine("Press enter to jump back to main menu");
            Console.ReadLine();

            MainMenu();
        }

        public static void ReadPet()
        {
            Console.Clear();
            RestService rest = new RestService("http://localhost:62480");

            Console.Write("ID of the pet: ");
            int petId = int.Parse(Console.ReadLine());

            Pet pet = rest.Get<Pet>(petId, "pet");

            Console.WriteLine();
            Console.WriteLine("Class: " + pet.Class);
            Console.WriteLine("Type: " + pet.Type);
            Console.WriteLine("Age: " + pet.Age);
            Console.WriteLine("Reception date: " + pet.AdoptionYear);
            Console.WriteLine("Owner ID: " + pet.OwnerId);
            Console.WriteLine("Animal shelter ID: " + pet.ShelterId);

            Console.WriteLine();
            Console.WriteLine("Press enter to jump back to main menu");
            Console.ReadLine();

            MainMenu();
        }

        public static void UpdateAnimalShelter()
        {
            Console.Clear();
            RestService rest = new RestService("http://localhost:62480");

            Console.Write("ID of the animal shelter: ");
            int shelterId = int.Parse(Console.ReadLine());

            Console.WriteLine();
            Console.Write("New animal shelter name: ");
            string shelterName = Console.ReadLine();
            Console.Write("New animal shelter address: ");
            string address = Console.ReadLine();
            Console.Write("New animal shelter phone number: ");
            string phoneNumber = Console.ReadLine();
            Console.Write("New animal shelter tax number: ");
            string taxNumber = Console.ReadLine();

            rest.Put(new AnimalShelter
            {
                ShelterId = shelterId,
                ShelterName = shelterName,
                Address = address,
                PhoneNumber = phoneNumber,
                TaxNumber = taxNumber
            }, "animalshelter");

            Console.WriteLine();
            Console.WriteLine("Animal shelter updated successfully");
            Console.WriteLine("Press enter to jump back to main menu");
            Console.ReadLine();

            MainMenu();
        }

        public static void UpdateOwner()
        {
            Console.Clear();
            RestService rest = new RestService("http://localhost:62480");

            Console.Write("ID of the owner: ");
            int ownerId = int.Parse(Console.ReadLine());

            Console.WriteLine();
            Console.Write("New owner first name: ");
            string firstName = Console.ReadLine();
            Console.Write("New owner last name: ");
            string lastName = Console.ReadLine();
            Console.Write("New owner address: ");
            string address = Console.ReadLine();
            Console.Write("New owner phone number: ");
            string phoneNumber = Console.ReadLine();
            Console.Write("New owner age: ");
            int age = int.Parse(Console.ReadLine());

            rest.Put(new Owner
            {
                OwnerId = ownerId,
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                PhoneNumber = phoneNumber,
                Age = age,

            }, "owner");

            Console.WriteLine();
            Console.WriteLine("Owner created successfully");
            Console.WriteLine("Press enter to jump back to main menu");
            Console.ReadLine();

            MainMenu();
        }

        public static void UpdatePet()
        {
            Console.Clear();
            RestService rest = new RestService("http://localhost:62480");

            Console.Write("ID of the pet: ");
            int petId = int.Parse(Console.ReadLine());

            Console.WriteLine();
            Console.Write("New pet class: ");
            string classy = Console.ReadLine();
            Console.Write("New pet type: ");
            string type = Console.ReadLine();
            Console.Write("New pet age: ");
            int age = int.Parse(Console.ReadLine());
            Console.Write("New pet adoption year: ");
            int adoptionYear = int.Parse(Console.ReadLine());
            Console.Write("New pet's owner ID: ");
            int ownerId = int.Parse(Console.ReadLine());
            Console.Write("New pet's animal shelter ID: ");
            int shelterId = int.Parse(Console.ReadLine());

            rest.Put(new Pet
            {
                PetId = petId,
                Class = classy,
                Type = type,
                Age = age,
                AdoptionYear = adoptionYear,
                OwnerId = ownerId,
                ShelterId = shelterId
            }, "pet");

            Console.WriteLine();
            Console.WriteLine("Pet updated successfully");
            Console.WriteLine("Press enter to jump back to main menu");
            Console.ReadLine();

            MainMenu();
        }

        public static void DeleteAnimalShelter()
        {
            Console.Clear();
            RestService rest = new RestService("http://localhost:62480");

            Console.Write("ID of the animal shelter: ");
            int shelterId = int.Parse(Console.ReadLine());

            rest.Delete(shelterId, "animalshelter");

            Console.WriteLine();
            Console.WriteLine("Animal shelter deleted successfully");
            Console.WriteLine("Press enter to jump back to main menu");
            Console.ReadLine();

            MainMenu();
        }

        public static void DeleteOwner()
        {
            Console.Clear();
            RestService rest = new RestService("http://localhost:62480");

            Console.Write("ID of the owner: ");
            int ownerId = int.Parse(Console.ReadLine());

            rest.Delete(ownerId, "owner");

            Console.WriteLine();
            Console.WriteLine("Owner deleted successfully");
            Console.WriteLine("Press enter to jump back to main menu");
            Console.ReadLine();

            MainMenu();
        }

        public static void DeletePet()
        {
            Console.Clear();
            RestService rest = new RestService("http://localhost:62480");

            Console.Write("ID of the pet: ");
            int petId = int.Parse(Console.ReadLine());

            rest.Delete(petId, "pet");

            Console.WriteLine();
            Console.WriteLine("Pet deleted successfully");
            Console.WriteLine("Press enter to jump back to main menu");
            Console.ReadLine();

            MainMenu();
        }

        public static void AvarageAgeByPetsAtOneShelter()
        {
            Console.Clear();
            RestService rest = new RestService("http://localhost:62480");

            Console.Write("Enter animal shelter ID: ");
            int shelterId = int.Parse(Console.ReadLine());
            var result = rest.GetSingle<double>($"/stat/avarageagebypetsatoneshelter/{shelterId}");

            Console.WriteLine();
            Console.WriteLine("The pets avarage age in a shelter:");
            Console.WriteLine(result);

            Console.WriteLine();
            Console.WriteLine("Press enter to jump back to main menu");
            Console.ReadLine();

            MainMenu();
        }

        public static void AvarageAgeOfDogsAtAllShelters()
        {
            Console.Clear();
            RestService rest = new RestService("http://localhost:62480");

            var result = rest.Get<AvarageAgeOfDogsAtAllShelters>("/stat/avarageageofdogsatallshelters");

            Console.WriteLine("Avarage age of dogs at all shelters:");
            result.ForEach(x => Console.WriteLine(x.ShelterName + "\t" + x.AvarageAge));

            Console.WriteLine();
            Console.WriteLine("Press enter to jump back to main menu");
            Console.ReadLine();

            MainMenu();
        }

        public static void DogsOfOwner()
        {
            Console.Clear();
            RestService rest = new RestService("http://localhost:62480");

            Console.Write("Enter owner ID: ");
            int ownerId = int.Parse(Console.ReadLine());
            var result = rest.Get<Pet>($"/stat/dogsofowner/{ownerId}");

            Console.WriteLine();
            Console.WriteLine("List of the dogs type:");
            result.ForEach(x => Console.WriteLine("-> " + x.Type));

            Console.WriteLine();
            Console.WriteLine("Press enter to jump back to main menu");
            Console.ReadLine();

            MainMenu();
        }

        public static void MostCatsAdoptedBy()
        {
            Console.Clear();
            RestService rest = new RestService("http://localhost:62480");

            var result = rest.GetSingle<Owner>("/stat/mostcatsadoptedby");

            Console.WriteLine("The owner who adopted the most cats:");
            Console.WriteLine(result.LastName + " " + result.FirstName);

            Console.WriteLine();
            Console.WriteLine("Press enter to jump back to main menu");
            Console.ReadLine();

            MainMenu();
        }

        public static void WhichOwnersAdoptedPetBefore2015()
        {
            Console.Clear();
            RestService rest = new RestService("http://localhost:62480");

            List<string> result = rest.Get<string>("/stat/whichownersadoptedpetbefore2015");

            Console.WriteLine("Owners who adopted pets before 2015:");
            foreach (string item in result)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine();
            Console.WriteLine("Press enter to jump back to main menu");
            Console.ReadLine();

            MainMenu();
        }

        public static void AvarageAgeOfPets()
        {
            Console.Clear();
            RestService rest = new RestService("http://localhost:62480");

            var result = rest.GetSingle<double>("/stat/avarageageofpets");

            Console.WriteLine("Avarage age of all pets:");
            Console.WriteLine(result);

            Console.WriteLine();
            Console.WriteLine("Press enter to jump back to main menu");
            Console.ReadLine();

            MainMenu();
        }

        public static void Exit()
        {
            Environment.Exit(0);
        }
    }
}

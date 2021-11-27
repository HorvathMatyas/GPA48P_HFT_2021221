﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GPA48P_HFT_2021221.Data;
using GPA48P_HFT_2021221.Models;

namespace GPA48P_HFT_2021221.Repository
{
    public class AnimalShelterRepository : IAnimalShelterRepository
    {
        AnimalShelterDbContext dataBase;

        public AnimalShelterRepository(AnimalShelterDbContext dataBase)
        {
            this.dataBase = dataBase;
        }

        public void Create(AnimalShelter animalShelter)
        {
            dataBase.AnimalShelters.Add(animalShelter);
            dataBase.SaveChanges();
        }

        public void Delete(int shelterId)
        {
            var animalShelterToDelete = Read(shelterId);
            dataBase.AnimalShelters.Remove(animalShelterToDelete);
            dataBase.SaveChanges();
        }

        public AnimalShelter Read(int shelterId)
        {
            return dataBase.AnimalShelters.FirstOrDefault(a => a.ShelterId == shelterId);
        }

        public IQueryable<AnimalShelter> GetAll()
        {
            return dataBase.AnimalShelters;
        }

        public void Update(AnimalShelter animalShelter)
        {
            var oldAnimalShelter = Read(animalShelter.ShelterId);
            oldAnimalShelter.ShelterId = animalShelter.ShelterId;
            oldAnimalShelter.SheltertName = animalShelter.SheltertName;
            oldAnimalShelter.Address = animalShelter.Address;
            oldAnimalShelter.PhoneNumber = animalShelter.PhoneNumber;
            oldAnimalShelter.TaxNumber = animalShelter.TaxNumber;
            dataBase.SaveChanges();
        }
    }
}

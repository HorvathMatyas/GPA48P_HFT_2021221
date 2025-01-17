﻿using System.Collections.Generic;
using GPA48P_HFT_2021221.Models;

namespace GPA48P_HFT_2021221.Logic
{
    public interface IAnimalShelterLogic
    {
        void Create(AnimalShelter animalShelter);

        void Delete(int shelterId);

        AnimalShelter Read(int shelterId);

        IEnumerable<AnimalShelter> GetAll();

        void Update(AnimalShelter animalShelter);

        double AvarageAgeByPetsAtOneShelter(int shelterId);

        IEnumerable<AvarageAgeOfDogsAtAllShelters> AvarageAgeOfDogsAtAllShelters();
    }
}

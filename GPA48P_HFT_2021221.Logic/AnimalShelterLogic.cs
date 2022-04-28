using System;
using System.Collections.Generic;
using System.Linq;
using GPA48P_HFT_2021221.Models;
using GPA48P_HFT_2021221.Repository;

namespace GPA48P_HFT_2021221.Logic
{
    public class AnimalShelterLogic : IAnimalShelterLogic
    {
        IAnimalShelterRepository animalShelterRepository;

        public AnimalShelterLogic(IAnimalShelterRepository animalShelterRepository)
        {
            this.animalShelterRepository = animalShelterRepository;
        }

        public void Create(AnimalShelter animalShelter)
        {
            if (animalShelter.ShelterName is null || animalShelter.ShelterName == "")
            {
                throw new Exception("A menhely neve nem lehet üres!");
            }
            else if (animalShelter.ShelterName.Length < 5)
            {
                throw new Exception("A menhely neve legalább 5 karakter hosszúságú legyen!");
            }
            else if (animalShelter.Address is null || animalShelter.Address == "")
            {
                throw new Exception("A menhely címe nem lehet üres!");
            }
            else if (animalShelter.PhoneNumber is null || animalShelter.PhoneNumber == "")
            {
                throw new Exception("A menhely telefonszáma nem lehet üres!");
            }
            else if (animalShelter.TaxNumber is null || animalShelter.TaxNumber == "")
            {
                throw new Exception("A menhely adószáma nem lehet üres!");
            }
            animalShelterRepository.Create(animalShelter);
        }

        public void Delete(int shelterId)
        {
            animalShelterRepository.Delete(shelterId);
        }

        public AnimalShelter Read(int shelterId)
        {
            return animalShelterRepository.Read(shelterId);
        }

        public IEnumerable<AnimalShelter> GetAll()
        {
            return animalShelterRepository.GetAll();
        }

        public void Update(AnimalShelter animalShelter)
        {
            animalShelterRepository.Update(animalShelter);
        }

        // Megadott menhely ID alapján nézzük meg a kisállatok átlagéletkorát!
        public double AvarageAgeByPetsAtOneShelter(int shelterId)
        {
            var result = animalShelterRepository.GetAll()
                                                .SingleOrDefault(x => x.ShelterId
                                                    .Equals(shelterId))
                                                .Pets.Average(x => x.Age);
            return result;
        }

        // Nézzük meg, hogy menhelyenként mennyi a kutyák átlagéletkora!
        public IEnumerable<AvarageAgeOfDogsAtAllShelters> AvarageAgeOfDogsAtAllShelters()
        {
            var result = animalShelterRepository.GetAll()
                                                .Select(x =>
                                                    new AvarageAgeOfDogsAtAllShelters
                                                    {
                                                        ShelterName = x.ShelterName,
                                                        AvarageAge = x.Pets.Where(y => y.Class
                                                            .Equals("Kutya")).Count()==0 ?0:   // ha 0, akkor nincs kutya, egyébként tovább
                                                        x.Pets.Where(y => y.Class
                                                            .Equals("Kutya")).Average(y => y.Age)
                                                    });
            return result;
        }
    }
}

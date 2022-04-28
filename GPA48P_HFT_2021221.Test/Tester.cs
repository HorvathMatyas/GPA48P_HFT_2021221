using System;
using System.Collections.Generic;
using System.Linq;
using GPA48P_HFT_2021221.Logic;
using GPA48P_HFT_2021221.Models;
using GPA48P_HFT_2021221.Repository;
using NUnit.Framework;
using Moq;

namespace GPA48P_HFT_2021221.Test
{
    [TestFixture]
    public class Tester
    {
        AnimalShelterLogic animalShelterLogic;
        OwnerLogic ownerLogic;
        PetLogic petLogic;

        IEnumerable<AvarageAgeOfDogsAtAllShelters> AvarageAgeOfDogsAtAllSheltersList;

        [SetUp]
        public void Init()
        {
            var mockAnimalShelterRepository = new Mock<IAnimalShelterRepository>();
            var mockOwnerRepository = new Mock<IOwnerRepository>();
            var mockPetRepsitory = new Mock<IPetRepository>();

            var pets = new List<Pet>()
            {
                new Pet()
                {
                    PetId = 1,
                    Class = "Kutya",
                    Type = "Labrador",
                    Age = 2,
                    AdoptionYear = 2019,
                    ShelterId = 1,
                    OwnerId = 1
                },
                new Pet()
                {
                    PetId = 2,
                    Class = "Kutya",
                    Type = "Border collie",
                    Age = 4,
                    AdoptionYear = 2018,
                    ShelterId = 1,
                    OwnerId = 1
                },
                new Pet()
                {
                    PetId = 3,
                    Class = "Kutya",
                    Type = "Beagle",
                    Age = 6,
                    AdoptionYear = 2017,
                    ShelterId = 1,
                    OwnerId = 1
                },
                new Pet()
                {
                    PetId = 4,
                    Class = "Macska",
                    Type = "Perzsa",
                    Age = 8,
                    AdoptionYear = 2016,
                    ShelterId = 2,
                    OwnerId = 2
                },
                new Pet()
                {
                    PetId = 5,
                    Class = "Macska",
                    Type = "Bengáli",
                    Age = 10,
                    AdoptionYear = 2014,
                    ShelterId = 3,
                    OwnerId = 3
                },
                new Pet()
                {
                    PetId = 6,
                    Class = "Macska",
                    Type = "Szfinx",
                    Age = 12,
                    AdoptionYear = 2013,
                    ShelterId = 2,
                    OwnerId = 2
                }
            }.AsQueryable();

            var animalShelters = new List<AnimalShelter>()
            {
                new AnimalShelter()
                {
                    ShelterId = 1,
                    ShelterName = "Menhely1",
                    Address = "New York",
                    PhoneNumber = "06709871234",
                    TaxNumber = "18942873562",
                    Pets = pets.Where(x => x.ShelterId.Equals(1)).ToList()
                },
                new AnimalShelter()
                {
                    ShelterId = 2,
                    ShelterName = "Menhely2",
                    Address = "Tokió",
                    PhoneNumber = "06209128456",
                    TaxNumber = "19371496731",
                    Pets = pets.Where(x => x.ShelterId.Equals(2)).ToList()
                },
                new AnimalShelter()
                {
                    ShelterId = 3,
                    ShelterName = "Menhely3",
                    Address = "Peking",
                    PhoneNumber = "06306541298",
                    TaxNumber = "19285239871",
                    Pets = pets.Where(x => x.ShelterId.Equals(3)).ToList()
                }
            }.AsQueryable();

            var owners = new List<Owner>()
            {
                new Owner()
                {
                    OwnerId = 1,
                    FirstName = "János",
                    LastName = "Fekete",
                    Address = "London",
                    PhoneNumber = "06201239876",
                    Age = 10,
                    Pets = pets.Where(x => x.ShelterId.Equals(1)).ToList()
                },
                new Owner()
                {
                    OwnerId = 2,
                    FirstName = "Dániel",
                    LastName = "Borzavári",
                    Address = "Los Angeles",
                    PhoneNumber = "06304561298",
                    Age = 20,
                    Pets = pets.Where(x => x.ShelterId.Equals(2)).ToList()
                },
                new Owner()
                {
                    OwnerId = 3,
                    FirstName = "Norbert",
                    LastName = "Tóth",
                    Address = "Párizs",
                    PhoneNumber = "06709871234",
                    Age = 30,
                    Pets = pets.Where(x => x.ShelterId.Equals(3)).ToList()
                }
            }.AsQueryable();

            AvarageAgeOfDogsAtAllSheltersList = new List<AvarageAgeOfDogsAtAllShelters>()
            {
                new AvarageAgeOfDogsAtAllShelters
                {
                    ShelterName = "Menhely1", AvarageAge = 4
                },
                new AvarageAgeOfDogsAtAllShelters
                {
                    ShelterName = "Menhely2", AvarageAge = 0
                },
                new AvarageAgeOfDogsAtAllShelters
                {
                    ShelterName = "Menhely3", AvarageAge = 0
                }
            };

            mockAnimalShelterRepository.Setup((x) => x.GetAll()).Returns(animalShelters);
            mockOwnerRepository.Setup((x) => x.GetAll()).Returns(owners);
            mockPetRepsitory.Setup((x) => x.GetAll()).Returns(pets);

            animalShelterLogic = new AnimalShelterLogic(mockAnimalShelterRepository.Object);
            ownerLogic = new OwnerLogic(mockOwnerRepository.Object);
            petLogic = new PetLogic(mockPetRepsitory.Object,mockOwnerRepository.Object);
        }

        // Teszt 1
        // Megadott menhely ID alapján nézzük meg a kisállatok átlagéletkorát!
        [Test]
        public void AvarageAgeByPetsAtOneShelterTest()
        {
            // ACT
            double result = animalShelterLogic.AvarageAgeByPetsAtOneShelter(1);

            // ASSERT
            Assert.That(result, Is.EqualTo(4));
        }

        // Teszt 2
        // // Nézzük meg, hogy menhelyenként mennyi a kutyák átlagéletkora!
        [Test]
        public void AvarageAgeOfDogsAtAllShelters()
        {
            // ACT
            var result = animalShelterLogic.AvarageAgeOfDogsAtAllShelters().ToList();

            // ASSERT
            Assert.IsTrue(CompareElement(result, AvarageAgeOfDogsAtAllSheltersList));
        }

        // Menhelyek és kutya átlagéletkor összehasonlító segédmetódus teszthez
        private bool CompareElement(IEnumerable<AvarageAgeOfDogsAtAllShelters> e, IEnumerable<AvarageAgeOfDogsAtAllShelters> l)
        {
            if (e.Count() != l.Count())
            {
                return false;
            }
            else if (e.Count() > 0)
            {
                foreach (var item in e)
                {
                    if (!l.Any(x => x.AvarageAge == item.AvarageAge && x.ShelterName == item.ShelterName))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        // Teszt 3
        // Megadott gazdi ID alapján nézzük meg, milyen kutyái vannak!
        [Test]
        public void DogsOfOwnerTest()
        {
            // ACT
            var result = ownerLogic.DogsOfOwner(2);

            // ASSERT
            Assert.That(result.Count(), Is.EqualTo(0));
        }

        // Teszt 4
        // Nézzük meg melyik gazdi fogadta örökbe a legtöbb macskát!
        [Test]
        public void MostCatsAdoptedByTest()
        {
            // ACT
            var result = ownerLogic.MostCatsAdoptedBy();

            // ASSERT
            Assert.That(result.OwnerId, Is.EqualTo(2));
        }

        // Teszt 5
        // // Nézzük meg, hogy mennyi az állatok átlagéletkora!
        [Test]
        public void AvarageAgeTest()
        {
            // ACT
            double result = petLogic.AvarageAgeOfPets();

            // ASSERT
            Assert.That(result, Is.EqualTo(7));
        }

        // Teszt 6
        // Nézzük meg mely gazdik fogadtak örökbe 2015 előtt kisállatot!
        [Test]
        public void WhichOwnersAdoptedPetBefore2015Test()
        {
            // ACT
            

            // ASSERT
            var mockOwnerRepository = new Mock<IOwnerRepository>();
            var mockPetRepository = new Mock<IPetRepository>();

            int id = 1;

            Owner owner = new Owner()
            {
                OwnerId = 1,
                FirstName = "Bence",
                LastName = "Fekete",
                Address = "asdasdasd",
                PhoneNumber = "06308769123",
                Age = 42
            };

            Pet pet = new Pet()
            {
                PetId = 1,
                Class = "asdasd",
                Type = "asdasd",
                Age = 8,
                AdoptionYear = 2013,
                ShelterId = 0,
                OwnerId = 1
            };

            List<Pet> petList = new List<Pet>();
            petList.Add(pet);

            mockOwnerRepository.Setup(x => x.Read(id)).Returns(owner);
            mockPetRepository.Setup(x => x.GetAll()).Returns(petList.AsQueryable());

            petLogic = new PetLogic(mockPetRepository.Object, mockOwnerRepository.Object);

            var result = petLogic.WhichOwnersAdoptedPetBefore2015();

            string name1 = "Fekete Bence";

            Assert.AreEqual(result.First(), name1);
        }

        // Teszt 7 - Create crud metódus tesztelés
        // Gazdi keresztneve lehet üres string? Nem!
        // Zöld: dob exceptiont || Piros: nem dob exceptiont
        [Test]
        public void CrudCreateOwnerFirstNameTest()
        {
            // ASSERT
            Assert.Throws<Exception>(() => ownerLogic.Create(new Owner
            {
                FirstName = "",
                LastName = "Horváth",
                Address = "Budapest Fehér utca 11",
                PhoneNumber = "06309876123",
                Age = 32
            }));
        }

        // Teszt 8 - Create crud metódus tesztelés
        // Gazdi vezetékneve lehet 2 karakter hosszúságú? Nem!
        // Zöld: dob exceptiont || Piros: nem dob exceptiont
        [Test]
        public void CrudCreateOwnerLastNameTest()
        {
            // ASSERT
            Assert.Throws<Exception>(() => ownerLogic.Create(new Owner
            {
                FirstName = "József",
                LastName = "Ok",
                Address = "Budapest Fehér utca 11",
                PhoneNumber = "06309876123",
                Age = 32
            }));
        }

        // Teszt 9 - Create crud metódus tesztelés
        // Kiskorú fogadhat örökbe kisállatot? Nem!
        // Zöld: dob exceptiont || Piros: nem dob exceptiont
        [Test]
        public void CrudCreateOwnerAgeTest()
        {
            // ASSERT
            Assert.Throws<Exception>(() => ownerLogic.Create(new Owner
            {
                FirstName = "József",
                LastName = "Horváth",
                Address = "Budapest Fehér utca 11",
                PhoneNumber = "06309876123",
                Age = 17
            }));
        }

        // Teszt 10 - Create crud metódus tesztelés
        // Lehet a menhely adószáma semmi? Nem!
        // Zöld: dob exceptiont || Piros: nem dob exceptiont
        [Test]
        public void CrudCreateAnimalShelterTaxNumberTest()
        {
            // ASSERT
            Assert.Throws<Exception>(() => animalShelterLogic.Create(new AnimalShelter
            {
                ShelterName = "Menhely",
                Address = "Budapest Fehér utca 12",
                PhoneNumber = "06308769321",
                TaxNumber = ""
            }));
        }
    }
}

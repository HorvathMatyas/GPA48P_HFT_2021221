using GPA48P_HFT_2021221.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace GPA48P_HFT_2021221.Data
{
    public class AnimalShelterDbContext : DbContext
    {
        public virtual DbSet<Owner> Owners { get; set; }
        public virtual DbSet<AnimalShelter> AnimalShelters { get; set; }
        public virtual DbSet<Pet> Pets { get; set; }

        public AnimalShelterDbContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                // a mindenkori munkakönyvtárban lévő lokális adatbázis fájlhoz kapcsolódik
                string connection = @"Data Source=(LocalDB)\MSSQLLocalDB;
                                    AttachDbFilename=|DataDirectory|\Database1.mdf;
                                    Integrated Security=True";
                builder.UseLazyLoadingProxies().UseSqlServer(connection);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Owner>(entity =>
            {
                entity
                .HasMany(owner => owner.Pets)
                .WithOne(pet => pet.Owner)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<AnimalShelter>(entity =>
            {
                entity
                .HasMany(shelter => shelter.Pets)
                .WithOne(pet => pet.AnimalShelter)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Pet>(entity =>
            {
                entity
                .HasOne(pet => pet.Owner)
                .WithMany(owner => owner.Pets)
                .HasForeignKey(pet => pet.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

                entity
                .HasOne(pet => pet.AnimalShelter)
                .WithMany(shelter => shelter.Pets)
                .HasForeignKey(pet => pet.ShelterId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            //-----------
            //   Owners
            //-----------

            Owner owner1 = new Owner()
            {
                OwnerId = 1,
                FirstName = "János",
                LastName = "Lézer",
                Address = "Székesfehérvár, Fekete u. 1, 8000",
                PhoneNumber = "06202349876",
                Age = 18
            };

            Owner owner2 = new Owner()
            {
                OwnerId = 2,
                FirstName = "Béla",
                LastName = "Vas",
                Address = "Győr, Fehér u. 15, 9000",
                PhoneNumber = "06305331203",
                Age = 35
            };

            Owner owner3 = new Owner()
            {
                OwnerId = 3,
                FirstName = "Sándor",
                LastName = "Erős",
                Address = "Debrecen, Zöld u. 9, 4000",
                PhoneNumber = "06704339812",
                Age = 56
            };

            //---------------------
            //   Animal shelters
            //---------------------

            AnimalShelter shelter1 = new AnimalShelter()
            {
                ShelterId = 1,
                ShelterName = "Menhely Alapítvány",
                Address = "Budapest, Vajdahunyad u. 3, 1082",
                PhoneNumber = "06202349876",
                TaxNumber = "19013213142"
            };
            
            AnimalShelter shelter2 = new AnimalShelter()
            {
                ShelterId = 2,
                ShelterName = "Állatmentő Sereg",
                Address = "Budapest, Nagymező u. 8, 1065",
                PhoneNumber = "06304564321",
                TaxNumber = "18334461142"
            };

            AnimalShelter shelter3 = new AnimalShelter()
            {
                ShelterId = 3,
                ShelterName = "Cicamentő angyalok",
                Address = "Budapest, Lipót u. 12, 1074",
                PhoneNumber = "06709871234",
                TaxNumber = "17816385193"
            };

            //----------
            //   Pets
            //----------

            Pet pet1 = new Pet()
            {
                PetId = 1,
                Class = "Kutya",
                Type = "Terrier",
                Age = 4,
                AdoptionYear = 2007,
                OwnerId = owner1.OwnerId,
                ShelterId = shelter1.ShelterId
            };

            Pet pet2 = new Pet()
            {
                PetId = 2,
                Class = "Kutya",
                Type = "Border Collie",
                Age = 7,
                AdoptionYear = 2004,
                OwnerId = owner2.OwnerId,
                ShelterId = shelter2.ShelterId
            };

            Pet pet3 = new Pet()
            {
                PetId = 3,
                Class = "Macska",
                Type = "Ragdoll",
                Age = 2,
                AdoptionYear = 2019,
                OwnerId = owner1.OwnerId,
                ShelterId = shelter2.ShelterId
            };

            Pet pet4 = new Pet()
            {
                PetId = 4,
                Class = "Macska",
                Type = "Sziámi",
                Age = 11,
                AdoptionYear = 2010,
                OwnerId = owner2.OwnerId,
                ShelterId = shelter1.ShelterId
            };

            Pet pet5 = new Pet()
            {
                PetId = 5,
                Class = "Kutya",
                Type = "Bernáthegyi",
                Age = 1,
                AdoptionYear = 2020,
                OwnerId = owner3.OwnerId,
                ShelterId = shelter3.ShelterId
            };

            Pet pet6 = new Pet()
            {
                PetId = 6,
                Class = "Macska",
                Type = "Bengáli",
                Age = 3,
                AdoptionYear = 2018,
                OwnerId = owner1.OwnerId,
                ShelterId = shelter3.ShelterId
            };

            Pet pet7 = new Pet()
            {
                PetId = 7,
                Class = "Kutya",
                Type = "Rottweiler",
                Age = 7,
                AdoptionYear = 2014,
                OwnerId = owner2.OwnerId,
                ShelterId = shelter1.ShelterId
            };

            Pet pet8 = new Pet()
            {
                PetId = 8,
                Class = "Macska",
                Type = "Abesszin",
                Age = 1,
                AdoptionYear = 2020,
                OwnerId = owner1.OwnerId,
                ShelterId = shelter3.ShelterId
            };

            Pet pet9 = new Pet()
            {
                PetId = 9,
                Class = "Kutya",
                Type = "Beagle",
                Age = 10,
                AdoptionYear = 2011,
                OwnerId = owner2.OwnerId,
                ShelterId = shelter2.ShelterId
            };

            Pet pet10 = new Pet()
            {
                PetId = 10,
                Class = "Macska",
                Type = "Burma",
                Age = 5,
                AdoptionYear = 2016,
                OwnerId = owner3.OwnerId,
                ShelterId = shelter3.ShelterId
            };

            modelBuilder.Entity<Owner>().HasData(owner1, owner2, owner3);
            modelBuilder.Entity<AnimalShelter>().HasData(shelter1, shelter2, shelter3);
            modelBuilder.Entity<Pet>().HasData(pet1, pet2, pet3, pet4, pet5, pet6, pet7, pet8, pet9, pet10);
        }
    }
}

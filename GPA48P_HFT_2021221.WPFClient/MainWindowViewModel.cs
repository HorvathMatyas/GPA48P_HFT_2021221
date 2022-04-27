using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GPA48P_HFT_2021221.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace GPA48P_HFT_2021221.WPFClient
{
    public class MainWindowViewModel : ObservableRecipient
    {
        public RestCollection<AnimalShelter> AnimalShelters { get; set; }
        public RestCollection<Owner> Owners { get; set; }
        public RestCollection<Pet> Pets { get; set; }

        private AnimalShelter selectedAnimalShelter;
        private Owner selectedOwner;
        private Pet selectedPet;

        public AnimalShelter SelectedAnimalShelter
        {
            get { return selectedAnimalShelter; }
            set
            {
                if (value != null)
                {
                    selectedAnimalShelter = new AnimalShelter()
                    {
                        ShelterId = value.ShelterId,
                        SheltertName = value.SheltertName,
                        Address = value.Address,
                        PhoneNumber = value.PhoneNumber,
                        TaxNumber = value.TaxNumber
                };
                    OnPropertyChanged();
                    ((RelayCommand)DeleteAnimalShelterCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public Owner SelectedOwner
        {
            get { return selectedOwner; }
            set
            {
                if (value != null)
                {
                    selectedOwner = new Owner()
                    {
                        OwnerId = value.OwnerId,
                        FirstName = value.FirstName,
                        LastName = value.LastName,
                        Address = value.Address,
                        PhoneNumber = value.PhoneNumber,
                        Age = value.Age
                    };
                    OnPropertyChanged();
                    ((RelayCommand)DeleteOwnerCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public Pet SelectedPet
        {
            get { return selectedPet; }
            set
            {
                if (value != null)
                {
                    selectedPet = new Pet()
                    {
                        PetId = value.PetId,
                        Class = value.Class,
                        Type = value.Type,
                        Age = value.Age,
                        AdoptionYear = value.AdoptionYear,
                        ShelterId = value.ShelterId,
                        OwnerId = value.OwnerId
                    };
                    OnPropertyChanged();
                    ((RelayCommand)DeletePetCommand).NotifyCanExecuteChanged();
                }
            }
        }

        //----------
        // COMMANDS
        //----------

        public ICommand CreateAnimalShelterCommand { get; set; }
        public ICommand UpdateAnimalShelterCommand { get; set; }
        public ICommand DeleteAnimalShelterCommand { get; set; }

        public ICommand CreateOwnerCommand { get; set; }
        public ICommand UpdateOwnerCommand { get; set; }
        public ICommand DeleteOwnerCommand { get; set; }

        public ICommand CreatePetCommand { get; set; }
        public ICommand UpdatePetCommand { get; set; }
        public ICommand DeletePetCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public MainWindowViewModel()
        {
            if(!IsInDesignMode)
            {
                AnimalShelters = new RestCollection<AnimalShelter>("http://localhost:62480/animalShelter", "animalShelter", "hub");
                Owners = new RestCollection<Owner>("http://localhost:62480/owner", "owner", "hub");
                Pets = new RestCollection<Pet>("http://localhost:62480/pet", "pet", "hub");

                //-------------------------------
                // Animal Shelter Relay Commands
                //-------------------------------

                CreateAnimalShelterCommand = new RelayCommand(() =>
                {
                    AnimalShelters.Add(new AnimalShelter()
                    {
                        ShelterId = SelectedAnimalShelter.ShelterId,
                        SheltertName = SelectedAnimalShelter.SheltertName,
                        Address = SelectedAnimalShelter.Address,
                        PhoneNumber = SelectedAnimalShelter.PhoneNumber,
                        TaxNumber = SelectedAnimalShelter.TaxNumber
                    });
                });

                UpdateAnimalShelterCommand = new RelayCommand(() =>
                {
                    AnimalShelters.Update(SelectedAnimalShelter);
                });

                DeleteAnimalShelterCommand = new RelayCommand(() =>
                {
                    AnimalShelters.Delete(SelectedAnimalShelter.ShelterId);
                },
                () =>
                {
                    return SelectedAnimalShelter != null;
                });

                //----------------------
                // Owner Relay Commands
                //----------------------

                CreateOwnerCommand = new RelayCommand(() =>
                {
                    Owners.Add(new Owner()
                    {
                        OwnerId = SelectedOwner.OwnerId,
                        FirstName = SelectedOwner.FirstName,
                        LastName = SelectedOwner.LastName,
                        Address = SelectedOwner.Address,
                        PhoneNumber= SelectedOwner.PhoneNumber,
                        Age = SelectedOwner.Age
                    });
                });

                UpdateOwnerCommand = new RelayCommand(() =>
                {
                    Owners.Update(SelectedOwner);
                });

                DeleteOwnerCommand = new RelayCommand(() =>
                {
                    Owners.Delete(SelectedOwner.OwnerId);
                },
                () =>
                {
                    return SelectedOwner != null;
                });

                //--------------------
                // Pet Relay Commands
                //--------------------

                CreatePetCommand = new RelayCommand(() =>
                {
                    Pets.Add(new Pet()
                    {
                        PetId = SelectedPet.PetId,
                        Class = SelectedPet.Class,
                        Type = SelectedPet.Type,
                        Age = SelectedPet.Age,
                        AdoptionYear = SelectedPet.AdoptionYear,
                        ShelterId = SelectedPet.ShelterId,
                        OwnerId = SelectedPet.OwnerId
                    });
                });

                UpdatePetCommand = new RelayCommand(() =>
                {
                    Pets.Update(SelectedPet);
                });

                DeletePetCommand = new RelayCommand(() =>
                {
                    Pets.Delete(SelectedPet.PetId);
                },
                () =>
                {
                    return SelectedPet != null;
                });
            }
        }
    }
}

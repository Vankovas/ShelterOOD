using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelter
{
    public class Shelter : DataHelper, GUIHelper
    {
        //fields
        private string name;
        private string address;
        private string telNo;
        private string email;
        private List<Animal> animals;
        private List<Owner> owners;

        //constructor
        public Shelter(string name, string address, string telNo, string email)
        {
            this.name = name;
            this.address = address;
            this.telNo = telNo;
            this.email = email;

            animals = new List<Animal>();
            owners = new List<Owner>();
        }

        //methods
        private Owner getOwner(string cardId)
        {
            foreach (Owner owner in owners)
            {
                if (owner.CardId == cardId)
                {
                    return owner;
                }
            }

            return null;
        }

        private Animal getAnimal(RFIDTag rfidTag)
        {
            foreach (Animal animal in animals)
            {
                if (animal.RfidTag.Number == rfidTag.Number)
                {
                    return animal;
                }
            }

            return null;
        }

        /// <summary>
        /// Dummy ScanRFID method, because we don't actually use real RFIDs
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public RFIDTag ScanRFID(string number)
        {
            return new RFIDTag(number);
        }

        public bool saveOneOwner(Owner owner)
        {
            if (!owners.Contains(owner))
            {
                owners.Add(owner);
                return true;
            }
            return false;
        }

        public bool saveOneAnimal(Animal animal)
        {
            
            if (!animals.Contains(animal))
            {
                animals.Add(animal);
                return true;
            }
            return false;
        }

        public bool saveOwnersList(List<Owner> owners)
        {
            foreach (Owner o in owners)
            {
                if (owners.Contains(o))
                {
                    return false;
                }
                this.owners.Add(o);
            }
            return true;
        }

        public bool saveAnimalsList(List<Animal> animals)
        {
            foreach (Animal a in animals)
            {
                if (animals.Contains(a))
                {
                    return false;
                }
                this.animals.Add(a);
            }
            return true;
        }

        public List<Owner> loadOwners()
        {
            return owners;
        }

        public List<Animal> loadAnimals()
        {
            return animals;
        }

        public bool AddAnimal(Animal animal)
        {
            return this.saveOneAnimal(animal);
        }

        public bool AddOwner(Owner owner)
        {
            return this.saveOneOwner(owner);
        }

        public int AdoptAnimal(string cardId, RFIDTag rfid)
        {
            if(this.getAnimal(rfid) != null)
            {
                this.getOwner(cardId).Adopt(this.getAnimal(rfid));
                return this.getAnimal(rfid).CalculateAdoptionPay();
            }
            return -1;
        }

        public int ClaimAnimal(string cardId, RFIDTag rfid)
        {
            this.getOwner(cardId).Claim(this.getAnimal(rfid));

            return this.getAnimal(rfid).CalculateClaimPay();
        }

        public void WalkDog(RFIDTag rfid)
        {
            ((Dog)this.getAnimal(rfid)).Walk();
        }

        public void EditCatDesc(RFIDTag rfid, string desc)
        {
            if(!((Cat)getAnimal(rfid)).ExtraInfo.Equals(desc))
            {
                ((Cat)getAnimal(rfid)).changeExtra(desc);
            }
        }

        public string[] GenerateOverview()
        {
            string[] overview = new string[3];
            int adoptable = 0;

            for(int i = 0; i < this.GetAdoptableAnimals().Length; i++)
            {
                if(this.GetAdoptableAnimals()[i] != null)
                {
                    adoptable++;
                }
            }

            overview[0] = adoptable.ToString();
            overview[1] = this.animals.Count.ToString();
            overview[2] = this.owners.Count.ToString();

            return overview;
        }

        public string GetOwnerInfo(string cardId)
        {
            if(this.getOwner(cardId) != null)
            {
                return this.getOwner(cardId).ToString();
            }
            
            return null;
        }

        public string[] GetOwnersInfo()
        {
            string[] strOwners = new string[owners.Count];
            int i = 0;

            foreach (Owner o in owners)
            {
                strOwners[i] = o.ToString();
                i++;
            }

            return strOwners;
        }

        public string GetAnimalInfo(RFIDTag rfid)
        {
            if(this.getAnimal(rfid) != null)
            {
                return this.getAnimal(rfid).ToString();
            }

            return null;
        }

        public string[] GetAnimalsInfo()
        {
            string[] strAnimals = new string[animals.Count];
            int i = 0;

            foreach (Animal a in animals)
            {
                strAnimals[i] = a.ToString();
                i++;
            }

            return strAnimals;
        }

        public string[] GetAdoptableAnimals()
        {
            string[] strAnimals = new string[animals.Count];
            int i = 0;

            foreach (Animal a in animals)
            {
                if(a.IsAdoptable())
                {
                    strAnimals[i] = a.ToString();
                    i++;
                }
                
            }

            return strAnimals;
        }

        public string[] GetNonAdoptableAnimals()
        {
            string[] strAnimals = new string[animals.Count];
            int i = 0;

            foreach (Animal a in animals)
            {
                if (!a.IsAdoptable())
                {
                    strAnimals[i] = a.ToString();
                    i++;
                }

            }

            return strAnimals;
        }

        public string[] GetAnimalsByOwner(string cardId)
        {
            foreach(Owner o in owners)
            {
                if(o.CardId == cardId)
                {
                    return o.GetAnimals();
                }
            }

            return null;
        }

        public string GetOwnerByAnimal(RFIDTag rfid)
        {
            foreach(Animal a in animals)
            {
                if(a.RfidTag.Number == rfid.Number && a.LastOwner != null)
                {
                    return a.LastOwner.ToString();
                }
            }

            return null;
        }

        public bool BackToShelter(RFIDTag rfid)
        {
            return this.getAnimal(rfid).ReturnToShelter();
        }

        public void ChangeLocation(RFIDTag rfid, string location)
        {
            this.getAnimal(rfid).ChangeLocation(location);
        }
    }
}

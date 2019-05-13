using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelter
{
    interface GUIHelper
    {
        bool AddAnimal(Animal animal);

        bool AddOwner(Owner owner);

        int AdoptAnimal(string cardId, RFIDTag rfid);

        int ClaimAnimal(string cardId, RFIDTag rfid);

        void WalkDog(RFIDTag rfid);

        void EditCatDesc(RFIDTag rfid, string desc);

        string[] GenerateOverview();

        string GetOwnerInfo(string cardId);

        string[] GetOwnersInfo();

        string GetAnimalInfo(RFIDTag rfid);

        string[] GetAnimalsInfo();

        string[] GetAdoptableAnimals();

        string[] GetNonAdoptableAnimals();

        string[] GetAnimalsByOwner(string cardId);
    }
}

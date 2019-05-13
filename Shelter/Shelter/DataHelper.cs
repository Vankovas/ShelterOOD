using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelter
{
    interface DataHelper
    {
        bool saveOneOwner(Owner owner);

        bool saveOneAnimal(Animal animal);

        bool saveOwnersList(List<Owner> owners);

        bool saveAnimalsList(List<Animal> animals);

        List<Owner> loadOwners();

        List<Animal> loadAnimals();
    }
}

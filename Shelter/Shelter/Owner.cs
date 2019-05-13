using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelter
{
    public class Owner
    {
        //fields
        private string cardId;
        private string lastName;
        private List<Animal> animals;

        //properties
        public string CardId
        {
            get { return cardId; }
        }

        public List<Animal> Animals
        {
            get { return animals; }
        }

        //constructor
        public Owner(string cardId, string lastName)
        {
            this.cardId = cardId;
            this.lastName = lastName;

            animals = new List<Animal>();
        }

        //methods
        public void Adopt(Animal animal)
        {
            if (animal.IsAdoptable())
            {
                animal.ChangeOwner(this);
                animal.TakeFromShelter();
                animals.Add(animal);
            }
        }

        public void Claim(Animal animal)
        {
            if (animal.LastOwner == this)
            {
                animal.TakeFromShelter();
            }
        }

        public string[] GetAnimals()
        {
            string[] strAnimals = new string[animals.Count];
            int i = 0;

            foreach(Animal a in animals)
            {
                strAnimals[i] = a.ToString();
            }

            return strAnimals;
        }

        public override string ToString()
        {
            return $"Card ID: {cardId}, Last name: {lastName}";
        }
    }
}

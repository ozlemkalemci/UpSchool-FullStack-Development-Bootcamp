using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpSchool.Domain.Pattern.MementoPattern
{
    public class ConcreteMemento : IMemento
    {
        private string _state;


        public ConcreteMemento(string state)
        {
            this._state = state;
        }

        // The Originator uses this method when restoring its state.
        public string GetState()
        {
            return this._state;
        }

        public string GetName()
        {
            return $"{this._state}";
        }

        public override string ToString()
        {
            return this._state;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSchool.Domain.Dtos;
using UpSchool.Domain.Utilities;
using UpSchool.Wasm.Pattern.MementoPattern;

namespace UpSchool.Domain.Pattern.MementoPattern
{
    public class Originator
    {
        private string _state;

        public PasswordGenerator passwordGenerator = new PasswordGenerator();

        public GeneratePasswordDto generatePasswordDto = new GeneratePasswordDto();

        public Originator(string state)
        {
            this._state = state;
        }

        public string OriginatorGeneratePasswords()
        {
            this._state = this.passwordGenerator.Generate(generatePasswordDto);
            return this._state;
        }

        public IMemento Save()
        {
            return new ConcreteMemento(this._state);
        }

        // Restores the Originator's state from a memento object.
        public void Restore(IMemento memento)
        {
            if (!(memento is ConcreteMemento))
            {
                throw new Exception("Unknown memento class " + memento.ToString());
            }

            this._state = memento.GetState();
        }
    }
}

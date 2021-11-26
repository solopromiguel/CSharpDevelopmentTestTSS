using Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.Person.Queries.GetPersonAll
{
  public  class PersonDto : IMapFrom<Domain.Entities.Persona>
    {
        public int IdPersona { get; set; }
        public string Apellidos { get; set; }
        public string Nombres { get; set; }
        public string FechaNacimiento { get; set; }
        public double Peso { get; set; }
    }
}

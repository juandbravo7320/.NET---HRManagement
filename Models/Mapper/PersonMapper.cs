using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRManagement.Models.DTO;
using HRManagement.Models.Entities;

namespace HRManagement.Models.Mapper
{
    public class PersonMapper
    {
        public PersonDTO mapToPersonDTO(Person person) {
            PersonDTO dto = new PersonDTO();
            dto.Name = person.Name;
            dto.Lastname = person.Lastname;
            dto.PersonalAddress = person.PersonalAddress;
            dto.Email = person.Email;
            dto.Phone = person.Phone;
            dto.Picture = person.Picture;
            return dto;
        }
    }
}
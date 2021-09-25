using System.Collections.Generic;

namespace CURSO_UDEMY_COGNIZANT_netcore31webapi.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Damage { get; set; }
        public List<Character> Characters { get; set; }
    }
}
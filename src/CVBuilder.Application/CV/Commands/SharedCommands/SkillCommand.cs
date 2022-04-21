using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBuilder.Application.CV.Commands.sharedCommands
{
    public class SkillCommand
    {
        public int CvId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
    }
}

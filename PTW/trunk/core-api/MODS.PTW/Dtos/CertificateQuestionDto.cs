using MODS.PTW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MODS.PTW.Dtos
{
    public class CertificateQuestionDto
    {
        public int ID { get; set; }
        public int CertificateID { get; set; }
        public int QuestionID { get; set; }
        public Nullable<bool> Answer { get; set; }
        public virtual Certificate Certificates { get; set; }
        public virtual Question CertificateQAs { get; set; }
    }
}

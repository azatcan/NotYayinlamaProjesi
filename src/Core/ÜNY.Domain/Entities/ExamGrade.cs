﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ÜNY.Domain.Entities
{
    public class ExamGrade
    {
        public Guid Id { get; set; }
        public Guid ExamId { get; set; }
        public virtual Exam Exam { get; set; }
        public Guid UserId { get; set; }
        public virtual Users User { get; set; }
        public decimal? Grade { get; set; }
    }
}

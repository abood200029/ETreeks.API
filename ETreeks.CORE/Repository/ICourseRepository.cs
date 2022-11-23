﻿using ETreeks.CORE.Data;
using ETreeks.CORE.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETreeks.CORE.Repository
{
    public interface ICourseRepository
    {
        public List<Course> GetByCatId(int cat_id);
        public List<CourseWithCategory> GetCourseWithCategory();
    }
}
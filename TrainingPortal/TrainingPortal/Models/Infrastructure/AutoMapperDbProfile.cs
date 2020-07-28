using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrainingPortal.DAL;
using TrainingPortalDomain;

namespace TrainingPortal.Models.Infrastructure
{
    public class AutoMapperDbProfile : Profile
    {
        public AutoMapperDbProfile()
        {
            //Reverse mapping
            CreateMap<RegisterModel, Student>();
            CreateMap<Student, RegisterModel>();

            CreateMap<StudentLoginModel, Student>();
            CreateMap<Student, StudentLoginModel>();

            CreateMap<Portal, Student>();
            CreateMap<Student, Portal>();

            //Course map
            CreateMap<CourseView, Course>();
            CreateMap<Course, CourseView>();

            //Payment
            CreateMap<PaymentTypeView, PaymentType>();
            CreateMap<PaymentType, PaymentTypeView>();

            CreateMap<StudentPortal, RegisterModel>();
            CreateMap<RegisterModel, StudentPortal>();

            //Enrollment
            CreateMap<EnrollmentView, Enrollment>();
            CreateMap<Enrollment, EnrollmentView>();



        }
    }
}
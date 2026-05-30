using System;
using System.Collections.Generic;
using System.Linq;
using PRN232.LMS.Repositories.Data;
using PRN232.LMS.Repositories.Entities;

namespace PRN232.LMS.API.Infrastructure;

public static class DbSeeder
{
    public static void Seed(LmsDbContext context)
    {
        if (context.Semesters.Any()) return; // Already seeded

        // 1. Seed Semesters
        var semesters = new List<Semester>
        {
            new() { SemesterName = "Spring 2024", StartDate = DateTime.Parse("2024-01-15"), EndDate = DateTime.Parse("2024-05-31") },
            new() { SemesterName = "Summer 2024", StartDate = DateTime.Parse("2024-06-01"), EndDate = DateTime.Parse("2024-08-31") },
            new() { SemesterName = "Fall 2024", StartDate = DateTime.Parse("2024-09-01"), EndDate = DateTime.Parse("2025-01-15") },
            new() { SemesterName = "Spring 2025", StartDate = DateTime.Parse("2025-01-20"), EndDate = DateTime.Parse("2025-05-31") },
            new() { SemesterName = "Fall 2025", StartDate = DateTime.Parse("2025-09-01"), EndDate = DateTime.Parse("2026-01-15") }
        };
        context.Semesters.AddRange(semesters);
        context.SaveChanges();

        // 2. Seed Subjects
        var subjects = new List<Subject>
        {
            new() { SubjectCode = "PRN211", SubjectName = "Basic Cross-Platform Application Programming With .NET", Credit = 3 },
            new() { SubjectCode = "PRN231", SubjectName = "Advanced Cross-Platform Application Programming With .NET", Credit = 3 },
            new() { SubjectCode = "PRN232", SubjectName = "Web API With .NET", Credit = 3 },
            new() { SubjectCode = "SWD391", SubjectName = "Software Architecture and Design", Credit = 3 },
            new() { SubjectCode = "SWE201", SubjectName = "Introduction to Software Engineering", Credit = 3 },
            new() { SubjectCode = "DBI202", SubjectName = "Database Systems", Credit = 3 },
            new() { SubjectCode = "MAE101", SubjectName = "Mathematics for Engineering", Credit = 3 },
            new() { SubjectCode = "OSG202", SubjectName = "Operating Systems", Credit = 3 },
            new() { SubjectCode = "NWC203", SubjectName = "Computer Networking", Credit = 3 },
            new() { SubjectCode = "WDP301", SubjectName = "Web Design & Development", Credit = 3 }
        };
        context.Subjects.AddRange(subjects);
        context.SaveChanges();

        // 3. Seed Courses
        var courses = new List<Course>
        {
            new() { CourseName = "PRN211 - Spring 2024", SemesterId = semesters[0].SemesterId },
            new() { CourseName = "PRN232 - Spring 2024", SemesterId = semesters[0].SemesterId },
            new() { CourseName = "SWD391 - Spring 2024", SemesterId = semesters[0].SemesterId },
            new() { CourseName = "DBI202 - Spring 2024", SemesterId = semesters[0].SemesterId },

            new() { CourseName = "PRN231 - Summer 2024", SemesterId = semesters[1].SemesterId },
            new() { CourseName = "SWE201 - Summer 2024", SemesterId = semesters[1].SemesterId },
            new() { CourseName = "MAE101 - Summer 2024", SemesterId = semesters[1].SemesterId },
            new() { CourseName = "WDP301 - Summer 2024", SemesterId = semesters[1].SemesterId },

            new() { CourseName = "PRN232 - Fall 2024", SemesterId = semesters[2].SemesterId },
            new() { CourseName = "OSG202 - Fall 2024", SemesterId = semesters[2].SemesterId },
            new() { CourseName = "NWC203 - Fall 2024", SemesterId = semesters[2].SemesterId },
            new() { CourseName = "DBI202 - Fall 2024", SemesterId = semesters[2].SemesterId },

            new() { CourseName = "PRN231 - Spring 2025", SemesterId = semesters[3].SemesterId },
            new() { CourseName = "PRN232 - Spring 2025", SemesterId = semesters[3].SemesterId },
            new() { CourseName = "SWD391 - Spring 2025", SemesterId = semesters[3].SemesterId },
            new() { CourseName = "WDP301 - Spring 2025", SemesterId = semesters[3].SemesterId },

            new() { CourseName = "PRN211 - Fall 2025", SemesterId = semesters[4].SemesterId },
            new() { CourseName = "PRN232 - Fall 2025", SemesterId = semesters[4].SemesterId },
            new() { CourseName = "MAE101 - Fall 2025", SemesterId = semesters[4].SemesterId },
            new() { CourseName = "NWC203 - Fall 2025", SemesterId = semesters[4].SemesterId }
        };
        context.Courses.AddRange(courses);
        context.SaveChanges();

        // 4. Seed Students (50)
        var students = new List<Student>();
        for (int i = 1; i <= 50; i++)
        {
            students.Add(new Student
            {
                FullName = $"Nguyen Van Student {i}",
                Email = $"student{i}@lms.edu.vn",
                DateOfBirth = DateTime.Parse("2000-01-01").AddYears(-18 - (i % 5)).AddDays(i * 7)
            });
        }
        context.Students.AddRange(students);
        context.SaveChanges();

        // 5. Seed Enrollments (500)
        var enrollments = new List<Enrollment>();
        var statuses = new[] { "Active", "Inactive", "Completed", "Dropped", "Pending" };
        var random = new Random(42); // Seed for deterministic results

        for (int sid = 1; sid <= 50; sid++)
        {
            var student = students[sid - 1];
            for (int j = 1; j <= 10; j++)
            {
                var courseIndex = ((sid + j - 2) % 20);
                var course = courses[courseIndex];
                enrollments.Add(new Enrollment
                {
                    StudentId = student.StudentId,
                    CourseId = course.CourseId,
                    EnrollDate = DateTime.Parse("2024-01-01").AddDays(sid * 3 + j),
                    Status = statuses[random.Next(statuses.Length)]
                });
            }
        }
        context.Enrollments.AddRange(enrollments);
        context.SaveChanges();
    }
}

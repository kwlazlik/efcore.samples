using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using AutoMapper.QueryableExtensions;
using Domain.School;
using EFC;

namespace TestProject.Core.ProjectToVsUseAsDataSourceResult
{
   internal class ProjectToVsUseAsDataSourceResult
   {
      public static void NestedViewModelsTest()
      {
         Mapper.Initialize(c =>
         {
            c.CreateMap<Exam, ExamViewModel>();
            c.CreateMap<Subject, SubjectViewModel>();
         });

         using (var context = new Context())
         {
            List<ExamViewModel> projectTo1 = context.Exams.ProjectTo<ExamViewModel>().ToList();
            List<ExamViewModel> useAsDataSource1 = context.Exams.UseAsDataSource().For<ExamViewModel>().ToList();

            // SELECT CASE
            // WHEN [dtoExam].[SubjectId] IS NULL
            // THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT)
            // END, [dtoExam.Subject].[Name], [dtoExam].[Time], [dtoExam].[Title]
            // FROM [Exams] AS [dtoExam]
            // LEFT JOIN [Subjects] AS [dtoExam.Subject] ON [dtoExam].[SubjectId] = [dtoExam.Subject].[Id]
         }

         using (var context = new Context())
         {
            List<ExamViewModel> projectTo1 = context.Exams.Where(e => e.Subject.Name.StartsWith("x")).ProjectTo<ExamViewModel>().ToList();
            List<ExamViewModel> useAsDataSource1 = context.Exams.Where(e => e.Subject.Name.StartsWith("x")).UseAsDataSource().For<ExamViewModel>().ToList();

            // SELECT CASE
            // WHEN [e].[SubjectId] IS NULL
            // THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT)
            // END, [e.Subject].[Name], [e].[Time], [e].[Title]
            // FROM [Exams] AS [e]
            // LEFT JOIN [Subjects] AS [e.Subject] ON [e].[SubjectId] = [e.Subject].[Id]
            // WHERE [e.Subject].[Name] LIKE N'x' + N'%' AND (LEFT([e.Subject].[Name], LEN(N'x')) = N'x')
         }

         using (var context = new Context())
         {
            //var projectTo1 = context.Exams.ProjectTo<ExamViewModel>().Where(e => e.Subject.Name.StartsWith("x")).ToList();
            // wywala się

            List<ExamViewModel> useAsDataSource1 = context.Exams.UseAsDataSource().For<ExamViewModel>().Where(e => e.Subject.Name.StartsWith("x")).ToList();

            // SELECT CASE
            // WHEN [e].[SubjectId] IS NULL
            // THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT)
            // END, [e.Subject].[Name], [e].[Time], [e].[Title]
            // FROM [Exams] AS [e]
            // LEFT JOIN [Subjects] AS [e.Subject] ON [e].[SubjectId] = [e.Subject].[Id]
            // WHERE [e.Subject].[Name] LIKE N'x' + N'%' AND (LEFT([e.Subject].[Name], LEN(N'x')) = N'x')
         }

         using (var context = new Context())
         {
            //var projectTo1 = context.Exams.ProjectTo<ExamViewModel>().OrderBy(e => e.Subject.Name).ToList();
            // wywala się

            List<ExamViewModel> useAsDataSource1 = context.Exams.UseAsDataSource().For<ExamViewModel>().OrderBy(e => e.Subject.Name).ToList();

            // SELECT CASE
            // WHEN [e].[SubjectId] IS NULL
            // THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT)
            // END, [e.Subject].[Name], [e].[Time], [e].[Title]
            // FROM [Exams] AS [e]
            // LEFT JOIN [Subjects] AS [e.Subject] ON [e].[SubjectId] = [e.Subject].[Id]
            // ORDER BY [e.Subject].[Name]
         }
      }

      public static void FlatViewModelsTest()
      {
         Mapper.Initialize(c =>
         {
            c.CreateMap<Exam, ExamViewModel2>();
         });

         using (var context = new Context())
         {
            List<ExamViewModel2> projectTo1 = context.Exams.ProjectTo<ExamViewModel2>().ToList();
            List<ExamViewModel2> useAsDataSource1 = context.Exams.UseAsDataSource().For<ExamViewModel2>().ToList();

            // SELECT [dtoExam.Subject].[Name] AS [SubjectName], [dtoExam].[Time], [dtoExam].[Title]
            // FROM [Exams] AS [dtoExam]
            // LEFT JOIN [Subjects] AS [dtoExam.Subject] ON [dtoExam].[SubjectId] = [dtoExam.Subject].[Id]
         }

         using (var context = new Context())
         {
            List<ExamViewModel2> projectTo1 = context.Exams.Where(e => e.Subject.Name == "x").ProjectTo<ExamViewModel2>().ToList();
            List<ExamViewModel2> useAsDataSource1 = context.Exams.Where(e => e.Subject.Name == "x").UseAsDataSource().For<ExamViewModel2>().ToList();

            // SELECT [e.Subject].[Name] AS [SubjectName], [e].[Time], [e].[Title]
            // FROM [Exams] AS [e]
            // LEFT JOIN [Subjects] AS [e.Subject] ON [e].[SubjectId] = [e.Subject].[Id]
            // WHERE [e.Subject].[Name] = N'x'
         }

         using (var context = new Context())
         {
            List<ExamViewModel2> projectTo1 = context.Exams.ProjectTo<ExamViewModel2>().Where(e => e.SubjectName == "x").ToList();

            // SELECT [dtoExam.Subject].[Name] AS [SubjectName], [dtoExam].[Time], [dtoExam].[Title]
            // FROM [Exams] AS [dtoExam]
            // LEFT JOIN [Subjects] AS [dtoExam.Subject] ON [dtoExam].[SubjectId] = [dtoExam.Subject].[Id]
            // WHERE [dtoExam.Subject].[Name] = N'x'

            List<ExamViewModel2> useAsDataSource1 = context.Exams.UseAsDataSource().For<ExamViewModel2>().Where(e => e.SubjectName == "x").ToList();

            // SELECT [e.Subject].[Name] AS [SubjectName], [e].[Time], [e].[Title]
            // FROM [Exams] AS [e]
            // LEFT JOIN [Subjects] AS [e.Subject] ON [e].[SubjectId] = [e.Subject].[Id]
            // WHERE [e.Subject].[Name] = N'x'
         }

         using (var context = new Context())
         {
            List<ExamViewModel2> projectTo1 = context.Exams.ProjectTo<ExamViewModel2>().OrderBy(e => e.SubjectName).ToList();

            // SELECT [dtoExam.Subject].[Name] AS [SubjectName], [dtoExam].[Time], [dtoExam].[Title]
            // FROM [Exams] AS [dtoExam]
            // LEFT JOIN [Subjects] AS [dtoExam.Subject] ON [dtoExam].[SubjectId] = [dtoExam.Subject].[Id]
            // ORDER BY [dtoExam.Subject].[Name]

            List<ExamViewModel2> useAsDataSource1 = context.Exams.UseAsDataSource().For<ExamViewModel2>().OrderBy(e => e.SubjectName).ToList();

            // SELECT [e.Subject].[Name] AS [SubjectName], [e].[Time], [e].[Title]
            // FROM [Exams] AS [e]
            // LEFT JOIN [Subjects] AS [e.Subject] ON [e].[SubjectId] = [e.Subject].[Id]
            // WHERE [e.Subject].[Name] = N'x'
         }
      }
   }
}

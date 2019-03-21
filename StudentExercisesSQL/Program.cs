using System;
using System.Collections.Generic;
using System.Linq;
namespace StudentExercisesSQL
{
    class Program
    {
        static void Main(string[] args)
        {
            Repository repository = new Repository();

            repository.AddNewExercise("Bangazon", "C#");

            List<Exercise> ExerciseList = repository.GetExercises();
            Console.WriteLine("All Exercises in the database");
            Console.WriteLine("====================================================");
            foreach (Exercise obj in ExerciseList) {
                Console.WriteLine($"{obj.Name} {obj.Language}");
            }
            

            List<Exercise> jsExerciseList = repository.JavaScriptExercises();
            Console.WriteLine();
            Console.WriteLine("Javascript Exercises");
            Console.WriteLine("====================================================");
            foreach (Exercise jsExercise in jsExerciseList) {
                Console.WriteLine($"{jsExercise.Name}: {jsExercise.Language}");
            }

            Console.WriteLine();

            List<Instructors> instructorList = repository.getInstructorsAndCohort();
            Console.WriteLine("List of Instructors and their cohorts");
            Console.WriteLine("====================================================");
            foreach (Instructors instructor in instructorList) {
                Console.WriteLine($"{instructor.FirstName} {instructor.LastName} Is Teaching Cohort: {instructor.CohortName}");
            }

            repository.AddNewInstructor("Hunter", "Metts", "@yerdTheVois", 1);
            repository.AssignNewExercise(1, 1);

            Console.ReadKey();

        }
    }
}

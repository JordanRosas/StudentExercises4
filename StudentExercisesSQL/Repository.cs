using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using StudentExercisesSQL;

namespace StudentExercisesSQL
{
    class Repository
    {
        public SqlConnection Connection {
            get {
                string _connectionString = "Server=DESKTOP-MU3EML4\\SQLEXPRESS;Database=studentExercises;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                return new SqlConnection(_connectionString);
            }
        }
        //===========================================
        //Query the database for all the Exercises.
        //===========================================
        public List<Exercise> GetExercises()
        {
            using (SqlConnection conn = Connection) {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand()) {
                    cmd.CommandText = "Select id, ExerciseName, ExerciseLanguage from Exercise";
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Exercise> exercises = new List<Exercise>();
                    while (reader.Read()) {

                        int ExerciseIdCol = reader.GetOrdinal("id");
                        int ExerciseIdVal = reader.GetInt32(ExerciseIdCol);

                        int ExerciseCol = reader.GetOrdinal("ExerciseName");
                        string ExerciseTitle = reader.GetString(ExerciseCol);

                        int ExerciseLanguage = reader.GetOrdinal("ExerciseLanguage");
                        string ExercisesLanguage = reader.GetString(ExerciseLanguage);

                        Exercise exercise = new Exercise
                        {
                            Id = ExerciseIdVal,
                            Name = ExerciseTitle,
                            Language = ExercisesLanguage
                        };
                        exercises.Add(exercise);
                    }
                    reader.Close();
                    return exercises;
                } 
            }

        }
        /*======================================================
         * Find all the exercises in the database where the language is JavaScript.
         ========================================================*/
        public List<Exercise> JavaScriptExercises()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "select exercise.id, exercise.ExerciseName, exercise.ExerciseLanguage from exercise where exercise.ExerciseLanguage = 'Javascript'";
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Exercise> jsExercise = new List<Exercise>();

                    while (reader.Read()) {
                        int ExerciseIdCol = reader.GetOrdinal("id");
                        int ExerciseIdVal = reader.GetInt32(ExerciseIdCol);

                        int ExerciseCol = reader.GetOrdinal("ExerciseName");
                        string ExerciseTitle = reader.GetString(ExerciseCol);

                        int ExerciseLanguage = reader.GetOrdinal("ExerciseLanguage");
                        string ExercisesLanguage = reader.GetString(ExerciseLanguage);

                        Exercise jsExercises = new Exercise
                        {
                            Id = ExerciseIdVal,
                            Name = ExerciseTitle,
                            Language = ExercisesLanguage
                        };
                        jsExercise.Add(jsExercises);
                    }
                    reader.Close();
                    return jsExercise;

                }
            }
        }
        /*======================================================
         * Insert a new exercise into the database.
         ======================================================*/
        public void AddNewExercise(string eName, string eLang)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"INSERT INTO exercise (ExerciseName, ExerciseLanguage) Values ('{eName}', '{eLang}')";
                    cmd.ExecuteNonQuery();
                }

            }
        }
        /*=========================================================
         * Find all instructors in the database. Include each instructor's cohort.
         ============================================================*/
        public List<Instructors> getInstructorsAndCohort() {
            using (SqlConnection conn = Connection) {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand()) {
                    cmd.CommandText = @"select Instructors.FirstName, Instructors.LastName, cohort.[Name] 
                                        from Instructors left join Cohort on Instructors.CohortId = cohort.Id";
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Instructors> InstructorsAndCohorts = new List<Instructors>();
                    while (reader.Read()) {

                        int instructorFirstNamePos = reader.GetOrdinal("FirstName");
                        string instructorNameValue = reader.GetString(instructorFirstNamePos);

                        int instructorLastNamePos = reader.GetOrdinal("LastName");
                        string instructorLastNameValue = reader.GetString(instructorLastNamePos);

                        int instructorCohortPos = reader.GetOrdinal("Name");
                        string instructorCohort = reader.GetString(instructorCohortPos);

                        Instructors instructor = new Instructors
                        {
                            FirstName = instructorNameValue,
                            LastName = instructorLastNameValue,
                            CohortName = instructorCohort
                        };
                        InstructorsAndCohorts.Add(instructor);
                    }
                    reader.Close();
                    return InstructorsAndCohorts;
                }
            }
        }
        /*========================================================================================
         * Insert a new instructor into the database. Assign the instructor to an existing cohort.
         =========================================================================================*/
        public void AddNewInstructor(string FirstName, string LastName, string slackHandle, int cohortId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $@"INSERT INTO Instructors (FirstName, LastName, SlackHandle, CohortId)
                                        Values 
                                        ('{FirstName}', '{LastName}', '{slackHandle}', '{cohortId}')";
                    cmd.ExecuteNonQuery();
                }

            }
        }
        /*============================================================
        * Assign an existing exercise to an existing student.
        =============================================================*/
        public void AssignNewExercise(int studentId, int exerciseId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $@"INSERT INTO StudentExercise (StudentId, ExerciseId)
                                        Values 
                                        ({studentId}, {exerciseId})";
                    cmd.ExecuteNonQuery();
                }

            }
        }


    }
}

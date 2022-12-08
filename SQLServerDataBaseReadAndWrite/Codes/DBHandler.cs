using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


internal class DBHandler
{
    //HUSK SELV OG ÆNDRE CONNECTIONSTRINGEN ELLER FÅR DU IKKE NOGET
    

    public string? ConnectionString
    {
        #warning HUSK OG ÆNDRE DIN CONNECTION STRING;
        
        get => "INGEN CONNECTION STRING LIGE NU";

        
    }

    public List<string?> GetAllDoctors()
    {
        List<string?> doctorsString = new();
        using (SqlConnection connection = new SqlConnection(ConnectionString))
        {
            string? cmd =
                            @"SELECT CONCAT('Johnny Sins har ', COUNT(*), ' Patienter') AS [Patienter per læge]
                            FROM Patients As P
                            Join Doctor as D on P.DoctorId = D.Id
                            WHERE D.FirstName = 'Johnny' AND D.LastName = 'Sins' AND D.Id = 1
                            UNION
                            SELECT CONCAT('Mike Oxsmoll har ', COUNT(*), ' Patienter')
                            FROM Patients As P
                            Join Doctor as D on P.DoctorId = D.Id
                            WHERE D.FirstName = 'Mike' AND D.LastName = 'Oxsmoll' AND D.Id = 2
                            UNION
                            SELECT CONCAT('Stephen Einstein har ', COUNT(*), ' Patienter')
                            FROM Patients As P
                            Join Doctor as D on P.DoctorId = D.Id
                            WHERE D.FirstName = 'Stephen' AND D.LastName = 'Einstein' AND D.Id = 3
                            UNION 
                            SELECT CONCAT('Harry Cox har ', COUNT(*), ' Patienter')
                            FROM Patients As P
                            Join Doctor as D on P.DoctorId = D.Id
                            WHERE D.FirstName = 'Harry' AND D.LastName = 'Cox' AND D.Id = 4
                            UNION
                            SELECT CONCAT('Ben Dover har ', COUNT(*), ' Patienter')
                            FROM Patients As P
                            Join Doctor as D on P.DoctorId = D.Id
                            WHERE D.FirstName = 'Ben' AND D.LastName = 'Dover' AND D.Id = 5
                            UNION
                            SELECT CONCAT('Mikeal Jacksonnn har ', COUNT(*), ' Patienter')
                            FROM Patients As P
                            Join Doctor as D on P.DoctorId = D.Id
                            WHERE D.FirstName = 'Mikeal' AND D.LastName = 'Jacksonnn' AND D.Id = 6
                            UNION
                            SELECT CONCAT('Elvis Pringles har ', COUNT(*), ' Patienter')
                            FROM Patients As P
                            Join Doctor as D on P.DoctorId = D.Id
                            WHERE D.FirstName = 'Elvis' AND D.LastName = 'Pringles' AND D.Id = 7
                            UNION
                            SELECT CONCAT('Dickens Cider har ', COUNT(*), ' Patienter')
                            FROM Patients As P
                            Join Doctor as D on P.DoctorId = D.Id
                            WHERE D.FirstName = 'Dickens' AND D.LastName = 'Cider' AND D.Id = 8
                            ";
            SqlCommand command = new SqlCommand(cmd, connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string doctorString = reader.GetString(0);
                doctorsString.Add(doctorString);
            }
            connection.Close();
        }

        return doctorsString;
    }
    public void AddPatient(string? fullName, string doctorFirstName, string doctorLastName)
    {
        using (SqlConnection connection = new SqlConnection(ConnectionString))
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = $"USE Hospital;" +
                $"INSERT INTO Patients VALUES ('{fullName}', (SELECT Doctor.Id FROM Doctor WHERE Doctor.FirstName = '{doctorFirstName}' AND Doctor.LastName = '{doctorLastName}'))";
            cmd.Connection = connection;

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }
    public List<PatientModel> ShowPatientInfoAndDoctorInfo()
    {
        List<PatientModel> patients = new();
        using (SqlConnection connection = new SqlConnection(ConnectionString))
        {
            string? cmd =
                $"USE Hospital;" +
                $"SELECT Patients.FullName, Doctor.FirstName, Doctor.LastName, Specialties.SpecialtyName AS [Doctor Specialty] FROM Patients " +
                $"JOIN Doctor " +
                $"ON Patients.DoctorId = Doctor.Id " +
                $"JOIN Specialties " +
                $"ON Doctor.SpecialtyId= Specialties.Id";
            SqlCommand command = new SqlCommand(cmd, connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                DoctorModel doctor = new() { FirstName = reader.GetString(1), LastName = reader.GetString(2), SpecialtyName = reader.GetString(3) };
                PatientModel patient = new() { FullName = reader.GetString(0), Doctor = doctor };
                patients.Add(patient);
            }
            connection.Close();
        }
        return patients;
    }
}


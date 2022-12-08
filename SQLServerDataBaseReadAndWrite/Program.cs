
using System.Diagnostics;

DBHandler dbHandler = new();


void WriteOutAllDoctors()
{
    Console.WriteLine("List of doctors ------------------------------------------------------");
    Console.WriteLine();
    List<string> doctorsString = dbHandler.GetAllDoctors();
    doctorsString.ForEach(doctorString => Console.WriteLine(doctorString));
    Console.WriteLine();
    Console.WriteLine("--------------------------------------------------------------------");
}
void AddPatient()
{
    Console.Write("Patient fullname: ");
    string? patientFullName = Console.ReadLine();

    Console.Write("Doctors fullname: ");
    string? doctorFullName = Console.ReadLine();

    string doctorFirstName = "";
    string doctorLastName = "";
    if (doctorFullName != null)
    {
        string[] doctorFullNameSplitByWhiteSpace = doctorFullName.Split(" ");
        doctorFirstName = doctorFullNameSplitByWhiteSpace.First();
        doctorLastName = doctorFullNameSplitByWhiteSpace.Last();
    }

    try
    {
        dbHandler.AddPatient(patientFullName, doctorFirstName, doctorLastName);
        //dbHandler.TilmeldElevTilEtFag_SQLFil(fornavn, efternavn, fag);
        PrintMessageToUser(patientFullName, doctorFullName);
    }
    catch (Exception exc)
    {
        Console.WriteLine(exc.Message);
    }
}

void PrintMessageToUser(string patientFullName, string doctorFullName)
{
    Console.WriteLine();
    Console.BackgroundColor = ConsoleColor.White;
    Console.ForegroundColor = ConsoleColor.Black;
    Console.WriteLine($"Patient {patientFullName} is now created in the Patient table with the doctor {doctorFullName}!");

    Console.ResetColor();
    Console.WriteLine();
}

void PrintAllPatients()
{
    List<PatientModel> patients = dbHandler.ShowPatientInfoAndDoctorInfo();
    foreach (PatientModel patient in patients)
    {
        Console.WriteLine($"{patient.FullName} with dr. {patient.Doctor?.LastName}");
    }
}

while (true)
{
    WriteOutAllDoctors();
    AddPatient();
    PrintAllPatients();

    Console.Write("WILL YOU EXIT OR NOT : [y/n] : ");
    string? shouldContinue = Console.ReadLine();

    if (shouldContinue == "n")
        Environment.Exit(0);
    else if (shouldContinue == "y")
        Console.Clear();
}


//ALEXANDER ER MED I GRUPPEN GIV OGSÅ HAM 12
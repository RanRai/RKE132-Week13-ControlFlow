using System.Data.SQLite;

//ReadData(CreateConnection());//see tekitatud funktsioon toob välja all kirjeldatud kohtadest all küsitud andmed
//InsertCustomer(CreateConnection());//ANDMETE SISESTAMINE, deaktiveeritud praegu
//RemoveCustomer(CreateConnection());//ANDMETE KUSTUTAMINE, deaktiveeritud praegu
FindCustomer(CreateConnection());

static SQLiteConnection CreateConnection()
{
    SQLiteConnection connection = new SQLiteConnection("Data Source=mydb.db; Version = 3; New = True; Compress = True;");

    try
    {
        connection.Open();
        //Console.WriteLine("DB found.");//PRAEGU DEAKTIVEERITUD
    }
    catch
    {
        Console.WriteLine("DB not found");
    }
    return connection;
}

static void ReadData(SQLiteConnection myConnection) //avab ühendse, loeb andmed maha, paneb baasi kinni, kuvab andmed
{
    Console.Clear();//puhastame konsooli
    SQLiteDataReader reader;//loome objekti(reader) kuhu hakkame andmeid salvestama RIDADE kaupa
    SQLiteCommand command;//käsk andmebaasile

    command = myConnection.CreateCommand();//luuakse päring, mis andmeid soovime
    command.CommandText = "SELECT rowid, * FROM customer";//anmeid mida soovime maha lugeda

    reader = command.ExecuteReader();//hakkame readerisse andmeid lugema ja salvestama, neid mida soovime. HAKKA LUGEMA JA SALVESTAMA ANDMEID

    while (reader.Read())//while tsükliga käib rida realt andmebaasi läbi, nii palju kui ridu on. Kui read on otsas, siis käks muutub false-ks.
    {
        string readerRowId = reader["rowid"].ToString();//Ei TÖÖTA ->string readerRowId = reader.GetString(0);
        string readerStringFirstName = reader.GetString(1);//saame eesnime igast reast
        string readerStringLastName = reader.GetString(2);//saame perenime igast reast
        string readerStringDoB = reader.GetString(3);//saame staatuse igast reast

        Console.WriteLine($"{readerRowId}. Full name: {readerStringFirstName} {readerStringLastName}; DoB: {readerStringDoB}");
    }
    myConnection.Close();//baasi sulgemine peale mahalugemist
}

static void InsertCustomer(SQLiteConnection myConnection)//KLIENTIDE LISAMINE.(andmete lisamine, baasi avamine)
{
    SQLiteCommand command;
    string fName, lName, dob;

    Console.WriteLine("Enter first name:");
    fName = Console.ReadLine();
    Console.WriteLine("Enter last name:");
    lName = Console.ReadLine();
    Console.WriteLine("Enter date of birth (mm-dd-yyyy):");
    dob = Console.ReadLine();

    command = myConnection.CreateCommand();
    command.CommandText = $"INSERT INTO customer(firstName, lastName, dateOfBirth) " +
    $"VALUES ('{fName}', '{lName}', '{dob}')";

    int rowInserted = command.ExecuteNonQuery();//lisab andmed tabelisse
    Console.WriteLine($"Row inserted: {rowInserted}");

    ReadData(myConnection);
}

static void RemoveCustomer(SQLiteConnection myConnection)//andmete kustutamine
{
    SQLiteCommand command;
    string idToDelete;
    Console.WriteLine("Enter an id to delete a customer:");
    idToDelete = Console.ReadLine();

    command = myConnection.CreateCommand();
    command.CommandText = $"DELETE FROM customer WHERE rowid = {idToDelete}";
    int rowRemoved = command.ExecuteNonQuery();
    Console.WriteLine($"{rowRemoved} was removed from the table customer.");

    ReadData(myConnection);
}

static void FindCustomer(SQLiteConnection myConnection)//minu pakutu sulgudes
{
    SQLiteDataReader reader;//loome objekti kuhu hakkame andmeid salvestama RIDADE kaupa
    SQLiteCommand command;
    string searchName;
    Console.WriteLine("Enter a first name to display customer data:");
    searchName = Console.ReadLine();//arvuti pakkus enne võrdusmäri oleva 
    command = myConnection.CreateCommand();
    command.CommandText = $"SELECT customer.rowid, customer.firstname, customer.lastname, status.statustype FROM customerStatus\r\n" +
        $"JOIN customer ON customer.rowid = customerStatus.customerid\r\n" +
        $"JOIN status ON status.rowid = customerStatus.statusId " +
        $"WHERE firstname LIKE '{searchName}'";
    reader = command.ExecuteReader();
    while (reader.Read())//while tsükliga käib rida realt andmebaasi läbi, nii palju kui ridu on. Kui read on otsas, siis käks muutub false-ks.
    {
        string readerRowId = reader["rowid"].ToString();//Ei TÖÖTA ->string readerRowId = reader.GetString(0);
        string readerStringFirstName = reader.GetString(1);//saame eesnime igast reast
        string readerStringLastName = reader.GetString(2);//saame perenime igast reast
        string readerStringStatus = reader.GetString(3);//saame staatuse igast reast

        Console.WriteLine($"Search result: ID: {readerRowId}. {readerStringFirstName} {readerStringLastName}. Status: {readerStringStatus}");
    }
    myConnection.Close();//baasi sulgemine peale mahalugemist
}
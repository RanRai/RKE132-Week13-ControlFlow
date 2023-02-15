//command.CommandText = "SELECT *FROM customer";


//reader = command.ExecuteReader();//hakkame readerisse andmeid lugema ja salvestama

//while (reader.Read())//while tsükliga käib rida realt andmebaasi läbi, nii palju kui ridu on
//{
//    string readerStringFirstName = reader.GetString(0);//saame eesnime igast reast
//    string readerStringLastName = reader.GetString(1);//saame perenime igast reast
//    string readerStringDoB = reader.GetString(2);//saame staatuse igast reast

//    Console.WriteLine($"Full name: {readerStringFirstName} {readerStringLastName}; DoB: {readerStringDoB}");
//}
//myConnection.Close();//baasi sulgemine peale mahalugemist


//KOGU KÄSK
//command.CommandText = "SELECT customer.firstname, customer.lastname, status.statustype FROM customerStatus\r\n" +//kopeerisin SQL lehelt
//    "JOIN customer ON customer.rowid = customerStatus.customerid\r\n" +//saan ees-, perenime ja staatuse
//    "JOIN status ON status.rowid = customerStatus.statusId\r\n" +//saan ees-, perenime ja staatuse
//    "ORDER BY status.statusType";//paneb statuse tähestikulisse järejekorda



//reader = command.ExecuteReader();//hakkame readerisse andmeid lugema ja salvestama

//while (reader.Read())//while tsükliga käib rida realt andmebaasi läbi, nii palju kui ridu on
//{
//    string readerStringFirstName = reader.GetString(0);//saame eesnime igast reast
//    string readerStringLastName = reader.GetString(1);//saame perenime igast reast
//    string readerStringStatus = reader.GetString(2);//saame staatuse igast reast

//    Console.WriteLine($"Rull name: {readerStringFirstName} {readerStringLastName}; Status: {readerStringStatus}");
//}
//myConnection.Close();//baasi sulgemine peale mahalugemist


//KLIENTIDE ID

command.CommandText = $"SELECT customer.rowid, customer.firstname, customer.lastname, status.statustype FROM customerStatus\r\n" +
       $"JOIN customer ON customer.rowid = customerStatus.customerid\r\n" +
       $"JOIN status ON status.rowid = customerStatus.statusId " +
       $"WHERE firstname LIKE '{searchName}'";
# Code

### Databasemanager
```cs
    DatabaseManager myManager = new DatabaseManager();
    //Returns UserID from all known Users
    myManager.GetDatabaseUsers();

    //Adds user to the List expects User record
    myManager.AddUser("useCase", "UserID/Username", "Password");

    //Removes a user form List via Index in Array
    myManager.RemoveUserAt(0);

    //espects user Object, dbCommand string, and a list off arguments [param2=value]
    myManager.ConnectWithUser(0);
```

#### Level Enum
```cs
public enum Level
{
    Read,
    Write,
    DropTable,
    CreateTable,
    Admin
}
```
#### User record

```cs
public record User
    {
        public required string UseCase { get; set; }
        public required string UserID { get; set; }
        public required string Password { get; set; }
    }
```

# Migration


### First Migration
```
dotnet ef migrations add InitialCreate
``` 

### Create database and schema
```
dotnet ef database update
```

### Model
```
public class Blog
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedTimestamp { get; set; }
}
```

### Add Migration
```
dotnet ef migrations add AddBlogCreatedTimestamp
```

### Update database
```
dotnet ef database update
```

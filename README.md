## CRUD Service

[![MIT License](https://img.shields.io/badge/License-MIT-green.svg)](https://github.com/DeadyCool/EngUtil.EF.CRUDService/blob/master/License.txt)

The CRUD-service provides simple crud operations for data transfer between data and business layer.

### Basic usage

The following code illustrates the basic implementation of the CRUD repository for EF Core.

For detailed usage of [EF Core click](https://github.com/dotnet/efcore).

## Prepare implementation

Bellow a example implementation.

### Entity
```cs the entity
  public class UserEntity : EntityBase
  {
      public string Name { get; set; }
      public string Surename { get; set; }
      public string? EMail { get; set; }
      public string StreetAddress { get; set; }
      public string Location { get; set; }
      public string State { get; set; }
      public string ZIPCode { get; set; }
      public string Password { get; set; }
      public DateTime DayOfBirth { get; set; }
      public ICollection<NewsEntity> News { get; set; }
      public ICollection<CommentEntity> Comments { get; set; }
  }
```
### DbContext
```cs
    public class NewspaperContext : DbContext
    {  
        public NewspaperContext(DbContextOptions options) 
            : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; }

        ...

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity
                    .HasMany(x => x.News)
                    .WithOne(x => x.Reporter)
                    .HasForeignKey(x => x.ReporterId)
                    .OnDelete(DeleteBehavior.Cascade);
            }

            ...
        }
```
### Model
```cs
public class UserModel : ModelBase
  {
      public string Name { get; set; }
      public string Surename { get; set; }
      public string EMail { get; set; }
      public string StreetAddress { get; set; }
      public string Location { get; set; }
      public string State { get; set; }
      public string ZIPCode { get; set; }
      public string Password { get; set; }
      public DateTime DayOfBirth { get; set; }   
  }
```
### DTO-Mapping
```cs
 public static partial class Dto
 {
     public static Expression<Func<UserEntity, UserModel>> ToUserModel =>
         x => new UserModel
         {
             Id = x.RecId,
             State = x.State,
             StreetAddress = x.StreetAddress,
             DayOfBirth = x.DayOfBirth,  
             Surename = x.Surename,  
             EMail = x.EMail,
             Location = x.Location,
             Name = x.Name,
             Password = x.Password,
             ZIPCode = x.ZIPCode,
             Created = x.Created,
             Updated = x.Updated,
             CreatedBy = x.CreatedBy,
             UpdatedBy = x.UpdatedBy
         };
     
     public static Expression<Func<UserModel, UserEntity>> ToUserEntity =>
         x => new UserEntity
         {
             RecId = x.Id,
             State = x.State,
             StreetAddress = x.StreetAddress,
             DayOfBirth = x.DayOfBirth,
             Surename = x.Surename,
             EMail = x.EMail,
             Location = x.Location,
             Name = x.Name,
             Password = x.Password,
             ZIPCode = x.ZIPCode,
             Created = x.Created,
             Updated = x.Updated,
             CreatedBy = x.CreatedBy,
             UpdatedBy = x.UpdatedBy
         };
 }
```
### Repository implementation
```cs
    public class UserRepository : Repository<UserEntity, UserModel>
    {
        public UserRepository(DbContextOptions<NewspaperContext> contextOptions) 
            : base(contextOptions)
        {
        }

        public override Expression<Func<UserModel, UserEntity>> AsEntityExpression => Dto.ToUserEntity;

        public override Expression<Func<UserEntity, UserModel>> AsModelExpression => Dto.ToUserModel;
    }
```

### Example dependency injection implementation

```cs
 public class Startup
 {
      public void ConfigureServices(IServiceCollection services)
      {
          // *** add DbContext ***
          services.AddDbContext<NewspaperContext>(options =>
          {      
              var connection = Configuration.GetConnectionString("MyConnection");
              options.UseSqlServer(connection);
          });
          ...

         // *** register repositories ***     
         services.AddScoped<IRepository<UserModel>, UserRepository>();
         services.AddScoped<IRepository<NewsModel>, NewsRepository>();
         services.AddScoped<IRepository<CommentModel>, CommentRepository>();
         ...

         // *** register business ***
         services.AddScoped<IUserManager, UserManager>();
         ...
        
      }
}
```

### Example business usage 
```
    public class UserManager : ManagerBase, IUserManager
    {
        private readonly IRepository<UserModel> _userRepository;

        public UserManager(
            IRepository<ContractModel> userRepository,
            ILogger<UserManager> logger) 
            : base(logger)
        {
            _userRepository = userRepository;
        }

        /// <inheritdoc/>
        public async Task<UserModel> AddUserAsync(UserModel user)
        {
            var newUser = await _contractRepository.InsertAsync(user);
            return newUser;            
        }
    
        ...
    }
```


using DulcesPro.DTOs;
using DulcesPro.Maper;
using DulcesPro.Models;
using DulcesPro.Repository;
using DulcesPro.Service;
using DulcesPro.Validetors;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DulcesContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//service

builder.Services.AddScoped<IDulcesService<IngredientDTO, IngredientPostDTO, IngredietUpdateDTO >, IngredientService>();
builder.Services.AddScoped<IDulcesService<RecipeDTO, RecipePostDTO, RecipeUpdateDTO>, RecipeService>();
builder.Services.AddScoped<IRecipeIngredientService, RecipeIngredientService>();

//Validators
builder.Services.AddScoped<IValidator<IngredientPostDTO>, IngredientInsertValidator>();
builder.Services.AddScoped<IValidator<IngredietUpdateDTO>, IngredientUpdateValidator>();
builder.Services.AddScoped<IValidator<RecipePostDTO>, RecipeInsertValidator>();
builder.Services.AddScoped<IValidator<RecipeUpdateDTO>, RecipeUpdateValidator>();
builder.Services.AddScoped <IValidator<RecipeIngredientPostDTO>, RecipeIngredientInsertValidator>();
builder.Services.AddScoped<IValidator<RecipeIngredientUpdateDTO>, RecipeIngredientUpdateValidator>();

//reposiroty

builder.Services.AddScoped<IDulcesRepository<Ingredient>, IngredientRepository>();
builder.Services.AddScoped<IDulcesRepository<Recipe>, RecipeRepository>();
builder.Services.AddScoped<IDulcesRepository<RecipeIngredient>, RecipeIngredientRepository>();

//mapper
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MappingProfile>();

});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

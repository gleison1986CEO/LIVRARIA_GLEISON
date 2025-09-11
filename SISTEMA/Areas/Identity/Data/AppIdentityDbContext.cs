﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using sistema.Models;

namespace sistema.Areas.Identity.Data;

public class AppIdentityDbContext : IdentityDbContext<AppUser>
{
  
    public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options) { }

    public DbSet<sistema.Models.Usuario>? Usuario { get; set; }
    public DbSet<sistema.Models.Dispositivo>? Dispositivo { get; set; }
    public DbSet<sistema.Models.Usuario>? UsuarioGet { get; set; }
    public DbSet<sistema.Models.Logs>? Logs { get; set; }
    public DbSet<sistema.Models.LogsInsert>? LogsInsert { get; set; }
    public DbSet<sistema.Models.DispositivoInsert>? DispositivoInsert { get; set; }
    public DbSet<sistema.Models.Datalog>? Datalog{ get; set; }
    public DbSet<sistema.Models.Identificacao> Identificacao { get; set; }
    public DbSet<sistema.Models.Ips> Ips { get; set; }
    public DbSet<sistema.Models.Estado> Estado{ get; set; }  
    public DbSet<sistema.Models.Cidade> Cidade{ get; set; }  
    public DbSet<sistema.Models.Mapa> Mapa{ get; set; } 
    public DbSet<sistema.Models.Local> Local{ get; set; }    
    public DbSet<sistema.Models.Desconto> Desconto{ get; set; }  
    public DbSet<sistema.Models.Sobre> Sobre { get; set; }  
    public DbSet<sistema.Models.Categoria> Categoria{ get; set; }   
    public DbSet<sistema.Models.SubCategoria> SubCategoria{ get; set; } 
    public DbSet<sistema.Models.Produto> Produto{ get; set; }   
    public DbSet<sistema.Models.Consoles> Consoles{ get; set; }   
    public DbSet<sistema.Models.Banner> Banner{ get; set; }  
    public DbSet<sistema.Models.Cupom> Cupom{ get; set; }  
    public DbSet<sistema.Models.Requisito> Requisito{ get; set; } 

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

    }

   
}